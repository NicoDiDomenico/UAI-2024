# PROMPT – LEVANTAMIENTO DEL PROYECTO

## MindFit Intelligence

Actuá como **arquitecto de software senior y desarrollador full-stack**, con experiencia en proyectos reales y académicos.

---

## Objetivo

Diseñar y planificar un **sistema web de gestión integral de gimnasios** (MindFit Intelligence), destinado a **gimnasios de musculación de Rosario, Argentina**, siguiendo buenas prácticas de ingeniería de software, arquitectura limpia y patrones de diseño adecuados.

El sistema se ofrece bajo modalidad **E-Business B2B** y se implementa como una solución **SaaS (Software as a Service)** accesible desde la web.

---

## Contexto del proyecto

- **Tipo de aplicación:** Web (sin app nativa), accesible desde navegador (PC y móvil).
- **Modalidad:** **SaaS B2B**.
- **Estructura del producto (2 áreas):**
  1. **Sitio público (Marketing):** sin autenticación. Presenta el servicio (Inicio, Funcionalidades, Precios, Testimonios, Blog, Contacto) y captura leads (Solicitar demo / Contacto).
  2. **Portal de clientes (Plataforma):** área autenticada. Los gimnasios que adquieren el servicio usan el sistema de gestión (socios, turnos, rutinas, etc.).
- **Público objetivo:**
  - Sitio público: dueños/administradores de gimnasios (potenciales clientes).
  - Portal: dueño/administración del gimnasio, asistentes, entrenadores y socios.
- **Alcance:** Versión académica / iterativa (MVP por módulos), con base para evolucionar a funcionalidades más avanzadas (incluye IA como eje futuro).
- **Restricciones:**
  - Adaptado a gimnasios de musculación de Rosario.
  - Idioma español.
  - No incluye gestión contable, fiscal ni impositiva.
  - Concurrencia limitada en esta etapa (con posibilidad de escalar).

---

## Stack tecnológico (base)

- **Frontend:** React.js.
- **Backend:** C# con .NET 8, utilizando arquitectura basada en controladores, servicios y repositorios.
- **Base de datos:** Microsoft SQL Server.
- **Autenticación y autorización:** **JWT (Access Token) + Refresh Token** + control de acceso por permisos (RBAC por grupos).

---

## Estructura de Backend (definición cerrada)

El backend se organiza en un único proyecto ASP.NET Core (.NET 8)
siguiendo una arquitectura en capas simples, con las siguientes carpetas:

- Controllers
- Services
- Models
- DTOs
- Validators
- Migrations

### Capas y responsabilidades

- **Controllers**

  - Exponen endpoints HTTP (API REST).
  - No contienen lógica de negocio.
  - Aplican autenticación/autorización (policies/atributos).
  - Delegan toda la lógica a Services.

- **Services**

  - Contienen la lógica de negocio.
  - Aplican reglas del dominio.
  - Respetan el aislamiento por `GymId` en todas las operaciones del portal.

- **Models**

  - Entidades del dominio persistidas en SQL Server (tablas).
  - Todas las entidades del Portal incluyen `GymId`.

- **DTOs**

  - Modelos de entrada/salida de la API (requests/responses).
  - No se exponen entidades directamente desde Controllers.

- **Validators**

  - Validaciones sobre DTOs (por ejemplo con FluentValidation).

- **Migrations**
  - Migraciones de Entity Framework Core.

---

## 🧩 Multi-tenant (SaaS) – Definición cerrada

- La plataforma es **multi-tenant**: cada gimnasio es un **Tenant**.
- Se implementa en una **única base de datos** (SQL Server) con un identificador `GymId` (TenantId).
- Todas las entidades del **Portal** que pertenezcan a un gimnasio deben incluir `GymId`.
- Un usuario del portal pertenece a **un único gimnasio** (por simplicidad académica): `Usuario.GymId`.
- El acceso al portal inicia con un **selector de gimnasio** (“Buscar gimnasio…”) antes del login.

