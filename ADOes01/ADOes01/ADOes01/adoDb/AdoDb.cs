using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
/*eseguiamo inserimento di un cliente 
 * Fabio Rossi Fabio.Rossi@yahoo.it zonaId =3;
*INNER JOIN clienti con zone
 * 
 * select solo clienti con zona compresa tra 2 e 4
 * 
 * writeline(nome, cognome, 'nome della zona')*/
namespace ADOes01.adoDb
{
    internal class AdoDb
    {
        private string GetCon()
        {
            string con = "Data Source=LAPTOP_MARCO\\SQLEXPRESS;Initial Catalog=Negozio;Integrated Security=True;";
            //string con  = "Data Source=WIN10-2023-08-2;Initial Catalog=Negozio;Integrated Security=True;";
            return con;
        }

        public void ClientiJoin()
        {

            try
            {
                string query = "SELECT FROM Clienti INNER JOIN Zone ON Clienti.ZonaId = Zone.Id";

                using (SqlConnection connection = new SqlConnection(GetCon()))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string nome = reader["Nome"].ToString();
                        string cognome = reader["Cognome"].ToString();
                        string email = reader["email"].ToString();
                        //-- costruzione stringa da mandare in stampa
                        //string s = "Nome Cliente: " + nome + " Cognome: " + cognome;
                        string output = $"Nome Cliente: {nome} Cognome: {cognome} email: {email}";
                        Console.WriteLine(output);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex) { Console.WriteLine("aiut"); }
            
        }
        public void ClientiAdd(string nome, string cognome, string email,int zona)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetCon())){
                    connection.Open();
                    SqlTransaction tran = connection.BeginTransaction();
                    Console.WriteLine("Avvia Transaction");

                    try
                    {
                       
                            string query = $"INSERT INTO Clienti (nome, cognome, email, zonaID) VALUES ({nome},{cognome},{email},{zona.ToString()});";
                            Console.WriteLine(query);
                            SqlCommand cmd = new SqlCommand(query, connection);
                             cmd.Transaction =tran ;
                  
                   
                            tran.Commit();  
                    }
                    catch (SqlException ex) {
                        Console.WriteLine("NON HO POTUTO AGGIUNGERE");
                        tran.Rollback();
                    }
                }
            }
            catch (Exception ex) { }
        }
        public void ClientiUpdate()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetCon()))
                {
                    connection.Open();
                    SqlTransaction tr =connection.BeginTransaction();
                    Console.WriteLine("Avvia Transaction");
                    try
                    {
                        {
                            string query = "UPDATE Clienti Set Nome='Fausto' where ClienteID = 1";
                            SqlCommand cmd = new SqlCommand(query, connection);
                            int i = cmd.ExecuteNonQuery();
                        }

                        {
                            string query = "UPDATE Clienti Set ZonaID='14' where ClienteID = 4";
                            SqlCommand cmd = new SqlCommand(query, connection);
                            int i = cmd.ExecuteNonQuery();
                        }


                        {
                            string query = "UPDATE Clienti Set Nome='Ugo' where ClienteID = 3";
                            SqlCommand cmd = new SqlCommand(query, connection);
                            int i = cmd.ExecuteNonQuery();
                        }
                        tr.Commit();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Intervcetta errore, esegue Rollback");
                        tr.Rollback();

                    }


                }
            }
            catch (Exception e)
            {

                //throw;
            }

        }
        public void ClientiRead()
        {
            try
            {
                using (SqlConnection connection  =new SqlConnection(GetCon()))
                {
                    connection.Open();
                    string query = "SELECT nome,Cognome,EMail FROM Clienti WHERE Cognome >='G' ORDER BY Cognome";
                    
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                       SqlDataReader  reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string nome = reader["Nome"].ToString();
                            string cognome = reader["Cognome"].ToString();
                            string email = reader["email"].ToString();
                            //-- costruzione stringa da mandare in stampa
                            //string s = "Nome Cliente: " + nome + " Cognome: " + cognome;
                            string output = $"Nome Cliente: {nome} Cognome: {cognome} email: {email}";
                            Console.WriteLine(output);
                        }
                    }
                    
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        public void ClientiRead_Normale()
        {
            //* Creazione oggetto di Connessione
            SqlConnection con = new SqlConnection(GetCon());
            try
            {
                //* Apertura Connessione
                con.Open();
                /*
                 * codice per lettra dati du Db
                */

            }
            catch (Exception e )
            {

            }
            finally
            {
                con.Close();
            }
            
        }

        public void OrdiniJoin()
        {
            try { 
            

                using (SqlConnection connection = new SqlConnection(GetCon()))
                {
                    connection.Open();
                    string query = "SELECT C.Nome as Nome, C.Cognome as Cognome, p.NomeProdotto as Articolo, o.DatOrdine,o.QUantita, o.ImportoDiRigha FROM ORdini" +
                        "JOIN Clienti c ClienteId = c.ClienteId"+"JOIN Prodotti p ON ProdottoID = p.ProdottoId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string nome = reader["Nome"].ToString();
                            string cognome = reader["Cognome"].ToString();
                            string articolo = reader["Articolo"].ToString();
                            string dt = reader["DataOrdine"].ToString();
                            string qta = reader["Quantità"].ToString();
                            string importo = reader["ImportoDiRiga"].ToString();
                        }

                        reader.Close();
                    }
                }
                
            }
            catch (SqlException e)
            {
                Console.WriteLine("ERRORE");
            }
        }
    }
}
