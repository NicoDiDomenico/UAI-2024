# CHECKLIST DE IMPLEMENTACIÓN — Módulo de Seguridad (PARTE 2)

Continuación del checklist detallado para implementar el módulo de seguridad.

**Inicio desde:** FASE 5.2 (Módulo Groups)  
**Proyecto:** MyHotelFlow - Sistema de Reservas Hoteleras

---

## FASE 5: Módulos Core de Seguridad (Continuación)

### 5.2 Crear módulo Groups

#### 5.2.1 DTOs del módulo Groups

**Archivo:** `src/modules/groups/dto/create-group.dto.ts`

```typescript
import { IsString, IsNotEmpty, IsOptional, MaxLength } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class CreateGroupDto {
  @ApiProperty({ example: 'rol.gerente', description: 'Clave única del grupo' })
  @IsString()
  @IsNotEmpty()
  @MaxLength(100)
  key: string;

  @ApiProperty({ example: 'Gerente de Hotel', description: 'Nombre del grupo' })
  @IsString()
  @IsNotEmpty()
  @MaxLength(255)
  name: string;

  @ApiProperty({ example: 'Gestión y supervisión general', required: false })
  @IsString()
  @IsOptional()
  description?: string;
}
```

**Archivo:** `src/modules/groups/dto/update-group.dto.ts`

```typescript
import { PartialType } from '@nestjs/swagger';
import { CreateGroupDto } from './create-group.dto';

export class UpdateGroupDto extends PartialType(CreateGroupDto) {}
```

**Archivo:** `src/modules/groups/dto/set-group-actions.dto.ts`

```typescript
import { IsArray, IsString } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class SetGroupActionsDto {
  @ApiProperty({
    example: ['reservas.crear', 'reservas.modificar'],
    description: 'Array de keys de acciones a asignar al grupo',
  })
  @IsArray()
  @IsString({ each: true })
  actionKeys: string[];
}
```

**Archivo:** `src/modules/groups/dto/set-group-children.dto.ts`

```typescript
import { IsArray, IsString } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class SetGroupChildrenDto {
  @ApiProperty({
    example: ['group.frontdesk', 'group.housekeeping'],
    description: 'Array de keys de grupos hijos',
  })
  @IsArray()
  @IsString({ each: true })
  childGroupKeys: string[];
}
```

**Archivo:** `src/modules/groups/dto/index.ts`

```typescript
export * from './create-group.dto';
export * from './update-group.dto';
export * from './set-group-actions.dto';
export * from './set-group-children.dto';
```

**Tareas:**
- [ ] Crear directorio `src/modules/groups/dto`
- [ ] Crear todos los DTOs con validaciones
- [ ] Usar decoradores de class-validator
- [ ] Documentar con ApiProperty de Swagger
- [ ] Crear archivo index para exports
- [ ] Verificar compilación

---

#### 5.2.2 Service de Groups

**Archivo:** `src/modules/groups/groups.service.ts`

