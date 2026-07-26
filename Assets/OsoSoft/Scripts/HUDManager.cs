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

        textoVidas.text = "Vidas: " + GameManager.instancia.vidas;
        textoMonedas.text = "Monedas: " + GameManager.instancia.monedas;

        float t = GameManager.instancia.tiempo;
        int minutos = Mathf.FloorToInt(t / 60);
        int segundos = Mathf.FloorToInt(t % 60);

        textoTiempo.text = "Tiempo: " + minutos.ToString("00") + ":" + segundos.ToString("00");
    }
}