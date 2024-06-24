class Estudiante:
    def __init__(self, nombre, apellido, fecha_nacimiento, direccion, telefono, email, colegio):
        self.nombre = nombre
        self.apellido = apellido
        self.fecha_nacimiento = fecha_nacimiento
        self.direccion = direccion
        self.telefono = telefono
        self.email = email
        self.colegio = colegio
        self.cursos_inscritos = []

    def inscribir_curso(self, curso):
        if curso in self.colegio.obtener_cursos():
            self.cursos_inscritos.append(curso)
        else:
            print("El curso no se imparte en el Colegio")

    def abandonar_curso(self, curso):
        if curso in self.cursos_inscritos:
            self.cursos_inscritos.remove(curso)

    def obtener_edad(self):
    # Aquí iría la lógica para calcular la edad del estudiante
        return 0

    def obtener_cursos_inscritos(self):
        return self.cursos_inscritos

    def set_datos_contacto(self, direccion, telefono, email):
        self.direccion = direccion
        self.telefono = telefono
        self.email = email

    def get_datos_estudiante(self):
        return (f"Nombre: {self.nombre} {self.apellido}, Fecha de nacimiento: {self.fecha_nacimiento}, "
                f"Dirección: {self.direccion}, Teléfono: {self.telefono}, Email: {self.email}")
