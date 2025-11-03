import {
  Entity,
  PrimaryGeneratedColumn,
  Column,
  CreateDateColumn,
  UpdateDateColumn,
} from 'typeorm';

/**
 * Entidad Action - Representa un permiso atómico en el sistema
 * Patrón: Entity (Domain Model)
 * Ejemplo: 'reservas.crear', 'checkin.registrar', 'comprobantes.imprimir'
 */
@Entity('action')
export class ActionEntity {
  @PrimaryGeneratedColumn()
  id: number;

  /**
   * Clave única del permiso (namespaced)
   * Formato: <area>.<operacion>
   * Ejemplos: 'reservas.crear', 'usuarios.modificar', 'config.grupos.eliminar'
   */
  @Column({ unique: true, length: 100 })
  key: string;

  /**
   * Nombre legible de la acción
   */
  @Column({ length: 255 })
  name: string;

  /**
   * Descripción detallada de lo que permite esta acción
   */
  @Column({ type: 'text', nullable: true })
  description?: string;

  /**
   * Área funcional a la que pertenece (ej: 'reservas', 'checkin', 'config')
   * Extraído automáticamente del key
   */
  @Column({ length: 50, nullable: true })
  area?: string;

  @CreateDateColumn()
  createdAt: Date;

  @UpdateDateColumn()
  updatedAt: Date;
}
