-- Parte Pr�ctica.
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
-- Alta Alumnos
INSERT INTO Alumnos (CodAlu, NyA, Direccion)
VALUES	(1, 'Marcos Dorato', 'Pres. Roca 1802'),
		(2, 'Bruno Javioli', 'Salta 880'),
		(3, 'Pepe Majul', 'Entre R�os 1057'),
		(4, 'Lucia Palma', 'Tucum�n 1500'),
		(5, 'Ana Walt', 'Leandro N. Alem 810'),
		(6, 'Sofia D�az', 'Uriburu 170'),
		(7, 'Angie Garz�n', 'Jos� Ingenieros 1379'),
		(8, 'Micaela Mart�nez', 'Suipacha 864'),
		(9, 'Miguel Moreno', 'Riccheri 340'),
		(10, 'Mariana Ronco', 'San Juan 2450');

-- Alta Materias
INSERT INTO Materias (CodMat, Nombre)
VALUES	(101, 'An�lisis Matem�tico'),
		(102, 'Programacion I'),
		(103, 'Sistemas y Organizaciones'),
		(104, 'Algoritmos y Estructuras de Datos'),
		(201, 'F�sica'),
		(202, 'Ingl�s I'),
		(203, 'Paradigmas de Programaci�n'),
		(204, 'Sistemas Operativos'),
		(301, 'Arquitectura de Computadoras'),
		(302, 'Programacion II'),
		(303, 'Dise�o de Sistemas'),
		(304, 'Gesti�n de Datos'),
		(401, 'Ingl�s II'),
		(402, 'Programacion III'),
		(403, 'Administraci�n de Recursos'),
		(404, 'Redes y Comunicaciones');

-- Alta Profesores
INSERT INTO Profesores (CodProf, NomProf, SitRevista, Carrera)
VALUES	(1, 'Marcos Sancho', 'Titular', 'Analista de Sistemas'),
		(2, 'Luis Reus', 'Asociado', 'Tecnolog�a Inform�tica'),
		(3, 'Esteban Dortmund', 'Interino', 'Licenciatura en Redes y Comunicaci�n de Datos'),
		(4, 'Pedro Fern�ndez', 'Interino', 'Ingenier�a Mec�nica'),
		(5, 'Lucio Torres', 'Titular', 'Licenciatura en Matem�tica'),
		(6, 'Juan P�rez', 'Titular', 'Ingenieria en Sistemas de Informacion'),
		(7, 'Claudia Gafete', 'Asociado', 'Traductorado en Ingl�s'),
		(8, 'Manuel Garc�a', 'Titular', 'Tecnolog�a Inform�tica'),
		(9, 'Roberto Sampodia', 'Titular', 'Licenciatura en Programacion'),
		(10, 'Elena L�pez', 'Titular', 'Administracion de Empresas');

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
-- ex�menes rendidos entre el 1-1-2023 y el 31-12-2023 (C�digo de Materia, Nombre de la
-- Materia, Promedio). Al final del reporte debe aparecer el promedio general de todos los
-- ex�menes.
SELECT 
    m.CodMat AS 'C�digo de Materia',
    m.Nombre AS 'Nombre de la Materia',
    AVG(n.Nota) AS 'Promedio por Materia',
	(SELECT AVG(n.Nota)
	FROM Notas n
	INNER JOIN Materias m 
	ON n.CodMat = m.CodMat
	WHERE n.Fecha BETWEEN '2023-01-01' AND '2023-12-31') AS 'Promedio General'
FROM Notas n
INNER JOIN Materias m 
ON n.CodMat = m.CodMat
WHERE n.Fecha BETWEEN '2023-01-01' AND '2023-12-31'
GROUP BY m.CodMat, m.Nombre

-- 2) Emita un reporte que muestre los alumnos cuyo promedio sea mayor que el promedio
-- general. (C�digo de Alumno, Nombre y Apellido, promedio)
SELECT 
    a.CodAlu AS 'C�digo de Alumno',
    a.NyA AS 'Nombre y Apellido',
    AVG(n.Nota) AS 'Promedio'
FROM Alumnos a
INNER JOIN Notas n
ON a.CodAlu = n.CodAlu
GROUP BY a.CodAlu, a.NyA
HAVING AVG(n.Nota) > (SELECT AVG(Nota) FROM Notas)

-- 3) Con la idea de recuperar alumnos emita un listado con los alumnos que no han rendido
-- ex�menes desde el a�o 2024 a la fecha. (C�digo de Alumno, Nombre y Apellido).
SELECT 
    CodAlu AS 'C�digo de Alumno',
    NyA AS 'Nombre y Apellido'
FROM Alumnos
WHERE CodAlu NOT IN	(SELECT DISTINCT CodAlu
					 FROM Notas
					 WHERE Fecha >= '2024-01-01');

-- 4)Emita el listado de los profesores con el curso que tiene a cargo.(C�digo de profesor,
-- Nombre y Apellido del profesor, C�digo de curso a cargo)
SELECT 
    p.CodProf AS 'C�digo de Profesor',
    p.NomProf AS 'Nombre y Apellido del Profesor',
    c.CodMat AS 'C�digo de Curso a Cargo'
FROM Profesores p
LEFT JOIN Cursos c ON p.CodProf = c.CodProf;

-- 5) Libre. Generar una consulta de inter�s al modelo.
-- Listado de profesores que est�n asignados a m�s de una materia
SELECT 
    p.CodProf AS 'C�digo de Profesor',
    p.NomProf AS 'Nombre y Apellido del Profesor',
    COUNT(c.CodMat) AS 'Cantidad de Materias'
FROM Profesores p
INNER JOIN Cursos c 
ON p.CodProf = c.CodProf
GROUP BY p.CodProf, p.NomProf
HAVING COUNT(c.CodMat) > 1;
