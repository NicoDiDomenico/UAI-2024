import pandas as pd
import numpy as np

# Establecer una semilla para reproducibilidad
np.random.seed(0)

# Generar datos ficticios
dates = pd.date_range(start='2023-01-01', periods=365)
products = ['Laptop', 'Smartphone', 'Tablet', 'Headphones', 'Smartwatch']
categories = ['Electronics', 'Accessories']
prices = np.random.uniform(50, 1000, size=365)
quantities = np.random.randint(1, 10, size=365)
customer_ids = np.random.randint(1000, 2000, size=365)

# Crear el DataFrame
data = {
    'date': np.random.choice(dates, size=365),
    'product': np.random.choice(products, size=365),
    'category': np.random.choice(categories, size=365),
    'price': prices,
    'quantity': quantities,
    'customer_id': customer_ids
}

df = pd.DataFrame(data)

# Guardar el DataFrame en un archivo CSV
df.to_csv('sales_data.csv', index=False)
