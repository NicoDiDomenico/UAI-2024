# Brinda a los usuarios un mayor control al momento de mostrar los resultados
print('Arroz con leche me quiero casar')
str_1 = 'leche'
str_2 = 'casar'

# Como lo haciamos
print('Arroz con ' + str_1 + ' me quiero ' + str_2)
# Aplicando formato
print('Arroz con {0} me quiero {1}'.format('leche', 'casar'))
print('Arroz con {str_1} me quiero {str_2}'.format(str_1='leche', str_2='casar'))
# Otra manera:
texto = 'Arroz con {} me quiero {}'
print(texto.format(str_1, str_2))
# Agregando espacios con format:
print()
nombre = 'Nico'
print('Hola, mi nombre es {}'.format(nombre))
print('Hola, mi nombre es {}. Mucho gusto!'.format(nombre))
print('Hola, mi nombre es {:20}. Mucho gusto!'.format(nombre))
print('Hola, mi nombre es {:<20}. Mucho gusto!'.format(nombre))
print('Hola, mi nombre es {:>20}. Mucho gusto!'.format(nombre))
print('Hola, mi nombre es {:^20}. Mucho gusto!'.format(nombre))
# Redondear
numero = 3.14159
print('El numero es: {:.2f}'.format(numero))
# Convertir a numero binario
print('El numero es: {:o}'.format(1000))
# Convertir a numero octal
print('El numero es: {:o}'.format(1000))
# Convertir a hexa
print('El numero es: {:x}'.format(1000))
print('El numero es: {:X}'.format(1000))
# Notacion cientifica
print('El numero es: {:e}'.format(1000))
# Mi forma
print()
print(f'Arroz con {str_1} me quiero {str_2}')
