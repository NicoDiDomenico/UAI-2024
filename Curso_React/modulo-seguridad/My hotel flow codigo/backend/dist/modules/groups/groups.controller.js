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
Object.defineProperty(exports, "__esModule", { value: true });
exports.GroupsController = void 0;
const common_1 = require("@nestjs/common");
const passport_1 = require("@nestjs/passport");
const decorators_1 = require("../../common/decorators");
const guards_1 = require("../../common/guards");
const swagger_1 = require("@nestjs/swagger");
const groups_service_1 = require("./groups.service");
const dto_1 = require("./dto");
let GroupsController = class GroupsController {
    groupsService;
    constructor(groupsService) {
        this.groupsService = groupsService;
    }
    async create(createGroupDto) {
        return await this.groupsService.create(createGroupDto);
    }
    async findAll() {
        return await this.groupsService.findAll();
    }
    async findOne(id) {
        return await this.groupsService.findOne(id);
    }
    async getEffectiveActions(id) {
        const actions = await this.groupsService.getEffectiveActions(id);
        return { actions: Array.from(actions) };
    }
    async update(id, updateGroupDto) {
        return await this.groupsService.update(id, updateGroupDto);
    }
    async remove(id) {
        await this.groupsService.remove(id);
    }
    async setActions(id, dto) {
        return await this.groupsService.setActions(id, dto);
    }
    async setChildren(id, dto) {
        return await this.groupsService.setChildren(id, dto);
    }
    async seed() {
        await this.groupsService.seed();
        return { message: 'Groups seeded successfully' };
    }
};
exports.GroupsController = GroupsController;
__decorate([
    (0, common_1.Post)(),
    (0, decorators_1.Actions)('config.grupos.crear'),
    (0, swagger_1.ApiOperation)({ summary: 'Crear nuevo grupo' }),
    (0, swagger_1.ApiResponse)({
        status: 201,
        description: 'Grupo creado exitosamente',
    }),
    (0, swagger_1.ApiResponse)({
        status: 400,
        description: 'Bad Request - El grupo ya existe',
    }),
    __param(0, (0, common_1.Body)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [dto_1.CreateGroupDto]),
    __metadata("design:returntype", Promise)
], GroupsController.prototype, "create", null);
__decorate([
    (0, common_1.Get)(),
    (0, decorators_1.Actions)('config.grupos.listar'),
    (0, swagger_1.ApiOperation)({ summary: 'Listar todos los grupos' }),
    (0, swagger_1.ApiResponse)({
        status: 200,
        description: 'Lista de grupos con sus acciones y grupos hijos',
    }),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", Promise)
], GroupsController.prototype, "findAll", null);
__decorate([
    (0, common_1.Get)(':id'),
    (0, decorators_1.Actions)('config.grupos.listar'),
    (0, swagger_1.ApiOperation)({ summary: 'Obtener grupo por ID' }),
    (0, swagger_1.ApiResponse)({
        status: 200,
        description: 'Grupo encontrado',
    }),
    (0, swagger_1.ApiResponse)({
        status: 404,
        description: 'Grupo no encontrado',
    }),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number]),
    __metadata("design:returntype", Promise)
], GroupsController.prototype, "findOne", null);
__decorate([
    (0, common_1.Get)(':id/effective-actions'),
    (0, decorators_1.Actions)('config.grupos.listar'),
    (0, swagger_1.ApiOperation)({
        summary: 'Obtener acciones efectivas del grupo (recursivo)',
    }),
    (0, swagger_1.ApiResponse)({
        status: 200,
        description: 'Set de acciones efectivas incluyendo hijos',
    }),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number]),
    __metadata("design:returntype", Promise)
], GroupsController.prototype, "getEffectiveActions", null);
__decorate([
    (0, common_1.Patch)(':id'),
    (0, decorators_1.Actions)('config.grupos.modificar'),
    (0, swagger_1.ApiOperation)({ summary: 'Actualizar grupo' }),
    (0, swagger_1.ApiResponse)({
        status: 200,
        description: 'Grupo actualizado exitosamente',
    }),
    (0, swagger_1.ApiResponse)({
        status: 404,
        description: 'Grupo no encontrado',
    }),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __param(1, (0, common_1.Body)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number, dto_1.UpdateGroupDto]),
    __metadata("design:returntype", Promise)
], GroupsController.prototype, "update", null);
__decorate([
    (0, common_1.Delete)(':id'),
    (0, common_1.HttpCode)(common_1.HttpStatus.NO_CONTENT),
    (0, decorators_1.Actions)('config.grupos.eliminar'),
    (0, swagger_1.ApiOperation)({ summary: 'Eliminar grupo' }),
    (0, swagger_1.ApiResponse)({
        status: 204,
        description: 'Grupo eliminado exitosamente',
    }),
    (0, swagger_1.ApiResponse)({
        status: 404,
        description: 'Grupo no encontrado',
    }),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number]),
    __metadata("design:returntype", Promise)
], GroupsController.prototype, "remove", null);
__decorate([
    (0, common_1.Patch)(':id/actions'),
    (0, decorators_1.Actions)('config.grupos.asignarAcciones'),
    (0, swagger_1.ApiOperation)({ summary: 'Asignar acciones a un grupo' }),
    (0, swagger_1.ApiResponse)({
        status: 200,
        description: 'Acciones asignadas exitosamente',
    }),
    (0, swagger_1.ApiResponse)({
        status: 400,
        description: 'Una o más acciones no existen',
    }),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __param(1, (0, common_1.Body)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number, dto_1.SetGroupActionsDto]),
    __metadata("design:returntype", Promise)
], GroupsController.prototype, "setActions", null);
__decorate([
    (0, common_1.Patch)(':id/children'),
    (0, decorators_1.Actions)('config.grupos.asignarHijos'),
    (0, swagger_1.ApiOperation)({ summary: 'Asignar grupos hijos (composición)' }),
    (0, swagger_1.ApiResponse)({
        status: 200,
        description: 'Grupos hijos asignados exitosamente',
    }),
    (0, swagger_1.ApiResponse)({
        status: 400,
        description: 'Crearía un ciclo en la jerarquía o grupo hijo no encontrado',
    }),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __param(1, (0, common_1.Body)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number, dto_1.SetGroupChildrenDto]),
    __metadata("design:returntype", Promise)
], GroupsController.prototype, "setChildren", null);
__decorate([
    (0, common_1.Post)('seed'),
    (0, decorators_1.Public)(),
    (0, common_1.HttpCode)(common_1.HttpStatus.OK),
    (0, swagger_1.ApiOperation)({ summary: 'Poblar base de datos con grupos iniciales' }),
    (0, swagger_1.ApiResponse)({
        status: 200,
        description: 'Seed ejecutado exitosamente',
    }),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", Promise)
], GroupsController.prototype, "seed", null);
exports.GroupsController = GroupsController = __decorate([
    (0, swagger_1.ApiTags)('Groups'),
    (0, common_1.Controller)('groups'),
    (0, swagger_1.ApiBearerAuth)(),
    (0, common_1.UseGuards)((0, passport_1.AuthGuard)('jwt'), guards_1.ActionsGuard),
    __metadata("design:paramtypes", [groups_service_1.GroupsService])
], GroupsController);
//# sourceMappingURL=groups.controller.js.map