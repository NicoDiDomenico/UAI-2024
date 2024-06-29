import smtplib
from email.mime.text import MIMEText
from datetime import datetime, timedelta

class Socio:
    def __init__(self, id, nombreYApellido, fechaNacimiento, genero, nroDocumento, ciudad, direccion, telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta, plan, estadoSociio, fechaInicioActividades):
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
        self.__fechaFinActividades = self.calcular_fecha_fin_actividades(plan, fechaInicioActividades)
        self.__fechaNotificacion = null # Falta implementacion
        self.__respuestaNotificacion = False # Falta implementacion
        self.__dias = ['Lunes','Miercoles', 'Viernes'] # Falta implementacion
    
    def __calcularFechaFinActividades(self, plan, fecha_inicio):
        fecha_inicio = datetime.strptime(fecha_inicio, '%Y-%m-%d')  # Convertir fecha_inicio a objeto datetime
        if plan == 'mensual':
            fecha_fin = fecha_inicio + timedelta(days=30)
        elif plan == 'anual':
            fecha_fin = fecha_inicio + timedelta(days=365)
        else:
            raise ValueError("Plan no válido")
        
        return fecha_fin.strftime('%Y-%m-%d')  # Devolver fecha_fin como string en formato YYYY-MM-DD
    
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

    def __enviarCorreoConfirmacion(self):
        asunto = "Envio de credenciales"
        cuerpo = f"Estimado/a {self.__nombreYApellido},\n\nPor favor confirme su correo electrónico haciendo clic en el siguiente enlace.\n\nGracias."
        remitente = "tu_empresa@example.com"
        destinatario = self.__email

        msg = MIMEText(cuerpo)
        msg['Subject'] = asunto
        msg['From'] = remitente
        msg['To'] = destinatario

        try:
            with smtplib.SMTP('smtp.example.com') as server:
                server.login("tu_usuario", "tu_contrasena")
                server.sendmail(remitente, [destinatario], msg.as_string())
            print("Correo enviado exitosamente.")
        except Exception as e:
            print(f"Error al enviar el correo: {e}")

       

    def __enviarCredenciales(self, pagina):
        asunto = "Confirmación de correo electrónico"
        cuerpo = f"Estimado/a {self.__nombreYApellido}, le dejamos las credenciales de acceso a la pagina web {pagina}.\n\nID para presentar al Administrador: {self.idSocio}\nUsuario: {self.__nombreUsuario}\nContraseña:{self.__contrasena}\nPregunta de recuperación:{self.pregunta}\nRespuesta:{self.__respuesta}"
        remitente = "tu_empresa@example.com"
        destinatario = self.__email

        msg = MIMEText(cuerpo)
        msg['Subject'] = asunto
        msg['From'] = remitente
        msg['To'] = destinatario

        try:
            with smtplib.SMTP('smtp.example.com') as server:
                server.login("tu_usuario", "tu_contrasena")
                server.sendmail(remitente, [destinatario], msg.as_string())
            print("Correo enviado exitosamente.")
        except Exception as e:
            print(f"Error al enviar el correo: {e}")
    

