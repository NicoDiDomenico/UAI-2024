import {
  Entity,
  PrimaryGeneratedColumn,
  Column,
  CreateDateColumn,
  UpdateDateColumn,
  ManyToMany,
  JoinTable,
} from 'typeorm';
import { ActionEntity } from './action.entity';

/**
 * Entidad Group - Representa un grupo de permisos con composición jerárquica
 * Patrón: Composite (permite grupos dentro de grupos)
 * Los grupos pueden contener:
 * - Acciones directas
 * - Grupos hijos (herencia de permisos)
 */
@Entity('group')
export class GroupEntity {
  @PrimaryGeneratedColumn()
  id: number;

  /**
   * Clave única del grupo
   * Ejemplos: 'rol.admin', 'rol.recepcionista', 'group.frontdesk'
   */
  @Column({ unique: true, length: 100 })
  key: string;

  /**
   * Nombre legible del grupo
   */
  @Column({ length: 255 })
  name: string;

  /**
   * Descripción del grupo y su propósito
   */
  @Column({ type: 'text', nullable: true })
  description?: string;

  /**
   * Acciones directamente asignadas a este grupo
   * Relación Many-to-Many con ActionEntity
   */
  @ManyToMany(() => ActionEntity)
  @JoinTable({
    name: 'group_actions',
    joinColumn: { name: 'group_id', referencedColumnName: 'id' },
    inverseJoinColumn: { name: 'action_id', referencedColumnName: 'id' },
  })
  actions: ActionEntity[];

  /**
   * Grupos hijos (composición jerárquica)
   * Permite herencia de permisos de forma recursiva
   * IMPORTANTE: Se valida que no haya ciclos
   * NO usar eager: true para evitar loops infinitos
   */
  @ManyToMany(() => GroupEntity)
  @JoinTable({
    name: 'group_children',
    joinColumn: { name: 'parent_group_id', referencedColumnName: 'id' },
    inverseJoinColumn: { name: 'child_group_id', referencedColumnName: 'id' },
  })
  children?: GroupEntity[];

  @CreateDateColumn()
  createdAt: Date;

  @UpdateDateColumn()
  updatedAt: Date;
}
