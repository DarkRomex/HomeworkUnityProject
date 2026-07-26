using UnityEngine;

public class CambioEscena : MonoBehaviour
{
    public string nombreEscena;

    public void IrAEscena()
    {
        if (AudioManager.instancia != null)
            AudioManager.instancia.CambiarEscenaConSonido(nombreEscena);
    }
}