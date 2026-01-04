# PROMPT – LEVANTAMIENTO DEL PROYECTO

## MindFit Intelligence

Actuá como **arquitecto de software senior y desarrollador full-stack**, con experiencia en proyectos reales y académicos.

---

## Objetivo

Diseñar y planificar un **sistema web de gestión integral de gimnasios** (MindFit Intelligence), destinado a **gimnasios de musculación de Rosario, Argentina**, siguiendo buenas prácticas de ingeniería de software, arquitectura limpia y patrones de diseño adecuados.

---

## Contexto del proyecto

- **Tipo de aplicación:** Web (sin app nativa), accesible desde navegador (PC y móvil).
- **Público objetivo:** Dueño del gimnasio, asistentes, entrenadores y socios.
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

## 🔐 Diseño de Autenticación (definido)

### Tokens

- **Access Token (JWT):**

  - Duración: **15 minutos**.
  - Se envía en cada request mediante el header  
    `Authorization: Bearer <token>`.
  - Contiene únicamente claims de identificación:
    - `sub` (UserId)
    - email o username.
  - **No contiene información de grupos ni permisos**.

- **Refresh Token:**
  - Duración: **7 días**.
  - Se almacena en **cookie HttpOnly**.
  - Se persiste en base de datos para permitir **revocación y rotación**.

---

### Resolución de permisos

- En cada request autenticado:
  1. Se valida el JWT.
  2. Se obtiene el `UserId` desde el claim `sub`.
  3. Se consultan en base de datos los **grupos y permisos** asociados al usuario.
  4. Se autoriza el acceso según los permisos vigentes.
- Los cambios en usuarios, grupos o permisos impactan de forma inmediata.

---

### Endpoints de autenticación

- `POST /auth/login`
- `POST /auth/refresh`
- `POST /auth/logout`
- `POST /auth/forgot-password`
- `POST /auth/reset-password`

---

### Frontend (React)

- El **access token se mantiene en memoria** (no localStorage).
- El refresh token viaja únicamente como cookie HttpOnly.
- Ante un `401` por token expirado:
  - Se llama automáticamente a `/auth/refresh`.
  - Se reintenta la request original.

---

## Requerimientos funcionales (módulos principales)

### 1. Gestión de socios

- Alta, consulta, modificación, eliminación lógica y recuperación.
- Validación de ingreso y verificación de cuota al día.

### 2. Gestión de turnos

- Reservar, visualizar y cancelar turnos.
- Visualizar cupos y entrenadores disponibles.
- Envío de notificaciones automáticas por correo electrónico.

### 3. Gestión de rutinas

- Asignación y modificación de rutinas personalizadas.
- Historial de rutinas por socio.

### 4. Gestión del gimnasio

- Administración de máquinas, ejercicios y equipamiento.
- Gestión de horarios, entrenadores y configuraciones generales del gimnasio.

### 5. Asistencia con IA (base y evolución)

- Recolección de datos del socio para recomendaciones futuras.
- Sugerencias automatizadas y chat asistido por IA para rutinas.
- Implementación desacoplada, permitiendo evolución sin afectar funciones críticas.

---

## 🔐 Módulo de Seguridad (obligatorio)

El sistema deberá implementar un **módulo de seguridad de aplicación** que permita gestionar el acceso a las distintas áreas del sistema mediante un modelo de permisos basado en grupos.

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

---

### Modelo de datos mínimo esperado para seguridad

- `Usuario`
- `Grupo`
- `Permiso`
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

1. El usuario solicita recuperación (`/auth/forgot-password`) con email o username.
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

## Alcance (inclusiones / exclusiones)

### Incluye

- Gestión del gimnasio.
- Gestión de socios.
- Gestión de turnos.
- Gestión de rutinas.
- Asistencia mediante IA (nivel base).
- Módulo de Seguridad completo (RBAC + JWT + Refresh).

### Excluye (por ahora)

- Gestión de profesionales externos.
- Asignación profesional ↔ socio.
- Seguimiento nutricional.
- Integración con medios de pago.
- Aplicación móvil nativa.
- Múltiples sedes.
- Análisis avanzado y dispositivos inteligentes.

---

## Requerimientos no funcionales

- **Concurrencia:** hasta 75 usuarios concurrentes.
- **Performance:** reserva de turnos ≤ 2 segundos.
- **Usabilidad:** operaciones clave en ≤ 6 clics.
- **Seguridad:**
  - Encriptación de datos sensibles.
  - Control de acceso basado en permisos por grupos.
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
