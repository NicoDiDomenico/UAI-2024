use EjercicioVuelos;

/*1- Mostrar todos los propietarios de Aviones */
SELECT * 
FROM Propietario;

/*2- Mostrar todos los propietarios de Aviones ordenados por apellido */
SELECT * 
FROM Propietario p 
ORDER BY p.apellido ASC;

/*3- Mostrar todos los propietarios que tienen m�s de un Avi�n y mostrar cuantos*/
SELECT p.cuit, p.apellido, p.nombre, count(p.cuit) as cantidad_aviones 
FROM Propietario p 
INNER JOIN Avion a 
ON p.cuit = a.cuit 
GROUP BY p.cuit, p.apellido, p.nombre
HAVING count(p.cuit) > 1;

/*4- Mostrar la cantidad de pasajeros que volaron en nuestra empresa*/
SELECT count(DISTINCT dni) as cantidad_pasajeros_que_volaron
FROM Pasajero_Vuelo;

/*5- Mostrar los 3 pasajero que m�s veces volaron*/	
SELECT TOP 3 p.dni, p.nombre, p.apellido, COUNT(p.dni) cantidad_viajes 
FROM Pasajero_Vuelo pv 
INNER JOIN Pasajero p 
ON p.dni = pv.dni
GROUP BY p.dni, p.nombre, p.apellido		
ORDER BY cantidad_viajes  DESC;

/*6- Mostrar aquellos pasajeros que solo volaron una vez*/	
SELECT pv.dni, p.nombre, p.apellido  
FROM Pasajero_Vuelo pv 
INNER JOIN Pasajero p 
ON p.dni = pv.dni 
GROUP BY pv.dni, p.nombre, p.apellido 
HAVING COUNT(pv.dni) = 1;


/*7- Mostrar las localidades en las cual residen la mayor�a de nuestros pasajeros. (las 3 --principales) NO ESTOY SEGURO */	
SELECT TOP 3 l.localidad
FROM Localidad l
INNER JOIN Pasajero p
ON l.cp = p.cp
INNER JOIN Pasajero_Vuelo pv
ON pv.dni = pv.dni
GROUP BY l.localidad
ORDER BY COUNT(l.localidad) DESC;

/*8- Mostrar las 3 aeronaves m�s antiguas junto con los datos de su propietario*/	
SELECT TOP 3 a.nroAvion, a.a�oFabricacion, p.* 
FROM Avion a 
INNER JOIN Propietario p 
ON p.cuit = a.cuit
ORDER BY a.a�oFabricacion DESC;

/*9- El avi�n m�s antiguo, cu�ntos a�os tiene al d�a de hoy?*/	
SELECT TOP 1 a.nroAvion, a.a�oFabricacion , (YEAR(GETDATE()) - a.a�oFabricacion) Edad
FROM Avion a 
ORDER BY Edad DESC

/*10- Mostrar la cantidad total de pasajeros por vuelos.*/
SELECT pv.nroVuelo, COUNT(pv.dni) as cantidad_pasajeros
FROM Pasajero_vuelo pv 
GROUP BY pv.nroVuelo 
