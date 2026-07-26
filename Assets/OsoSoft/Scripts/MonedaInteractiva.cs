using UnityEngine;
using System.Collections;

public class MonedaInteractiva : MonoBehaviour
{
    [Header("Configuración de Giro")]
    public float velocidadGiro = 100f;
    public Vector3 ejeGiro = Vector3.up;

    private bool recolectada = false;
    private Collider miCollider;
    private Renderer[] renderersMoneda;

    void Awake()
    {
        miCollider = GetComponent<Collider>();
        renderersMoneda = GetComponentsInChildren<Renderer>();
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
        if (other.CompareTag("Player") && !recolectada)
        {
            recolectada = true;

            if (AudioManager.instancia != null)
                AudioManager.instancia.ReproducirMoneda();

            StartCoroutine(AnimacionRecoleccion());
        }
    }

    IEnumerator AnimacionRecoleccion()
    {
        float tiempo = 0f;
        float duracionAnimacion = 0.25f;
        Vector3 escalaInicial = transform.localScale;
        Vector3 posicionInicial = transform.position;

        if (miCollider != null)
            miCollider.enabled = false;

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