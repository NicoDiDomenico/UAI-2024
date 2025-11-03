import { Repository } from 'typeorm';
import type { Cache } from 'cache-manager';
import { UserEntity, GroupEntity } from '@infra/database/entities';
export declare class AuthorizationService {
    private userRepo;
    private groupRepo;
    private cacheManager;
    private readonly logger;
    private readonly CACHE_TTL;
    constructor(userRepo: Repository<UserEntity>, groupRepo: Repository<GroupEntity>, cacheManager: Cache);
    getEffectiveActions(userId: number): Promise<Set<string>>;
    hasAction(userId: number, actionKey: string): Promise<boolean>;
    hasAllActions(userId: number, actionKeys: string[]): Promise<boolean>;
    hasAnyAction(userId: number, actionKeys: string[]): Promise<boolean>;
    invalidateCache(userId: number): Promise<void>;
    private collectGroupActions;
}
