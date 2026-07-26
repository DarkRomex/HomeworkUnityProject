using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Datos del juego")]
    public int vidas = 3;
    public int monedas = 0;
    public int monedasRampa = 5; 
    
    // Si son 5 en el primer nivel y 24 en el segundo, el total para ganar es 29.
    public int monedasVictoria = 29; 
    public float tiempo = 0f;

    [Header("Elementos Escena 1")]
    public GameObject rampaDeAcceso; 
    public GameObject cabezaMario; 

    [Header("Final (Victoria)")]
    public GameObject mensajeFelicidades; 
    public GameObject[] elementosHUD; // Arrastra aquí TextoVidas, TextoMonedas y TextoTiempo

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        tiempo += Time.deltaTime;

        if (SceneManager.GetActiveScene().name == "MarioGame")
        {
            if (rampaDeAcceso == null)
            {
                GameObject rampa = GameObject.Find("RampaAcceso");
                if (rampa != null) rampaDeAcceso = rampa;
            }
            if (cabezaMario == null)
            {
                GameObject cabeza = GameObject.Find("cabeza_mario");
                if (cabeza != null) cabezaMario = cabeza;
            }

            if (monedas >= monedasRampa)
            {
                if (rampaDeAcceso != null && !rampaDeAcceso.activeSelf) rampaDeAcceso.SetActive(true);
                if (cabezaMario != null && !cabezaMario.activeSelf) cabezaMario.SetActive(true);
            }
        }
    }

    public void AgregarMoneda(int cantidad)
    {
        monedas += cantidad;
        Debug.Log("Objetos recogidos: " + monedas); 

        if (monedas == monedasRampa)
        {
            if (rampaDeAcceso != null) rampaDeAcceso.SetActive(true);
            if (cabezaMario != null) cabezaMario.SetActive(true);
        }

        if (monedas >= monedasVictoria)
        {
            Debug.Log("¡Misión completada!"); 
            
            // 1. Mostrar mensaje gigante
            if (mensajeFelicidades != null) mensajeFelicidades.SetActive(true);
            
            // 2. Ocultar los textos del HUD
            foreach (GameObject hudElement in elementosHUD)
            {
                if (hudElement != null) hudElement.SetActive(false);
            }
        }
    }

    public void PerderVida(int cantidad = 1)
    {
        vidas -= cantidad;
        if (vidas < 0) vidas = 0;
    }
}