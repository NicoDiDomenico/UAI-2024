import { prisma } from "../prisma.js";
import { comparePassword } from "../utils/password.util.js";
import { generateToken } from "../utils/jwt.util.js";
import type { AuthUser } from "../types/jwt.types.js";

export class AuthService {
  async login(gymId: number, nombreUsuario: string, password: string) {
    // 1. Buscar usuario en el gym especificado
    const usuario = await prisma.usuario.findUnique({
      where: {
        gymId_nombreUsuario: {
          gymId,
          nombreUsuario,
        },
      },
      include: {
        gym: true,
        persona: true,
      },
    });

    if (!usuario) {
      throw new Error("Credenciales inválidas");
    }

    // 2. Verificar contraseña
    const isValidPassword = await comparePassword(password, usuario.passwordHash);
    if (!isValidPassword) {
      throw new Error("Credenciales inválidas");
    }

    // 3. Generar JWT con payload mínimo
    const token = generateToken({
      usuarioId: usuario.usuarioId,
      gymId: usuario.gymId,
    });

    // 4. Obtener roles y permisos
    const authUser = await this.getUserWithPermissions(usuario.usuarioId, gymId);

    return {
      token,
      usuario: authUser,
    };
  }

  async getUserWithPermissions(usuarioId: number, gymId: number): Promise<AuthUser> {
    // 1. Obtener usuario con relaciones
    const usuario = await prisma.usuario.findUnique({
      where: { usuarioId },
      include: {
        gym: true,
        persona: true,
        usuarioRoles: {
          include: {
            rol: {
              include: {
                rolPermisos: {
                  include: {
                    permiso: true,
                  },
                },
              },
            },
          },
        },
        usuarioPermisos: {
          include: {
            permiso: true,
          },
        },
      },
    });

    if (!usuario || usuario.gymId !== gymId) {
      throw new Error("Usuario no encontrado o no pertenece al gym");
    }

    // 2. Extraer roles
    const roles = usuario.usuarioRoles.map((ur) => ur.rol.nombre);

    // 3. Extraer permisos de roles
    const permisosFromRoles = usuario.usuarioRoles.flatMap((ur) =>
      ur.rol.rolPermisos.map((rp) => rp.permiso.codigo)
    );

    // 4. Extraer permisos directos
    const permisosDirectos = usuario.usuarioPermisos.map((up) => up.permiso.codigo);

    // 5. Combinar y deduplicar permisos
    const permisos = [...new Set([...permisosFromRoles, ...permisosDirectos])];

    return {
      usuarioId: usuario.usuarioId,
      nombreUsuario: usuario.nombreUsuario,
      gym: {
        gymId: usuario.gym.gymId,
        nombre: usuario.gym.nombre,
      },
      persona: {
        personaId: usuario.persona.personaId,
        nombreYApellido: usuario.persona.nombreYApellido,
        email: usuario.persona.email,
      },
      roles,
      permisos,
    };
  }
}
