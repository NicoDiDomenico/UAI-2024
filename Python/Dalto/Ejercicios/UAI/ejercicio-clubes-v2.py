# TP01 - Ejercicio Clubes:

# Luego de recabar datos de los socios en cada uno de los 17 clubes
# más importantes de la ciudad se quiere determinar, para cada una de ellos,
# entre los socios censados mayores de edad (tienen 18 años o más) quienes son
# más numerosos, los que son socios temporales (código 1) o los que son
# socios permanentes (código 2).
# Para resolver esto se dispone, por cada socio de cada uno de los clubes,
# su código de asociado (1 para temporal, 2 para permanente) y edad.
# Un código de asociado 0 (cero) indica que no hay más datos de ese club.
# Validar el código entre 0, 1 y 2; no permita otros valores.
# Validar la edad que no sea negativa y reconfirme (¿esta seguro?)
# si es mayor a 100.

# Utilizar funciones,listas para guardar los datos (por ejemplo para la edad y socios), no vamos a usar objetos, usar while para validar.

# Mio
def validar_codigo(codigo):
    while codigo not in ['0', '1', '2']:
        codigo = input("Ingrese un código válido (0 para salir, 1 para socio temporal, 2 para socio permanente): ")
    return codigo

def validar_edad():
    edad = int(input("Ingrese la edad del socio: "))
    while edad < 0 or edad > 100:
        confirmacion = input("¿Está seguro de que la edad es correcta? (s/n): ")
        if confirmacion.lower() == 's':
            break
        else:
            edad = int(input("Ingrese la edad del socio nuevamente: "))
    return edad

def determinar_tipo_socio():
    tipo_socio = validar_codigo(input("Ingrese el código de asociado (0 para salir, 1 para temporal, 2 para permanente): "))
    if tipo_socio == '0':
        return None, None
    edad = validar_edad()
    return tipo_socio, edad

def contar_socios(club):
    temporales = 0
    permanentes = 0
    tipo_socio, edad = determinar_tipo_socio()
    while tipo_socio:
        if edad >= 18:
            if tipo_socio == '1':
                temporales += 1
            elif tipo_socio == '2':
                permanentes += 1
        tipo_socio, edad = determinar_tipo_socio()
    return temporales, permanentes

#def main():
clubes = list()
for i in range(1, 18):
    print(f"\nClub {i}:")
    temporales, permanentes = contar_socios(i)
    clubes.append((temporales, permanentes))

print()
for i, (temporales, permanentes) in enumerate(clubes, start=1):
    if temporales > permanentes:
        print(f"En el club {i}, hay más socios temporales que permanentes.")
    elif temporales < permanentes:
        print(f"En el club {i}, hay más socios permanentes que temporales.")
    else:
        print(f"En el club {i}, hay igual cantidad de socios temporales y permanentes.")
# main()

