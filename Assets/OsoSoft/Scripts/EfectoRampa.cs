using UnityEngine;

public class EfectoRampa : MonoBehaviour
{
    [Header("Configuración de Escala")]
    public float escalaPequena = 0.3f; // El tamaño que tendrá al final de la rampa (dentro de la boca)
    private Vector3 escalaOriginal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Guardamos el tamaño normal por si acaso
            escalaOriginal = other.transform.localScale;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Calculamos el progreso según la altura (Y) que va subiendo la esfera en la rampa
            // Ajusta el valor '0f' (altura inicial) y '3f' (altura final aproximada de la rampa) según tu escena
            float alturaActual = other.transform.position.y;
            float progreso = Mathf.InverseLerp(0f, 3f, alturaActual);

            // Achicamos la esfera progresivamente
            other.transform.localScale = Vector3.Lerp(escalaOriginal, Vector3.one * escalaPequena, progreso);
        }
    }
}