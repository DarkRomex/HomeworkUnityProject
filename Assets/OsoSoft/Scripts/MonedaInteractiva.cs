using UnityEngine;
using System.Collections;

public class MonedaInteractiva : MonoBehaviour
{
    [Header("Configuración de Giro")]
    public float velocidadGiro = 100f;
    public Vector3 ejeGiro = Vector3.up;

    [Header("Recompensa")]
    public int valorMoneda = 1;

    [Header("Animación")]
    public float duracionAnimacion = 0.25f;
    public float alturaSubida = 1.5f;

    private bool recolectada = false;
    private Collider miCollider;

    void Awake()
    {
        miCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (!recolectada)
        {
            transform.Rotate(ejeGiro * velocidadGiro * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || recolectada)
            return;

        recolectada = true;

        if (miCollider != null)
            miCollider.enabled = false;

        if (GameManager.instancia != null)
            GameManager.instancia.AgregarMoneda(valorMoneda);

        if (AudioManager.instancia != null)
            AudioManager.instancia.ReproducirMoneda();

        StartCoroutine(AnimacionRecoleccion());
    }

    IEnumerator AnimacionRecoleccion()
    {
        float tiempo = 0f;
        Vector3 escalaInicial = transform.localScale;
        Vector3 posicionInicial = transform.position;

        while (tiempo < duracionAnimacion)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracionAnimacion;

            transform.position = posicionInicial + Vector3.up * (progreso * alturaSubida);
            transform.localScale = Vector3.Lerp(escalaInicial, Vector3.zero, progreso);

            yield return null;
        }

        Destroy(gameObject);
    }
}