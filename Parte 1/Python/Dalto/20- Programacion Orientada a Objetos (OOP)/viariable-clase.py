from auto import Auto

auto_1 = Auto('Chevy', 'Corvette', 2023, 'Azul')
auto_2 = Auto('Ford', 'Mustang', 2022, 'Rojo')

print(Auto.ruedas)
Auto.ruedas = 2
print(auto_1.ruedas)
print(auto_2.ruedas)