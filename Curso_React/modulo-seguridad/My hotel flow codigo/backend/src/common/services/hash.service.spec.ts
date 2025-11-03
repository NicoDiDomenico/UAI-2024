import { Test, TestingModule } from '@nestjs/testing';
import { ConfigService } from '@nestjs/config';
import { HashService } from './hash.service';

/**
 * UNIT TEST - HashService
 *
 * Objetivo: Verificar que el servicio de hashing con Argon2id funciona correctamente
 *
 * Casos de prueba:
 * 1. hash() - Debe generar un hash válido a partir de un password
 * 2. hash() - El hash debe ser diferente cada vez (sal aleatoria)
 * 3. verify() - Debe validar correctamente un password correcto
 * 4. verify() - Debe rechazar un password incorrecto
 * 5. verify() - Debe rechazar un hash inválido
 *
 * Técnicas:
 * - Testing de servicios sin dependencias externas
 * - Verificación de algoritmos criptográficos
 * - Validación de seguridad (sal aleatoria, hash único)
 */
describe('HashService (Unit)', () => {
  let service: HashService;
  const testPassword = 'TestPassword123!';

  // Mock ConfigService
  const mockConfigService = {
    get: jest.fn((key: string, defaultValue?: any) => {
      const config: Record<string, any> = {
        'argon2.memoryCost': 65536, // 64MB
        'argon2.timeCost': 3,
        'argon2.parallelism': 4,
      };
      return config[key] !== undefined ? config[key] : defaultValue;
    }),
  };

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [
        HashService,
        {
          provide: ConfigService,
          useValue: mockConfigService,
        },
      ],
    }).compile();

    service = module.get<HashService>(HashService);
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });

  describe('hash()', () => {
    it('debe generar un hash válido para un password', async () => {
      const hash = await service.hash(testPassword);

      // Argon2id genera hashes con formato específico
      expect(hash).toBeDefined();
      expect(typeof hash).toBe('string');
      expect(hash).toContain('$argon2id$');
      expect(hash.length).toBeGreaterThan(50);
    });

    it('debe generar hashes diferentes cada vez (sal aleatoria)', async () => {
      const hash1 = await service.hash(testPassword);
      const hash2 = await service.hash(testPassword);

      // Los hashes deben ser diferentes porque Argon2 usa sal aleatoria
      expect(hash1).not.toBe(hash2);
    });

    // Nota: Argon2 permite hashear strings vacíos (es válido)
    // El DTOvalidation rechazará passwords vacíos antes de llegar al servicio
  });

  describe('verify()', () => {
    let validHash: string;

    beforeEach(async () => {
      validHash = await service.hash(testPassword);
    });

    it('debe validar correctamente un password correcto', async () => {
      const isValid = await service.verify(validHash, testPassword);
      expect(isValid).toBe(true);
    });

    it('debe rechazar un password incorrecto', async () => {
      const isValid = await service.verify(validHash, 'WrongPassword');
      expect(isValid).toBe(false);
    });

    it('debe rechazar un hash inválido', async () => {
      const isValid = await service.verify('invalid-hash', testPassword);
      expect(isValid).toBe(false);
    });

    it('debe rechazar password vacío', async () => {
      const isValid = await service.verify(validHash, '');
      expect(isValid).toBe(false);
    });

    it('debe ser case-sensitive', async () => {
      const isValid = await service.verify(
        validHash,
        testPassword.toLowerCase(),
      );
      expect(isValid).toBe(false);
    });
  });

  describe('Seguridad', () => {
    it('debe usar configuración segura de Argon2id', async () => {
      const hash = await service.hash(testPassword);

      // Verificar que usa Argon2id (versión más segura)
      expect(hash).toContain('$argon2id$');

      // Verificar versión Argon2
      expect(hash).toContain('v=19');

      // Verificar que tiene parámetros de memoria y tiempo
      expect(hash).toMatch(/m=\d+/); // Memory cost
      expect(hash).toMatch(/t=\d+/); // Time cost
      expect(hash).toMatch(/p=\d+/); // Parallelism
    });

    it('debe resistir timing attacks (tiempo constante)', async () => {
      const hash = await service.hash(testPassword);
      const iterations = 10; // Reducido de 100 a 10 para evitar timeout

      // Medir tiempo con password correcto
      const startCorrect = Date.now();
      for (let i = 0; i < iterations; i++) {
        await service.verify(hash, testPassword);
      }
      const timeCorrect = Date.now() - startCorrect;

      // Medir tiempo con password incorrecto
      const startWrong = Date.now();
      for (let i = 0; i < iterations; i++) {
        await service.verify(hash, 'WrongPassword');
      }
      const timeWrong = Date.now() - startWrong;

      // La diferencia de tiempo debe ser mínima (< 15%)
      // Esto previene timing attacks
      // Nota: Con pocas iteraciones (10) la varianza puede ser mayor
      const timeDiff = Math.abs(timeCorrect - timeWrong);
      const avgTime = (timeCorrect + timeWrong) / 2;
      const percentageDiff = (timeDiff / avgTime) * 100;

      expect(percentageDiff).toBeLessThan(15);
    }, 10000); // Timeout 10s
  });

  describe('Performance', () => {
    it('debe hashear un password en menos de 500ms', async () => {
      const start = Date.now();
      await service.hash(testPassword);
      const duration = Date.now() - start;

      // Argon2id configurado correctamente no debe ser ni muy rápido ni muy lento
      expect(duration).toBeLessThan(500);
      expect(duration).toBeGreaterThan(10); // Muy rápido = inseguro
    });

    it('debe verificar un password en menos de 500ms', async () => {
      const hash = await service.hash(testPassword);

      const start = Date.now();
      await service.verify(hash, testPassword);
      const duration = Date.now() - start;

      expect(duration).toBeLessThan(500);
    });
  });
});
