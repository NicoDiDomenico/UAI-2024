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
var GroupsService_1;
Object.defineProperty(exports, "__esModule", { value: true });
exports.GroupsService = void 0;
const common_1 = require("@nestjs/common");
const typeorm_1 = require("@nestjs/typeorm");
const typeorm_2 = require("typeorm");
const entities_1 = require("../../infra/database/entities");
const actions_service_1 = require("../actions/actions.service");
const logger_service_1 = require("../../common/logger/logger.service");
let GroupsService = GroupsService_1 = class GroupsService {
    groupRepo;
    actionsService;
    logger = new logger_service_1.LoggerService(GroupsService_1.name);
    constructor(groupRepo, actionsService) {
        this.groupRepo = groupRepo;
        this.actionsService = actionsService;
    }
    async create(dto) {
        const exists = await this.groupRepo.findOne({ where: { key: dto.key } });
        if (exists) {
            throw new common_1.BadRequestException(`Group with key '${dto.key}' already exists`);
        }
        const group = this.groupRepo.create(dto);
        const saved = await this.groupRepo.save(group);
        this.logger.log(`Group created: ${saved.key}`);
        return saved;
    }
    async findAll() {
        return await this.groupRepo.find({
            relations: ['actions', 'children'],
            order: { key: 'ASC' },
        });
    }
    async findOne(id) {
        const group = await this.groupRepo.findOne({
            where: { id },
            relations: ['actions', 'children'],
        });
        if (!group) {
            throw new common_1.NotFoundException(`Group with id ${id} not found`);
        }
        return group;
    }
    async findByKey(key, loadRelations = true) {
        return await this.groupRepo.findOne({
            where: { key },
            relations: loadRelations ? ['actions', 'children'] : [],
        });
    }
    async update(id, dto) {
        const group = await this.findOne(id);
        if (dto.key && dto.key !== group.key) {
            const exists = await this.groupRepo.findOne({ where: { key: dto.key } });
            if (exists) {
                throw new common_1.BadRequestException(`Group with key '${dto.key}' already exists`);
            }
        }
        Object.assign(group, dto);
        const updated = await this.groupRepo.save(group);
        this.logger.log(`Group updated: ${updated.key}`);
        return updated;
    }
    async remove(id) {
        const group = await this.findOne(id);
        await this.groupRepo.remove(group);
        this.logger.log(`Group deleted: ${group.key}`);
    }
    async setActions(id, dto) {
        const group = await this.findOne(id);
        const actions = await this.actionsService.findByKeys(dto.actionKeys);
        if (actions.length !== dto.actionKeys.length) {
            const foundKeys = actions.map((a) => a.key);
            const missingKeys = dto.actionKeys.filter((k) => !foundKeys.includes(k));
            throw new common_1.BadRequestException(`The following action keys were not found: ${missingKeys.join(', ')}`);
        }
        group.actions = actions;
        const saved = await this.groupRepo.save(group);
        this.logger.log(`Group '${saved.key}' assigned ${actions.length} action(s)`);
        return saved;
    }
    async setChildren(id, dto) {
        const parentGroup = await this.findOne(id);
        const children = [];
        for (const childKey of dto.childGroupKeys) {
            const child = await this.findByKey(childKey);
            if (!child) {
                throw new common_1.BadRequestException(`Child group '${childKey}' not found`);
            }
            children.push(child);
        }
        for (const child of children) {
            if (await this.wouldCreateCycle(parentGroup.id, child.id)) {
                throw new common_1.BadRequestException(`Cannot add '${child.key}' as child of '${parentGroup.key}': would create a cycle in hierarchy`);
            }
        }
        parentGroup.children = children;
        const saved = await this.groupRepo.save(parentGroup);
        this.logger.log(`Group '${saved.key}' assigned ${children.length} child group(s)`);
        return saved;
    }
    async wouldCreateCycle(parentId, childId) {
        if (parentId === childId) {
            return true;
        }
        const visited = new Set();
        const stack = [childId];
        while (stack.length > 0) {
            const currentId = stack.pop();
            if (visited.has(currentId)) {
                continue;
            }
            visited.add(currentId);
            if (currentId === parentId) {
                return true;
            }
            const current = await this.groupRepo.findOne({
                where: { id: currentId },
                relations: ['children'],
            });
            if (current?.children) {
                for (const child of current.children) {
                    if (!visited.has(child.id)) {
                        stack.push(child.id);
                    }
                }
            }
        }
        return false;
    }
    async getEffectiveActions(groupId) {
        const effectiveActions = new Set();
        const visited = new Set();
        await this.collectActions(groupId, effectiveActions, visited);
        return effectiveActions;
    }
    async collectActions(groupId, effectiveActions, visited) {
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
                await this.collectActions(child.id, effectiveActions, visited);
            }
        }
    }
    async seed() {
        const groupsToSeed = [
            {
                key: 'rol.cliente',
                name: 'Cliente',
                description: 'Usuario cliente con permisos básicos de reserva',
            },
            {
                key: 'rol.recepcionista',
                name: 'Recepcionista',
                description: 'Personal de recepción y atención al cliente',
            },
            {
                key: 'rol.admin',
                name: 'Administrador',
                description: 'Administrador del sistema con acceso completo',
            },
            {
                key: 'group.frontdesk',
                name: 'Front Desk',
                description: 'Operaciones de mostrador y recepción',
            },
        ];
        let createdCount = 0;
        let skippedCount = 0;
        this.logger.seed('Starting groups seed...');
        for (const groupDto of groupsToSeed) {
            try {
                const exists = await this.findByKey(groupDto.key, false);
                if (!exists) {
                    await this.create(groupDto);
                    createdCount++;
                }
                else {
                    skippedCount++;
                }
            }
            catch (error) {
                this.logger.error(`Error seeding group ${groupDto.key}: ${error.message}`, '');
            }
        }
        this.logger.success(`Groups seed completed: ${createdCount} created, ${skippedCount} skipped`);
        this.logger.seed('Assigning initial actions to groups...');
        await this.assignInitialActions();
        this.logger.success('Initial actions assigned successfully');
    }
    async assignInitialActions() {
        try {
            const adminGroup = await this.findByKey('rol.admin', false);
            if (adminGroup) {
                const allActions = await this.actionsService.findAll();
                const allActionKeys = allActions.map((action) => action.key);
                await this.setActions(adminGroup.id, { actionKeys: allActionKeys });
                this.logger.log(`Assigned ${allActionKeys.length} actions to rol.admin`);
            }
            const recepcionistaGroup = await this.findByKey('rol.recepcionista', false);
            if (recepcionistaGroup) {
                const recepcionistaActions = [
                    'reservas.listar',
                    'reservas.ver',
                    'reservas.crear',
                    'reservas.modificar',
                    'checkin.registrar',
                    'checkin.asignarHabitacion',
                    'checkout.calcularCargos',
                    'checkout.registrarPago',
                    'checkout.cerrar',
                    'habitaciones.listar',
                    'habitaciones.ver',
                    'habitaciones.cambiarEstado',
                    'clientes.listar',
                    'clientes.ver',
                    'clientes.crear',
                    'clientes.modificar',
                ];
                await this.setActions(recepcionistaGroup.id, {
                    actionKeys: recepcionistaActions,
                });
                this.logger.log(`Assigned ${recepcionistaActions.length} actions to rol.recepcionista`);
            }
            const clienteGroup = await this.findByKey('rol.cliente', false);
            if (clienteGroup) {
                const clienteActions = ['reservas.listar', 'reservas.ver'];
                await this.setActions(clienteGroup.id, {
                    actionKeys: clienteActions,
                });
                this.logger.log(`Assigned ${clienteActions.length} actions to rol.cliente`);
            }
        }
        catch (error) {
            this.logger.error(`Error assigning initial actions: ${error.message}`, '');
        }
    }
};
exports.GroupsService = GroupsService;
exports.GroupsService = GroupsService = GroupsService_1 = __decorate([
    (0, common_1.Injectable)(),
    __param(0, (0, typeorm_1.InjectRepository)(entities_1.GroupEntity)),
    __metadata("design:paramtypes", [typeorm_2.Repository,
        actions_service_1.ActionsService])
], GroupsService);
//# sourceMappingURL=groups.service.js.map