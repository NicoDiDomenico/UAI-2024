import {
  Controller,
  Get,
  Post,
  Patch,
  Delete,
  Body,
  Param,
  Query,
  ParseIntPipe,
  HttpCode,
  HttpStatus,
  Logger,
  UseGuards,
} from '@nestjs/common';
import { AuthGuard } from '@nestjs/passport';
import { Actions, Public } from '@common/decorators';
import { ActionsGuard } from '@common/guards';
import { UsersService } from './users.service';
import {
  CreateUserDto,
  UpdateUserDto,
  SetUserGroupsDto,
  SetUserActionsDto,
  ResetPasswordDto,
  FindAllUsersDto,
} from './dto';

/**
 * Controlador de usuarios
 * Patrón: REST API Controller
 * Endpoints:
 * - POST /users - crear usuario
 * - GET /users - listar usuarios
 * - GET /users/:id - obtener usuario
 * - PATCH /users/:id - actualizar usuario
 * - DELETE /users/:id - eliminar usuario
 * - PATCH /users/:id/groups - asignar grupos
 * - PATCH /users/:id/actions - asignar acciones
 * - POST /users/:id/reset-password - resetear contraseña
 * - POST /users/seed - seed de usuarios iniciales
 */
@Controller('users')
@UseGuards(AuthGuard('jwt'), ActionsGuard)
export class UsersController {
  private readonly logger = new Logger(UsersController.name);

  constructor(private readonly usersService: UsersService) {}

  /**
   * Crear un nuevo usuario
   * POST /users
   */
  @Post()
  @Actions('config.usuarios.crear')
  async create(@Body() dto: CreateUserDto) {
    this.logger.log(`Creating user: ${dto.username}`);
    return await this.usersService.create(dto);
  }

  /**
   * Listar todos los usuarios con paginación, búsqueda y filtros
   * GET /users?page=1&limit=10&search=john&role=cliente&isActive=true
   */
  @Get()
  @Actions('config.usuarios.listar')
  async findAll(@Query() query: FindAllUsersDto) {
    return await this.usersService.findAll(query);
  }

  /**
   * Obtener un usuario por ID
   * GET /users/:id
   */
  @Get(':id')
  @Actions('config.usuarios.listar')
  async findOne(@Param('id', ParseIntPipe) id: number) {
    return await this.usersService.findOne(id);
  }

  /**
   * Obtener acciones heredadas de los grupos del usuario
   * GET /users/:id/inherited-actions
   */
  @Get(':id/inherited-actions')
  @Actions('config.usuarios.listar')
  async getInheritedActions(@Param('id', ParseIntPipe) id: number) {
    this.logger.log(`Getting inherited actions for user ${id}`);
    return await this.usersService.getInheritedActions(id);
  }

  /**
   * Actualizar un usuario
   * PATCH /users/:id
   */
  @Patch(':id')
  @Actions('config.usuarios.modificar')
  async update(
    @Param('id', ParseIntPipe) id: number,
    @Body() dto: UpdateUserDto,
  ) {
    this.logger.log(`Updating user: ${id}`);
    return await this.usersService.update(id, dto);
  }

  /**
   * Eliminar un usuario
   * DELETE /users/:id
   */
  @Delete(':id')
  @HttpCode(HttpStatus.NO_CONTENT)
  @Actions('config.usuarios.eliminar')
  async remove(@Param('id', ParseIntPipe) id: number) {
    this.logger.log(`Deleting user: ${id}`);
    await this.usersService.remove(id);
  }

  /**
   * Asignar grupos a un usuario
   * PATCH /users/:id/groups
   */
  @Patch(':id/groups')
  @Actions('config.usuarios.asignarGrupos')
  async setGroups(
    @Param('id', ParseIntPipe) id: number,
    @Body() dto: SetUserGroupsDto,
  ) {
    this.logger.log(`Setting groups for user: ${id}`);
    return await this.usersService.setGroups(id, dto);
  }

  /**
   * Asignar acciones individuales a un usuario
   * PATCH /users/:id/actions
   */
  @Patch(':id/actions')
  @Actions('config.usuarios.asignarAcciones')
  async setActions(
    @Param('id', ParseIntPipe) id: number,
    @Body() dto: SetUserActionsDto,
  ) {
    this.logger.log(`Setting actions for user: ${id}`);
    return await this.usersService.setActions(id, dto);
  }

  /**
   * Resetear contraseña de un usuario
   * POST /users/:id/reset-password
   */
  @Post(':id/reset-password')
  @HttpCode(HttpStatus.NO_CONTENT)
  @Actions('config.usuarios.resetearClave')
  async resetPassword(
    @Param('id', ParseIntPipe) id: number,
    @Body() dto: ResetPasswordDto,
  ) {
    this.logger.log(`Resetting password for user: ${id}`);
    await this.usersService.resetPassword(id, dto);
  }

  /**
   * Seed de usuarios iniciales
   * POST /users/seed
   */
  @Post('seed')
  @Public()
  @HttpCode(HttpStatus.NO_CONTENT)
  async seed() {
    this.logger.log('Seeding users...');
    await this.usersService.seed();
  }
}
