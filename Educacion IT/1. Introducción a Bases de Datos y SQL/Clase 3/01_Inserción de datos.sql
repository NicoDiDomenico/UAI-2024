-- Mostrar las Bases de Datos del servidor
show databases;

use comercioit;

-- Mostrar las tablas de la base de datos activa
show tables;

select * from productos;

desc productos;
describe productos;

# Manera Completa
-- Insertamos un registo sin asiganar el precio, por lo queda en Null el precio
INSERT INTO Productos (Nombre, Marca, Categoria, Stock, Disponible) VALUES
('Laptop HP 17', 'HP', 'Portátiles', 2, true);

INSERT INTO Productos ( Marca, Nombre, Categoria, Precio, Stock, Disponible) VALUES
					('Teclado', 'Laptop R9', 'Portátiles', 1200, 1, true);

INSERT INTO Productos (Nombre, Marca, Categoria, Stock) VALUES
					  ('Laptop Apple', 'Apple', 'Portátiles', 10);


# Manera SQL
INSERT INTO Productos 
SET Nombre = 'Antivirus', Marca = 'AVG', Categoria = 'Utilidades', Stock = 10, Disponible = true;

# Manera Simplificada
INSERT INTO Productos 
VALUES ( null, 'Mouse', 20, 'LG', 'Dispositivos', 0, false );
