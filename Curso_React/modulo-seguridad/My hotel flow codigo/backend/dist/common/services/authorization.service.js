"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
var AuthorizationService_1;
Object.defineProperty(exports, "__esModule", { value: true });
exports.AuthorizationService = void 0;
const common_1 = require("@nestjs/common");
const typeorm_1 = require("@nestjs/typeorm");
const typeorm_2 = require("typeorm");
const cache_manager_1 = require("@nestjs/cache-manager");
const entities_1 = require("../../infra/database/entities");
let AuthorizationService = AuthorizationService_1 = class AuthorizationService {
    userRepo;
    groupRepo;
    cacheManager;
    logger = new common_1.Logger(AuthorizationService_1.name);
    CACHE_TTL = 900;
    constructor(userRepo, groupRepo, cacheManager) {
        this.userRepo = userRepo;
        this.groupRepo = groupRepo;
        this.cacheManager = cacheManager;
    }
    async getEffectiveActions(userId) {
        const cacheKey = `user:permissions:${userId}`;
        const cached = await this.cacheManager.get(cacheKey);
        if (cached) {
            this.logger.debug(`Permissions cache HIT for user ${userId}`);
            return new Set(cached);
        }
        this.logger.debug(`Permissions cache MISS for user ${userId}`);
        const user = await this.userRepo.findOne({
            where: { id: userId },
            relations: ['groups', 'actions'],
        });
        if (!user) {
            return new Set();
        }
        const effectiveActions = new Set();
        for (const action of user.actions) {
            effectiveActions.add(action.key);
        }
        const visited = new Set();
        for (const group of user.groups) {
            await this.collectGroupActions(group.id, effectiveActions, visited);
        }
        const actionsArray = Array.from(effectiveActions);
        await this.cacheManager.set(cacheKey, actionsArray, this.CACHE_TTL);
        this.logger.debug(`User ${userId} has ${effectiveActions.size} effective action(s)`);
        return effectiveActions;
    }
    async hasAction(userId, actionKey) {
        const actions = await this.getEffectiveActions(userId);
        return actions.has(actionKey);
    }
    async hasAllActions(userId, actionKeys) {
        const actions = await this.getEffectiveActions(userId);
        for (const key of actionKeys) {
            if (!actions.has(key)) {
                this.logger.warn(`User ${userId} missing required action: ${key}`);
                return false;
            }
        }
        return true;
    }
    async hasAnyAction(userId, actionKeys) {
        const actions = await this.getEffectiveActions(userId);
        for (const key of actionKeys) {
            if (actions.has(key)) {
                return true;
            }
        }
        return false;
    }
    async invalidateCache(userId) {
        const cacheKey = `user:permissions:${userId}`;
        await this.cacheManager.del(cacheKey);
        this.logger.log(`Permissions cache invalidated for user ${userId}`);
    }
    async collectGroupActions(groupId, effectiveActions, visited) {
        if (visited.has(groupId)) {
            return;
        }
        visited.add(groupId);
        const group = await this.groupRepo.findOne({
            where: { id: groupId },
            relations: ['actions', 'children'],
        });
        if (!group) {
            return;
        }
        for (const action of group.actions) {
            effectiveActions.add(action.key);
        }
        if (group.children) {
            for (const child of group.children) {
                await this.collectGroupActions(child.id, effectiveActions, visited);
            }
        }
    }
};
exports.AuthorizationService = AuthorizationService;
exports.AuthorizationService = AuthorizationService = AuthorizationService_1 = __decorate([
    (0, common_1.Injectable)(),
    __param(0, (0, typeorm_1.InjectRepository)(entities_1.UserEntity)),
    __param(1, (0, typeorm_1.InjectRepository)(entities_1.GroupEntity)),
    __param(2, (0, common_1.Inject)(cache_manager_1.CACHE_MANAGER)),
    __metadata("design:paramtypes", [typeorm_2.Repository,
        typeorm_2.Repository, Object])
], AuthorizationService);
//# sourceMappingURL=authorization.service.js.map