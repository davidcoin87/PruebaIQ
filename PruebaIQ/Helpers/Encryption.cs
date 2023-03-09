using System.Text;

namespace PruebaIQ.Helpers
{
    public class Encryption
    {
        public static string Encrypt(string palabra)
        {
            int seed = GenerateSeed();
            StringBuilder sb = new StringBuilder();

            char letraInicial = (char)(seed + 50);
            char letrraFinal = (char)GenerateSeed2();

            sb.Append(letraInicial);
            for (int i = palabra.Length - 1; i >= 0; i--)
            {
                char letra = palabra[i];
                int codigoLetra = (int)letra;
                int nuevoCodigo = codigoLetra + seed;
                if (nuevoCodigo == 92)
                {// BACK SLASH \
                    nuevoCodigo = 123;// {
                }
                sb.Append((char)(nuevoCodigo));
            }
            sb.Append(letrraFinal);
            return sb.ToString();
        }

        public static string Decrypt(string encripcion)
        {
            char seedChar = (char)encripcion[0];

            int seed = (int)seedChar - 50;

            encripcion = encripcion.Substring(1, encripcion.Length - 1);
            StringBuilder sb = new StringBuilder();
            for (int i = encripcion.Length - 1; i >= 0; i--)
            {
                char letra = (char)encripcion[i];
                int codigoLetra = (int)letra;
                if (codigoLetra == 123)
                {
                    codigoLetra = 92;
                }
                int nuevoCodigoLetra = codigoLetra - seed;
                sb.Append((char)(nuevoCodigoLetra));
            }
            return sb.ToString().Substring(1, sb.ToString().Length - 1);
        }

        static int GenerateSeed()
        {
            int min = 10;
            int max = 34;
            Random rnd = new Random();
            int s = min + (rnd.Next(1, (max - min)));
            return s;
        }

        // Genera un número aleatorio en el rango [48, 91]
        static int GenerateSeed2()
        {
            int min = 48;
            int max = 91;
            Random rnd = new Random();
            int s = min + (rnd.Next(1, (max - min)));
            return s;
        }
    }
}
