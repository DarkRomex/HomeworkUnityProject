using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 6f;
    public float velocidadGiro = 360f;
    public float fuerzaSalto = 0f;

    [Header("Suelo")]
    public LayerMask capaSuelo;
    public float distanciaSuelo = 1.2f;

    [Header("Corrección visual")]
    public float offsetRotacionVisual = -90f;

    private Rigidbody rb;
    private Vector3 direccion;
    private bool tocarSuelo;
    private bool pedirSalto;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.useGravity = true;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        float x = 0f;
        float z = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x = 1f;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) z = 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) z = -1f;

        direccion = new Vector3(x, 0f, z).normalized;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            pedirSalto = true;
    }

    void FixedUpdate()
    {
        Vector3 origenRaycast = transform.position + Vector3.up * 0.2f;
        tocarSuelo = Physics.Raycast(origenRaycast, Vector3.down, distanciaSuelo, capaSuelo);

        Vector3 velocidadActual = rb.linearVelocity;
        Vector3 nuevaVelocidad = new Vector3(
            direccion.x * velocidad,
            velocidadActual.y,
            direccion.z * velocidad
        );

        rb.linearVelocity = nuevaVelocidad;

        if (direccion != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion, Vector3.up);
            rotacionObjetivo *= Quaternion.Euler(0f, offsetRotacionVisual, 0f);

            Quaternion nuevaRotacion = Quaternion.RotateTowards(
                rb.rotation,
                rotacionObjetivo,
                velocidadGiro * Time.fixedDeltaTime
            );

            rb.MoveRotation(nuevaRotacion);
        }

        if (pedirSalto && tocarSuelo && fuerzaSalto > 0f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }

        pedirSalto = false;
    }
}