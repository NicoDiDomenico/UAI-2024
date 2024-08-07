-- Creacion de la base de datos
CREATE DATABASE parcial_Practica;

USE parcial_Practica;

-- Creacion de tablas
CREATE TABLE tipos_comprobantes (
    id_tipo_comprobante INT PRIMARY KEY,
    descrip_comprobante NVARCHAR(100)
);

CREATE TABLE tipos_documento (
    id_tipo_documento NVARCHAR(20) PRIMARY KEY,
    descrip_documento NVARCHAR(100)
);

CREATE TABLE clientes (
    id_cliente INT PRIMARY KEY,
    apellido_nombre NVARCHAR(100),
    id_tipo_documento NVARCHAR(20),
    nro_documento INT,
    FOREIGN KEY (id_tipo_documento) REFERENCES tipos_documento(id_tipo_documento)
);

CREATE TABLE comprobantes (
    nro_comprobante INT PRIMARY KEY,
    fecha DATE,
    id_tipo_comprobante INT,
    id_cliente INT,
    importe DECIMAL(10,2),
    FOREIGN KEY (id_tipo_comprobante) REFERENCES tipos_comprobantes(id_tipo_comprobante),
    FOREIGN KEY (id_cliente) REFERENCES clientes(id_cliente)
);

-- Carga de datos
INSERT INTO tipos_documento (id_tipo_documento, descrip_documento)
VALUES 
(0, 'Ningun Documento'),
(1, 'D.N.I.'),
(2, 'C.U.I.L.'),
(3, 'C.U.I.T.'),
(4, 'Cedula de Identificacion'),
(5, 'Pasaporte'),
(6, 'Libreta Civica'),
(7, 'Libreta de Enrolamiento');

INSERT INTO tipos_comprobantes (id_tipo_comprobante, descrip_comprobante)
VALUES 
(1, 'Factura A'),
(2, 'Nota Debito A'),
(3, 'Nota Credito A'),
(4, 'Recibos A'),
(5, 'Nota Venta Contado A'),
(6, 'Factura B'),
(7, 'Nota Debito B'),
(8, 'Nota Credito B'),
(9, 'Recibo B'),
(10, 'Nota Venta Contado B'),
(11, 'Facturas C'),
(12, 'Nota Debito C'),
(13, 'Nota Credito C'),
(15, 'Recibo C'),
(16, 'Nota Venta Contado C'),
(81, 'Tique Factura A'),
(82, 'Tique Factura B'),
(83, 'Tique'),
(111, 'Tique Factura C'),
(112, 'Tique Nota Credito A'),
(113, 'Tique Nota Credito B'),
(114, 'Tique Nota Credito C');

INSERT INTO clientes (id_cliente, apellido_nombre, id_tipo_documento, nro_documento)
VALUES 
(1, 'Perez, Juan', '1', 12345678),
(2, 'Gomez, Maria', '2', 23456789),
(3, 'Lopez, Pedro', '1', 34567890),
(4, 'Rodriguez, Marta', '3', 45678901),
(5, 'Diaz, Jose', '1', 56789012),
(6, 'Gonzalez, Laura', '2', 67890123),
(7, 'Martinez, Carlos', '1', 78901234),
(8, 'Fernandez, Ana', '3', 89012345),
(9, 'Torres, Jorge', '1', 90123456),
(10, 'Sanchez, Carmen', '2', 12345670);

INSERT INTO comprobantes (nro_comprobante, fecha, id_tipo_comprobante, id_cliente, importe)
VALUES 
(1, '2023-01-01', 1, 1, 100.00),
(2, '2023-02-02', 2, 2, 200.00),
(3, '2023-03-03', 3, 3, 300.00),
(4, '2023-04-04', 4, 4, 400.00),
(5, '2023-05-05', 5, 5, 500.00),
(6, '2023-06-06', 6, 6, 600.00),
(7, '2023-07-07', 7, 7, 700.00),
(8, '2023-08-08', 8, 8, 800.00),
(9, '2023-09-09', 9, 9, 900.00),
(10, '2023-10-10', 10, 10, 1000.00);

