<%@ Page Title="" Language="C#" MasterPageFile="~/standard_layout.Master" AutoEventWireup="true" CodeBehind="allgemeine_fragen.aspx.cs" Inherits="kundenfeedbackanwendung_varena.allgemeine_fragen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <style type="text/css">
        .test {
            font-family: Calibri;
            font-size: 20px;
            color: #000066;
        }
        .antwort {
            font-family: Calibri;
            font-size: 17px;
            color: #000066;
        }
        .antwort2 {
            font-family: Calibri;
            font-size: 17px;
            color: #000066;
            margin-left: 10px;
        }
        .div_fragenLinks {
            position: relative;
            top: 70px;
            left: 200px;
            width: 1104px;
            height: 605px;
        }
        .div_fragenRechts {
            position: relative;
            top: -515px;
            left: 591px;
            width: 400px;
        }
        .fortschritt {
            width: 200px;
            height: 30px;
            left: 345px;
            position: absolute;
            font-family: Calibri;
            font-size: 36px;
            color: #000066;
            top: 108px;
        }
        .weiter_button {
            position: relative;
            left: 379px;
            width: 145px;
            height: 43px;
            bottom: 80px;
        }
        .zurueck_button {
            position: relative;
            left: 330px;
            width: 145px;
            height: 43px;
            bottom: 80px;
        }
        .error {
            font-family: Calibri;
            font-size: 20px;
            color: #FF0000;
            position: relative;
            left: 56px;
            bottom: 120px;
        }
        .div_fortschritt {
            background-image: url('90prozent.jpg');
            position: relative;
            top: 34px;
            left: 520px;
            height: 22px;
            width: 426px;
        }
    </style>
    <p class="fortschritt">Fortschritt:</p>
    <div class="div_fortschritt"></div>
    <div class="div_fragenLinks">
        <asp:Label ID="LabelA1" runat="server" Text="Am Ende noch drei allgemeine Fragen über die VARENA:" CssClass="test"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelA2" runat="server" Text="1. Was gefällt Ihnen an der VARENA am besten:" CssClass="test"></asp:Label>
        <asp:RadioButtonList ID="rblAllgemein1" runat="server" RepeatDirection="Horizontal" CssClass="antwort2">
            <asp:ListItem Value="1">Geographische Lage</asp:ListItem>
            <asp:ListItem Value="2">Angebot an Geschäften</asp:ListItem>
            <asp:ListItem Value="3">Veranstaltungen</asp:ListItem>
            <asp:ListItem Value="4">Personal</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:Label ID="LabelA3" runat="server" Text="2. Wie zufrieden sind Sie mit dem Parkplatzangebot:" CssClass="test"></asp:Label>
        <asp:RadioButtonList ID="rblAllgemein2" runat="server" RepeatDirection="Horizontal" CssClass="antwort2">
            <asp:ListItem Value="1">Sehr zufrieden</asp:ListItem>
            <asp:ListItem Value="2">Zufrieden</asp:ListItem>
            <asp:ListItem Value="3">Wenig zufrieden</asp:ListItem>
            <asp:ListItem Value="4">Nicht zufrieden</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:Label ID="LabelA4" runat="server" Text="Welche zukünftigen Veranstaltung(en) wünschen Sie sich:" CssClass="test"></asp:Label>
        <br />
        <asp:TextBox ID="txtVeranstaltung" runat="server" Height="70px" TextMode="MultiLine" Width="602px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="LabelA5" runat="server" Text="Für eine Rückmeldung der VARENA an Sie, geben Sie hier Ihre Daten an (optional):" CssClass="test"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelA6" runat="server" Text="Nachname: " CssClass="test"></asp:Label><asp:TextBox ID="txtNachname" runat="server" style="margin-left: 13px" Width="202px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelA7" runat="server" Text="Vorname: " CssClass="test"></asp:Label>&nbsp;<asp:TextBox ID="txtVorname" runat="server" style="margin-left: 73px" Width="202px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="LabelA8" runat="server" Text="E-Mail: " CssClass="test"></asp:Label><asp:TextBox ID="txtEmail" runat="server" style="margin-left: 48px" Width="202px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Label ID="LabelA9" runat="server" Text="Telefonnummer: " CssClass="test"></asp:Label><asp:TextBox ID="txtTele" runat="server" style="margin-left: 15px" Width="202px"></asp:TextBox>
    </div>
    <asp:Button ID="btnAllgemeinZurueck" runat="server" Text="Zurück" CssClass="zurueck_button" OnClick="btnAllgemeinZurueck_Click" />
    <asp:Button ID="Button1" runat="server" Text="Fertig" CssClass="weiter_button" OnClick="Button1_Click"/>
    <asp:Label ID="lblError" runat="server" Text="Bitte beantworten Sie alle Fragen!" CssClass="error" Visible="False"></asp:Label>
    <asp:Label ID="lblErrorKontakt" runat="server" Text="Bitte füllen Sie alle Felder der Kontaktaufnahme aus oder keines!" CssClass="error" Visible="False"></asp:Label>
    <asp:Label ID="lblErrorTele" runat="server" Text="Bitte fügen Sie bei der Telefonnummer nur Ziffern ein!" CssClass="error" Visible="False"></asp:Label>
    <asp:Label ID="lblErrorEmail" runat="server" Text="Bitte geben sie eine gültige E-Mail Adresse ein!" CssClass="error" Visible="False"></asp:Label>
</asp:Content>
