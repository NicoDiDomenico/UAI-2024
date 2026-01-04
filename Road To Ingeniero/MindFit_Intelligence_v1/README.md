# MindFit Intelligence

## Sistema Web de Gestión Integral de Gimnasios

**Modalidad:** SaaS B2B  
**Stack:** React + .NET 8 + SQL Server  
**Tipo:** Proyecto Académico (MVP)

---

## 📋 Descripción

MindFit Intelligence es un sistema web SaaS para la gestión integral de gimnasios de musculación en Rosario, Argentina. El sistema consta de dos áreas principales:

1. **Sitio Público (Marketing):** Landing page para captación de clientes (gimnasios)
2. **Portal de Clientes (Plataforma):** Sistema multi-tenant para gestión del gimnasio

---

## 🎯 Objetivos del Proyecto

- Diseñar y planificar un sistema web siguiendo buenas prácticas de ingeniería de software
- Implementar arquitectura limpia y patrones de diseño adecuados
- Desarrollar un MVP académico incremental con base para evolucionar a funcionalidades avanzadas
- Incluir módulos de gestión de socios, turnos, rutinas, gimnasio, seguridad e IA (mock inicial)

---

## 📚 Documentación

La documentación completa del proyecto se encuentra en los siguientes archivos:

### Documentos de Diseño

| Documento                                          | Descripción                                                                  |
| -------------------------------------------------- | ---------------------------------------------------------------------------- |
| [ARQUITECTURA_GENERAL.md](ARQUITECTURA_GENERAL.md) | Arquitectura de alto nivel, componentes, flujos de datos, patrones de diseño |
| [ESTRUCTURA_PROYECTO.md](ESTRUCTURA_PROYECTO.md)   | Estructura de carpetas Backend y Frontend, convenciones de nomenclatura      |
| [MODELO_DATOS.md](MODELO_DATOS.md)                 | Modelo de datos completo con 20+ entidades, relaciones, índices, scripts SQL |
| [ENDPOINTS_API.md](ENDPOINTS_API.md)               | Especificación de 51+ endpoints REST con requests/responses y casos de uso   |
| [DECISIONES_TECNICAS.md](DECISIONES_TECNICAS.md)   | Justificación de decisiones arquitectónicas y análisis de alternativas       |
| [PLAN_IMPLEMENTACION.md](PLAN_IMPLEMENTACION.md)   | Plan de 10 fases incrementales con cronograma de 14 semanas                  |

### Documentos de Gestión

| Documento                                                        | Descripción                                                                  |
| ---------------------------------------------------------------- | ---------------------------------------------------------------------------- |
| [AI_WORKLOG.md](AI_WORKLOG.md)                                   | Registro de decisiones, supuestos, cambios y evolución del proyecto          |
| [PROMPT_MindFit_Intelligence.md](PROMPT_MindFit_Intelligence.md) | Especificación original del proyecto (requerimientos funcionales y técnicos) |

---

## 🏗️ Arquitectura

### Componentes Principales

```
┌─────────────────────────────────────────────────────────────┐
│                    FRONTEND (React.js)                       │
│  ┌────────────────────┐  ┌────────────────────────────────┐ │
│  │  Sitio Público     │  │  Portal de Clientes            │ │
│  │  (Marketing)       │  │  (Plataforma Multi-tenant)     │ │
│  └────────────────────┘  └────────────────────────────────┘ │
└──────────────────────┬──────────────────────────────────────┘
                       │ REST API (JWT Bearer)
                       ▼
┌─────────────────────────────────────────────────────────────┐
│              BACKEND (ASP.NET Core 8)                        │
│  Controllers → Services → Models → DbContext                 │
└──────────────────────┬──────────────────────────────────────┘
                       │ EF Core
                       ▼
┌─────────────────────────────────────────────────────────────┐
│           BASE DE DATOS (SQL Server)                         │
│  Multi-tenant con discriminador GymId                        │
└─────────────────────────────────────────────────────────────┘
```

### Stack Tecnológico

- **Frontend:** React 18+, React Router, Axios, Material-UI o TailwindCSS
- **Backend:** ASP.NET Core 8, Entity Framework Core 8, FluentValidation
- **Base de Datos:** Microsoft SQL Server 2019+
- **Autenticación:** JWT (Access Token 15 min) + Refresh Token (7 días, HttpOnly cookie)
- **Autorización:** RBAC por grupos con permisos dinámicos

---

## 🔐 Seguridad

### Multi-tenancy

- **Base de datos única** con discriminador `GymId` (TenantId)
- **Query Filters automáticos** de EF Core para aislamiento
- Todas las entidades del portal incluyen `GymId`

