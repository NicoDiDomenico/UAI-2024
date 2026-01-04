# Plan de Trabajo y Estructura

Este plan configura una aplicación full-stack básica con:

- **Backend**: API REST con Express que responde en puerto 3001
- **Frontend**: Aplicación React con TypeScript que corre en puerto 5173
- **Base de datos**: PostgreSQL con Prisma como ORM para manejar migraciones

## Estructura de Carpetas Mínima

```
v2/
├── backend/
│   ├── src/
│   │   └── index.ts
│   ├── prisma/
│   │   └── schema.prisma
│   ├── package.json
│   └── tsconfig.json
├── frontend/
│   ├── src/
│   │   ├── App.tsx
│   │   └── main.tsx
│   ├── index.html
│   ├── package.json
│   ├── tsconfig.json
│   └── vite.config.ts
└── README.md
```

## Plan de Trabajo (10 pasos)

### **Paso 1: Configurar Backend**

**Qué hace:** Crea la carpeta del backend e instala las dependencias mínimas necesarias.

- `express`: framework web para crear el servidor y los endpoints
- `prisma` + `@prisma/client`: ORM para conectar con PostgreSQL
- `typescript` y tipos: para trabajar con TypeScript
- `ts-node`: ejecuta TypeScript directamente sin compilar
- `nodemon`: reinicia automáticamente el servidor cuando cambias código

```powershell
cd c:\Users\Nicol\Desktop\UAI-2024\MindFit Intelligence\v2
mkdir backend
cd backend
npm init -y
npm install express prisma @prisma/client
npm install -D typescript @types/express @types/node ts-node nodemon
npx tsc --init
```

### **Paso 2: Crear archivo index.ts del backend**

**Qué hace:** Crea el punto de entrada del servidor backend.

- Define un servidor Express
- Crea el endpoint `GET /health` que responde `{"status": "ok"}` para verificar que el servidor funciona
- El servidor escucha en puerto 3001

Crear `backend/src/index.ts` con endpoint GET /health y servidor Express en puerto 3001

### **Paso 3: Configurar scripts en package.json del backend**

**Qué hace:** Define comandos npm para trabajar con el backend.

- `npm run dev`: inicia el servidor en modo desarrollo con recarga automática
- `npm run build`: compila TypeScript a JavaScript para producción

Agregar scripts: `"dev": "nodemon src/index.ts"` y `"build": "tsc"`

### **Paso 4: Inicializar Prisma**

**Qué hace:** Configura Prisma en tu proyecto.

- Crea la carpeta `prisma/` con el archivo `schema.prisma` (donde defines tus tablas)
- Crea un archivo `.env` para guardar la URL de conexión a PostgreSQL de forma segura

```powershell
cd c:\Users\Nicol\Desktop\UAI-2024\MindFit Intelligence\v2\backend
npx prisma init
```

### **Paso 5: Configurar conexión PostgreSQL**

**Qué hace:** Conecta Prisma con tu base de datos PostgreSQL.

- Editas el archivo `.env` con las credenciales de tu base de datos local
- Asegúrate de tener PostgreSQL instalado y corriendo
- Reemplaza `usuario` y `password` con tus credenciales reales

Editar `.env` con tu conexión: `DATABASE_URL="postgresql://usuario:password@localhost:5432/mindfit_db"`

### **Paso 6: Crear schema básico en Prisma**

**Qué hace:** Define la estructura de tu base de datos.

- Creas un modelo (tabla) de ejemplo para probar la conexión
- Ejemplo: tabla `User` con campos básicos como `id` y `email`
- Prisma usa este schema para generar las migraciones SQL

Editar `prisma/schema.prisma` con un modelo de prueba (ej: User con id y email)

### **Paso 7: Ejecutar migración**

**Qué hace:** Crea las tablas en PostgreSQL según tu schema.

- Genera archivos SQL en `prisma/migrations/` con los comandos CREATE TABLE
- Ejecuta esos comandos en tu base de datos
- Verifica que la conexión a PostgreSQL funciona correctamente

```powershell
npx prisma migrate dev --name init
```

### **Paso 8: Configurar Frontend**

**Qué hace:** Crea la aplicación React con TypeScript usando Vite.

- Vite es un bundler moderno y rápido (alternativa a Create React App)
- `--template react-ts` usa la plantilla oficial de React + TypeScript
- Instala todas las dependencias necesarias (React, TypeScript, etc.)

```powershell
cd c:\Users\Nicol\Desktop\UAI-2024\MindFit Intelligence\v2
npm create vite@latest frontend -- --template react-ts
cd frontend
npm install
```

### **Paso 9: Modificar App.tsx**

**Qué hace:** Simplifica la interfaz para verificar que React funciona.

- Eliminas el código de ejemplo que trae Vite
- Dejas solo un texto "App OK" para confirmar que la app se renderiza correctamente

Editar `frontend/src/App.tsx` para mostrar solo "App OK"

### **Paso 10: Levantar ambos servidores**

**Qué hace:** Inicia backend y frontend simultáneamente.

- Necesitas 2 terminales porque son procesos separados
- Backend corre en puerto 3001 y expone la API
- Frontend corre en puerto 5173 y sirve la interfaz React

Terminal 1:

```powershell
cd backend
npm run dev
```

Terminal 2:

```powershell
cd frontend
npm run dev
```

**Verificación**:

- Backend: abre http://localhost:3001/health → deberías ver `{"status": "ok"}`
- Frontend: abre http://localhost:5173 → deberías ver "App OK" en pantalla
