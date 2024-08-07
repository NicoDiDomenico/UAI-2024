create database EjercicioVuelos;

USE EjercicioVuelos;

CREATE TABLE Localidad (
	cp INT PRIMARY KEY,
	localidad VARCHAR(50) NOT NULL
);

CREATE TABLE Propietario (
	cuit INT PRIMARY KEY,
	apellido VARCHAR(50) NOT NULL,
	nombre VARCHAR(50) NOT NULL,
	telefono INT NOT NULL,
	domicilio VARCHAR(50) NOT NULL,
	mail VARCHAR(50) NOT NULL,
	cp INT NOT NULL,
	CONSTRAINT fk_propietario_localidad
	FOREIGN KEY (cp) REFERENCES Localidad(cp)
);

CREATE TABLE Pasajero (
	dni INT PRIMARY KEY,
	apellido VARCHAR(50) NOT NULL,
	nombre VARCHAR(50) NOT NULL,
	telefono INT NOT NULL,
	domicilio VARCHAR(50) NOT NULL,
	mail VARCHAR(50) NOT NULL,
	cp INT NOT NULL,
	CONSTRAINT fk_pasajero_localidad
	FOREIGN KEY (cp) REFERENCES Localidad(cp)
);

CREATE TABLE Avion (
	nroAvion INT PRIMARY KEY,
	cantPasajeros INT NOT NULL,
	tipoAeronave VARCHAR(50) NOT NULL,
	añoFabricacion INT NOT NULL,
	cuit INT NOT NULL,
	CONSTRAINT fk_avion_propietario
	FOREIGN KEY (cuit) REFERENCES Propietario(cuit)	
);

CREATE TABLE Vuelo (
	nroVuelo INT PRIMARY KEY,
	origen VARCHAR(50) NOT NULL,
	destino VARCHAR(50) NOT NULL,
	km INT NOT NULL,
	fecha DATE NOT NULL,
	horaSalida TIME NOT NULL,
	horaLlegada TIME NOT NULL,
	nroAvion INT NOT NULL,
	CONSTRAINT fk_vuelo_avion
	FOREIGN KEY (nroAvion) REFERENCES Avion(nroAvion)
);

CREATE TABLE Pasajero_Vuelo (
	dni INT NOT NULL,
	nroVuelo INT NOT NULL,
	CONSTRAINT pk_pasajero_vuelo PRIMARY KEY (dni, nroVuelo),
	CONSTRAINT fk_pasajero_vuelo_pasajero
	FOREIGN KEY (dni) REFERENCES Pasajero(dni),
	CONSTRAINT fk_pasajero_vuelo_vuelo
	FOREIGN KEY (nroVuelo) REFERENCES Vuelo(nroVuelo)
)