### Autenticación

- **Access Token (JWT):**

  - Duración: 15 minutos
  - Claims: `sub` (UserId), `email`, `gymId`
  - No incluye permisos (se consultan en cada request)

- **Refresh Token:**
  - Duración: 7 días
  - Almacenado en cookie HttpOnly (protegido contra XSS)
  - Persistido en BD para revocación y rotación

### Autorización

- **RBAC por grupos:** Usuarios pertenecen a grupos, grupos tienen permisos
- **Permisos dinámicos:** Se consultan en BD en cada request (cambios inmediatos)
- **Grupos predefinidos:** ADMIN_GYM, ADMIN_SEGURIDAD, ENTRENADOR, ASISTENTE, SOCIO

### Contraseñas

- **Hasheadas con BCrypt** (work factor 12)
- **Recuperación de contraseña:** Token con expiración de 30 minutos
- **Cambio de contraseña:** Revoca todos los Refresh Tokens activos

---

## 📦 Módulos del Sistema

### Sitio Público (sin autenticación)

- **Inicio:** Landing page
- **Funcionalidades:** Descripción del sistema
- **Precios:** Planes y precios
- **Testimonios:** Testimonios de clientes
- **Blog:** Artículos (contenido estático)
- **Contacto:** Formulario de contacto
- **Solicitar Demo:** Captura de leads

### Portal de Clientes (autenticado, multi-tenant)

1. **Socios:** Gestión completa (CRUD, eliminación lógica, validación de cuota)
2. **Turnos:** Reserva, visualización, cancelación, notificaciones por email
3. **Rutinas:** Asignación personalizada, historial por socio
4. **Gimnasio:** Máquinas, ejercicios, equipamiento, horarios, entrenadores, configuraciones
5. **Seguridad:** Usuarios, grupos, permisos (RBAC)
6. **IA:** Asistente inteligente (mock inicial, preparado para evolución)

---

## 🗄️ Modelo de Datos

### Entidades Principales

**Capa SaaS:**

- Gym (Tenant)
- Lead (Solicitudes de demo)
- ContactMessage (Mensajes de contacto)

**Capa Seguridad:**

- Usuario, Grupo, Permiso
- UsuarioGrupo (N:N), GrupoPermiso (N:N)
- RefreshToken, PasswordResetToken

**Capa Portal:**

- Socio, Turno, Rutina, RutinaEjercicio
- Ejercicio, Maquina, Equipamiento
- Horario, Entrenador, Configuracion

**Total:** 20+ entidades con relaciones definidas

Ver [MODELO_DATOS.md](MODELO_DATOS.md) para detalles completos.

---

## 🌐 API REST

### Endpoints por Módulo

| Módulo        | Endpoints | Autenticación                                                               |
| ------------- | --------- | --------------------------------------------------------------------------- |
| Autenticación | 6         | Login, Refresh, Logout, Forgot Password, Reset Password, Change Password    |
| Sitio Público | 3         | Lead, Contacto, Búsqueda de gimnasios                                       |
| Socios        | 6         | CRUD + Recuperar eliminados                                                 |
| Turnos        | 4         | Listar, Disponibles, Reservar, Cancelar                                     |
| Rutinas       | 5         | CRUD + Detalle con ejercicios                                               |
| Gimnasio      | 15+       | Máquinas, Ejercicios, Equipamiento, Horarios, Entrenadores, Configuraciones |
| Seguridad     | 10+       | Usuarios, Grupos, Permisos                                                  |
| IA            | 2         | Chat, Recomendaciones (mock)                                                |

**Total:** 51+ endpoints documentados

Ver [ENDPOINTS_API.md](ENDPOINTS_API.md) para especificación completa.

---

## 📅 Plan de Implementación

### Fases del Proyecto

| Fase                                        | Duración    | Descripción                                       |
| ------------------------------------------- | ----------- | ------------------------------------------------- |
| **Fase 0:** Preparación                     | 3-5 días    | Configurar entorno, crear estructura de proyectos |
| **Fase 1:** Infraestructura y Autenticación | 1-2 semanas | JWT + Refresh Token, Multi-tenancy, RBAC          |
| **Fase 2:** Módulo Seguridad                | 1 semana    | Gestión de usuarios, grupos, permisos             |
| **Fase 3:** Módulo Socios                   | 1 semana    | CRUD completo de socios                           |
| **Fase 4:** Módulo Turnos                   | 1-2 semanas | Reservas, validaciones, notificaciones            |
| **Fase 5:** Módulo Rutinas                  | 1-2 semanas | Rutinas personalizadas con ejercicios             |
| **Fase 6:** Módulo Gimnasio                 | 1 semana    | Máquinas, equipamiento, entrenadores              |
| **Fase 7:** Módulo IA                       | 3-5 días    | Mock inicial de asistente IA                      |
| **Fase 8:** Sitio Público                   | 1 semana    | Landing page, captura de leads                    |
| **Fase 9:** Testing y Refinamiento          | 1-2 semanas | Tests, optimización, corrección de bugs           |
| **Fase 10:** Deployment                     | 3-5 días    | Despliegue en producción                          |

