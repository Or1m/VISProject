<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GamePage.aspx.cs" Inherits="WebApp.GamePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="margin-top:50px">
        <form id="form2" runat="server">
            Name:
            <p><asp:TextBox ID="textbox1" CssClass="form-control" runat="server" ReadOnly="true"/></p>
            Description:
            <p><asp:TextBox ID="textbox2" CssClass="form-control" runat="server" ReadOnly="true"/></p>
            Developer:
            <p><asp:TextBox ID="textbox3" CssClass="form-control" runat="server" ReadOnly="true"/></p>
            Rating:
            <p><asp:TextBox ID="textbox4" CssClass="form-control" runat="server" ReadOnly="true"/></p>
            Release Date:
            <p><asp:TextBox ID="textbox5" CssClass="form-control" runat="server" ReadOnly="true"/></p>
            Average User Score:
            <p><asp:TextBox ID="textbox6" CssClass="form-control" runat="server" ReadOnly="true"/></p>
            Average Reviewer Score:
            <p><asp:TextBox ID="textbox7" CssClass="form-control" runat="server" ReadOnly="true"/></p>
        
            <span><asp:Button ID="Button1" CssClass="btn btn-success" runat="server" Text="Add review" OnClick="Button1_Click" /></span>
            <span><asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="Add game to favorite" OnClick="Button2_Click" /></span>
        </form>
    </div>

</asp:Content>
