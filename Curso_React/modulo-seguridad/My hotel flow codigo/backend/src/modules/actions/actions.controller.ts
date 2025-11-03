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
import { Actions as ActionsDecorator, Public } from '@common/decorators';
import { ActionsGuard } from '@common/guards';
import {
  ApiTags,
  ApiOperation,
  ApiResponse,
  ApiBearerAuth,
} from '@nestjs/swagger';
import { ActionsService } from './actions.service';
import { CreateActionDto, UpdateActionDto } from './dto';

/**
 * Controlador de acciones
 * Endpoint: /actions
 * Patrón: Controller (NestJS)
 */
@ApiTags('Actions')
@Controller('actions')
@ApiBearerAuth()
@UseGuards(AuthGuard('jwt'), ActionsGuard)
export class ActionsController {
  constructor(private readonly actionsService: ActionsService) {}

  @Post()
  @ActionsDecorator('config.acciones.crear')
  @ApiOperation({ summary: 'Crear nueva acción' })
  @ApiResponse({
    status: 201,
    description: 'Acción creada exitosamente',
  })
  @ApiResponse({
    status: 400,
    description: 'Bad Request - La acción ya existe',
  })
  async create(@Body() createActionDto: CreateActionDto) {
    return await this.actionsService.create(createActionDto);
  }

  @Get()
  @ActionsDecorator('config.acciones.listar')
  @ApiOperation({ summary: 'Listar todas las acciones' })
  @ApiResponse({
    status: 200,
    description: 'Lista de acciones',
  })
  async findAll() {
    return await this.actionsService.findAll();
  }

  @Get('area/:area')
  @ActionsDecorator('config.acciones.listar')
  @ApiOperation({ summary: 'Buscar acciones por área funcional' })
  @ApiResponse({
    status: 200,
    description: 'Acciones del área especificada',
  })
  async findByArea(@Param('area') area: string) {
    return await this.actionsService.findByArea(area);
  }

  @Get(':id')
  @ActionsDecorator('config.acciones.listar')
  @ApiOperation({ summary: 'Obtener acción por ID' })
  @ApiResponse({
    status: 200,
    description: 'Acción encontrada',
  })
  @ApiResponse({
    status: 404,
    description: 'Acción no encontrada',
  })
  async findOne(@Param('id', ParseIntPipe) id: number) {
    return await this.actionsService.findOne(id);
  }

  @Patch(':id')
  @ActionsDecorator('config.acciones.modificar')
  @ApiOperation({ summary: 'Actualizar acción' })
  @ApiResponse({
    status: 200,
    description: 'Acción actualizada exitosamente',
  })
  @ApiResponse({
    status: 404,
    description: 'Acción no encontrada',
  })
  async update(
    @Param('id', ParseIntPipe) id: number,
    @Body() updateActionDto: UpdateActionDto,
  ) {
    return await this.actionsService.update(id, updateActionDto);
  }

  @Delete(':id')
  @HttpCode(HttpStatus.NO_CONTENT)
  @ActionsDecorator('config.acciones.eliminar')
  @ApiOperation({ summary: 'Eliminar acción' })
  @ApiResponse({
    status: 204,
    description: 'Acción eliminada exitosamente',
  })
  @ApiResponse({
    status: 404,
    description: 'Acción no encontrada',
  })
  async remove(@Param('id', ParseIntPipe) id: number) {
    await this.actionsService.remove(id);
  }

  @Post('seed')
  @Public()
  @HttpCode(HttpStatus.OK)
  @ApiOperation({ summary: 'Poblar base de datos con acciones iniciales' })
  @ApiResponse({
    status: 200,
    description: 'Seed ejecutado exitosamente',
  })
  async seed() {
    await this.actionsService.seed();
    return { message: 'Actions seeded successfully' };
  }
}
