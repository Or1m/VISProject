<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GamePage.aspx.cs" Inherits="WebApp.GamePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="form2" runat="server">
            <p>Name:</p>
            <p><asp:TextBox ID="textbox1" CssClass="form-control" runat="server" /></p>
            <span><asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Log in" OnClick="Button1_Click" /></span>
        
            <span style="margin-left:10px"><asp:Label runat="server" ID="labelResults"></asp:Label></span>

            <p style="margin-top:20px">
                <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server"></asp:DropDownList>
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </p>
            <p>
                <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="Add review" OnClick="Button2_Click" />
            </p>

    </form>
</asp:Content>
