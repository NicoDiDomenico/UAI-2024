# Cada fila de mi .CSV será un objeto del tipo Produccion
class Produccion:
    def __init__(self, dia, mes, costo_unitario, ensamblados): # Metodo constructor
        self.dia = int(dia)
        self.mes = int(mes)
        self.costo_unitario = float(costo_unitario)
        self.ensamblados = [int(x) for x in ensamblados] # A las cantidades ensambladas de cada operador los colocamos en una lista por compresion

