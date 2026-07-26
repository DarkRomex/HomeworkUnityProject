using UnityEngine;
using System.Collections;

public class MonedaInteractiva : MonoBehaviour
{
    [Header("Configuración de Giro")]
    public float velocidadGiro = 100f;
    public Vector3 ejeGiro = Vector3.up;

    private bool recolectada = false;

    void Update()
    {
        if (!recolectada)
        {
            // Hace que la moneda dé vueltas constantemente sobre su eje
            transform.Rotate(ejeGiro * velocidadGiro * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Detecta cuando el jugador toca la moneda
        if (other.CompareTag("Player") && !recolectada)
        {
            recolectada = true;
            StartCoroutine(AnimacionRecoleccion());
        }
    }

    IEnumerator AnimacionRecoleccion()
    {
        float tiempo = 0f;
        float duracionAnimacion = 0.25f;
        Vector3 escalaInicial = transform.localScale;
        Vector3 posicionInicial = transform.position;

        // Animación suave: se eleva un poco y se encoge hasta desaparecer
        while (tiempo < duracionAnimacion)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracionAnimacion;

            transform.position = posicionInicial + Vector3.up * (progreso * 1.5f);
            transform.localScale = Vector3.Lerp(escalaInicial, Vector3.zero, progreso);

            yield return null;
        }

        Destroy(gameObject);
    }
}

