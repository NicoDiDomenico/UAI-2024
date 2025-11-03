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
var ActionsGuard_1;
Object.defineProperty(exports, "__esModule", { value: true });
exports.ActionsGuard = void 0;
const common_1 = require("@nestjs/common");
const core_1 = require("@nestjs/core");
const services_1 = require("../services");
let ActionsGuard = ActionsGuard_1 = class ActionsGuard {
    reflector;
    authorizationService;
    logger = new common_1.Logger(ActionsGuard_1.name);
    constructor(reflector, authorizationService) {
        this.reflector = reflector;
        this.authorizationService = authorizationService;
    }
    async canActivate(context) {
        const requiredActions = this.reflector.getAllAndOverride('actions', [context.getHandler(), context.getClass()]);
        if (!requiredActions || requiredActions.length === 0) {
            return true;
        }
        const isPublic = this.reflector.getAllAndOverride('isPublic', [
            context.getHandler(),
            context.getClass(),
        ]);
        if (isPublic) {
            return true;
        }
        const request = context.switchToHttp().getRequest();
        const user = request.user;
        if (!user) {
            this.logger.warn('ActionsGuard: No user found in request');
            throw new common_1.ForbiddenException('Access denied');
        }
        const hasPermission = await this.authorizationService.hasAllActions(user.id, requiredActions);
        if (!hasPermission) {
            this.logger.warn(`User ${user.username} (ID: ${user.id}) does not have required actions: ${requiredActions.join(', ')}`);
            throw new common_1.ForbiddenException(`Insufficient permissions. Required actions: ${requiredActions.join(', ')}`);
        }
        return true;
    }
};
exports.ActionsGuard = ActionsGuard;
exports.ActionsGuard = ActionsGuard = ActionsGuard_1 = __decorate([
    (0, common_1.Injectable)(),
    __metadata("design:paramtypes", [core_1.Reflector,
        services_1.AuthorizationService])
], ActionsGuard);
//# sourceMappingURL=actions.guard.js.map