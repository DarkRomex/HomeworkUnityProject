using UnityEngine;

public class ContadorMonedas : MonoBehaviour
{
    private int monedasRecogidas = 0;
    private int monedasNecesarias = 2;
    private bool misionCumplida = false;

    public void SumarMoneda()
    {
        monedasRecogidas++;

        Debug.Log(
            "Monedas recogidas: "
            + monedasRecogidas
            + " de "
            + monedasNecesarias
        );

        if (monedasRecogidas >= monedasNecesarias &&
            misionCumplida == false)
        {
            misionCumplida = true;

            Debug.Log("¡MISIÓN CUMPLIDA!");
        }
    }
}