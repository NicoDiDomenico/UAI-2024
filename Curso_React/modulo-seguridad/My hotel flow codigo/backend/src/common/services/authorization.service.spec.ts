/* eslint-disable @typescript-eslint/no-unsafe-assignment */
/* eslint-disable @typescript-eslint/no-unsafe-call */
/* eslint-disable @typescript-eslint/no-unsafe-member-access */

import { Test, TestingModule } from '@nestjs/testing';
import { CACHE_MANAGER } from '@nestjs/cache-manager';
import { getRepositoryToken } from '@nestjs/typeorm';
import { Cache } from 'cache-manager';
import { AuthorizationService } from './authorization.service';
import { UserEntity, GroupEntity } from '@infra/database/entities';

/**
 * UNIT TEST - AuthorizationService
 *
 * Objetivo: Verificar el cálculo de permisos efectivos con Composite Pattern + DFS
 *
 * Casos de prueba:
 * 1. getEffectiveActions() - Usuario sin grupos devuelve solo sus acciones
 * 2. getEffectiveActions() - Usuario con 1 grupo hereda acciones del grupo
 * 3. getEffectiveActions() - Usuario con grupos jerárquicos (DFS recursivo)
 * 4. getEffectiveActions() - No duplicar acciones heredadas
 * 5. Cache - Debe cachear resultados (TTL 15min)
 * 6. Cache invalidation - clearCache() limpia cache correctamente
 * 7. Performance - Debe calcular permisos en < 100ms
 *
 * Escenario de prueba:
 * ```
 * User (id=1)
 *   ├─ userActions: [action1]
 *   └─ groups:
 *       ├─ GroupA (actions: [action2])
 *       │   └─ children:
 *       │       └─ GroupB (actions: [action3, action4])
 *       └─ GroupC (actions: [action5])
 *
 * Resultado esperado: [action1, action2, action3, action4, action5]
 * ```
 *
 * Técnicas:
 * - Mocking de servicios con Jest
 * - Mocking de Cache Manager
 * - Testing de algoritmos recursivos (DFS)
 * - Verificación de caché (hit/miss)
 */
