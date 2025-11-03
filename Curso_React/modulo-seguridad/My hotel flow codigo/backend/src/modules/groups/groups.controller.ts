import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
  ParseIntPipe,
  HttpCode,
  HttpStatus,
  UseGuards,
} from '@nestjs/common';
import { AuthGuard } from '@nestjs/passport';
import { Actions, Public } from '@common/decorators';
import { ActionsGuard } from '@common/guards';
import {
  ApiTags,
  ApiOperation,
  ApiResponse,
  ApiBearerAuth,
} from '@nestjs/swagger';
import { GroupsService } from './groups.service';
import {
  CreateGroupDto,
  UpdateGroupDto,
  SetGroupActionsDto,
  SetGroupChildrenDto,
} from './dto';

/**
 * Controlador de grupos
 * Endpoint: /groups
 * Patrón: Controller (NestJS)
 */
@ApiTags('Groups')
@Controller('groups')
@ApiBearerAuth()
@UseGuards(AuthGuard('jwt'), ActionsGuard)
export class GroupsController {
  constructor(private readonly groupsService: GroupsService) {}

  @Post()
  @Actions('config.grupos.crear')
  @ApiOperation({ summary: 'Crear nuevo grupo' })
  @ApiResponse({
    status: 201,
    description: 'Grupo creado exitosamente',
  })
  @ApiResponse({
    status: 400,
    description: 'Bad Request - El grupo ya existe',
  })
  async create(@Body() createGroupDto: CreateGroupDto) {
    return await this.groupsService.create(createGroupDto);
  }

  @Get()
  @Actions('config.grupos.listar')
  @ApiOperation({ summary: 'Listar todos los grupos' })
  @ApiResponse({
    status: 200,
    description: 'Lista de grupos con sus acciones y grupos hijos',
  })
  async findAll() {
    return await this.groupsService.findAll();
  }

  @Get(':id')
  @Actions('config.grupos.listar')
  @ApiOperation({ summary: 'Obtener grupo por ID' })
  @ApiResponse({
    status: 200,
    description: 'Grupo encontrado',
  })
  @ApiResponse({
    status: 404,
    description: 'Grupo no encontrado',
  })
  async findOne(@Param('id', ParseIntPipe) id: number) {
    return await this.groupsService.findOne(id);
  }

  @Get(':id/effective-actions')
  @Actions('config.grupos.listar')
  @ApiOperation({
    summary: 'Obtener acciones efectivas del grupo (recursivo)',
  })
  @ApiResponse({
    status: 200,
    description: 'Set de acciones efectivas incluyendo hijos',
  })
  async getEffectiveActions(@Param('id', ParseIntPipe) id: number) {
    const actions = await this.groupsService.getEffectiveActions(id);
    return { actions: Array.from(actions) };
  }

  @Patch(':id')
  @Actions('config.grupos.modificar')
  @ApiOperation({ summary: 'Actualizar grupo' })
  @ApiResponse({
    status: 200,
    description: 'Grupo actualizado exitosamente',
  })
  @ApiResponse({
    status: 404,
    description: 'Grupo no encontrado',
  })
  async update(
    @Param('id', ParseIntPipe) id: number,
    @Body() updateGroupDto: UpdateGroupDto,
  ) {
    return await this.groupsService.update(id, updateGroupDto);
  }

  @Delete(':id')
  @HttpCode(HttpStatus.NO_CONTENT)
  @Actions('config.grupos.eliminar')
  @ApiOperation({ summary: 'Eliminar grupo' })
  @ApiResponse({
    status: 204,
    description: 'Grupo eliminado exitosamente',
  })
  @ApiResponse({
    status: 404,
    description: 'Grupo no encontrado',
  })
  async remove(@Param('id', ParseIntPipe) id: number) {
    await this.groupsService.remove(id);
  }

  @Patch(':id/actions')
  @Actions('config.grupos.asignarAcciones')
  @ApiOperation({ summary: 'Asignar acciones a un grupo' })
  @ApiResponse({
    status: 200,
    description: 'Acciones asignadas exitosamente',
  })
  @ApiResponse({
    status: 400,
    description: 'Una o más acciones no existen',
  })
  async setActions(
    @Param('id', ParseIntPipe) id: number,
    @Body() dto: SetGroupActionsDto,
  ) {
    return await this.groupsService.setActions(id, dto);
  }

  @Patch(':id/children')
  @Actions('config.grupos.asignarHijos')
  @ApiOperation({ summary: 'Asignar grupos hijos (composición)' })
  @ApiResponse({
    status: 200,
    description: 'Grupos hijos asignados exitosamente',
  })
  @ApiResponse({
    status: 400,
    description: 'Crearía un ciclo en la jerarquía o grupo hijo no encontrado',
  })
  async setChildren(
    @Param('id', ParseIntPipe) id: number,
    @Body() dto: SetGroupChildrenDto,
  ) {
    return await this.groupsService.setChildren(id, dto);
  }

  @Post('seed')
  @Public()
  @HttpCode(HttpStatus.OK)
  @ApiOperation({ summary: 'Poblar base de datos con grupos iniciales' })
  @ApiResponse({
    status: 200,
    description: 'Seed ejecutado exitosamente',
  })
  async seed() {
    await this.groupsService.seed();
    return { message: 'Groups seeded successfully' };
  }
}
