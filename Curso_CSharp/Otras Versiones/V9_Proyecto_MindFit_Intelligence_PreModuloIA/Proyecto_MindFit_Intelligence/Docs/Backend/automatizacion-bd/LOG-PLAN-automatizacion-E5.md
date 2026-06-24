# Log de implementacion - Etapa 5

Fecha: 2026-06-20

## Alcance realizado

Se implemento la Etapa 5 de `PLAN-automatizacion.md`.

`POST /api/Gyms/onboarding` ahora ejecuta sincronicamente el flujo completo:

1. registra `Gym`, `UsuarioMaster` y `PersonaResponsableMaster` en master;
2. conserva `Gym.Activo = false`;
3. crea la base tenant y aplica las migraciones;
4. ejecuta el seed estructural;
5. crea el primer administrador tenant;
6. cambia `Gym.Activo` a `true`;
7. devuelve el `idGym` solamente cuando todo termino correctamente.

No se modificaron el endpoint, el DTO ni la forma de la respuesta exitosa.

## Orden y limites transaccionales

La transaccion master de la Etapa 1 conserva solamente el registro inicial de:

- `Gym` inactivo;
- `UsuarioMaster`;
- `PersonaResponsableMaster`.

La transaccion se confirma antes de crear la base tenant. Luego se ejecutan las operaciones de las Etapas 2, 3 y 4.

El seed y el administrador mantienen su transaccion local tenant. No se agrego una transaccion distribuida entre ambas bases.

La activacion master es la ultima escritura del flujo.

## Activacion

Se agrego a `IGymRepository` y `GymRepository`:

```text
ActualizarActivoAsync(idGym, activo)
```

La operacion busca el gimnasio por `idGym`, actualiza `Activo` y persiste el cambio en `MindFitMasterContext`.

`GymPublicoService` solo la invoca con `Activo = true` despues de que hayan terminado correctamente:

- las migraciones;
- el seed;
- la creacion del administrador.

## Manejo de fallos

Si falla el aprovisionamiento o la activacion:

- el error se registra con `ILogger<GymPublicoService>`;
- el log identifica solamente el `idGym` y no registra el DTO ni la contrasena;
- el onboarding devuelve un error;
- el registro master permanece inactivo si la activacion no se completo;
- el gimnasio no aparece en `GET /api/Gyms/activos`;
- no se ejecuta ninguna eliminacion automatica de bases o datos.

Se mantiene el comportamiento previsto: una migracion fallida puede dejar una base parcial, y una activacion fallida puede dejar una base preparada asociada a un gimnasio todavia inactivo.

## Prueba integrada exitosa

Se inicio una instancia local temporal de la API y se invoco el endpoint real.

Gimnasio de validacion:

```text
Nombre: E5 Validation 20260620215210
IdGym: 7
Base: MindFit_E5Validation20260620215210_71012581
```

Resultado del onboarding:

```text
Duracion: 3,24 segundos
Respuesta con idGym: correcta
Aparicion en GET /api/Gyms/activos: correcta
```

Luego se invoco `POST /api/Auth/login` con el nuevo `X-Gym-Id`.

Resultado:

```text
Access token presente: true
Refresh token presente: true
Permisos devueltos: 30
Nombre personal: Admin
```

La inspeccion SQL de solo lectura confirmo:

```text
Master: IdGym 7, Activo = 1
Migraciones tenant: 18
Usuarios tenant: 1
Personas responsables tenant: 1
Relaciones UsuarioGrupo tenant: 1
```

## Prueba integrada de fallo

Se inicio otra instancia local temporal con credenciales tenant deliberadamente invalidas. Esto fuerza el fallo despues del registro master y antes de crear la base.

Gimnasio de validacion:

```text
Nombre: E5 Failure 20260620215339
IdGym: 8
```

Resultado:

```text
Estado HTTP: 500
Activo en master: 0
Aparece en GET /api/Gyms/activos: false
Base fisica creada: false
```

La prueba confirma que un fallo de aprovisionamiento no activa ni publica el gimnasio.

## Datos de validacion conservados

De acuerdo con las reglas del plan, no se eliminaron automaticamente los datos de prueba:

- `IdGym 7` permanece activo y su base tenant queda disponible para inspeccion;
- `IdGym 8` permanece inactivo y no tiene base fisica creada.

La limpieza, si se decide realizarla, debe ser una operacion manual y separada.

## Compilacion

Comando:

```text
dotnet build Backend/MindFit_Intelligence_Backend/MindFit_Intelligence_Backend.csproj --no-restore
```

Resultado del rebuild final:

```text
0 errores
3 advertencias de nulabilidad preexistentes en Repository/UsuarioRepository.cs
```

Las advertencias no fueron introducidas por esta etapa.

## Archivos modificados

- `Backend/MindFit_Intelligence_Backend/Services/GymPublicoService.cs`
- `Backend/MindFit_Intelligence_Backend/Repository/GymRepository.cs`
- `Backend/MindFit_Intelligence_Backend/Repository/Interfaces/IGymRepository.cs`

## Archivos creados

- `Docs/Backend/automatizacion-bd/LOG-PLAN-automatizacion-E5.md`

## Sin cambios

No fue necesario modificar:

- controladores;
- DTOs;
- modelos master o tenant;
- migraciones;
- middleware o resolver tenant;
- `Program.cs`;
- frontend.

## Pendiente para la Etapa 6

- actualizar unicamente el mensaje de exito del frontend;
- ejecutar `npm run build`;
- verificar desde la interfaz que el boton permanezca deshabilitado durante el request y que el usuario pueda pasar al login al finalizar.
