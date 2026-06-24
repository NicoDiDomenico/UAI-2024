-- 1. Asegurarnos de que la tabla esté limpia para no duplicar datos (opcional, pero recomendado)
TRUNCATE TABLE [MindFitIntelligence].[dbo].[FormularioPermiso];
-- Si TRUNCATE te da error de Foreign Key, usá: DELETE FROM [MindFitIntelligence].[dbo].[FormularioPermiso];

-- 2. Insertar todas las relaciones de formularios y permisos
INSERT INTO [MindFitIntelligence].[dbo].[FormularioPermiso] (IdFormulario, IdPermiso)
VALUES
    -- IdFormulario 1: FORMULARIO_TURNOS_SOCIOS (Permisos del 1 al 8)
    (1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6), (1, 7), (1, 8),

    -- IdFormulario 2: FORMULARIO_GYM_USUARIOS (Permisos del 9 al 12)
    (2, 9), (2, 10), (2, 11), (2, 12),

    -- IdFormulario 3: FORMULARIO_GYM_PERMISOS (Permisos del 13 al 15)
    (3, 13), (3, 14), (3, 15),

    -- IdFormulario 4: FORMULARIO_EQUIPAMIENTOS (Permisos del 16 al 18)
    (4, 16), (4, 17), (4, 18),

    -- IdFormulario 5: FORMULARIO_MAQUINAS (Permisos del 19 al 21)
    (5, 19), (5, 20), (5, 21),

    -- IdFormulario 6: FORMULARIO_EJERCICIOS (Permisos del 22 al 24)
    (6, 22), (6, 23), (6, 24),

    -- IdFormulario 7: FORMULARIO_GYM_HORARIOS (Permisos del 25 al 26)
    (7, 25), (7, 26),

    -- IdFormulario 8: FORMULARIO_RUTINAS (Permisos del 27 al 30)
    (8, 27), (8, 28), (8, 29), (8, 30);