CREATE PROC SP_GUARDAR_EMPLEADOS
    @cNombre varchar(100),
    @cDireccion varchar(150),
    @dFechaNacimiento date,
    @cTelefono varchar(80),
    @nSalario money,
    @nIdDepartamento int,
    @nIdCargo int
AS
INSERT INTO tb_empleados (
    nombre_empleado,
    direccion_empleado,
    fecha_nacimiento_empleado,
    telefono_empleado,
    salario_empleado,
    id_departamento,
    id_cargo,
    activo_empleado
)
VALUES (
    @cNombre,
    @cDireccion,
    @dFechaNacimiento,
    @cTelefono,
    @nSalario,
    @nIdDepartamento,
    @nIdCargo,
	1
);
GO