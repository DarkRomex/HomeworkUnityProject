using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI textoVidas;
    public TextMeshProUGUI textoMonedas;
    public TextMeshProUGUI textoTiempo;

    private void Update()
    {
        if (GameManager.instancia == null) return;

        // NUEVO: Si llegamos a la meta, apagamos los textos para que no estorben
        if (GameManager.instancia.monedas >= GameManager.instancia.monedasVictoria)
        {
            textoVidas.gameObject.SetActive(false);
            textoMonedas.gameObject.SetActive(false);
            textoTiempo.gameObject.SetActive(false);
            return; // Detiene el contador
        }

        textoVidas.text = "Vidas: " + GameManager.instancia.vidas;
        textoMonedas.text = "Monedas: " + GameManager.instancia.monedas;

        float t = GameManager.instancia.tiempo;
        int minutos = Mathf.FloorToInt(t / 60);
        int segundos = Mathf.FloorToInt(t % 60);

        textoTiempo.text = "Tiempo: " + minutos.ToString("00") + ":" + segundos.ToString("00");
    }
}