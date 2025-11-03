import {
  Entity,
  PrimaryGeneratedColumn,
  Column,
  CreateDateColumn,
  Index,
} from 'typeorm';

/**
 * Entidad AuditLog - Registro de auditoría de operaciones de seguridad
 * Patrón: Audit Trail
 * Registra todas las operaciones críticas del sistema para compliance y troubleshooting
 */
@Entity('audit_log')
@Index(['userId', 'createdAt'])
@Index(['action', 'createdAt'])
export class AuditLogEntity {
  @PrimaryGeneratedColumn()
  id: number;

  /**
   * ID del usuario que realizó la acción
   * Puede ser null para acciones anónimas (ej: login fallido)
   */
  @Column({ nullable: true })
  userId?: number;

  /**
   * Email o username del usuario (para facilitar búsquedas)
   */
  @Column({ length: 255, nullable: true })
  userIdentity?: string;

  /**
   * Acción realizada (namespaced)
   * Ejemplos: 'auth.login', 'auth.logout', 'users.create', 'groups.assign-actions'
   */
  @Column({ length: 100 })
  action: string;

  /**
   * Tipo de entidad afectada
   * Ejemplos: 'user', 'group', 'action', 'reservation'
   */
  @Column({ length: 50, nullable: true })
  entityType?: string;

  /**
   * ID de la entidad afectada (como string para flexibilidad)
   */
  @Column({ length: 50, nullable: true })
  entityId?: string;

  /**
   * Resultado de la operación
   * 'success' | 'failure' | 'partial'
   */
  @Column({ length: 20, default: 'success' })
  status: string;

  /**
   * Mensaje descriptivo o error
   */
  @Column({ type: 'text', nullable: true })
  message?: string;

  /**
   * Metadata adicional en formato JSON
   * Puede contener: cambios realizados, valores anteriores, datos de request, etc.
   */
  @Column({ type: 'jsonb', nullable: true })
  metadata?: Record<string, any>;

  /**
   * IP del cliente que realizó la acción
   */
  @Column({ length: 50, nullable: true })
  ip?: string;

  /**
   * User-Agent del cliente
   */
  @Column({ type: 'text', nullable: true })
  userAgent?: string;

  /**
   * Nivel de severidad
   * 'info' | 'warning' | 'error' | 'critical'
   */
  @Column({ length: 20, default: 'info' })
  severity: string;

  @CreateDateColumn()
  createdAt: Date;
}
