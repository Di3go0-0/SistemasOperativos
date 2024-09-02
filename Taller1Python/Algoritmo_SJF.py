import matplotlib.pyplot as plt
from tabulate import tabulate

def calcular_tiempos_sjf(procesos):
    # Inicialización de variables
    tiempo_actual = 0
    procesos_ordenados = []
    tiempos_en_sistema = []
    tiempos_en_espera = []
    acumulacion_ejecucion = []

    # Procesos pendientes de ser seleccionados para la ejecución
    procesos_pendientes = sorted(procesos, key=lambda x: x['llegada'])

    while procesos_pendientes:
        # Seleccionar los procesos que ya han llegado
        procesos_disponibles = [p for p in procesos_pendientes if p['llegada'] <= tiempo_actual]

        if procesos_disponibles:
            # Elegir el proceso con la ráfaga más corta
            proceso_a_ejecutar = min(procesos_disponibles, key=lambda x: x['ejecucion'])

            # Calcular tiempos para el proceso seleccionado
            tiempo_en_espera = tiempo_actual - proceso_a_ejecutar['llegada']
            tiempos_en_espera.append(tiempo_en_espera)
            
            tiempo_actual += proceso_a_ejecutar['ejecucion']
            tiempo_en_sistema = tiempo_actual - proceso_a_ejecutar['llegada']
            tiempos_en_sistema.append(tiempo_en_sistema)
            
            acumulacion_ejecucion.append(tiempo_actual)
            
            procesos_ordenados.append(proceso_a_ejecutar)
            procesos_pendientes.remove(proceso_a_ejecutar)
        else:
            # Si no hay procesos disponibles, adelantar el tiempo
            tiempo_actual = procesos_pendientes[0]['llegada']

    # Calcular promedios
    tiempo_espera_promedio = sum(tiempos_en_espera) / len(procesos)
    tiempo_promedio_sistema = sum(tiempos_en_sistema) / len(procesos)

    return procesos_ordenados, tiempos_en_sistema, tiempos_en_espera, tiempo_espera_promedio, tiempo_promedio_sistema, acumulacion_ejecucion

def mostrar_tabla(procesos, tiempos_en_sistema, tiempos_en_espera, tiempo_espera_promedio, tiempo_promedio_sistema):
    # Preparar los datos para la tabla
    tabla = []
    for i, proceso in enumerate(procesos):
        tabla.append([proceso['nombre'], proceso['ejecucion'], proceso['llegada'], tiempos_en_espera[i], tiempos_en_sistema[i]])

    # Definir los encabezados de la tabla
    encabezados = ['Proceso', 'Rafaga (CPU)', 'Tiempo de Llegada', 'Tiempo de Espera', 'Tiempo en Sistema']

    # Imprimir la tabla utilizando tabulate
    print(tabulate(tabla, headers=encabezados, tablefmt="fancy_grid", numalign="center", stralign="center"))

    # Mostrar los promedios
    print("\nTiempo promedio de espera: {:.2f}".format(tiempo_espera_promedio))
    print("Tiempo promedio en sistema: {:.2f}".format(tiempo_promedio_sistema))

def mostrar_grafico(procesos, acumulacion_ejecucion, tiempos_en_espera):
    # Crear el gráfico
    plt.figure(figsize=(10,8))

    # Dibujar lineas horizontales para cada proceso
    for i, proceso in enumerate(procesos):
        # Circulos para el inicio de llegada de cada Proceso
        plt.scatter(y=i, x=proceso['llegada'], color='blue', s=40, zorder=5)

        # Lineas de Tiempo de Espera
        plt.hlines(y=i, xmin=proceso['llegada'], xmax=acumulacion_ejecucion[i] - proceso['ejecucion'],  color='blue', linestyle='--', linewidth=2)
        
        # Lineas de Tiempo de Ejecucion
        plt.hlines(y=i, xmin=proceso['llegada'] + tiempos_en_espera[i], xmax=acumulacion_ejecucion[i],  color='red', linestyle='-', linewidth=2)


    # Configurar el eje x con números de uno en uno
    plt.xticks(range(0, int(max(acumulacion_ejecucion)) + 1, 1))

    # Configurar el gráfico
    plt.yticks(range(len(procesos)), [proceso['nombre'] for proceso in procesos])
    plt.title('Algoritmo SJF')
    plt.xlabel('Tiempo')
    plt.ylabel('Proceso')
    plt.grid(True)

    # Obtener el gestor de la figura
    manager = plt.get_current_fig_manager()

    # Establecer el título de la ventana gráfica
    manager.set_window_title('Gráfico del Algoritmo SJF')

    # Mostrar el gráfico
    plt.show()

def main_sjf():
    num_procesos = int(input("Ingrese el número de procesos: "))
    procesos = []

    for i in range(1, num_procesos + 1):
        nombre = input(f"Ingrese el nombre para el proceso {i}: ")
        ejecucion = int(input(f"Ingrese la Rafaga (CPU) para {nombre}: "))
        llegada = int(input(f"Ingrese el tiempo de llegada para {nombre}: "))
        procesos.append({'nombre': nombre, 'llegada': llegada, 'ejecucion': ejecucion})

    # Calcular tiempos con SJF
    procesos_ordenados, tiempos_en_sistema, tiempos_en_espera, tiempo_espera_promedio, tiempo_promedio_sistema, acumulacion_ejecucion = calcular_tiempos_sjf(procesos)

    # Mostrar la tabla de resultados
    mostrar_tabla(procesos_ordenados, tiempos_en_sistema, tiempos_en_espera, tiempo_espera_promedio, tiempo_promedio_sistema)

    # Mostrar el gráfico
    mostrar_grafico(procesos_ordenados, acumulacion_ejecucion, tiempos_en_espera)

if __name__ == "__main__":
    main_sjf()
