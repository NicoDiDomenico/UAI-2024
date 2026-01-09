# Cómo levantar el proyecto (backend + frontend)

Resumen rápido:
- Backend en http://localhost:3001
- Frontend (Vite) en http://localhost:5173

Prerrequisitos:
- Tener instalado Node.js (recomiendo v18+). 
- Tener `npm` disponible.

Instalación (una sola vez):

```bash
# Desde la raíz del repo
cd backend
npm install

cd ../frontend
npm install
```

Arrancar en desarrollo:

Backend (modo desarrollo):

```bash
cd backend
npm run dev
```

Notas backend:
- El script `dev` ya está configurado para ejecutar TypeScript con `ts-node` y ESM.
- Si usas Prisma y necesitas generar cliente o aplicar migraciones:

```bash
cd backend
npx prisma generate        # genera el cliente de Prisma
# Opcional: aplicar migraciones (si corres una DB local configurada)
npx prisma migrate dev
```

Frontend (modo desarrollo):

```bash
cd frontend
npm run dev
```

Verificar que ambos funcionan:

```bash
# Health check backend
curl http://localhost:3001/health

# Abrir frontend (Vite) en el navegador
# Por defecto: http://localhost:5173
```

Variables de entorno (opcional):
- Crea `backend/.env` para configurar `DATABASE_URL`, `PORT`, etc.
- Ejemplo mínimo:

```env
PORT=3001
DATABASE_URL="postgresql://user:pass@localhost:5432/dbname"
```

Problemas comunes:
- Error "Unknown file extension .ts": asegúrate de tener Node >= 18 y haber corrido `npm install`. El `dev` script usa `node --loader ts-node/esm` para permitir ESM+TypeScript.
- Puertos ocupados: cambia `PORT` en `backend/.env` o define `PORT` al ejecutar.
- Si falta `prisma` o `@prisma/client`, corre `npm install` en `backend`.

¿Quieres que ejecute un chequeo rápido del endpoint `/health` ahora? 
