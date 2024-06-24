USE parcial_Practica

EXEC GenerateText;

EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
EXEC sp_configure 'xp_cmdshell', 1;
RECONFIGURE;

select * from clientes;

-- Llamada SP que genera archivo .txt
EXEC GenerateTextFile @clienteId = 2;

-- Llamada a VW
SELECT * FROM ComprobanteView;

-- Llamada SP total facturado
EXEC TotalFacturado @fechaInicio = '2023-01-01' , @fechaFin = '2023-08-08';

-- Llamada a funcion
SELECT *
FROM ComprobanteConImpuesto(1);

-- Activacion trigger
INSERT INTO comprobantes (nro_comprobante, fecha, id_tipo_comprobante, id_cliente, importe)
VALUES (11, '2023-11-01', 1, 1, 150.00);

INSERT INTO comprobantes (nro_comprobante, fecha, id_tipo_comprobante, id_cliente, importe)
VALUES (13, '2023-01-11', 2, 1, 120.00);


