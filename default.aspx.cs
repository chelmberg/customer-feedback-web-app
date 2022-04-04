using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace kundenfeedbackanwendung_varena
{
    public partial class _default : System.Web.UI.Page
    {
        OdbcConnection connection = new OdbcConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sqlDB"].ConnectionString);
        
        int fId;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT MAX(ID_Fragebogen) FROM Fragebogen";          
            fId = (Datenbankzugriff.Scalar(sql)) + 1;
            sql = "INSERT INTO Fragebogen(ID_Fragebogen, Geschlecht, Altersbereich, PLZ, PersonenHaushalt, BesuchIntervall, Bewertungsart, EigeneMeinung, VarenaBeste, Parkplatzangebot, Veranstaltungen, Kontaktaufnahme) VALUES('" + fId + "','" + 0 + "','" + 0 + "','" + 1063 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + "" + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "')";
            Datenbankzugriff.NonQuery(sql); //Methode in der klasse für Datenbankzugriff

            Response.Redirect("benutzerfragen.aspx?" + (Page.Session["fId"] = fId));
        }
    }
}