```typescript
import { Injectable, NotFoundException, BadRequestException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { GroupEntity } from '@infra/database/entities';
import { ActionsService } from '@modules/actions/actions.service';
import { CreateGroupDto, UpdateGroupDto, SetGroupActionsDto, SetGroupChildrenDto } from './dto';

/**
 * Servicio de gestión de grupos
 * Patrón: Service Layer + Repository
 */
@Injectable()
export class GroupsService {
  constructor(
    @InjectRepository(GroupEntity)
    private readonly groupRepo: Repository<GroupEntity>,
    private readonly actionsService: ActionsService,
  ) {}

  /**
   * Crear un nuevo grupo
   */
  async create(dto: CreateGroupDto): Promise<GroupEntity> {
    // Verificar que no exista
    const exists = await this.groupRepo.findOne({ where: { key: dto.key } });
    if (exists) {
      throw new BadRequestException(`Group with key '${dto.key}' already exists`);
    }

    const group = this.groupRepo.create(dto);
    return await this.groupRepo.save(group);
  }

  /**
   * Listar todos los grupos
   */
  async findAll(): Promise<GroupEntity[]> {
    return await this.groupRepo.find({
      relations: ['actions', 'children'],
      order: { key: 'ASC' },
    });
  }

  /**
   * Buscar grupo por ID
   */
  async findOne(id: number): Promise<GroupEntity> {
    const group = await this.groupRepo.findOne({
      where: { id },
      relations: ['actions', 'children'],
    });

    if (!group) {
      throw new NotFoundException(`Group with id ${id} not found`);
    }

    return group;
  }

  /**
   * Buscar grupo por key
   */
  async findByKey(key: string): Promise<GroupEntity | null> {
    return await this.groupRepo.findOne({
      where: { key },
      relations: ['actions', 'children'],
    });
  }

  /**
   * Actualizar grupo
   */
  async update(id: number, dto: UpdateGroupDto): Promise<GroupEntity> {
    const group = await this.findOne(id);

    // Si cambia la key, verificar que no exista
    if (dto.key && dto.key !== group.key) {
      const exists = await this.groupRepo.findOne({ where: { key: dto.key } });
      if (exists) {
        throw new BadRequestException(`Group with key '${dto.key}' already exists`);
      }
    }

    Object.assign(group, dto);
    return await this.groupRepo.save(group);
  }

  /**
   * Eliminar grupo
   */
  async remove(id: number): Promise<void> {
    const group = await this.findOne(id);
    await this.groupRepo.remove(group);
  }

  /**
   * Asignar acciones a un grupo
   */
  async setActions(id: number, dto: SetGroupActionsDto): Promise<GroupEntity> {
    const group = await this.findOne(id);
    const actions = await this.actionsService.findByKeys(dto.actionKeys);

    if (actions.length !== dto.actionKeys.length) {
      throw new BadRequestException('One or more action keys are invalid');
    }

    group.actions = actions;
    return await this.groupRepo.save(group);
  }

  /**
   * Asignar grupos hijos (composición)
   * Incluye validación anti-ciclos
   */
  async setChildren(id: number, dto: SetGroupChildrenDto): Promise<GroupEntity> {
    const parentGroup = await this.findOne(id);

    // Buscar grupos hijos
    const children: GroupEntity[] = [];
    for (const childKey of dto.childGroupKeys) {
      const child = await this.findByKey(childKey);
      if (!child) {
        throw new BadRequestException(`Child group '${childKey}' not found`);
      }
      children.push(child);
    }

    // Validar anti-ciclos: ningún hijo puede tener al padre como ancestro
    for (const child of children) {
      if (await this.wouldCreateCycle(parentGroup.id, child.id)) {
        throw new BadRequestException(
          `Cannot add '${child.key}' as child: would create a cycle`,
        );
      }
    }

    parentGroup.children = children;
    return await this.groupRepo.save(parentGroup);
  }

  /**
   * Validación anti-ciclos usando DFS
   * Patrón: Depth-First Search
   */
  private async wouldCreateCycle(parentId: number, childId: number): Promise<boolean> {
    if (parentId === childId) return true;

    const visited = new Set<number>();
    const stack: number[] = [childId];

    while (stack.length > 0) {
      const currentId = stack.pop()!;

      if (visited.has(currentId)) continue;
      visited.add(currentId);

      if (currentId === parentId) return true;

      // Cargar hijos del grupo actual
      const current = await this.groupRepo.findOne({
        where: { id: currentId },
        relations: ['children'],
      });

      if (current?.children) {
        for (const child of current.children) {
          stack.push(child.id);
        }
      }
    }

    return false;
  }
}
```

**Tareas:**
- [ ] Crear `groups.service.ts`
- [ ] Implementar métodos CRUD completos
- [ ] Implementar setActions con validación
- [ ] Implementar setChildren con validación anti-ciclos
- [ ] Implementar algoritmo DFS para detectar ciclos
- [ ] Documentar con JSDoc cada método
- [ ] Verificar compilación

---

#### 5.2.3 Controller de Groups

**Archivo:** `src/modules/groups/groups.controller.ts`

```typescript
import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
  ParseIntPipe,
  UseGuards,
} from '@nestjs/common';
import { ApiTags, ApiOperation, ApiBearerAuth, ApiResponse } from '@nestjs/swagger';
import { GroupsService } from './groups.service';
import { CreateGroupDto, UpdateGroupDto, SetGroupActionsDto, SetGroupChildrenDto } from './dto';
import { JwtAuthGuard } from '@common/guards/jwt-auth.guard';
import { ActionsGuard } from '@common/guards/actions.guard';
import { Actions } from '@common/decorators/actions.decorator';

/**
 * Controlador de grupos
 * Endpoint: /groups
 */
@ApiTags('Groups')
@Controller('groups')
@UseGuards(JwtAuthGuard, ActionsGuard)
@ApiBearerAuth()
export class GroupsController {
  constructor(private readonly groupsService: GroupsService) {}

  @Post()
  @Actions('config.grupos.crear')
  @ApiOperation({ summary: 'Crear nuevo grupo' })
  @ApiResponse({ status: 201, description: 'Grupo creado exitosamente' })
  async create(@Body() createGroupDto: CreateGroupDto) {
    return await this.groupsService.create(createGroupDto);
  }

  @Get()
  @Actions('config.grupos.listar')
  @ApiOperation({ summary: 'Listar todos los grupos' })
  async findAll() {
    return await this.groupsService.findAll();
  }

  @Get(':id')
  @Actions('config.grupos.listar')
  @ApiOperation({ summary: 'Obtener grupo por ID' })
  async findOne(@Param('id', ParseIntPipe) id: number) {
    return await this.groupsService.findOne(id);
  }

  @Patch(':id')
  @Actions('config.grupos.modificar')
  @ApiOperation({ summary: 'Actualizar grupo' })
  async update(@Param('id', ParseIntPipe) id: number, @Body() updateGroupDto: UpdateGroupDto) {
    return await this.groupsService.update(id, updateGroupDto);
  }

  @Delete(':id')
  @Actions('config.grupos.eliminar')
  @ApiOperation({ summary: 'Eliminar grupo' })
  @ApiResponse({ status: 204, description: 'Grupo eliminado' })
  async remove(@Param('id', ParseIntPipe) id: number) {
    await this.groupsService.remove(id);
  }

  @Patch(':id/actions')
  @Actions('config.grupos.asignarAcciones')
  @ApiOperation({ summary: 'Asignar acciones a grupo' })
  async setActions(
    @Param('id', ParseIntPipe) id: number,
    @Body() dto: SetGroupActionsDto,
  ) {
    return await this.groupsService.setActions(id, dto);
  }

  @Patch(':id/children')
  @Actions('config.grupos.asignarHijos')
  @ApiOperation({ summary: 'Asignar grupos hijos (composición)' })
  async setChildren(
    @Param('id', ParseIntPipe) id: number,
    @Body() dto: SetGroupChildrenDto,
  ) {
    return await this.groupsService.setChildren(id, dto);
  }
}
```

