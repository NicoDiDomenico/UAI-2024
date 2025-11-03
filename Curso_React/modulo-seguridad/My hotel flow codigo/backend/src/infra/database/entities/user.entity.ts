import {
  Entity,
  PrimaryGeneratedColumn,
  Column,
  CreateDateColumn,
  UpdateDateColumn,
  ManyToMany,
  JoinTable,
} from 'typeorm';
import { GroupEntity } from './group.entity';
import { ActionEntity } from './action.entity';

/**
 * Entidad User - Representa un usuario del sistema
 * Los permisos efectivos de un usuario se calculan como:
 * - Acciones propias (excepciones)
 * - Unión de acciones de todos sus grupos y grupos hijos (recursivo)
 */
@Entity('user')
export class UserEntity {
  @PrimaryGeneratedColumn()
  id: number;

  /**
   * Nombre de usuario único
   */
  @Column({ unique: true, length: 50 })
  username: string;

  /**
   * Email único del usuario
   */
  @Column({ unique: true, length: 255 })
  email: string;

  /**
   * Hash de la contraseña (Argon2id)
   * Nunca se debe exponer en respuestas API
   */
  @Column({ length: 255 })
  passwordHash: string;

  /**
   * Nombre completo del usuario
   */
  @Column({ length: 255, nullable: true })
  fullName?: string;

  /**
   * Rol del usuario en el sistema
   * Define el tipo de usuario: admin, recepcionista o cliente
   */
  @Column({ length: 50, default: 'cliente' })
  role: string;

  /**
   * Indica si el usuario está activo
   * Los usuarios inactivos no pueden iniciar sesión
   */
  @Column({ default: true })
  isActive: boolean;

  /**
   * Fecha del último inicio de sesión exitoso
   */
  @Column({ type: 'timestamp', nullable: true })
  lastLoginAt?: Date;

  /**
   * Contador de intentos fallidos de inicio de sesión
   * Se reinicia a 0 después de un login exitoso
   */
  @Column({ default: 0 })
  failedLoginAttempts: number;

  /**
   * Fecha hasta la cual el usuario está bloqueado
   * Lockout por intentos fallidos (5 intentos = 15 min de bloqueo)
   */
  @Column({ type: 'timestamp', nullable: true })
  lockedUntil?: Date;

  /**
   * Grupos asignados al usuario
   * Los permisos se heredan de forma recursiva
   * NO usar eager: true para evitar loops infinitos con children
   */
  @ManyToMany(() => GroupEntity)
  @JoinTable({
    name: 'user_groups',
    joinColumn: { name: 'user_id', referencedColumnName: 'id' },
    inverseJoinColumn: { name: 'group_id', referencedColumnName: 'id' },
  })
  groups: GroupEntity[];

  /**
   * Acciones individuales asignadas al usuario (excepciones)
   * Útil para dar permisos puntuales sin crear un grupo
   */
  @ManyToMany(() => ActionEntity)
  @JoinTable({
    name: 'user_actions',
    joinColumn: { name: 'user_id', referencedColumnName: 'id' },
    inverseJoinColumn: { name: 'action_id', referencedColumnName: 'id' },
  })
  actions: ActionEntity[];

  /**
   * Token de recuperación de contraseña (one-time use)
   * Se invalida después de usarse o cuando expira
   */
  @Column({ length: 255, nullable: true })
  passwordResetToken?: string;

  /**
   * Fecha de expiración del token de recuperación
   */
  @Column({ type: 'timestamp', nullable: true })
  passwordResetExpires?: Date;

  @CreateDateColumn()
  createdAt: Date;

  @UpdateDateColumn()
  updatedAt: Date;
}
