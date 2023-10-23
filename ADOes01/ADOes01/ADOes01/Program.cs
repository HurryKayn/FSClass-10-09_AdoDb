using ADOes01.adoDb;

namespace ADOes01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AdoDb ado = new AdoDb();
            //ado.ClientiRead();
            Console.WriteLine("*****************************************************");
           // ado.ClientiUpdate();
            //Console.WriteLine("*****************************************************");
            //ado.ClientiRead();
            //Console.WriteLine();
            //ado.OrdiniJoin();
            //ado.ClientiAdd("Fabio","Rossi","Fabio.Rossi@yahoo.it", 3);
            Console.WriteLine("Applicazione Terminata");
            Console.WriteLine("*****************************************************");

            //ado.ClientiRead();


        }
    }
}