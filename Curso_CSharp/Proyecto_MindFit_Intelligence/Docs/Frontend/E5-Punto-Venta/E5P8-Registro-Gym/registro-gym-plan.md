Necesito implementar en el frontend el formulario público de **Registro de Gimnasio / Plan Adquisición Gym**.

Endpoint a consumir:

`POST /api/Gyms/onboarding`

Este endpoint registra un nuevo gimnasio y su usuario master/responsable.

Trabajar solo dentro de `/Frontend`.
No modificar `/Backend`.

## Archivos a revisar antes

- `AGENTS.md`
- `frontend-skill.md`
- `Frontend/src/routes/AppRouter.tsx`
- `Frontend/src/services/apiClient.ts`
- servicios existentes dentro de `Frontend/src/services`
- tipos existentes dentro de `Frontend/src/types`
- `Frontend/src/App.css`
- página landing actual si ya existe: `Frontend/src/pages/LandingPage.tsx`

## Objetivo

Crear un formulario público para registrar un nuevo gimnasio.

El formulario debe poder abrirse desde la landing pública al hacer clic en los botones:

`Solicitar Demo`
`¡Lo quiero!`

Al enviar el formulario en un boton llamado `Registrar`, se debe llamar a:

`POST /api/Gyms/onboarding`

## Ruta sugerida

Crear una ruta pública:

`/registro-gym`

Esta ruta debe estar fuera de `ProtectedRoute`.

No debe requerir autenticación.

## DTO esperado

Crear los tipos TypeScript necesarios, por ejemplo en:

`Frontend/src/types/gymOnboarding.ts`

El request debe respetar esta estructura:

```ts
export interface GymOnboardingRequest {
  nombreGym: string;
  usuarioMaster: {
    username: string;
    password: string;
    personaResponsable: {
      nombre: string;
      apellido: string;
      email: string;
      telefono: string;
      direccion: string;
      ciudad: string;
      tipoDocumento: string;
      nroDocumento: string;
      genero: string;
      fechaNacimiento: string;
    };
  };
}
```

## Servicio

Crear un servicio centralizado, por ejemplo:

`Frontend/src/services/gymsService.ts`

Con una función:

```ts
registrarGymOnboarding(payload: GymOnboardingRequest)
```

Debe llamar a:

```ts
apiClient.post("/Gyms/onboarding", payload);
```

El `apiClient` ya incorpora el prefijo `/api` mediante su `baseURL`, por lo que no debe
repetirse en la ruta relativa del servicio. No hardcodear URLs completas.

## Importante sobre headers

Este endpoint es público para registrar un nuevo gimnasio.

No enviar manualmente `X-Gym-Id` desde el formulario.

Si el `apiClient` ya agrega `X-Gym-Id` automáticamente desde localStorage, no modificar toda la lógica global salvo que sea necesario. En ese caso, resolverlo de forma puntual y segura para este endpoint público, evitando romper login, forgot-password, reset-password o endpoints autenticados.

## Formulario

Crear una página nueva, por ejemplo:

`Frontend/src/pages/GymOnboardingPage.tsx`

Campos del formulario:

### Datos del gimnasio

- Nombre del gimnasio

### Datos de acceso del usuario master

- Username
- Password
- Confirmar password

### Datos personales del responsable

- Nombre
- Apellido
- Email
- Teléfono
- Dirección
- Ciudad
- Tipo de documento
- Número de documento
- Género
- Fecha de nacimiento

## Validaciones frontend

Validar antes de enviar:

- Nombre del gimnasio requerido.
- Username requerido.
- Password requerida.
- Confirmar password requerida.
- Password y Confirmar password deben coincidir.
- Nombre requerido.
- Apellido requerido.
- Email requerido.
- Email con formato válido.
- Teléfono requerido.
- Dirección requerida.
- Ciudad requerida.
- Tipo de documento requerido.
- Número de documento requerido.
- Género requerido.
- Fecha de nacimiento requerida.

No enviar el request si hay errores de validación.

## Comportamiento

Al hacer submit:

1. Mostrar estado de loading.
2. Deshabilitar el botón mientras se envía.
3. Enviar el request a `POST /api/Gyms/onboarding`.
4. Si la respuesta es exitosa:
   - mostrar mensaje de éxito claro
   - informar que el gimnasio fue registrado y queda pendiente de activación
   - limpiar el formulario
   - mostrar un botón `Ir al login`, aclarando que el acceso estará disponible después de la activación

5. Si falla:
   - mostrar el mensaje real del backend si existe
   - si no existe, mostrar un mensaje genérico claro

Usar el helper existente de errores si el proyecto ya tiene algo como `getApiErrorMessage`.

## Navegación desde las páginas públicas

En la landing pública, el botón:

`Solicitar Demo`

Y en la página de precios, el botón:

`¡Lo quiero!`

Deben navegar internamente a:

`/registro-gym`

Usar `Link` o `useNavigate`.

No usar `window.location.href`.

El botón `Acceso Clientes` debe seguir navegando a:

`/login`

El botón `Registrar` pertenece exclusivamente al formulario de `/registro-gym` y debe
ejecutar el envío del formulario; no es un botón adicional de la landing.

## Estilos

Agregar estilos en:

`Frontend/src/App.css`

Encapsular los estilos bajo una clase contenedora, por ejemplo:

`.gym-onboarding-page`

para evitar colisiones con otras pantallas.

El diseño debe mantener la estética de MindFit:

- moderno
- limpio
- confiable
- orientado a salud
- responsive
- consistente con la landing pública

## Rutas

Modificar:

`Frontend/src/routes/AppRouter.tsx`

Agregar la ruta pública:

`/registro-gym`

Debe quedar fuera de `ProtectedRoute`.

No romper estas rutas existentes:

- `/`
- `/login`
- `/forgot-password`
- `/reset-password`
- `/dashboard`
- `/socio/inicio`
- `/socios`
- `/gimnasio`

## Validación final

Ejecutar desde `/Frontend`:

```bash
npm run build
```

Si falla por errores dentro del alcance de este cambio, corregirlos.

## Log

Crear o actualizar:

`IMPLEMENTATION_LOG_registro-gym-plan.md`

El log debe incluir:

- archivos creados o modificados
- endpoint integrado
- DTO implementado
- validaciones agregadas
- manejo de loading/error/success
- cambios de rutas
- resultado de `npm run build`
- TODOs o limitaciones detectadas
