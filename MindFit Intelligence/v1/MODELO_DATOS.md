# Modelo de Datos - MindFit Intelligence

## Índice

1. [Diagrama Entidad-Relación](#diagrama-entidad-relación)
2. [Entidades SaaS](#entidades-saas)
3. [Entidades de Seguridad](#entidades-de-seguridad)
4. [Entidades del Portal](#entidades-del-portal)
5. [Relaciones](#relaciones)
6. [Índices y constraints](#índices-y-constraints)
7. [Scripts SQL](#scripts-sql)

---

## Diagrama Entidad-Relación

### Vista general

```
┌─────────────────────────────────────────────────────────────────┐
│                         CAPA SAAS                                │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ┌──────────┐           ┌─────────────────┐                     │
│  │   Gym    │◄─────────┤   Usuarios       │                     │
│  │ (Tenant) │           │   (Multi-tenant) │                     │
│  └──────────┘           └─────────────────┘                     │
│       │                                                          │
│       │  ┌────────┐     ┌─────────────────┐                     │
│       │  │  Lead  │     │ ContactMessage  │                     │
│       │  └────────┘     └─────────────────┘                     │
│       │                                                          │
└───────┼──────────────────────────────────────────────────────────┘
        │
        │ GymId (Foreign Key)
        │
┌───────┼──────────────────────────────────────────────────────────┐
│       │              CAPA SEGURIDAD (Portal)                      │
├───────┼──────────────────────────────────────────────────────────┤
│       │                                                           │
│       │          ┌─────────────┐                                 │
│       │          │   Usuario   │                                 │
│       └─────────►│  (GymId)    │                                 │
│                  └──────┬──────┘                                 │
│                         │                                         │
│                         │ N:N                                     │
│                         │                                         │
│                  ┌──────▼──────┐         ┌────────────┐          │
│                  │ UsuarioGrupo│◄───────►│   Grupo    │          │
│                  └─────────────┘         │  (GymId)   │          │
│                                          └──────┬─────┘          │
│                                                 │                 │
│                                                 │ N:N             │
│                                                 │                 │
│                                          ┌──────▼─────┐          │
│                                          │GrupoPermiso│          │
│                                          └──────┬─────┘          │
│                                                 │                 │
│                                          ┌──────▼──────┐         │
│                                          │   Permiso   │         │
│                                          │ (Sin GymId) │         │
│                                          └─────────────┘         │
│                                                                   │
│  ┌───────────────┐       ┌──────────────────────┐               │
│  │ RefreshToken  │       │ PasswordResetToken   │               │
│  │ (FK: Usuario) │       │ (FK: Usuario)        │               │
│  └───────────────┘       └──────────────────────┘               │
│                                                                   │
└───────────────────────────────────────────────────────────────────┘

┌───────────────────────────────────────────────────────────────────┐
│                    CAPA PORTAL (Módulos)                           │
├───────────────────────────────────────────────────────────────────┤
│                                                                    │
│  ┌──────────┐           ┌─────────────┐                          │
│  │   Gym    │◄─────────┤    Socio     │                          │
│  │ (Tenant) │           │   (GymId)    │                          │
│  └──────────┘           └──────┬──────┘                          │
│       │                        │                                  │
│       │                        │                                  │
│       │                  ┌─────▼──────┐      ┌──────────────┐    │
│       │                  │   Turno    │      │    Rutina    │    │
│       │                  │  (GymId)   │      │   (GymId)    │    │
│       │                  └────────────┘      └──────┬───────┘    │
│       │                                             │             │
│       │                                             │ 1:N         │
│       │                                             │             │
│       │                                      ┌──────▼──────────┐ │
│       │                                      │ RutinaEjercicio │ │
│       │                                      │    (GymId)      │ │
│       │                                      └──────┬──────────┘ │
│       │                                             │             │
│       │                                             │             │
│       │                  ┌──────────────┐    ┌─────▼────────┐   │
│       ├─────────────────►│   Ejercicio  │◄───┤   Maquina    │   │
│       │                  │   (GymId)    │    │   (GymId)    │   │
│       │                  └──────────────┘    └──────────────┘   │
│       │                                                           │
│       │                  ┌──────────────┐    ┌──────────────┐   │
│       ├─────────────────►│ Equipamiento │    │   Horario    │   │
│       │                  │   (GymId)    │    │   (GymId)    │   │
│       │                  └──────────────┘    └──────────────┘   │
│       │                                                           │
│       │                  ┌──────────────┐    ┌───────────────┐  │
│       ├─────────────────►│  Entrenador  │    │ Configuracion │  │
│       │                  │   (GymId)    │    │   (GymId)     │  │
│       │                  └──────────────┘    └───────────────┘  │
│       │                                                           │
└───────────────────────────────────────────────────────────────────┘
```

---

## Entidades SaaS

### 1. Gym (Tenant)

Representa un gimnasio registrado en la plataforma.

| Columna      | Tipo          | Constraints                 | Descripción                                 |
| ------------ | ------------- | --------------------------- | ------------------------------------------- |
| Id           | INT           | PK, IDENTITY                | Identificador único del gimnasio (TenantId) |
| Nombre       | NVARCHAR(200) | NOT NULL, UNIQUE            | Nombre del gimnasio                         |
| Email        | NVARCHAR(255) | NOT NULL                    | Email de contacto                           |
| Telefono     | NVARCHAR(20)  | NULL                        | Teléfono de contacto                        |
| Direccion    | NVARCHAR(500) | NULL                        | Dirección física                            |
| Ciudad       | NVARCHAR(100) | NOT NULL                    | Ciudad (ej: Rosario)                        |
| Provincia    | NVARCHAR(100) | NOT NULL                    | Provincia (ej: Santa Fe)                    |
| CodigoPostal | NVARCHAR(10)  | NULL                        | Código postal                               |
| Activo       | BIT           | NOT NULL, DEFAULT 1         | Si el gimnasio está activo                  |
| FechaAlta    | DATETIME2     | NOT NULL, DEFAULT GETDATE() | Fecha de alta en la plataforma              |
| FechaBaja    | DATETIME2     | NULL                        | Fecha de baja (si aplica)                   |
| CreatedAt    | DATETIME2     | NOT NULL, DEFAULT GETDATE() | Fecha de creación del registro              |
| UpdatedAt    | DATETIME2     | NULL                        | Fecha de última actualización               |

**Índices:**

- PK: Id
- UNIQUE: Nombre
- INDEX: Email, Activo

---

### 2. Lead

Solicitudes de demo capturadas desde el sitio público.

| Columna          | Tipo           | Constraints                   | Descripción                                           |
| ---------------- | -------------- | ----------------------------- | ----------------------------------------------------- |
| Id               | INT            | PK, IDENTITY                  | Identificador único                                   |
| NombreContacto   | NVARCHAR(200)  | NOT NULL                      | Nombre del contacto                                   |
| EmailContacto    | NVARCHAR(255)  | NOT NULL                      | Email del contacto                                    |
| TelefonoContacto | NVARCHAR(20)   | NULL                          | Teléfono del contacto                                 |
| NombreGimnasio   | NVARCHAR(200)  | NOT NULL                      | Nombre del gimnasio de interés                        |
| Ciudad           | NVARCHAR(100)  | NOT NULL                      | Ciudad del gimnasio                                   |
| Mensaje          | NVARCHAR(1000) | NULL                          | Mensaje adicional                                     |
| Estado           | NVARCHAR(50)   | NOT NULL, DEFAULT 'Pendiente' | Estado: Pendiente, Contactado, Convertido, Descartado |
| FechaSolicitud   | DATETIME2      | NOT NULL, DEFAULT GETDATE()   | Fecha de solicitud                                    |
| CreatedAt        | DATETIME2      | NOT NULL, DEFAULT GETDATE()   | Fecha de creación                                     |

**Índices:**

- PK: Id
- INDEX: EmailContacto, FechaSolicitud, Estado

---

### 3. ContactMessage

Mensajes enviados desde el formulario de contacto.

| Columna      | Tipo           | Constraints                 | Descripción             |
| ------------ | -------------- | --------------------------- | ----------------------- |
| Id           | INT            | PK, IDENTITY                | Identificador único     |
| Nombre       | NVARCHAR(200)  | NOT NULL                    | Nombre del remitente    |
| Email        | NVARCHAR(255)  | NOT NULL                    | Email del remitente     |
| Asunto       | NVARCHAR(200)  | NOT NULL                    | Asunto del mensaje      |
| Mensaje      | NVARCHAR(2000) | NOT NULL                    | Contenido del mensaje   |
| Leido        | BIT            | NOT NULL, DEFAULT 0         | Si el mensaje fue leído |
| FechaMensaje | DATETIME2      | NOT NULL, DEFAULT GETDATE() | Fecha del mensaje       |
| CreatedAt    | DATETIME2      | NOT NULL, DEFAULT GETDATE() | Fecha de creación       |

**Índices:**

- PK: Id
- INDEX: Email, FechaMensaje, Leido

---

## Entidades de Seguridad

### 4. Usuario

Usuarios del portal (administradores, entrenadores, asistentes, socios).

| Columna      | Tipo          | Constraints                 | Descripción                            |
| ------------ | ------------- | --------------------------- | -------------------------------------- |
| Id           | INT           | PK, IDENTITY                | Identificador único                    |
| GymId        | INT           | FK → Gym.Id, NOT NULL       | Gimnasio al que pertenece (TenantId)   |
| Username     | NVARCHAR(100) | NOT NULL                    | Nombre de usuario (único por gimnasio) |
| Email        | NVARCHAR(255) | NOT NULL                    | Email (único por gimnasio)             |
| PasswordHash | NVARCHAR(255) | NOT NULL                    | Contraseña hasheada (bcrypt/argon2)    |
| Nombre       | NVARCHAR(100) | NOT NULL                    | Nombre real                            |
| Apellido     | NVARCHAR(100) | NOT NULL                    | Apellido                               |
| Telefono     | NVARCHAR(20)  | NULL                        | Teléfono                               |
| Activo       | BIT           | NOT NULL, DEFAULT 1         | Si el usuario está activo              |
| FechaAlta    | DATETIME2     | NOT NULL, DEFAULT GETDATE() | Fecha de alta                          |
| FechaBaja    | DATETIME2     | NULL                        | Fecha de baja                          |
| UltimoAcceso | DATETIME2     | NULL                        | Fecha de último acceso                 |
| CreatedAt    | DATETIME2     | NOT NULL, DEFAULT GETDATE() | Fecha de creación                      |
| UpdatedAt    | DATETIME2     | NULL                        | Fecha de actualización                 |

**Índices:**

- PK: Id
- UNIQUE: (GymId, Username)
- UNIQUE: (GymId, Email)
- INDEX: GymId, Activo
- FK: GymId → Gym.Id

**Constraints:**

- Username y Email únicos dentro del mismo GymId

---

### 5. Grupo

Grupos de permisos (roles) del gimnasio.

| Columna     | Tipo          | Constraints                 | Descripción                            |
| ----------- | ------------- | --------------------------- | -------------------------------------- |
| Id          | INT           | PK, IDENTITY                | Identificador único                    |
| GymId       | INT           | FK → Gym.Id, NOT NULL       | Gimnasio al que pertenece              |
| Nombre      | NVARCHAR(100) | NOT NULL                    | Nombre del grupo (ej: ADMIN_GYM)       |
| Descripcion | NVARCHAR(500) | NULL                        | Descripción del grupo                  |
| EsSistema   | BIT           | NOT NULL, DEFAULT 0         | Si es un grupo predefinido del sistema |
| Activo      | BIT           | NOT NULL, DEFAULT 1         | Si el grupo está activo                |
| CreatedAt   | DATETIME2     | NOT NULL, DEFAULT GETDATE() | Fecha de creación                      |
| UpdatedAt   | DATETIME2     | NULL                        | Fecha de actualización                 |

**Índices:**

- PK: Id
- UNIQUE: (GymId, Nombre)
- INDEX: GymId, Activo
- FK: GymId → Gym.Id

**Grupos predefinidos (inicializados por gimnasio):**

- ADMIN_GYM
- ADMIN_SEGURIDAD
- ENTRENADOR
- ASISTENTE
- SOCIO

---

### 6. Permiso

Catálogo global de permisos del sistema (sin GymId).

| Columna     | Tipo          | Constraints                 | Descripción                                    |
| ----------- | ------------- | --------------------------- | ---------------------------------------------- |
| Id          | INT           | PK, IDENTITY                | Identificador único                            |
| Codigo      | NVARCHAR(100) | NOT NULL, UNIQUE            | Código del permiso (ej: SOCIOS_VER)            |
| Nombre      | NVARCHAR(200) | NOT NULL                    | Nombre descriptivo                             |
| Modulo      | NVARCHAR(100) | NOT NULL                    | Módulo al que pertenece (Socios, Turnos, etc.) |
| Descripcion | NVARCHAR(500) | NULL                        | Descripción del permiso                        |
| CreatedAt   | DATETIME2     | NOT NULL, DEFAULT GETDATE() | Fecha de creación                              |

**Índices:**

- PK: Id
- UNIQUE: Codigo
- INDEX: Modulo

**Permisos del sistema:**

**Módulo SOCIOS:**

- SOCIOS_VER
- SOCIOS_CREAR
- SOCIOS_EDITAR
- SOCIOS_ELIMINAR
- SOCIOS_RECUPERAR

**Módulo TURNOS:**

- TURNOS_VER
- TURNOS_CREAR
- TURNOS_CANCELAR
- TURNOS_EDITAR
- TURNOS_GESTIONAR

**Módulo RUTINAS:**

- RUTINAS_VER
- RUTINAS_ASIGNAR
- RUTINAS_EDITAR
- RUTINAS_ELIMINAR

**Módulo GIMNASIO:**

- GIMNASIO_MAQUINAS_GESTIONAR
- GIMNASIO_EJERCICIOS_GESTIONAR
- GIMNASIO_EQUIPAMIENTO_GESTIONAR
- GIMNASIO_HORARIOS_GESTIONAR
- GIMNASIO_ENTRENADORES_GESTIONAR
- GIMNASIO_CONFIGURACION_GESTIONAR

**Módulo SEGURIDAD:**

- SEGURIDAD_USUARIOS_GESTIONAR
- SEGURIDAD_GRUPOS_GESTIONAR
- SEGURIDAD_PERMISOS_VER

**Módulo IA:**

- IA_USAR

---

### 7. UsuarioGrupo

Relación N:N entre Usuarios y Grupos.

| Columna         | Tipo      | Constraints                 | Descripción         |
| --------------- | --------- | --------------------------- | ------------------- |
| Id              | INT       | PK, IDENTITY                | Identificador único |
| UsuarioId       | INT       | FK → Usuario.Id, NOT NULL   | ID del usuario      |
| GrupoId         | INT       | FK → Grupo.Id, NOT NULL     | ID del grupo        |
| FechaAsignacion | DATETIME2 | NOT NULL, DEFAULT GETDATE() | Fecha de asignación |

**Índices:**

- PK: Id
- UNIQUE: (UsuarioId, GrupoId)
- INDEX: UsuarioId
- INDEX: GrupoId
- FK: UsuarioId → Usuario.Id (ON DELETE CASCADE)
- FK: GrupoId → Grupo.Id (ON DELETE CASCADE)

---

### 8. GrupoPermiso

Relación N:N entre Grupos y Permisos.

| Columna         | Tipo      | Constraints                 | Descripción         |
| --------------- | --------- | --------------------------- | ------------------- |
| Id              | INT       | PK, IDENTITY                | Identificador único |
| GrupoId         | INT       | FK → Grupo.Id, NOT NULL     | ID del grupo        |
| PermisoId       | INT       | FK → Permiso.Id, NOT NULL   | ID del permiso      |
| FechaAsignacion | DATETIME2 | NOT NULL, DEFAULT GETDATE() | Fecha de asignación |

**Índices:**

- PK: Id
- UNIQUE: (GrupoId, PermisoId)
- INDEX: GrupoId
- INDEX: PermisoId
- FK: GrupoId → Grupo.Id (ON DELETE CASCADE)
- FK: PermisoId → Permiso.Id (ON DELETE CASCADE)

---

### 9. RefreshToken

Tokens de refresco para renovación de Access Token.

| Columna         | Tipo          | Constraints                 | Descripción                                |
| --------------- | ------------- | --------------------------- | ------------------------------------------ |
| Id              | INT           | PK, IDENTITY                | Identificador único                        |
| UsuarioId       | INT           | FK → Usuario.Id, NOT NULL   | ID del usuario                             |
| TokenHash       | NVARCHAR(255) | NOT NULL, UNIQUE            | Hash del token (SHA256)                    |
| ExpiresAt       | DATETIME2     | NOT NULL                    | Fecha de expiración (7 días)               |
| CreatedAt       | DATETIME2     | NOT NULL, DEFAULT GETDATE() | Fecha de creación                          |
| RevokedAt       | DATETIME2     | NULL                        | Fecha de revocación (si aplica)            |
| ReplacedByToken | NVARCHAR(255) | NULL                        | Hash del token que lo reemplazó (rotación) |

**Índices:**

- PK: Id
- UNIQUE: TokenHash
- INDEX: UsuarioId
- INDEX: ExpiresAt
- FK: UsuarioId → Usuario.Id (ON DELETE CASCADE)

---

### 10. PasswordResetToken

Tokens para reseteo de contraseña.

| Columna   | Tipo          | Constraints                 | Descripción                      |
| --------- | ------------- | --------------------------- | -------------------------------- |
| Id        | INT           | PK, IDENTITY                | Identificador único              |
| UsuarioId | INT           | FK → Usuario.Id, NOT NULL   | ID del usuario                   |
| TokenHash | NVARCHAR(255) | NOT NULL, UNIQUE            | Hash del token (SHA256)          |
| CreatedAt | DATETIME2     | NOT NULL, DEFAULT GETDATE() | Fecha de creación                |
| ExpiresAt | DATETIME2     | NOT NULL                    | Fecha de expiración (30 minutos) |
| UsedAt    | DATETIME2     | NULL                        | Fecha de uso (NULL si no usado)  |

**Índices:**

- PK: Id
- UNIQUE: TokenHash
- INDEX: UsuarioId
- INDEX: ExpiresAt
- FK: UsuarioId → Usuario.Id (ON DELETE CASCADE)

---

## Entidades del Portal

### 11. Socio

Socios (clientes) del gimnasio.

| Columna          | Tipo           | Constraints                 | Descripción                                  |
| ---------------- | -------------- | --------------------------- | -------------------------------------------- |
| Id               | INT            | PK, IDENTITY                | Identificador único                          |
| GymId            | INT            | FK → Gym.Id, NOT NULL       | Gimnasio al que pertenece                    |
| UsuarioId        | INT            | FK → Usuario.Id, NULL       | Usuario asociado (si tiene acceso al portal) |
| NumeroSocio      | NVARCHAR(50)   | NOT NULL                    | Número de socio (único por gimnasio)         |
| Nombre           | NVARCHAR(100)  | NOT NULL                    | Nombre                                       |
| Apellido         | NVARCHAR(100)  | NOT NULL                    | Apellido                                     |
| Email            | NVARCHAR(255)  | NOT NULL                    | Email                                        |
| Telefono         | NVARCHAR(20)   | NULL                        | Teléfono                                     |
| FechaNacimiento  | DATE           | NULL                        | Fecha de nacimiento                          |
| Direccion        | NVARCHAR(500)  | NULL                        | Dirección                                    |
| DNI              | NVARCHAR(20)   | NULL                        | Documento de identidad                       |
| FechaInscripcion | DATE           | NOT NULL                    | Fecha de inscripción                         |
| Activo           | BIT            | NOT NULL, DEFAULT 1         | Si el socio está activo                      |
| CuotaAlDia       | BIT            | NOT NULL, DEFAULT 1         | Si la cuota está al día                      |
| UltimaFechaPago  | DATE           | NULL                        | Última fecha de pago de cuota                |
| Observaciones    | NVARCHAR(1000) | NULL                        | Observaciones                                |
| EliminadoLogico  | BIT            | NOT NULL, DEFAULT 0         | Eliminación lógica                           |
| CreatedAt        | DATETIME2      | NOT NULL, DEFAULT GETDATE() | Fecha de creación                            |
| UpdatedAt        | DATETIME2      | NULL                        | Fecha de actualización                       |

**Índices:**

- PK: Id
- UNIQUE: (GymId, NumeroSocio)
- INDEX: GymId, Activo
- INDEX: Email
- INDEX: UsuarioId
- FK: GymId → Gym.Id
- FK: UsuarioId → Usuario.Id (ON DELETE SET NULL)

---

### 12. Turno

Turnos/reservas para asistir al gimnasio.

| Columna          | Tipo          | Constraints                   | Descripción                                  |
| ---------------- | ------------- | ----------------------------- | -------------------------------------------- |
| Id               | INT           | PK, IDENTITY                  | Identificador único                          |
| GymId            | INT           | FK → Gym.Id, NOT NULL         | Gimnasio                                     |
| SocioId          | INT           | FK → Socio.Id, NOT NULL       | Socio que reserva                            |
| EntrenadorId     | INT           | FK → Entrenador.Id, NULL      | Entrenador asignado (opcional)               |
| FechaTurno       | DATE          | NOT NULL                      | Fecha del turno                              |
| HoraInicio       | TIME          | NOT NULL                      | Hora de inicio                               |
| HoraFin          | TIME          | NOT NULL                      | Hora de fin                                  |
| Cupos            | INT           | NOT NULL, DEFAULT 1           | Cupos del turno                              |
| CuposDisponibles | INT           | NOT NULL                      | Cupos disponibles                            |
| Estado           | NVARCHAR(50)  | NOT NULL, DEFAULT 'Reservado' | Reservado, Confirmado, Cancelado, Completado |
| FechaReserva     | DATETIME2     | NOT NULL, DEFAULT GETDATE()   | Fecha de reserva                             |
| FechaCancelacion | DATETIME2     | NULL                          | Fecha de cancelación                         |
| Observaciones    | NVARCHAR(500) | NULL                          | Observaciones                                |
| CreatedAt        | DATETIME2     | NOT NULL, DEFAULT GETDATE()   | Fecha de creación                            |
| UpdatedAt        | DATETIME2     | NULL                          | Fecha de actualización                       |

**Índices:**

- PK: Id
- INDEX: GymId, FechaTurno, Estado
- INDEX: SocioId
- INDEX: EntrenadorId
- FK: GymId → Gym.Id
- FK: SocioId → Socio.Id (ON DELETE CASCADE)
- FK: EntrenadorId → Entrenador.Id (ON DELETE SET NULL)

---

### 13. Rutina

Rutinas de ejercicios asignadas a socios.

| Columna       | Tipo           | Constraints                 | Descripción                        |
| ------------- | -------------- | --------------------------- | ---------------------------------- |
| Id            | INT            | PK, IDENTITY                | Identificador único                |
| GymId         | INT            | FK → Gym.Id, NOT NULL       | Gimnasio                           |
| SocioId       | INT            | FK → Socio.Id, NOT NULL     | Socio asignado                     |
| EntrenadorId  | INT            | FK → Entrenador.Id, NULL    | Entrenador que creó la rutina      |
| Nombre        | NVARCHAR(200)  | NOT NULL                    | Nombre de la rutina                |
| Descripcion   | NVARCHAR(1000) | NULL                        | Descripción                        |
| Objetivo      | NVARCHAR(500)  | NULL                        | Objetivo (ej: Fuerza, Resistencia) |
| FechaInicio   | DATE           | NOT NULL                    | Fecha de inicio                    |
| FechaFin      | DATE           | NULL                        | Fecha de fin (NULL si indefinida)  |
| Activa        | BIT            | NOT NULL, DEFAULT 1         | Si está activa                     |
| Observaciones | NVARCHAR(1000) | NULL                        | Observaciones                      |
| CreatedAt     | DATETIME2      | NOT NULL, DEFAULT GETDATE() | Fecha de creación                  |
| UpdatedAt     | DATETIME2      | NULL                        | Fecha de actualización             |

**Índices:**

- PK: Id
- INDEX: GymId, SocioId, Activa
- INDEX: EntrenadorId
- FK: GymId → Gym.Id
- FK: SocioId → Socio.Id (ON DELETE CASCADE)
- FK: EntrenadorId → Entrenador.Id (ON DELETE SET NULL)

---

### 14. RutinaEjercicio

Ejercicios que componen una rutina.

| Columna        | Tipo          | Constraints                 | Descripción                       |
| -------------- | ------------- | --------------------------- | --------------------------------- |
| Id             | INT           | PK, IDENTITY                | Identificador único               |
| GymId          | INT           | FK → Gym.Id, NOT NULL       | Gimnasio                          |
| RutinaId       | INT           | FK → Rutina.Id, NOT NULL    | Rutina                            |
| EjercicioId    | INT           | FK → Ejercicio.Id, NOT NULL | Ejercicio                         |
| Orden          | INT           | NOT NULL                    | Orden en la rutina                |
| Series         | INT           | NOT NULL                    | Cantidad de series                |
| Repeticiones   | NVARCHAR(50)  | NULL                        | Repeticiones (ej: "10-12", "Max") |
| Peso           | DECIMAL(5,2)  | NULL                        | Peso en kg (si aplica)            |
| TiempoDescanso | INT           | NULL                        | Descanso en segundos entre series |
| Observaciones  | NVARCHAR(500) | NULL                        | Observaciones                     |
| CreatedAt      | DATETIME2     | NOT NULL, DEFAULT GETDATE() | Fecha de creación                 |

**Índices:**

- PK: Id
- INDEX: GymId, RutinaId, Orden
- INDEX: EjercicioId
- FK: GymId → Gym.Id
- FK: RutinaId → Rutina.Id (ON DELETE CASCADE)
- FK: EjercicioId → Ejercicio.Id

---

### 15. Ejercicio

Catálogo de ejercicios del gimnasio.

| Columna       | Tipo           | Constraints                 | Descripción                             |
| ------------- | -------------- | --------------------------- | --------------------------------------- |
| Id            | INT            | PK, IDENTITY                | Identificador único                     |
| GymId         | INT            | FK → Gym.Id, NOT NULL       | Gimnasio                                |
| Nombre        | NVARCHAR(200)  | NOT NULL                    | Nombre del ejercicio                    |
| Descripcion   | NVARCHAR(1000) | NULL                        | Descripción                             |
| GrupoMuscular | NVARCHAR(100)  | NULL                        | Grupo muscular (ej: Pecho, Espalda)     |
| TipoEjercicio | NVARCHAR(100)  | NULL                        | Tipo (ej: Fuerza, Cardio, Flexibilidad) |
| MaquinaId     | INT            | FK → Maquina.Id, NULL       | Máquina asociada (si aplica)            |
| Activo        | BIT            | NOT NULL, DEFAULT 1         | Si está activo                          |
| CreatedAt     | DATETIME2      | NOT NULL, DEFAULT GETDATE() | Fecha de creación                       |
| UpdatedAt     | DATETIME2      | NULL                        | Fecha de actualización                  |

**Índices:**

- PK: Id
- INDEX: GymId, Activo
- INDEX: GrupoMuscular
- INDEX: MaquinaId
- FK: GymId → Gym.Id
- FK: MaquinaId → Maquina.Id (ON DELETE SET NULL)

---

### 16. Maquina

Máquinas/equipamiento del gimnasio.

| Columna             | Tipo           | Constraints                   | Descripción                                 |
| ------------------- | -------------- | ----------------------------- | ------------------------------------------- |
| Id                  | INT            | PK, IDENTITY                  | Identificador único                         |
| GymId               | INT            | FK → Gym.Id, NOT NULL         | Gimnasio                                    |
| Nombre              | NVARCHAR(200)  | NOT NULL                      | Nombre de la máquina                        |
| Descripcion         | NVARCHAR(1000) | NULL                          | Descripción                                 |
| Marca               | NVARCHAR(100)  | NULL                          | Marca                                       |
| Modelo              | NVARCHAR(100)  | NULL                          | Modelo                                      |
| NumeroSerie         | NVARCHAR(100)  | NULL                          | Número de serie                             |
| FechaCompra         | DATE           | NULL                          | Fecha de compra                             |
| Estado              | NVARCHAR(50)   | NOT NULL, DEFAULT 'Operativa' | Operativa, Mantenimiento, Fuera de servicio |
| UltimoMantenimiento | DATE           | NULL                          | Fecha de último mantenimiento               |
| Observaciones       | NVARCHAR(1000) | NULL                          | Observaciones                               |
| Activo              | BIT            | NOT NULL, DEFAULT 1           | Si está activa                              |
| CreatedAt           | DATETIME2      | NOT NULL, DEFAULT GETDATE()   | Fecha de creación                           |
| UpdatedAt           | DATETIME2      | NULL                          | Fecha de actualización                      |

**Índices:**

- PK: Id
- INDEX: GymId, Activo, Estado
- FK: GymId → Gym.Id

---

### 17. Equipamiento

Equipamiento adicional del gimnasio (pesas, mancuernas, etc.).

| Columna     | Tipo           | Constraints                 | Descripción                                   |
| ----------- | -------------- | --------------------------- | --------------------------------------------- |
| Id          | INT            | PK, IDENTITY                | Identificador único                           |
| GymId       | INT            | FK → Gym.Id, NOT NULL       | Gimnasio                                      |
| Nombre      | NVARCHAR(200)  | NOT NULL                    | Nombre del equipamiento                       |
| Tipo        | NVARCHAR(100)  | NOT NULL                    | Tipo (ej: Pesa, Mancuerna, Barra, Colchoneta) |
| Cantidad    | INT            | NOT NULL                    | Cantidad disponible                           |
| Peso        | DECIMAL(5,2)   | NULL                        | Peso en kg (si aplica)                        |
| Descripcion | NVARCHAR(1000) | NULL                        | Descripción                                   |
| Activo      | BIT            | NOT NULL, DEFAULT 1         | Si está activo                                |
| CreatedAt   | DATETIME2      | NOT NULL, DEFAULT GETDATE() | Fecha de creación                             |
| UpdatedAt   | DATETIME2      | NULL                        | Fecha de actualización                        |

**Índices:**

- PK: Id
- INDEX: GymId, Activo
- FK: GymId → Gym.Id

---

### 18. Horario

Horarios de atención del gimnasio.

| Columna      | Tipo      | Constraints                 | Descripción            |
| ------------ | --------- | --------------------------- | ---------------------- |
| Id           | INT       | PK, IDENTITY                | Identificador único    |
| GymId        | INT       | FK → Gym.Id, NOT NULL       | Gimnasio               |
| DiaSemana    | INT       | NOT NULL                    | 1=Lunes, 7=Domingo     |
| HoraApertura | TIME      | NOT NULL                    | Hora de apertura       |
| HoraCierre   | TIME      | NOT NULL                    | Hora de cierre         |
| Activo       | BIT       | NOT NULL, DEFAULT 1         | Si está activo         |
| CreatedAt    | DATETIME2 | NOT NULL, DEFAULT GETDATE() | Fecha de creación      |
| UpdatedAt    | DATETIME2 | NULL                        | Fecha de actualización |

**Índices:**

- PK: Id
- UNIQUE: (GymId, DiaSemana)
- INDEX: GymId, Activo
- FK: GymId → Gym.Id

---

### 19. Entrenador

Entrenadores del gimnasio.

| Columna         | Tipo           | Constraints                 | Descripción                                  |
| --------------- | -------------- | --------------------------- | -------------------------------------------- |
| Id              | INT            | PK, IDENTITY                | Identificador único                          |
| GymId           | INT            | FK → Gym.Id, NOT NULL       | Gimnasio                                     |
| UsuarioId       | INT            | FK → Usuario.Id, NULL       | Usuario asociado (si tiene acceso al portal) |
| Nombre          | NVARCHAR(100)  | NOT NULL                    | Nombre                                       |
| Apellido        | NVARCHAR(100)  | NOT NULL                    | Apellido                                     |
| Email           | NVARCHAR(255)  | NOT NULL                    | Email                                        |
| Telefono        | NVARCHAR(20)   | NULL                        | Teléfono                                     |
| Especialidad    | NVARCHAR(200)  | NULL                        | Especialidad                                 |
| Certificaciones | NVARCHAR(500)  | NULL                        | Certificaciones                              |
| FechaAlta       | DATE           | NOT NULL                    | Fecha de alta                                |
| Activo          | BIT            | NOT NULL, DEFAULT 1         | Si está activo                               |
| Observaciones   | NVARCHAR(1000) | NULL                        | Observaciones                                |
| CreatedAt       | DATETIME2      | NOT NULL, DEFAULT GETDATE() | Fecha de creación                            |
| UpdatedAt       | DATETIME2      | NULL                        | Fecha de actualización                       |

**Índices:**

- PK: Id
- INDEX: GymId, Activo
- INDEX: UsuarioId
- FK: GymId → Gym.Id
- FK: UsuarioId → Usuario.Id (ON DELETE SET NULL)

---

### 20. Configuracion

Configuraciones generales del gimnasio.

| Columna     | Tipo           | Constraints                 | Descripción                         |
| ----------- | -------------- | --------------------------- | ----------------------------------- |
| Id          | INT            | PK, IDENTITY                | Identificador único                 |
| GymId       | INT            | FK → Gym.Id, NOT NULL       | Gimnasio                            |
| Clave       | NVARCHAR(100)  | NOT NULL                    | Clave de configuración              |
| Valor       | NVARCHAR(1000) | NULL                        | Valor                               |
| Tipo        | NVARCHAR(50)   | NOT NULL                    | Tipo: String, Number, Boolean, JSON |
| Descripcion | NVARCHAR(500)  | NULL                        | Descripción                         |
| CreatedAt   | DATETIME2      | NOT NULL, DEFAULT GETDATE() | Fecha de creación                   |
| UpdatedAt   | DATETIME2      | NULL                        | Fecha de actualización              |

**Índices:**

- PK: Id
- UNIQUE: (GymId, Clave)
- INDEX: GymId
- FK: GymId → Gym.Id

**Configuraciones sugeridas:**

- `CuposMaximosPorTurno`
- `DuracionTurnoMinutos`
- `HorasAnticipacionReserva`
- `HorasCancelacionTurno`
- `NotificacionesEmailActivo`
- `MensajeBienvenida`

---

## Relaciones

### Resumen de relaciones

| Relación                     | Tipo | Descripción                                      |
| ---------------------------- | ---- | ------------------------------------------------ |
| Gym → Usuario                | 1:N  | Un gimnasio tiene muchos usuarios                |
| Gym → Grupo                  | 1:N  | Un gimnasio tiene muchos grupos                  |
| Gym → Socio                  | 1:N  | Un gimnasio tiene muchos socios                  |
| Gym → Turno                  | 1:N  | Un gimnasio tiene muchos turnos                  |
| Gym → Rutina                 | 1:N  | Un gimnasio tiene muchas rutinas                 |
| Gym → Ejercicio              | 1:N  | Un gimnasio tiene muchos ejercicios              |
| Gym → Maquina                | 1:N  | Un gimnasio tiene muchas máquinas                |
| Gym → Equipamiento           | 1:N  | Un gimnasio tiene mucho equipamiento             |
| Gym → Horario                | 1:N  | Un gimnasio tiene muchos horarios                |
| Gym → Entrenador             | 1:N  | Un gimnasio tiene muchos entrenadores            |
| Gym → Configuracion          | 1:N  | Un gimnasio tiene muchas configuraciones         |
| Usuario ↔ Grupo              | N:N  | Usuarios pertenecen a múltiples grupos           |
| Grupo ↔ Permiso              | N:N  | Grupos tienen múltiples permisos                 |
| Usuario → RefreshToken       | 1:N  | Un usuario puede tener múltiples tokens activos  |
| Usuario → PasswordResetToken | 1:N  | Un usuario puede solicitar múltiples resets      |
| Usuario → Socio              | 1:1  | Un usuario puede estar asociado a un socio       |
| Usuario → Entrenador         | 1:1  | Un usuario puede estar asociado a un entrenador  |
| Socio → Turno                | 1:N  | Un socio puede reservar múltiples turnos         |
| Socio → Rutina               | 1:N  | Un socio puede tener múltiples rutinas           |
| Entrenador → Turno           | 1:N  | Un entrenador puede estar en múltiples turnos    |
| Entrenador → Rutina          | 1:N  | Un entrenador puede crear múltiples rutinas      |
| Rutina → RutinaEjercicio     | 1:N  | Una rutina tiene múltiples ejercicios            |
| Ejercicio → RutinaEjercicio  | 1:N  | Un ejercicio puede estar en múltiples rutinas    |
| Maquina → Ejercicio          | 1:N  | Una máquina puede usarse en múltiples ejercicios |

---

## Índices y constraints

### Estrategia de índices

1. **Claves primarias:** Todas las tablas tienen `Id INT IDENTITY` como PK
2. **Claves foráneas:** Índices automáticos en todas las FK
3. **Multi-tenancy:** Índices compuestos en (GymId, ...) para filtrado rápido
4. **Unicidad:** UNIQUE constraints en combinaciones (GymId, Username), etc.
5. **Búsquedas frecuentes:** Índices en campos de filtrado (Activo, Estado, FechaTurno)

### Query Filters (EF Core)

Para aislamiento automático multi-tenant:

```csharp
modelBuilder.Entity<Usuario>().HasQueryFilter(e => e.GymId == _tenantId);
modelBuilder.Entity<Socio>().HasQueryFilter(e => e.GymId == _tenantId);
modelBuilder.Entity<Turno>().HasQueryFilter(e => e.GymId == _tenantId);
// ... aplicar a todas las entidades con GymId
```

### ON DELETE behaviors

- **RefreshToken, PasswordResetToken:** CASCADE (eliminar tokens al eliminar usuario)
- **UsuarioGrupo, GrupoPermiso:** CASCADE (eliminar relaciones al eliminar entidad)
- **Socio, Turno, Rutina:** CASCADE al eliminar Gym (eliminación en cascada del tenant)
- **UsuarioId en Socio/Entrenador:** SET NULL (preservar datos si se elimina usuario)
- **Maquina en Ejercicio:** SET NULL (preservar ejercicio si se elimina máquina)

---

## Scripts SQL

### Script de creación de base de datos

```sql
-- Crear base de datos
CREATE DATABASE MindFitDB;
GO

USE MindFitDB;
GO

-- =============================================
-- CAPA SAAS
-- =============================================

CREATE TABLE Gyms (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL UNIQUE,
    Email NVARCHAR(255) NOT NULL,
    Telefono NVARCHAR(20) NULL,
    Direccion NVARCHAR(500) NULL,
    Ciudad NVARCHAR(100) NOT NULL,
    Provincia NVARCHAR(100) NOT NULL,
    CodigoPostal NVARCHAR(10) NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaAlta DATETIME2 NOT NULL DEFAULT GETDATE(),
    FechaBaja DATETIME2 NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL
);

CREATE INDEX IX_Gyms_Email ON Gyms(Email);
CREATE INDEX IX_Gyms_Activo ON Gyms(Activo);

CREATE TABLE Leads (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreContacto NVARCHAR(200) NOT NULL,
    EmailContacto NVARCHAR(255) NOT NULL,
    TelefonoContacto NVARCHAR(20) NULL,
    NombreGimnasio NVARCHAR(200) NOT NULL,
    Ciudad NVARCHAR(100) NOT NULL,
    Mensaje NVARCHAR(1000) NULL,
    Estado NVARCHAR(50) NOT NULL DEFAULT 'Pendiente',
    FechaSolicitud DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE INDEX IX_Leads_EmailContacto ON Leads(EmailContacto);
CREATE INDEX IX_Leads_FechaSolicitud ON Leads(FechaSolicitud);
CREATE INDEX IX_Leads_Estado ON Leads(Estado);

CREATE TABLE ContactMessages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Asunto NVARCHAR(200) NOT NULL,
    Mensaje NVARCHAR(2000) NOT NULL,
    Leido BIT NOT NULL DEFAULT 0,
    FechaMensaje DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE INDEX IX_ContactMessages_Email ON ContactMessages(Email);
CREATE INDEX IX_ContactMessages_FechaMensaje ON ContactMessages(FechaMensaje);
CREATE INDEX IX_ContactMessages_Leido ON ContactMessages(Leido);

-- =============================================
-- CAPA SEGURIDAD
-- =============================================

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    GymId INT NOT NULL,
    Username NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Telefono NVARCHAR(20) NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaAlta DATETIME2 NOT NULL DEFAULT GETDATE(),
    FechaBaja DATETIME2 NULL,
    UltimoAcceso DATETIME2 NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL,
    CONSTRAINT FK_Usuarios_Gym FOREIGN KEY (GymId) REFERENCES Gyms(Id),
    CONSTRAINT UQ_Usuarios_GymId_Username UNIQUE (GymId, Username),
    CONSTRAINT UQ_Usuarios_GymId_Email UNIQUE (GymId, Email)
);

CREATE INDEX IX_Usuarios_GymId ON Usuarios(GymId);
CREATE INDEX IX_Usuarios_Activo ON Usuarios(Activo);

CREATE TABLE Grupos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    GymId INT NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    EsSistema BIT NOT NULL DEFAULT 0,
    Activo BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL,
    CONSTRAINT FK_Grupos_Gym FOREIGN KEY (GymId) REFERENCES Gyms(Id),
    CONSTRAINT UQ_Grupos_GymId_Nombre UNIQUE (GymId, Nombre)
);

CREATE INDEX IX_Grupos_GymId ON Grupos(GymId);
CREATE INDEX IX_Grupos_Activo ON Grupos(Activo);

CREATE TABLE Permisos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo NVARCHAR(100) NOT NULL UNIQUE,
    Nombre NVARCHAR(200) NOT NULL,
    Modulo NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE INDEX IX_Permisos_Modulo ON Permisos(Modulo);

CREATE TABLE UsuarioGrupo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    GrupoId INT NOT NULL,
    FechaAsignacion DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_UsuarioGrupo_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    CONSTRAINT FK_UsuarioGrupo_Grupo FOREIGN KEY (GrupoId) REFERENCES Grupos(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_UsuarioGrupo UNIQUE (UsuarioId, GrupoId)
);

CREATE INDEX IX_UsuarioGrupo_UsuarioId ON UsuarioGrupo(UsuarioId);
CREATE INDEX IX_UsuarioGrupo_GrupoId ON UsuarioGrupo(GrupoId);

CREATE TABLE GrupoPermiso (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    GrupoId INT NOT NULL,
    PermisoId INT NOT NULL,
    FechaAsignacion DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_GrupoPermiso_Grupo FOREIGN KEY (GrupoId) REFERENCES Grupos(Id) ON DELETE CASCADE,
    CONSTRAINT FK_GrupoPermiso_Permiso FOREIGN KEY (PermisoId) REFERENCES Permisos(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_GrupoPermiso UNIQUE (GrupoId, PermisoId)
);

CREATE INDEX IX_GrupoPermiso_GrupoId ON GrupoPermiso(GrupoId);
CREATE INDEX IX_GrupoPermiso_PermisoId ON GrupoPermiso(PermisoId);

CREATE TABLE RefreshTokens (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    TokenHash NVARCHAR(255) NOT NULL UNIQUE,
    ExpiresAt DATETIME2 NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    RevokedAt DATETIME2 NULL,
    ReplacedByToken NVARCHAR(255) NULL,
    CONSTRAINT FK_RefreshTokens_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE
);

CREATE INDEX IX_RefreshTokens_UsuarioId ON RefreshTokens(UsuarioId);
CREATE INDEX IX_RefreshTokens_ExpiresAt ON RefreshTokens(ExpiresAt);

CREATE TABLE PasswordResetTokens (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    TokenHash NVARCHAR(255) NOT NULL UNIQUE,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    ExpiresAt DATETIME2 NOT NULL,
    UsedAt DATETIME2 NULL,
    CONSTRAINT FK_PasswordResetTokens_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE
);

CREATE INDEX IX_PasswordResetTokens_UsuarioId ON PasswordResetTokens(UsuarioId);
CREATE INDEX IX_PasswordResetTokens_ExpiresAt ON PasswordResetTokens(ExpiresAt);

-- =============================================
-- CAPA PORTAL
-- =============================================

-- Resto de tablas en siguiente sección...
```

**Nota:** El script completo se puede generar via EF Core Migrations una vez implementado el código.

---

## Próximos pasos

1. ✅ Modelo de datos completo diseñado
2. ⏳ Especificación de endpoints REST
3. ⏳ Casos de uso detallados
4. ⏳ Decisiones técnicas documentadas
5. ⏳ Plan de implementación

---

**Última actualización:** 01/01/2026
