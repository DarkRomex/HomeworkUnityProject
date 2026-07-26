using UnityEngine;

public class ContadorMonedas : MonoBehaviour
{
    private int monedasRecogidas = 0;
    private int monedasNecesarias = 5;
    
    [Header("Elementos que aparecen al juntar las 5 estrellas")]
    public GameObject rampaDeAcceso; 
    public GameObject cabezaMario; // Arrastra aquí el objeto de la cabeza de Mario

    public void SumarMoneda()
    {
        monedasRecogidas++;
        Debug.Log("Estrellas recogidas: " + monedasRecogidas + " de " + monedasNecesarias);

        if (monedasRecogidas >= monedasNecesarias)
        {
            Debug.Log("¡5 estrellas conseguidas! Activando rampa y cabeza de Mario.");
            
            // Aparecen ambos objetos de golpe
            if (rampaDeAcceso != null) rampaDeAcceso.SetActive(true);
            if (cabezaMario != null) cabezaMario.SetActive(true);
        }
    }
}