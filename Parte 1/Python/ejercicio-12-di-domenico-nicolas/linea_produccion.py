import csv
from produccion import Produccion

# Hago que toda la tabla del .CSV sea un objeto, entonces guardare cada fila(del tipo objeto Produccion) en esta tabla mediante el metodo cargar_datos()
class LineaProduccion:
    def __init__(self): # Metodo constructor
        self.filas = [] # Lista vacía para guardar objetos de tipo Produccion

# La tabla la hago con este metodo:
    def cargar_datos(self, archivo):
        with open(archivo, 'r') as file: # Abro produccion.csv en modo lectura
            reader = csv.reader(file) # Creo un objeto reader para leer el archivo gracias a import csv
            next(reader)  # Salteo el encabezado
            for row in reader: # Armo la fila
                dia = row[0]
                mes = row[1]
                costo_unitario = row[2]
                ensamblados = row[3:]
                produccion = Produccion(dia, mes, costo_unitario, ensamblados) # La defino como objeto
                self.filas.append(produccion) # La agrego a la lisa de objetos (lista de filas)

    def calcular_costo_total(self):
        costo_total_por_dia = {} # Creo un diccionario vacio
        for fila in self.filas: # Recorro uno a uno el objeto asi uso sus atributos
            dia = fila.dia
            costo_total = sum(fila.ensamblados) * fila.costo_unitario
            if dia in costo_total_por_dia:
                costo_total_por_dia[dia] += costo_total # Para el mismo dia, pero de otra semana, le incremento el costo
            else:
                costo_total_por_dia[dia] = costo_total # Agrego a la nueva key el dia con su correspondiente value (costo_total)
        total = sum(costo_total_por_dia.values())
        return total

    def promedio_productos_turno2(self):
        suma_productos = 0  # Inicializo una variable para almacenar la suma de los productos del turno 2
        cantidad_productos = 0  # Inicializo una variable para contar la cantidad de productos del turno 2
        for fila in self.filas:
            suma_productos += sum(fila.ensamblados[5:10]) # Sumo los productos del turno 2 de la fila
            cantidad_productos += len(fila.ensamblados[5:10]) # Cuento la cantidad de productos del turno 2 de la fila

        if cantidad_productos > 0:
            promedio = suma_productos / cantidad_productos
        else:
            promedio = 0
        return promedio

    def ausencias_por_turno(self):
        ausencias = {1: 0, 2: 0, 3: 0} # La key son los turnos
        for fila in self.filas:
            for i, cantidad in enumerate(fila.ensamblados):
                if cantidad == 0:
                    if 0 <= i <= 4: # op1 a op5
                        ausencias[1] += 1
                    elif 5 <= i <= 9: # op6 a op10
                        ausencias[2] += 1
                    else:
                        ausencias[3] += 1 # op10 a op15
        return ausencias

    def mayor_ensamble(self):
        max_ensamble = 0
        max_dia_mes = [0, 0]
        for fila in self.filas:
            total_ensamble = sum(fila.ensamblados)
            if total_ensamble > max_ensamble: # Me voy a quedar con el dia y mes que mas se ensambló
                max_ensamble = total_ensamble
                max_dia_mes = [fila.dia, fila.mes]
        return max_dia_mes

    def promedio_productos_mes_6(self):
        total_productos = 0
        total_dias = 0
        promedio = 0
        for fila in self.filas:
            if fila.mes == 6: # Quiero el promedio pero del mes 6
                total_productos += sum(fila.ensamblados)
                total_dias += 1
        if total_dias > 0:
            promedio = total_productos / total_dias
        return promedio

    def alertas_ensamble(self):
        alertas = []
        for fila in self.filas:
            total_ensamble = sum(fila.ensamblados)
            if total_ensamble < 400 or total_ensamble > 1700:
                alertas.append((fila.dia, fila.mes)) # Ese dia y ese mes tendrá una alerta
        return alertas

    def productos_por_turno(self):
        productos_turno = {1: 0, 2: 0, 3: 0} # La key son los turnos
        for fila in self.filas:
            productos_turno[1] += sum(fila.ensamblados[0:5]) # op1 a op5
            productos_turno[2] += sum(fila.ensamblados[5:10]) # op6 a op10
            productos_turno[3] += sum(fila.ensamblados[10:15]) # op10 a op15
        return productos_turno

    def mayor_costo_unitario(self):
        max_costo = 0
        max_mes = 0
        for fila in self.filas: # Me voy a quedar con el mes que hubo mayor costo unitario
            if fila.costo_unitario > max_costo:
                max_costo = fila.costo_unitario
                max_mes = fila.mes
        return max_mes, max_costo