# Guía de Inicio Rápido - MindFit Intelligence

## 🚀 Inicio Rápido con Docker

La forma más rápida de ejecutar el proyecto completo:

```bash
# Clonar el repositorio
git clone <repository-url>
cd MindFit_Intelligence_v2

# Ejecutar con Docker Compose
docker-compose up -d

# Esperar a que los contenedores estén listos (aproximadamente 2 minutos)
# La aplicación estará disponible en:
# - Frontend: http://localhost:3000
# - Backend API: http://localhost:5000
# - Swagger: http://localhost:5000/swagger
```

## 📋 Inicio Manual (Sin Docker)

### 1. Base de Datos

```bash
# Abrir SQL Server Management Studio
# Conectarse a tu instancia de SQL Server
# Ejecutar los scripts en orden:
# 1. 01_CreateSchema.sql
# 2. 02_StoredProcedures.sql
# 3. 03_SampleData.sql
# 4. 04_Triggers.sql
# 5. 05_Views.sql
```

### 2. Backend

```bash
cd src/Backend/MindFit.API

# Editar appsettings.json con tu cadena de conexión
# Luego ejecutar:
dotnet run
```

### 3. Frontend

```bash
cd src/Frontend

npm install
npm run dev
```

## 👤 Credenciales de Prueba

```
Usuario: admin
Contraseña: admin123
```

## 📚 Documentación de la API

Una vez iniciado el backend, visita:

- Swagger UI: http://localhost:5000/swagger

## 🧪 Datos de Prueba

El script `03_SampleData.sql` incluye:

- 5 miembros de prueba
- 3 entrenadores
- 4 clases con horarios
- 3 planes de membresía
- Pagos y asistencias de ejemplo

## ❓ Problemas Comunes

### Error de conexión a SQL Server

```bash
# Verificar que SQL Server esté ejecutándose
# Windows: Services -> SQL Server
# Docker: docker ps | grep sqlserver
```

### Puerto ocupado

```bash
# Cambiar puertos en docker-compose.yml o en las configuraciones
```

## 📞 Soporte

Para más información, consulta el README.md principal.
