########## LIBRERIAS A UTILIZAR ##########

# Se importan la librerias a utilizar
import numpy as np
from sklearn import datasets, linear_model
import matplotlib.pyplot as plt
from sklearn.model_selection import train_test_split

########## PREPARAR LA DATA ##########

# Importamos los datos de la misma librería de scikit-learn
boston = datasets.load_boston()
print(boston)
print()

########## ENTENDIMIENTO DE LA DATA ##########

# Verifico la información contenida en el dataset
print('Información en el dataset:')
print(boston.keys())
print()

# Verifico las características del dataset
print('Características del dataset:')
print(boston.DESCR)

# Verifico la cantidad de datos que hay en los dataset
print('Cantidad de datos:')
print(boston.data.shape)
print()

# Verifico la información de las columnas
print('Nombres columnas:')
print(boston.feature_names)

########## PREPARAR LA DATA REGRESIÓN LINEAL MÚLTIPLE ##########

# Seleccionamos las columnas 5, 6, 7  del dataset (Seleccionamos varias porque es RL MÚLTIPLE)
X_multiple = boston.data[:, 5:8]
print(X_multiple)

# Defino los datos correspondientes a las etiquetas
y_multiple = boston.target

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

