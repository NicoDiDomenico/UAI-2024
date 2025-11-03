/* eslint-disable @typescript-eslint/no-unsafe-assignment */

/* eslint-disable @typescript-eslint/no-unsafe-member-access */
/* eslint-disable @typescript-eslint/no-unsafe-return */
import { Test, TestingModule } from '@nestjs/testing';
import { ConfigService } from '@nestjs/config';
import { JwtService } from '@nestjs/jwt';
import { getRepositoryToken } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { TokenService } from './token.service';
import { RevokedTokenEntity } from '@infra/database/entities';
import { UnauthorizedException } from '@nestjs/common';

/**
 * UNIT TEST - TokenService
 *
 * Objetivo: Verificar generación, verificación y revocación de JWT tokens
 *
 * Casos de prueba:
 * 1. generateTokenPair() - Genera access y refresh tokens válidos
 * 2. verifyToken() - Verifica tokens válidos correctamente
 * 3. verifyToken() - Rechaza tokens inválidos o expirados
 * 4. revokeToken() - Añade token a blacklist
 * 5. isRevoked() - Verifica si un token está revocado
 * 6. Token rotation - Refresh token se revoca al usarse
 * 7. Payload correcto - userId, username, email, jti, type
 *
 * Técnicas:
 * - Mocking de TypeORM Repository
 * - Mocking de ConfigService
 * - Testing de JWT (jsonwebtoken)
 * - Verificación de blacklist
 */
