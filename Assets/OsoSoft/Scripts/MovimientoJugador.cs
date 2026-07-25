using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;
    public Transform puntoSuelo;
    public float radioChequeoSuelo = 0.2f;
    public LayerMask capaSuelo;

    private Rigidbody rb;
    private bool enElSuelo;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // evita que el objeto se caiga de costado
    }

    void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        // --- Movimiento horizontal ---
        float movimientoX = 0f;
        float movimientoZ = 0f;

        if (Keyboard.current.aKey.isPressed) movimientoX = -1f;
        if (Keyboard.current.dKey.isPressed) movimientoX = 1f;
        if (Keyboard.current.wKey.isPressed) movimientoZ = 1f;
        if (Keyboard.current.sKey.isPressed) movimientoZ = -1f;

        Vector3 movimiento = new Vector3(movimientoX, 0f, movimientoZ).normalized;
        transform.Translate(movimiento * velocidad * Time.deltaTime, Space.World);

        // --- Chequeo de suelo ---
        Vector3 origen = puntoSuelo != null ? puntoSuelo.position : transform.position;
        enElSuelo = Physics.CheckSphere(origen, radioChequeoSuelo, capaSuelo);

        // --- Salto con Espacio ---
        if (Keyboard.current.spaceKey.wasPressedThisFrame && enElSuelo)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }
}