**Tareas:**
- [ ] Crear `groups.controller.ts`
- [ ] Implementar todos los endpoints REST
- [ ] Agregar decoradores de Swagger
- [ ] Proteger endpoints con guards
- [ ] Especificar acciones requeridas con @Actions
- [ ] Verificar compilación

---

#### 5.2.4 Module de Groups

**Archivo:** `src/modules/groups/groups.module.ts`

```typescript
import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { GroupEntity } from '@infra/database/entities';
import { GroupsController } from './groups.controller';
import { GroupsService } from './groups.service';
import { ActionsModule } from '@modules/actions/actions.module';

@Module({
  imports: [TypeOrmModule.forFeature([GroupEntity]), ActionsModule],
  controllers: [GroupsController],
  providers: [GroupsService],
  exports: [GroupsService],
})
export class GroupsModule {}
```

**Tareas:**
- [ ] Crear `groups.module.ts`
- [ ] Importar TypeOrmModule con GroupEntity
- [ ] Importar ActionsModule
- [ ] Registrar controller y service
- [ ] Exportar service para otros módulos
- [ ] Verificar compilación

---

### 5.3 Crear módulo Users

#### 5.3.1 DTOs del módulo Users

**Archivo:** `src/modules/users/dto/create-user.dto.ts`

```typescript
import { IsString, IsEmail, IsNotEmpty, IsOptional, IsBoolean, MinLength } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class CreateUserDto {
  @ApiProperty({ example: 'jdoe', description: 'Nombre de usuario único' })
  @IsString()
  @IsNotEmpty()
  username: string;

  @ApiProperty({ example: 'jdoe@hotel.com', description: 'Email único' })
  @IsEmail()
  @IsNotEmpty()
  email: string;

  @ApiProperty({
    example: 'SecurePass123!',
    description: 'Contraseña (mín 10 caracteres)',
    required: false,
  })
  @IsString()
  @IsOptional()
  @MinLength(10)
  password?: string;

  @ApiProperty({ example: true, description: 'Estado activo del usuario', required: false })
  @IsBoolean()
  @IsOptional()
  isActive?: boolean;
}
```

**Archivo:** `src/modules/users/dto/update-user.dto.ts`

```typescript
import { PartialType, OmitType } from '@nestjs/swagger';
import { CreateUserDto } from './create-user.dto';

export class UpdateUserDto extends PartialType(OmitType(CreateUserDto, ['password'] as const)) {}
```

**Archivo:** `src/modules/users/dto/set-user-groups.dto.ts`

```typescript
import { IsArray, IsString } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class SetUserGroupsDto {
  @ApiProperty({
    example: ['rol.recepcionista'],
    description: 'Array de keys de grupos a asignar',
  })
  @IsArray()
  @IsString({ each: true })
  groupKeys: string[];
}
```

**Archivo:** `src/modules/users/dto/set-user-actions.dto.ts`

```typescript
import { IsArray, IsString } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class SetUserActionsDto {
  @ApiProperty({
    example: ['reportes.ver'],
    description: 'Array de keys de acciones excepcionales',
  })
  @IsArray()
  @IsString({ each: true })
  actionKeys: string[];
}
```

**Archivo:** `src/modules/users/dto/reset-password.dto.ts`

```typescript
import { IsString, MinLength } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class ResetPasswordDto {
  @ApiProperty({ example: 'NewSecure123!', description: 'Nueva contraseña temporal' })
  @IsString()
  @MinLength(10)
  newPassword: string;
}
```

**Archivo:** `src/modules/users/dto/index.ts`

