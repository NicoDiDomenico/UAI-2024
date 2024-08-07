/* Creacion de Indices
Hay 2 tipos de indices: Agrupados y No agrupados
*/

-- Crear tabla sin llave primaria
CREATE TABLE [dbo].[Countries]
(
	[CountryID]			VARCHAR(2)		NOT NULL
	,[CountryName]		VARCHAR(100)	NOT NULL
	,[CountryStatus]	BIT				NOT NULL
)

-- Insertar datos a la tabla
--	ISO 3166-1 alpha2: codes are two-letter country codes defined in ISO 3166-1

INSERT INTO [dbo].[Countries] ([CountryID], [CountryName], [CountryStatus]) VALUES ('MX', 'México', 1)
INSERT INTO [dbo].[Countries] ([CountryID], [CountryName], [CountryStatus]) VALUES ('CO', 'Colombia', 1)
INSERT INTO [dbo].[Countries] ([CountryID], [CountryName], [CountryStatus]) VALUES ('AR', 'Argentina', 1)
INSERT INTO [dbo].[Countries] ([CountryID], [CountryName], [CountryStatus]) VALUES ('PE', 'Perú', 1)
INSERT INTO [dbo].[Countries] ([CountryID], [CountryName], [CountryStatus]) VALUES ('VE', 'Venezuela', 1)
INSERT INTO [dbo].[Countries] ([CountryID], [CountryName], [CountryStatus]) VALUES ('MT', 'Malta', 0)
INSERT INTO [dbo].[Countries] ([CountryID], [CountryName], [CountryStatus]) VALUES ('YT', 'Mayotte', 0)
INSERT INTO [dbo].[Countries] ([CountryID], [CountryName], [CountryStatus]) VALUES ('UY', 'Uruguay', 1)
INSERT INTO [dbo].[Countries] ([CountryID], [CountryName], [CountryStatus]) VALUES ('ES', 'España', 1)
INSERT INTO [dbo].[Countries] ([CountryID], [CountryName], [CountryStatus]) VALUES ('EG', 'Egipto', 0)
INSERT INTO [dbo].[Countries] ([CountryID], [CountryName], [CountryStatus]) VALUES ('EC', 'Ecuador', 1)
INSERT INTO [dbo].[Countries] ([CountryID], [CountryName], [CountryStatus]) VALUES ('MA', 'Marruecos', 0)

-- Seleccionar data de la tabla countries
SELECT [CountryID], [CountryName], [CountryStatus] FROM [dbo].[Countries]

-- Verificar los indices que tiene una tabla
EXECUTE sp_helpindex 'Countries'

-- Crear INDICE ORDENADO
CREATE CLUSTERED INDEX IDX_Countries_CountryName
ON [dbo].[Countries] ([CountryName])

-- Seleccionar data de la tabla countries
SELECT [CountryID], [CountryName], [CountryStatus] FROM [dbo].[Countries]

-- Verificar los indices que tiene una tabla
EXECUTE sp_helpindex 'Countries'

-- Crear INDICE NO AGRUPADO
CREATE NONCLUSTERED INDEX IDX_Countries_CountryID
ON [dbo].[Countries] ([CountryID])

-- Seleccionar todas las columnas de la tabla countries
SELECT * FROM [dbo].[Countries]

-- Seleccionar data de la tabla countries
SELECT [CountryID], [CountryName] FROM [dbo].[Countries]

/* Teoria Clase

1. **Índices y su importancia**: Los índices son estructuras que ayudan a mejorar el rendimiento de las consultas en una base de datos al permitir un acceso más rápido a los datos. Sin embargo, su uso debe ser administrado correctamente para evitar efectos negativos en el rendimiento.

2. **Tipos de índices**: Se mencionan dos tipos principales de índices: primarios y secundarios. Estos se diferencian en la forma en que están organizados y en qué atributos se basan.

   - **Índices primarios**: Estos se crean a partir de un atributo que ya está ordenado en la base de datos. Se subdividen en densos, dispersos y multinivel.
     - **Primario denso**: Se genera un registro de índice para cada valor de clave en la base de datos.
     - **Primario disperso**: Se genera un registro de índice para ciertos rangos de valores de clave en la base de datos.
     - **Primario multinivel**: Tiene uno o varios niveles de índices dispersos y un último nivel denso que apunta a la base de datos.

   - **Índices secundarios**: Estos se construyen sobre un atributo que no está previamente ordenado en la base de datos. Para esto, se necesita reconstruir el orden. Se utiliza un archivo de índices que contiene un campo con el valor de la clave y un campo que apunta a los registros con ese valor.

3. **Índice B+**: Aunque no se describe en detalle en este fragmento, el índice B+ es un tipo de índice muy utilizado en bases de datos. Es similar a los índices primarios multinivel mencionados anteriormente, ya que utiliza un árbol B+ para almacenar las claves y los punteros a los registros de datos.

- Los índices son esenciales para mejorar el rendimiento de las consultas en una base de datos, pero su diseño y administración adecuados son clave para evitar problemas de rendimiento. Los tipos de índices primarios y secundarios ofrecen diferentes formas de organizar y acceder a los datos en función de los requisitos específicos de la base de datos y las consultas que se ejecutarán.
*/
