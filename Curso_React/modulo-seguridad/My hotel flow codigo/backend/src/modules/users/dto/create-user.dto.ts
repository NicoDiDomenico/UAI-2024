import {
  IsString,
  IsEmail,
  IsNotEmpty,
  IsOptional,
  MinLength,
  MaxLength,
  IsBoolean,
  IsEnum,
} from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

/**
 * Roles de usuario disponibles
 */
export enum UserRole {
  ADMIN = 'admin',
  RECEPCIONISTA = 'recepcionista',
  CLIENTE = 'cliente',
}

/**
 * DTO para crear un nuevo usuario
 */
export class CreateUserDto {
  @ApiProperty({
    example: 'jdoe',
    description: 'Nombre de usuario único',
  })
  @IsString()
  @IsNotEmpty()
  @MinLength(3)
  @MaxLength(50)
  username: string;

  @ApiProperty({
    example: 'john.doe@hotel.com',
    description: 'Email único del usuario',
  })
  @IsEmail()
  @IsNotEmpty()
  email: string;

  @ApiProperty({
    example: 'SecurePass123!',
    description:
      'Contraseña (mínimo 8 caracteres, 1 mayúscula, 1 minúscula, 1 número)',
    minLength: 8,
  })
  @IsString()
  @IsNotEmpty()
  @MinLength(8)
  password: string;

  @ApiProperty({
    example: 'John Doe',
    description: 'Nombre completo del usuario',
    required: false,
  })
  @IsString()
  @IsOptional()
  @MaxLength(255)
  fullName?: string;

  @ApiProperty({
    example: UserRole.CLIENTE,
    description: 'Rol del usuario en el sistema',
    enum: UserRole,
    default: UserRole.CLIENTE,
    required: false,
  })
  @IsEnum(UserRole)
  @IsOptional()
  role?: UserRole;

  @ApiProperty({
    example: true,
    description: 'Indica si el usuario está activo',
    default: true,
    required: false,
  })
  @IsBoolean()
  @IsOptional()
  isActive?: boolean;
}
