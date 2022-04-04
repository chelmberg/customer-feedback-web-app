using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Odbc;

namespace kundenfeedbackanwendung_varena
{
    public partial class auswahl_restaurant : System.Web.UI.Page
    {
        OdbcConnection connection = new OdbcConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sqlDB"].ConnectionString);
        int fId;
        int wertKellner, wertDauer, wertEssenGetraenke, wertWartezeit;
        string selectionRestaurant, oldSelectionRestaurant;

        protected void Page_Load(object sender, EventArgs e)
        {
            selectionRestaurant = Convert.ToString(Page.Session["ID"]);
            oldSelectionRestaurant = Convert.ToString(Page.Session["oldSelection"]);
            fId = Convert.ToInt32(Page.Session["idFrag"]);

            if (Page.IsPostBack == false)
            {
                ddRestaurantAuswahl.Items.Add("Cafe & Arena");
                ddRestaurantAuswahl.Items.Add("Restaurant Genino");
                ddRestaurantAuswahl.Items.Add("INTERSPAR Restaurant");
                ddRestaurantAuswahl.Items.Add("Kai Li");
                ddRestaurantAuswahl.Items.Add("Levinsky Cafe-Bar-Galerie");
                ddRestaurantAuswahl.Items.Add("Subway");
            }

            // Aktualisiert die Radiobuttons der ganzen Seite wenn sie neu aufgerufen wird.
            OdbcCommand cmdUpdateAll = new OdbcCommand("SELECT * FROM Restaurant_Antworten WHERE FID='" + fId + "'", connection);
            OdbcDataReader drUpdateAll;
            int ausgewaehltesRestaurantId = 0;

            connection.Open();
            drUpdateAll = cmdUpdateAll.ExecuteReader();
            if (drUpdateAll.Read())
            {
                if (Page.IsPostBack == false)
                {
                    if (oldSelectionRestaurant == selectionRestaurant)
                    {
                        wertKellner = Convert.ToInt32(drUpdateAll["Kellner"]);
                        wertDauer = Convert.ToInt32(drUpdateAll["Bestellungsaufnahme"]);
                        wertEssenGetraenke = Convert.ToInt32(drUpdateAll["Essen_Getränke"]);
                        wertWartezeit = Convert.ToInt32(drUpdateAll["Wartezeit_Essen_Getränke"]);

                        // hier wird geprüft ob schon einmal ein Restaurant ausgewählt wurde, wenn ja dann wird die ID des gewählten Shops gespeichert in ausgewaehlterShopId
                        if (drUpdateAll["ID_Restaurant"].GetType() == ausgewaehltesRestaurantId.GetType())
                        {
                            ausgewaehltesRestaurantId = Convert.ToInt32(drUpdateAll["ID_Restaurant"]);
                            drUpdateAll.Close();

                            //// Auswahl des Dropdown Menu aktualisieren
                            OdbcCommand cmdBezeichnung = new OdbcCommand("SELECT * FROM Restaurant WHERE RID='" + ausgewaehltesRestaurantId + "'", connection);
                            OdbcDataReader dr;
                            dr = cmdBezeichnung.ExecuteReader();
                            if (dr.Read())
                            {
                                ddRestaurantAuswahl.SelectedValue = dr["Bezeichnung"].ToString();
                            }
                            dr.Close();

                            if (Page.IsPostBack == false)
                            {
                                ddRestaurantAuswahl.Enabled = false;
                                btnRAendernDD.Enabled = true;
                                txtREigeneMeinung.Enabled = false;
                                btnAendernEigeneM.Enabled = true;
                            }

                            // Eigene Meinung aktualisieren
                            OdbcCommand cmdUpdateMeinung = new OdbcCommand("SELECT * FROM Fragebogen WHERE ID_Fragebogen='" + fId + "'", connection);
                            OdbcDataReader drUpdateMeinung;
                            drUpdateMeinung = cmdUpdateMeinung.ExecuteReader();
                            if (drUpdateMeinung.Read()) txtREigeneMeinung.Text = drUpdateMeinung["EigeneMeinung"].ToString();
                            drUpdateMeinung.Close();
                        }
                        else drUpdateAll.Close();
                        if (wertKellner > 0) Funktion.Check(wertKellner, rbR11, rbR12, rbR13, rbR14);
                        if (wertDauer > 0) Funktion.Check(wertDauer, rbR21, rbR22, rbR23, rbR24);
                        if (wertEssenGetraenke > 0) Funktion.Check(wertEssenGetraenke, rbR31, rbR32, rbR33, rbR34);
                        if (wertWartezeit > 0) Funktion.Check(wertWartezeit, rbR41, rbR42, rbR43, rbR44);
                    }
                    else drUpdateAll.Close();
                }
            }
            else
            {
                int bewertungsart = 0;
                OdbcCommand cmdBewertungsart = new OdbcCommand("SELECT * FROM Fragebogen WHERE ID_Fragebogen='" + fId + "'", connection);
                OdbcDataReader drBewertungsart;
                OdbcCommand cmdDelete;

                drBewertungsart = cmdBewertungsart.ExecuteReader();
                if (drBewertungsart.Read()) bewertungsart = Convert.ToInt32(drBewertungsart["Bewertungsart"]);
                drBewertungsart.Close();
                switch (bewertungsart)
                {
                    case 1:
                        // delete shop
                        cmdDelete = new OdbcCommand("DELETE FROM Shop_Antworten WHERE FID='" + fId + "'", connection);
                        cmdDelete.ExecuteNonQuery();
                        break;
                    case 3:
                        // delete kleidung
                        cmdDelete = new OdbcCommand("DELETE FROM Kleidungsgeschäfte_Antworten WHERE FID='" + fId + "'", connection);
                        cmdDelete.ExecuteNonQuery();
                        break;
                }
                drUpdateAll.Close();
                OdbcCommand cmdFirstInsert = new OdbcCommand("INSERT INTO Restaurant_Antworten(Kellner,Bestellungsaufnahme,Essen_Getränke,Wartezeit_Essen_Getränke,FID)VALUES ('" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + fId + "')", connection);
                cmdFirstInsert.ExecuteNonQuery();
                OdbcCommand cmdUpdateBewertungsart = new OdbcCommand("UPDATE Fragebogen SET Bewertungsart='" + 2 + "' WHERE ID_Fragebogen='" + fId + "'", connection);
                cmdUpdateBewertungsart.ExecuteNonQuery();        
            }
            connection.Close();
        }

