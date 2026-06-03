# Implementation Plan - Etapa 1 Auth Paso 1 Login

## Resumen

Crear `Docs/Frontend/Etapa-1-Auth/Paso-1-Login/implementation-plan.md` como documento técnico para implementar el login en `/Frontend`, sin modificar backend.

La implementación futura reemplazará el starter de Vite por una arquitectura base de autenticación con React, TypeScript, React Router, Axios y `react-select`, consumiendo los endpoints reales:

- `GET /api/Gyms/activos`
- `POST /api/Auth/login`

El backend queda como fuente de verdad y no se modifica.

## Arquitectura Propuesta

Usar una arquitectura frontend simple y extensible por módulos:

```txt
Frontend/src/
├─ components/
│  └─ auth/
├─ contexts/
├─ hooks/
├─ layouts/
├─ pages/
│  └─ auth/
├─ routes/
├─ services/
├─ types/
└─ utils/
```

Responsabilidades:

- `pages`: pantallas completas como login, dashboard placeholder y recuperación placeholder.
- `components/auth`: componentes específicos de login, incluido el select searchable de gimnasios.
- `services`: cliente Axios centralizado y servicios para Auth/Gyms.
- `contexts`: estado global mínimo de sesión.
- `routes`: definición de rutas y protección básica.
- `utils`: helpers puros para `localStorage` y normalización de errores.
- `types`: contratos TypeScript alineados con DTOs backend.

Dirección UI:

- Visual thesis: login moderno, calmo, limpio y confiable, orientado a operación de gimnasio.
- Content plan: selección de gimnasio, credenciales, acción principal, recuperación placeholder.
- Interaction thesis: estados de carga claros, transición suave entre submit/error/success y feedback visible sin ruido visual.

## Archivos a Crear o Modificar

Crear:

- `src/services/apiClient.ts`
- `src/services/authService.ts`
- `src/services/gymsService.ts`
- `src/types/auth.ts`
- `src/types/gym.ts`
- `src/utils/authStorage.ts`
- `src/utils/apiError.ts`
- `src/contexts/AuthContext.tsx`
- `src/hooks/useAuth.ts`
- `src/routes/AppRouter.tsx` (centralizar todas las rutas de aplicación)
- `src/routes/ProtectedRoute.tsx`
- `src/pages/auth/LoginPage.tsx`
- `src/pages/auth/ForgotPasswordPage.tsx`
- `src/pages/DashboardPage.tsx` (placeholder temporal)
- `src/components/auth/GymSelect.tsx`
- `.env.example`

Modificar:

- `src/App.tsx` para delegar en el router.
- `src/main.tsx` para envolver con `AuthProvider`.
- `src/index.css` y/o `src/App.css` para reemplazar estilos starter.
- `vite.config.ts` para proxy recomendado.
- `package.json` para dependencias necesarias.

## Tipos e Interfaces Frontend

Definir tipos alineados con JSON camelCase de ASP.NET:

```ts
export interface GymPublico {
  idGym: number;
  nombreGym: string;
}

export interface LoginRequest {
  username: string;
  password: string;
}

export interface TokenResponse {
  accessToken: string;
  refreshToken: string;
  permisos: string[];
}

export interface AuthSession extends TokenResponse {
  idGym: number;
}
```

No agregar campos no documentados. Los permisos se guardan como `string[]` exactamente como llegan.

## Estrategia de Autenticación

`AuthContext` expondrá:

- `session`
- `isAuthenticated`
- `login({ idGym, username, password })`
- `logout()`

Flujo:

1. Login carga gimnasios activos al montar.
2. Usuario selecciona `idGym`, completa username/password.
3. `authService.login` envía `POST /Auth/login`.
4. El body contiene solo `username` y `password`.
5. El header del request incluye `X-Gym-Id: {idGym}`.
6. Si responde `200`, se persiste sesión y se redirige a `/dashboard`.
7. Si falla, se muestra mensaje genérico.

No implementar refresh automático de token en este paso.

## Manejo de localStorage

Usar helper centralizado `authStorage.ts` con claves namespaced:

