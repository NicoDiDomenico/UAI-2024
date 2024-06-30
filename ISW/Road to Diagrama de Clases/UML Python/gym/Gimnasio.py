from Socio import Socio

class Gimnasio:
    def __init__(self):
        self.socios = []
        self.paginaWeb = "www.SariesGym.com"
    
    
    def agregarSocio(self, idSocio, nombreYApellido, fechaNacimiento, genero, telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta, plan, estadoSocio, fechaInicioActividades):
        # estadoSocio = "Nuevo"
        # me gustaria que la id del socio se autogenere, y que no la tenga que ingresar el administrador
        # faltaria un metodo para validar el resto de datos
        nuevo_socio = Socio(idSocio, nombreYApellido, fechaNacimiento, genero, telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta, plan, estadoSocio, fechaInicioActividades)

        rta = nuevo_socio.__enviarCorreoConfirmacion() # hay que revisar esto
        if (rta == True):
            self.socios.append(nuevo_socio)
            print(f"Socio agregado correctamente: {nombreYApellido}")
        
        nuevo_socio.__enviarCredenciales(self.paginaWeb)

    def consultarSocio(id):
        for socio in socios:
            if (socio.idSocio == id):
                socio.getDatosSocios
            else:
                print(f"No se encontró el socio con id {id}")

    def eliminarSocio(id):