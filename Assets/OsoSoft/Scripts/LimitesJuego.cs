using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LimitesJuego : MonoBehaviour
{
    [Header("Límites del Plano")]
    public float minX = -4.5f;
    public float maxX = 4.5f;
    public float minZ = -4.5f;
    public float maxZ = 4.5f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Obtenemos la posición actual desde el Rigidbody
        Vector3 posicionActual = rb.position;

        // Limitamos los valores en X y Z
        float xLimitada = Mathf.Clamp(posicionActual.x, minX, maxX);
        float zLimitada = Mathf.Clamp(posicionActual.z, minZ, maxZ);

        // Si la esfera intenta salir del límite, la retenemos de forma física
        if (posicionActual.x != xLimitada || posicionActual.z != zLimitada)
        {
            Vector3 nuevaPosicion = new Vector3(xLimitada, posicionActual.y, zLimitada);
            rb.MovePosition(nuevaPosicion);
        }
    }
}