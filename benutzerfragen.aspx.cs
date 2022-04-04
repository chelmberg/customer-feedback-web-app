using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Data;

namespace kundenfeedbackanwendung_varena
{
    public partial class benutzerfragen : System.Web.UI.Page
    {
        OdbcConnection connection = new OdbcConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sqlDB"].ConnectionString);
        int idFrag;
        int wertAlter, wertHaushalt, wertIntervall;
        char wertGeschl;
        string sql;
        Datenbankzugriff db = new Datenbankzugriff();

        protected void Page_Load(object sender, EventArgs e)
        {
            string oldSelection = Convert.ToString(Page.Session["oldSelection"]);
            idFrag = Convert.ToInt32(Page.Session["fId"]);

            if (Page.IsPostBack == false)
            {      
                sql = "SELECT * FROM Fragebogen WHERE ID_Fragebogen='" + idFrag + "'";
                OdbcDataReader dr;
                dr = Datenbankzugriff.Reader(sql);

                if (dr.Read())
                {
                    wertGeschl = Convert.ToChar(dr["Geschlecht"]);
                    wertAlter = Convert.ToInt32(dr["Altersbereich"]);
                    wertHaushalt = Convert.ToInt32(dr["PersonenHaushalt"]);
                    wertIntervall = Convert.ToInt32(dr["BesuchIntervall"]);
                    
                    txtPLZ.Text = Convert.ToString(dr["PLZ"]);
                    if (txtPLZ.Text == "1063")
                    {
                        txtPLZ.Text = "";
                    }

                    if (wertGeschl == 'm') rblGeschlecht.Items[0].Selected = true;
                    else if (wertGeschl == 'w') rblGeschlecht.Items[1].Selected = true;

                    if (wertAlter > 0) rblAlter.Items[Convert.ToInt32(dr["Altersbereich"]) - 1].Selected = true;
                    if (wertHaushalt > 0) rblHaushalt.Items[Convert.ToInt32(dr["PersonenHaushalt"]) - 1].Selected = true;
                    if (wertIntervall > 0) rblIntervall.Items[Convert.ToInt32(dr["BesuchIntervall"]) - 1].Selected = true;

                    ddAuswahl.SelectedValue = oldSelection;
                }
                dr.Close();
                connection.Close();
            }
        }

        protected void btnZurueck_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        protected void btnWeiter_Click(object sender, EventArgs e)
        {
            string oldSelection = Convert.ToString(Page.Session["oldSelection"]);
            int go, hilfe;

            if (Int32.TryParse(txtPLZ.Text, out hilfe)) // Int32.TryParse -> Prüft ob Eingabe eine Zahl ist
            {
                sql = "SELECT COUNT(*) FROM Ort WHERE PLZ='" + txtPLZ.Text + "'";
                go = Datenbankzugriff.Scalar(sql);
            }
            else
                go = 0;
            

            if (rblGeschlecht.SelectedIndex == -1  || rblAlter.SelectedIndex == -1 || rblHaushalt.SelectedIndex == -1 || rblIntervall.SelectedIndex == -1)
            {
                lblErrorPLZ.Visible = false;
                lblError.Visible = true;
            }
            else if (txtPLZ.Text == "" || go == 0)
            {
                lblError.Visible = false;
                lblErrorPLZ.Visible = true;
            }
            else
            {
                sql = "UPDATE Fragebogen SET " 
                + "Geschlecht='" + rblGeschlecht.SelectedValue + "'"
                + ", Altersbereich='" + rblAlter.SelectedValue + "'"
                + ", PLZ='" + txtPLZ.Text + "'"
                + ", PersonenHaushalt='" + rblHaushalt.SelectedValue + "'"
                + ", BesuchIntervall='" + rblIntervall.SelectedValue + "'"
                + "WHERE ID_Fragebogen='" + idFrag + "'";
                Datenbankzugriff.NonQuery(sql);

                switch (ddAuswahl.SelectedValue)
                {
                    case "Mode":
                    case "Schuhe":
                        Response.Redirect("auswahl_kleidung.aspx?" + (Page.Session["ID"] = (ddAuswahl.SelectedValue)) + (Page.Session["idFrag"] = idFrag) + (Page.Session["oldSelection"] = (oldSelection)));
                        break;
                    case "Gastronomie":
                        Response.Redirect("auswahl_restaurant.aspx?" + (Page.Session["ID"] = (ddAuswahl.SelectedValue)) + (Page.Session["idFrag"] = idFrag) + (Page.Session["oldSelection"] = (oldSelection)));
                        break;
                    default:
                        Response.Redirect("auswahl_shop.aspx?" + (Page.Session["ID"] = (ddAuswahl.SelectedValue)) + (Page.Session["idFrag"] = idFrag) + (Page.Session["oldSelection"] = (oldSelection)));
                        break;
                }
            }
        }
    }
}