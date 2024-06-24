# Se importan las librerías a utilizar
import numpy as np
from sklearn import datasets, linear_model
import matplotlib.pyplot as plt
from sklearn.model_selection import train_test_split

########## PREPARAR LA DATA ##########

# Importamos los datos del vino de la librería de scikit-learn
wine = datasets.load_wine()
print(wine)
print()

########## ENTENDIMIENTO DE LA DATA ##########

# Verifico la información contenida en el dataset
print('Información en el dataset:')
print(wine.keys())
print()

# Verifico las características del dataset
print('Características del dataset:')
print(wine.DESCR)

# Verifico la cantidad de datos que hay en el dataset
print('Cantidad de datos:')
print(wine.data.shape)
print()

# Verifico la información de las columnas
print('Nombres columnas:')
print(wine.feature_names)

########## PREPARAR LA DATA REGRESIÓN LINEAL SIMPLE ##########

# Seleccionamos solamente la primera característica del dataset
X = wine.data[:, np.newaxis, 0]

# Defino los datos correspondientes a las etiquetas
y = wine.target

# Graficamos los datos correspondientes
plt.scatter(X, y)
plt.xlabel(wine.feature_names[0])
plt.ylabel('Clase de vino')
plt.show()

########## IMPLEMENTACIÓN DE REGRESIÓN LINEAL SIMPLE ##########

# Separo los datos de "train" en entrenamiento y prueba para probar los algoritmos
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2)

# Defino el algoritmo a utilizar
lr = linear_model.LinearRegression()

# Entreno el modelo
lr.fit(X_train, y_train)

# Realizo una predicción
y_pred = lr.predict(X_test)

# Graficamos los datos junto con el modelo
plt.scatter(X_test, y_test)
plt.plot(X_test, y_pred, color='red', linewidth=3)
plt.title('Regresión Lineal Simple')
plt.xlabel(wine.feature_names[0])
plt.ylabel('Clase de vino')
plt.show()

print()
print('DATOS DEL MODELO REGRESIÓN LINEAL SIMPLE')
print()
print('Valor de la pendiente o coeficiente "a":')
print(lr.coef_)
print('Valor de la intersección o coeficiente "b":')
print(lr.intercept_)
print()
print('La ecuación del modelo es igual a:')
print('y = ', lr.coef_, 'x ', lr.intercept_)
print()
print('Precisión del modelo:')
print(lr.score(X_train, y_train))
