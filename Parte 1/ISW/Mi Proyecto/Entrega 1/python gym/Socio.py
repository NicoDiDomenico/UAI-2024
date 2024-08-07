import datetime
from email.mime.text import MIMEText
from queue import Full
from Turno import Turno
from Rutina import Rutina

# CUD02 - Agregar Socio
class Socio:
    def __init__(self, id, nombreYApellido, fechaNacimiento, genero, nroDocumento, ciudad, direccion, telefono, email, obraSocial, nombreUsuario, contrasena, pregunta, respuesta, plan, estadoSocio, fechaInicioActividades, fechaFinActividades):
        self.__idSocio = id
        self.__nombreYApellido = nombreYApellido
        self.__fechaNacimiento = fechaNacimiento
        self.__genero = genero
        self.__nroDocumento = nroDocumento
        self.__ciudad = ciudad
        self.__direccion = direccion
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
        self.__fechaNotificacion = Full # Falta implementacion
        self.__respuestaNotificacion = False # Falta implementacion
        self.__rutinas = []
        self.__turnos = []
    
    def getDatosPreviosSocio(self):
        return (self.nombreYApellido, self.estadoSocio, self.fechaFinActividades)
    
    def getDatosSocio(self):
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
            self.__respuestaNotificacion
        )

    # CUD02 - Agregar Socio
    # Esto es un intento de construir varios objetos Rutina que tenga solo el dia cargado, el resto de atributos vacios, supongo que los podré agregar después cuando haga la otra iteración
    def agregarDias(self, dias):
        for dia in dias:
            rutina = Rutina(dia)
            self.__rutinas.append(rutina) 

    # CUD06 - Gestionar Turno
    def mostrarHistorialTurnos(self):
        for turno in self.__turnos:
            print(turno.__fechaTurno, turno.rangoHorario.__horaDesde, turno.rangoHorario.__horaHasta, "Entrenador", turno.__estadoTurno) # falta implementar lo del entrenador

    # CUD07 - Agregar Turno
    def agregarTurno(self, fechaTurno, horaTurno, entrenador):
        if (self.__estadoSocio == "Nuevo" or self.__estadoSocio == "Actualizado"):
            idTurno = self.asignarIdAutomaticamenteAlTurno()
            turno = Turno(idTurno, fechaTurno, "En curso", entrenador, horaTurno) # Esto esta todo mal, porque se ingresa y en realidad  el entrenador y el rango horario tiene que venir de otros modulos
            self.__turnos.append(turno)
            print("Turno agregado exitosamente")
            self.enviarNotificacionTurno(turno) 
        else:
            print("No puede sacar un turno. ¡¡Debe renovar la cuota!!") 

    # CUD08 - Eliminar Turno
    def cancelarTurno(self, idTurno):
        for turno in self.__turnos:
            if turno.idTurno == idTurno and turno.__estadoTurno == "En curso":
                if not self.validarAnticipacion(turno):
                    return
                confirmacion = input(f"¿Está seguro que desea cancelar el turno del {turno.__fechaTurno} a las {turno.__rangoHorario}? (s/n): ")
                if confirmacion.lower() == 's':
                    turno.__estadoTurno = "Cancelado"
                    print(f"Turno {idTurno} cancelado.")
                    self.enviarNotificacionCancelacionTurno(turno)
                return
        print(f"No se encontró el turno {idTurno} o no se puede cancelar.")
    
    def validarAnticipacion(self, turno):
        fecha_turno = datetime.strptime(turno.__fechaTurno, "%Y-%m-%d %H:%M")
        if fecha_turno - datetime.now() < datetime.timedelta(hours=3):
            print("No se puede cancelar el turno con menos de 3 horas de anticipación.")
            return False
        return True

    def asignarIdAutomaticamenteAlTurno():
        pass
    
    def enviarNotificacionTurno(self, turno):
        pass

    def enviarNotificacionCancelacionTurno(turno):
        pass