```typescript
export * from './create-user.dto';
export * from './update-user.dto';
export * from './set-user-groups.dto';
export * from './set-user-actions.dto';
export * from './reset-password.dto';
```

**Tareas:**
- [ ] Crear directorio `src/modules/users/dto`
- [ ] Crear todos los DTOs con validaciones
- [ ] Omitir password en UpdateUserDto
- [ ] Documentar con ApiProperty
- [ ] Crear archivo index
- [ ] Verificar compilación

---

#### 5.3.2 Service de Users

**Archivo:** `src/modules/users/users.service.ts`

```typescript
import { Injectable, NotFoundException, BadRequestException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { UserEntity } from '@infra/database/entities';
import { GroupsService } from '@modules/groups/groups.service';
import { ActionsService } from '@modules/actions/actions.service';
import { HashService } from '@common/services/hash.service';
import {
  CreateUserDto,
  UpdateUserDto,
  SetUserGroupsDto,
  SetUserActionsDto,
  ResetPasswordDto,
} from './dto';

/**
 * Servicio de gestión de usuarios
 * Patrón: Service Layer
 */
@Injectable()
export class UsersService {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepo: Repository<UserEntity>,
    private readonly groupsService: GroupsService,
    private readonly actionsService: ActionsService,
    private readonly hashService: HashService,
  ) {}

  /**
   * Crear un nuevo usuario
   */
  async create(dto: CreateUserDto): Promise<UserEntity> {
    // Verificar unicidad
    const existsByEmail = await this.userRepo.findOne({ where: { email: dto.email } });
    if (existsByEmail) {
      throw new BadRequestException('Email already exists');
    }

    const existsByUsername = await this.userRepo.findOne({ where: { username: dto.username } });
    if (existsByUsername) {
      throw new BadRequestException('Username already exists');
    }

    // Hash de contraseña si se proporciona
    let passwordHash = '';
    if (dto.password) {
      passwordHash = await this.hashService.hash(dto.password);
    } else {
      // Generar contraseña temporal
      const tempPassword = this.generateTemporaryPassword();
      passwordHash = await this.hashService.hash(tempPassword);
      // TODO: Enviar email con contraseña temporal
    }

    const user = this.userRepo.create({
      username: dto.username,
      email: dto.email,
      passwordHash,
      isActive: dto.isActive ?? true,
    });

    return await this.userRepo.save(user);
  }

  /**
   * Listar todos los usuarios
   */
  async findAll(): Promise<UserEntity[]> {
    return await this.userRepo.find({
      relations: ['groups', 'actions'],
      order: { username: 'ASC' },
    });
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
   * Buscar usuario por email
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
   * Actualizar usuario
   */
  async update(id: number, dto: UpdateUserDto): Promise<UserEntity> {
    const user = await this.findOne(id);

    // Verificar unicidad si cambian email o username
    if (dto.email && dto.email !== user.email) {
      const exists = await this.userRepo.findOne({ where: { email: dto.email } });
      if (exists) {
        throw new BadRequestException('Email already exists');
      }
    }

    if (dto.username && dto.username !== user.username) {
      const exists = await this.userRepo.findOne({ where: { username: dto.username } });
      if (exists) {
        throw new BadRequestException('Username already exists');
      }
    }

    Object.assign(user, dto);
    return await this.userRepo.save(user);
  }

  /**
   * Eliminar usuario
   */
  async remove(id: number): Promise<void> {
    const user = await this.findOne(id);
    await this.userRepo.remove(user);
  }

  /**
   * Asignar grupos a usuario
   */
  async setGroups(id: number, dto: SetUserGroupsDto): Promise<UserEntity> {
    const user = await this.findOne(id);

    const groups = [];
    for (const groupKey of dto.groupKeys) {
      const group = await this.groupsService.findByKey(groupKey);
      if (!group) {
        throw new BadRequestException(`Group '${groupKey}' not found`);
      }
      groups.push(group);
    }

    user.groups = groups;
    return await this.userRepo.save(user);
  }

  /**
   * Asignar acciones específicas a usuario (excepciones)
   */
  async setActions(id: number, dto: SetUserActionsDto): Promise<UserEntity> {
    const user = await this.findOne(id);
    const actions = await this.actionsService.findByKeys(dto.actionKeys);

    if (actions.length !== dto.actionKeys.length) {
      throw new BadRequestException('One or more action keys are invalid');
    }

    user.actions = actions;
    return await this.userRepo.save(user);
  }

  /**
   * Resetear contraseña (Admin)
   */
  async resetPassword(id: number, dto: ResetPasswordDto): Promise<void> {
    const user = await this.findOne(id);

    const passwordHash = await this.hashService.hash(dto.newPassword);
    user.passwordHash = passwordHash;
    user.failedAttempts = 0;
    user.lockedUntil = null;

    await this.userRepo.save(user);
    // TODO: Enviar email notificando cambio de contraseña
  }

  /**
   * Incrementar intentos fallidos
   */
  async incrementFailedAttempts(userId: number): Promise<void> {
    const user = await this.findOne(userId);
    user.failedAttempts += 1;

    // Lockout si alcanza el límite
    const maxAttempts = parseInt(process.env.LOCKOUT_MAX_ATTEMPTS || '5', 10);
    const lockoutDuration = parseInt(process.env.LOCKOUT_DURATION || '900', 10); // segundos

    if (user.failedAttempts >= maxAttempts) {
      user.lockedUntil = new Date(Date.now() + lockoutDuration * 1000);
    }

    await this.userRepo.save(user);
  }

  /**
   * Resetear intentos fallidos (login exitoso)
   */
  async resetFailedAttempts(userId: number): Promise<void> {
    const user = await this.findOne(userId);
    user.failedAttempts = 0;
    user.lockedUntil = null;
    user.lastLoginAt = new Date();
    await this.userRepo.save(user);
  }

  /**
   * Verificar si usuario está bloqueado
   */
  async isLocked(userId: number): Promise<boolean> {
    const user = await this.findOne(userId);
    if (!user.lockedUntil) return false;
    return user.lockedUntil > new Date();
  }

  /**
   * Generar contraseña temporal
   */
  private generateTemporaryPassword(): string {
    const length = 12;
    const charset = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%';
    let password = '';
    for (let i = 0; i < length; i++) {
      password += charset.charAt(Math.floor(Math.random() * charset.length));
    }
    return password;
  }
}
```

