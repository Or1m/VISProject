<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddReviewPage.aspx.cs" Inherits="WebApp.AddReviewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top:50px">
        <form id="form3" runat="server">
            Title:
            <p><asp:TextBox ID="textbox1" CssClass="form-control" runat="server"/></p>
            Score:
            <p><asp:DropDownList ID="DropDown2" CssClass="form-control" runat="server"/></p>
            Order of Review:
            <p><asp:DropDownList ID="DropDown3" CssClass="form-control" runat="server"/></p>
        
            <span><asp:Button ID="Button1" CssClass="btn btn-success" runat="server" Text="Add review" OnClick="Button1_Click" /></span>
        </form>
    </div>
</asp:Content>
