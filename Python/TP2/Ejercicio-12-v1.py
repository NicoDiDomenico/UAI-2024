import csv

class Produccion:
    def __init__(self, dia, mes, costo_unitario, ensamblados):
        self.dia = dia
        self.mes = mes
        self.costo_unitario = costo_unitario
        self.ensamblados = ensamblados

class LineaProduccion:
    def __init__(self):
        self.datos = []

    def cargar_datos(self, archivo):
        with open(archivo, 'r') as file:
            reader = csv.reader(file)
            next(reader)  # Saltar encabezados
            for row in reader:
                dia = int(row[0])
                mes = int(row[1])
                costo_unitario = float(row[2])
                ensamblados = [int(x) for x in row[3:]]
                produccion = Produccion(dia, mes, costo_unitario, ensamblados)
                self.datos.append(produccion)

    def calcular_costo_total_por_dia(self):
        costos_totales = {}
        for produccion in self.datos:
            total = produccion.costo_unitario * sum(produccion.ensamblados)
            costos_totales[(produccion.dia, produccion.mes)] = total
        return costos_totales

    def promedio_productos_turno2(self):
        suma = 0
        conteo = 0
        for produccion in self.datos:
            suma += sum(produccion.ensamblados[5:10])
            conteo += 1
        return suma / conteo if conteo != 0 else 0

    def ausencias_por_turno(self):
        ausencias = [0, 0, 0]
        for produccion in self.datos:
            ausencias[0] += produccion.ensamblados[:5].count(0)
            ausencias[1] += produccion.ensamblados[5:10].count(0)
            ausencias[2] += produccion.ensamblados[10:15].count(0)
        return ausencias

    def mayor_ensamble(self):
        mayor_ensamble = None
        max_productos = 0
        for produccion in self.datos:
            total = sum(produccion.ensamblados)
            if total > max_productos:
                max_productos = total
                mayor_ensamble = (produccion.dia, produccion.mes)
        return mayor_ensamble

    def promedio_productos_mes_6(self):
        suma = 0
        conteo = 0
        for produccion in self.datos:
            if produccion.mes == 6:
                suma += sum(produccion.ensamblados)
                conteo += 1
        return suma / conteo if conteo != 0 else 0

    def alertas_ensamble(self):
        alertas = []
        for produccion in self.datos:
            total = sum(produccion.ensamblados)
            if total < 400 or total > 1700:
                alertas.append((produccion.dia, produccion.mes, total))
        return alertas

    def productos_por_turno(self):
        total_turnos = [0, 0, 0]
        for produccion in self.datos:
            total_turnos[0] += sum(produccion.ensamblados[:5])
            total_turnos[1] += sum(produccion.ensamblados[5:10])
            total_turnos[2] += sum(produccion.ensamblados[10:15])
        return total_turnos

    def mayor_costo_unitario(self):
        max_costo = 0
        mes = None
        for produccion in self.datos:
            if produccion.costo_unitario > max_costo:
                max_costo = produccion.costo_unitario
                mes = produccion.mes
        return mes, max_costo


if __name__ == "__main__":
    linea_produccion = LineaProduccion()
    linea_produccion.cargar_datos('produccion.csv')

    print("Costo total de producción por día:", linea_produccion.calcular_costo_total_por_dia())
    print("Promedio de productos ensamblados por colaboradores del turno 2:", linea_produccion.promedio_productos_turno2())
    print("Cantidad de ausencias por turno:", linea_produccion.ausencias_por_turno())
    print("Jornada de mayor ensamble:", linea_produccion.mayor_ensamble())
    print("Promedio diario de productos ensamblados en el mes 6:", linea_produccion.promedio_productos_mes_6())
    print("Alertas de ensamble:", linea_produccion.alertas_ensamble())
    print("Cantidad total de productos ensamblados por turno:", linea_produccion.productos_por_turno())
    print("Mes con mayor costo unitario:", linea_produccion.mayor_costo_unitario())
