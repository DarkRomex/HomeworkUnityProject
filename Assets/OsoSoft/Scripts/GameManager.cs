using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Datos del juego")]
    public int vidas = 3;
    public int monedas = 0;
    public int monedasNecesarias = 5;
    public float tiempo = 0f;

    [Header("Elementos que aparecen al juntar las 5 estrellas")]
    public GameObject rampaDeAcceso; 
    public GameObject cabezaMario; 

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

        // Si estamos en la escena principal, buscamos la rampa y la cabeza si están inactivas
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

            // Si ya se consiguieron las 5 monedas antes, nos aseguramos de que sigan visibles
            if (monedas >= monedasNecesarias)
            {
                if (rampaDeAcceso != null && !rampaDeAcceso.activeSelf) rampaDeAcceso.SetActive(true);
                if (cabezaMario != null && !cabezaMario.activeSelf) cabezaMario.SetActive(true);
            }
        }
    }

    public void AgregarMoneda(int cantidad)
    {
        monedas += cantidad;
        Debug.Log("Estrellas recogidas: " + monedas + " de " + monedasNecesarias);

        if (monedas >= monedasNecesarias)
        {
            Debug.Log("¡5 estrellas conseguidas! Activando rampa y cabeza de Mario.");
            if (rampaDeAcceso != null) rampaDeAcceso.SetActive(true);
            if (cabezaMario != null) cabezaMario.SetActive(true);
        }
    }

    public void PerderVida(int cantidad = 1)
    {
        vidas -= cantidad;
        if (vidas < 0) vidas = 0;
    }
}