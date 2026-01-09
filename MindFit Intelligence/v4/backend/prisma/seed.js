import "dotenv/config";
import { PrismaClient } from "@prisma/client";
import { PrismaPg } from "@prisma/adapter-pg";
import { Pool } from "pg";
import bcrypt from "bcrypt";
const pool = new Pool({
    connectionString: process.env.DATABASE_URL,
});
const adapter = new PrismaPg(pool);
const prisma = new PrismaClient({ adapter });
async function main() {
    // 1) Gym
    const gym = await prisma.gym.upsert({
        where: { nombre: "Gym Olimpo" },
        update: {},
        create: { nombre: "Gym Olimpo" },
    });
    // 2) Permisos globales
    const permisos = [
        "USER_CREATE",
        "USER_UPDATE",
        "USER_DELETE",
        "ROLE_CREATE",
        "ROLE_EDIT_PERMISSIONS",
        "USER_ASSIGN_ROLE",
        "TURNOS_VER",
        "TURNOS_CREAR",
    ];
    for (const codigo of permisos) {
        await prisma.permiso.upsert({
            where: { codigo },
            update: {},
            create: { codigo },
        });
    }
    // 3) Rol ADMIN (del gym)
    const rolAdmin = await prisma.rol.upsert({
        where: { gymId_nombre: { gymId: gym.gymId, nombre: "ADMIN" } },
        update: {},
        create: { gymId: gym.gymId, nombre: "ADMIN" },
    });
    // 4) Vincular permisos al rol ADMIN (todos)
    const permisosDB = await prisma.permiso.findMany();
    for (const p of permisosDB) {
        await prisma.rolPermiso.upsert({
            where: { rolId_permisoId: { rolId: rolAdmin.rolId, permisoId: p.permisoId } },
            update: {},
            create: { rolId: rolAdmin.rolId, permisoId: p.permisoId },
        });
    }
    // 5) Persona + Usuario admin
    const passwordHash = await bcrypt.hash("admin123", 10);
    const personaAdmin = await prisma.persona.upsert({
        where: { gymId_email: { gymId: gym.gymId, email: "admin@olimpo.com" } },
        update: {},
        create: {
            gymId: gym.gymId,
            nombreYApellido: "Admin Olimpo",
            email: "admin@olimpo.com",
        },
    });
    const usuarioAdmin = await prisma.usuario.upsert({
        where: { gymId_nombreUsuario: { gymId: gym.gymId, nombreUsuario: "admin" } },
        update: { passwordHash },
        create: {
            gymId: gym.gymId,
            personaId: personaAdmin.personaId,
            nombreUsuario: "admin",
            passwordHash,
        },
    });
    // 6) Asignar rol ADMIN al usuario admin
    await prisma.usuarioRol.upsert({
        where: { usuarioId_rolId: { usuarioId: usuarioAdmin.usuarioId, rolId: rolAdmin.rolId } },
        update: {},
        create: { usuarioId: usuarioAdmin.usuarioId, rolId: rolAdmin.rolId },
    });
    console.log("✅ Seed OK: Gym + permisos + rol ADMIN + usuario admin (admin/admin123)");
}
main()
    .catch((e) => {
    console.error(e);
    process.exit(1);
})
    .finally(async () => {
    await prisma.$disconnect();
    await pool.end();
});
//# sourceMappingURL=seed.js.map