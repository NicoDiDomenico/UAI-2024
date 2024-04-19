-- 1_ Crear base de datos eventos_apellido
create database eventos_DiDomenico;

-- 2_ Crear tablas, claves, relaciones, restricciones: DER
use eventos_DiDomenico;

CREATE TABLE ADM_Paises (
	id_pais INT PRIMARY KEY,
	nombre_pais NVARCHAR(100) NOT NULL
);

CREATE TABLE ADM_Ciudades (
	id_ciudad INT PRIMARY KEY,
	nombre_ciudad NVARCHAR(100) NOT NULL,
	id_pais INT NOT NULL,
	CONSTRAINT fk_ciudades_paises
	FOREIGN KEY (id_pais) REFERENCES ADM_Paises(id_pais)
);


CREATE TABLE FER_Predios (
	id_predio INT IDENTITY(1,1) PRIMARY KEY ,
	nombre_predio NVARCHAR(100) NOT NULL,
	id_ciudad INT NOT NULL,
	superficie NUMERIC(9) NOT NULL,
	CONSTRAINT fk_predios_ciudades
	FOREIGN KEY (id_ciudad) REFERENCES ADM_Ciudades(id_ciudad)
);

CREATE TABLE FER_Rubros (
	id_rubro INT PRIMARY KEY,
	rubro NVARCHAR(100) NOT NULL,
);

CREATE TABLE FER_Expos (
	id_feria INT PRIMARY KEY,
	nombre NVARCHAR(100) NOT NULL,
	id_rubro INT NOT NULL,
	fecha_apertura DATETIME NOT NULL,
	fecha_cierre DATETIME NOT NULL,
	id_predio INT NOT NULL,
	CONSTRAINT fk_expos_rubros
	FOREIGN KEY (id_rubro) REFERENCES FER_Rubros(id_rubro),
	CONSTRAINT fk_expos_predios
	FOREIGN KEY (id_predio) REFERENCES FER_Predios(id_predio)
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
INSERT INTO FER_Predios (nombre_predio, id_ciudad, superficie)
VALUES ( 'La Posta', 1, 1200);
INSERT INTO FER_Predios (nombre_predio, id_ciudad, superficie)
VALUES ( 'El Quincho', 1, 1000);
INSERT INTO FER_Predios (nombre_predio, id_ciudad, superficie)
VALUES ( 'Francia', 3, 4000);
INSERT INTO FER_Predios (nombre_predio, id_ciudad, superficie)
VALUES ( 'El Palomar', 4, 2500);
INSERT INTO FER_Predios (nombre_predio, id_ciudad, superficie)
VALUES ( 'La Noche', 1, 200);
INSERT INTO FER_Predios (nombre_predio, id_ciudad, superficie)
VALUES ( 'La Estrella', 2, 5000);
INSERT INTO FER_Predios (nombre_predio, id_ciudad, superficie)
VALUES ( 'El Establo', 6, 600);

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

-- 6_ Crear una vista (VW_Predios2) con nombre_predio –id_ciudad – superficie. Utilizar esta vista para hacer el insert de un nuevo predio. (Tener en cuenta que al no poder ingresar el id_predio este deberá ser auto numérico y así se incrementaracorrectamente)
CREATE VIEW VW_Predios2 AS
SELECT nombre_predio, id_ciudad, superficie
FROM FER_Predios; 

select *
from VW_Predios2

INSERT INTO VW_Predios2 (nombre_predio, id_ciudad, superficie)
VALUES ('La Vista', 1, 2000);

select *
from FER_Predios;