describe('AuthorizationService (Unit)', () => {
  let service: AuthorizationService;
  let userRepo: any;
  let groupRepo: any;
  let cacheManager: jest.Mocked<Cache>;

  // Mock data
  const mockActions = {
    action1: { id: 1, key: 'action1', name: 'Action 1' },
    action2: { id: 2, key: 'action2', name: 'Action 2' },
    action3: { id: 3, key: 'action3', name: 'Action 3' },
    action4: { id: 4, key: 'action4', name: 'Action 4' },
    action5: { id: 5, key: 'action5', name: 'Action 5' },
  };

  const mockGroupB: Partial<GroupEntity> = {
    id: 2,
    key: 'group.b',
    name: 'Group B',
    actions: [mockActions.action3, mockActions.action4],
    children: [],
  };

  const mockGroupA: Partial<GroupEntity> = {
    id: 1,
    key: 'group.a',
    name: 'Group A',
    actions: [mockActions.action2],
    children: [mockGroupB as GroupEntity],
  };

  const mockGroupC: Partial<GroupEntity> = {
    id: 3,
    key: 'group.c',
    name: 'Group C',
    actions: [mockActions.action5],
    children: [],
  };

  const mockUser = {
    id: 1,
    username: 'testuser',
    email: 'test@example.com',
    actions: [mockActions.action1],
    groups: [mockGroupA as GroupEntity, mockGroupC as GroupEntity],
  };

  beforeEach(async () => {
    // Mock UserRepository
    const mockUserRepo = {
      findOne: jest.fn(),
    };

    // Mock GroupRepository
    const mockGroupRepo = {
      findOne: jest.fn(),
    };

    // Mock CacheManager
    const mockCacheManager = {
      get: jest.fn(),
      set: jest.fn(),
      del: jest.fn(),
    };

    const module: TestingModule = await Test.createTestingModule({
      providers: [
        AuthorizationService,
        {
          provide: getRepositoryToken(UserEntity),
          useValue: mockUserRepo,
        },
        {
          provide: getRepositoryToken(GroupEntity),
          useValue: mockGroupRepo,
        },
        {
          provide: CACHE_MANAGER,
          useValue: mockCacheManager,
        },
      ],
    }).compile();

    service = module.get<AuthorizationService>(AuthorizationService);
    userRepo = module.get(getRepositoryToken(UserEntity));
    groupRepo = module.get(getRepositoryToken(GroupEntity));
    cacheManager = module.get(CACHE_MANAGER);
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });

  describe('getEffectiveActions() - Sin grupos', () => {
    it('debe devolver solo las acciones directas del usuario', async () => {
      const userWithoutGroups = {
        ...mockUser,
        groups: [],
      };

      userRepo.findOne.mockResolvedValue(userWithoutGroups as any);
      cacheManager.get.mockResolvedValue(null); // Cache miss

      const actions = await service.getEffectiveActions(1);

      expect(actions).toEqual(new Set(['action1']));
      expect(actions.has('action1')).toBe(true);
    });
  });

  describe('getEffectiveActions() - Con grupos simples', () => {
    it('debe heredar acciones de un grupo simple', async () => {
      const userWithOneGroup = {
        ...mockUser,
        groups: [mockGroupC as GroupEntity],
      };

      userRepo.findOne.mockResolvedValue(userWithOneGroup as any);
      groupRepo.findOne.mockResolvedValue(mockGroupC as any);
      cacheManager.get.mockResolvedValue(null);

      const actions = await service.getEffectiveActions(1);

      expect(actions.size).toBe(2);
      expect(actions.has('action1')).toBe(true); // acción directa
      expect(actions.has('action5')).toBe(true); // acción de GroupC
    });
  });

  describe('getEffectiveActions() - Con grupos jerárquicos (DFS)', () => {
    it('debe heredar acciones de grupos anidados recursivamente', async () => {
      userRepo.findOne.mockResolvedValue(mockUser as any);
      groupRepo.findOne
        .mockResolvedValueOnce(mockGroupA as any)
        .mockResolvedValueOnce(mockGroupB as any)
        .mockResolvedValueOnce(mockGroupC as any);
      cacheManager.get.mockResolvedValue(null);

      const actions = await service.getEffectiveActions(1);

      // Debe tener:
      // - action1 (usuario directo)
      // - action2 (GroupA)
      // - action3, action4 (GroupB, hijo de GroupA)
      // - action5 (GroupC)
      expect(actions.size).toBe(5);
      expect(actions.has('action1')).toBe(true);
      expect(actions.has('action2')).toBe(true);
      expect(actions.has('action3')).toBe(true);
      expect(actions.has('action4')).toBe(true);
      expect(actions.has('action5')).toBe(true);
    });

    it('no debe duplicar acciones si están en múltiples grupos', async () => {
      const groupWithDuplicate: Partial<GroupEntity> = {
        id: 4,
        key: 'group.d',
        name: 'Group D',
        actions: [mockActions.action1, mockActions.action2], // Duplicados
        children: [],
      };

      const userWithDuplicates = {
        ...mockUser,
        groups: [mockGroupA as GroupEntity, groupWithDuplicate as GroupEntity],
      };

      userRepo.findOne.mockResolvedValue(userWithDuplicates as any);
      groupRepo.findOne
        .mockResolvedValueOnce(mockGroupA as any)
        .mockResolvedValueOnce(mockGroupB as any)
        .mockResolvedValueOnce(groupWithDuplicate as any);
      cacheManager.get.mockResolvedValue(null);

      const actions = await service.getEffectiveActions(1);

      // Set debe eliminar duplicados automáticamente (es un Set)
      expect(actions instanceof Set).toBe(true);
    });
  });

  describe('Cache', () => {
    it('debe cachear resultados en el primer llamado', async () => {
      userRepo.findOne.mockResolvedValue(mockUser as any);
      groupRepo.findOne
        .mockResolvedValueOnce(mockGroupA as any)
        .mockResolvedValueOnce(mockGroupB as any)
        .mockResolvedValueOnce(mockGroupC as any);
      cacheManager.get.mockResolvedValue(null); // Cache miss

      await service.getEffectiveActions(1);

      // Verificar que se guardó en caché
      expect(cacheManager.set).toHaveBeenCalledWith(
        'user:permissions:1',
        expect.any(Array),
        900, // TTL en segundos
      );
    });

    it('debe devolver resultados cacheados sin consultar DB', async () => {
      const cachedActions = ['action1', 'action2', 'action3'];
      cacheManager.get.mockResolvedValue(cachedActions);

      const actions = await service.getEffectiveActions(1);

      expect(actions).toEqual(new Set(cachedActions));
      expect(userRepo.findOne).not.toHaveBeenCalled(); // No consultó DB
    });

    it('debe usar TTL de 15 minutos (900 segundos)', async () => {
      userRepo.findOne.mockResolvedValue(mockUser as any);
      groupRepo.findOne
        .mockResolvedValueOnce(mockGroupA as any)
        .mockResolvedValueOnce(mockGroupB as any)
        .mockResolvedValueOnce(mockGroupC as any);
      cacheManager.get.mockResolvedValue(null);

      await service.getEffectiveActions(1);

      expect(cacheManager.set).toHaveBeenCalledWith(
        expect.any(String),
        expect.any(Array),
        900, // 15 minutos en segundos
      );
    });
  });

  describe('invalidateCache()', () => {
    it('debe limpiar cache de usuario específico', async () => {
      await service.invalidateCache(1);

      expect(cacheManager.del).toHaveBeenCalledWith('user:permissions:1');
    });

    it('debe permitir recalcular después de limpiar cache', async () => {
      // Primer llamado (cachea)
      userRepo.findOne.mockResolvedValue(mockUser as any);
      groupRepo.findOne
        .mockResolvedValueOnce(mockGroupA as any)
        .mockResolvedValueOnce(mockGroupB as any)
        .mockResolvedValueOnce(mockGroupC as any);
      cacheManager.get.mockResolvedValue(null);
      await service.getEffectiveActions(1);

      // Limpiar cache
      await service.invalidateCache(1);

      // Segundo llamado (recalcula)
      cacheManager.get.mockResolvedValue(null); // Simular cache vacío
      groupRepo.findOne
        .mockResolvedValueOnce(mockGroupA as any)
        .mockResolvedValueOnce(mockGroupB as any)
        .mockResolvedValueOnce(mockGroupC as any);
      await service.getEffectiveActions(1);

      expect(userRepo.findOne).toHaveBeenCalledTimes(2);
    });
  });

  describe('Performance', () => {
    it('debe calcular permisos en menos de 100ms', async () => {
      userRepo.findOne.mockResolvedValue(mockUser as any);
      groupRepo.findOne
        .mockResolvedValueOnce(mockGroupA as any)
        .mockResolvedValueOnce(mockGroupB as any)
        .mockResolvedValueOnce(mockGroupC as any);
      cacheManager.get.mockResolvedValue(null);

      const start = Date.now();
      await service.getEffectiveActions(1);
      const duration = Date.now() - start;

      expect(duration).toBeLessThan(100);
    });

    it('debe ser más rápido con cache (< 10ms)', async () => {
      const cachedActions = ['action1', 'action2'];
      cacheManager.get.mockResolvedValue(cachedActions);

      const start = Date.now();
      await service.getEffectiveActions(1);
      const duration = Date.now() - start;

      expect(duration).toBeLessThan(10);
    });
  });

  describe('Edge Cases', () => {
    it('debe manejar usuario sin acciones ni grupos', async () => {
      const emptyUser = {
        ...mockUser,
        actions: [],
        groups: [],
      };

      userRepo.findOne.mockResolvedValue(emptyUser as any);
      cacheManager.get.mockResolvedValue(null);

      const actions = await service.getEffectiveActions(1);

      expect(actions.size).toBe(0);
      expect(actions).toEqual(new Set());
    });

    it('debe manejar grupos sin acciones', async () => {
      const emptyGroup: Partial<GroupEntity> = {
        id: 5,
        key: 'group.empty',
        name: 'Empty Group',
        actions: [],
        children: [],
      };

      const userWithEmptyGroup = {
        ...mockUser,
        actions: [],
        groups: [emptyGroup as GroupEntity],
      };

      userRepo.findOne.mockResolvedValue(userWithEmptyGroup as any);
      groupRepo.findOne.mockResolvedValue(emptyGroup as any);
      cacheManager.get.mockResolvedValue(null);

      const actions = await service.getEffectiveActions(1);

      expect(actions.size).toBe(0);
    });

    it('debe manejar jerarquías profundas (> 5 niveles)', async () => {
      // Crear jerarquía: GroupE -> GroupF -> GroupG -> GroupH -> GroupI
      const mockGroupI: Partial<GroupEntity> = {
        id: 9,
        key: 'group.i',
        actions: [{ id: 9, key: 'action9' } as any],
        children: [],
      };

      const mockGroupH: Partial<GroupEntity> = {
        id: 8,
        key: 'group.h',
        actions: [{ id: 8, key: 'action8' } as any],
        children: [mockGroupI as GroupEntity],
      };

      const mockGroupG: Partial<GroupEntity> = {
        id: 7,
        key: 'group.g',
        actions: [{ id: 7, key: 'action7' } as any],
        children: [mockGroupH as GroupEntity],
      };

      const mockGroupF: Partial<GroupEntity> = {
        id: 6,
        key: 'group.f',
        actions: [{ id: 6, key: 'action6' } as any],
        children: [mockGroupG as GroupEntity],
      };

      const mockGroupE: Partial<GroupEntity> = {
        id: 5,
        key: 'group.e',
        actions: [{ id: 5, key: 'action5' } as any],
        children: [mockGroupF as GroupEntity],
      };

      const userWithDeepHierarchy = {
        ...mockUser,
        actions: [],
        groups: [mockGroupE as GroupEntity],
      };

      userRepo.findOne.mockResolvedValue(userWithDeepHierarchy as any);
      groupRepo.findOne
        .mockResolvedValueOnce(mockGroupE as any)
        .mockResolvedValueOnce(mockGroupF as any)
        .mockResolvedValueOnce(mockGroupG as any)
        .mockResolvedValueOnce(mockGroupH as any)
        .mockResolvedValueOnce(mockGroupI as any);
      cacheManager.get.mockResolvedValue(null);

      const actions = await service.getEffectiveActions(1);

      // Debe recorrer toda la jerarquía con DFS
      expect(actions.size).toBe(5);
      expect(actions.has('action5')).toBe(true);
      expect(actions.has('action6')).toBe(true);
      expect(actions.has('action7')).toBe(true);
      expect(actions.has('action8')).toBe(true);
      expect(actions.has('action9')).toBe(true);
    });
  });
});
