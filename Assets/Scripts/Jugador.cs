using UnityEngine;

namespace logica_jugador
{
    public class Jugador : MonoBehaviour
    {
        public float velocidad = 3f;

        private Rigidbody2D rb;
        private float movimiento;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
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
