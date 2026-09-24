using UnityEngine;

namespace logica_jugador
{
    public class Jugador : MonoBehaviour
    {
        public float velocidad = 3f;

        private Rigidbody2D rb;
        private float movimiento;

        public float alturaSalto = 4f;
        private bool esPiso;

        public Transform comprobadorPiso;
        public float radioComprobadorPiso = 0.1f;
        public LayerMask layerPiso;

        // Animator del personaje
        private Animator animator;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            movimiento = Input.GetAxisRaw("Horizontal");

            rb.linearVelocity = new Vector2(
                movimiento * velocidad,
                rb.linearVelocity.y
            );

            if (movimiento != 0)
            {
                transform.localScale = new Vector3(
                    Mathf.Sign(movimiento),
                    1,
                    1
                );
            }

            if (Input.GetButtonDown("Jump") && esPiso)
            {
                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    alturaSalto
                );
            }

            // Envía la velocidad horizontal al Animator
            animator.SetFloat(
                "Velocidad",
                Mathf.Abs(movimiento)
            );
            animator.SetFloat(
                  "VelocidadVertical",
                rb.linearVelocity.y
                  );
                  animator.SetBool(
               "estaEnPiso",
                    esPiso
                        );
        }

        void FixedUpdate()
        {
            esPiso = Physics2D.OverlapCircle(
                comprobadorPiso.position,
                radioComprobadorPiso,
                layerPiso
            );
        }
    }
}

public class Enemigo
{

}

namespace herramientas
{
    namespace calculos
    {
        using logica_jugador;

        public class Ejemplo
        {
            public void metodoEjemplo()
            {
                Jugador j;
            }
        }
    }

    namespace conectividad
    {
        public partial class herramientas
        {

        }
    }
}

namespace carla
{
    namespace test
    {
        using herramientas.calculos;

        public class Prueba
        {
            public void metodoTest()
            {
                Ejemplo e;
            }
        }
    }
}