# Trabajo Práctico con Pandas

# Importar Pandas y Matplotlib
import pandas as pd
import matplotlib.pyplot as plt

# Cargar los datos desde un archivo CSV
df = pd.read_csv('sales_data.csv')

# Mostrar las primeras filas del DataFrame
print(df.head())

# Inspeccionar y limpiar los datos
print(df.info())  # Verificar la presencia de valores nulos y el tipo de datos
df.drop_duplicates(inplace=True)  # Eliminar duplicados
df.fillna(0, inplace=True)  # Rellenar valores nulos con 0

# Transformar los datos
# Convertir la columna de fecha a formato de fecha y extraer el año, mes y día
df['date'] = pd.to_datetime(df['date'])
df['year'] = df['date'].dt.year
df['month'] = df['date'].dt.month
df['day'] = df['date'].dt.day

# Análisis de ventas por producto
df['total_sales'] = df['quantity'] * df['price']  # Calcular el total de ventas por producto
product_sales = df.groupby('product')['total_sales'].sum()  # Agrupar por producto y sumar las ventas
top_product = product_sales.idxmax()  # Producto con mayores ventas totales
top_product_sales = product_sales.max()  # Ventas del producto más vendido

print(f"Producto con mayores ventas: {top_product} (${top_product_sales:.2f})")

# Análisis de ventas por categoría
category_sales = df.groupby('category')['total_sales'].sum()  # Agrupar por categoría y sumar las ventas
top_category = category_sales.idxmax()  # Categoría más popular
top_category_sales = category_sales.max()  # Ventas de la categoría más popular

print(f"Categoría más popular: {top_category} (${top_category_sales:.2f})")

# Análisis de tendencias de ventas mensuales
monthly_sales = df.groupby('month')['total_sales'].sum()  # Agrupar por mes y sumar las ventas

# Visualizar las tendencias de ventas mensuales usando matplotlib
import matplotlib.pyplot as plt

plt.plot(monthly_sales.index, monthly_sales.values, marker='o')
plt.xlabel('Mes')
plt.ylabel('Ventas Totales ($)')
plt.title('Tendencias de Ventas Mensuales')
plt.grid(True)
plt.show()

# Análisis de clientes frecuentes
top_customers = df['customer_id'].value_counts().head(10)  # Identificar a los clientes con mayor número de compras

print("Clientes con mayor número de compras:")
print(top_customers)

# Guardar resultados
product_sales.to_csv('product_sales_summary.csv', header=True)  # Guardar resumen de ventas por producto
category_sales.to_csv('category_sales_summary.csv', header=True)  # Guardar resumen de ventas por categoría
