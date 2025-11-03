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
var UsersController_1;
Object.defineProperty(exports, "__esModule", { value: true });
exports.UsersController = void 0;
const common_1 = require("@nestjs/common");
const passport_1 = require("@nestjs/passport");
const decorators_1 = require("../../common/decorators");
const guards_1 = require("../../common/guards");
const users_service_1 = require("./users.service");
const dto_1 = require("./dto");
let UsersController = UsersController_1 = class UsersController {
    usersService;
    logger = new common_1.Logger(UsersController_1.name);
    constructor(usersService) {
        this.usersService = usersService;
    }
    async create(dto) {
        this.logger.log(`Creating user: ${dto.username}`);
        return await this.usersService.create(dto);
    }
    async findAll(query) {
        return await this.usersService.findAll(query);
    }
    async findOne(id) {
        return await this.usersService.findOne(id);
    }
    async getInheritedActions(id) {
        this.logger.log(`Getting inherited actions for user ${id}`);
        return await this.usersService.getInheritedActions(id);
    }
    async update(id, dto) {
        this.logger.log(`Updating user: ${id}`);
        return await this.usersService.update(id, dto);
    }
    async remove(id) {
        this.logger.log(`Deleting user: ${id}`);
        await this.usersService.remove(id);
    }
    async setGroups(id, dto) {
        this.logger.log(`Setting groups for user: ${id}`);
        return await this.usersService.setGroups(id, dto);
    }
    async setActions(id, dto) {
        this.logger.log(`Setting actions for user: ${id}`);
        return await this.usersService.setActions(id, dto);
    }
    async resetPassword(id, dto) {
        this.logger.log(`Resetting password for user: ${id}`);
        await this.usersService.resetPassword(id, dto);
    }
    async seed() {
        this.logger.log('Seeding users...');
        await this.usersService.seed();
    }
};
exports.UsersController = UsersController;
__decorate([
    (0, common_1.Post)(),
    (0, decorators_1.Actions)('config.usuarios.crear'),
    __param(0, (0, common_1.Body)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [dto_1.CreateUserDto]),
    __metadata("design:returntype", Promise)
], UsersController.prototype, "create", null);
__decorate([
    (0, common_1.Get)(),
    (0, decorators_1.Actions)('config.usuarios.listar'),
    __param(0, (0, common_1.Query)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [dto_1.FindAllUsersDto]),
    __metadata("design:returntype", Promise)
], UsersController.prototype, "findAll", null);
__decorate([
    (0, common_1.Get)(':id'),
    (0, decorators_1.Actions)('config.usuarios.listar'),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number]),
    __metadata("design:returntype", Promise)
], UsersController.prototype, "findOne", null);
__decorate([
    (0, common_1.Get)(':id/inherited-actions'),
    (0, decorators_1.Actions)('config.usuarios.listar'),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number]),
    __metadata("design:returntype", Promise)
], UsersController.prototype, "getInheritedActions", null);
__decorate([
    (0, common_1.Patch)(':id'),
    (0, decorators_1.Actions)('config.usuarios.modificar'),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __param(1, (0, common_1.Body)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number, dto_1.UpdateUserDto]),
    __metadata("design:returntype", Promise)
], UsersController.prototype, "update", null);
__decorate([
    (0, common_1.Delete)(':id'),
    (0, common_1.HttpCode)(common_1.HttpStatus.NO_CONTENT),
    (0, decorators_1.Actions)('config.usuarios.eliminar'),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number]),
    __metadata("design:returntype", Promise)
], UsersController.prototype, "remove", null);
__decorate([
    (0, common_1.Patch)(':id/groups'),
    (0, decorators_1.Actions)('config.usuarios.asignarGrupos'),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __param(1, (0, common_1.Body)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number, dto_1.SetUserGroupsDto]),
    __metadata("design:returntype", Promise)
], UsersController.prototype, "setGroups", null);
__decorate([
    (0, common_1.Patch)(':id/actions'),
    (0, decorators_1.Actions)('config.usuarios.asignarAcciones'),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __param(1, (0, common_1.Body)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number, dto_1.SetUserActionsDto]),
    __metadata("design:returntype", Promise)
], UsersController.prototype, "setActions", null);
__decorate([
    (0, common_1.Post)(':id/reset-password'),
    (0, common_1.HttpCode)(common_1.HttpStatus.NO_CONTENT),
    (0, decorators_1.Actions)('config.usuarios.resetearClave'),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __param(1, (0, common_1.Body)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number, dto_1.ResetPasswordDto]),
    __metadata("design:returntype", Promise)
], UsersController.prototype, "resetPassword", null);
__decorate([
    (0, common_1.Post)('seed'),
    (0, decorators_1.Public)(),
    (0, common_1.HttpCode)(common_1.HttpStatus.NO_CONTENT),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", Promise)
], UsersController.prototype, "seed", null);
exports.UsersController = UsersController = UsersController_1 = __decorate([
    (0, common_1.Controller)('users'),
    (0, common_1.UseGuards)((0, passport_1.AuthGuard)('jwt'), guards_1.ActionsGuard),
    __metadata("design:paramtypes", [users_service_1.UsersService])
], UsersController);
//# sourceMappingURL=users.controller.js.map