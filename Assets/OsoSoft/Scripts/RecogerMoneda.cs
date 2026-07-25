using UnityEngine;

public class RecogerMoneda : MonoBehaviour
{
    private void OnTriggerEnter(Collider otroObjeto)
    {
        if (otroObjeto.CompareTag("Player"))
        {
            ContadorMonedas contador =
                FindFirstObjectByType<ContadorMonedas>();

            if (contador != null)
            {
                contador.SumarMoneda();
            }

            Destroy(gameObject);
        }
    }
}