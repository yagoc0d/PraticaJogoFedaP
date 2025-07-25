
using System.IO;
namespace TrabalhoPratico.Data
{
    public class Log
    {
        private static string caminhoLog = "log_partida.txt";

        public static void Registrar(string mensagem)
        {
            using (StreamWriter writer = new StreamWriter(caminhoLog, true))
            {
                writer.WriteLine(mensagem);
            }
        }

        public static void LimparLog()
        {
            File.WriteAllText(caminhoLog, "");
        }
    }

}
