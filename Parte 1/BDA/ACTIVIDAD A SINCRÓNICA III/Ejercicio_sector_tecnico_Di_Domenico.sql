-- 8) Generar un scrip que se pueda ejecutar en forma completa y este genere la BD, tablas, relaciones, carga de datos. Sin provocar error.
create database sector_tecnico;

use sector_tecnico;

CREATE TABLE Localidades (
    cp INT PRIMARY KEY,
    localidad NVARCHAR(60) NOT NULL
);

CREATE TABLE Tecnicos(
    clave_tec INT PRIMARY KEY,
    nombre NVARCHAR(80) NOT NULL,
	contador INT,
	acumulador MONEY
);

CREATE TABLE Clientes (
    id_cliente INT PRIMARY KEY,
    nombre NVARCHAR(80) NOT NULL,
	direccion_c NVARCHAR(80) NOT NULL,
	direccion_n INT NOT NULL,
	cp INT NOT NULL,
    FOREIGN KEY (cp) REFERENCES Localidades(cp)
);

CREATE TABLE Equipos (
    id_equipo INT PRIMARY KEY,
    descripcion NVARCHAR(80) NOT NULL,
	id_cliente INT NOT NULL,
	importe MONEY NOT NULL,
    FOREIGN KEY (id_cliente) REFERENCES Clientes(id_cliente)
);

CREATE TABLE Reparaciones(
    id_trabajo INT PRIMARY KEY,
    clave_tec INT NOT NULL,
	id_equipo INT NOT NULL,
	importe MONEY NOT NULL,
	fecha_ini DATETIME NOT NULL,
	fecha_fin DATETIME NOT NULL,
    FOREIGN KEY (clave_tec) REFERENCES Tecnicos(clave_tec),
	FOREIGN KEY (id_equipo) REFERENCES Equipos(id_equipo)
);

-- 1) Cargar registros en las tablas mediante insert. (al menos 2 en cada tabla)
INSERT INTO Localidades (cp, localidad) VALUES
(28001, 'Madrid'),
(08001, 'Barcelona');

INSERT INTO Tecnicos (clave_tec, nombre, contador, acumulador) VALUES
(1, 'Juan Pérez', 10, 1500.00),
(2, 'María López', 15, 2000.00);

INSERT INTO Clientes (id_cliente, nombre, direccion_c, direccion_n, cp) VALUES
(1, 'Carlos García', 'Calle Mayor', 10, 28001),
(2, 'Ana Martínez', 'Gran Vía', 5, 08001);

INSERT INTO Equipos (id_equipo, descripcion, id_cliente, importe) VALUES
(1, 'Ordenador portátil', 1, 750.00),
(2, 'Impresora láser', 2, 200.00);

INSERT INTO Reparaciones (id_trabajo, clave_tec, id_equipo, importe, fecha_ini, fecha_fin) VALUES
(1, 1, 1, 100.00, '2024-01-01 09:00:00', '2024-01-02 17:00:00'),
(2, 2, 2, 50.00, '2024-01-03 10:00:00', '2024-01-04 12:00:00');

-- 2) Cargar 6 reparaciones, respetando la integridad
INSERT INTO Reparaciones (id_trabajo, clave_tec, id_equipo, importe, fecha_ini, fecha_fin) VALUES
(3, 1, 1, 120.00, '2024-01-05 09:00:00', '2024-01-06 17:00:00'),
(4, 1, 2, 80.00, '2024-01-07 09:00:00', '2024-01-07 12:00:00'),
(5, 2, 1, 150.00, '2024-01-08 10:00:00', '2024-01-09 14:00:00'),
(6, 2, 2, 60.00, '2024-01-10 11:00:00', '2024-01-10 13:00:00'),
(7, 1, 1, 90.00, '2024-01-11 09:00:00', '2024-01-12 15:00:00'),
(8, 2, 2, 70.00, '2024-01-13 14:00:00', '2024-01-14 16:00:00');

-- 3) Mostrar los clientes con su localidad
SELECT c.nombre AS Nombre_Cliente, l.localidad AS Localidad
FROM Clientes c
INNER JOIN Localidades l
ON c.cp = l.cp;

-- 4) Cuantos clientes hay por localidad
SELECT l.localidad, COUNT(c.id_cliente) AS numero_de_clientes
FROM Clientes c
INNER JOIN Localidades l
ON c.cp = l.cp
GROUP BY l.localidad;

-- 5) Cuantos equipos tiene cada cliente
SELECT c.id_cliente, c.nombre AS nombre_cliente, COUNT(e.id_equipo) AS numero_de_equipos
FROM Clientes c
INNER JOIN Equipos e
ON c.id_cliente = e.id_cliente
GROUP BY c.id_cliente, c.nombre;

-- 6) Mostrar una reparación completa.(todos los datos asociados a la misma)
SELECT 
    r.id_trabajo,
    r.importe AS importe_reparacion,
    r.fecha_ini,
    r.fecha_fin,
    t.clave_tec,
    t.nombre AS nombre_tecnico,
    e.id_equipo,
    e.descripcion AS descripcion_equipo,
    e.importe AS importe_equipo,
    c.id_cliente,
    c.nombre AS nombre_cliente,
    c.direccion_c,
    c.direccion_n,
    l.localidad
FROM Reparaciones r
INNER JOIN Tecnicos t
ON r.clave_tec = t.clave_tec
INNER JOIN Equipos e
ON r.id_equipo = e.id_equipo
INNER JOIN Clientes c
ON e.id_cliente = c.id_cliente
INNER JOIN Localidades l
ON c.cp = l.cp
WHERE r.id_trabajo = 1; -- Muestro solo una reparación

-- 7) Con el punto 6 generar una vista.
CREATE VIEW VistaReparacionCompleta AS
SELECT 
    r.id_trabajo,
    r.importe AS importe_reparacion,
    r.fecha_ini,
    r.fecha_fin,
    t.clave_tec,
    t.nombre AS nombre_tecnico,
    e.id_equipo,
    e.descripcion AS descripcion_equipo,
    e.importe AS importe_equipo,
    c.id_cliente,
    c.nombre AS nombre_cliente,
    c.direccion_c,
    c.direccion_n,
    l.localidad
FROM Reparaciones r
INNER JOIN Tecnicos t
ON r.clave_tec = t.clave_tec
INNER JOIN Equipos e
ON r.id_equipo = e.id_equipo
INNER JOIN Clientes c
ON e.id_cliente = c.id_cliente
INNER JOIN Localidades l
ON c.cp = l.cp;
-- le saco el where asi la reparacion que quiero se hace al llamar la vista

SELECT * FROM VistaReparacionCompleta WHERE id_trabajo = 1; 

--9) Generar un índice no agrupado en la tabla clientes, por su campo nombre
EXECUTE sp_helpindex 'Clientes';

CREATE NONCLUSTERED INDEX indice_Clientes_nombre
ON Clientes (nombre);

EXECUTE sp_helpindex 'Clientes';

-- 10) Mostrar todas las reparaciones donde en la descripción del equipo contenga ‘i3’. Generar 
-- un índice sobre la tabla reparaciones para optimizar la consulta.

-- Consulta:
SELECT r.*, e.descripcion
FROM Reparaciones r
INNER JOIN Equipos e
ON r.id_equipo = e.id_equipo
WHERE e.descripcion LIKE '%i3%';

-- Indice:
EXECUTE sp_helpindex 'Reparaciones';

CREATE NONCLUSTERED INDEX indice_Reparaciones_id_equipo
ON Reparaciones (id_equipo);

EXECUTE sp_helpindex 'Reparaciones';