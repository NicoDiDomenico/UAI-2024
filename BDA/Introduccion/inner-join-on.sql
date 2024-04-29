select e.apellido as Apellido, e.nombre as Nombre, m.nombre as Materia
from Estudiante e
inner join Materia m -- Forma Explícita, si uso un where se lo denomina forma implícitita
on e.id = m.id_estudiante
order by e.apellido;