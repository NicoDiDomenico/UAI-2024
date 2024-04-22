# Cambiar el tipo de dato de una columna 
import pandas as pd
df = pd.read_csv('ArchivosProblemas\\datos.csv')

# Muestro la tabla
print(df)
print()

# Chequeo el tipo de dato de la columna edad y fila 0
print(type(df['edad'][0]))
print()

# Como no me sirve ese tipo de dato, paso la columna edad a str
df['edad'] = df['edad'].astype(str) 
# .astype() es un método de pandas. que permite cambiar el tipo de datos de una serie a otro tipo de datos 
# especificado. En este caso, se está convirtiendo la columna 'edad' del DataFrame df en strings.

# Compruebo que el valor en la columna edad y fila 0 sea str
print(type(df['edad'][0]))
print()

# Lo muestro
print(df['edad'][0])
print()

## También puedo remplazar valores - .replace()
print('Remplazo Cami por Sofi con .replace: ')
df["nombre"].replace("Cami","Sofi", inplace = True)
print(df)
print()

'''
### YO LO HICE DE ESTA FORMA Y SIRVE IGUAL
print('Remplazo Cami por Sofi2 con mi forma: ')
df["nombre"][3] = "Sofi2"
print(df)
print()
''' # No sirve al final :(
    
## Eliminando las filas con datos faltantes
print('Eliminamos las filas vacias: ')
df = df.dropna()
print(df)

## Eliminando las filas repetidas
print('Eliminamos las filas duplicadas: ')
df = df.drop_duplicates()
print(df)
print()

# Creando un CSV con el dataframe resultante (limpio)
df.to_csv('ArchivosProblemas\\datos_limpios.csv')