describe('TokenService (Unit)', () => {
  let service: TokenService;
  let revokedTokenRepo: jest.Mocked<Repository<RevokedTokenEntity>>;
  let configService: jest.Mocked<ConfigService>;
  let jwtService: JwtService;

  const mockConfig = {
    'jwt.secret': 'test-secret-key-12345',
    'jwt.accessExpiration': '15m',
    'jwt.refreshExpiration': '7d',
  };

  const mockUser = {
    id: 1,
    username: 'testuser',
    email: 'test@example.com',
  };

  beforeEach(async () => {
    // Mock del repository
    const mockRevokedTokenRepo = {
      create: jest.fn(),
      save: jest.fn(),
      findOne: jest.fn(),
      delete: jest.fn(),
      createQueryBuilder: jest.fn().mockReturnValue({
        delete: jest.fn().mockReturnThis(),
        where: jest.fn().mockReturnThis(),
        execute: jest.fn().mockResolvedValue({ affected: 0 }),
      }),
    };

    // Mock del ConfigService
    const mockConfigService = {
      get: jest.fn((key: string) => mockConfig[key]),
    };

    const module: TestingModule = await Test.createTestingModule({
      providers: [
        TokenService,
        {
          provide: JwtService,
          useValue: new JwtService({
            secret: 'test-secret-key-12345',
          }),
        },
        {
          provide: getRepositoryToken(RevokedTokenEntity),
          useValue: mockRevokedTokenRepo,
        },
        {
          provide: ConfigService,
          useValue: mockConfigService,
        },
      ],
    }).compile();

    service = module.get<TokenService>(TokenService);
    jwtService = module.get<JwtService>(JwtService);
    revokedTokenRepo = module.get(getRepositoryToken(RevokedTokenEntity));
    configService = module.get(ConfigService);
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });

  describe('generateTokenPair()', () => {
    it('debe generar access y refresh tokens', () => {
      const tokens = service.generateTokenPair(
        mockUser.id,
        mockUser.username,
        mockUser.email,
      );

      expect(tokens).toBeDefined();
      expect(tokens.accessToken).toBeDefined();
      expect(tokens.refreshToken).toBeDefined();
      expect(tokens.expiresIn).toBeDefined();
      expect(typeof tokens.accessToken).toBe('string');
      expect(typeof tokens.refreshToken).toBe('string');
    });

    it('debe incluir payload correcto en access token', async () => {
      const tokens = service.generateTokenPair(
        mockUser.id,
        mockUser.username,
        mockUser.email,
      );

      revokedTokenRepo.findOne.mockResolvedValue(null);
      const payload = await service.verifyToken(tokens.accessToken, 'access');

      expect(payload.sub).toBe(mockUser.id);
      expect(payload.username).toBe(mockUser.username);
      expect(payload.email).toBe(mockUser.email);
      expect(payload.type).toBe('access');
      expect(payload.jti).toBeDefined();
      expect(payload.iat).toBeDefined();
      expect(payload.exp).toBeDefined();
    });

    it('debe incluir payload correcto en refresh token', async () => {
      const tokens = service.generateTokenPair(
        mockUser.id,
        mockUser.username,
        mockUser.email,
      );

      revokedTokenRepo.findOne.mockResolvedValue(null);
      const payload = await service.verifyToken(tokens.refreshToken, 'refresh');

      expect(payload.sub).toBe(mockUser.id);
      expect(payload.type).toBe('refresh');
      expect(payload.jti).toBeDefined();
    });

    it('debe generar JTI únicos para cada token', async () => {
      const tokens1 = service.generateTokenPair(
        mockUser.id,
        mockUser.username,
        mockUser.email,
      );
      const tokens2 = service.generateTokenPair(
        mockUser.id,
        mockUser.username,
        mockUser.email,
      );

      revokedTokenRepo.findOne.mockResolvedValue(null);
      const payload1Access = await service.verifyToken(
        tokens1.accessToken,
        'access',
      );
      const payload1Refresh = await service.verifyToken(
        tokens1.refreshToken,
        'refresh',
      );
      const payload2Access = await service.verifyToken(
        tokens2.accessToken,
        'access',
      );

      expect(payload1Access.jti).not.toBe(payload1Refresh.jti);
      expect(payload1Access.jti).not.toBe(payload2Access.jti);
    });
  });

  describe('verifyToken()', () => {
    it('debe verificar access token válido', async () => {
      const tokens = service.generateTokenPair(
        mockUser.id,
        mockUser.username,
        mockUser.email,
      );

      revokedTokenRepo.findOne.mockResolvedValue(null);
      const payload = await service.verifyToken(tokens.accessToken, 'access');

      expect(payload).toBeDefined();
      expect(payload.sub).toBe(mockUser.id);
    });

    it('debe verificar refresh token válido', async () => {
      const tokens = service.generateTokenPair(
        mockUser.id,
        mockUser.username,
        mockUser.email,
      );

      revokedTokenRepo.findOne.mockResolvedValue(null);
      const payload = await service.verifyToken(tokens.refreshToken, 'refresh');

      expect(payload).toBeDefined();
      expect(payload.sub).toBe(mockUser.id);
    });

    it('debe rechazar token inválido', async () => {
      revokedTokenRepo.findOne.mockResolvedValue(null);
      await expect(
        service.verifyToken('invalid-token', 'access'),
      ).rejects.toThrow(UnauthorizedException);
    });

    it('debe rechazar token con firma incorrecta', async () => {
      const fakeToken =
        'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOjEsInR5cGUiOiJhY2Nlc3MifQ.fake-signature';

      revokedTokenRepo.findOne.mockResolvedValue(null);
      await expect(service.verifyToken(fakeToken, 'access')).rejects.toThrow(
        UnauthorizedException,
      );
    });

    it('debe rechazar token revocado', async () => {
      const tokens = service.generateTokenPair(
        mockUser.id,
        mockUser.username,
        mockUser.email,
      );

      const payload = jwtService.decode(tokens.accessToken);
      revokedTokenRepo.findOne.mockResolvedValue({
        jti: payload.jti,
      } as RevokedTokenEntity);

      await expect(
        service.verifyToken(tokens.accessToken, 'access'),
      ).rejects.toThrow(UnauthorizedException);
    });
  });

  describe('revokeToken()', () => {
    it('debe añadir token a la blacklist', async () => {
      const jti = 'test-jti-123';
      const userId = 1;
      const tokenType = 'access';
      const reason = 'User logout';

      revokedTokenRepo.create.mockReturnValue({
        jti,
        userId,
        tokenType,
        reason,
      } as RevokedTokenEntity);

      revokedTokenRepo.save.mockResolvedValue({
        jti,
        userId,
        tokenType,
        reason,
      } as RevokedTokenEntity);

      await service.revokeToken(jti, userId, tokenType, reason);

      expect(revokedTokenRepo.create).toHaveBeenCalled();
      expect(revokedTokenRepo.save).toHaveBeenCalled();
    });
  });

  describe('isRevoked()', () => {
    it('debe devolver true si el token está revocado', async () => {
      const jti = 'revoked-jti';
      revokedTokenRepo.findOne.mockResolvedValue({
        jti,
      } as RevokedTokenEntity);

      const isRevoked = await service.isRevoked(jti);

      expect(isRevoked).toBe(true);
      expect(revokedTokenRepo.findOne).toHaveBeenCalledWith({
        where: { jti },
      });
    });

    it('debe devolver false si el token NO está revocado', async () => {
      revokedTokenRepo.findOne.mockResolvedValue(null);

      const isRevoked = await service.isRevoked('valid-jti');

      expect(isRevoked).toBe(false);
    });
  });

  describe('Token Expiration', () => {
    it('access token debe tener expiracion de 15 minutos', async () => {
      const tokens = service.generateTokenPair(
        mockUser.id,
        mockUser.username,
        mockUser.email,
      );

      revokedTokenRepo.findOne.mockResolvedValue(null);
      const payload = await service.verifyToken(tokens.accessToken, 'access');

      const expirationTime = payload.exp - payload.iat;
      const fifteenMinutes = 15 * 60;

      // Verificar que la expiración sea aproximadamente 15 minutos
      expect(expirationTime).toBeGreaterThanOrEqual(fifteenMinutes - 5);
      expect(expirationTime).toBeLessThanOrEqual(fifteenMinutes + 5);
    });

    it('refresh token debe tener expiracion de 7 días', async () => {
      const tokens = service.generateTokenPair(
        mockUser.id,
        mockUser.username,
        mockUser.email,
      );

      revokedTokenRepo.findOne.mockResolvedValue(null);
      const payload = await service.verifyToken(tokens.refreshToken, 'refresh');

      const expirationTime = payload.exp - payload.iat;
      const sevenDays = 7 * 24 * 60 * 60;

      // Verificar que la expiración sea aproximadamente 7 días
      expect(expirationTime).toBeGreaterThanOrEqual(sevenDays - 60);
      expect(expirationTime).toBeLessThanOrEqual(sevenDays + 60);
    });
  });

  describe('Security', () => {
    it('no debe incluir información sensible en el payload', async () => {
      const tokens = service.generateTokenPair(
        mockUser.id,
        mockUser.username,
        mockUser.email,
      );

      revokedTokenRepo.findOne.mockResolvedValue(null);
      const payload = await service.verifyToken(tokens.accessToken, 'access');

      // El payload NO debe incluir password, passwordHash, etc.
      expect(payload).not.toHaveProperty('password');
      expect(payload).not.toHaveProperty('passwordHash');
    });
  });
});
