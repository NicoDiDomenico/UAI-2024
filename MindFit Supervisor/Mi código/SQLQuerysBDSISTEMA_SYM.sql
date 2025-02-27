/* Proyecto Gym */
CREATE database DBSISTEMA_GYM

USE DBSISTEMA_GYM

GO

CREATE TABLE Rol (
    IdRol INT PRIMARY KEY IDENTITY,
    Descripcion VARCHAR(50),
    FechaRegistro DATETIME DEFAULT GETDATE()
);

INSERT INTO Rol (Descripcion) VALUES ('ADMINISTRADOR');
INSERT INTO Rol (Descripcion) VALUES ('ASISTENTE');
INSERT INTO Rol (Descripcion) VALUES ('ENTRENADOR');

SELECT * FROM Rol;
GO

CREATE TABLE Permiso (
    IdPermiso INT PRIMARY KEY IDENTITY,
    IdRol INT REFERENCES rol(idRol),
    NombreMenu VARCHAR(100),
    FechaRegistro DATETIME DEFAULT GETDATE()
);

INSERT INTO Permiso (idRol, nombreMenu) VALUES
(1, 'menuGestionarRutinas'),
(1, 'menuSocios'),
(1, 'menuGestionarGimnasio'),
(2, 'menuSocios'),
(3, 'menuGestionarRutinas');

SELECT * FROM Permiso;

SELECT r.IdRol, r.Descripcion, p.IdPermiso, p.NombreMenu  
FROM Permiso p
INNER JOIN Rol r ON p.IdRol = r.IdRol;
GO

CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY IDENTITY,
--  idGimnasio INT NOT NULL,  -- Clave foránea (suponiendo que hay una tabla Gimnasio)
    NombreYApellido VARCHAR(100) NOT NULL,
    Email VARCHAR(50) NOT NULL UNIQUE,
    Telefono VARCHAR(50),
    Direccion VARCHAR(50),
    Ciudad VARCHAR(50),
    NroDocumento INT UNIQUE NOT NULL,
    Genero VARCHAR(50),
    FechaNacimiento DATETIME,
--  tipoResponsable VARCHAR(50),
	NombreUsuario VARCHAR(50),
    Clave VARCHAR(50),
    IdRol INT REFERENCES rol(idRol),
    Estado BIT,
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

INSERT INTO Usuario 
    (nombreUsuario, nombreYApellido, email, telefono, direccion, ciudad, nroDocumento, genero, fechaNacimiento, clave, idRol, estado) 
VALUES
    ('adm', 'Administrador General', 'admin@example.com', '123456789', 'Av. Principal 123', 'Ciudad Ejemplo', 12345678, 'Masculino', '1980-05-15 08:30:00', 'admin123', 1, 1),
    ('asistente1', 'Juan Pérez', 'juan.perez@example.com', '987654321', 'Calle Secundaria 456', 'Ciudad Ejemplo', 87654321, 'Masculino', '1990-08-20 14:45:00', 'asistente2024', 2, 1),
    ('entrenador1', 'María López', 'maria.lopez@example.com', '555111222', 'Avenida Fitness 789', 'Ciudad Deportiva', 13579246, 'Femenino', '1985-12-10 19:15:00', 'trainerfit', 3, 1);

select * from Usuario

select u.IdUsuario, u.NombreUsuario, NombreYApellido, u.Email, Telefono, u.Direccion, u.Ciudad, u.NroDocumento, u.Genero, FechaNacimiento, u.Clave, r.IdRol, r.Descripcion, u.Estado
from Usuario u
inner join Rol r
on r.IdRol = u.IdRol

SELECT p.IdRol, p.NombreMenu 
FROM PERMISO p
INNER JOIN ROL r ON r.IdRol = p.IdRol
INNER JOIN USUARIO u ON u.IdRol = r.IdRol
WHERE u.IdUsuario = 2

update Usuario set Clave = 123

go

CREATE TABLE Gimnasio (
    IdGimnasio INT PRIMARY KEY IDENTITY,
	NombreGimnasio VARCHAR(50),
	Direccion VARCHAR(50),
	Telefono VARCHAR(50),
	Logo varbinary(max) NULL
);
GO

INSERT INTO Gimnasio 
    (NombreGimnasio, Direccion, Telefono, Logo) 
VALUES
    ('SariesGym', 'Av. Principal 123', '123456789', null)

select * from Gimnasio