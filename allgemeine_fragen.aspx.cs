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
    public partial class allgemeine_fragen : System.Web.UI.Page
    {
        OdbcConnection connection = new OdbcConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sqlDB"].ConnectionString);
        int idFrag;
        int wertAllgemein1, wertAllgemein2;
        string sql;
        double hilfe;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack == false)
            {
                idFrag = Convert.ToInt32(Page.Session["idFrag"]);
                sql = "SELECT * FROM Fragebogen WHERE ID_Fragebogen='" + idFrag + "'";
                OdbcDataReader dr;
                dr = Datenbankzugriff.Reader(sql);
                sql = "SELECT * FROM Kontaktaufnahme WHERE ID='" + idFrag + "'";

                if (dr.Read())
                {
                    wertAllgemein1 = Convert.ToInt32(dr["VarenaBeste"]);
                    wertAllgemein2 = Convert.ToInt32(dr["Parkplatzangebot"]);

                    txtVeranstaltung.Text = Convert.ToString(dr["Veranstaltungen"]);
                    if (txtVeranstaltung.Text == "0")
                    {
                        txtVeranstaltung.Text = "";
                    }
                    if (wertAllgemein1 > 0) rblAllgemein1.Items[Convert.ToInt32(dr["VarenaBeste"]) - 1].Selected = true;
                    if (wertAllgemein2 > 0) rblAllgemein2.Items[Convert.ToInt32(dr["Parkplatzangebot"]) - 1].Selected = true;
                }
                dr.Close();
                connection.Close();

                sql = "SELECT * FROM Kontaktaufnahme WHERE ID='" + idFrag + "'";
                dr = Datenbankzugriff.Reader(sql);

                if (dr.Read())
                {
                    if (Page.IsPostBack == false)
                    {
                        txtEmail.Text = Convert.ToString(dr["Email"]);
                        txtVorname.Text = Convert.ToString(dr["Vorname"]);
                        txtNachname.Text = Convert.ToString(dr["Nachname"]);
                        txtTele.Text = Convert.ToString(dr["Telefonnummer"]);
                    }
                }
                dr.Close();
                connection.Close();
            }
        }

        protected void btnAllgemeinZurueck_Click(object sender, EventArgs e)
        {
            string oldSelection = Convert.ToString(Page.Session["oldSelection"]);
            string lastSelection = Convert.ToString(Page.Session["auswahlId"]);

            sql = "UPDATE Fragebogen SET "
            + "VarenaBeste='" + rblAllgemein1.SelectedValue + "'"
            + ", Parkplatzangebot='" + rblAllgemein2.SelectedValue + "'"
            + ", Veranstaltungen='" + txtVeranstaltung.Text + "'"
            + ", Kontaktaufnahme='" + 1 + "'"
            + "WHERE ID_Fragebogen='" + Page.Session["idFrag"] + "'";
            Datenbankzugriff.NonQuery(sql);
            sql = "SELECT MAX(ID) From Kontaktaufnahme";
            idFrag = Convert.ToInt32(Datenbankzugriff.Scalar(sql));


            if (idFrag != Convert.ToInt32(Page.Session["idFrag"]))
            {
                sql = "INSERT INTO Kontaktaufnahme(ID,Email,Vorname,Nachname,Telefonnummer)VALUES ('" + Page.Session["idFrag"] + "','" + txtEmail.Text + "','" + txtVorname.Text + "','" + txtNachname.Text + "','" + txtTele.Text + "')";
               
                Datenbankzugriff.NonQuery(sql);
                switch (lastSelection)
                {
                    case "Mode":
                    case "Schuhe":
                        Response.Redirect("auswahl_kleidung.aspx?" + (Page.Session["ID"] = (lastSelection)) + Page.Session["idFrag"] + (Page.Session["oldSelection"] = (oldSelection)));
                        break;
                    case "Gastronomie":
                        Response.Redirect("auswahl_restaurant.aspx?" + (Page.Session["ID"] = (lastSelection)) + Page.Session["idFrag"] + (Page.Session["oldSelection"] = (oldSelection)));
                        break;
                    default:
                        Response.Redirect("auswahl_shop.aspx?" + (Page.Session["ID"] = (lastSelection)) + Page.Session["idFrag"] + (Page.Session["oldSelection"] = (oldSelection)));
                        break;
                }
            }
            else
            {
                sql = "UPDATE Kontaktaufnahme SET Email='" + txtEmail.Text + "', Vorname='" + txtVorname.Text + "', Nachname='" + txtNachname.Text + "', Telefonnummer='" + txtTele.Text + "' WHERE ID='" + Page.Session["idFrag"] + "'";
                  
                Datenbankzugriff.NonQuery(sql);
                switch (lastSelection)
                {
                    case "Mode":
                    case "Schuhe":
                        Response.Redirect("auswahl_kleidung.aspx?" + (Page.Session["ID"] = (lastSelection)) + Page.Session["idFrag"] + (Page.Session["oldSelection"] = (oldSelection)));
                        break;
                    case "Gastronomie":
                        Response.Redirect("auswahl_restaurant.aspx?" + (Page.Session["ID"] = (lastSelection)) + Page.Session["idFrag"] + (Page.Session["oldSelection"] = (oldSelection)));
                        break;
                    default:
                        Response.Redirect("auswahl_shop.aspx?" + (Page.Session["ID"] = (lastSelection)) + Page.Session["idFrag"] + (Page.Session["oldSelection"] = (oldSelection)));
                        break;
                }
             }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            sql = "UPDATE Fragebogen SET "
            + "VarenaBeste='" + rblAllgemein1.SelectedValue + "'"
            + ", Parkplatzangebot='" + rblAllgemein2.SelectedValue + "'"
            + ", Veranstaltungen='" + txtVeranstaltung.Text + "'"
            + ", Kontaktaufnahme='" + 1 + "'"
            + "WHERE ID_Fragebogen='" + Page.Session["idFrag"] + "'";

            Datenbankzugriff.NonQuery(sql);
            sql = "SELECT MAX(ID) From Kontaktaufnahme";
            idFrag = Convert.ToInt32(Datenbankzugriff.Scalar(sql));

            if (rblAllgemein1.SelectedIndex == -1 || rblAllgemein2.SelectedIndex == -1)
            {
                lblErrorKontakt.Visible = false;
                lblErrorEmail.Visible = false;
                lblErrorTele.Visible = false;
                lblError.Visible = true;
            }
            else
            {
                if (txtEmail.Text.Length != 0 && txtNachname.Text.Length != 0 && txtTele.Text.Length != 0 && txtVorname.Text.Length != 0)
                {
                    if (idFrag != Convert.ToInt32(Page.Session["idFrag"]))
                    {
                        sql = "INSERT INTO Kontaktaufnahme(ID,Email,Vorname,Nachname,Telefonnummer) VALUES ('" + Page.Session["idFrag"] + "','" + txtEmail.Text + "','" + txtVorname.Text + "','" + txtNachname.Text + "','" + txtTele.Text + "')";
                        if (!Double.TryParse(txtTele.Text, out hilfe))
                        {
                            lblErrorKontakt.Visible = false;
                            lblError.Visible = false;
                            lblErrorEmail.Visible = false;
                            lblErrorTele.Visible = true;
                        }
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, "^[\\w\\.\\-]+@[a-zA-Z0-9\\-]+(\\.[a-zA-Z0-9\\-]{1,})*(\\.[a-zA-Z]{2,3}){1,2}$"))
                        {
                            lblErrorKontakt.Visible = false;
                            lblError.Visible = false;
                            lblErrorTele.Visible = false;
                            lblErrorEmail.Visible = true;
                        }
                        else
                        {
                            Datenbankzugriff.NonQuery(sql);
                            Response.Redirect("dank_seite.aspx");
                        }

                    }
                    else
                    {
                        sql = "UPDATE Kontaktaufnahme SET " 
                        + "Email='" + txtEmail.Text + "'"
                        + ", Vorname='" + txtVorname.Text + "'"
                        + ", Nachname='" + txtNachname.Text + "'"
                        + ", Telefonnummer='" + txtTele.Text + "'"
                        + "WHERE ID='" + Page.Session["idFrag"] + "'";
                        if (!Double.TryParse(txtTele.Text, out hilfe))
                        {
                            lblErrorKontakt.Visible = false;
                            lblError.Visible = false;
                            lblErrorEmail.Visible = false;
                            lblErrorTele.Visible = true;
                        }
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, "^[\\w\\.\\-]+@[a-zA-Z0-9\\-]+(\\.[a-zA-Z0-9\\-]{1,})*(\\.[a-zA-Z]{2,3}){1,2}$"))
                        {
                            lblErrorKontakt.Visible = false;
                            lblError.Visible = false;
                            lblErrorTele.Visible = false;
                            lblErrorEmail.Visible = true;
                        }
                        else
                        {
                            Datenbankzugriff.NonQuery(sql);
                            Response.Redirect("dank_seite.aspx");
                        }
                    }
                }
                else
                {
                    lblError.Visible = false;
                    lblErrorEmail.Visible = false;
                    lblErrorTele.Visible = false;
                    lblErrorKontakt.Visible = true;
                    if (txtEmail.Text.Length == 0 && txtNachname.Text.Length == 0 && txtTele.Text.Length == 0 && txtVorname.Text.Length == 0)
                    {
                        sql = "UPDATE Fragebogen Set Kontaktaufnahme='" + 0 + "'";
                        Datenbankzugriff.NonQuery(sql);
                        Response.Redirect("dank_seite.aspx");
                    }
                }
            }
        }
    }
}