**Tareas:**
- [ ] Crear `users.service.ts`
- [ ] Implementar CRUD completo
- [ ] Implementar setGroups y setActions
- [ ] Implementar resetPassword
- [ ] Implementar lógica de lockout (incrementFailedAttempts, isLocked)
- [ ] Agregar generación de contraseña temporal
- [ ] Documentar con JSDoc
- [ ] Verificar compilación

---

#### 5.3.3 Controller de Users

**Archivo:** `src/modules/users/users.controller.ts`

```typescript
import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
  ParseIntPipe,
  UseGuards,
  UseInterceptors,
  ClassSerializerInterceptor,
} from '@nestjs/common';
import { ApiTags, ApiOperation, ApiBearerAuth, ApiResponse } from '@nestjs/swagger';
import { UsersService } from './users.service';
import {
  CreateUserDto,
  UpdateUserDto,
  SetUserGroupsDto,
  SetUserActionsDto,
  ResetPasswordDto,
} from './dto';
import { JwtAuthGuard } from '@common/guards/jwt-auth.guard';
import { ActionsGuard } from '@common/guards/actions.guard';
import { Actions } from '@common/decorators/actions.decorator';

/**
 * Controlador de usuarios
 * Endpoint: /users
 */
@ApiTags('Users')
@Controller('users')
@UseGuards(JwtAuthGuard, ActionsGuard)
@ApiBearerAuth()
@UseInterceptors(ClassSerializerInterceptor) // Excluir passwordHash
export class UsersController {
  constructor(private readonly usersService: UsersService) {}

  @Post()
  @Actions('config.usuarios.crear')
  @ApiOperation({ summary: 'Crear nuevo usuario' })
  @ApiResponse({ status: 201, description: 'Usuario creado' })
  async create(@Body() createUserDto: CreateUserDto) {
    return await this.usersService.create(createUserDto);
  }

  @Get()
  @Actions('config.usuarios.listar')
  @ApiOperation({ summary: 'Listar todos los usuarios' })
  async findAll() {
    return await this.usersService.findAll();
  }

  @Get(':id')
  @Actions('config.usuarios.listar')
  @ApiOperation({ summary: 'Obtener usuario por ID' })
  async findOne(@Param('id', ParseIntPipe) id: number) {
    return await this.usersService.findOne(id);
  }

  @Patch(':id')
  @Actions('config.usuarios.modificar')
  @ApiOperation({ summary: 'Actualizar usuario' })
  async update(@Param('id', ParseIntPipe) id: number, @Body() updateUserDto: UpdateUserDto) {
    return await this.usersService.update(id, updateUserDto);
  }

  @Delete(':id')
  @Actions('config.usuarios.eliminar')
  @ApiOperation({ summary: 'Eliminar usuario' })
  @ApiResponse({ status: 204 })
  async remove(@Param('id', ParseIntPipe) id: number) {
    await this.usersService.remove(id);
  }

  @Patch(':id/groups')
  @Actions('config.usuarios.asignarGrupos')
  @ApiOperation({ summary: 'Asignar grupos a usuario' })
  async setGroups(@Param('id', ParseIntPipe) id: number, @Body() dto: SetUserGroupsDto) {
    return await this.usersService.setGroups(id, dto);
  }

  @Patch(':id/actions')
  @Actions('config.usuarios.asignarAcciones')
  @ApiOperation({ summary: 'Asignar acciones específicas a usuario' })
  async setActions(@Param('id', ParseIntPipe) id: number, @Body() dto: SetUserActionsDto) {
    return await this.usersService.setActions(id, dto);
  }

  @Post(':id/reset-password')
  @Actions('config.usuarios.resetPassword')
  @ApiOperation({ summary: 'Resetear contraseña de usuario (Admin)' })
  @ApiResponse({ status: 204 })
  async resetPassword(@Param('id', ParseIntPipe) id: number, @Body() dto: ResetPasswordDto) {
    await this.usersService.resetPassword(id, dto);
  }
}
```

