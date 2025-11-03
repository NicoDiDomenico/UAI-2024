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
exports.ActionsController = void 0;
const common_1 = require("@nestjs/common");
const passport_1 = require("@nestjs/passport");
const decorators_1 = require("../../common/decorators");
const guards_1 = require("../../common/guards");
const swagger_1 = require("@nestjs/swagger");
const actions_service_1 = require("./actions.service");
const dto_1 = require("./dto");
let ActionsController = class ActionsController {
    actionsService;
    constructor(actionsService) {
        this.actionsService = actionsService;
    }
    async create(createActionDto) {
        return await this.actionsService.create(createActionDto);
    }
    async findAll() {
        return await this.actionsService.findAll();
    }
    async findByArea(area) {
        return await this.actionsService.findByArea(area);
    }
    async findOne(id) {
        return await this.actionsService.findOne(id);
    }
    async update(id, updateActionDto) {
        return await this.actionsService.update(id, updateActionDto);
    }
    async remove(id) {
        await this.actionsService.remove(id);
    }
    async seed() {
        await this.actionsService.seed();
        return { message: 'Actions seeded successfully' };
    }
};
exports.ActionsController = ActionsController;
__decorate([
    (0, common_1.Post)(),
    (0, decorators_1.Actions)('config.acciones.crear'),
    (0, swagger_1.ApiOperation)({ summary: 'Crear nueva acción' }),
    (0, swagger_1.ApiResponse)({
        status: 201,
        description: 'Acción creada exitosamente',
    }),
    (0, swagger_1.ApiResponse)({
        status: 400,
        description: 'Bad Request - La acción ya existe',
    }),
    __param(0, (0, common_1.Body)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [dto_1.CreateActionDto]),
    __metadata("design:returntype", Promise)
], ActionsController.prototype, "create", null);
__decorate([
    (0, common_1.Get)(),
    (0, decorators_1.Actions)('config.acciones.listar'),
    (0, swagger_1.ApiOperation)({ summary: 'Listar todas las acciones' }),
    (0, swagger_1.ApiResponse)({
        status: 200,
        description: 'Lista de acciones',
    }),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", Promise)
], ActionsController.prototype, "findAll", null);
__decorate([
    (0, common_1.Get)('area/:area'),
    (0, decorators_1.Actions)('config.acciones.listar'),
    (0, swagger_1.ApiOperation)({ summary: 'Buscar acciones por área funcional' }),
    (0, swagger_1.ApiResponse)({
        status: 200,
        description: 'Acciones del área especificada',
    }),
    __param(0, (0, common_1.Param)('area')),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [String]),
    __metadata("design:returntype", Promise)
], ActionsController.prototype, "findByArea", null);
__decorate([
    (0, common_1.Get)(':id'),
    (0, decorators_1.Actions)('config.acciones.listar'),
    (0, swagger_1.ApiOperation)({ summary: 'Obtener acción por ID' }),
    (0, swagger_1.ApiResponse)({
        status: 200,
        description: 'Acción encontrada',
    }),
    (0, swagger_1.ApiResponse)({
        status: 404,
        description: 'Acción no encontrada',
    }),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number]),
    __metadata("design:returntype", Promise)
], ActionsController.prototype, "findOne", null);
__decorate([
    (0, common_1.Patch)(':id'),
    (0, decorators_1.Actions)('config.acciones.modificar'),
    (0, swagger_1.ApiOperation)({ summary: 'Actualizar acción' }),
    (0, swagger_1.ApiResponse)({
        status: 200,
        description: 'Acción actualizada exitosamente',
    }),
    (0, swagger_1.ApiResponse)({
        status: 404,
        description: 'Acción no encontrada',
    }),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __param(1, (0, common_1.Body)()),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number, dto_1.UpdateActionDto]),
    __metadata("design:returntype", Promise)
], ActionsController.prototype, "update", null);
__decorate([
    (0, common_1.Delete)(':id'),
    (0, common_1.HttpCode)(common_1.HttpStatus.NO_CONTENT),
    (0, decorators_1.Actions)('config.acciones.eliminar'),
    (0, swagger_1.ApiOperation)({ summary: 'Eliminar acción' }),
    (0, swagger_1.ApiResponse)({
        status: 204,
        description: 'Acción eliminada exitosamente',
    }),
    (0, swagger_1.ApiResponse)({
        status: 404,
        description: 'Acción no encontrada',
    }),
    __param(0, (0, common_1.Param)('id', common_1.ParseIntPipe)),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", [Number]),
    __metadata("design:returntype", Promise)
], ActionsController.prototype, "remove", null);
__decorate([
    (0, common_1.Post)('seed'),
    (0, decorators_1.Public)(),
    (0, common_1.HttpCode)(common_1.HttpStatus.OK),
    (0, swagger_1.ApiOperation)({ summary: 'Poblar base de datos con acciones iniciales' }),
    (0, swagger_1.ApiResponse)({
        status: 200,
        description: 'Seed ejecutado exitosamente',
    }),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", Promise)
], ActionsController.prototype, "seed", null);
exports.ActionsController = ActionsController = __decorate([
    (0, swagger_1.ApiTags)('Actions'),
    (0, common_1.Controller)('actions'),
    (0, swagger_1.ApiBearerAuth)(),
    (0, common_1.UseGuards)((0, passport_1.AuthGuard)('jwt'), guards_1.ActionsGuard),
    __metadata("design:paramtypes", [actions_service_1.ActionsService])
], ActionsController);
//# sourceMappingURL=actions.controller.js.map