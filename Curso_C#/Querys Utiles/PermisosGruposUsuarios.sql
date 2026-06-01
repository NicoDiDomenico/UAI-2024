select u.IdUsuario, CONCAT(pr.Nombre,' ',pr.Apellido) as Nombre_Apellido, u.Username, g.Nombre from Usuario u
inner join UsuarioGrupo ug
on u.IdUsuario = ug.IdUsuario
inner join Grupo g
on ug.IdGrupo = g.IdGrupo
inner join PersonaResponsable pr
on pr.IdUsuario = u.IdUsuario