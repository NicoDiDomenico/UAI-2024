-- Parte Práctica.
-- Crear base de datos:
create database notas_alumnos;

-- Crear tablas:
use notas_alumnos;

CREATE TABLE Alumnos (
    CodAlu INT PRIMARY KEY,
    NyA VARCHAR(100) NOT NULL,
    Direccion VARCHAR(200) NOT NULL
);

CREATE TABLE Materias (
    CodMat INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Profesores (
    CodProf INT PRIMARY KEY,
    NomProf VARCHAR(100) NOT NULL,
    SitRevista VARCHAR(50) NOT NULL,
    Carrera VARCHAR(100) NOT NULL
);

CREATE TABLE Cursos (
    CodMat INT NOT NULL,
    CodProf INT NOT NULL,
    PRIMARY KEY (CodMat, CodProf),
    FOREIGN KEY (CodMat) REFERENCES Materias(CodMat),
    FOREIGN KEY (CodProf) REFERENCES Profesores(CodProf)
);

CREATE TABLE Notas (
    CodAlu INT NOT NULL,
    CodMat INT NOT NULL,
    Fecha DATE NOT NULL,
    Nota DECIMAL(4, 2) NOT NULL,
    PRIMARY KEY (CodAlu, CodMat, Fecha),
    FOREIGN KEY (CodAlu) REFERENCES Alumnos(CodAlu),
    FOREIGN KEY (CodMat) REFERENCES Materias(CodMat)
);

-- Cargar datos:
-- Alta Alumnos (MODIFICAR NOMBRES)
INSERT INTO Alumnos (CodAlu, NyA, Direccion)
VALUES	(1, 'Juan Pérez', 'Calle Falsa 123'),
		(2, 'María López', 'Avenida Siempreviva 456'),
		(3, 'Carlos Gómez', 'Calle del Sol 789'),
		(4, 'Lucía Fernández', 'Pasaje Olmos 345'),
		(5, 'Ana Torres', 'Av. Libertador 654'),
		(6, 'Miguel Díaz', 'Calle Mayor 123'),
		(7, 'Laura García', 'Calle de la Luna 567'),
		(8, 'Diego Martínez', 'Av. Principal 789'),
		(9, 'Sofía Moreno', 'Calle del Prado 234'),
		(10, 'Fernando Ramírez', 'Calle Serrano 101');

-- Alta Materias
INSERT INTO Materias (CodMat, Nombre)
VALUES	(101, 'Análisis Matemático'),
		(102, 'Programacion I'),
		(103, 'Sistemas y Organizaciones'),
		(104, 'Algoritmo y Estructuras de Datos'),
		(201, 'Física'),
		(202, 'Inglés I'),
		(203, 'Paradigmas de Programación'),
		(204, 'Sistemas Operativos'),
		(301, 'Arquitectura de Computadoras'),
		(302, 'Programacion II'),
		(303, 'Diseño de Sistemas'),
		(304, 'Gestión de Datos'),
		(401, 'Inglés II'),
		(402, 'Programacion III'),
		(403, 'Administración de Recursos'),
		(404, 'Redes y Comunicaciones');

-- Alta Profesores
INSERT INTO Profesores (CodProf, NomProf, SitRevista, Carrera)
VALUES	(1, 'Marcos Sancho', 'Titular', 'Analista de Sistemas'),
		(2, 'Luis Reus', 'Asociado', 'Tecnología Informática'),
		(3, 'Esteban Dortmund', 'Interino', 'Licenciatura en Redes y Comunicación de Datos'),
		(4, 'Pedro Fernández', 'Interino', 'Ingeniería Mecánica'),
		(5, 'Lucio Torres', 'Titular', 'Licenciatura en Matemática'),
		(6, 'Juan Pérez', 'Titular', 'Ingenieria en Sistemas de Informacion'),
		(7, 'Claudia Gómez', 'Asociado', 'Traductorado en Inglés'),
		(8, 'Manuel García', 'Titular', 'Tecnología Informática'),
		(9, 'Roberto Sampodia', 'Titular', 'Licenciatura en Programacion'),
		(10, 'Elena López', 'Titular', 'Administracion de Empresas');

-- Alta Cursos
INSERT INTO Cursos (CodMat, CodProf)
VALUES	(101, 5),
		(102, 9),
		(103, 10),
		(104, 1),
		(201, 4),
		(202, 7),
		(203, 9),
		(204, 2),
		(301, 8),
		(302, 9),
		(303, 6),
		(304, 1),
		(401, 7),
		(402, 9),
		(403, 10),
		(404, 3);

-- Alta Notas
INSERT INTO Notas (CodAlu, CodMat, Fecha, Nota)
VALUES	(1, 101, '2020-02-12', 5.55),
		(2, 102, '2020-10-25', 7.20),
		(3, 103, '2021-07-03', 6.40),
		(4, 104, '2020-04-18', 9.75),
		(5, 201, '2023-09-09', 7.50),
		(6, 202, '2022-11-30', 8.25),
		(7, 203, '2022-06-22', 4.00),
		(8, 401, '2023-01-15', 9.50),
		(9, 401, '2023-12-07', 8.50),
		(10, 302, '2021-03-29', 7.30),
		(1, 101, '2021-02-01', 7.10),
		(2, 404, '2020-08-14', 8.40),
		(3, 104, '2022-05-11', 7.75),
		(4, 402, '2023-10-06', 6.90),
		(5, 303, '2021-11-23', 8.60),
		(6, 204, '2024-03-12', 10.00),
		(7, 403, '2022-01-04', 8.90),
		(8, 301, '2020-07-28', 4.90),
		(9, 304, '2023-06-17', 8.10),
		(10, 403, '2021-09-02', 9.25);

-- 1)Emita un reporte que muestre los promedios por materia y el promedio general de los
-- exámenes rendidos entre el 1-1-2023 y el 31-12-2023 (Código de Materia, Nombre de la
-- Materia, Promedio). Al final del reporte debe aparecer el promedio general de todos los
-- exámenes.
SELECT 
    m.CodMat AS 'Código de Materia',
    m.Nombre AS 'Nombre de la Materia',
    AVG(n.Nota) AS 'Promedio por Materia',
	(SELECT AVG(n.Nota)
	FROM Notas n
	INNER JOIN Materias m 
	ON n.CodMat = m.CodMat
	WHERE n.Fecha BETWEEN '2023-01-01' AND '2023-12-31') AS 'Promedio General' -- Si me pedia para todos los examenes sin portar la fecha le sado el where
