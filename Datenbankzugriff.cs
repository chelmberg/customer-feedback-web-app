using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace kundenfeedbackanwendung_varena
{
    public class Datenbankzugriff
    {
        public Datenbankzugriff()
        {

        }


        public static void NonQuery(string sql)
        {
            OdbcConnection connection = new OdbcConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sqlDB"].ConnectionString);
            
            OdbcCommand cmd = new OdbcCommand(sql, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public static int Scalar(string sql)
        {
            int rückgabe;
            OdbcConnection connection = new OdbcConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sqlDB"].ConnectionString);

            OdbcCommand cmd = new OdbcCommand(sql, connection);
            connection.Open();
            rückgabe = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();

            return rückgabe;
        }
        public static OdbcDataReader Reader(string sql)
        {
            OdbcDataReader reader;
            OdbcConnection connection = new OdbcConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sqlDB"].ConnectionString);

            OdbcCommand cmd = new OdbcCommand(sql, connection);
            connection.Open();
            reader = cmd.ExecuteReader();
            
            return reader;
        }
    }
}