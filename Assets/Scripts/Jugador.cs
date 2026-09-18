using UnityEngine;

namespace logica_jugador
{
    public class Jugador : MonoBehaviour
    {
       
        void Start()
        {

        }

        void Update()
        {

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
