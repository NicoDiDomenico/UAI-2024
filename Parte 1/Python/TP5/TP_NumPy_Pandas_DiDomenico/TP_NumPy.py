# Trabajo Práctico con NumPy

# Importar NumPy y Matplotlib
import numpy as np
import matplotlib.pyplot as plt

# Establecer una semilla para reproducibilidad
np.random.seed(0)

# Generar datos de temperaturas simuladas para 365 días en el rango de -10 a 35 grados Celsius
temperatures = np.random.uniform(-10, 35, 365)

# Calcular estadísticas básicas
mean_temp = np.mean(temperatures)  # Media de las temperaturas
median_temp = np.median(temperatures)  # Mediana de las temperaturas
std_temp = np.std(temperatures)  # Desviación estándar de las temperaturas
min_temp = np.min(temperatures)  # Temperatura mínima
max_temp = np.max(temperatures)  # Temperatura máxima

# Imprimir las estadísticas básicas
print(f"Media: {mean_temp:.2f}°C")
print(f"Mediana: {median_temp:.2f}°C")
print(f"Desviación Estándar: {std_temp:.2f}°C")
print(f"Temperatura Mínima: {min_temp:.2f}°C")
print(f"Temperatura Máxima: {max_temp:.2f}°C")

# Identificar días con temperaturas extremas
cold_days = np.where(temperatures < -5)  # Días con temperaturas por debajo de -5°C
hot_days = np.where(temperatures > 30)  # Días con temperaturas por encima de 30°C

# Imprimir la cantidad de días con temperaturas extremas
print(f"Días fríos (< -5°C): {len(cold_days[0])}")
print(f"Días calientes (> 30°C): {len(hot_days[0])}")

# Analizar tendencias estacionales
# Suponiendo 90 días para invierno, primavera, verano y el resto para otoño
winter_avg = np.mean(temperatures[:90])  # Promedio de temperaturas en invierno
spring_avg = np.mean(temperatures[90:180])  # Promedio de temperaturas en primavera
summer_avg = np.mean(temperatures[180:270])  # Promedio de temperaturas en verano
autumn_avg = np.mean(temperatures[270:])  # Promedio de temperaturas en otoño

# Imprimir los promedios estacionales
print(f"Promedio Invierno: {winter_avg:.2f}°C")
print(f"Promedio Primavera: {spring_avg:.2f}°C")
print(f"Promedio Verano: {summer_avg:.2f}°C")
print(f"Promedio Otoño: {autumn_avg:.2f}°C")

# Visualizar los datos usando matplotlib

plt.plot(temperatures, label='Temperatura Diaria')
plt.axhline(winter_avg, color='b', linestyle='--', label='Promedio Invierno')
plt.axhline(spring_avg, color='g', linestyle='--', label='Promedio Primavera')
plt.axhline(summer_avg, color='r', linestyle='--', label='Promedio Verano')
plt.axhline(autumn_avg, color='y', linestyle='--', label='Promedio Otoño')
plt.xlabel('Días')
plt.ylabel('Temperatura (°C)')
plt.title('Análisis de Temperaturas Diarias')
plt.legend()
plt.show()
