import {
  Injectable,
  NotFoundException,
  BadRequestException,
} from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { GroupEntity } from '@infra/database/entities';
import { ActionsService } from '@modules/actions/actions.service';
import { LoggerService } from '@common/logger/logger.service';
import {
  CreateGroupDto,
  UpdateGroupDto,
  SetGroupActionsDto,
  SetGroupChildrenDto,
} from './dto';

/**
 * Servicio de gestión de grupos
 * Patrón: Service Layer + Repository + Composite Pattern
 * Responsabilidad: CRUD de grupos + composición jerárquica + validación anti-ciclos
 */
@Injectable()
export class GroupsService {
  private readonly logger = new LoggerService(GroupsService.name);

  constructor(
    @InjectRepository(GroupEntity)
    private readonly groupRepo: Repository<GroupEntity>,
    private readonly actionsService: ActionsService,
  ) {}

  /**
   * Crear un nuevo grupo
   */
  async create(dto: CreateGroupDto): Promise<GroupEntity> {
    // Verificar que no exista un grupo con el mismo key
    const exists = await this.groupRepo.findOne({ where: { key: dto.key } });
    if (exists) {
      throw new BadRequestException(
        `Group with key '${dto.key}' already exists`,
      );
    }

    const group = this.groupRepo.create(dto);
    const saved = await this.groupRepo.save(group);

    this.logger.log(`Group created: ${saved.key}`);

    return saved;
  }

  /**
   * Listar todos los grupos con sus relaciones
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
   * @param loadRelations - Si debe cargar las relaciones (default: true)
   */
  async findByKey(
    key: string,
    loadRelations = true,
  ): Promise<GroupEntity | null> {
    return await this.groupRepo.findOne({
      where: { key },
      relations: loadRelations ? ['actions', 'children'] : [],
    });
  }

  /**
   * Actualizar un grupo
   */
  async update(id: number, dto: UpdateGroupDto): Promise<GroupEntity> {
    const group = await this.findOne(id);

    // Si cambia la key, verificar que no exista
    if (dto.key && dto.key !== group.key) {
      const exists = await this.groupRepo.findOne({ where: { key: dto.key } });
      if (exists) {
        throw new BadRequestException(
          `Group with key '${dto.key}' already exists`,
        );
      }
    }

    Object.assign(group, dto);
    const updated = await this.groupRepo.save(group);

    this.logger.log(`Group updated: ${updated.key}`);

    return updated;
  }

  /**
   * Eliminar un grupo
   * NOTA: En producción considerar soft delete o verificar dependencias
   */
  async remove(id: number): Promise<void> {
    const group = await this.findOne(id);
    await this.groupRepo.remove(group);

    this.logger.log(`Group deleted: ${group.key}`);
  }

  /**
   * Asignar acciones a un grupo
   * Valida que todas las acciones existan antes de asignar
   */
  async setActions(id: number, dto: SetGroupActionsDto): Promise<GroupEntity> {
    const group = await this.findOne(id);

    // Buscar todas las acciones por sus keys
    const actions = await this.actionsService.findByKeys(dto.actionKeys);

    // Validar que se encontraron todas las acciones
    if (actions.length !== dto.actionKeys.length) {
      const foundKeys = actions.map((a) => a.key);
      const missingKeys = dto.actionKeys.filter((k) => !foundKeys.includes(k));
      throw new BadRequestException(
        `The following action keys were not found: ${missingKeys.join(', ')}`,
      );
    }

    group.actions = actions;
    const saved = await this.groupRepo.save(group);

    this.logger.log(
      `Group '${saved.key}' assigned ${actions.length} action(s)`,
    );

    return saved;
  }