---

## 🔐 Diseño de Autenticación (definido)

### Tokens

- **Access Token (JWT):**

  - Duración: **15 minutos**.
  - Se envía en cada request mediante el header  
    `Authorization: Bearer <token>`.
  - Contiene únicamente claims de identificación:
    - `sub` (UserId)
    - email o username.
    - `gymId` (TenantId) para identificar el gimnasio del usuario autenticado.
  - **No contiene información de grupos ni permisos**.

- **Refresh Token:**
  - Duración: **7 días**.
  - Se almacena en **cookie HttpOnly**.
  - Se persiste en base de datos para permitir **revocación y rotación**.

---

### Resolución de permisos

- En cada request autenticado:
  1. Se valida el JWT.
  2. Se obtiene el `UserId` desde el claim `sub` y el `GymId` desde el claim `gymId`.
  3. Se consultan en base de datos los **grupos y permisos** asociados al usuario (filtrando por `GymId`).
  4. Se autoriza el acceso según los permisos vigentes.
- Los cambios en usuarios, grupos o permisos impactan de forma inmediata.

---

### Endpoints de autenticación

- `POST /auth/login` (requiere `gymId` seleccionado + credenciales)
- `POST /auth/refresh`
- `POST /auth/logout`
- `POST /auth/forgot-password`
- `POST /auth/reset-password`
- `GET /gyms/search?query=...` (para el selector “Buscar gimnasio…” en pantalla de acceso)

---

### Frontend (React)

- El **sitio público** es accesible sin login.
- El **portal** se accede desde botón **“Acceso Clientes”**.
- En pantalla de acceso al portal:
  1. Selector/Autocomplete: **Buscar gimnasio…** (consulta `/gyms/search`)
  2. Usuario
  3. Contraseña
  4. Iniciar sesión
- El **access token se mantiene en memoria** (no localStorage).
- El refresh token viaja únicamente como cookie HttpOnly.
- Ante un `401` por token expirado:
  - Se llama automáticamente a `/auth/refresh`.
  - Se reintenta la request original.

---

## Requerimientos funcionales (módulos)

### Registro de gimnasios (flujo SaaS – definición cerrada)

- El sitio público no permite el alta automática de gimnasios.
- Los gimnasios se registran mediante un formulario de Solicitud de Demo (Lead).
- La creación del Gym (Tenant) y del usuario administrador inicial
  se realiza de forma manual o mediante un proceso administrativo interno,
  fuera del alcance del MVP académico.
- Una vez creado el Gym, los usuarios del gimnasio pueden acceder al portal
  mediante el flujo de autenticación definido.

### A) Sitio público (Marketing) – sin autenticación

- Navegación pública: **Inicio, Funcionalidades, Precios, Testimonios, Blog, Contacto**.
- CTA: **Solicitar Demo** (captura lead).
- Formulario de contacto (captura mensaje).

Endpoints mínimos del sitio público:

- `POST /public/lead/demo-request`
- `POST /public/contact`
- El contenido del sitio público (Blog, Testimonios, Precios) se maneja
  como contenido estático en el frontend.
- No se implementan ABM ni endpoints para estos contenidos.

---

### B) Portal de clientes (Plataforma) – autenticado

#### Módulos del Portal (post-login)

Al autenticar, el usuario accede a los siguientes módulos
(visibles según permisos RBAC):

1. Socios
2. Turnos
3. Rutinas
4. Gestión del Gimnasio  
   (máquinas, ejercicios, equipamiento, horarios, entrenadores, configuraciones)
5. Seguridad  
   (usuarios, grupos, permisos)
6. Asistencia IA  
   (mock inicial)

---

#### 1. Gestión de socios

- Alta, consulta, modificación, eliminación lógica y recuperación.
- Validación de ingreso y verificación de cuota al día.

