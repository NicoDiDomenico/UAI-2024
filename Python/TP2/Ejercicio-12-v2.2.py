import csv
from collections import defaultdict

def leer_csv(archivo):
    datos = []
    with open(archivo, 'r') as file:
        reader = csv.DictReader(file)
        for row in reader:
            row['dia'] = int(row['dia'])
            row['mes'] = int(row['mes'])
            row['costo_unitario'] = float(row['costo_unitario'])
            for key in row:
                if key.startswith('operador'):
                    row[key] = int(row[key])
            datos.append(row)
    return datos

datos = leer_csv('produccion.csv')

def costo_total_por_dia(datos):
    costos = defaultdict(float)
    for row in datos:
        total_ensamblados = sum(row[key] for key in row if key.startswith('operador'))
        costos[(row['dia'], row['mes'])] += total_ensamblados * row['costo_unitario']
    return costos

costos_totales = costo_total_por_dia(datos)
print(costos_totales)
