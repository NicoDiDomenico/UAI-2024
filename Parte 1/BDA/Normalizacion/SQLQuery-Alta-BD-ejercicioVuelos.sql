-- Alta Localidad
INSERT INTO Localidad (cp, localidad)
VALUES (1000, 'Ciudad A');

INSERT INTO Localidad (cp, localidad)
VALUES (2000, 'Ciudad B');

INSERT INTO Localidad (cp, localidad)
VALUES (3000, 'Ciudad C');

INSERT INTO Localidad (cp, localidad)
VALUES (4000, 'Ciudad D');

INSERT INTO Localidad (cp, localidad)
VALUES (5000, 'Ciudad E');

-- Alta Propietario 
INSERT INTO Propietario (cuit, apellido, nombre, telefono, domicilio, mail, cp)
VALUES (123456789, 'González', 'Juan', 1122334455, 'Calle 123', 'juan@example.com', 1000);

INSERT INTO Propietario (cuit, apellido, nombre, telefono, domicilio, mail, cp)
VALUES (987654321, 'Martínez', 'María', 5544332211, 'Avenida Principal', 'maria@example.com', 2000);

INSERT INTO Propietario (cuit, apellido, nombre, telefono, domicilio, mail, cp)
VALUES (456789123, 'Rodríguez', 'Pedro', 7788990011, 'Calle Principal', 'pedro@example.com', 3000);

INSERT INTO Propietario (cuit, apellido, nombre, telefono, domicilio, mail, cp)
VALUES (789123456, 'López', 'Ana', 9900112233, 'Avenida 456', 'ana@example.com', 4000);

INSERT INTO Propietario (cuit, apellido, nombre, telefono, domicilio, mail, cp)
VALUES (321654987, 'Sánchez', 'Carlos', 3344556677, 'Avenida Central', 'carlos@example.com', 5000);

-- Alta Pasajero
INSERT INTO Pasajero (dni, apellido, nombre, telefono, domicilio, mail, cp)
VALUES (123456789, 'González', 'Juan', 1122334455, 'Calle 123', 'juan@example.com', 1000);

INSERT INTO Pasajero (dni, apellido, nombre, telefono, domicilio, mail, cp)
VALUES (987654321, 'Martínez', 'María', 5544332211, 'Avenida Principal', 'maria@example.com', 2000);

INSERT INTO Pasajero (dni, apellido, nombre, telefono, domicilio, mail, cp)
VALUES (456789123, 'Rodríguez', 'Pedro', 7788990011, 'Calle Principal', 'pedro@example.com', 3000);

INSERT INTO Pasajero (dni, apellido, nombre, telefono, domicilio, mail, cp)
VALUES (789123456, 'López', 'Ana', 9900112233, 'Avenida 456', 'ana@example.com', 4000);

INSERT INTO Pasajero (dni, apellido, nombre, telefono, domicilio, mail, cp)
VALUES (321654987, 'Sánchez', 'Carlos', 3344556677, 'Avenida Central', 'carlos@example.com', 5000);

-- Alta Avion
INSERT INTO Avion (nroAvion, cantPasajeros, tipoAeronave, añoFabricacion, cuit)
VALUES (1, 150, 'Boeing 737', 2010, 123456789);

INSERT INTO Avion (nroAvion, cantPasajeros, tipoAeronave, añoFabricacion, cuit)
VALUES (2, 200, 'Airbus A320', 2015, 987654321);

INSERT INTO Avion (nroAvion, cantPasajeros, tipoAeronave, añoFabricacion, cuit)
VALUES (3, 180, 'Boeing 787', 2018, 456789123);

INSERT INTO Avion (nroAvion, cantPasajeros, tipoAeronave, añoFabricacion, cuit)
VALUES (4, 170, 'Airbus A380', 2012, 789123456);

INSERT INTO Avion (nroAvion, cantPasajeros, tipoAeronave, añoFabricacion, cuit)
VALUES (5, 220, 'Boeing 777', 2016, 321654987);

-- Alta Vuelo
INSERT INTO Vuelo (nroVuelo, origen, destino, km, fecha, horaSalida, horaLlegada, nroAvion)
VALUES (1, 'Ciudad A', 'Ciudad B', 1000, '2024-04-17', '08:00:00', '10:30:00', 1);

INSERT INTO Vuelo (nroVuelo, origen, destino, km, fecha, horaSalida, horaLlegada, nroAvion)
VALUES (2, 'Ciudad B', 'Ciudad C', 1200, '2024-04-18', '09:00:00', '12:00:00', 2);

INSERT INTO Vuelo (nroVuelo, origen, destino, km, fecha, horaSalida, horaLlegada, nroAvion)
VALUES (3, 'Ciudad C', 'Ciudad D', 800, '2024-04-19', '10:00:00', '11:30:00', 3);

INSERT INTO Vuelo (nroVuelo, origen, destino, km, fecha, horaSalida, horaLlegada, nroAvion)
VALUES (4, 'Ciudad D', 'Ciudad E', 600, '2024-04-20', '11:00:00', '12:15:00', 4);

INSERT INTO Vuelo (nroVuelo, origen, destino, km, fecha, horaSalida, horaLlegada, nroAvion)
VALUES (5, 'Ciudad E', 'Ciudad A', 1100, '2024-04-21', '12:00:00', '14:30:00', 5);

-- Alta Pasajero_Vuelo
INSERT INTO Pasajero_Vuelo (dni, nroVuelo)
VALUES (123456789, 1);

INSERT INTO Pasajero_Vuelo (dni, nroVuelo)
VALUES (987654321, 2);

INSERT INTO Pasajero_Vuelo (dni, nroVuelo)
VALUES (456789123, 3);

INSERT INTO Pasajero_Vuelo (dni, nroVuelo)
VALUES (789123456, 4);

INSERT INTO Pasajero_Vuelo (dni, nroVuelo)
VALUES (321654987, 5);