#### 2. Gestión de turnos

- Reservar, visualizar y cancelar turnos.
- Visualizar cupos y entrenadores disponibles.
- Envío de notificaciones automáticas por correo electrónico.

#### 3. Gestión de rutinas

- Asignación y modificación de rutinas personalizadas.
- Historial de rutinas por socio.

#### 4. Gestión del gimnasio

- Administración de máquinas, ejercicios y equipamiento.
- Gestión de horarios, entrenadores y configuraciones generales del gimnasio.

#### 5. Asistencia con IA (base y evolución)

- Recolección de datos del socio para recomendaciones futuras.
- Sugerencias automatizadas y chat asistido por IA para rutinas.
- Implementación inicial como módulo simulado (mock).
- Arquitectura preparada para integración futura sin afectar funciones críticas.

---

## 🔐 Módulo de Seguridad (obligatorio) – Portal

El sistema deberá implementar un **módulo de seguridad de aplicación** que permita gestionar el acceso a las distintas áreas del sistema mediante un modelo de permisos basado en grupos, **dentro del contexto del gimnasio (GymId)**.

---

### Funcionalidades requeridas

- Iniciar sesión.
- Cerrar sesión.
- Gestionar usuarios:
  - Agregar usuario.
  - Modificar usuario.
  - Eliminar usuario.
  - Resetear clave.
- Gestionar grupos:
  - Agregar grupo.
  - Modificar grupo.
  - Eliminar grupo.
- Cambiar clave.
- Recuperar clave.

---

### Modelo de permisos (RBAC por grupos)

- El **Administrador de Seguridad** gestiona usuarios y grupos.
- Los permisos se asignan a **grupos**.
- Un usuario puede pertenecer a **uno o varios grupos**.
- El acceso a módulos/áreas del sistema se autoriza según los permisos acumulados de los grupos asignados.
- Todas las relaciones de usuarios/grupos/permisos deben respetar el **`GymId`** (aislamiento entre gimnasios).

### Roles (grupos) iniciales y permisos asignados

El sistema debe inicializar, por cada gimnasio, los siguientes grupos
con una asignación base de permisos (editable posteriormente por el administrador):

#### ADMIN_GYM

- Gestión completa del gimnasio.
- Permisos sobre Socios, Turnos, Rutinas, Configuración del Gimnasio e IA.
- No gestiona usuarios ni grupos de seguridad.

#### ADMIN_SEGURIDAD

- SEGURIDAD_USUARIOS_GESTIONAR
- SEGURIDAD_GRUPOS_GESTIONAR

#### ENTRENADOR

- SOCIOS_VER
- TURNOS_VER
- RUTINAS_VER
- RUTINAS_ASIGNAR
- RUTINAS_EDITAR
- IA_USAR

#### ASISTENTE

- SOCIOS_VER
- SOCIOS_CREAR
- SOCIOS_EDITAR
- TURNOS_VER
- TURNOS_CREAR
- TURNOS_CANCELAR

#### SOCIO

- TURNOS_VER
- TURNOS_CREAR
- TURNOS_CANCELAR
- RUTINAS_VER
- IA_USAR

---

### Modelo de datos mínimo esperado para seguridad (Portal)

- `Usuario` (incluye `GymId`)
- `Grupo` (incluye `GymId`)
- `Permiso` (catálogo global, **sin `GymId`**)
- `UsuarioGrupo` (relación N a N)
- `GrupoPermiso` (relación N a N)
- `RefreshToken`
- `PasswordResetToken`

---

### Recuperar / Resetear clave (definición cerrada)

#### Entidad `PasswordResetToken`

- `Id`
- `UserId` (FK a Usuario)
- `TokenHash`
- `CreatedAt`
- `ExpiresAt` (**30 minutos**)
- `UsedAt` (nullable)

#### Flujo Recuperar clave

