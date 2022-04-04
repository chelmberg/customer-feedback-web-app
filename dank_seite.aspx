<%@ Page Title="" Language="C#" MasterPageFile="~/standard_layout.Master" AutoEventWireup="true" CodeBehind="dank_seite.aspx.cs" Inherits="kundenfeedbackanwendung_varena.dank_seite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .danke {
    font-family: Calibri;
    font-size: 50px;
    color: #000066;
    position: relative;
    left: 70px;
    top: 30px;
            width: 1120px;
        }
    .restText {
    font-family: Calibri;
    font-size: 30px;
    color: #000066;
    position: relative;
    left: 120px;
    top: 40px;
    }
</style>
    <%--<asp:Label ID="lblDanke" runat="server" Text="Danke für Ihr Feedback!" CssClass="danke"></asp:Label>--%>
    <p class="danke">&nbsp;&nbsp; Danke, dass Sie unsere Diplomarbeit<br />
        sowie die VARENA mit Ihrem Feedback<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; unterstützten!</p>
    <br />
    <p class="restText">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sie können die Seite nun schließen.</p>
    <%--<asp:Label ID="Label1" runat="server" Text="Sie können die Seite nun schließen." CssClass="restText"></asp:Label>--%>
</asp:Content>
