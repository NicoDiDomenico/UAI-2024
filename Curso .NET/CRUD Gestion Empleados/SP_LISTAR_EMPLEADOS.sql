use bd_gestion_empleados;

CREATE PROC SP_LISTAR_EMPLEADOS
@cBusqueda varchar(100) = ''
AS
SELECT e.id_empleado AS [ID],
	   e.nombre_empleado AS [Nombre],
	   d.nombre_departamento AS [Departamento],
	   c.nombre_cargo AS [Cargo],
	   e.salario_empleado [Salario],
	   e.fecha_nacimiento_empleado AS [Fecha Nacimiento]
FROM tb_empleados e
INNER JOIN tb_departamentos d
ON e.id_departamento = d.id_departamento
INNER JOIN tb_cargos c
ON e.id_cargo = c.id_cargo
WHERE e.activo_empleado = 1 AND 
UPPER(e.nombre_empleado) +
UPPER(d.nombre_departamento) +
UPPER(c.nombre_cargo)
LIKE '%' + UPPER(@cBusqueda)+'%';