  /**
   * Asignar grupos hijos (composición jerárquica)
   * Implementa validación anti-ciclos usando DFS
   * Patrón: Composite Pattern
   */
  async setChildren(
    id: number,
    dto: SetGroupChildrenDto,
  ): Promise<GroupEntity> {
    const parentGroup = await this.findOne(id);

    // Buscar todos los grupos hijos
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
          `Cannot add '${child.key}' as child of '${parentGroup.key}': would create a cycle in hierarchy`,
        );
      }
    }

    parentGroup.children = children;
    const saved = await this.groupRepo.save(parentGroup);

    this.logger.log(
      `Group '${saved.key}' assigned ${children.length} child group(s)`,
    );

    return saved;
  }

  /**
   * Validación anti-ciclos usando DFS (Depth-First Search)
   * Patrón: Graph Algorithm (DFS)
   *
   * Verifica si agregar childId como hijo de parentId crearía un ciclo.
   * Un ciclo existe si childId (o alguno de sus descendientes) contiene a parentId.
   *
   * @param parentId - ID del grupo padre
   * @param childId - ID del grupo hijo a agregar
   * @returns true si crearía un ciclo, false si es seguro
   */
  private async wouldCreateCycle(
    parentId: number,
    childId: number,
  ): Promise<boolean> {
    // Un grupo no puede ser hijo de sí mismo
    if (parentId === childId) {
      return true;
    }

    // Conjunto de nodos visitados para evitar procesar el mismo grupo dos veces
    const visited = new Set<number>();

    // Pila para DFS (empezamos desde el childId y buscamos si alcanzamos parentId)
    const stack: number[] = [childId];

    while (stack.length > 0) {
      const currentId = stack.pop()!;

      // Si ya visitamos este nodo, saltarlo
      if (visited.has(currentId)) {
        continue;
      }

      visited.add(currentId);

      // Si encontramos al padre en los descendientes del hijo, hay ciclo
      if (currentId === parentId) {
        return true;
      }

      // Cargar los hijos del grupo actual y agregarlos a la pila
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

    // No se encontró ciclo
    return false;
  }

  /**
   * Obtener todos los permisos efectivos de un grupo (recursivo)
   * Patrón: Composite Pattern
   * Calcula la unión de acciones propias + acciones de todos los hijos recursivamente
   *
   * @param groupId - ID del grupo
   * @returns Set de keys de acciones efectivas
   */
  async getEffectiveActions(groupId: number): Promise<Set<string>> {
    const effectiveActions = new Set<string>();
    const visited = new Set<number>();

    await this.collectActions(groupId, effectiveActions, visited);

    return effectiveActions;
  }

  /**
   * Método auxiliar recursivo para recolectar acciones
   * Usa DFS para recorrer la jerarquía de grupos
   */
  private async collectActions(
    groupId: number,
    effectiveActions: Set<string>,
    visited: Set<number>,
  ): Promise<void> {
    // Evitar procesar el mismo grupo dos veces (por si hay referencias cruzadas)
    if (visited.has(groupId)) {
      return;
    }

    visited.add(groupId);

    // Cargar el grupo con sus relaciones
    const group = await this.groupRepo.findOne({
      where: { id: groupId },
      relations: ['actions', 'children'],
    });

    if (!group) {
      return;
    }

    // Agregar acciones propias del grupo
    for (const action of group.actions) {
      effectiveActions.add(action.key);
    }

    // Recursivamente agregar acciones de grupos hijos
    if (group.children) {
      for (const child of group.children) {
        await this.collectActions(child.id, effectiveActions, visited);
      }
    }
  }

  /**
   * Seed de grupos iniciales
   * Crea los 3 roles base: cliente, recepcionista, admin
   */
  async seed(): Promise<void> {
    const groupsToSeed: CreateGroupDto[] = [
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
        // No cargar relaciones en el seed para evitar problemas de recursión
        const exists = await this.findByKey(groupDto.key, false);
        if (!exists) {
          await this.create(groupDto);
          createdCount++;
        } else {
          skippedCount++;
        }
      } catch (error) {
        this.logger.error(
          `Error seeding group ${groupDto.key}: ${(error as Error).message}`,
          '',
        );
      }
    }

    this.logger.success(
      `Groups seed completed: ${createdCount} created, ${skippedCount} skipped`,
    );

    // Asignar acciones iniciales a los grupos
    this.logger.seed('Assigning initial actions to groups...');
    await this.assignInitialActions();
    this.logger.success('Initial actions assigned successfully');
  }

  /**
   * Asignar acciones iniciales a los grupos seeded
   */
  private async assignInitialActions(): Promise<void> {
    try {
      // Grupo rol.admin: tiene TODOS los permisos
      const adminGroup = await this.findByKey('rol.admin', false);
      if (adminGroup) {
        const allActions = await this.actionsService.findAll();
        const allActionKeys = allActions.map((action) => action.key);
        await this.setActions(adminGroup.id, { actionKeys: allActionKeys });
        this.logger.log(
          `Assigned ${allActionKeys.length} actions to rol.admin`,
        );
      }

      // Grupo rol.recepcionista: permisos de operaciones básicas
      const recepcionistaGroup = await this.findByKey(
        'rol.recepcionista',
        false,
      );
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
        this.logger.log(
          `Assigned ${recepcionistaActions.length} actions to rol.recepcionista`,
        );
      }

      // Grupo rol.cliente: solo ver sus propias reservas (permisos mínimos)
      const clienteGroup = await this.findByKey('rol.cliente', false);
      if (clienteGroup) {
        const clienteActions = ['reservas.listar', 'reservas.ver'];
        await this.setActions(clienteGroup.id, {
          actionKeys: clienteActions,
        });
        this.logger.log(
          `Assigned ${clienteActions.length} actions to rol.cliente`,
        );
      }
    } catch (error) {
      this.logger.error(
        `Error assigning initial actions: ${(error as Error).message}`,
        '',
      );
    }
  }
}
