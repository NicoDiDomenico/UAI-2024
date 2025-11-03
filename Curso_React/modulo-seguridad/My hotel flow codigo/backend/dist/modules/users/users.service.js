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
var UsersService_1;
Object.defineProperty(exports, "__esModule", { value: true });
exports.UsersService = void 0;
const common_1 = require("@nestjs/common");
const typeorm_1 = require("@nestjs/typeorm");
const typeorm_2 = require("typeorm");
const entities_1 = require("../../infra/database/entities");
const services_1 = require("../../common/services");
const actions_service_1 = require("../actions/actions.service");
const groups_service_1 = require("../groups/groups.service");
const logger_service_1 = require("../../common/logger/logger.service");
let UsersService = UsersService_1 = class UsersService {
    userRepo;
    hashService;
    actionsService;
    groupsService;
    logger = new logger_service_1.LoggerService(UsersService_1.name);
    LOCKOUT_THRESHOLD;
    LOCKOUT_DURATION;
    constructor(userRepo, hashService, actionsService, groupsService) {
        this.userRepo = userRepo;
        this.hashService = hashService;
        this.actionsService = actionsService;
        this.groupsService = groupsService;
        this.LOCKOUT_THRESHOLD = 5;
        this.LOCKOUT_DURATION = 15 * 60 * 1000;
    }
    async create(dto) {
        const existingUser = await this.userRepo.findOne({
            where: [{ username: dto.username }, { email: dto.email }],
        });
        if (existingUser) {
            throw new common_1.BadRequestException('Username or email already exists');
        }
        const passwordHash = await this.hashService.hash(dto.password);
        const user = this.userRepo.create({
            ...dto,
            passwordHash,
            role: dto.role || 'cliente',
            isActive: dto.isActive ?? true,
        });
        const saved = await this.userRepo.save(user);
        this.logger.log(`User created: ${saved.username} (${saved.email}) - Role: ${saved.role}`);
        return saved;
    }
    async findAll(dto) {
        const { page = 1, limit = 10, search, role, isActive } = dto;
        const skip = (page - 1) * limit;
        const where = [];
        if (search) {
            where.push({ username: (0, typeorm_2.Like)(`%${search}%`) }, { email: (0, typeorm_2.Like)(`%${search}%`) }, { fullName: (0, typeorm_2.Like)(`%${search}%`) });
        }
        if (!search && (role !== undefined || isActive !== undefined)) {
            const filter = {};
            if (role)
                filter.role = role;
            if (isActive !== undefined)
                filter.isActive = isActive;
            where.push(filter);
        }
        if (search && (role || isActive !== undefined)) {
            const filters = [];
            const baseFilters = [
                { username: (0, typeorm_2.Like)(`%${search}%`) },
                { email: (0, typeorm_2.Like)(`%${search}%`) },
                { fullName: (0, typeorm_2.Like)(`%${search}%`) },
            ];
            baseFilters.forEach((baseFilter) => {
                const combined = { ...baseFilter };
                if (role)
                    combined.role = role;
                if (isActive !== undefined)
                    combined.isActive = isActive;
                filters.push(combined);
            });
            where.splice(0, where.length, ...filters);
        }
        const [data, total] = await this.userRepo.findAndCount({
            where: where.length > 0 ? where : undefined,
            relations: ['groups', 'actions'],
            select: {
                id: true,
                username: true,
                email: true,
                fullName: true,
                role: true,
                isActive: true,
                lastLoginAt: true,
                createdAt: true,
                updatedAt: true,
            },
            order: { createdAt: 'DESC' },
            skip,
            take: limit,
        });
        const totalPages = Math.ceil(total / limit);
        return {
            data,
            pagination: {
                page,
                limit,
                total,
                totalPages,
                hasNextPage: page < totalPages,
                hasPreviousPage: page > 1,
            },
        };
    }
    async findOne(id) {
        const user = await this.userRepo.findOne({
            where: { id },
            relations: ['groups', 'actions'],
        });
        if (!user) {
            throw new common_1.NotFoundException(`User with id ${id} not found`);
        }
        return user;
    }
    async getInheritedActions(userId) {
        const user = await this.userRepo.findOne({
            where: { id: userId },
            relations: ['groups', 'groups.actions'],
        });
        if (!user) {
            throw new common_1.NotFoundException(`User with id ${userId} not found`);
        }
        const inheritedActionsMap = new Map();
        for (const group of user.groups) {
            for (const action of group.actions) {
                inheritedActionsMap.set(action.id, action);
            }
        }
        return Array.from(inheritedActionsMap.values());
    }
    async findByEmail(email) {
        return await this.userRepo.findOne({
            where: { email },
            relations: ['groups', 'actions'],
        });
    }
    async findByUsername(username) {
        return await this.userRepo.findOne({
            where: { username },
            relations: ['groups', 'actions'],
        });
    }
    async update(id, dto) {
        const user = await this.findOne(id);
        if (dto.username && dto.username !== user.username) {
            const exists = await this.findByUsername(dto.username);
            if (exists) {
                throw new common_1.BadRequestException('Username already exists');
            }
        }
        if (dto.email && dto.email !== user.email) {
            const exists = await this.findByEmail(dto.email);
            if (exists) {
                throw new common_1.BadRequestException('Email already exists');
            }
        }
        if (dto.password) {
            user.passwordHash = await this.hashService.hash(dto.password);
        }
        Object.assign(user, {
            username: dto.username ?? user.username,
            email: dto.email ?? user.email,
            fullName: dto.fullName ?? user.fullName,
            isActive: dto.isActive ?? user.isActive,
        });
        const updated = await this.userRepo.save(user);
        this.logger.log(`User updated: ${updated.username}`);
        return updated;
    }
    async remove(id) {
        const user = await this.findOne(id);
        await this.userRepo.remove(user);
        this.logger.log(`User deleted: ${user.username}`);
    }
    async setGroups(id, dto) {
        const user = await this.findOne(id);
        const groups = [];
        for (const groupKey of dto.groupKeys) {
            const group = await this.groupsService.findByKey(groupKey);
            if (!group) {
                throw new common_1.BadRequestException(`Group '${groupKey}' not found`);
            }
            groups.push(group);
        }
        user.groups = groups;
        const saved = await this.userRepo.save(user);
        this.logger.log(`User '${saved.username}' assigned ${groups.length} group(s)`);
        return saved;
    }
    async setActions(id, dto) {
        const user = await this.findOne(id);
        const actions = await this.actionsService.findByKeys(dto.actionKeys);
        if (actions.length !== dto.actionKeys.length) {
            const foundKeys = actions.map((a) => a.key);
            const missingKeys = dto.actionKeys.filter((k) => !foundKeys.includes(k));
            throw new common_1.BadRequestException(`The following action keys were not found: ${missingKeys.join(', ')}`);
        }
        user.actions = actions;
        const saved = await this.userRepo.save(user);
        this.logger.log(`User '${saved.username}' assigned ${actions.length} action(s)`);
        return saved;
    }
    async resetPassword(id, dto) {
        const user = await this.findOne(id);
        user.passwordHash = await this.hashService.hash(dto.newPassword);
        user.failedLoginAttempts = 0;
        user.lockedUntil = undefined;
        await this.userRepo.save(user);
        this.logger.log(`Password reset for user: ${user.username}`);
    }
    async isLocked(userId) {
        const user = await this.findOne(userId);
        if (!user.lockedUntil) {
            return false;
        }
        if (user.lockedUntil < new Date()) {
            user.lockedUntil = undefined;
            user.failedLoginAttempts = 0;
            await this.userRepo.save(user);
            return false;
        }
        return true;
    }
    async incrementFailedAttempts(userId) {
        const user = await this.findOne(userId);
        user.failedLoginAttempts = (user.failedLoginAttempts || 0) + 1;
        if (user.failedLoginAttempts >= this.LOCKOUT_THRESHOLD) {
            user.lockedUntil = new Date(Date.now() + this.LOCKOUT_DURATION);
            this.logger.warn(`User '${user.username}' locked until ${user.lockedUntil.toISOString()}`);
        }
        await this.userRepo.save(user);
    }
    async resetFailedAttempts(userId) {
        const user = await this.findOne(userId);
        user.failedLoginAttempts = 0;
        user.lockedUntil = undefined;
        user.lastLoginAt = new Date();
        await this.userRepo.save(user);
    }
    async seed() {
        const usersToSeed = [
            {
                username: 'admin',
                email: 'admin@hotel.com',
                password: 'Admin123!',
                fullName: 'Administrador',
                isActive: true,
            },
            {
                username: 'recepcionista1',
                email: 'recepcionista1@hotel.com',
                password: 'Recep123!',
                fullName: 'María García',
                isActive: true,
            },
            {
                username: 'recepcionista2',
                email: 'recepcionista2@hotel.com',
                password: 'Recep123!',
                fullName: 'Carlos Rodríguez',
                isActive: true,
            },
            {
                username: 'cliente1',
                email: 'cliente1@hotel.com',
                password: 'Cliente123!',
                fullName: 'Juan Pérez',
                isActive: true,
            },
            {
                username: 'cliente2',
                email: 'cliente2@hotel.com',
                password: 'Cliente123!',
                fullName: 'Ana Martínez',
                isActive: true,
            },
            {
                username: 'cliente3',
                email: 'cliente3@hotel.com',
                password: 'Cliente123!',
                fullName: 'Luis Fernández',
                isActive: true,
            },
        ];
        let createdCount = 0;
        let skippedCount = 0;
        this.logger.seed('Starting users seed...');
        for (const userDto of usersToSeed) {
            try {
                const exists = await this.findByEmail(userDto.email);
                if (!exists) {
                    await this.create(userDto);
                    createdCount++;
                }
                else {
                    skippedCount++;
                }
            }
            catch (error) {
                this.logger.error(`Error seeding user ${userDto.username}: ${error.message}`, '');
            }
        }
        this.logger.success(`Users seed completed: ${createdCount} created, ${skippedCount} skipped`);
        this.logger.user('Assigning initial groups to users...');
        await this.assignInitialGroups();
        this.logger.success('Initial groups assigned successfully');
    }
    async assignInitialGroups() {
        try {
            const admin = await this.findByEmail('admin@hotel.com');
            const recepcionista1 = await this.findByEmail('recepcionista1@hotel.com');
            const recepcionista2 = await this.findByEmail('recepcionista2@hotel.com');
            const cliente1 = await this.findByEmail('cliente1@hotel.com');
            const cliente2 = await this.findByEmail('cliente2@hotel.com');
            const cliente3 = await this.findByEmail('cliente3@hotel.com');
            if (admin) {
                await this.setGroups(admin.id, { groupKeys: ['rol.admin'] });
            }
            if (recepcionista1) {
                await this.setGroups(recepcionista1.id, {
                    groupKeys: ['rol.recepcionista'],
                });
            }
            if (recepcionista2) {
                await this.setGroups(recepcionista2.id, {
                    groupKeys: ['rol.recepcionista'],
                });
            }
            if (cliente1) {
                await this.setGroups(cliente1.id, { groupKeys: ['rol.cliente'] });
            }
            if (cliente2) {
                await this.setGroups(cliente2.id, { groupKeys: ['rol.cliente'] });
            }
            if (cliente3) {
                await this.setGroups(cliente3.id, { groupKeys: ['rol.cliente'] });
            }
            this.logger.log('Initial groups assigned to seed users');
        }
        catch (error) {
            this.logger.error(`Error assigning initial groups: ${error.message}`);
        }
    }
};
exports.UsersService = UsersService;
exports.UsersService = UsersService = UsersService_1 = __decorate([
    (0, common_1.Injectable)(),
    __param(0, (0, typeorm_1.InjectRepository)(entities_1.UserEntity)),
    __metadata("design:paramtypes", [typeorm_2.Repository,
        services_1.HashService,
        actions_service_1.ActionsService,
        groups_service_1.GroupsService])
], UsersService);
//# sourceMappingURL=users.service.js.map