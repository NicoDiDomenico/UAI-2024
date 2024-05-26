# Ejercicio 12 - Di Domenico, Nicolás
import csv
from linea_produccion import LineaProduccion

linea_produccion = LineaProduccion() # Hago la tabla
linea_produccion.cargar_datos('produccion.csv') # le agrego las filas de produccion.csv

print("Algoritmo A:")
print(f"Costo total de producción por las unidades de ese día: $ {round(linea_produccion.calcular_costo_total(), 2)}")
print("Promedio de productos ensamblados por los operadores del turno 2:", round(linea_produccion.promedio_productos_turno2()), "Unidades")
ausencias = linea_produccion.ausencias_por_turno()
for k, v in ausencias.items(): # .tems devuelve una secuencia de tuplas
    print(f"Ausencias turno {k}: {v}")

print()
print("Algoritmo B:")
dia_mes = linea_produccion.mayor_ensamble()
print(f"La jornada de mayor ensamble fue el dia {dia_mes[0]} del mes {dia_mes[1]}")
print("Promedio diario de productos ensamblados en el mes 6:", linea_produccion.promedio_productos_mes_6())
print("¡Alerta de ensamble! ", linea_produccion.alertas_ensamble()) # No entiendo lo de "a medida que se vayan ingresando los datos", si yo ya los teno ingresados. Asi que lo hice a mi manera

print()
print("Algoritmo C:")
turnos = linea_produccion.productos_por_turno()
for k, v in turnos.items(): # .tems devuelve una secuencia de tuplas
    print(f"El turno {k} tiene {v} ensambles")
m, cu = linea_produccion.mayor_costo_unitario()
print(f"El mes {m} fue el que tuvo mayor costo unitario con un valor de $ {cu}", )
