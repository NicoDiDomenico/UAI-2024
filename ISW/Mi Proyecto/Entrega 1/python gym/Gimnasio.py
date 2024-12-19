import smtplib
from Socio import Socio
from datetime import datetime, timedelta

# Tengo que corregir los atributos para acceder ya que son privados y lo invoque como si fuesen publicos
class Gimnasio:
    def __init__(self):
        self.__socios = []
        self.__sociosEliminados = []
        self.__paginaWeb = "www.SariesGym.com"
        self.__responsables = []
    
    # CUD01 - Gestionar Socio
    def validarClaveAcceso(self, clave):
        pass
    
    def mostrarSocios(self):
        listaSocios = []
        for socio in self.__socios:
            listaSocios.append(socio)
            
        for socioEliminado in self.__sociosEliminados:
            listaSocios.append(socioEliminado)
            
        for socio in listaSocios:
            nombreYApellido, estadoSocio, fechaFinActividades = socio.getDatosPreviosSocio()
            return (nombreYApellido, estadoSocio, fechaFinActividades)
    
    # CUD02 - Agregar Socio
    def agregarSocio(self, nombreYApellido, fechaNacimiento, genero, nroDocumento, ciudad, direccion, telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta, plan, diasAsistencia):
        # Método para asignar ID automáticamente
        idSocio = self.asignarIdAutomaticamente()
        
        estadoSocio = "Nuevo"
    
        fecha_inicio = datetime.now()
        
        # Calcular la fecha de fin según el plan
        if plan == 'mensual':
            fecha_fin = fecha_inicio + timedelta(days=30)
        elif plan == 'anual':
            fecha_fin = fecha_inicio + timedelta(days=365)
        else:
            raise ValueError("Plan no válido")
        
        ok = self.validarDatos(nombreYApellido, fechaNacimiento, genero, telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta, plan)
        
        if ok:
            # Crear un nuevo socio
            nuevo_socio = Socio(
                idSocio, nombreYApellido, fechaNacimiento, genero, nroDocumento, ciudad, direccion, telefono, email,
                obraSocial, nombreUsuario, contrasena, pregunta, respuesta, plan,
                estadoSocio, fecha_inicio.strftime('%Y-%m-%d'), fecha_fin.strftime('%Y-%m-%d')
            )

            nuevo_socio.agregarDias(diasAsistencia)

            # Enviar correo de confirmación
            rta = self.enviarCorreoConfirmacion(nombreYApellido, email)
            if rta:
                self.socios.append(nuevo_socio)
                print(f"Socio agregado correctamente: {nombreYApellido}")

                # Enviar credenciales
                self.enviarCredenciales(self.paginaWeb)
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
    
    # CUD03 - Consultar Socio
    def consultarSocio(self, id):
        for socio in self.socios:
            if (socio.idSocio == id):
                socio.getDatosSocio()
            else:
                print(f"No se encontró el socio con id {id}")

    # CUD04 - Modificar Socio
    def modificarSocio(self, id, datoAModificar, valorDato): # datoAModificar se obtiene cuando se hace clic en el campo, valorDato lo ingresa el administrador o lo selecciona
        for socio in self.socios:
            if (socio.idSocio == id): 
                socio.datoAModificar = valorDato
                if (datoAModificar == socio.estadoSocio):
                    if socio.plan == 'mensual':
                        socio.fechaFinActividades += timedelta(days=30)
                    elif socio.plan == 'anual':
                        socio.fechaFinActividades += timedelta(days=365)

    # CUD05 – Eliminar Socio                
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

    # CUD09 – Validar Ingreso del Socio
    def validarIngreso(self, idSocio):
        socioEncontrado = None
        for socio in self.socios:
            if socio.idSocio == idSocio:
                socioEncontrado = socio
                break
        if socioEncontrado:
            turnoValido = False
            for turno in socioEncontrado.turnos:
                if turno.fecha == datetime.now().date():
                    turno.estado = "Finalizado"
                    turnoValido = True
                    print("Validación Exitosa!")
                    print(turno.getDatosTurnos())
                    break
            if not turnoValido:
                print("No tiene turno registrado!")
        else:
            print("Socio no encontrado!")
    
    # CUD06 - Gestionar Turno
    def iniciarSesion(self, nombreUsuario, contrasena):
        rta, socioEncontrado = self.validarCredenciales(nombreUsuario, contrasena)
        if (rta == True):
            socioEncontrado.mostrarHistorialTurnos()
            self.mostrarOpcionesGestion(socioEncontrado) 
        else:
            print("Credenciales incorrectas")

    def validarCredenciales(self, nombreUsuario, contrasena):
        for socio in self.socios:
            if (nombreUsuario == socio.__nombreUsuario and contrasena == socio.__contrasena):
                return True, socio
        return False, None

    def mostrarOpcionesGestion(self, socio):
        print("1. Agregar turno")
        print("2. Cancelar turno")
        opcion = input("Seleccione una opción: ")
        if opcion == "1":
            fechaTurno = input("Ingrese la fecha del turno (YYYY-MM-DD): ")
            horaTurno = input("Ingrese la hora del turno (HH:MM): ")
            entrenador = input("Ingrese el nombre del entrenador: ")
            socio.agregarTurno(fechaTurno, horaTurno, entrenador) # --> CUD07 - Agregar Turno
        elif opcion == "2":
            idTurno = input("Ingrese el ID del turno a cancelar: ") # En realidad esto está mal, porque por como está hecha mi interfaz, yo tendria que apretar la "X" que estará al lado de cada turno que esté en proceso y ahí eliminarlo.
            socio.cancelarTurno(idTurno) # --> CUD08 - Eliminar Turno
        else:
            print("Opción no válida")


        