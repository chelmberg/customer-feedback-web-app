using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Data;
using System.Data.Odbc;

namespace kundenfeedbackanwendung_varena
{
    public partial class auswahl_kleidung : System.Web.UI.Page
    {
        OdbcConnection connection = new OdbcConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["sqlDB"].ConnectionString);
        int fId;
        int wertPersonal, wertDauer, wertSortiment, wertAufteilung;
        string selectionKleidung, oldSelectionKleidung;

        protected void Page_Load(object sender, EventArgs e)
        {
            selectionKleidung = Convert.ToString(Page.Session["ID"]);
            oldSelectionKleidung = Convert.ToString(Page.Session["oldSelection"]);
            fId = Convert.ToInt32(Page.Session["idFrag"]);

            if (Page.IsPostBack == false)
            {
                switch (selectionKleidung)
                {
                    case "Mode":
                        ddKleidungAuswahl.Items.Add("Ann Christine");
                        ddKleidungAuswahl.Items.Add("Bonita");
                        ddKleidungAuswahl.Items.Add("C&A");
                        ddKleidungAuswahl.Items.Add("Calzedonia");
                        ddKleidungAuswahl.Items.Add("Camp David");
                        ddKleidungAuswahl.Items.Add("Colloseum");
                        ddKleidungAuswahl.Items.Add("Comma");
                        ddKleidungAuswahl.Items.Add("Foot Locker");
                        ddKleidungAuswahl.Items.Add("Hervis");
                        ddKleidungAuswahl.Items.Add("H&M");
                        ddKleidungAuswahl.Items.Add("Intimissimi");
                        ddKleidungAuswahl.Items.Add("Jack & Jones");
                        ddKleidungAuswahl.Items.Add("Jones");
                        ddKleidungAuswahl.Items.Add("KASTNER & ÖHLER");
                        ddKleidungAuswahl.Items.Add("New Yorker");
                        ddKleidungAuswahl.Items.Add("Northland");
                        ddKleidungAuswahl.Items.Add("Only");
                        ddKleidungAuswahl.Items.Add("Orsay");
                        ddKleidungAuswahl.Items.Add("Palmers");
                        ddKleidungAuswahl.Items.Add("s.Oliver");
                        ddKleidungAuswahl.Items.Add("Tom Tailor");
                        ddKleidungAuswahl.Items.Add("Tom Tailor Denim");
                        ddKleidungAuswahl.Items.Add("Triumph");
                        ddKleidungAuswahl.Items.Add("Vero Moda");
                        break;
                    case "Schuhe":
                        ddKleidungAuswahl.Items.Add("Deichmann");
                        ddKleidungAuswahl.Items.Add("Leder Hausmann");
                        ddKleidungAuswahl.Items.Add("HUMANIC");
                        ddKleidungAuswahl.Items.Add("Monzero");
                        break;
                }
            }

            // Aktualisiert die Radiobuttons der ganzen Seite wenn sie neu aufgerufen wird.
            OdbcCommand cmdUpdateAll = new OdbcCommand("SELECT * FROM Kleidungsgeschäfte_Antworten WHERE FID='" + fId + "'", connection);
            OdbcDataReader drUpdateAll;
            int ausgewaehlteKleidungId = 0;

            connection.Open();
            drUpdateAll = cmdUpdateAll.ExecuteReader();
            if (drUpdateAll.Read())
            {
                if (Page.IsPostBack == false)
                {
                    if (oldSelectionKleidung == selectionKleidung)
                    {
                        wertPersonal = Convert.ToInt32(drUpdateAll["Personal"]);
                        wertDauer = Convert.ToInt32(drUpdateAll["DauerAnsprache"]);
                        wertSortiment = Convert.ToInt32(drUpdateAll["Sortiment"]);
                        wertAufteilung = Convert.ToInt32(drUpdateAll["QualitätWare"]);

                        // hier wird geprüft ob schon einmal ein Shop ausgewählt wurde, wenn ja dann wird die ID des gewählten Shops gespeichert in ausgewaehlterShopId
                        if (drUpdateAll["ID_Kleidungsgeschäfte"].GetType() == ausgewaehlteKleidungId.GetType())
                        {
                            ausgewaehlteKleidungId = Convert.ToInt32(drUpdateAll["ID_Kleidungsgeschäfte"]);
                            drUpdateAll.Close();
                            ////ddWertFestlegen = true;

                            //// Auswahl des Dropdown Menu aktualisieren
                            OdbcCommand cmdBezeichnung = new OdbcCommand("SELECT * FROM Kleidungsgeschäfte WHERE KID='" + ausgewaehlteKleidungId + "'", connection);
                            OdbcDataReader dr;
                            dr = cmdBezeichnung.ExecuteReader();
                            if (dr.Read())
                            {
                                ddKleidungAuswahl.SelectedValue = dr["Bezeichnung"].ToString();
                            }
                            dr.Close();

                            if (Page.IsPostBack == false)
                            {
                                ddKleidungAuswahl.Enabled = false;
                                btnKAendernDD.Enabled = true;
                                txtKEigeneMeinung.Enabled = false;
                                btnAendernEigeneM.Enabled = true;
                            }

                            // Eigene Meinung aktualisieren
                            OdbcCommand cmdUpdateMeinung = new OdbcCommand("SELECT * FROM Fragebogen WHERE ID_Fragebogen='" + fId + "'", connection);
                            OdbcDataReader drUpdateMeinung;
                            drUpdateMeinung = cmdUpdateMeinung.ExecuteReader();
                            if (drUpdateMeinung.Read()) txtKEigeneMeinung.Text = drUpdateMeinung["EigeneMeinung"].ToString();
                            drUpdateMeinung.Close();
                        }
                        else drUpdateAll.Close();
                        if (wertPersonal > 0) Funktion.Check(wertPersonal, rbK11, rbK12, rbK13, rbK14);
                        if (wertDauer > 0) Funktion.Check(wertDauer, rbK21, rbK22, rbK23, rbK24);
                        if (wertSortiment > 0) Funktion.Check(wertSortiment, rbK31, rbK32, rbK33, rbK34);
                        if (wertAufteilung > 0) Funktion.Check(wertAufteilung, rbK41, rbK42, rbK43, rbK44);
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
                    case 2:
                        // delete restaurant
                        cmdDelete = new OdbcCommand("DELETE FROM Restaurant_Antworten WHERE FID='" + fId + "'", connection);
                        cmdDelete.ExecuteNonQuery();
                        break;
                }
                drUpdateAll.Close();
                OdbcCommand cmdFirstInsert = new OdbcCommand("INSERT INTO Kleidungsgeschäfte_Antworten(Personal,DauerAnsprache,Sortiment,QualitätWare,FID)VALUES ('" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + fId + "')", connection);
                OdbcCommand cmdUpdateBewertungsart = new OdbcCommand("UPDATE Fragebogen SET Bewertungsart='"+3+"' WHERE ID_Fragebogen='" + fId + "'", connection);
                cmdFirstInsert.ExecuteNonQuery();
                cmdUpdateBewertungsart.ExecuteNonQuery();
            }
            connection.Close();
        }

        protected void btnZurueckKleidung_Click(object sender, EventArgs e)
        {
            int kId;
            string sqlKid;

            if (ddKleidungNeu.Visible == true) sqlKid = "SELECT * FROM Kleidungsgeschäfte WHERE Bezeichnung='" + ddKleidungNeu.SelectedValue + "'";
            else sqlKid = "SELECT * FROM Kleidungsgeschäfte WHERE Bezeichnung='" + ddKleidungAuswahl.SelectedValue + "'";

            // Kleidungsgeschäfte ID aus Datenbank holen
            OdbcCommand cmdKid = new OdbcCommand(sqlKid, connection);
            OdbcDataReader dr;

            connection.Open();
            dr = cmdKid.ExecuteReader();
            if (dr.Read()) kId = Convert.ToInt32(dr["KID"]);
            else kId = 99;   // bei Fehlerfall
            dr.Close();
            connection.Close();

            Funktion.Uncheck(wertPersonal, rbK11, rbK12, rbK13, rbK14);
            int wertChechbox1 = Funktion.WertVergeben(wertPersonal, rbK11, rbK12, rbK13, rbK14);
            Funktion.Uncheck(wertDauer, rbK21, rbK22, rbK23, rbK24);
            int wertChechbox2 = Funktion.WertVergeben(wertDauer, rbK21, rbK22, rbK23, rbK24);
            Funktion.Uncheck(wertSortiment, rbK31, rbK32, rbK33, rbK34);
            int wertChechbox3 = Funktion.WertVergeben(wertSortiment, rbK31, rbK32, rbK33, rbK34);
            Funktion.Uncheck(wertAufteilung, rbK41, rbK42, rbK43, rbK44);
            int wertChechbox4 = Funktion.WertVergeben(wertAufteilung, rbK41, rbK42, rbK43, rbK44);

            // Antwort Tabelle füllen
            string sqlInsert = "UPDATE Kleidungsgeschäfte_Antworten SET "
                + "ID_Kleidungsgeschäfte ='" + kId + "'"
                + ", Personal='" + wertChechbox1 + "'"
                + ", DauerAnsprache = " + wertChechbox2 + ""
                + ", Sortiment = '" + wertChechbox3 + "'"
                + ", QualitätWare = '" + wertChechbox4 + "'"
                + " WHERE FID = '" + fId + "'";
            OdbcCommand cmdInsert = new OdbcCommand(sqlInsert, connection);
            OdbcCommand cmdMeinung;
            if (txtNeuEigeneMeinung.Visible == true) cmdMeinung = new OdbcCommand("UPDATE Fragebogen SET EigeneMeinung='" + txtNeuEigeneMeinung.Text + "' WHERE ID_Fragebogen='" + fId + "'", connection);
            else cmdMeinung = new OdbcCommand("UPDATE Fragebogen SET EigeneMeinung='" + txtKEigeneMeinung.Text + "' WHERE ID_Fragebogen='" + fId + "'", connection);

            connection.Open();
            cmdInsert.ExecuteNonQuery();
            cmdMeinung.ExecuteNonQuery();
            connection.Close();

            Response.Redirect("benutzerfragen.aspx?" + (Page.Session["idFrag"] = fId) + (Page.Session["oldSelection"] = (selectionKleidung)));
        }

        protected void btnWeiterKleidung_Click(object sender, EventArgs e)
        {
            int kId;
            string sqlKid;

            if (ddKleidungNeu.Visible == true) sqlKid = "SELECT * FROM Kleidungsgeschäfte WHERE Bezeichnung='" + ddKleidungNeu.SelectedValue + "'";
            else sqlKid = "SELECT * FROM Kleidungsgeschäfte WHERE Bezeichnung='" + ddKleidungAuswahl.SelectedValue + "'";

            // Kleidungsgeschäfte ID aus Datenbank holen
            OdbcCommand cmdKid = new OdbcCommand(sqlKid, connection);
            OdbcDataReader dr;

            connection.Open();
            dr = cmdKid.ExecuteReader();
            if (dr.Read()) kId = Convert.ToInt32(dr["KID"]);
            else kId = 99;   // bei Fehlerfall
            dr.Close();
            connection.Close();

            Funktion.Uncheck(wertPersonal, rbK11, rbK12, rbK13, rbK14);
            int wertChechbox1 = Funktion.WertVergeben(wertPersonal, rbK11, rbK12, rbK13, rbK14);
            Funktion.Uncheck(wertDauer, rbK21, rbK22, rbK23, rbK24);
            int wertChechbox2 = Funktion.WertVergeben(wertDauer, rbK21, rbK22, rbK23, rbK24);
            Funktion.Uncheck(wertSortiment, rbK31, rbK32, rbK33, rbK34);
            int wertChechbox3 = Funktion.WertVergeben(wertSortiment, rbK31, rbK32, rbK33, rbK34);
            Funktion.Uncheck(wertAufteilung, rbK41, rbK42, rbK43, rbK44);
            int wertChechbox4 = Funktion.WertVergeben(wertAufteilung, rbK41, rbK42, rbK43, rbK44);

            // Prüfen ob alle Radiobuttons betätigt wurden
            if (wertChechbox1 == 0 || wertChechbox2 == 0 || wertChechbox3 == 0 || wertChechbox4 == 0)
            {
                lblErrorRadioButton.Visible = true;
            }
            else
            {
                // Antwort Tabelle füllen
                string sqlInsert = "UPDATE Kleidungsgeschäfte_Antworten SET "
                    + "ID_Kleidungsgeschäfte ='" + kId + "'"
                    + ", Personal='" + wertChechbox1 + "'"
                    + ", DauerAnsprache = " + wertChechbox2 + ""
                    + ", Sortiment = '" + wertChechbox3 + "'"
                    + ", QualitätWare = '" + wertChechbox4 + "'"
                    + " WHERE FID = '" + fId + "'";
                OdbcCommand cmdInsert = new OdbcCommand(sqlInsert, connection);
                OdbcCommand cmdMeinung;
                if (txtNeuEigeneMeinung.Visible == true) cmdMeinung = new OdbcCommand("UPDATE Fragebogen SET EigeneMeinung='" + txtNeuEigeneMeinung.Text + "' WHERE ID_Fragebogen='" + fId + "'", connection);
                else cmdMeinung = new OdbcCommand("UPDATE Fragebogen SET EigeneMeinung='" + txtKEigeneMeinung.Text + "' WHERE ID_Fragebogen='" + fId + "'", connection);

                connection.Open();
                cmdInsert.ExecuteNonQuery();
                cmdMeinung.ExecuteNonQuery();
                connection.Close();

                Response.Redirect("allgemeine_fragen.aspx?" + (Page.Session["auswahlId"] = (Convert.ToString(Page.Session["ID"]))) + (Page.Session["oldSelection"] = (selectionKleidung)));
            }
        }

        protected void btnKAendernDD_Click(object sender, EventArgs e)
        {
            ddKleidungAuswahl.Visible = false;
            ddKleidungNeu.Visible = true;
            btnKAendernDD.Enabled = false;

            switch(selectionKleidung)
            {
                case "Mode":
                    ddKleidungNeu.Items.Add("Ann Christine");
                    ddKleidungNeu.Items.Add("Bonita");
                    ddKleidungNeu.Items.Add("C&A");
                    ddKleidungNeu.Items.Add("Calzedonia");
                    ddKleidungNeu.Items.Add("Camp David");
                    ddKleidungNeu.Items.Add("Colloseum");
                    ddKleidungNeu.Items.Add("Comma");
                    ddKleidungNeu.Items.Add("Foot Locker");
                    ddKleidungNeu.Items.Add("Hervis");
                    ddKleidungNeu.Items.Add("H&M");
                    ddKleidungNeu.Items.Add("Intimissimi");
                    ddKleidungNeu.Items.Add("Jack & Jones");
                    ddKleidungNeu.Items.Add("Jones");
                    ddKleidungNeu.Items.Add("KASTNER & ÖHLER");
                    ddKleidungNeu.Items.Add("New Yorker");
                    ddKleidungNeu.Items.Add("Northland");
                    ddKleidungNeu.Items.Add("Only");
                    ddKleidungNeu.Items.Add("Orsay");
                    ddKleidungNeu.Items.Add("Palmers");
                    ddKleidungNeu.Items.Add("s.Oliver");
                    ddKleidungNeu.Items.Add("Tom Tailor");
                    ddKleidungNeu.Items.Add("Tom Tailor Denim");
                    ddKleidungNeu.Items.Add("Triumph");
                    ddKleidungNeu.Items.Add("Vero Moda");
                    break;
                case "Schuhe":
                    ddKleidungNeu.Items.Add("Deichmann");
                    ddKleidungNeu.Items.Add("Leder Hausmann");
                    ddKleidungNeu.Items.Add("HUMANIC");
                    ddKleidungNeu.Items.Add("Monzero");
                    break;
            }
        }

        protected void btnAendernEigeneM_Click(object sender, EventArgs e)
        {
            txtKEigeneMeinung.Visible = false;
            txtNeuEigeneMeinung.Visible = true;
            btnAendernEigeneM.Enabled = false;
        }
    }
}