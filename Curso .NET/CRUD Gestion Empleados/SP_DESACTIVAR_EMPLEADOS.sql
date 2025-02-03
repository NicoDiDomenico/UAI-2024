CREATE PROC SP_DESACTIVAR_EMPLEADOS
    @nIdEmpleado int
AS
UPDATE tb_empleados SET activo_empleado = 0
WHERE id_empleado = @nIdEmpleado
GO