1. El usuario solicita recuperación (`/auth/forgot-password`) con email o username (y gimnasio seleccionado si aplica).
2. El backend genera un token seguro, guarda el **hash** en BD y envía el token por email.
3. El token expira a los 30 minutos.

#### Flujo Resetear clave

1. El usuario envía token + nueva clave (`/auth/reset-password`).
2. El backend valida que el token:
   - exista
   - no esté usado
   - no esté vencido
3. Se actualiza la contraseña (hasheada) y se marca el token como usado.
4. Se revocan los refresh tokens activos del usuario.

---

### Reglas para contraseñas

- Hashear contraseñas con algoritmo robusto (bcrypt o argon2).
- Longitud mínima obligatoria.
- Invalidar sesiones activas ante cambio/reset de clave.

---

## Modelo de datos mínimo (SaaS + Portal)

### SaaS / Plataforma

- `Gym` (Tenant)
- `Lead` (Solicitar demo)
- `ContactMessage` (Contacto)

### Seguridad (Portal)

- `Usuario` (con `GymId`)
- `Grupo` (con `GymId`)
- `Permiso` (catálogo global)
- `UsuarioGrupo`
- `GrupoPermiso`
- `RefreshToken`
- `PasswordResetToken`

---

## Alcance (inclusiones / exclusiones)

### Incluye

- Sitio público (marketing) con captura de leads.
- Portal de clientes (plataforma) multi-tenant por `GymId`.
- Gestión del gimnasio.
- Gestión de socios.
- Gestión de turnos.
- Gestión de rutinas.
- Asistencia mediante IA (nivel base).
- Módulo de Seguridad completo (RBAC + JWT + Refresh).

### Excluye (por ahora)

- Integración con pagos.
- Aplicación móvil nativa.
- Múltiples sedes por gimnasio.
- Análisis avanzado y dispositivos inteligentes.
- Gestión de profesionales externos, asignación profesional ↔ socio, seguimiento nutricional.

---

## Requerimientos no funcionales

- **Concurrencia:** hasta 75 usuarios concurrentes.
- **Performance:** reserva de turnos ≤ 2 segundos.
- **Usabilidad:** operaciones clave en ≤ 6 clics.
- **Seguridad:**
  - Encriptación de datos sensibles.
  - Control de acceso basado en permisos por grupos (aislados por `GymId`).
  - Autenticación JWT + Refresh con rotación y revocación.
  - Gestión de credenciales (cambio, reset y recuperación).
  - Trazabilidad mínima de operaciones administrativas.
- **Disponibilidad:** durante horario de atención.
- **Recuperación ante desastres:** restauración completa < 24 horas.
- **Compatibilidad:** Chrome, Firefox, Edge y Safari.
- **IA:** respuesta ≤ 3 segundos; datos anonimizados.

---

## Entregables esperados

A) Propuesta de arquitectura general.  
B) Estructura de proyectos y carpetas.  
C) Modelo de datos.  
D) Endpoints y casos de uso.  
E) Decisiones técnicas justificadas.  
F) Plan de implementación por etapas.

---

## Reglas de trabajo

- No asumir funcionalidades no explícitas.
- Declarar supuestos cuando falte información.
- Priorizar simplicidad y buenas prácticas.
- Mantener coherencia entre frontend, backend y base de datos.
- Diseñar como si fuera defendido ante tribunal académico.
- El envío de emails se implementa mediante un servicio abstracto de email.

---

## Registro del trabajo del agente (obligatorio)

Mantener un archivo **`AI_WORKLOG.md`** en la raíz del proyecto.

Registrar:

- Decisiones de arquitectura.
- Módulos creados o modificados.
- Supuestos asumidos.
- Cambios relevantes.
- Próximos pasos.

No borrar entradas anteriores.

---

## Antes de generar código

1. Analizar contexto.
2. Proponer diseño.
3. Validar coherencia.
4. Recién después implementar.
