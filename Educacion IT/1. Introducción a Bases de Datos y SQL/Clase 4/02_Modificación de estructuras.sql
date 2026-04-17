-- Modificación de estructuras
use comercioit;

desc productos;

#ALTER TABLE + ADD COLUMN
alter table productos add column Observaciones varchar(50) null;
desc productos;