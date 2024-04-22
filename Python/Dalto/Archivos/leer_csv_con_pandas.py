# Para el análisis de datos vamos a usar la libreria pandas, no la csv.
import pandas as pd # universalmente llamamos esta libreria como pd

# Si es un módulo se instaló correctamente 
print(type(pd)) 
print()

# Usando la función read_csv para leer el archivo CSV <=> se importó pandas as pd
df = pd.read_csv('Archivos\\datos.csv')
# - usamos df y no archivo. df es dataframe, estructuras de datos bidimensionales similares a una hoja de cálculos

# Imprimir el DataFrame original
print("DataFrame Original:")
print(df)
print()
# - para análisis de datos se usa: ctrl + shift + P -> Create: New Jupyter Notebook

# Obteniendo los datos de la columna nombre
print("Columna 'nombre':")
print(df["nombre"])
print()

'''
## Solo saberlo, ahora no lo vamos a usar.
# Agregando una primera fila tal que el resto de filas se desplaza hacia abajo
df = pd.read_csv('Archivos\\datos.csv', names = ['Name', 'Lastname', 'Age'])

# Imprimir DataFrame con la fila agregada
print("DataFrame con fila agregada:")
print(df)
print()
'''
# Concepto de Slicing
cadena = '0123456789'
print(cadena[2:7]) # muestra los valores desde el 2 incluido hasta el 7 sin incluir.

# Ordenando el dataframe por edad:
df_ordenado_asc = df.sort_values('edad') # .sort_values es un método para dataframes 
print("DataFrame ordenado por 'edad' de forma ascendente:")
print(df_ordenado_asc)
print()

# Ordenar por la columna 'edad' de forma descendente
df_ordenado_desc = df.sort_values(by='edad', ascending=False)

# Imprimir DataFrame después de ordenar por 'edad' de forma descendente
print("DataFrame ordenado por 'edad' de forma descendente:")
print(df_ordenado_desc)

# Concatenando 2 Dataframes:
df2 = pd.read_csv('Archivos\\datos.csv')

df_concatenado = pd.concat([df,df2])
print('Concatenamos 2 df:')
print(df_concatenado)
print()

# head() - el parámetro que ingresa a head() indica las n primeras filas que se guardarán en la var. definida.
fila_x = df.head(3) 
print('Mostrando primeras filas elegidas:')
print(fila_x)
print()

# tail() - el parámetro que ingresa a tail() indica las n últimas filas que se guardarán en la var. definida.
fila_x = df.tail(3) 
print('Mostrando últimas filas elegidas:')
print(fila_x)
print()

# shape - accediendo a la cantidad de filas y columnas con shape.
# Forma 1:
print('Forma 1:')
tupla_filas_columnas = df.shape
print(f'Cantidad de filas: {tupla_filas_columnas[0]}')

filas_totales,columnas_totales = df.shape 
print(f'Cantidad de columnas: {tupla_filas_columnas[1]}')

print()

# Forma 2:
print('Forma 2:')
cant_filas, cant_columnas = df.shape
print(f'Cantidad de filas: {cant_filas}')
print(f'Cantidad de columnas: {cant_columnas}')

print()

## 4- Funciones complejas para el análisis de datos

# Obteniendo data estadística del dataframe:
df_info = df.describe()
print(df_info)
print()

# Accediendo a un elemento específico del df con loc
elemento_específico_loc = df.loc[2]
print('Elemento específico indexado:')
print(elemento_específico_loc)
print()

# Podemos ser mas específicos y acceder a algún atributo (columna):
elemento_específico_loc = df.loc[2, 'edad']
print('loc - Edad del elemento específico indexado:')
print(elemento_específico_loc)
print()

# Podemos hacer lo mismo que lo anterior usando iloc (i por índice como argumento)
elemento_específico_iloc = df.iloc[2, 2]
print('iloc - Edad del elemento específico indexado:')
print(elemento_específico_iloc)
print()

# Accediendo a todas las filas de una columna 
elementos_específicos_iloc = df.iloc[:, 2] # Notar que usamos Slicing
print('Edades:')
print(elementos_específicos_iloc)
print()

# Accediendo a todas las columnas de una fila
elementos_específicos_iloc = df.iloc[0, :] # Notar que usamos Slicing
print('Persona con índice 0:')
print(elementos_específicos_iloc)
print()

# Accediendo a filas con edad mayor a 30:
mayor_que_30 = df.loc[df['edad']>30,:]
print('Filas con edad mayor a 30:')
print(mayor_que_30)
print()

