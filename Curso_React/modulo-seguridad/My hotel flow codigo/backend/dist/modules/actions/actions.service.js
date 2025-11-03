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
var ActionsService_1;
Object.defineProperty(exports, "__esModule", { value: true });
exports.ActionsService = void 0;
const common_1 = require("@nestjs/common");
const typeorm_1 = require("@nestjs/typeorm");
const typeorm_2 = require("typeorm");
const entities_1 = require("../../infra/database/entities");
const logger_service_1 = require("../../common/logger/logger.service");
let ActionsService = ActionsService_1 = class ActionsService {
    actionRepo;
    logger = new logger_service_1.LoggerService(ActionsService_1.name);
    constructor(actionRepo) {
        this.actionRepo = actionRepo;
    }
    async create(dto) {
        const exists = await this.actionRepo.findOne({ where: { key: dto.key } });
        if (exists) {
            throw new common_1.BadRequestException(`Action with key '${dto.key}' already exists`);
        }
        const area = dto.area || this.extractArea(dto.key);
        const action = this.actionRepo.create({
            ...dto,
            area,
        });
        const saved = await this.actionRepo.save(action);
        this.logger.log(`Action created: ${saved.key}`);
        return saved;
    }
    async findAll() {
        return await this.actionRepo.find({
            order: { area: 'ASC', key: 'ASC' },
        });
    }
    async findOne(id) {
        const action = await this.actionRepo.findOne({ where: { id } });
        if (!action) {
            throw new common_1.NotFoundException(`Action with id ${id} not found`);
        }
        return action;
    }
    async findByKey(key) {
        return await this.actionRepo.findOne({ where: { key } });
    }
    async findByKeys(keys) {
        if (!keys || keys.length === 0) {
            return [];
        }
        return await this.actionRepo.find({
            where: { key: (0, typeorm_2.In)(keys) },
        });
    }
    async findByArea(area) {
        return await this.actionRepo.find({
            where: { area },
            order: { key: 'ASC' },
        });
    }
    async update(id, dto) {
        const action = await this.findOne(id);
        if (dto.key && dto.key !== action.key) {
            const exists = await this.actionRepo.findOne({ where: { key: dto.key } });
            if (exists) {
                throw new common_1.BadRequestException(`Action with key '${dto.key}' already exists`);
            }
        }
        if (dto.key && !dto.area) {
            dto.area = this.extractArea(dto.key);
        }
        Object.assign(action, dto);
        const updated = await this.actionRepo.save(action);
        this.logger.log(`Action updated: ${updated.key}`);
        return updated;
    }
    async remove(id) {
        const action = await this.findOne(id);
        await this.actionRepo.remove(action);
        this.logger.log(`Action deleted: ${action.key}`);
    }
    extractArea(key) {
        const parts = key.split('.');
        return parts[0] || 'general';
    }
    async seed() {
        const actionsToSeed = [
            {
                key: 'reservas.listar',
                name: 'Listar Reservas',
                description: 'Ver listado de reservas',
                area: 'reservas',
            },
            {
                key: 'reservas.ver',
                name: 'Ver Detalle de Reserva',
                description: 'Ver detalles de una reserva específica',
                area: 'reservas',
            },
            {
                key: 'reservas.crear',
                name: 'Crear Reserva',
                description: 'Crear nuevas reservas',
                area: 'reservas',
            },
            {
                key: 'reservas.modificar',
                name: 'Modificar Reserva',
                description: 'Modificar reservas existentes',
                area: 'reservas',
            },
            {
                key: 'reservas.cancelar',
                name: 'Cancelar Reserva',
                description: 'Cancelar reservas',
                area: 'reservas',
            },
            {
                key: 'checkin.registrar',
                name: 'Registrar Check-in',
                description: 'Realizar proceso de check-in',
                area: 'checkin',
            },
            {
                key: 'checkin.asignarHabitacion',
                name: 'Asignar Habitación',
                description: 'Asignar habitación en el check-in',
                area: 'checkin',
            },
            {
                key: 'checkout.calcularCargos',
                name: 'Calcular Cargos',
                description: 'Calcular cargos finales en check-out',
                area: 'checkout',
            },
            {
                key: 'checkout.registrarPago',
                name: 'Registrar Pago',
                description: 'Registrar pagos en check-out',
                area: 'checkout',
            },
            {
                key: 'checkout.cerrar',
                name: 'Cerrar Check-out',
                description: 'Finalizar proceso de check-out',
                area: 'checkout',
            },
            {
                key: 'comprobantes.emitir',
                name: 'Emitir Comprobante',
                description: 'Emitir comprobantes fiscales',
                area: 'comprobantes',
            },
            {
                key: 'comprobantes.anular',
                name: 'Anular Comprobante',
                description: 'Anular comprobantes emitidos',
                area: 'comprobantes',
            },
            {
                key: 'comprobantes.imprimir',
                name: 'Imprimir Comprobante',
                description: 'Imprimir comprobantes',
                area: 'comprobantes',
            },
            {
                key: 'comprobantes.ver',
                name: 'Ver Comprobantes',
                description: 'Ver comprobantes emitidos',
                area: 'comprobantes',
            },
            {
                key: 'habitaciones.listar',
                name: 'Listar Habitaciones',
                description: 'Ver listado de habitaciones',
                area: 'habitaciones',
            },
            {
                key: 'habitaciones.ver',
                name: 'Ver Habitación',
                description: 'Ver detalles de una habitación',
                area: 'habitaciones',
            },
            {
                key: 'habitaciones.crear',
                name: 'Crear Habitación',
                description: 'Crear nuevas habitaciones',
                area: 'habitaciones',
            },
            {
                key: 'habitaciones.modificar',
                name: 'Modificar Habitación',
                description: 'Modificar habitaciones existentes',
                area: 'habitaciones',
            },
            {
                key: 'habitaciones.cambiarEstado',
                name: 'Cambiar Estado de Habitación',
                description: 'Cambiar estado de disponibilidad',
                area: 'habitaciones',
            },
            {
                key: 'clientes.listar',
                name: 'Listar Clientes',
                description: 'Ver listado de clientes',
                area: 'clientes',
            },
            {
                key: 'clientes.ver',
                name: 'Ver Cliente',
                description: 'Ver detalles de un cliente',
                area: 'clientes',
            },
            {
                key: 'clientes.crear',
                name: 'Crear Cliente',
                description: 'Registrar nuevos clientes',
                area: 'clientes',
            },
            {
                key: 'clientes.modificar',
                name: 'Modificar Cliente',
                description: 'Modificar datos de clientes',
                area: 'clientes',
            },
            {
                key: 'config.usuarios.listar',
                name: 'Listar Usuarios',
                description: 'Ver listado de usuarios del sistema',
                area: 'config',
            },
            {
                key: 'config.usuarios.crear',
                name: 'Crear Usuario',
                description: 'Crear nuevos usuarios',
                area: 'config',
            },
            {
                key: 'config.usuarios.modificar',
                name: 'Modificar Usuario',
                description: 'Modificar usuarios existentes',
                area: 'config',
            },
            {
                key: 'config.usuarios.eliminar',
                name: 'Eliminar Usuario',
                description: 'Eliminar usuarios',
                area: 'config',
            },
            {
                key: 'config.usuarios.resetearClave',
                name: 'Resetear Contraseña',
                description: 'Resetear contraseña de usuarios',
                area: 'config',
            },
            {
                key: 'config.usuarios.asignarGrupos',
                name: 'Asignar Grupos',
                description: 'Asignar grupos a usuarios',
                area: 'config',
            },
            {
                key: 'config.usuarios.asignarAcciones',
                name: 'Asignar Acciones',
                description: 'Asignar acciones individuales a usuarios',
                area: 'config',
            },
            {
                key: 'config.grupos.listar',
                name: 'Listar Grupos',
                description: 'Ver listado de grupos',
                area: 'config',
            },
            {
                key: 'config.grupos.crear',
                name: 'Crear Grupo',
                description: 'Crear nuevos grupos',
                area: 'config',
            },
            {
                key: 'config.grupos.modificar',
                name: 'Modificar Grupo',
                description: 'Modificar grupos existentes',
                area: 'config',
            },
            {
                key: 'config.grupos.eliminar',
                name: 'Eliminar Grupo',
                description: 'Eliminar grupos',
                area: 'config',
            },
            {
                key: 'config.grupos.asignarAcciones',
                name: 'Asignar Acciones a Grupo',
                description: 'Asignar acciones a grupos',
                area: 'config',
            },
            {
                key: 'config.grupos.asignarHijos',
                name: 'Asignar Grupos Hijos',
                description: 'Configurar jerarquía de grupos',
                area: 'config',
            },
            {
                key: 'config.acciones.listar',
                name: 'Listar Acciones',
                description: 'Ver catálogo de acciones',
                area: 'config',
            },
            {
                key: 'config.acciones.crear',
                name: 'Crear Acción',
                description: 'Crear nuevas acciones',
                area: 'config',
            },
            {
                key: 'config.acciones.modificar',
                name: 'Modificar Acción',
                description: 'Modificar acciones existentes',
                area: 'config',
            },
            {
                key: 'config.acciones.eliminar',
                name: 'Eliminar Acción',
                description: 'Eliminar acciones',
                area: 'config',
            },
        ];
        let createdCount = 0;
        let skippedCount = 0;
        this.logger.seed('Starting actions seed...');
        for (const actionDto of actionsToSeed) {
            try {
                const exists = await this.findByKey(actionDto.key);
                if (!exists) {
                    await this.create(actionDto);
                    createdCount++;
                }
                else {
                    skippedCount++;
                }
            }
            catch (error) {
                this.logger.error(`Error seeding action ${actionDto.key}: ${error.message}`, '');
            }
        }
        this.logger.success(`Actions seed completed: ${createdCount} created, ${skippedCount} skipped`);
    }
};
exports.ActionsService = ActionsService;
exports.ActionsService = ActionsService = ActionsService_1 = __decorate([
    (0, common_1.Injectable)(),
    __param(0, (0, typeorm_1.InjectRepository)(entities_1.ActionEntity)),
    __metadata("design:paramtypes", [typeorm_2.Repository])
], ActionsService);
//# sourceMappingURL=actions.service.js.map