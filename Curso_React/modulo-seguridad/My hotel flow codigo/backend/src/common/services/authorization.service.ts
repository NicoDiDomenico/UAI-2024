import { Injectable, Logger, Inject } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { CACHE_MANAGER } from '@nestjs/cache-manager';
import type { Cache } from 'cache-manager';
import {
  UserEntity,
  GroupEntity,
  ActionEntity,
} from '@infra/database/entities';

/**
 * Servicio de Autorización
 * Patrón: Service Layer + Composite Pattern + Cache
 * Responsabilidad: Calcular permisos efectivos de usuarios usando composición de grupos
 */
@Injectable()
export class AuthorizationService {
  private readonly logger = new Logger(AuthorizationService.name);
  private readonly CACHE_TTL = 900; // 15 minutos

  constructor(
    @InjectRepository(UserEntity)
    private userRepo: Repository<UserEntity>,
    @InjectRepository(GroupEntity)
    private groupRepo: Repository<GroupEntity>,
    @Inject(CACHE_MANAGER)
    private cacheManager: Cache,
  ) {}

  /**
   * Obtener permisos efectivos de un usuario
   * Patrón: Composite - combina acciones propias + acciones de grupos (recursivo)
   * Usa caché para optimizar performance
   *
   * @param userId - ID del usuario
   * @returns Set de keys de acciones efectivas
   */
  async getEffectiveActions(userId: number): Promise<Set<string>> {
    const cacheKey = `user:permissions:${userId}`;

    // Intentar obtener del caché
    const cached = await this.cacheManager.get<string[]>(cacheKey);
    if (cached) {
      this.logger.debug(`Permissions cache HIT for user ${userId}`);
      return new Set(cached);
    }

    this.logger.debug(`Permissions cache MISS for user ${userId}`);

    // Cargar usuario con todas sus relaciones
    const user = await this.userRepo.findOne({
      where: { id: userId },
      relations: ['groups', 'actions'],
    });

    if (!user) {
      return new Set();
    }

    const effectiveActions = new Set<string>();

    // 1. Agregar acciones propias del usuario (excepciones)
    for (const action of user.actions) {
      effectiveActions.add(action.key);
    }

    // 2. Agregar acciones de todos los grupos (recursivo)
    const visited = new Set<number>();
    for (const group of user.groups) {
      await this.collectGroupActions(group.id, effectiveActions, visited);
    }

    // Guardar en caché
    const actionsArray = Array.from(effectiveActions);
    await this.cacheManager.set(cacheKey, actionsArray, this.CACHE_TTL);

    this.logger.debug(
      `User ${userId} has ${effectiveActions.size} effective action(s)`,
    );

    return effectiveActions;
  }

  /**
   * Verificar si un usuario tiene UNA acción específica
   *
   * @param userId - ID del usuario
   * @param actionKey - Key de la acción
   * @returns true si tiene el permiso, false si no
   */
  async hasAction(userId: number, actionKey: string): Promise<boolean> {
    const actions = await this.getEffectiveActions(userId);
    return actions.has(actionKey);
  }

  /**
   * Verificar si un usuario tiene TODAS las acciones especificadas
   *
   * @param userId - ID del usuario
   * @param actionKeys - Array de keys de acciones
   * @returns true si tiene todos los permisos, false si falta alguno
   */
  async hasAllActions(userId: number, actionKeys: string[]): Promise<boolean> {
    const actions = await this.getEffectiveActions(userId);

    for (const key of actionKeys) {
      if (!actions.has(key)) {
        this.logger.warn(`User ${userId} missing required action: ${key}`);
        return false;
      }
    }

    return true;
  }

  /**
   * Verificar si un usuario tiene AL MENOS UNA de las acciones especificadas
   *
   * @param userId - ID del usuario
   * @param actionKeys - Array de keys de acciones
   * @returns true si tiene al menos una, false si no tiene ninguna
   */
  async hasAnyAction(userId: number, actionKeys: string[]): Promise<boolean> {
    const actions = await this.getEffectiveActions(userId);

    for (const key of actionKeys) {
      if (actions.has(key)) {
        return true;
      }
    }

    return false;
  }

  /**
   * Invalidar caché de permisos de un usuario
   * Debe llamarse cuando cambian los grupos o acciones del usuario
   *
   * @param userId - ID del usuario
   */
  async invalidateCache(userId: number): Promise<void> {
    const cacheKey = `user:permissions:${userId}`;
    await this.cacheManager.del(cacheKey);
    this.logger.log(`Permissions cache invalidated for user ${userId}`);
  }

  /**
   * Método auxiliar recursivo para recolectar acciones de grupos
   * Usa DFS para recorrer la jerarquía de grupos
   * Patrón: Depth-First Search
   *
   * @param groupId - ID del grupo actual
   * @param effectiveActions - Set donde se acumulan las acciones
   * @param visited - Set de IDs de grupos ya visitados (evita ciclos)
   */
  private async collectGroupActions(
    groupId: number,
    effectiveActions: Set<string>,
    visited: Set<number>,
  ): Promise<void> {
    // Evitar procesar el mismo grupo dos veces
    if (visited.has(groupId)) {
      return;
    }

    visited.add(groupId);

    // Cargar grupo con sus relaciones
    const group = await this.groupRepo.findOne({
      where: { id: groupId },
      relations: ['actions', 'children'],
    });

    if (!group) {
      return;
    }

    // Agregar acciones propias del grupo
    for (const action of group.actions) {
      effectiveActions.add(action.key);
    }

    // Recursivamente agregar acciones de grupos hijos
    if (group.children) {
      for (const child of group.children) {
        await this.collectGroupActions(child.id, effectiveActions, visited);
      }
    }
  }
}
