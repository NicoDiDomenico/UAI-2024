-- 1_ Crear base de datos eventos_apellido
create database eventos_apellido;

-- 2_ Crear tablas, claves, relaciones, restricciones: DER
use eventos_apellido;

CREATE TABLE ADM_Paises (
	id_pais INT PRIMARY KEY,
	nombre_pais VARCHAR(50) NOT NULL
);

CREATE TABLE ADM_Ciudades (
	id_ciudad INT PRIMARY KEY,
	nombre_ciudad VARCHAR(50) NOT NULL,
	id_pais INT NOT NULL,
	CONSTRAINT fk_ciudades_paises
	FOREIGN KEY (id_pais) REFERENCES ADM_Paises(id_pais)
);


CREATE TABLE FER_Predios (
	id_predio INT PRIMARY KEY,
	nombre_predio VARCHAR(50) NOT NULL,
	id_ciudad INT NOT NULL,
	superficie REAL NOT NULL,
	CONSTRAINT fk_predios_ciudades
	FOREIGN KEY (id_ciudad) REFERENCES ADM_Ciudades(id_ciudad)
);

CREATE TABLE FER_Rubros (
	id_rubro INT PRIMARY KEY,
	rubro VARCHAR(50) NOT NULL,
);

CREATE TABLE FER_Expos (
	id_feria INT PRIMARY KEY,
	nombre VARCHAR(50) NOT NULL,
	id_rubro INT NOT NULL,
	fecha_apertura DATE NOT NULL,
	fecha_cierre DATE NOT NULL,
	id_predio INT NOT NULL,
	CONSTRAINT fk_expos_rubros
	FOREIGN KEY (id_rubro) REFERENCES ADM_Rubros(id_rubro),
	CONSTRAINT fk_expos_predios
	FOREIGN KEY (id_predio) REFERENCES ADM_Predios(id_predio)
);

-- 3_ Carga de datos: Ver ANEXO
-- Alta Paises
INSERT INTO ADM_Paises (id_pais, nombre_pais)
VALUES (1, 'Argentina');
INSERT INTO ADM_Paises (id_pais, nombre_pais)
VALUES (2, 'Brasil');
INSERT INTO ADM_Paises (id_pais, nombre_pais)
VALUES (3, 'Uruguay');

-- Alta Ciudades
INSERT INTO ADM_Ciudades (id_ciudad, nombre_ciudad, id_pais)
VALUES (1, 'Rosario', 1);
INSERT INTO ADM_Ciudades (id_ciudad, nombre_ciudad, id_pais)
VALUES (2, 'Cordoba', 1);
INSERT INTO ADM_Ciudades (id_ciudad, nombre_ciudad, id_pais)
VALUES (3, 'Motevideo', 3);
INSERT INTO ADM_Ciudades (id_ciudad, nombre_ciudad, id_pais)
VALUES (4, 'San Pablo', 2);
INSERT INTO ADM_Ciudades (id_ciudad, nombre_ciudad, id_pais)
VALUES (5, 'Floria', 2);
INSERT INTO ADM_Ciudades (id_ciudad, nombre_ciudad, id_pais)
VALUES (6, 'Santa Fe', 1);

-- Alta Predios
INSERT INTO FER_Predios (id_predio, nombre_predio, id_ciudad, superficie)
VALUES (1, 'La Posta', 1, 1200);
INSERT INTO FER_Predios (id_predio, nombre_predio, id_ciudad, superficie)
VALUES (2, 'El Quincho', 1, 1000);
INSERT INTO FER_Predios (id_predio, nombre_predio, id_ciudad, superficie)
VALUES (3, 'Francia', 3, 4000);
INSERT INTO FER_Predios (id_predio, nombre_predio, id_ciudad, superficie)
VALUES (4, 'El Palomar', 4, 2500);
INSERT INTO FER_Predios (id_predio, nombre_predio, id_ciudad, superficie)
VALUES (5, 'La Noche', 1, 200);
INSERT INTO FER_Predios (id_predio, nombre_predio, id_ciudad, superficie)
VALUES (6, 'La Estrella', 2, 5000);
INSERT INTO FER_Predios (id_predio, nombre_predio, id_ciudad, superficie)
VALUES (7, 'El Establo', 6, 600);

