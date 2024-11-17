using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] Light luz;             // La luz que se va a controlar
    [SerializeField] float tiempoMinimo = 0.1f;  // Tiempo mínimo de encendido/apagado
    [SerializeField] float tiempoMaximo = 1f;    // Tiempo máximo de encendido/apagado

    [SerializeField] float timer = 0f;              // Temporizador interno
    [SerializeField] float tiempoEncendido = 0f;   // Tiempo aleatorio de encendido
    [SerializeField] float tiempoApagado = 0f;     // Tiempo aleatorio de apagado
    [SerializeField] bool luzEncendida = true;     // Controla si la luz está encendida o apagada

    private void Start()
    {
        if (luz == null)
        {
            luz = GetComponent<Light>();   // Si no se asignó una luz en el inspector, la obtenemos del objeto.
        }

        // Generamos los tiempos aleatorios de encendido y apagado al inicio
        AsignarTiemposAleatorios();
    }

    private void Update()
    {
        // Actualizamos el temporizador
        timer += Time.deltaTime;

        // Si el temporizador ha superado el tiempo de encendido o apagado, alternamos el estado de la luz
        if (luzEncendida && timer >= tiempoEncendido)
        {
            luz.enabled = false;  // Apagamos la luz
            luzEncendida = false;
            timer = 0f;  // Reiniciamos el temporizador
            AsignarTiemposAleatorios();  // Generamos nuevos tiempos aleatorios
        }
        else if (!luzEncendida && timer >= tiempoApagado)
        {
            luz.enabled = true;   // Encendemos la luz
            luzEncendida = true;
            timer = 0f;  // Reiniciamos el temporizador
            AsignarTiemposAleatorios();  // Generamos nuevos tiempos aleatorios
        }
    }

    // Función para asignar tiempos aleatorios
    private void AsignarTiemposAleatorios()
    {
        tiempoEncendido = Random.Range(tiempoMinimo, tiempoMaximo);
        tiempoApagado = Random.Range(tiempoMinimo, tiempoMaximo);
    }
}
