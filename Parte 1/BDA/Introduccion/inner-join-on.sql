select e.apellido as Apellido, e.nombre as Nombre, m.nombre as Materia
from Estudiante e
inner join Materia m -- Forma Expl�cita, si uso un where se lo denomina forma impl�citita
on e.id = m.id_estudiante
order by e.apellido;