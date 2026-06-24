-- =====================================================================
-- 1. LIMPIEZA DE TABLAS
-- Se eliminan primero los registros de las tablas hijas para evitar 
-- errores de Foreign Key, y por último la tabla padre.
-- =====================================================================
DELETE FROM GrupoPermiso;
DELETE FROM FormularioPermiso;
DELETE FROM Permiso;

-- =====================================================================
-- 2. REINICIO DE IDENTITY
-- Reiniciamos el contador para que el próximo registro insertado sea 1.
-- =====================================================================
DBCC CHECKIDENT ('Permiso', RESEED, 0);

-- =====================================================================
-- 3. INSERCIÓN DE PERMISOS (ORDEN C#)
-- Los IDs se asignarán automáticamente del 1 al 30.
-- =====================================================================
INSERT INTO Permiso (Codigo, Descripcion) VALUES
('AGREGAR_TURNO', 'Permite la creación o reserva de nuevos turnos'),                               -- Nuevo ID: 1 (Antes: 9)
('CANCELAR_TURNO', 'Permite cancelar turnos previamente agendados'),                                 -- Nuevo ID: 2 (Antes: 10)
('VALIDAR_INGRESO', 'Permite validar el acceso o entrada al establecimiento'),                       -- Nuevo ID: 3 (Antes: 12)
('CREAR_USUARIO_SOCIO', 'Permite la creación de nuevos usuarios tipo Socio (cliente)'),              -- Nuevo ID: 4 (Antes: 1)
('EDITAR_USUARIO_SOCIO', 'Permite modificar la información de usuarios tipo Socio'),                 -- Nuevo ID: 5 (Antes: 2)
('ELIMINAR_USUARIO_SOCIO', 'Permite dar de baja usuarios tipo Socio'),                               -- Nuevo ID: 6 (Antes: 3)
('ELIMINAR_USUARIO_SOCIO_DEFINITIVAMENTE', 'Permite eliminar definitivamente del sistema a un Socio'),-- Nuevo ID: 7 (Antes: 26)
('CAMBIAR_CONTRASENA_SOCIO', 'Permite cambiar la contraseña de usuarios tipo Socio'),                -- Nuevo ID: 8 (Antes: 4)

('CREAR_USUARIO_RESPONSABLE', 'Permite la creación de nuevos usuarios tipo Responsable (staff)'),    -- Nuevo ID: 9 (Antes: 27)
('EDITAR_USUARIO_RESPONSABLE', 'Permite modificar la información de usuarios tipo Responsable'),     -- Nuevo ID: 10 (Antes: 28)
('ELIMINAR_USUARIO_RESPONSABLE_DEFINITIVAMENTE', 'Permite eliminar definitivamente a un Responsable'),-- Nuevo ID: 11 (Antes: 29)
('CAMBIAR_CONTRASENA_RESPONSABLE', 'Permite cambiar la contraseña de usuarios tipo Responsable'),    -- Nuevo ID: 12 (Antes: 30)

('CREAR_GRUPO', 'Permite la creación de nuevos grupos de permisos'),                                 -- Nuevo ID: 13 (Antes: 5)
('EDITAR_GRUPO', 'Permite modificar los nombres o alcances de los grupos'),                          -- Nuevo ID: 14 (Antes: 6)
('ELIMINAR_GRUPO', 'Permite eliminar grupos de permisos del sistema'),                               -- Nuevo ID: 15 (Antes: 7)

('CREAR_EQUIPAMIENTO', 'Permite registrar nuevo equipamiento'),                                      -- Nuevo ID: 16 (Antes: 16)
('EDITAR_EQUIPAMIENTO', 'Permite modificar información de equipamiento'),                            -- Nuevo ID: 17 (Antes: 17)
('ELIMINAR_EQUIPAMIENTO', 'Permite eliminar registros de equipamiento'),                             -- Nuevo ID: 18 (Antes: 18)

('CREAR_MAQUINA', 'Permite registrar nuevas máquinas en el sistema'),                                -- Nuevo ID: 19 (Antes: 13)
('EDITAR_MAQUINA', 'Permite modificar información de máquinas existentes'),                          -- Nuevo ID: 20 (Antes: 14)
('ELIMINAR_MAQUINA', 'Permite dar de baja máquinas del sistema'),                                    -- Nuevo ID: 21 (Antes: 15)

('CREAR_EJERCICIO', 'Permite crear nuevos ejercicios en el catálogo'),                               -- Nuevo ID: 22 (Antes: 19)
('EDITAR_EJERCICIO', 'Permite modificar detalles de ejercicios existentes'),                         -- Nuevo ID: 23 (Antes: 20)
('ELIMINAR_EJERCICIO', 'Permite eliminar ejercicios del catálogo'),                                  -- Nuevo ID: 24 (Antes: 21)

('MODIFICAR_DIA_RH', 'Permite modificar días y rangos horarios'),                                    -- Nuevo ID: 25 (Antes: 11)
('QUITAR_ENTRENADOR_DIA_RH', 'Permite quitar a un entrenador asignado a un día y rango horario'),    -- Nuevo ID: 26 (Antes: 31)

('EDITAR_RUTINA', 'Permite modificar las rutinas asignadas'),                                        -- Nuevo ID: 27 (Antes: 22)
('VER_HISTORIAL_RUTINA', 'Permite visualizar el historial de rutinas del socio'),                    -- Nuevo ID: 28 (Antes: 23)
('ELIMINAR_RUTINA', 'Permite dar de baja o eliminar rutinas'),                                       -- Nuevo ID: 29 (Antes: 24)
('RECUPERAR_RUTINA', 'Permite restaurar rutinas eliminadas anteriormente');                          -- Nuevo ID: 30 (Antes: 25)

-- =====================================================================
-- 4. INSERCIÓN DE GRUPOS (CON IDs MAPEADOS)
-- =====================================================================
INSERT INTO GrupoPermiso (IdGrupo, IdPermiso) VALUES
-- Módulo Gestión de Turnos (Nuevos IDs: 1, 2, 3)
(1, 1), 
(1, 2), 
(1, 3), 

-- Módulo Usuario Socio (Nuevos IDs: 4, 5, 6, 8)
(1, 4), 
(1, 5), 
(1, 6), 
(1, 8), 

-- Módulo Permisos / Grupos (Nuevos IDs: 13, 14, 15)
(1, 13), 
(1, 14), 
(1, 15), 

-- Equipamiento (Se mantienen: 16, 17, 18)
(1, 16), 
(1, 17), 
(1, 18), 

-- Máquinas (Nuevos IDs: 19, 20, 21)
(1, 19), 
(1, 20), 
(1, 21), 

-- Ejercicios (Nuevos IDs: 22, 23, 24)
(1, 22), 
(1, 23), 
(1, 24), 

-- Rangos Horarios (Nuevo ID: 25)
(1, 25), 

-- Rutinas (Nuevos IDs: 27, 28, 29, 30)
(1, 27), (4, 27),
(1, 28), (4, 28),
(1, 29), (4, 29),
(1, 30), (4, 30);

-- =====================================================================
-- 5. INSERCIÓN DE FORMULARIOS (CON IDs MAPEADOS)
-- =====================================================================
INSERT INTO FormularioPermiso (IdFormulario, IdPermiso) VALUES
-- Formulario 1 (Máquinas - Nuevos IDs: 19, 20, 21)
(1, 19), 
(1, 20), 
(1, 21), 

-- Formulario 2 (Equipamiento - Se mantienen: 16, 17, 18)
(2, 16), 
(2, 17), 
(2, 18), 

-- Formulario 3 (Ejercicios - Nuevos IDs: 22, 23, 24)
(3, 22), 
(3, 23), 
(3, 24);