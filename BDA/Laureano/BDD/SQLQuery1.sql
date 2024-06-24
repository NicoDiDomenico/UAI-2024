CREATE DATABASE eventos_Mandrile2

CREATE TABLE ADM_Paises (
    id_pais int PRIMARY KEY,
    nombre_pais nvarchar(100)
)

CREATE TABLE ADM_Ciudades (
    id_ciudad int PRIMARY KEY,
    nombre_ciudad nvarchar(100),
    id_pais int,
    FOREIGN KEY (id_pais) REFERENCES ADM_Paises(id_pais)
)


CREATE TABLE FER_Predios (
    id_predio int PRIMARY KEY IDENTITY,
    nombre_predio nvarchar(100),
    id_ciudad int,
    superficie numeric(9),
    FOREIGN KEY (id_ciudad) REFERENCES ADM_Ciudades(id_ciudad)
)

CREATE TABLE FER_Rubros (
    id_rubro int PRIMARY KEY,
    rubro nvarchar(100)
)

CREATE TABLE FER_Expos (
    id_feria int PRIMARY KEY,
    nombre nvarchar(100),
    id_rubro int,
    fecha_apertura datetime,
    fecha_cierre datetime,
    id_predio int,
    FOREIGN KEY (id_rubro) REFERENCES FER_Rubros(id_rubro),
    FOREIGN KEY (id_predio) REFERENCES FER_Predios(id_predio)
)