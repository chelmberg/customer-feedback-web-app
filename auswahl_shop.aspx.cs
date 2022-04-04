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
    public partial class auswahl_shop : System.Web.UI.Page
    {
        OdbcConnection connection = new OdbcConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sqlDB"].ConnectionString);
        int fId;
        int wertPersonal, wertDauer, wertSortiment, wertAufteilung;
        string selectionShop, oldSelectionShop;

        protected void Page_Load(object sender, EventArgs e)
        {
            selectionShop = Convert.ToString(Page.Session["ID"]);
            oldSelectionShop = Convert.ToString(Page.Session["oldSelection"]);
            fId = Convert.ToInt32(Page.Session["idFrag"]);

            if (Page.IsPostBack==false)
            {
                switch (selectionShop)
                {
                    case "Lebensmittel":
                        ddShopAuswahl.Items.Add("INTERSPAR");
                        ddShopAuswahl.Items.Add("INTERSPAR Frische-Bäcker");
                        break;
                    case "Uhren":
                        ddShopAuswahl.Items.Add("Bijou Brigitte");
                        ddShopAuswahl.Items.Add("Claire´s");
                        ddShopAuswahl.Items.Add("Kristall");
                        ddShopAuswahl.Items.Add("Le Clou");
                        ddShopAuswahl.Items.Add("Nanu Nana");
                        ddShopAuswahl.Items.Add("SchmuckArena");
                        break;
                    case "Buecher":
                        ddShopAuswahl.Items.Add("Thalia");
                        break;
                    case "Kinderwelt":
                        ddShopAuswahl.Items.Add("LOLLIPOP");
                        break;
                    case "Technik":
                        ddShopAuswahl.Items.Add("3Store");
                        ddShopAuswahl.Items.Add("A1 Shop");
                        ddShopAuswahl.Items.Add("Fielmann");
                        ddShopAuswahl.Items.Add("Game Stop");
                        ddShopAuswahl.Items.Add("Hartlauer Handy pur");
                        ddShopAuswahl.Items.Add("Media Markt");
                        ddShopAuswahl.Items.Add("Pearle");
                        ddShopAuswahl.Items.Add("Telering");
                        break;
                    case "Friseur":
                        ddShopAuswahl.Items.Add("Cutting Crew Style");
                        ddShopAuswahl.Items.Add("dm friseurstudio");
                        ddShopAuswahl.Items.Add("Headwork");
                        ddShopAuswahl.Items.Add("Roma Friseurbedarf");
                        break;
                    case "Drogerie":
                        ddShopAuswahl.Items.Add("dm drogerie markt");
                        ddShopAuswahl.Items.Add("Douglas");
                        ddShopAuswahl.Items.Add("Reformhaus Martin");
                        break;
                    case "Wohnen":
                        ddShopAuswahl.Items.Add("Depot");
                        ddShopAuswahl.Items.Add("Ruefa");
                        ddShopAuswahl.Items.Add("Stern Reisen Wintereder");
                        break;
                    case "Dienstleistung":
                        ddShopAuswahl.Items.Add("Sparkasse Oberösterreich");
                        break;
                }
            }
            // Aktualisiert die Radiobuttons der ganzen Seite wenn sie neu aufgerufen wird.
            OdbcCommand cmdUpdateAll = new OdbcCommand("SELECT * FROM Shop_Antworten WHERE FID='" + fId + "'", connection);
            OdbcDataReader drUpdateAll;
            int ausgewaehlterShopId = 0;

            connection.Open();
            drUpdateAll = cmdUpdateAll.ExecuteReader();
            if (drUpdateAll.Read())
            {
                if (Page.IsPostBack == false)
                {
                    if (oldSelectionShop == selectionShop)
                    {
                        wertPersonal = Convert.ToInt32(drUpdateAll["Personal"]);
                        wertDauer = Convert.ToInt32(drUpdateAll["DauerAnsprache"]);
                        wertSortiment = Convert.ToInt32(drUpdateAll["Sortiment"]);
                        wertAufteilung = Convert.ToInt32(drUpdateAll["AufteilungShop"]);

                        // hier wird geprüft ob schon einmal ein Shop ausgewählt wurde, wenn ja dann wird die ID des gewählten Shops gespeichert in ausgewaehlterShopId
                        if (drUpdateAll["ID_Shop"].GetType() == ausgewaehlterShopId.GetType())
                        {
                            ausgewaehlterShopId = Convert.ToInt32(drUpdateAll["ID_Shop"]);
                            drUpdateAll.Close();
                            ////ddWertFestlegen = true;
                            //// Auswahl des Dropdown Menu aktualisieren
                            OdbcCommand cmdBezeichnung = new OdbcCommand("SELECT * FROM Shop WHERE SID='" + ausgewaehlterShopId + "'", connection);
                            OdbcDataReader dr;
                            dr = cmdBezeichnung.ExecuteReader();
                            if (dr.Read())
                            {
                                ddShopAuswahl.SelectedValue = dr["Bezeichnung"].ToString();
                            }
                            dr.Close();

                            if (Page.IsPostBack == false)
                            {
                                ddShopAuswahl.Enabled = false;
                                btnSAendernDD.Enabled = true;
                                txtSEigeneMeinung.Enabled = false;
                                btnAendernEigeneM.Enabled = true;
                            }

                            // Eigene Meinung aktualisieren
                            OdbcCommand cmdUpdateMeinung = new OdbcCommand("SELECT * FROM Fragebogen WHERE ID_Fragebogen='" + fId + "'", connection);
                            OdbcDataReader drUpdateMeinung;
                            drUpdateMeinung = cmdUpdateMeinung.ExecuteReader();
                            if (drUpdateMeinung.Read()) txtSEigeneMeinung.Text = drUpdateMeinung["EigeneMeinung"].ToString();
                            drUpdateMeinung.Close();
                        }
                        else drUpdateAll.Close();
                        if (wertPersonal > 0) Funktion.Check(wertPersonal, rb11, rb12, rb13, rb14);
                        if (wertDauer > 0) Funktion.Check(wertDauer, rb21, rb22, rb23, rb24);
                        if (wertSortiment > 0) Funktion.Check(wertSortiment, rb31, rb32, rb33, rb34);
                        if (wertAufteilung > 0) Funktion.Check(wertAufteilung, rb41, rb42, rb43, rb44);
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
                    case 2:
                        // delete restaurant
                        cmdDelete = new OdbcCommand("DELETE FROM Restaurant_Antworten WHERE FID='" + fId + "'", connection);
                        cmdDelete.ExecuteNonQuery();
                        break;
                    case 3:
                        // delete kleidung
                        cmdDelete = new OdbcCommand("DELETE FROM Kleidungsgeschäfte_Antworten WHERE FID='" + fId + "'", connection);
                        cmdDelete.ExecuteNonQuery();
                        break;
                }
                drUpdateAll.Close();
                OdbcCommand cmdFirstInsert = new OdbcCommand("INSERT INTO Shop_Antworten(Personal,DauerAnsprache,Sortiment,AufteilungShop,FID)VALUES ('" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + fId + "')", connection);
                cmdFirstInsert.ExecuteNonQuery();
                OdbcCommand cmdUpdateBewertungsart = new OdbcCommand("UPDATE Fragebogen SET Bewertungsart='" + 1 + "' WHERE ID_Fragebogen='" + fId + "'", connection);
                cmdUpdateBewertungsart.ExecuteNonQuery();
            }
            connection.Close();
        }

        protected void btnZurueckShop_Click(object sender, EventArgs e)
        {
            
            int sId;
            string sqlSid;

            if (ddShopNeu.Visible == true) sqlSid = "SELECT * FROM Shop WHERE Bezeichnung='" + ddShopNeu.SelectedValue + "'";
            else sqlSid = "SELECT * FROM Shop WHERE Bezeichnung='" + ddShopAuswahl.SelectedValue + "'";

            // Shop ID aus Datenbank holen
            OdbcCommand cmdSid = new OdbcCommand(sqlSid, connection);
            OdbcDataReader dr;

            connection.Open();
            dr = cmdSid.ExecuteReader();
            if (dr.Read()) sId = Convert.ToInt32(dr["SID"]);
            else sId = 99;   // bei Fehlerfall
            dr.Close();
            connection.Close();

            Funktion.Uncheck(wertPersonal, rb11, rb12, rb13, rb14);
            int wertChechbox1 = Funktion.WertVergeben(wertPersonal, rb11, rb12, rb13, rb14);
            Funktion.Uncheck(wertDauer, rb21, rb22, rb23, rb24);
            int wertChechbox2 = Funktion.WertVergeben(wertDauer, rb21, rb22, rb23, rb24);
            Funktion.Uncheck(wertSortiment, rb31, rb32, rb33, rb34);
            int wertChechbox3 = Funktion.WertVergeben(wertSortiment, rb31, rb32, rb33, rb34);
            Funktion.Uncheck(wertAufteilung, rb41, rb42, rb43, rb44);
            int wertChechbox4 = Funktion.WertVergeben(wertAufteilung, rb41, rb42, rb43, rb44);

            // Antwort Tabelle füllen
            string sqlInsert = "UPDATE Shop_Antworten SET "
                + "ID_Shop ='" + sId + "'"
                + ", Personal='" + wertChechbox1 + "'"
                + ", DauerAnsprache = " + wertChechbox2 + ""
                + ", Sortiment = '" + wertChechbox3 + "'"
                + ", AufteilungShop = '" + wertChechbox4 + "'"
                + " WHERE FID = '" + fId + "'";
            OdbcCommand cmdInsert = new OdbcCommand(sqlInsert, connection);
            OdbcCommand cmdMeinung;
            if (txtNeuEigeneMeinung.Visible == true) cmdMeinung = new OdbcCommand("UPDATE Fragebogen SET EigeneMeinung='" + txtNeuEigeneMeinung.Text + "' WHERE ID_Fragebogen='" + fId + "'", connection);
            else cmdMeinung = new OdbcCommand("UPDATE Fragebogen SET EigeneMeinung='" + txtSEigeneMeinung.Text + "' WHERE ID_Fragebogen='" + fId + "'", connection);

            connection.Open();
            cmdInsert.ExecuteNonQuery();
            cmdMeinung.ExecuteNonQuery();
            connection.Close();

            Response.Redirect("benutzerfragen.aspx?" + (Page.Session["idFrag"] = fId) + (Page.Session["oldSelection"] = (selectionShop)));
        }

        protected void btnWeiterShop_Click(object sender, EventArgs e)
        {
            int sId;
            string sqlSid;

            if (ddShopNeu.Visible == true) sqlSid = "SELECT * FROM Shop WHERE Bezeichnung='" + ddShopNeu.SelectedValue + "'";
            else sqlSid = "SELECT * FROM Shop WHERE Bezeichnung='" + ddShopAuswahl.SelectedValue + "'";

            // Shop ID aus Datenbank holen
            OdbcCommand cmdSid = new OdbcCommand(sqlSid, connection);
            OdbcDataReader dr;

            connection.Open();
            dr = cmdSid.ExecuteReader();
            if (dr.Read()) sId = Convert.ToInt32(dr["SID"]);
            else sId = 99;   // bei Fehlerfall
            dr.Close();
            connection.Close();

            Funktion.Uncheck(wertPersonal, rb11, rb12, rb13, rb14);
            int wertChechbox1 = Funktion.WertVergeben(wertPersonal, rb11, rb12, rb13, rb14);
            Funktion.Uncheck(wertDauer, rb21, rb22, rb23, rb24);
            int wertChechbox2 = Funktion.WertVergeben(wertDauer, rb21, rb22, rb23, rb24);
            Funktion.Uncheck(wertSortiment, rb31, rb32, rb33, rb34);
            int wertChechbox3 = Funktion.WertVergeben(wertSortiment, rb31, rb32, rb33, rb34);
            Funktion.Uncheck(wertAufteilung, rb41, rb42, rb43, rb44);
            int wertChechbox4 = Funktion.WertVergeben(wertAufteilung, rb41, rb42, rb43, rb44);

            // Prüfen ob alle Radiobuttons betätigt wurden
            if (wertChechbox1 == 0 || wertChechbox2 == 0 || wertChechbox3 == 0 || wertChechbox4 == 0)
            {
                lblErrorRadioButton.Visible = true;
            }
            else
            {
                // Antwort Tabelle füllen
                string sqlInsert = "UPDATE Shop_Antworten SET "
                    + "ID_Shop ='" + sId + "'"
                    + ", Personal='" + wertChechbox1 + "'"
                    + ", DauerAnsprache = " + wertChechbox2 + ""
                    + ", Sortiment = '" + wertChechbox3 + "'"
                    + ", AufteilungShop = '" + wertChechbox4 + "'"
                    + " WHERE FID = '" + fId + "'";
                OdbcCommand cmdInsert = new OdbcCommand(sqlInsert, connection);
                OdbcCommand cmdMeinung;
                if (txtNeuEigeneMeinung.Visible == true) cmdMeinung = new OdbcCommand("UPDATE Fragebogen SET EigeneMeinung='" + txtNeuEigeneMeinung.Text + "' WHERE ID_Fragebogen='" + fId + "'", connection);
                else cmdMeinung = new OdbcCommand("UPDATE Fragebogen SET EigeneMeinung='" + txtSEigeneMeinung.Text + "' WHERE ID_Fragebogen='" + fId + "'", connection);

                connection.Open();
                cmdInsert.ExecuteNonQuery();
                cmdMeinung.ExecuteNonQuery();
                connection.Close();

                Response.Redirect("allgemeine_fragen.aspx?" + (Page.Session["auswahlId"] = (Convert.ToString(Page.Session["ID"]))) + (Page.Session["oldSelection"] = (selectionShop)));
            }
        }

        protected void btnSAendernDD_Click(object sender, EventArgs e)
        {
            ddShopAuswahl.Visible = false;
            ddShopNeu.Visible = true;
            btnSAendernDD.Enabled = false;

            switch (selectionShop)
            {
                case "Lebensmittel":
                    ddShopNeu.Items.Add("INTERSPAR");
                    ddShopNeu.Items.Add("INTERSPAR Frische-Bäcker");
                    break;
                case "Uhren":
                    ddShopNeu.Items.Add("Bijou Brigitte");
                    ddShopNeu.Items.Add("Claire´s");
                    ddShopNeu.Items.Add("Kristall");
                    ddShopNeu.Items.Add("Le Clou");
                    ddShopNeu.Items.Add("Nanu Nana");
                    ddShopNeu.Items.Add("SchmuckArena");
                    break;
                case "Buecher":
                    ddShopNeu.Items.Add("Thalia");
                    break;
                case "Kinderwelt":
                    ddShopNeu.Items.Add("LOLLIPOP");
                    break;
                case "Technik":
                    ddShopNeu.Items.Add("3Store");
                    ddShopNeu.Items.Add("A1 Shop");
                    ddShopNeu.Items.Add("Fielmann");
                    ddShopNeu.Items.Add("Game Stop");
                    ddShopNeu.Items.Add("Hartlauer Handy pur");
                    ddShopNeu.Items.Add("Media Markt");
                    ddShopNeu.Items.Add("Pearle");
                    ddShopNeu.Items.Add("Telering");
                    break;
                case "Friseur":
                    ddShopNeu.Items.Add("Cutting Crew Style");
                    ddShopNeu.Items.Add("dm friseurstudio");
                    ddShopNeu.Items.Add("Headwork");
                    ddShopNeu.Items.Add("Roma Friseurbedarf");
                    break;
                case "Drogerie":
                    ddShopNeu.Items.Add("dm drogerie markt");
                    ddShopNeu.Items.Add("Douglas");
                    ddShopNeu.Items.Add("Reformhaus Martin");
                    break;
                case "Wohnen":
                    ddShopNeu.Items.Add("Depot");
                    ddShopNeu.Items.Add("Ruefa");
                    ddShopNeu.Items.Add("Stern Reisen Wintereder");
                    break;
                case "Dienstleistung":
                    ddShopNeu.Items.Add("Sparkasse Oberösterreich");
                    break;
            }
        }

        protected void btnAendernEigeneM_Click(object sender, EventArgs e)
        {
            txtSEigeneMeinung.Visible = false;
            txtNeuEigeneMeinung.Visible = true;
            btnAendernEigeneM.Enabled = false;
        }
    }
}