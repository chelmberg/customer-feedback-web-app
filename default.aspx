<%@ Page Title="" Language="C#" MasterPageFile="~/standard_layout.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="kundenfeedbackanwendung_varena._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
    .test {
    font-family: Calibri;
    font-size: 20px;
    color: #000066;
    }
</style>
    <p style="width: 100%; height: 291px;">
    <asp:Label ID="Label1" runat="server" CssClass="test" Text="Starten Sie mit dem Feedback:  "></asp:Label>
        <asp:Button ID="btnStart" runat="server" Height="30px" Text="Start" Width="100px" OnClick="btnStart_Click"/>
    <br />
</p>
</asp:Content>
