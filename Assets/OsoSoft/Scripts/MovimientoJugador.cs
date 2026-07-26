using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 90f;     // ¡Velocidad extrema que tú quieres!
    public float fuerzaSalto = 8f;

    private Rigidbody rb;
    private bool enElSuelo;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = false; 
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; 
        rb.interpolation = RigidbodyInterpolation.Interpolate; 
    }

    void FixedUpdate()
    {
        if (Keyboard.current == null) return;

        // --- Detección de teclas ---
        float movimientoX = 0f;
        float movimientoZ = 0f;

        if (Keyboard.current.aKey.isPressed) movimientoX = -1f;
        if (Keyboard.current.dKey.isPressed) movimientoX = 1f;
        if (Keyboard.current.wKey.isPressed) movimientoZ = 1f;
        if (Keyboard.current.sKey.isPressed) movimientoZ = -1f;

        Vector3 inputDir = new Vector3(movimientoX, 0f, movimientoZ).normalized;

        // --- Raycast para detectar la rampa o el suelo debajo ---
        RaycastHit hit;
        bool tocandoSuelo = false;
        Vector3 direccionEnPendiente = inputDir;

        // Lanzamos un rayo hacia abajo desde la esfera para ver la inclinación de la rampa
        if (Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down, out hit, 1.5f))
        {
            if (hit.collider.gameObject != gameObject)
            {
                tocandoSuelo = true;
                // Proyectamos el movimiento sobre el plano de la rampa para que no despegue
                direccionEnPendiente = Vector3.ProjectOnPlane(inputDir, hit.normal);
            }
        }

        Vector3 velocidadDeseada = direccionEnPendiente * velocidad;

        if (tocandoSuelo)
        {
            rb.useGravity = false; // Desactivamos gravedad en la rampa para evitar tirones
            velocidadDeseada.y -= 10f; // Fuerza constante hacia abajo para mantenerla pegada
        }
        else
        {
            rb.useGravity = true; // Gravedad normal si salta o está en el aire
            velocidadDeseada.y = rb.linearVelocity.y;
        }

        rb.linearVelocity = velocidadDeseada;

        // --- Salto con Espacio ---
        if (Keyboard.current.spaceKey.wasPressedThisFrame && tocandoSuelo)
        {
            rb.useGravity = true;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }
}