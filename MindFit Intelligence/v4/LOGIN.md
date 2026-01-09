# Plan de Implementación: Sistema de Login con JWT

## 📋 Resumen

Sistema de autenticación multi-tenant que requiere selección de gimnasio, usuario y contraseña para acceder a la aplicación. Utiliza JWT para mantener la sesión.

---

## 🗄️ Análisis de la Base de Datos

### Estructura Multi-tenant

- **Gym**: Cada gimnasio es un tenant independiente
- **Usuario**: Pertenece a un Gym específico (gymId)
- **Persona**: Información personal del usuario
- **Rol**: Roles dentro de cada gimnasio (RBAC)
- **Permiso**: Permisos globales que se asignan a roles

### Datos de Prueba (seed.ts)

- **Gym**: "Gym Olimpo"
- **Usuario**: `admin` / Contraseña: `admin123`
- **Email**: admin@olimpo.com
- **Rol**: ADMIN con todos los permisos

---

## 🔐 Flujo de Autenticación

### 1. Pantalla de Login

```
┌─────────────────────────────────────┐
│         MindFit Intelligence        │
├─────────────────────────────────────┤
│                                     │
│  Seleccionar Gimnasio:              │
│  [▼ Gym Olimpo          ]           │
│                                     │
│  Usuario:                           │
│  [admin                ]           │
│                                     │
│  Contraseña:                        │
│  [••••••••              ]           │
│                                     │
│         [  INICIAR SESIÓN  ]        │
│                                     │
└─────────────────────────────────────┘
```

### 2. Proceso de Autenticación

1. Usuario selecciona un Gym del dropdown
2. Ingresa nombre de usuario
3. Ingresa contraseña
4. Backend valida credenciales
5. Backend genera JWT con información del usuario
6. Frontend guarda token y redirige a Home

---

## 🛠️ Implementación Backend

### Estructura de Carpetas

```
backend/
├── src/
│   ├── index.ts (servidor principal)
│   ├── config/
│   │   └── env.ts
│   ├── middleware/
│   │   ├── auth.middleware.ts
│   │   └── error.middleware.ts
│   ├── routes/
│   │   ├── auth.routes.ts
│   │   └── gym.routes.ts
│   ├── controllers/
│   │   ├── auth.controller.ts
│   │   └── gym.controller.ts
│   ├── services/
│   │   ├── auth.service.ts
│   │   └── gym.service.ts
│   ├── types/
│   │   └── jwt.types.ts
│   └── utils/
│       ├── jwt.util.ts
│       └── password.util.ts
```

### Endpoints a Crear

#### 1. GET /api/gyms

Obtener lista de gimnasios para el dropdown

```typescript
Response: {
  gyms: [{ gymId: 1, nombre: "Gym Olimpo" }];
}
```

#### 2. POST /api/auth/login

Autenticar usuario

```typescript
Request: {
  gymId: number,
  nombreUsuario: string,
  password: string
}

Response: {
  token: string,
  usuario: {
    usuarioId: number,
    nombreUsuario: string,
    gym: { gymId: number, nombre: string },
    persona: {
      nombreYApellido: string,
      email: string
    },
    roles: string[],
    permisos: string[]
  }
}
```

#### 3. GET /api/auth/me (protegido)

Verificar sesión y obtener datos del usuario actual

```typescript
Headers: { Authorization: "Bearer <token>" }

Response: {
  usuario: { ... }
}
```

---

## 🔑 Estructura del JWT

### Payload del Token

```typescript
interface JwtPayload {
  usuarioId: number;
  gymId: number;
  nombreUsuario: string;
  roles: string[];
  permisos: string[];
  iat: number; // issued at
  exp: number; // expiration
}
```

### Configuración

- **Secret**: Variable de entorno `JWT_SECRET`
- **Expiración**: 8 horas (configurable)
- **Algoritmo**: HS256

---

## 🎨 Implementación Frontend

### Estructura de Carpetas

