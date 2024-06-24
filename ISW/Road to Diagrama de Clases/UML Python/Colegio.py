class Colegio:
    def __init__(self, nombre, direccion, telefono, director, capacidad):
        self.nombre = nombre
        self.direccion = direccion
        self.telefono = telefono
        self.director = director
        self.capacidad = capacidad
        self.estudiantes = []
        self.profesores = []
        self.cursos = []
        
    def matricular_estudiante(self, estudiante, curso):
        if len(self.estudiantes) < self.capacidad:
            if curso in self.cursos:
                estudiante.inscribir_curso(curso)
                self.estudiantes.append(estudiante)
            else:
                print("El curso no se imparte en el Colegio")
        else:
            print("Capacidad máxima alcanzada")

    def expulsar_estudiante(self, estudiante):
        self.estudiantes.remove(estudiante)

    def contratar_profesor(self, profesor):
        self.profesores.append(profesor)

    def despedir_profesor(self, profesor):
        self.profesores.remove(profesor)

    def agregar_curso(self, curso):
        self.cursos.append(curso)

    def eliminar_curso(self, curso):
        self.cursos.remove(curso)

    def get_datos_colegio(self):
        return (f"Nombre: {self.nombre}, Dirección: {self.direccion}, "
                f"Teléfono: {self.telefono}, Capacidad: {self.capacidad}, Director: {self.director}")

    def set_datos_colegio(self, nombre, direccion, telefono, capacidad, director):
        self.nombre = nombre
        self.direccion = direccion
        self.telefono = telefono
        self.capacidad = capacidad
        self.director = director

    def obtener_cursos(self):
        return self.cursos