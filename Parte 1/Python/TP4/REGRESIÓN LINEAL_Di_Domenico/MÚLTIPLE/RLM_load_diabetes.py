########## LIBRERIAS A UTILIZAR ##########

# Se importan las librerías a utilizar
import numpy as np
from sklearn import datasets, linear_model
import matplotlib.pyplot as plt
from sklearn.model_selection import train_test_split

########## PREPARAR LA DATA ##########

# Importamos los datos de diabetes de la librería de scikit-learn
diabetes = datasets.load_diabetes()
print(diabetes)
print()

########## ENTENDIMIENTO DE LA DATA ##########

# Verifico la información contenida en el dataset
print('Información en el dataset:')
print(diabetes.keys())
print()

# Verifico las características del dataset
print('Características del dataset:')
print(diabetes.DESCR)

# Verifico la cantidad de datos que hay en el dataset
print('Cantidad de datos:')
print(diabetes.data.shape)
print()

# Verifico la información de las columnas
print('Nombres columnas:')
print(diabetes.feature_names)

########## PREPARAR LA DATA REGRESIÓN LINEAL MÚLTIPLE ##########

# Seleccionamos varias columnas del dataset (por ejemplo, todas las columnas)
X_multiple = diabetes.data
print(X_multiple)

# Defino los datos correspondientes a las etiquetas
y_multiple = diabetes.target

########## IMPLEMENTACIÓN DE REGRESIÓN LINEAL MÚLTIPLE ##########

# Separo los datos de "train" en entrenamiento y prueba para probar los algoritmos
X_train, X_test, y_train, y_test = train_test_split(X_multiple, y_multiple, test_size=0.2)

# Defino el algoritmo a utilizar
lr_multiple = linear_model.LinearRegression()

# Entreno el modelo
lr_multiple.fit(X_train, y_train)

# Realizo una predicción
y_pred_multiple = lr_multiple.predict(X_test)

print()
print('DATOS DEL MODELO REGRESIÓN LINEAL MÚLTIPLE')
print()
print('Valor de la pendiente o coeficiente "a":')
print(lr_multiple.coef_)
print('Valor de la intersección o coeficiente "b":')
print(lr_multiple.intercept_)
print()
print('Precisión del modelo:')
print(lr_multiple.score(X_train, y_train))

