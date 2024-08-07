from auto import Auto

auto_1 = Auto('Chevy', 'Corvette', 2023, 'Azul')
auto_2 = Auto('Ford', 'Mustang', 2022, 'Rojo')

# Accediendo a los datos
print(auto_1.marca)
print(auto_1.modelo)
print(auto_1.ano)
print(auto_1.color)
print()
print(auto_2.marca)
print(auto_2.modelo)
print(auto_2.ano)
print(auto_2.color)
print()

# Accediendo a los metodos
# Metodo de Instancia
auto_1.encendido()
auto_2.apagado()
print()
# Metodo de Clase
print(auto_1.obtener_ruedas())
print()
# Probando variables y metodos privados
auto_1.ruedas_priv() # si le saco el '__' al metodo ruedas_priv de auto.py notar que se hace publico y se muestra
auto_1.__ruedas_priv() # cuando lo haga publico desde acá lo hago privado porque le agrego el '__' desde main.py