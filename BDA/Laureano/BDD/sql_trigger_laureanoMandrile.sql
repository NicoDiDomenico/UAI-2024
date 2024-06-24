CREATE DATABASE triggers_Mandrile;

USE triggers_Mandrile;

CREATE TABLE CLIENTES (
    id_cliente INT PRIMARY KEY,
    razon_social VARCHAR(255),
    acumulado DECIMAL(10,2)
);

CREATE TABLE PRODUCTOS (
    id_producto INT PRIMARY KEY,
    descripcion VARCHAR(255),
    stock INT,
    precio_venta DECIMAL(10,2)
);

CREATE TABLE VENTAS (
    id_venta INT PRIMARY KEY,
    fecha DATE,
    id_cliente INT,
    importe DECIMAL(10,2),
    FOREIGN KEY (id_cliente) REFERENCES CLIENTES(id_cliente)
);

CREATE TABLE DETALLE_VENTA (
    id_venta INT,
    id_producto INT,
    cantidad INT,
    precio_venta DECIMAL(10,2),
    precio_r DECIMAL(10,2),
    PRIMARY KEY (id_venta, id_producto),
    FOREIGN KEY (id_venta) REFERENCES VENTAS(id_venta),
    FOREIGN KEY (id_producto) REFERENCES PRODUCTOS(id_producto)
);

INSERT INTO CLIENTES (id_cliente, razon_social, acumulado)
VALUES 
(1, 'Cliente 1', 1000),
(2, 'Cliente 2', 2000),
(3, 'Cliente 3', 3000),
(4, 'Cliente 4', 4000);

INSERT INTO PRODUCTOS (id_producto, descripcion, stock, precio_venta)
VALUES 
(1, 'Producto 1', 100, 10.00),
(2, 'Producto 2', 200, 20.00),
(3, 'Producto 3', 300, 30.00),
(4, 'Producto 4', 400, 40.00);

CREATE TRIGGER tr_abrir_venta
ON VENTAS
INSTEAD OF INSERT
AS
BEGIN
	INSERT INTO VENTAS (id_venta, fecha, id_cliente, importe)
	select inserted.id_venta, inserted.fecha, inserted.id_cliente, 0
	FROM inserted;
END;

INSERT INTO VENTAS (id_venta, fecha, id_cliente, importe)
VALUES (1, '2023-06-01', 1, 123456);

INSERT INTO VENTAS (id_venta, fecha, id_cliente)
VALUES (2, '2023-06-02', 2);

INSERT INTO VENTAS (id_venta, fecha, id_cliente)
VALUES (3, '2023-06-03', 3);

select * from VENTAS

CREATE TRIGGER tr_detalle_venta
ON DETALLE_VENTA
AFTER INSERT
AS
BEGIN
	--Actualizar stock
	UPDATE PRODUCTOS
	SET stock = stock - i.cantidad
	FROM PRODUCTOS p
	inner join inserted i on p.id_producto = i.id_producto;

	--Actualizar importe
	UPDATE VENTAS
	SET importe =(
		select SUM(d.cantidad * d.precio_venta)
		from DETALLE_VENTA d
		where d.id_venta = inserted.id_venta
		)
	FROM VENTAS v
	inner join inserted on v.id_venta = inserted.id_venta;
END;

INSERT INTO DETALLE_VENTA (id_venta, id_producto, cantidad, precio_venta) VALUES (2, 1, 10, 10)
INSERT INTO DETALLE_VENTA (id_venta, id_producto, cantidad, precio_venta) VALUES (1, 2, 20, 20)
INSERT INTO DETALLE_VENTA (id_venta, id_producto, cantidad, precio_venta) VALUES (2, 3, 30, 30)
INSERT INTO DETALLE_VENTA (id_venta, id_producto, cantidad, precio_venta) VALUES (3, 4, 40, 40);

select * from VENTAS
select * from PRODUCTOS

CREATE TRIGGER tr_cliente_acumulado
ON VENTAS
AFTER UPDATE
AS
BEGIN
	UPDATE CLIENTES
	SET CLIENTES.acumulado = CLIENTES.acumulado + inserted.importe - deleted.importe
	FROM CLIENTES
    INNER JOIN inserted ON CLIENTES.id_cliente = inserted.id_cliente
	INNER JOIN deleted ON inserted.id_venta = deleted.id_venta;	
END;