FROM Notas n
INNER JOIN Materias m 
ON n.CodMat = m.CodMat
WHERE n.Fecha BETWEEN '2023-01-01' AND '2023-12-31'
GROUP BY m.CodMat, m.Nombre

-- 2) Emita un reporte que muestre los alumnos cuyo promedio sea mayor que el promedio
-- general. (Código de Alumno, Nombre y Apellido, promedio)
SELECT 
    a.CodAlu AS 'Código de Alumno',
    a.NyA AS 'Nombre y Apellido',
    AVG(n.Nota) AS 'Promedio'
FROM Alumnos a
INNER JOIN Notas n
ON a.CodAlu = n.CodAlu
GROUP BY a.CodAlu, a.NyA
HAVING AVG(n.Nota) > (SELECT AVG(Nota) FROM Notas)

-- 3) Con la idea de recuperar alumnos emita un listado con los alumnos que no han rendido
-- exámenes desde el año 2024 a la fecha. (Código de Alumno, Nombre y Apellido).
SELECT 
    CodAlu AS 'Código de Alumno',
    NyA AS 'Nombre y Apellido'
FROM Alumnos
WHERE CodAlu NOT IN	(SELECT DISTINCT CodAlu
					 FROM Notas
					 WHERE Fecha >= '2024-01-01');

-- 4)Emita el listado de los profesores con el curso que tiene a cargo.(Código de profesor,
-- Nombre y Apellido del profesor, Código de curso a cargo)
SELECT 
    p.CodProf AS 'Código de Profesor',
    p.NomProf AS 'Nombre y Apellido del Profesor',
    c.CodMat AS 'Código de Curso a Cargo'
FROM Profesores p
LEFT JOIN Cursos c ON p.CodProf = c.CodProf;

-- 5) Listado de profesores que están asignados a más de una materia
SELECT 
    p.CodProf AS 'Código de Profesor',
    p.NomProf AS 'Nombre y Apellido del Profesor',
    COUNT(c.CodMat) AS 'Cantidad de Materias'
FROM Profesores p
INNER JOIN Cursos c 
ON p.CodProf = c.CodProf
GROUP BY p.CodProf, p.NomProf
HAVING COUNT(c.CodMat) > 1;

--Parte Teórica.
-- 1. Tipos de índices. Nombrar y explicar brevemente

-- **Índices Primarios:**
--   - **Índice Primario Denso:** Cada valor de clave de la tabla tiene su propia entrada en el índice, lo que facilita la búsqueda rápida de registros individuales.
--   - **Índice Primario Disperso:** Los valores de clave se agrupan en rangos y se asigna una entrada de índice a cada rango, lo que puede reducir la cantidad de entradas en el índice y mejorar el rendimiento en ciertos casos.
--   - **Índice Primario Multinivel:** Este tipo de índice tiene uno o varios niveles de índices dispersos, seguidos por un último nivel denso que apunta directamente a la base de datos. Puede mejorar la eficiencia al organizar los datos en múltiples niveles de estructuras de índices.

--. **Índices Secundarios:**
--   - Los índices secundarios se construyen sobre atributos que no son claves primarias. Se utilizan para mejorar la velocidad de las consultas que filtran, ordenan o agrupan datos según estos atributos.

-- 2. 2. Grafique un índice Primario Disperso
-- (Pegar)

-- 3. Explique porque se justifica la generación de un índice primario disperso en lugar de un índice primario denso.
-- Si bien un indice primario denso facilita la busqueda rapida de registros individuales en la tabla, un indice primario disperso reduce la cantidad de entradas en el indice logrando mejorar el rendimiento. Hay que tener en cuenta que un mal uso de los indices se traduce en una reduccion de rendimiento.