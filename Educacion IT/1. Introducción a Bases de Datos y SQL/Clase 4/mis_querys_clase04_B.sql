select * from articulos;

CREATE TABLE Articulos (
    ID INT AUTO_INCREMENT PRIMARY KEY, -- Identificador único del artículo
    Nombre VARCHAR(100) NOT NULL, -- Nombre descriptivo del artículo
    Precio DECIMAL(10, 2) NOT NULL -- Precio del artículo con dos decimales
);

-- Clausula Where
-- Insertar registros de ejemplo en la tabla Articulos
INSERT INTO Articulos (Nombre, Precio) VALUES
('Yerba Mate 1kg', 2999.00), 
('Dulce de Leche 400g', 1790.00), 
('Alfajor de Chocolate', 1349.00), 
('Vino Malbec Reserva 750ml', 9690.00), 
('Fernet 750ml', 12500.00), 
('Galletitas Criollitas 170g', 750.00), 
('Asado de Tira (kg)', 12310.00), 
('Tapas para Empanadas 12 Unidades', 1565.00), 
('Queso Cremoso (kg)', 8450.00), 
('Facturas Docena', 10270.00);

-- Esto lo ejecute yo porque dupliqie el insert:
SET SQL_SAFE_UPDATES = 0;
DELETE a1 FROM Articulos a1
INNER JOIN Articulos a2 
WHERE a1.ID > a2.ID 
  AND a1.Nombre = a2.Nombre 
  AND a1.Precio = a2.Precio;
SET SQL_SAFE_UPDATES = 1;
    
select * from articulos;
-- Clausula Where

-- Operadores de comparacion
select * from articulos where Id = 3;
select * from articulos where Id > 3;
select * from articulos where Id < 3;
select * from articulos where Id >= 3;
select * from articulos where Id <= 3;
select * from articulos where Id <> 3;

-- Operadores Logicos
-- AND
-- Es un operador lógico que se utiliza para combinar dos o más condiciones en una consulta SQL. 
-- Devuelve verdadero solo si todas las condiciones son verdaderas.
-- tabla de verdad
-- a        b       resultado
-- true     true    true
-- true     false   false
-- false    true    false
-- false    false   false

select * from productos;
select * from productos where precio >= 100 and stock = 1;

-- OR
-- Es un operador lógico que se utiliza para combinar dos o más condiciones en una consulta SQL. 
-- Devuelve verdadero si al menos una de las condiciones es verdadera.
-- tabla de verdad
-- a        b       resultado
-- true     true    true
-- true     false   true
-- false    true    true
-- false    false   false

-- NOT
-- Es un operador lógico que se utiliza para invertir el valor de una condición en una consulta SQL. 
-- Devuelve verdadero si la condición es falsa y viceversa.
-- tabla de verdad
-- x        resultado
-- true     false
-- false    true

-- BETWEEN
-- Es un operador lógico que se utiliza para filtrar resultados dentro de un rango específico en una consulta SQL. 
-- Devuelve verdadero si el valor se encuentra dentro del rango, incluyendo los límites.
select * from articulos where precio between 2000 and 9000;
-- NOT BETWEEN
select * from articulos where precio not between 2000 and 9000;

-- IN 
-- Es un operador lógico que se utiliza para filtrar resultados que coinciden con un conjunto específico de valores en una consulta SQL. 
-- Devuelve verdadero si el valor se encuentra en la lista de valores especificada.
select * from articulos where Id in (3, 5, 10);
-- NOT IN 
select * from articulos where Id not in (3, 5, 10);