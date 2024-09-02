import matplotlib.pyplot as plt
from tabulate import tabulate

def calcular_tiempos_prioridad(procesos):
    tiempo_ejecucion_acumulado = 0
    tiempos_en_sistema = []
    tiempos_en_espera = []
    acumulacion_ejecucion = []  # Lista para almacenar la acumulación de tiempo en ejecución
    procesos_ordenados = []

    while procesos:
        # Filtrar los procesos que ya llegaron
        procesos_disponibles = [proceso for proceso in procesos if proceso['llegada'] <= tiempo_ejecucion_acumulado]
        
        if not procesos_disponibles:
            # Si no hay procesos disponibles, avanzar el tiempo hasta el próximo proceso
            tiempo_ejecucion_acumulado = min(procesos, key=lambda x: x['llegada'])['llegada']
            continue

        # Ordenar los procesos disponibles por prioridad (y por tiempo de llegada en caso de empate)
        procesos_disponibles.sort(key=lambda x: (x['prioridad'], x['llegada']))

        # Seleccionar el siguiente proceso a ejecutar
        proceso = procesos_disponibles[0]
        procesos_ordenados.append(proceso)
        procesos.remove(proceso)

        # Calcular el tiempo de ejecución acumulado
        tiempo_ejecucion_acumulado += proceso['ejecucion']
        acumulacion_ejecucion.append(tiempo_ejecucion_acumulado)
        
        # Calcular el tiempo en sistema y de espera
        tiempo_en_sistema = tiempo_ejecucion_acumulado - proceso['llegada']
        tiempos_en_sistema.append(tiempo_en_sistema)
        
        tiempo_en_espera = tiempo_en_sistema - proceso['ejecucion']
        tiempos_en_espera.append(tiempo_en_espera)
    
    # Calcular el tiempo de espera promedio
    tiempo_espera_promedio = sum(tiempos_en_espera) / len(procesos_ordenados)

    # Calcular el tiempo promedio total en el sistema
    tiempo_promedio_sistema = sum(tiempos_en_sistema) / len(procesos_ordenados)

    return procesos_ordenados, tiempos_en_sistema, tiempos_en_espera, tiempo_espera_promedio, tiempo_promedio_sistema, acumulacion_ejecucion

def mostrar_tabla(procesos, tiempos_en_sistema, tiempos_en_espera, tiempo_espera_promedio, tiempo_promedio_sistema):
    # Preparar los datos para la tabla
    tabla = []
    for i, proceso in enumerate(procesos):
        tabla.append([proceso['prioridad'], proceso['nombre'], proceso['ejecucion'], proceso['llegada'], tiempos_en_espera[i], tiempos_en_sistema[i]])

    # Definir los encabezados de la tabla
    encabezados = ['Prioridad', 'Proceso', 'Ráfaga (CPU)', 'Tiempo de Llegada', 'Tiempo de Espera', 'Tiempo en Sistema']

    # Imprimir la tabla utilizando tabulate
    print(tabulate(tabla, headers=encabezados, tablefmt="fancy_grid", numalign="center", stralign="center"))

    # Mostrar los promedios
    print("\nTiempo promedio de espera: {:.2f}".format(tiempo_espera_promedio))
    print("Tiempo promedio en sistema: {:.2f}".format(tiempo_promedio_sistema))

def mostrar_grafico(procesos, acumulacion_ejecucion, tiempos_en_espera):
    # Crear el gráfico
    plt.figure(figsize=(10,8))

    # Dibujar líneas horizontales para cada proceso
    for i, proceso in enumerate(procesos):
        # Círculos para el inicio de llegada de cada proceso
        plt.scatter(y=i, x=proceso['llegada'], color='blue', s=40, zorder=5)

        # Líneas de tiempo de espera
        plt.hlines(y=i, xmin=proceso['llegada'], xmax=acumulacion_ejecucion[i] - proceso['ejecucion'], color='blue', linestyle='--', linewidth=2)
        
        # Líneas de tiempo de ejecución
        plt.hlines(y=i, xmin=proceso['llegada'] + tiempos_en_espera[i], xmax=acumulacion_ejecucion[i], color='red', linestyle='-', linewidth=2)

    # Configurar el eje x con números de uno en uno
    plt.xticks(range(0, int(max(acumulacion_ejecucion)) + 1, 1))

    # Configurar el gráfico
    plt.yticks(range(len(procesos)), [proceso['nombre'] for proceso in procesos])
    plt.title('Algoritmo de Prioridad')
    plt.xlabel('Tiempo')
    plt.ylabel('Proceso')
    plt.grid(True)

    # Obtener el gestor de la figura
    manager = plt.get_current_fig_manager()

    # Establecer el título de la ventana gráfica
    manager.set_window_title('Gráfico del Algoritmo de Prioridad')

    # Mostrar el gráfico
    plt.show()

def main_prioridad():
    # Solicitar al usuario el número de procesos
    num_procesos = int(input("Ingrese el número de procesos: "))
    procesos = []

    # Solicitar detalles para cada proceso (nombre, llegada, ejecución, prioridad)
    for i in range(1, num_procesos + 1):
        nombre = input(f"Ingrese el nombre para el proceso {i}: ")
        ejecucion = int(input(f"Ingrese la Ráfaga (CPU) para {nombre}: "))
        llegada = int(input(f"Ingrese el tiempo de llegada para {nombre}: "))
        prioridad = int(input(f"Ingrese la prioridad para {nombre}: "))
        procesos.append({'nombre': nombre, 'llegada': llegada, 'ejecucion': ejecucion, 'prioridad': prioridad})

    # Calcular tiempos y mostrar la tabla
    procesos_ordenados, tiempos_en_sistema, tiempos_en_espera, tiempo_espera_promedio, tiempo_promedio_sistema, acumulacion_ejecucion = calcular_tiempos_prioridad(procesos)

    # Mostrar la tabla de resultados
    mostrar_tabla(procesos_ordenados, tiempos_en_sistema, tiempos_en_espera, tiempo_espera_promedio, tiempo_promedio_sistema)

    # Mostrar el gráfico
    mostrar_grafico(procesos_ordenados, acumulacion_ejecucion, tiempos_en_espera)

if __name__ == "__main__":
    main_prioridad()
