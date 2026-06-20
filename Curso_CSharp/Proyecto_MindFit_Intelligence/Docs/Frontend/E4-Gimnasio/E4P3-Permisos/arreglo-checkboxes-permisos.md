- No me gusta como se muestra la lista de permisos selecionables, me gustaria darles cierta agrupacion a los permisos, para eso quiero que uses el sigueinte endpoint:
  https://localhost:7199/api/Formulario (en FormularioController.cs)
  Cuya respuesta es:
  Code: 200
  Response body:
  [
  {
  "idFormulario": 1,
  "nombreFormulario": "FORMULARIO_TURNOS_SOCIOS",
  "permisos": [
  "AGREGAR_TURNO",
  "CANCELAR_TURNO",
  "VALIDAR_INGRESO",
  "CREAR_USUARIO_SOCIO",
  "EDITAR_USUARIO_SOCIO",
  "ELIMINAR_USUARIO_SOCIO",
  "ELIMINAR_USUARIO_SOCIO_DEFINITIVAMENTE",
  "CAMBIAR_CONTRASENA_SOCIO"
  ]
  },
  {
  "idFormulario": 2,
  "nombreFormulario": "FORMULARIO_GYM_USUARIOS",
  "permisos": [
  "CREAR_USUARIO_RESPONSABLE",
  "EDITAR_USUARIO_RESPONSABLE",
  "ELIMINAR_USUARIO_RESPONSABLE_DEFINITIVAMENTE",
  "CAMBIAR_CONTRASENA_RESPONSABLE"
  ]
  },
  {
  "idFormulario": 3,
  "nombreFormulario": "FORMULARIO_GYM_PERMISOS",
  "permisos": [
  "CREAR_GRUPO",
  "EDITAR_GRUPO",
  "ELIMINAR_GRUPO"
  ]
  },
  {
  "idFormulario": 4,
  "nombreFormulario": "FORMULARIO_EQUIPAMIENTOS",
  "permisos": [
  "CREAR_EQUIPAMIENTO",
  "EDITAR_EQUIPAMIENTO",
  "ELIMINAR_EQUIPAMIENTO"
  ]
  },
  {
  "idFormulario": 5,
  "nombreFormulario": "FORMULARIO_MAQUINAS",
  "permisos": [
  "CREAR_MAQUINA",
  "EDITAR_MAQUINA",
  "ELIMINAR_MAQUINA"
  ]
  },
  {
  "idFormulario": 6,
  "nombreFormulario": "FORMULARIO_EJERCICIOS",
  "permisos": [
  "CREAR_EJERCICIO",
  "EDITAR_EJERCICIO",
  "ELIMINAR_EJERCICIO"
  ]
  },
  {
  "idFormulario": 7,
  "nombreFormulario": "FORMULARIO_GYM_HORARIOS",
  "permisos": [
  "MODIFICAR_DIA_RH",
  "QUITAR_ENTRENADOR_DIA_RH"
  ]
  },
  {
  "idFormulario": 8,
  "nombreFormulario": "FORMULARIO_RUTINAS",
  "permisos": [
  "EDITAR_RUTINA",
  "VER_HISTORIAL_RUTINA",
  "ELIMINAR_RUTINA",
  "RECUPERAR_RUTINA"
  ]
  }
  ]

La idea seria que compares los nombres de los permisos que usas para cada checkbox con los permisos que trae el endpoint que te pasé, notar que estan ordenados por formulario entonces si coincide agrupar permisos por el nombre del formulario.
