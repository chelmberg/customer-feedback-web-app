<%@ Page Title="" Language="C#" MasterPageFile="~/standard_layout.Master" AutoEventWireup="true" CodeBehind="benutzerfragen.aspx.cs" Inherits="kundenfeedbackanwendung_varena.benutzerfragen" %>
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
        top: 90px;
        left: 200px;
        width: 881px;
        height: 605px;
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
        left: 568px;
        width: 145px;
        height: 43px;
        bottom: 93px;
    }
    .zurueck_button {
        position: relative;
        left: 229px;
        width: 145px;
        height: 43px;
        bottom: 93px;
    }
    .error {
        font-family: Calibri;
        font-size: 20px;
        color: #FF0000;
        position: relative;
        left: 110px;
        bottom: 138px;
        }
    .div_fortschritt {
        background-image: url('30prozent.jpg');
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
        <asp:Label ID="Label3" runat="server" CssClass="test" Text="Ihr Geschlecht:"></asp:Label>
        <asp:RadioButtonList ID="rblGeschlecht" runat="server" CssClass="antwort" RepeatDirection="Horizontal" Height="28px" Width="201px">
            <asp:ListItem Value="m">Männlich</asp:ListItem><asp:ListItem Value="w">Weiblich</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:Label ID="Label4" runat="server" CssClass="test" Text="Altersbereich:"></asp:Label>
        <asp:RadioButtonList ID="rblAlter" runat="server" CssClass="antwort" RepeatDirection="Horizontal" Height="27px" Width="526px">
            <asp:ListItem Value="1">Unter 16 Jahre</asp:ListItem>
            <asp:ListItem Value="2">16-29 Jahre</asp:ListItem>
            <asp:ListItem Value="3">30-45 Jahre</asp:ListItem>
            <asp:ListItem Value="4">46 Jahre oder älter</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:Label ID="Label5" runat="server" CssClass="test" Text="Woher kommen Sie:"></asp:Label>
        <br />
        &nbsp;<asp:Label ID="Label6" runat="server" CssClass="test" Text="PLZ: "></asp:Label>
        <asp:TextBox ID="txtPLZ" runat="server" CssClass="antwort" Height="17px" Width="102px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Personen im Haushalt:" CssClass="test"></asp:Label>
        <asp:RadioButtonList ID="rblHaushalt" runat="server" CssClass="antwort" RepeatDirection="Horizontal" Width="834px" Height="16px">
            <asp:ListItem Value="1">Single Haushalt</asp:ListItem>
            <asp:ListItem Value="2">2 Personen-Haushalt</asp:ListItem>
            <asp:ListItem Value="3">3 Personen-Haushalt</asp:ListItem>
            <asp:ListItem Value="4">4 Personen-Haushalt</asp:ListItem>
            <asp:ListItem Value="5">5 oder mehr</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Wie oft besuchen Sie die VARENA:" CssClass="test"></asp:Label>
        <asp:RadioButtonList ID="rblIntervall" runat="server" CssClass="antwort" RepeatDirection="Horizontal" Height="25px" Width="603px">
            <asp:ListItem Value="1">Wöchentlich</asp:ListItem>
            <asp:ListItem Value="2">2-3 mal im Monat</asp:ListItem>
            <asp:ListItem Value="3">Einmal im Monat</asp:ListItem>
            <asp:ListItem Value="4">Seltener</asp:ListItem>
            <asp:ListItem Value="5">Gar nicht</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:Label ID="Label8" runat="server" CssClass="test" Text="Was möchten Sie bewerten:"></asp:Label>
        <asp:DropDownList ID="ddAuswahl" runat="server" Height="20px" Width="313px">
            <asp:ListItem Value="Buecher">Bücher/Schreib- und Spielwaren</asp:ListItem>
            <asp:ListItem Value="Dienstleistung">Bank</asp:ListItem>
            <asp:ListItem Value="Drogerie">Drogerie/Parfümerie</asp:ListItem>
            <asp:ListItem Value="Friseur">Friseur/Haarkosmetik/Nageldesign</asp:ListItem>
            <asp:ListItem Value="Gastronomie">Gastronomie/Imbiss</asp:ListItem>
            <asp:ListItem>Kinderwelt</asp:ListItem>
            <asp:ListItem Value="Lebensmittel">Lebensmittel</asp:ListItem>
            <asp:ListItem Value="Mode">Mode/Sport</asp:ListItem>
            <asp:ListItem Value="Schuhe">Schuhe/Lederwaren</asp:ListItem>
            <asp:ListItem Value="Technik">Technik/Optik/Telekommunikation</asp:ListItem>
            <asp:ListItem Value="Uhren">Uhren/Schmuck/Geschenke</asp:ListItem>
            <asp:ListItem Value="Wohnen">Wohnen/Reisen/Freizeit</asp:ListItem>
        </asp:DropDownList>
    </div>
    <asp:Button ID="btnWeiter" runat="server" Text="Weiter" CssClass="weiter_button" OnClick="btnWeiter_Click" />
    <asp:Button ID="btnZurueck" runat="server" Text="Zurück" CssClass="zurueck_button" OnClick="btnZurueck_Click" Enabled="False" />
    <asp:Label ID="lblError" runat="server" Text="Bitte beantworten Sie alle Fragen!" CssClass="error" Visible="False"></asp:Label>
    <asp:Label ID="lblErrorPLZ" runat="server" Text="Bitte geben Sie eine gültige Postleitzahl ein!" CssClass="error" Visible="False"></asp:Label>
</asp:Content>
