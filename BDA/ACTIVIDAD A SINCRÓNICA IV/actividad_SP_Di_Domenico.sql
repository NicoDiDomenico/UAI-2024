CREATE DATABASE BancoDB;

USE BancoDB;

-- Creación de Tablas
CREATE TABLE Tipos_Cuenta (
    ID_TIPO_CUENTA INT PRIMARY KEY,
    DESCRIP_TIPO_CUENTA VARCHAR(100)
);

CREATE TABLE Titulares (
    ID_TITULAR INT PRIMARY KEY,
    RAZON_SOCIAL_TITULAR VARCHAR(100)
);

CREATE TABLE Cuentas (
    NRO_CUENTA INT PRIMARY KEY,
    SALDO DECIMAL(10, 2),
    FECHA_APERTURA DATE,
    ID_TIPO_CUENTA INT,
    DESCRIP_CUENTA VARCHAR(100),
    ID_TITULAR INT,
    ESTADO BIT,
    FOREIGN KEY (ID_TIPO_CUENTA) REFERENCES Tipos_Cuenta(ID_TIPO_CUENTA),
    FOREIGN KEY (ID_TITULAR) REFERENCES Titulares(ID_TITULAR)
);

-- SP PARA LA CARGA DE DATOS PARA CADA ENTIDAD	
-- Carga de datos en Tipos_Cuenta
CREATE PROCEDURE sp_CargarTiposCuenta
    @ID_TIPO_CUENTA INT,
    @DESCRIP_TIPO_CUENTA VARCHAR(100)
AS
BEGIN
    INSERT INTO Tipos_Cuenta (ID_TIPO_CUENTA, DESCRIP_TIPO_CUENTA)
    VALUES (@ID_TIPO_CUENTA, @DESCRIP_TIPO_CUENTA);
END

-- Carga de datos en Titulares
CREATE PROCEDURE sp_CargarTitulares
    @ID_TITULAR INT,
    @RAZON_SOCIAL_TITULAR VARCHAR(100)
AS
BEGIN
    INSERT INTO Titulares (ID_TITULAR, RAZON_SOCIAL_TITULAR)
    VALUES (@ID_TITULAR, @RAZON_SOCIAL_TITULAR);
END

-- Carga de datos en Cuentas
CREATE PROCEDURE sp_CargarCuentas
    @NRO_CUENTA INT,
    @SALDO DECIMAL(10, 2),
    @FECHA_APERTURA DATE,
    @ID_TIPO_CUENTA INT,
    @DESCRIP_CUENTA VARCHAR(100),
    @ID_TITULAR INT,
    @ESTADO BIT
AS
BEGIN
    INSERT INTO Cuentas (NRO_CUENTA, SALDO, FECHA_APERTURA, ID_TIPO_CUENTA, DESCRIP_CUENTA, ID_TITULAR, ESTADO)
    VALUES (@NRO_CUENTA, @SALDO, @FECHA_APERTURA, @ID_TIPO_CUENTA, @DESCRIP_CUENTA, @ID_TITULAR, @ESTADO);
END

--  SP PARA MOSTRAR CUENTAS SEGUN SU ESTADO
CREATE PROCEDURE sp_MostrarCuentasPorEstado
    @ESTADO BIT
AS
BEGIN
    SELECT * FROM Cuentas
    WHERE ESTADO = @ESTADO;
END

-- SP PARA CAMBIAR EL ESTADO DE UNA CUENTA
CREATE PROCEDURE sp_CambiarEstadoCuenta
    @NRO_CUENTA INT,
    @NUEVO_ESTADO BIT
AS
BEGIN
    UPDATE Cuentas
    SET ESTADO = @NUEVO_ESTADO
    WHERE NRO_CUENTA = @NRO_CUENTA;
END

--  SP PARA GENERAR UN TXT SEGUN DETALLE:
--	NRO_CUENTA[0000 COMPLETAR IZQ]
--	DESCRIP_CUENTA[30 COMPLETAR CON ESPACIOS EN BLANCO A LA DERECHA]
--	RAZON_SOCIAL_TITULAR[30 COMPLETAR CON ESPACIOS EN BLANCO A LA DERECHA]
--	SALDO [0000000000 COMPLETAR IZQ] DE TODAS LAS CUENTAS PARA UN DETERMINADO RANGO DE FECHAS. 
CREATE PROCEDURE sp_GenerarTxtCuentas
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    DECLARE @cmd VARCHAR(1000);
	DECLARE @FilePath VARCHAR(255);

	-- Establecer la ruta fija para el archivo de salida
    SET @FilePath = 'C:\\Users\\Usuario\\Desktop\\UAI-2024\\BDA\\INFO_CUENTAS.txt';

    -- Eliminar datos anteriores en la tabla temporal si existe
    IF OBJECT_ID('tempdb..#CuentasTxt') IS NOT NULL
        DROP TABLE #CuentasTxt;

    -- Crear tabla temporal para almacenar los datos formateados
    CREATE TABLE #CuentasTxt (
        Nro_Cuenta CHAR(4),
        Descrip_Cuenta CHAR(30),
        Razon_Social_Titular CHAR(30),
        Saldo CHAR(10)
    );

    -- Insertar datos formateados en la tabla temporal
    INSERT INTO #CuentasTxt
    SELECT 
        RIGHT('0000' + LTRIM(RTRIM(CAST(NRO_CUENTA AS VARCHAR(4)))), 4) AS Nro_Cuenta,
        LEFT(LTRIM(RTRIM(DESCRIP_CUENTA)) + REPLICATE(' ', 30), 30) AS Descrip_Cuenta,
        LEFT(LTRIM(RTRIM(t.RAZON_SOCIAL_TITULAR)) + REPLICATE(' ', 30), 30) AS Razon_Social_Titular,
        RIGHT('0000000000' + LTRIM(RTRIM(CAST(CAST(SALDO AS INT) AS VARCHAR(10)))), 10) AS Saldo
    FROM 
        Cuentas c
    JOIN 
        Titulares t ON c.ID_TITULAR = t.ID_TITULAR
    WHERE 
        FECHA_APERTURA BETWEEN @FechaInicio AND @FechaFin;

    -- Generar el comando para exportar los datos a un archivo de texto
    SET @cmd = 'bcp "SELECT Nro_Cuenta + Descrip_Cuenta + Razon_Social_Titular + Saldo FROM #CuentasTxt" queryout "' + @FilePath + '" -c -t';

    -- Ejecutar el comando para exportar el archivo
    EXEC xp_cmdshell @cmd;
END;

EXEC sp_GenerarTxtCuentas '2024-01-01', '2024-12-31';
