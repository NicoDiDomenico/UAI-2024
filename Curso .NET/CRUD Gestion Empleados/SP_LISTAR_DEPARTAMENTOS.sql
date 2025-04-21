
CREATE PROC SP_LISTAR_CARGOS
AS
SELECT id_cargo, nombre_cargo FROM tb_cargos
WHERE activo_cargo = 1;
GO