-- Genera la salida con el formato requerido para el .txt
CREATE PROCEDURE GenerateText
AS
BEGIN
    DECLARE @text VARCHAR(118);

    SELECT 
        @text = CONCAT(
            FORMAT(c.fecha, 'yyyyMMdd'),  -- Campo 1: fecha de comprobante
            RIGHT('000' + CAST(tc.id_tipo_comprobante AS NVARCHAR), 3),  -- Campo 2: tipo de comprobante
            RIGHT('00000000000000000000' + CAST(c.nro_comprobante AS NVARCHAR), 20),  -- Campo 3: numero de comprobante desde
            RIGHT('00000000000000000000' + CAST(c.nro_comprobante AS NVARCHAR), 20),  -- Campo 4: numero de comprobante hasta
            RIGHT('00' + td.id_tipo_documento, 2),  -- Campo 5: codigo de documento del comprador
            RIGHT('00000000000000000000' + CAST(cl.nro_documento AS NVARCHAR), 20),  -- Campo 6: numero de documento del comprador
            LEFT(cl.apellido_nombre + SPACE(30), 30),  -- Campo 7: apellido y nombre del comprador
            RIGHT('000000000000000' + REPLACE(FORMAT(c.importe, '0.00'), '.', ''), 15)  -- Campo 8: importe total de la operacion
        )
    FROM comprobantes c
    INNER JOIN clientes cl ON c.id_cliente = cl.id_cliente
    INNER JOIN tipos_comprobantes tc ON c.id_tipo_comprobante = tc.id_tipo_comprobante
    INNER JOIN tipos_documento td ON cl.id_tipo_documento = td.id_tipo_documento;
    
    SELECT @text;
END

-- Generacion archivo .txt
CREATE PROCEDURE GenerateTextFile @clienteId INT
AS
BEGIN
    DECLARE @text VARCHAR(118);
    DECLARE @cmd  VARCHAR(1000);

    SELECT 
        @text = CONCAT(
            FORMAT(c.fecha, 'yyyyMMdd'),  -- Campo 1: fecha de comprobante
            RIGHT('000' + CAST(tc.id_tipo_comprobante AS NVARCHAR), 3),  -- Campo 2: tipo de comprobante
            RIGHT('00000000000000000000' + CAST(c.nro_comprobante AS NVARCHAR), 20),  -- Campo 3: numero de comprobante desde
            RIGHT('00000000000000000000' + CAST(c.nro_comprobante AS NVARCHAR), 20),  -- Campo 4: numero de comprobante hasta
            RIGHT('00' + td.id_tipo_documento, 2),  -- Campo 5: codigo de documento del comprador
            RIGHT('00000000000000000000' + CAST(cl.nro_documento AS NVARCHAR), 20),  -- Campo 6: numero de documento del comprador
            LEFT(cl.apellido_nombre + SPACE(30), 30),  -- Campo 7: apellido y nombre del comprador
            RIGHT('000000000000000' + REPLACE(FORMAT(c.importe, '0.00'), '.', ''), 15)  -- Campo 8: importe total de la operacion
        )
    FROM comprobantes c
    INNER JOIN clientes cl ON c.id_cliente = cl.id_cliente AND cl.id_cliente = @clienteId
    INNER JOIN tipos_comprobantes tc ON c.id_tipo_comprobante = tc.id_tipo_comprobante
    INNER JOIN tipos_documento td ON cl.id_tipo_documento = td.id_tipo_documento;
    
    SET @cmd = 'echo ' + @text + ' > C:\Users\laure\OneDrive\Escritorio\output.txt';
    EXEC xp_cmdshell @cmd, no_output;
END

-- Creacion Vista 
CREATE VIEW ComprobanteView AS
SELECT 
    c.nro_comprobante,  -- Numero de comprobante
    c.fecha,  -- Fecha del comprobante
    tc.descrip_comprobante AS descripcion_comprobante,  -- Descripcion del tipo de comprobante
    cl.apellido_nombre AS nombre_y_apellido,  -- Nombre y apellido del cliente
    c.importe  -- Importe del comprobante
FROM 
    comprobantes c
INNER JOIN 
    clientes cl ON c.id_cliente = cl.id_cliente
INNER JOIN 
    tipos_comprobantes tc ON c.id_tipo_comprobante = tc.id_tipo_comprobante;


-- Creacion SP total facturado
CREATE PROCEDURE TotalFacturado
    @fechaInicio DATE,
    @fechaFin DATE
AS
BEGIN
    SELECT SUM(importe) AS TotalFacturado
    FROM comprobantes
    WHERE fecha BETWEEN @fechaInicio AND @fechaFin;
END

-- Creacion de Funcion
CREATE FUNCTION ComprobanteConImpuesto(@comprobanteID INT)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        c.*, 
        c.importe * 1.21 AS BaseImponible
    FROM 
        comprobantes AS c
    WHERE
        c.nro_comprobante = @comprobanteID
);

-- Creacion Trigger
-- Agregado del campo acumulado
ALTER TABLE clientes
ADD acumulado_compras DECIMAL(18, 2) NOT NULL DEFAULT 0;

CREATE TRIGGER Tr_ActualizarAcumuladoCompras
ON comprobantes
AFTER INSERT
AS
BEGIN
    UPDATE clientes
    SET acumulado_compras = ISNULL(acumulado_compras, 0) + i.importe
    FROM inserted i
    WHERE clientes.id_cliente = i.id_cliente;
END

select * from clientes;