-- Alta Rubros
INSERT INTO FER_Rubros (id_rubro, rubro)
VALUES (1, 'Promociones');
INSERT INTO FER_Rubros (id_rubro, rubro)
VALUES (2, 'Cumpleaños');
INSERT INTO FER_Rubros (id_rubro, rubro)
VALUES (3, 'Despedidas');
INSERT INTO FER_Rubros (id_rubro, rubro)
VALUES (4, 'Casamientos');

-- Alta Expos
INSERT INTO FER_Expos (id_feria, nombre, id_rubro, fecha_apertura, fecha_cierre, id_predio)
VALUES (1, 'casamiento Juan', 4, '2007-08-25', '2007-08-25', 1);
INSERT INTO FER_Expos (id_feria, nombre, id_rubro, fecha_apertura, fecha_cierre, id_predio)
VALUES (2, 'comidas ricas', 1, '2008-04-30', '2008-05-02', 4);
INSERT INTO FER_Expos (id_feria, nombre, id_rubro, fecha_apertura, fecha_cierre, id_predio)
VALUES (3, 'fin2004', 3, '2004-12-20', '2004-12-21', 3);
INSERT INTO FER_Expos (id_feria, nombre, id_rubro, fecha_apertura, fecha_cierre, id_predio)
VALUES (4, 'fin2005', 3, '2008-05-14', '2008-05-15', 1);
INSERT INTO FER_Expos (id_feria, nombre, id_rubro, fecha_apertura, fecha_cierre, id_predio)
VALUES (5, 'casamiento Ariel', 4, '2009-01-05', '2009-01-06', 2);
INSERT INTO FER_Expos (id_feria, nombre, id_rubro, fecha_apertura, fecha_cierre, id_predio)
VALUES (6, 'cumple15', 2, '2009-04-25', '2009-04-26', 2);

-- 4_ Crear una Vista (VW_Predios) que devuelva todos lospredios que comienzan con la letra L. Las columnas amostrar son las siguientes.nombre_predio – nombre_ciudad – nombre_pais
CREATE VIEW VW_Predios AS
SELECT p.nombre_predio, c.nombre_ciudad, pa.nombre_pais
FROM FER_Predios p
INNER JOIN ADM_Ciudades c
ON p.id_ciudad = c.id_ciudad
INNER JOIN ADM_Paises pa
ON pa.id_pais = c.id_pais
WHERE p.nombre_predio  LIKE 'L%';

-- 5_ Crear una Vista (VW_Feriashoy) que devuelva las todaslas ferias con: nombre_feria – rubro – fecha_apertura – fecha_cierre de Ferias/Exposiciones que tengan lugar entre 01/01/2024 y hoy.
CREATE VIEW VW_Feriashoy AS
SELECT e.nombre nombre_feria, r.rubro, e.fecha_apertura, e.fecha_cierre
FROM FER_Expos e
INNER JOIN FER_Rubros r
ON r.id_rubro = e.id_rubro
WHERE fecha_apertura >= '2024-01-01' AND fecha_cierre <= GETDATE();

-- 6_ Crear una vista (VW_Predios2) con nombre_predio –id_ciudad – superficie Utilizar esta vista para hacer el insert de un nuevo predio. (Tener en cuentaque al no poder ingresar el id_predio este deberáser auto numérico y así se incrementaracorrectamente)
-- LLEVAR A CONSULTA - NO ESTOY SEGURO 
-- Creando la vista sin id_predio
CREATE VIEW VW_Predios2 AS
SELECT nombre_predio, id_ciudad, superficie
FROM FER_Predios; 

-- Insertando un nuevo predio utilizando la vista:
INSERT INTO VW_Predios2 (nombre_predio, id_ciudad, superficie)
VALUES ('Predio 1', 1, 12000);

-- Para que esto tenga efecto la PK debe ser auto-incermental:
CREATE TABLE FER_Predios (
	id_predio INT PRIMARY KEY IDENTITY,
	nombre_predio VARCHAR(50) NOT NULL,
	id_ciudad INT NOT NULL,
	superficie REAL NOT NULL,
	CONSTRAINT fk_predios_ciudades
	FOREIGN KEY (id_ciudad) REFERENCES ADM_Ciudades(id_ciudad)
);