import { Injectable } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';
import * as argon2 from 'argon2';

/**
 * Servicio de hashing de contraseñas
 * Patrón: Service Layer
 * Usa Argon2id para máxima seguridad
 */
@Injectable()
export class HashService {
  private readonly options: argon2.Options;

  constructor(private config: ConfigService) {
    this.options = {
      type: argon2.argon2id,
      memoryCost: this.config.get<number>('argon2.memoryCost', 65536),
      timeCost: this.config.get<number>('argon2.timeCost', 3),
      parallelism: this.config.get<number>('argon2.parallelism', 4),
    };
  }

  /**
   * Hash de una contraseña usando Argon2id
   * @param password - Contraseña en texto plano
   * @returns Hash de la contraseña
   */
  async hash(password: string): Promise<string> {
    return await argon2.hash(password, this.options);
  }

  /**
   * Verificar si una contraseña coincide con su hash
   * @param hash - Hash almacenado
   * @param password - Contraseña a verificar
   * @returns true si coincide, false si no
   */
  async verify(hash: string, password: string): Promise<boolean> {
    try {
      return await argon2.verify(hash, password, this.options);
    } catch {
      return false;
    }
  }

  /**
   * Verificar si un hash necesita ser rehashed (cambió la configuración)
   * @param hash - Hash a verificar
   * @returns true si necesita rehash, false si no
   */
  needsRehash(hash: string): boolean {
    return argon2.needsRehash(hash, this.options);
  }
}
