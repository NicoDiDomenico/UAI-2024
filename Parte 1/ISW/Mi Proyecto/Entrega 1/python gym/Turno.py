from Rango_Horario import Rango_Horario

# Tengo que corregir los atributos para acceder ya que son privados y lo invoque como si fuesen publicos
class Turno:
    def __init__(self, idTurno, fechaTurno, estadoTurno, entrenador, rangoHorario):
        self.__idTurno = idTurno
        self.__fechaTurno = fechaTurno
        self.__estadoTurno = estadoTurno
        self.__entrenador = entrenador
        self.__rangoHorario = rangoHorario

    def getDatosTurnos():
        return (self.__idTurno, self.__fechaTurno, self.__rangoHorario.horaDesde, self.__rangoHorario.horaHasta) # No estoy seguro de esta parte, porque en la interfaz no lo puse pero si en el CU