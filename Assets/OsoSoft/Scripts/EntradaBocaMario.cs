using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class EntradaBocaMario : MonoBehaviour
{
    [Header("Nombre de la escena interior")]
    public string nombreEscenaInterior = "MundoInterior"; // Escribe aquí el nombre exacto de tu escena

    private void OnTriggerEnter(Collider other)
    {
        // Solo se activa si el que entra es el Jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Entrando a la boca de Mario! Viajando al mundo interior...");
            
            // Carga la nueva escena
            SceneManager.LoadScene(nombreEscenaInterior);
        }
    }
}