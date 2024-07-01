import smtplib
from email.mime.text import MIMEText

class Socio:
    def __init__(self, id, nombreYApellido, fechaNacimiento, genero, nroDocumento, ciudad, direccion, telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta, plan, estadoSociio, fechaInicioActividades, diasAsistencia, fechaFinActividades):
        self.__idSocio = id
        self.__nombreYApellido = nombreYApellido
        self.__fechaNacimiento = fechaNacimiento
        self.__genero = genero
        self.__telefono = telefono
        self.__email = email
        self.__obraSocial = obraSocial
        self.__nombreUsuario = nombreUsuario
        self.__contrasena = contrasena
        self.__pregunta = pregunta
        self.__respuesta = respuesta
        self.__plan = plan
        self.__estadoSocio = estadoSocio
        self.__fechaInicioActividades = fechaInicioActividades
        self.__fechaFinActividades = fechaFinActividades
        self.__fechaNotificacion = null # Falta implementacion
        self.__respuestaNotificacion = False # Falta implementacion
        self.__dias = diasAsistencia # por ej. ['Lunes','Miercoles', 'Viernes']
        self.__rutinas = []
    
    def getDatosPreviosSocio(self):
        return (self.nombreYApellido, self.estadoSocio, self.fechaFinActividades)
    
    def getDatosSocios(self):
        return (
            self.__idSocio,
            self.__nombreYApellido,
            self.__fechaNacimiento,
            self.__genero,
            self.__nroDocumento,
            self.__ciudad,
            self.__direccion,
            self.__telefono,
            self.__email,
            self.__obraSocial,
            self.__nombreUsuario,
            self.__contrasena,
            self.__pregunta,
            self.__respuesta,
            self.__plan,
            self.__estadoSocio,
            self.__fechaInicioActividades,
            self.__fechaFinActividades,
            self.__fechaNotificacion,
            self.__respuestaNotificacion,
            self.__dias
        )


    