        protected void btnZurueckRestaurant_Click(object sender, EventArgs e)
        {
            int rId;
            string sqlRid;

            if (ddRestaurantNeu.Visible == true) sqlRid = "SELECT * FROM Restaurant WHERE Bezeichnung='" + ddRestaurantNeu.SelectedValue + "'";
            else sqlRid = "SELECT * FROM Restaurant WHERE Bezeichnung='" + ddRestaurantAuswahl.SelectedValue + "'";

            // Shop ID aus Datenbank holen
            OdbcCommand cmdSid = new OdbcCommand(sqlRid, connection);
            OdbcDataReader dr;

            connection.Open();
            dr = cmdSid.ExecuteReader();
            if (dr.Read()) rId = Convert.ToInt32(dr["RID"]);
            else rId = 99;   // bei Fehlerfall
            dr.Close();
            connection.Close();

            Funktion.Uncheck(wertKellner, rbR11, rbR12, rbR13, rbR14);
            int wertChechbox1 = Funktion.WertVergeben(wertKellner, rbR11, rbR12, rbR13, rbR14);
            Funktion.Uncheck(wertDauer, rbR21, rbR22, rbR23, rbR24);
            int wertChechbox2 = Funktion.WertVergeben(wertDauer, rbR21, rbR22, rbR23, rbR24);
            Funktion.Uncheck(wertEssenGetraenke, rbR31, rbR32, rbR33, rbR34);
            int wertChechbox3 = Funktion.WertVergeben(wertEssenGetraenke, rbR31, rbR32, rbR33, rbR34);
            Funktion.Uncheck(wertWartezeit, rbR41, rbR42, rbR43, rbR44);
            int wertChechbox4 = Funktion.WertVergeben(wertWartezeit, rbR41, rbR42, rbR43, rbR44);

                // Antwort Tabelle füllen
                string sqlInsert = "UPDATE Restaurant_Antworten SET "
                    + "ID_Restaurant ='" + rId + "'"
                    + ", Kellner='" + wertChechbox1 + "'"
                    + ", Bestellungsaufnahme = " + wertChechbox2 + ""
                    + ", Essen_Getränke = '" + wertChechbox3 + "'"
                    + ", Wartezeit_Essen_Getränke = '" + wertChechbox4 + "'"
                    + " WHERE FID = '" + fId + "'";
                OdbcCommand cmdInsert = new OdbcCommand(sqlInsert, connection);
                OdbcCommand cmdMeinung;
                if (txtNeuEigeneMeinung.Visible == true) cmdMeinung = new OdbcCommand("UPDATE Fragebogen SET EigeneMeinung='" + txtNeuEigeneMeinung.Text + "' WHERE ID_Fragebogen='" + fId + "'", connection);
                else cmdMeinung = new OdbcCommand("UPDATE Fragebogen SET EigeneMeinung='" + txtREigeneMeinung.Text + "' WHERE ID_Fragebogen='" + fId + "'", connection);

                connection.Open();
                cmdInsert.ExecuteNonQuery();
                cmdMeinung.ExecuteNonQuery();
                connection.Close();
            Response.Redirect("benutzerfragen.aspx?" + (Page.Session["idFrag"] = fId) + (Page.Session["oldSelection"] = (selectionRestaurant)));
        }

