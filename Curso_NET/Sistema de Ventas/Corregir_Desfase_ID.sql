
/*Corregir desfase en ID:*/
DELETE FROM USUARIO WHERE IdUsuario = 6;

DBCC CHECKIDENT ('USUARIO', RESEED, 3);

SELECT * FROM USUARIO;

INSERT INTO USUARIO (Documento, NombreCompleto, Correo, Clave, IdRol, Estado)
VALUES ('999999', 'Test User', 'testuser@email.com', 'password', 1, 1);
