from Socio import Socio
from datetime import datetime, timedelta

# Tengo que corregir los atributos para acceder ya que son privados y lo invoque como si fuesen publicos
class Gimnasio:
    def __init__(self):
        self.__socios = []
        self.__sociosEliminados = []
        self.__paginaWeb = "www.SariesGym.com"
        self.__turnos = []
    
    def agregarSocio(self, nombreYApellido, fechaNacimiento, genero, telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta, plan, diasAsistencia):
        # Método para asignar ID automáticamente
        idSocio = self.asignarIdAutomaticamente()
        
        # Estado inicial del socio
        estadoSocio = "Nuevo"
        
        # Obtener la fecha actual
        fecha_inicio = datetime.now()
        
        # Calcular la fecha de fin según el plan
        if plan == 'mensual':
            fecha_fin = fecha_inicio + timedelta(days=30)
        elif plan == 'anual':
            fecha_fin = fecha_inicio + timedelta(days=365)
        else:
            raise ValueError("Plan no válido")
        
        # Validar los datos ingresados
        ok = self.validarDatos(nombreYApellido, fechaNacimiento, genero, telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta, plan, diasAsistencia)
        
        if ok:
            # Crear un nuevo socio
            nuevo_socio = Socio(
                idSocio, nombreYApellido, fechaNacimiento, genero, telefono, email,
                obraSocial, nombreUsuario, contrasena, pregunta, respuesta, plan,
                estadoSocio, fecha_inicio.strftime('%Y-%m-%d'), diasAsistencia, fecha_fin.strftime('%Y-%m-%d')
            )

            # Enviar correo de confirmación
            rta = nuevo_socio.enviarCorreoConfirmacion()
            if rta:
                self.socios.append(nuevo_socio)
                print(f"Socio agregado correctamente: {nombreYApellido}")

                # Enviar credenciales
                nuevo_socio.enviarCredenciales(self.paginaWeb)
            else:
                print("Error al enviar el correo de confirmación.")
        else:
            print('Datos incorrectos!!!')

    def asignarIdAutomaticamente(self):
        # Implementar lógica para generar ID único
        pass

    def validarDatos(self, nombreYApellido, fechaNacimiento, genero, telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta, plan, diasAsistencia):
        # Implementar la lógica de validación de datos
        pass

    def enviarCorreo(self, destinatario, asunto, cuerpo):
        remitente = "tu_empresa@example.com"

        msg = MIMEText(cuerpo)
        msg['Subject'] = asunto
        msg['From'] = remitente
        msg['To'] = destinatario

        try:
            with smtplib.SMTP('smtp.example.com') as server:
                server.login("tu_usuario", "tu_contrasena")
                server.sendmail(remitente, [destinatario], msg.as_string())
            print("Correo enviado exitosamente.")
            return True
        except Exception as e:
            print(f"Error al enviar el correo: {e}")
            return False

    def enviarCorreoConfirmacion(self, nombreYApellido, email):
        asunto = "Confirmación de correo electrónico"
        cuerpo = f"Estimado/a {nombreYApellido},\n\nPor favor confirme su correo electrónico haciendo clic en el siguiente enlace.\n\nGracias."
        return self.enviarCorreo(email, asunto, cuerpo)

    def enviarCredenciales(self, nombreYApellido, email, idSocio, nombreUsuario, contrasena, pregunta, respuesta):
        asunto = "Envio de credenciales"
        cuerpo = (f"Estimado/a {nombreYApellido},\n\n"
                  f"Le dejamos las credenciales de acceso a la página web {self.paginaWeb}.\n\n"
                  f"ID para presentar al Administrador: {idSocio}\n"
                  f"Usuario: {nombreUsuario}\n"
                  f"Contraseña: {contrasena}\n"
                  f"Pregunta de recuperación: {pregunta}\n"
                  f"Respuesta: {respuesta}")
        return self.enviarCorreo(email, asunto, cuerpo)

    def consultarSocio(self, id):
        for socio in self.socios:
            if (socio.idSocio == id):
                socio.getDatosSocios()
            else:
                print(f"No se encontró el socio con id {id}")

    def modificarSocio(self, id, datoAModificar, valorDato): # datoAModificar se obtiene cuando se hace clic en el campo, valorDato lo ingresa el administrador o lo selecciona
        for socio in self.socios:
            if (socio.idSocio == id): 
                socio.datoAModificar = valorDatos
                if (datoAModificar == socio.estadoSocio):
                    if socio.plan == 'mensual':
                        socio.fechaFinActividades += timedelta(days=30)
                    elif socio.plan == 'anual':
                        socio.fechaFinActividades += timedelta(days=365)
                    
    def eliminarSocio(self, id):
        for socio in self.socios:
            if (socio.idSocio == id):
                if (socio.__rutinas is not null):
                    socio.estadoSocio = "Eliminado"
                    self.sociosEliminados.append(socio)
                    self.socios.remove(socio)
                else:
                    print('No se pueden eliminar socios que no tengan rutinas asignadas')

    def recuperarSocio(self, id):
        for socio in self.sociosEliminados:
            if (socio.idSocio == id):
                socio.estadoSocio = "Suspendido"
                self.socios.append(socio)
                self.sociosEliminados.remove(socio)

    def validarIngreso(self, id):
        for turno in self.__turnos:
            if (turno.Socio.idSocio == id and turno.fechaTurno == datetime.now()):
                turno.estadoTurno = "Finalizado"
                turno.getDatosTurnos() # No estoy seguro de esta parte, porque en la interfaz no lo puse pero si en el CU
                print("Validación Exitosa!")
            else:
                print("No tiene turno registrado!")