**Tareas:**
- [ ] Crear `users.controller.ts`
- [ ] Implementar todos los endpoints
- [ ] Usar ClassSerializerInterceptor para excluir passwordHash
- [ ] Documentar con Swagger
- [ ] Proteger con guards y acciones
- [ ] Verificar compilación

---

#### 5.3.4 Module de Users

**Archivo:** `src/modules/users/users.module.ts`

```typescript
import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { UserEntity } from '@infra/database/entities';
import { UsersController } from './users.controller';
import { UsersService } from './users.service';
import { GroupsModule } from '@modules/groups/groups.module';
import { ActionsModule } from '@modules/actions/actions.module';
import { HashService } from '@common/services/hash.service';

@Module({
  imports: [TypeOrmModule.forFeature([UserEntity]), GroupsModule, ActionsModule],
  controllers: [UsersController],
  providers: [UsersService, HashService],
  exports: [UsersService],
})
export class UsersModule {}
```

**Tareas:**
- [ ] Crear `users.module.ts`
- [ ] Importar TypeOrmModule con UserEntity
- [ ] Importar GroupsModule y ActionsModule
- [ ] Registrar HashService como provider
- [ ] Exportar UsersService
- [ ] Verificar compilación

---

## FASE 6: Módulo de Autenticación

### 6.1 Crear servicio Hash

**Archivo:** `src/common/services/hash.service.ts`

```typescript
import { Injectable } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';
import * as argon2 from 'argon2';

/**
 * Servicio de hashing de contraseñas con Argon2id
 * Patrón: Service Layer
 */
@Injectable()
export class HashService {
  private readonly memoryCost: number;
  private readonly timeCost: number;
  private readonly parallelism: number;

  constructor(private readonly config: ConfigService) {
    this.memoryCost = this.config.get<number>('argon2.memory', 65536);
    this.timeCost = this.config.get<number>('argon2.iterations', 3);
    this.parallelism = this.config.get<number>('argon2.parallelism', 4);
  }

  /**
   * Hash de contraseña con Argon2id
   */
  async hash(password: string): Promise<string> {
    return await argon2.hash(password, {
      type: argon2.argon2id,
      memoryCost: this.memoryCost,
      timeCost: this.timeCost,
      parallelism: this.parallelism,
    });
  }

  /**
   * Verificar contraseña
   */
  async verify(hash: string, password: string): Promise<boolean> {
    try {
      return await argon2.verify(hash, password);
    } catch {
      return false;
    }
  }

  /**
   * Verificar si el hash necesita rehashing (cambio de parámetros)
   */
  needsRehash(hash: string): boolean {
    return argon2.needsRehash(hash, {
      type: argon2.argon2id,
      memoryCost: this.memoryCost,
      timeCost: this.timeCost,
      parallelism: this.parallelism,
    });
  }
}
```

**Tareas:**
- [ ] Crear directorio `src/common/services`
- [ ] Crear `hash.service.ts`
- [ ] Implementar hash con Argon2id
- [ ] Implementar verify
- [ ] Implementar needsRehash para rehashing automático
- [ ] Leer parámetros desde ConfigService
- [ ] Documentar con JSDoc
- [ ] Verificar compilación

---

### 6.2 Crear servicio Token

**Archivo:** `src/common/services/token.service.ts`

