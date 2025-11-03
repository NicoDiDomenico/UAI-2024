import {
  Injectable,
  NotFoundException,
  BadRequestException,
} from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository, In } from 'typeorm';
import { ActionEntity } from '@infra/database/entities';
import { CreateActionDto, UpdateActionDto } from './dto';
import { LoggerService } from '@common/logger/logger.service';

/**
 * Servicio de gestión de acciones
 * Patrón: Service Layer + Repository
 * Responsabilidad: CRUD de acciones y búsquedas
 */
@Injectable()
export class ActionsService {
  private readonly logger = new LoggerService(ActionsService.name);

  constructor(
    @InjectRepository(ActionEntity)
    private readonly actionRepo: Repository<ActionEntity>,
  ) {}

  /**
   * Crear una nueva acción
   * Extrae el área automáticamente del key si no se proporciona
   */
  async create(dto: CreateActionDto): Promise<ActionEntity> {
    // Verificar que no exista una acción con el mismo key
    const exists = await this.actionRepo.findOne({ where: { key: dto.key } });
    if (exists) {
      throw new BadRequestException(
        `Action with key '${dto.key}' already exists`,
      );
    }

    // Extraer área del key si no está definida (ej: 'reservas.crear' -> 'reservas')
    const area = dto.area || this.extractArea(dto.key);

    const action = this.actionRepo.create({
      ...dto,
      area,
    });

    const saved = await this.actionRepo.save(action);
    this.logger.log(`Action created: ${saved.key}`);

    return saved;
  }

  /**
   * Listar todas las acciones
   * Ordenadas por área y key
   */
  async findAll(): Promise<ActionEntity[]> {
    return await this.actionRepo.find({
      order: { area: 'ASC', key: 'ASC' },
    });
  }

  /**
   * Buscar acción por ID
   */
  async findOne(id: number): Promise<ActionEntity> {
    const action = await this.actionRepo.findOne({ where: { id } });

    if (!action) {
      throw new NotFoundException(`Action with id ${id} not found`);
    }

    return action;
  }

  /**
   * Buscar acción por key
   */
  async findByKey(key: string): Promise<ActionEntity | null> {
    return await this.actionRepo.findOne({ where: { key } });
  }

  /**
   * Buscar múltiples acciones por sus keys
   * Útil para validación al asignar acciones a grupos/usuarios
   */
  async findByKeys(keys: string[]): Promise<ActionEntity[]> {
    if (!keys || keys.length === 0) {
      return [];
    }

    return await this.actionRepo.find({
      where: { key: In(keys) },
    });
  }

  /**
   * Buscar acciones por área funcional
   */
  async findByArea(area: string): Promise<ActionEntity[]> {
    return await this.actionRepo.find({
      where: { area },
      order: { key: 'ASC' },
    });
  }

  /**
   * Actualizar una acción
   */
  async update(id: number, dto: UpdateActionDto): Promise<ActionEntity> {
    const action = await this.findOne(id);

    // Si cambia el key, verificar que no exista
    if (dto.key && dto.key !== action.key) {
      const exists = await this.actionRepo.findOne({ where: { key: dto.key } });
      if (exists) {
        throw new BadRequestException(
          `Action with key '${dto.key}' already exists`,
        );
      }
    }

    // Actualizar área si cambia el key
    if (dto.key && !dto.area) {
      dto.area = this.extractArea(dto.key);
    }

    Object.assign(action, dto);
    const updated = await this.actionRepo.save(action);

    this.logger.log(`Action updated: ${updated.key}`);

    return updated;
  }

  /**
   * Eliminar una acción
   * NOTA: En producción considerar soft delete o verificar dependencias
   */
  async remove(id: number): Promise<void> {
    const action = await this.findOne(id);
    await this.actionRepo.remove(action);

    this.logger.log(`Action deleted: ${action.key}`);
  }

  /**
   * Extraer el área funcional del key
   * Ejemplo: 'reservas.crear' -> 'reservas'
   */
  private extractArea(key: string): string {
    const parts = key.split('.');
    return parts[0] || 'general';
  }

  /**
   * Seed de acciones iniciales
   * Método auxiliar para poblar la base de datos
   */
  async seed(): Promise<void> {
    const actionsToSeed: CreateActionDto[] = [
      // Reservas
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
      // Check-in
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
      // Check-out
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
      // Comprobantes
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
      // Habitaciones
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
      // Clientes
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
      // Configuración - Usuarios
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
      // Configuración - Grupos
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
      // Configuración - Acciones
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
        } else {
          skippedCount++;
        }
      } catch (error) {
        this.logger.error(
          `Error seeding action ${actionDto.key}: ${error.message}`,
          '',
        );
      }
    }

    this.logger.success(
      `Actions seed completed: ${createdCount} created, ${skippedCount} skipped`,
    );
  }
}
