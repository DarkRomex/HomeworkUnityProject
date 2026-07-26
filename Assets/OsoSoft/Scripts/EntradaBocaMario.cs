using UnityEngine;

public class EntradaBocaMario : MonoBehaviour
{
    [Header("Nombre de la escena interior")]
    public string nombreEscenaInterior = "MundoInterior";

    private bool entrando = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !entrando)
        {
            entrando = true;
            Debug.Log("¡Entrando a la boca de Mario! Viajando al mundo interior...");

            if (AudioManager.instancia != null)
            {
                AudioManager.instancia.CambiarEscenaConSonido(nombreEscenaInterior);
            }
        }
    }
}