```
frontend/
├── src/
│   ├── pages/
│   │   ├── Login.tsx
│   │   └── Home.tsx
│   ├── components/
│   │   ├── ui/
│   │   │   ├── Input.tsx
│   │   │   ├── Button.tsx
│   │   │   └── Select.tsx
│   │   └── auth/
│   │       └── ProtectedRoute.tsx
│   ├── services/
│   │   └── api.service.ts
│   ├── context/
│   │   └── AuthContext.tsx
│   ├── hooks/
│   │   └── useAuth.ts
│   ├── types/
│   │   └── auth.types.ts
│   └── utils/
│       └── storage.util.ts
```

### Componentes Principales

#### 1. Login.tsx

- Dropdown para seleccionar gimnasio
- Inputs para usuario y contraseña
- Botón de login
- Manejo de errores (credenciales inválidas, gym no seleccionado, etc.)

#### 2. AuthContext

Contexto global para manejar:

- Estado de autenticación (isAuthenticated)
- Datos del usuario actual
- Token JWT
- Función login()
- Función logout()

#### 3. ProtectedRoute

HOC para proteger rutas que requieren autenticación

#### 4. Home.tsx

Página en blanco que muestra mensaje de bienvenida con nombre del usuario

---

## 🔧 Tecnologías y Dependencias

### Backend

```json
{
  "dependencies": {
    "express": "^5.2.1",
    "jsonwebtoken": "^9.0.2",
    "bcrypt": "^6.0.0",
    "@prisma/client": "^7.2.0",
    "dotenv": "^17.2.3",
    "cors": "^2.8.5",
    "@prisma/adapter-pg": "^7.2.0",
    "pg": "^8.16.3"
  },
  "devDependencies": {
    "@types/express": "^5.0.6",
    "@types/jsonwebtoken": "^9.0.10",
    "@types/bcrypt": "^6.0.0",
    "@types/cors": "^2.8.19",
    "@types/node": "^25.0.3",
    "@types/pg": "^8.16.0",
    "typescript": "^5.9.3",
    "ts-node": "^10.9.2",
    "nodemon": "^3.1.11"
  }
}
```

### Frontend

```json
{
  "dependencies": {
    "react": "^19.2.0",
    "react-dom": "^19.2.0",
    "react-router-dom": "^7.12.0",
    "axios": "^1.13.2"
  },
  "devDependencies": {
    "@types/react": "^19.2.5",
    "@types/react-dom": "^19.2.3",
    "typescript": "^5.9.3",
    "vite": "^7.2.4",
    "@vitejs/plugin-react": "^5.1.1"
  }
}
```

---

## 🔒 Seguridad

### Medidas Implementadas

1. **Contraseñas hasheadas**: bcrypt con salt rounds = 10
2. **JWT**: Tokens firmados y con expiración
3. **Validación multi-tenant**: Usuario debe pertenecer al gym seleccionado
4. **CORS**: Configurado para permitir solo orígenes confiables
5. **HTTP-only cookies** (opcional): Para mayor seguridad del token

### Validaciones

- Verificar que el gym existe
- Verificar que el usuario existe en ese gym
- Comparar password con bcrypt
- Verificar que el usuario tiene al menos un rol activo

---

## 📝 Variables de Entorno

### Backend (.env)

```env
DATABASE_URL="postgresql://user:password@localhost:5432/mindfit"
JWT_SECRET="tu-secret-super-seguro-cambiar-en-produccion"
JWT_EXPIRATION="8h"
PORT=3000
FRONTEND_URL="http://localhost:5173"
```

### Frontend (.env)

```env
VITE_API_URL="http://localhost:3000/api"
```

---

## 🚀 Flujo de Desarrollo

### Fase 1: Backend

1. ✅ Configurar variables de entorno
2. ✅ Crear utilidades (JWT, password)
3. ✅ Implementar servicios (auth, gym)
4. ✅ Crear controladores
5. ✅ Definir rutas
6. ✅ Crear middleware de autenticación
7. ✅ Probar endpoints con Postman/Thunder Client