```txt
mindfit.idGym
mindfit.accessToken
mindfit.refreshToken
mindfit.permisos
```

Reglas:

- Guardar `idGym` como string para header y convertir a number al hidratar sesión.
- Guardar `permisos` con `JSON.stringify`.
- Leer sesión completa al iniciar app.
- Si faltan `idGym`, `accessToken` o `refreshToken`, considerar sesión inválida.
- `logout()` elimina las cuatro claves.
- No guardar username ni password.

## Configuración Axios

Crear un único `apiClient` con:

- `baseURL` desde `import.meta.env.VITE_API_BASE_URL`.
- Default recomendado local: `/api`.
- Proxy Vite local hacia `https://localhost:7199` para evitar CORS sin tocar backend.
- Request interceptor para agregar:
  - `Authorization: Bearer {accessToken}` si existe.
  - `X-Gym-Id: {idGym}` si existe.
- En login, pasar `X-Gym-Id` explícitamente porque todavía no hay sesión persistida.

Servicios:

- `gymsService.getActiveGyms(): Promise<GymPublico[]>`
- `authService.login(request: LoginRequest, idGym: number): Promise<TokenResponse>`

## Rutas Protegidas

Rutas iniciales:

```txt
/login
/forgot-password
/dashboard
*
```

Comportamiento:

- `/login`: pantalla pública.
- `/forgot-password`: página pública con texto `Próximamente...`; el link desde login abre en nueva pestaña.
- `/dashboard`: ruta protegida con placeholder temporal.
- `*`: redirige a `/dashboard` si hay sesión o `/login` si no hay sesión.

`ProtectedRoute` valida `isAuthenticated`. Si no hay sesión, redirige a `/login` con `replace`.

En esta etapa no se implementa autorización visual por permisos.

## Librerías Sugeridas

Agregar:

- `axios`: cliente HTTP centralizado, interceptores para token y `X-Gym-Id`.
- `react-router-dom`: rutas públicas/protegidas y navegación a `/dashboard`.
- `react-select`: select searchable estable, tipado y simple para gimnasios.

No agregar:

- Framework UI pesado: innecesario para esta etapa.
- Framer Motion: no hace falta para el login inicial.
- Librería de formularios: validaciones simples se resuelven con estado local.
- Librería de permisos: solo se persisten permisos, no se evalúan todavía.
- No agregar React Query/TanStack Query todavía: el alcance actual no lo necesita.

## Decisiones Técnicas Importantes

- Trabajar únicamente en `/Frontend`.
- Mantener backend intacto.
- Usar `/api` como base local recomendada con proxy Vite a `https://localhost:7199`.
- Mantener `VITE_API_BASE_URL` configurable para otros entornos.
- Centralizar HTTP y storage desde el primer paso para evitar duplicación futura.
- Mostrar errores genéricos de login para `401`, `4xx`, `500` y red.
- Mostrar error específico solo para carga de gimnasios.
- Deshabilitar submit mientras hay request.
- Deshabilitar select si no hay gimnasios disponibles.
- Preservar permisos exactamente como los retorna backend.

## Plan de Pruebas

Verificar manualmente:

- La app compila con `npm run build`.
- `/login` carga sin errores.
- `GET /api/Gyms/activos` llena el select con `nombreGym`.
- El select guarda internamente `idGym`.
- Submit sin gimnasio, username o password muestra validación frontend.
- Login envía body `{ username, password }` y header `X-Gym-Id`.
- Login exitoso guarda sesión en `localStorage` y redirige a `/dashboard`.
- `/dashboard` sin sesión redirige a `/login`.
- `/login` con sesión activa redirige a `/dashboard`.
- Link “Olvidé mi contraseña” abre `/forgot-password` en nueva pestaña.
- `npm run lint` no reporta errores evitables.

## Supuestos

- ASP.NET serializa DTOs en camelCase, según ejemplos y comportamiento default.
- `https://localhost:7199` es el backend local.
- La etapa no requiere refresh token automático.
- La etapa no requiere dashboard real.
- La etapa no requiere autorización visual avanzada por permisos.
- No se agregan endpoints fuera de los documentados para este paso.
