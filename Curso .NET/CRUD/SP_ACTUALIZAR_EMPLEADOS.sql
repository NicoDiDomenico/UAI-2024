CREATE PROC SP_ACTUALIZAR_EMPLEADOS
    @nIdEmpleado int,
    @cNombre varchar(100),
    @cDireccion varchar(150),
    @dFechaNacimiento date,
    @cTelefono varchar(80),
    @nSalario money,
    @nIdDepartamento int,
    @nIdCargo int
AS
UPDATE tb_empleados SET 
    nombre_empleado = @cNombre,
    direccion_empleado = @cDireccion,
    fecha_nacimiento_empleado = @dFechaNacimiento,
    telefono_empleado = @cTelefono,
    salario_empleado = @nSalario,
    id_departamento = @nIdDepartamento,
    id_cargo = @nIdCargo
WHERE id_empleado = @nIdEmpleado
GO
