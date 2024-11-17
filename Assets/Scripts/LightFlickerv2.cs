using UnityEngine;

public class LightFlicker2 : MonoBehaviour
{
    public Light luz;                     // La luz que se va a controlar
    public Renderer objetoRenderer;        // El renderer del objeto con el material que tiene emisión
    public float tiempoMinimo = 0.1f;     // Tiempo mínimo de encendido/apagado
    public float tiempoMaximo = 1f;       // Tiempo máximo de encendido/apagado
    public Color colorEmisionEncendido = Color.white;  // Color de emisión cuando la luz está encendida
    public Color colorEmisionApagado = Color.black;    // Color de emisión cuando la luz está apagada
    public float intensidadMaxima = 5f;  // Intensidad máxima de emisión cuando la luz está encendida
    public float intensidadMinima = 0f;  // Intensidad mínima de emisión cuando la luz está apagada

    private float timer = 0f;             // Temporizador interno
    private float tiempoEncendido = 0f;   // Tiempo aleatorio de encendido
    private float tiempoApagado = 0f;     // Tiempo aleatorio de apagado
    private bool luzEncendida = true;     // Controla si la luz está encendida o apagada
    private Material material;            // Material del objeto para modificar la emisión

    private void Start()
    {
        if (luz == null)
        {
            luz = GetComponent<Light>();  // Si no se asignó una luz en el inspector, la obtenemos del objeto.
        }

        if (objetoRenderer == null)
        {
            objetoRenderer = GetComponent<Renderer>();  // Si no se asignó un Renderer, lo obtenemos automáticamente
        }

        material = objetoRenderer.material;  // Obtenemos el material del Renderer

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
            AdjustMaterialEmission(intensidadMinima, colorEmisionApagado);  // Reducimos la emisión del material
            luzEncendida = false;
            timer = 0f;  // Reiniciamos el temporizador
            AsignarTiemposAleatorios();  // Generamos nuevos tiempos aleatorios
        }
        else if (!luzEncendida && timer >= tiempoApagado)
        {
            luz.enabled = true;   // Encendemos la luz
            AdjustMaterialEmission(intensidadMaxima, colorEmisionEncendido);  // Aumentamos la emisión del material
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

    // Función para ajustar la emisión del material
    private void AdjustMaterialEmission(float intensidad, Color color)
    {
        // Asegurarnos de que el material tiene una propiedad de emisión
        if (material.HasProperty("_EmissionColor"))
        {
            material.SetColor("_EmissionColor", color * intensidad);  // Ajustamos la emisión multiplicando por la intensidad
            DynamicGI.SetEmissive(objetoRenderer, color * intensidad);       // Esto asegura que la iluminación global se actualice
        }
    }
}
