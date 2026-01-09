/*
  Warnings:

  - You are about to drop the `User` table. If the table is not empty, all the data it contains will be lost.

*/
-- DropTable
DROP TABLE "User";

-- CreateTable
CREATE TABLE "Gym" (
    "gymId" SERIAL NOT NULL,
    "nombre" TEXT NOT NULL,

    CONSTRAINT "Gym_pkey" PRIMARY KEY ("gymId")
);

-- CreateTable
CREATE TABLE "Persona" (
    "personaId" SERIAL NOT NULL,
    "gymId" INTEGER NOT NULL,
    "nombreYApellido" TEXT NOT NULL,
    "email" TEXT NOT NULL,
    "telefono" TEXT,
    "direccion" TEXT,
    "ciudad" TEXT,
    "nroDocumento" INTEGER,
    "genero" TEXT,
    "fechaNacimiento" TIMESTAMP(3),

    CONSTRAINT "Persona_pkey" PRIMARY KEY ("personaId")
);

-- CreateTable
CREATE TABLE "Usuario" (
    "usuarioId" SERIAL NOT NULL,
    "gymId" INTEGER NOT NULL,
    "personaId" INTEGER NOT NULL,
    "nombreUsuario" TEXT NOT NULL,
    "passwordHash" TEXT NOT NULL,
    "fechaRegistro" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "Usuario_pkey" PRIMARY KEY ("usuarioId")
);

-- CreateTable
CREATE TABLE "Rol" (
    "rolId" SERIAL NOT NULL,
    "gymId" INTEGER NOT NULL,
    "nombre" TEXT NOT NULL,

    CONSTRAINT "Rol_pkey" PRIMARY KEY ("rolId")
);

-- CreateTable
CREATE TABLE "Permiso" (
    "permisoId" SERIAL NOT NULL,
    "codigo" TEXT NOT NULL,

    CONSTRAINT "Permiso_pkey" PRIMARY KEY ("permisoId")
);

-- CreateTable
CREATE TABLE "UsuarioRol" (
    "usuarioId" INTEGER NOT NULL,
    "rolId" INTEGER NOT NULL,

    CONSTRAINT "UsuarioRol_pkey" PRIMARY KEY ("usuarioId","rolId")
);

-- CreateTable
CREATE TABLE "RolPermiso" (
    "rolId" INTEGER NOT NULL,
    "permisoId" INTEGER NOT NULL,

    CONSTRAINT "RolPermiso_pkey" PRIMARY KEY ("rolId","permisoId")
);

-- CreateTable
CREATE TABLE "UsuarioPermiso" (
    "usuarioId" INTEGER NOT NULL,
    "permisoId" INTEGER NOT NULL,

    CONSTRAINT "UsuarioPermiso_pkey" PRIMARY KEY ("usuarioId","permisoId")
);

-- CreateIndex
CREATE UNIQUE INDEX "Gym_nombre_key" ON "Gym"("nombre");

-- CreateIndex
CREATE INDEX "Persona_gymId_idx" ON "Persona"("gymId");

-- CreateIndex
CREATE UNIQUE INDEX "Persona_gymId_email_key" ON "Persona"("gymId", "email");

-- CreateIndex
CREATE UNIQUE INDEX "Usuario_personaId_key" ON "Usuario"("personaId");

-- CreateIndex
CREATE INDEX "Usuario_gymId_idx" ON "Usuario"("gymId");

-- CreateIndex
CREATE UNIQUE INDEX "Usuario_gymId_nombreUsuario_key" ON "Usuario"("gymId", "nombreUsuario");

-- CreateIndex
CREATE INDEX "Rol_gymId_idx" ON "Rol"("gymId");

-- CreateIndex
CREATE UNIQUE INDEX "Rol_gymId_nombre_key" ON "Rol"("gymId", "nombre");

-- CreateIndex
CREATE UNIQUE INDEX "Permiso_codigo_key" ON "Permiso"("codigo");

-- CreateIndex
CREATE INDEX "UsuarioRol_rolId_idx" ON "UsuarioRol"("rolId");

-- CreateIndex
CREATE INDEX "RolPermiso_permisoId_idx" ON "RolPermiso"("permisoId");

-- AddForeignKey
ALTER TABLE "Persona" ADD CONSTRAINT "Persona_gymId_fkey" FOREIGN KEY ("gymId") REFERENCES "Gym"("gymId") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "Usuario" ADD CONSTRAINT "Usuario_gymId_fkey" FOREIGN KEY ("gymId") REFERENCES "Gym"("gymId") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "Usuario" ADD CONSTRAINT "Usuario_personaId_fkey" FOREIGN KEY ("personaId") REFERENCES "Persona"("personaId") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "Rol" ADD CONSTRAINT "Rol_gymId_fkey" FOREIGN KEY ("gymId") REFERENCES "Gym"("gymId") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "UsuarioRol" ADD CONSTRAINT "UsuarioRol_usuarioId_fkey" FOREIGN KEY ("usuarioId") REFERENCES "Usuario"("usuarioId") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "UsuarioRol" ADD CONSTRAINT "UsuarioRol_rolId_fkey" FOREIGN KEY ("rolId") REFERENCES "Rol"("rolId") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "RolPermiso" ADD CONSTRAINT "RolPermiso_rolId_fkey" FOREIGN KEY ("rolId") REFERENCES "Rol"("rolId") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "RolPermiso" ADD CONSTRAINT "RolPermiso_permisoId_fkey" FOREIGN KEY ("permisoId") REFERENCES "Permiso"("permisoId") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "UsuarioPermiso" ADD CONSTRAINT "UsuarioPermiso_usuarioId_fkey" FOREIGN KEY ("usuarioId") REFERENCES "Usuario"("usuarioId") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "UsuarioPermiso" ADD CONSTRAINT "UsuarioPermiso_permisoId_fkey" FOREIGN KEY ("permisoId") REFERENCES "Permiso"("permisoId") ON DELETE CASCADE ON UPDATE CASCADE;
