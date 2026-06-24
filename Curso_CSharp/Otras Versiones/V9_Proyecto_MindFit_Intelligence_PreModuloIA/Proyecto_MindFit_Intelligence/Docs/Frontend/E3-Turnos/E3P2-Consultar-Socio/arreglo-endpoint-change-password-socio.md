# Arreglo Endpoint Cambio de Contrasena de Socio

## Objetivo

Actualizar el frontend para que el cambio de contrasena desde Consultar Socio use el endpoint del socio seleccionado:

```http
POST /api/Auth/socio/{idUsuario}/change-password
```

El endpoint anterior `POST /api/Auth/socio/change-password` tomaba el usuario desde el JWT y terminaba cambiando la contrasena del usuario autenticado.

## Cambios Implementados

- `Frontend/src/services/sociosService.ts`
  - `changeSocioPassword` ahora recibe `idUsuario` y `ChangePasswordRequestDto`.
  - La URL se construye como `/Auth/socio/${idUsuario}/change-password`.
  - El body mantiene solo `currentPassword` y `newPassword`.

- `Frontend/src/components/socios/ConsultarSocioModal.tsx`
  - `handlePasswordChange` pasa el `idUsuario` del modal al servicio.
  - Se mantienen las validaciones, loading, success/error y limpieza del formulario.

## Contrato Frontend/Backend

Backend esperado:

```csharp
[HttpPost("socio/{idUsuario:int}/change-password")]
public async Task<IActionResult> ChangePasswordSocioSeleccionado(
    int idUsuario,
    ChangePasswordRequestDto dto)
```

Request body:

```json
{
  "currentPassword": "string",
  "newPassword": "string"
}
```

No se envia `idUsuario` en el body. La autenticacion, autorizacion y `X-Gym-Id` siguen centralizados en `apiClient`.

## Validacion Esperada

- En la pestaña Seguridad de Consultar Socio, confirmar cambio de contrasena.
- Verificar en Network que la request use:

```http
POST /Auth/socio/{idUsuario}/change-password
```

- Confirmar que `{idUsuario}` corresponde al socio seleccionado.
- Confirmar que el body contiene solo `currentPassword` y `newPassword`.