**Duración total estimada:** 14 semanas (3.5 meses)

Ver [PLAN_IMPLEMENTACION.md](PLAN_IMPLEMENTACION.md) para detalles completos.

---

## 🚀 Próximos Pasos

### Para iniciar la implementación:

1. **Preparar entorno de desarrollo:**

   - Instalar .NET 8 SDK
   - Instalar Node.js 18+
   - Instalar SQL Server 2019+
   - Instalar Visual Studio Code o Visual Studio 2022

2. **Crear proyectos:**

   - Backend: `dotnet new webapi -n MindFit.Api`
   - Frontend: `npm create vite@latest mindfit-web -- --template react`

3. **Configurar base de datos:**

   - Crear BD: `CREATE DATABASE MindFitDB`
   - Configurar connection string en `appsettings.json`

4. **Iniciar con Fase 1:**

   - Implementar modelo de datos base (Gym, Usuario, Grupo, Permiso)
   - Configurar EF Core con Query Filters
   - Implementar autenticación (JWT + Refresh Token)
   - Implementar RBAC básico

5. **Seguir plan de implementación incremental**

Ver [PLAN_IMPLEMENTACION.md - Fase 0](PLAN_IMPLEMENTACION.md#fase-0-preparación) para instrucciones detalladas.

---

## 📊 Estadísticas del Diseño

- **Entidades:** 20+ tablas en SQL Server
- **Endpoints:** 51+ endpoints REST documentados
- **Permisos:** 24+ permisos granulares
- **Grupos predefinidos:** 5 roles base (ADMIN_GYM, ADMIN_SEGURIDAD, ENTRENADOR, ASISTENTE, SOCIO)
- **Módulos del portal:** 6 módulos funcionales
- **Documentación:** 7 archivos detallados (200+ páginas)

---

## 🎓 Alcance Académico

### Incluye

✅ Sitio público (marketing) con captura de leads  
✅ Portal multi-tenant por `GymId`  
✅ Gestión de socios, turnos, rutinas  
✅ Gestión del gimnasio (máquinas, ejercicios, horarios, entrenadores)  
✅ Módulo de seguridad completo (RBAC + JWT + Refresh)  
✅ Asistencia IA (mock inicial, arquitectura para evolución)

### Excluye (por ahora)

❌ Integración con pagos (MercadoPago, Stripe)  
❌ Aplicación móvil nativa  
❌ Múltiples sedes por gimnasio  
❌ Análisis avanzado y dispositivos inteligentes  
❌ Gestión contable, fiscal e impositiva  
❌ Seguimiento nutricional completo

---

## 📝 Requerimientos No Funcionales

- **Concurrencia:** Hasta 75 usuarios concurrentes
- **Performance:** Reserva de turnos ≤ 2 segundos
- **Usabilidad:** Operaciones clave en ≤ 6 clics
- **Seguridad:** Encriptación, control de acceso RBAC, autenticación JWT, trazabilidad
- **Disponibilidad:** Durante horario de atención
- **Recuperación:** Restauración completa < 24 horas
- **Compatibilidad:** Chrome, Firefox, Edge, Safari
- **IA:** Respuesta ≤ 3 segundos (mock)

---

## 🤝 Contribución

Este es un proyecto académico. Para contribuir:

1. Leer toda la documentación (especialmente [ARQUITECTURA_GENERAL.md](ARQUITECTURA_GENERAL.md) y [DECISIONES_TECNICAS.md](DECISIONES_TECNICAS.md))
2. Seguir las convenciones definidas en [ESTRUCTURA_PROYECTO.md](ESTRUCTURA_PROYECTO.md)
3. Implementar según el [PLAN_IMPLEMENTACION.md](PLAN_IMPLEMENTACION.md)
4. Actualizar [AI_WORKLOG.md](AI_WORKLOG.md) con cambios importantes

---

## 📄 Licencia

Proyecto académico - Universidad Abierta Interamericana (UAI) 2024-2026

---

## 📧 Contacto

Para consultas sobre el proyecto, referirse a la documentación en este repositorio.

---

**Última actualización:** 01/01/2026  
**Versión:** 1.0 (Diseño completo)  
**Estado:** Listo para implementación
