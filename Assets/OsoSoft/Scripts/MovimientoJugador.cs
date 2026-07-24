using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{public float velocidad = 5f;
    private Rigidbody rb;
    private Transform camaraPrincipal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camaraPrincipal = Camera.main.transform;
    }

    void Update()
    {
        float moverH = Input.GetAxisRaw("Horizontal");
        float moverV = Input.GetAxisRaw("Vertical");

        Vector3 adelante = camaraPrincipal.forward;
        Vector3 derecha = camaraPrincipal.right;

        adelante.y = 0f;
        derecha.y = 0f;

        adelante.Normalize();
        derecha.Normalize();

        Vector3 direccion = (adelante * moverV + derecha * moverH).normalized;
        Vector3 nuevaPosicion = rb.position + direccion * velocidad * Time.deltaTime;
        
        rb.MovePosition(nuevaPosicion);
    }
}
