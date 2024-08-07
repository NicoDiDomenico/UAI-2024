select e.apellido as Apellido, e.nombre as Nombre, m.nombre as Materia
from Estudiante e
left join Materia m -- Notar que como el lado izquierdo es la tabla Estudiante => se mostrará no solo la interseccion de ambas tablas sino tambíen
					-- los datos de la tabla Estudiantes, es por eso que aquellos estudiantes que no tengan nterseccion con la tabla materia, 
					-- tendrán los datos de esta última en NULL.
on e.id = m.id_estudiante
order by e.apellido;