        protected void btnWeiterRestaurant_Click(object sender, EventArgs e)
        {
            int rId;
            string sqlRid;

            if (ddRestaurantNeu.Visible == true) sqlRid = "SELECT * FROM Restaurant WHERE Bezeichnung='" + ddRestaurantNeu.SelectedValue + "'";
            else sqlRid = "SELECT * FROM Restaurant WHERE Bezeichnung='" + ddRestaurantAuswahl.SelectedValue + "'";

            // Shop ID aus Datenbank holen
            OdbcCommand cmdSid = new OdbcCommand(sqlRid, connection);
            OdbcDataReader dr;

            connection.Open();
            dr = cmdSid.ExecuteReader();
            if (dr.Read()) rId = Convert.ToInt32(dr["RID"]);
            else rId = 99;   // bei Fehlerfall
            dr.Close();
            connection.Close();

            Funktion.Uncheck(wertKellner, rbR11, rbR12, rbR13, rbR14);
            int wertChechbox1 = Funktion.WertVergeben(wertKellner, rbR11, rbR12, rbR13, rbR14);
            Funktion.Uncheck(wertDauer, rbR21, rbR22, rbR23, rbR24);
            int wertChechbox2 = Funktion.WertVergeben(wertDauer, rbR21, rbR22, rbR23, rbR24);
            Funktion.Uncheck(wertEssenGetraenke, rbR31, rbR32, rbR33, rbR34);
            int wertChechbox3 = Funktion.WertVergeben(wertEssenGetraenke, rbR31, rbR32, rbR33, rbR34);
            Funktion.Uncheck(wertWartezeit, rbR41, rbR42, rbR43, rbR44);
            int wertChechbox4 = Funktion.WertVergeben(wertWartezeit, rbR41, rbR42, rbR43, rbR44);


            // Prüfen ob alle Radiobuttons betätigt wurden
            if (wertChechbox1 == 0 || wertChechbox2 == 0 || wertChechbox3 == 0 || wertChechbox4 == 0)
            {
                lblErrorRadioButton.Visible = true;
            }
            else
            {
                // Antwort Tabelle füllen
                string sqlInsert = "UPDATE Restaurant_Antworten SET "
                    + "ID_Restaurant ='" + rId + "'"
                    + ", Kellner='" + wertChechbox1 + "'"
                    + ", Bestellungsaufnahme = " + wertChechbox2 + ""
                    + ", Essen_Getränke = '" + wertChechbox3 + "'"
                    + ", Wartezeit_Essen_Getränke = '" + wertChechbox4 + "'"
                    + " WHERE FID = '" + fId + "'";
                OdbcCommand cmdInsert = new OdbcCommand(sqlInsert, connection);
                OdbcCommand cmdMeinung;
                if (txtNeuEigeneMeinung.Visible == true) cmdMeinung = new OdbcCommand("UPDATE Fragebogen SET EigeneMeinung='" + txtNeuEigeneMeinung.Text + "' WHERE ID_Fragebogen='" + fId + "'", connection);
                else cmdMeinung = new OdbcCommand("UPDATE Fragebogen SET EigeneMeinung='" + txtREigeneMeinung.Text + "' WHERE ID_Fragebogen='" + fId + "'", connection);

                connection.Open();
                cmdInsert.ExecuteNonQuery();
                cmdMeinung.ExecuteNonQuery();
                connection.Close();

                Response.Redirect("allgemeine_fragen.aspx?" + (Page.Session["auswahlId"] = (Convert.ToString(Page.Session["ID"]))) + (Page.Session["oldSelection"] = (selectionRestaurant)));
            }
        }