### Fase 2: Frontend

1. ✅ Configurar React Router
2. ✅ Crear AuthContext
3. ✅ Implementar servicio API (axios)
4. ✅ Crear componentes de UI básicos
5. ✅ Desarrollar página de Login
6. ✅ Implementar ProtectedRoute
7. ✅ Crear página Home básica
8. ✅ Integrar y probar flujo completo

### Fase 3: Pruebas

1. ✅ Login exitoso
2. ✅ Credenciales incorrectas
3. ✅ Usuario no existe
4. ✅ Gym no seleccionado
5. ✅ Token expirado
6. ✅ Acceso a rutas protegidas sin token
7. ✅ Logout y limpieza de token

---

## 🎯 Casos de Uso

### Caso 1: Login Exitoso

1. Usuario selecciona "Gym Olimpo"
2. Ingresa "admin" y "admin123"
3. Backend valida y genera JWT
4. Frontend guarda token en localStorage
5. Usuario es redirigido a /home
6. Home muestra: "Bienvenido, Admin Olimpo"

### Caso 2: Credenciales Incorrectas

1. Usuario selecciona gym e ingresa datos erróneos
2. Backend retorna error 401
3. Frontend muestra mensaje: "Usuario o contraseña incorrectos"

### Caso 3: Token Expirado

1. Usuario intenta acceder a ruta protegida
2. Middleware detecta token expirado
3. Frontend redirige a login
4. Muestra mensaje: "Sesión expirada, ingrese nuevamente"

### Caso 4: Acceso Directo a Home

1. Usuario intenta ir a /home sin autenticarse
2. ProtectedRoute detecta falta de token
3. Redirige automáticamente a /login

---

## 📊 Modelo de Datos de Respuesta

### Usuario Completo (para JWT y Context)

```typescript
interface AuthUser {
  usuarioId: number;
  nombreUsuario: string;
  gym: {
    gymId: number;
    nombre: string;
  };
  persona: {
    personaId: number;
    nombreYApellido: string;
    email: string;
  };
  roles: string[]; // ["ADMIN"]
  permisos: string[]; // ["USER_CREATE", "USER_UPDATE", ...]
}
```

---

## ✨ Mejoras Futuras (No implementar ahora)

- [ ] Recuperación de contraseña
- [ ] Recordar usuario/gym
- [ ] Multi-factor authentication
- [ ] Refresh tokens
- [ ] Rate limiting en login
- [ ] Captcha después de X intentos fallidos
- [ ] Logs de auditoría de accesos
- [ ] Sesiones concurrentes

---

## 🧪 Pruebas Manuales

### Checklist

```
□ Obtener lista de gyms desde el dropdown
□ Login con credenciales correctas (admin/admin123)
□ Verificar que se guarda el token
□ Verificar redirección a /home
□ Ver nombre de usuario en Home
□ Cerrar sesión
□ Intentar acceder a /home sin token → redirige a login
□ Login con contraseña incorrecta → error
□ Login con usuario inexistente → error
□ Login sin seleccionar gym → error de validación
```

---

## 📌 Notas Importantes

1. **Multi-tenancy**: Cada consulta debe filtrar por `gymId`
2. **Unicidad**: `nombreUsuario` es único por gym, NO global
3. **RBAC**: Los permisos se cargan desde roles + permisos directos
4. **Seed**: Ya existe usuario "admin" con contraseña "admin123" en "Gym Olimpo"
5. **Home**: Por ahora solo mostrar mensaje de bienvenida, sin funcionalidad

---

## 🎬 Siguiente Paso

Una vez aprobado este plan, procederemos a:

1. Implementar Backend (endpoints, servicios, middleware)
2. Implementar Frontend (login, context, rutas)
3. Integrar y probar flujo completo

**¿Proceder con la implementación?** 🚀