```typescript
import { Injectable } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';
import { JwtService } from '@nestjs/jwt';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { RevokedTokenEntity } from '@infra/database/entities';
import { v4 as uuidv4 } from 'uuid';

export interface JwtPayload {
  sub: number; // userId
  email: string;
  jti: string; // JWT ID
  type: 'access' | 'refresh';
}

export interface TokenPair {
  accessToken: string;
  refreshToken: string;
}

/**
 * Servicio de gestión de tokens JWT
 * Patrón: Service Layer
 */
@Injectable()
export class TokenService {
  constructor(
    private readonly jwtService: JwtService,
    private readonly config: ConfigService,
    @InjectRepository(RevokedTokenEntity)
    private readonly revokedTokenRepo: Repository<RevokedTokenEntity>,
  ) {}

  /**
   * Generar par de tokens (access + refresh)
   */
  async generateTokenPair(userId: number, email: string): Promise<TokenPair> {
    const accessJti = uuidv4();
    const refreshJti = uuidv4();

    const accessPayload: JwtPayload = {
      sub: userId,
      email,
      jti: accessJti,
      type: 'access',
    };

    const refreshPayload: JwtPayload = {
      sub: userId,
      email,
      jti: refreshJti,
      type: 'refresh',
    };

    const accessToken = this.jwtService.sign(accessPayload, {
      expiresIn: this.config.get('jwt.accessExpiration'),
    });

    const refreshToken = this.jwtService.sign(refreshPayload, {
      expiresIn: this.config.get('jwt.refreshExpiration'),
    });

    return { accessToken, refreshToken };
  }

  /**
   * Verificar y decodificar token
   */
  async verifyToken(token: string): Promise<JwtPayload> {
    return this.jwtService.verify<JwtPayload>(token);
  }

  /**
   * Revocar token (blacklist)
   */
  async revokeToken(jti: string, userId: number, reason: string): Promise<void> {
    // Calcular expiración del token (máximo refresh expiration)
    const maxExpiration = this.parseExpiration(this.config.get('jwt.refreshExpiration'));
    const expiresAt = new Date(Date.now() + maxExpiration);

    const revokedToken = this.revokedTokenRepo.create({
      jti,
      userId,
      reason,
      expiresAt,
    });

    await this.revokedTokenRepo.save(revokedToken);
  }

  /**
   * Verificar si un token está revocado
   */
  async isRevoked(jti: string): Promise<boolean> {
    const revoked = await this.revokedTokenRepo.findOne({
      where: { jti },
    });
    return !!revoked;
  }

  /**
   * Limpiar tokens expirados (tarea programada)
   */
  async cleanExpiredTokens(): Promise<void> {
    await this.revokedTokenRepo
      .createQueryBuilder()
      .delete()
      .where('expires_at < :now', { now: new Date() })
      .execute();
  }

  /**
   * Parsear string de expiración a milisegundos
   */
  private parseExpiration(expiration: string): number {
    const match = expiration.match(/^(\d+)([smhd])$/);
    if (!match) return 900000; // default 15m

    const value = parseInt(match[1], 10);
    const unit = match[2];

    switch (unit) {
      case 's':
        return value * 1000;
      case 'm':
        return value * 60 * 1000;
      case 'h':
        return value * 60 * 60 * 1000;
      case 'd':
        return value * 24 * 60 * 60 * 1000;
      default:
        return 900000;
    }
  }
}
```

**Tareas:**
- [ ] Crear `token.service.ts`
- [ ] Implementar generateTokenPair con JTI único
- [ ] Implementar verifyToken
- [ ] Implementar revokeToken (blacklist)
- [ ] Implementar isRevoked
- [ ] Implementar cleanExpiredTokens para cron job
- [ ] Documentar con JSDoc
- [ ] Verificar compilación

---

### 6.3 Crear servicio Authorization

**Archivo:** `src/common/services/authorization.service.ts`

```typescript
import { Injectable, Inject } from '@nestjs/common';
import { CACHE_MANAGER } from '@nestjs/cache-manager';
import { Cache } from 'cache-manager';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { UserEntity, GroupEntity } from '@infra/database/entities';

/**
 * Servicio de autorización (permisos efectivos)
 * Patrón: Composite + Cache Aside
 */
@Injectable()
export class AuthorizationService {
  private readonly CACHE_PREFIX = 'user:perm:';
  private readonly CACHE_TTL = 900; // 15 minutos

  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepo: Repository<UserEntity>,
    @Inject(CACHE_MANAGER)
    private readonly cacheManager: Cache,
  ) {}

  /**
   * Obtener permisos efectivos de un usuario (con caché)
   * Patrón: Composite - recursión sobre grupos
   */
  async getEffectiveActions(userId: number): Promise<Set<string>> {
    // Intentar obtener de caché
    const cacheKey = `${this.CACHE_PREFIX}${userId}`;
    const cached = await this.cacheManager.get<string[]>(cacheKey);

    if (cached) {
      return new Set(cached);
    }

    // Calcular permisos
    const actions = await this.calculateEffectiveActions(userId);

    // Guardar en caché
    await this.cacheManager.set(cacheKey, Array.from(actions), this.CACHE_TTL);

    return actions;
  }

  /**
   * Calcular permisos efectivos (sin caché)
   */
  private async calculateEffectiveActions(userId: number): Promise<Set<string>> {
    const user = await this.userRepo.findOne({
      where: { id: userId },
      relations: ['groups', 'groups.children', 'groups.actions', 'actions'],
    });

    if (!user) {
      return new Set();
    }

    const visited = new Set<number>();
    const result = new Set<string>();

    // Función recursiva para procesar grupos (Composite)
    const addGroupActions = (group: GroupEntity): void => {
      if (visited.has(group.id)) return;
      visited.add(group.id);

      // Agregar acciones del grupo
      group.actions?.forEach((action) => result.add(action.key));

      // Recursión sobre grupos hijos
      group.children?.forEach(addGroupActions);
    };

    // Agregar acciones propias del usuario (excepciones)
    user.actions?.forEach((action) => result.add(action.key));

    // Recorrer grupos del usuario
    user.groups?.forEach(addGroupActions);

    return result;
  }

  /**
   * Verificar si un usuario tiene una acción específica
   */
  async hasAction(userId: number, actionKey: string): Promise<boolean> {
    const actions = await this.getEffectiveActions(userId);
    return actions.has(actionKey);
  }

  /**
   * Verificar si un usuario tiene TODAS las acciones requeridas
   */
  async hasAllActions(userId: number, actionKeys: string[]): Promise<boolean> {
    const actions = await this.getEffectiveActions(userId);
    return actionKeys.every((key) => actions.has(key));
  }

  /**
   * Invalidar caché de permisos de un usuario
   */
  async invalidateCache(userId: number): Promise<void> {
    const cacheKey = `${this.CACHE_PREFIX}${userId}`;
    await this.cacheManager.del(cacheKey);
  }

  /**
   * Invalidar caché de todos los usuarios de un grupo
   * (llamar cuando se modifican acciones de un grupo)
   */
  async invalidateGroupCache(groupId: number): Promise<void> {
    // Obtener todos los usuarios del grupo
    const users = await this.userRepo
      .createQueryBuilder('user')
      .innerJoin('user.groups', 'group')
      .where('group.id = :groupId', { groupId })
      .getMany();

    // Invalidar caché de cada usuario
    for (const user of users) {
      await this.invalidateCache(user.id);
    }
  }
}
```