        protected void btnRAendernDD_Click(object sender, EventArgs e)
        {
            ddRestaurantAuswahl.Visible = false;
            ddRestaurantNeu.Visible = true;
            btnRAendernDD.Enabled = false;

            ddRestaurantNeu.Items.Add("Cafe & Arena");
            ddRestaurantNeu.Items.Add("Restaurant Genino");
            ddRestaurantNeu.Items.Add("INTERSPAR Restaurant");
            ddRestaurantNeu.Items.Add("Kai Li");
            ddRestaurantNeu.Items.Add("Levinsky Cafe-Bar-Galerie");
            ddRestaurantNeu.Items.Add("Subway");
        }

        protected void btnAendernEigeneM_Click(object sender, EventArgs e)
        {
            txtREigeneMeinung.Visible = false;
            txtNeuEigeneMeinung.Visible = true;
            btnAendernEigeneM.Enabled = false;
        }
    }
}


//OdbcConnection connection = new OdbcConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sqlDB"].ConnectionString);

//protected void Page_Load(object sender, EventArgs e)
//{
//    string selectionRest = Convert.ToString(Page.Session["ID"]);
//    int fId = Convert.ToInt32(Page.Session["idFrag"]);

//    ddRestaurantAuswahl.Items.Add("Cafe & Arena");
//    ddRestaurantAuswahl.Items.Add("Restaurant Genino");
//    ddRestaurantAuswahl.Items.Add("INTERSPAR Restaurant");
//    ddRestaurantAuswahl.Items.Add("Kai Li");
//    ddRestaurantAuswahl.Items.Add("Levinsky Cafe-Bar-Galerie");
//    ddRestaurantAuswahl.Items.Add("Subway");

//    // Aktualisiert die Radiobuttons der ganzen Seite wenn sie neu aufgerufen wird.
//    OdbcCommand cmdUpdateAll = new OdbcCommand("SELECT * FROM Restaurant_Antworten WHERE FID='" + fId + "'", connection);
//    OdbcDataReader drUpdateAll;
//    int ausgewaehlterRestaurantId = 0;
//    bool ddWertFestlegen = false;

//    connection.Open();
//    drUpdateAll = cmdUpdateAll.ExecuteReader();
//    if (drUpdateAll.Read())
//    {
//        if (drUpdateAll["ID_Restaurant"].GetType() == ausgewaehlterRestaurantId.GetType())
//        {
//            ausgewaehlterRestaurantId = Convert.ToInt32(drUpdateAll["ID_Restaurant"]);
//            ddWertFestlegen = true;
//        }
//        if (Convert.ToInt32(drUpdateAll["Kellner"]) > 0) rbRestaurant1.Items[Convert.ToInt32(drUpdateAll["Kellner"]) - 1].Selected = true;
//        if (Convert.ToInt32(drUpdateAll["Bestellungsaufnahme"]) > 0) rbRestaurant2.Items[Convert.ToInt32(drUpdateAll["Bestellungsaufnahme"]) - 1].Selected = true;
//        if (Convert.ToInt32(drUpdateAll["Essen_Getränke"]) > 0) rbRestaurant3.Items[Convert.ToInt32(drUpdateAll["Essen_Getränke"]) - 1].Selected = true;
//        if (Convert.ToInt32(drUpdateAll["Wartezeit_Essen_Getränke"]) > 0) rbRestaurant4.Items[Convert.ToInt32(drUpdateAll["Wartezeit_Essen_Getränke"]) - 1].Selected = true;
//        drUpdateAll.Close();
//        connection.Close();
//    }

