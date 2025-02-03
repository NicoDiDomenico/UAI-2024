use DBSISTEMA_VENTA

select * from usuario

/*
insert into rol (Descripcion)
values('ADMINISTRADOR')

insert into USUARIO (Documento, NombreCompleto, Correo, Clave, IdRol, Estado)
values 
('101010', 'ADMIN', '@GMAIL.COM', '123', 1, 1)

insert into USUARIO (Documento, NombreCompleto, Correo, Clave, IdRol, Estado)
values 
('20', 'EMPLEADO', '@GMAIL.COM', '456', 2, 1);
*/
select * from rol

select IdPermiso, idRol, NombreMenu from PERMISO
/*
insert into PERMISO (IdRol, NombreMenu) values
(1, 'menuUsuarios'),
(1, 'menuMantenedor'),
(1, 'menuVentas'),
(1, 'menuCompras'),
(1, 'menuClientes'),
(1, 'menuProveedores'),
(1, 'menuReportes'),
(1, 'menuAcercaDe')

insert into rol (Descripcion)
values('EMPLEADO')

insert into PERMISO (IdRol, NombreMenu) values
/*(2, 'menuUsuarios'),
(2, 'menuMantenedor'),*/ /*No quiero que tenga estos permisos */
(2, 'menuVentas'),
(2, 'menuCompras'),
(2, 'menuClientes'),
(2, 'menuProveedores'),
/*(2, 'menuReportes'),*/
(2, 'menuAcercaDe')
*/

select * from rol

select p.IdRol, p.NombreMenu 
from PERMISO p
inner join ROL r on r.IdRol = p.IdRol
inner join USUARIO u on u.IdRol = r.IdRol
where u.IdUsuario = 1