**Tareas:**
- [ ] Crear `authorization.service.ts`
- [ ] Implementar getEffectiveActions con patrón Composite
- [ ] Implementar recursión sobre grupos hijos
- [ ] Implementar caché en Redis (Cache Aside pattern)
- [ ] Implementar hasAction y hasAllActions
- [ ] Implementar invalidateCache e invalidateGroupCache
- [ ] Documentar patrón Composite en comentarios
- [ ] Verificar compilación

---

### 6.4 DTOs de Auth

**Archivo:** `src/modules/auth/dto/login.dto.ts`

```typescript
import { IsString, IsNotEmpty } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class LoginDto {
  @ApiProperty({ example: 'admin@hotel.com', description: 'Email o username' })
  @IsString()
  @IsNotEmpty()
  identity: string;

  @ApiProperty({ example: 'Admin123!', description: 'Contraseña' })
  @IsString()
  @IsNotEmpty()
  password: string;
}
```

**Archivo:** `src/modules/auth/dto/refresh.dto.ts`

```typescript
import { IsString, IsNotEmpty } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class RefreshDto {
  @ApiProperty({ description: 'Refresh token' })
  @IsString()
  @IsNotEmpty()
  refreshToken: string;
}
```

**Archivo:** `src/modules/auth/dto/change-password.dto.ts`

```typescript
import { IsString, IsNotEmpty, MinLength } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class ChangePasswordDto {
  @ApiProperty({ example: 'OldPassword123!', description: 'Contraseña actual' })
  @IsString()
  @IsNotEmpty()
  currentPassword: string;

  @ApiProperty({ example: 'NewPassword456!', description: 'Nueva contraseña (mín 10)' })
  @IsString()
  @MinLength(10)
  newPassword: string;
}
```

**Archivo:** `src/modules/auth/dto/recover-request.dto.ts`

```typescript
import { IsEmail, IsNotEmpty } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class RecoverRequestDto {
  @ApiProperty({ example: 'user@hotel.com', description: 'Email del usuario' })
  @IsEmail()
  @IsNotEmpty()
  email: string;
}
```

**Archivo:** `src/modules/auth/dto/recover-confirm.dto.ts`

```typescript
import { IsString, IsNotEmpty, MinLength } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class RecoverConfirmDto {
  @ApiProperty({ description: 'Token de recuperación recibido por email' })
  @IsString()
  @IsNotEmpty()
  token: string;

  @ApiProperty({ example: 'NewSecure123!', description: 'Nueva contraseña' })
  @IsString()
  @MinLength(10)
  newPassword: string;
}
```

**Archivo:** `src/modules/auth/dto/index.ts`

```typescript
export * from './login.dto';
export * from './refresh.dto';
export * from './change-password.dto';
export * from './recover-request.dto';
export * from './recover-confirm.dto';
```

**Tareas:**
- [ ] Crear directorio `src/modules/auth/dto`
- [ ] Crear todos los DTOs de autenticación
- [ ] Validar con class-validator
- [ ] Documentar con ApiProperty
- [ ] Crear archivo index
- [ ] Verificar compilación

---

**(El archivo continuará con Auth Service, Auth Controller, JWT Strategies, Guards, Decorators, Tests, etc. en la siguiente sección para no exceder el límite)**

**¿Quieres que continúe con el resto de la FASE 6 y las fases 7-11 en otro archivo adicional?**
