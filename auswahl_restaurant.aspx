<%@ Page Title="" Language="C#" MasterPageFile="~/standard_layout.Master" AutoEventWireup="true" CodeBehind="auswahl_restaurant.aspx.cs" Inherits="kundenfeedbackanwendung_varena.auswahl_restaurant" %>
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
            height: 699px;
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
            left: 423px;
            width: 145px;
            height: 43px;
            bottom: 135px;
        }
        .zurueck_button {
            position: relative;
            left: 374px;
            width: 145px;
            height: 43px;
            bottom: 135px;
        }
        .error {
            font-family: Calibri;
            font-size: 20px;
            color: #FF0000;
            position: relative;
            left: 110px;
            bottom: 180px;
        }
        .div_fortschritt {
            background-image: url('60prozent.jpg');
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
        <asp:Label ID="Label3" runat="server" Text="Wählen Sie ein Unternehmen aus:" CssClass="test"></asp:Label><asp:DropDownList ID="ddRestaurantAuswahl" runat="server" Height="18px" Width="304px" style="margin-left: 15px"></asp:DropDownList><asp:DropDownList ID="ddRestaurantNeu" runat="server"  Height="18px" Width="304px" Visible="False" style="margin-left: 15px"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRAendernDD" runat="server" Text="Ändern" OnClick="btnRAendernDD_Click" Visible="True" Enabled="False" />
        <br />
        <br />
        <asp:Label ID="LabelR1" runat="server" Text="1. Wie zufrieden sind Sie mit den Kellnern:" CssClass="test"></asp:Label>
        <br />
        <br />
        <asp:RadioButton ID="rbR11" runat="server" Text="Sehr zufrieden" GroupName="Group1" CssClass="antwort"/>
&nbsp;<asp:RadioButton ID="rbR12" runat="server" Text="Zufrieden" GroupName="Group1" CssClass="antwort2"/>
&nbsp;<asp:RadioButton ID="rbR13" runat="server" Text="Wenig zufrieden" GroupName="Group1" CssClass="antwort2"/>
&nbsp;<asp:RadioButton ID="rbR14" runat="server" Text="Nicht zufrieden" GroupName="Group1" CssClass="antwort2"/>
        <br />
        <br />
        <asp:Label ID="LabelR2" runat="server" Text="2. Wie lange dauerte es, bis ein Kellner Ihre Bestellung aufnahm:" CssClass="test"></asp:Label>
        <br />
        <br />
        <asp:RadioButton ID="rbR21" runat="server" Text="Sofort" GroupName="Group2" CssClass="antwort"/>
&nbsp;<asp:RadioButton ID="rbR22" runat="server" Text="Kurze Wartedauer" GroupName="Group2" CssClass="antwort2"/>
&nbsp;<asp:RadioButton ID="rbR23" runat="server" Text="Lange Wartedauer" GroupName="Group2" CssClass="antwort2"/>
&nbsp;<asp:RadioButton ID="rbR24" runat="server" Text="Gar nicht" GroupName="Group2" CssClass="antwort2"/>
        <br />
        <br />
        <asp:Label ID="LabelR3" runat="server" Text="3. Wie zufrieden sind Sie mit dem Essen bzw. den Getränken:" CssClass="test"></asp:Label>
        <br />
        <br />
        <asp:RadioButton ID="rbR31" runat="server" Text="Sehr zufrieden" GroupName="Group3" CssClass="antwort"/>
&nbsp;<asp:RadioButton ID="rbR32" runat="server" Text="Zufrieden" GroupName="Group3" CssClass="antwort2"/>
&nbsp;<asp:RadioButton ID="rbR33" runat="server" Text="Wenig zufrieden" GroupName="Group3" CssClass="antwort2"/>
&nbsp;<asp:RadioButton ID="rbR34" runat="server" Text="Nicht zufrieden" GroupName="Group3" CssClass="antwort2"/>
        <br />
        <br />
        <asp:Label ID="LabelR4" runat="server" Text="4. Wie zufrieden sind Sie mit der Wartezeit auf das Essen bzw. die Getränke:" CssClass="test"></asp:Label>
        <br />
        <br />
        <asp:RadioButton ID="rbR41" runat="server" Text="Sehr zufrieden" GroupName="Group4" CssClass="antwort"/>
&nbsp;<asp:RadioButton ID="rbR42" runat="server" Text="Zufrieden" GroupName="Group4" CssClass="antwort2"/>
&nbsp;<asp:RadioButton ID="rbR43" runat="server" Text="Wenig zufrieden" GroupName="Group4" CssClass="antwort2"/>
&nbsp;<asp:RadioButton ID="rbR44" runat="server" Text="Nicht zufrieden" GroupName="Group4" CssClass="antwort2"/>
        <br />
        <br />
        <asp:Label ID="LabelREG" runat="server" Text="Eigene Meinung:" CssClass="test"></asp:Label>
        <br />
        <asp:TextBox ID="txtREigeneMeinung" runat="server" Height="45px" TextMode="MultiLine" Width="602px" Wrap="False" MaxLength="10000" Visible="True"></asp:TextBox><asp:TextBox ID="txtNeuEigeneMeinung" runat="server" Height="45px" Width="602px" TextMode="MultiLine" MaxLength="10000" Visible="False"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAendernEigeneM" runat="server" Text="Ändern" Enabled="False" OnClick="btnAendernEigeneM_Click" />
    </div>
    <asp:Button ID="btnZurueckRestaurant" runat="server" Text="Zurück" CssClass="zurueck_button" OnClick="btnZurueckRestaurant_Click" />
    <asp:Button ID="btnWeiterRestaurant" runat="server" Text="Weiter" CssClass="weiter_button" OnClick="btnWeiterRestaurant_Click" />
    <asp:Label ID="lblErrorRadioButton" runat="server" Text="Bitte beantworten Sie alle Fragen!" CssClass="error" Visible="False"></asp:Label>
</asp:Content>