//    if (ddWertFestlegen == true)
//    {
//        OdbcCommand cmdBezeichnung = new OdbcCommand("SELECT * FROM Restaurant WHERE RID='" + ausgewaehlterRestaurantId + "'", connection);
//        OdbcDataReader dr;

//        connection.Open();
//        dr = cmdBezeichnung.ExecuteReader();
//        if (dr.Read())
//        {
//            ddRestaurantAuswahl.SelectedValue = dr["Bezeichnung"].ToString();
//            dr.Close();
//            connection.Close();
//        }
//    }
//}

//protected void btnZurueckRestaurant_Click(object sender, EventArgs e)
//{
//    Response.Redirect("benutzerfragen.aspx");
//}

//protected void btnWeiterRestaurant_Click(object sender, EventArgs e)
//{
//    int rId;

//    // Shop ID aus Datenbank holen
//    string sqlRid = "SELECT * FROM Restaurant WHERE Bezeichnung='" + ddRestaurantAuswahl.SelectedValue + "'";
//    OdbcCommand cmdRid = new OdbcCommand(sqlRid, connection);
//    OdbcDataReader dr;

//    //connection.Open();
//    dr = cmdRid.ExecuteReader();
//    if (dr.Read())
//    {
//        rId = Convert.ToInt32(dr["RID"]);
//        dr.Close();
//        connection.Close();
//    }
//    else
//    {
//        rId = 99;
//        dr.Close();
//        connection.Close();
//    }

//    // Antwort Tabelle füllen
//    string sqlInsert = "INSERT INTO Restaurant_Antworten(ID_Restaurant,Kellner,Bestellungsaufnahme,Essen_Getränke,Wartezeit_Essen_Getränke,FID)VALUES ('" + rId + "','" + Convert.ToInt32(rbRestaurant1.SelectedValue) + "','" + Convert.ToInt32(rbRestaurant2.SelectedValue) + "','" + Convert.ToInt32(rbRestaurant3.SelectedValue) + "','" + Convert.ToInt32(rbRestaurant4.SelectedValue) + "','" + Page.Session["idFrag"] + "')";
//    //string sqlInsert = "UPDATE Restaurant_Antworten SET "
//    //    + "ID_Restaurant ='" + rId + "'"
//    //    + ", Kellner='" + Convert.ToInt32(rbRestaurant1.SelectedValue) + "'"
//    //    + ", Bestellungsaufnahme = " + Convert.ToInt32(rbRestaurant2.SelectedValue) + ""
//    //    + ", Essen_Getränke = '" + Convert.ToInt32(rbRestaurant3.SelectedValue) + "'"
//    //    + ", Wartezeit_Essen_Getränke = '" + Convert.ToInt32(rbRestaurant4.SelectedValue) + "'"
//    //    + " WHERE FID = '" + Page.Session["idFrag"] + "'";
//    OdbcCommand cmdInsert = new OdbcCommand(sqlInsert, connection);
//    OdbcCommand cmdMeinung = new OdbcCommand("UPDATE Fragebogen SET EigeneMeinung='" + txtREigeneMeinung.Text + "' WHERE ID_Fragebogen='" + Page.Session["idFrag"] + "'", connection);
//    connection.Open();
//    cmdMeinung.ExecuteNonQuery();
//    cmdInsert.ExecuteNonQuery();
//    connection.Close();

//    Response.Redirect("allgemeine_fragen.aspx?" + (Page.Session["auswahlId"] = (Convert.ToString(Page.Session["ID"]))));
//}