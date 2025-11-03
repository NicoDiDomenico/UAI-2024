import {
  Injectable,
  NotFoundException,
  BadRequestException,
} from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository, Like, FindOptionsWhere } from 'typeorm';
import { UserEntity, ActionEntity } from '@infra/database/entities';
import { HashService } from '@common/services';
import { ActionsService } from '@modules/actions/actions.service';
import { GroupsService } from '@modules/groups/groups.service';
import { LoggerService } from '@common/logger/logger.service';
import {
  CreateUserDto,
  UpdateUserDto,
  SetUserGroupsDto,
  SetUserActionsDto,
  ResetPasswordDto,
  FindAllUsersDto,
} from './dto';

/**
 * Servicio de gestión de usuarios
 * Patrón: Service Layer + Repository
 * Responsabilidad: CRUD de usuarios + lockout + reset password + asignación grupos/acciones
 */
@Injectable()
export class UsersService {
  private readonly logger = new LoggerService(UsersService.name);
  private readonly LOCKOUT_THRESHOLD: number;
  private readonly LOCKOUT_DURATION: number;

  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepo: Repository<UserEntity>,
    private readonly hashService: HashService,
    private readonly actionsService: ActionsService,
    private readonly groupsService: GroupsService,
  ) {
    this.LOCKOUT_THRESHOLD = 5; // 5 intentos fallidos
    this.LOCKOUT_DURATION = 15 * 60 * 1000; // 15 minutos en ms
  }

  /**
   * Crear un nuevo usuario
   */
  async create(dto: CreateUserDto): Promise<UserEntity> {
    // Verificar que no exista el username o email
    const existingUser = await this.userRepo.findOne({
      where: [{ username: dto.username }, { email: dto.email }],
    });

    if (existingUser) {
      throw new BadRequestException('Username or email already exists');
    }

    // Hash de la contraseña
    const passwordHash = await this.hashService.hash(dto.password);

    const user = this.userRepo.create({
      ...dto,
      passwordHash,
      role: dto.role || 'cliente', // Default a cliente si no se especifica
      isActive: dto.isActive ?? true,
    });

    const saved = await this.userRepo.save(user);
    this.logger.log(`User created: ${saved.username} (${saved.email}) - Role: ${saved.role}`);

    return saved;
  }

  /**
   * Listar todos los usuarios con paginación, búsqueda y filtros
   */
  async findAll(dto: FindAllUsersDto): Promise<{
    data: UserEntity[];
    pagination: {
      page: number;
      limit: number;
      total: number;
      totalPages: number;
      hasNextPage: boolean;
      hasPreviousPage: boolean;
    };
  }> {
    const { page = 1, limit = 10, search, role, isActive } = dto;
    const skip = (page - 1) * limit;

    // Construir filtros
    const where: FindOptionsWhere<UserEntity>[] = [];

    if (search) {
      // Buscar en username, email o fullName
      where.push(
        { username: Like(`%${search}%`) },
        { email: Like(`%${search}%`) },
        { fullName: Like(`%${search}%`) },
      );
    }

    // Si no hay búsqueda pero hay otros filtros
    if (!search && (role !== undefined || isActive !== undefined)) {
      const filter: FindOptionsWhere<UserEntity> = {};
      if (role) filter.role = role;
      if (isActive !== undefined) filter.isActive = isActive;
      where.push(filter);
    }

    // Si hay búsqueda y otros filtros, combinarlos
    if (search && (role || isActive !== undefined)) {
      const filters: FindOptionsWhere<UserEntity>[] = [];
      const baseFilters = [
        { username: Like(`%${search}%`) },
        { email: Like(`%${search}%`) },
        { fullName: Like(`%${search}%`) },
      ];

      baseFilters.forEach((baseFilter) => {
        const combined: FindOptionsWhere<UserEntity> = { ...baseFilter };
        if (role) combined.role = role;
        if (isActive !== undefined) combined.isActive = isActive;
        filters.push(combined);
      });

      where.splice(0, where.length, ...filters);
    }

    // Ejecutar query con paginación
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
        // Omitir passwordHash por seguridad
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

  /**
   * Buscar usuario por ID
   */
  async findOne(id: number): Promise<UserEntity> {
    const user = await this.userRepo.findOne({
      where: { id },
      relations: ['groups', 'actions'],
    });

    if (!user) {
      throw new NotFoundException(`User with id ${id} not found`);
    }

    return user;
  }

  /**
   * Obtener acciones heredadas de los grupos del usuario
   * Esto permite al frontend filtrar acciones duplicadas
   */
  async getInheritedActions(userId: number): Promise<ActionEntity[]> {
    const user = await this.userRepo.findOne({
      where: { id: userId },
      relations: ['groups', 'groups.actions'],
    });

    if (!user) {
      throw new NotFoundException(`User with id ${userId} not found`);
    }

    // Recolectar todas las acciones de todos los grupos
    const inheritedActionsMap = new Map<number, ActionEntity>();
    
    for (const group of user.groups) {
      for (const action of group.actions) {
        inheritedActionsMap.set(action.id, action);
      }
    }

    // Convertir el Map a array para devolver acciones únicas
    return Array.from(inheritedActionsMap.values());
  }

  /**
   * Buscar usuario por email (usado en login)
   */
  async findByEmail(email: string): Promise<UserEntity | null> {
    return await this.userRepo.findOne({
      where: { email },
      relations: ['groups', 'actions'],
    });
  }

  /**
   * Buscar usuario por username
   */
  async findByUsername(username: string): Promise<UserEntity | null> {
    return await this.userRepo.findOne({
      where: { username },
      relations: ['groups', 'actions'],
    });
  }

  /**
   * Actualizar un usuario
   */
  async update(id: number, dto: UpdateUserDto): Promise<UserEntity> {
    const user = await this.findOne(id);

    // Verificar username único si se está cambiando
    if (dto.username && dto.username !== user.username) {
      const exists = await this.findByUsername(dto.username);
      if (exists) {
        throw new BadRequestException('Username already exists');
      }
    }

    // Verificar email único si se está cambiando
    if (dto.email && dto.email !== user.email) {
      const exists = await this.findByEmail(dto.email);
      if (exists) {
        throw new BadRequestException('Email already exists');
      }
    }

    // Si se proporciona nueva contraseña, hashearla
    if (dto.password) {
      user.passwordHash = await this.hashService.hash(dto.password);
    }

    // Actualizar otros campos
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

  /**
   * Eliminar un usuario
   */
  async remove(id: number): Promise<void> {
    const user = await this.findOne(id);
    await this.userRepo.remove(user);
    this.logger.log(`User deleted: ${user.username}`);
  }

  /**
   * Asignar grupos a un usuario
   */
  async setGroups(id: number, dto: SetUserGroupsDto): Promise<UserEntity> {
    const user = await this.findOne(id);

    // Buscar todos los grupos
    const groups: any[] = [];
    for (const groupKey of dto.groupKeys) {
      const group = await this.groupsService.findByKey(groupKey);
      if (!group) {
        throw new BadRequestException(`Group '${groupKey}' not found`);
      }
      groups.push(group);
    }

    user.groups = groups;
    const saved = await this.userRepo.save(user);

    this.logger.log(
      `User '${saved.username}' assigned ${groups.length} group(s)`,
    );

    return saved;
  }

  /**
   * Asignar acciones individuales a un usuario (excepciones)
   */
  async setActions(id: number, dto: SetUserActionsDto): Promise<UserEntity> {
    const user = await this.findOne(id);

    // Buscar todas las acciones
    const actions = await this.actionsService.findByKeys(dto.actionKeys);

    if (actions.length !== dto.actionKeys.length) {
      const foundKeys = actions.map((a) => a.key);
      const missingKeys = dto.actionKeys.filter((k) => !foundKeys.includes(k));
      throw new BadRequestException(
        `The following action keys were not found: ${missingKeys.join(', ')}`,
      );
    }

    user.actions = actions;
    const saved = await this.userRepo.save(user);

    this.logger.log(
      `User '${saved.username}' assigned ${actions.length} action(s)`,
    );

    return saved;
  }

  /**
   * Resetear contraseña de un usuario (admin)
   */
  async resetPassword(id: number, dto: ResetPasswordDto): Promise<void> {
    const user = await this.findOne(id);

    user.passwordHash = await this.hashService.hash(dto.newPassword);
    user.failedLoginAttempts = 0;
    user.lockedUntil = undefined;

    await this.userRepo.save(user);

    this.logger.log(`Password reset for user: ${user.username}`);
  }

  /**
   * Verificar si un usuario está bloqueado por intentos fallidos
   */
  async isLocked(userId: number): Promise<boolean> {
    const user = await this.findOne(userId);

    if (!user.lockedUntil) {
      return false;
    }

    // Si ya pasó el tiempo de bloqueo, desbloquearlo automáticamente
    if (user.lockedUntil < new Date()) {
      user.lockedUntil = undefined;
      user.failedLoginAttempts = 0;
      await this.userRepo.save(user);
      return false;
    }

    return true;
  }

  /**
   * Incrementar contador de intentos fallidos
   * Bloquear usuario si alcanza el umbral
   */
  async incrementFailedAttempts(userId: number): Promise<void> {
    const user = await this.findOne(userId);

    user.failedLoginAttempts = (user.failedLoginAttempts || 0) + 1;

    // Si alcanza el umbral, bloquear
    if (user.failedLoginAttempts >= this.LOCKOUT_THRESHOLD) {
      user.lockedUntil = new Date(Date.now() + this.LOCKOUT_DURATION);
      this.logger.warn(
        `User '${user.username}' locked until ${user.lockedUntil.toISOString()}`,
      );
    }

    await this.userRepo.save(user);
  }

  /**
   * Resetear contador de intentos fallidos (después de login exitoso)
   */
  async resetFailedAttempts(userId: number): Promise<void> {
    const user = await this.findOne(userId);

    user.failedLoginAttempts = 0;
    user.lockedUntil = undefined;
    user.lastLoginAt = new Date();

    await this.userRepo.save(user);
  }

  /**
   * Seed de usuarios iniciales
   */
  async seed(): Promise<void> {
    const usersToSeed: CreateUserDto[] = [
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
        } else {
          skippedCount++;
        }
      } catch (error) {
        this.logger.error(
          `Error seeding user ${userDto.username}: ${(error as Error).message}`,
          '',
        );
      }
    }

    this.logger.success(
      `Users seed completed: ${createdCount} created, ${skippedCount} skipped`,
    );

    // Asignar grupos a los usuarios
    this.logger.user('Assigning initial groups to users...');
    await this.assignInitialGroups();
    this.logger.success('Initial groups assigned successfully');
  }

  /**
   * Asignar grupos iniciales a los usuarios seeded
   */
  private async assignInitialGroups(): Promise<void> {
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
    } catch (error) {
      this.logger.error(
        `Error assigning initial groups: ${(error as Error).message}`,
      );
    }
  }
}
