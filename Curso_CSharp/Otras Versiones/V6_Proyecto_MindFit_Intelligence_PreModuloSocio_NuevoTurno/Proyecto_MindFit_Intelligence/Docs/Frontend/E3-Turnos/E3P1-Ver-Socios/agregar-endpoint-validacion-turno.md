## Ajuste requerido - Procesamiento automático de turnos vencidos

Antes de cargar la información de turnos desde el backend, el frontend debe ejecutar:

`PATCH /api/Turno/procesar-turnos-vencidos`

Objetivo:

Permitir que el backend procese automáticamente los turnos cuya fecha ya venció, cambiando su estado de `EnCurso` a `Vencido`.

Consideraciones:

- El frontend no debe implementar esta lógica de negocio.
- El frontend solo debe invocar el endpoint y luego continuar con el flujo normal de carga de datos.
- La llamada debe ejecutarse internamente al ingresar a la pantalla correspondiente donde se visualizan turnos.
- Si el procesamiento es exitoso, continuar normalmente.
- Si falla, manejar el error utilizando la misma estrategia de manejo de errores utilizada en el resto de la aplicación.
