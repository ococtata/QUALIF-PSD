<%@ Page Title="Cyberware Store" Language="C#" MasterPageFile="~/Views/Layouts/Header.Master" AutoEventWireup="true" CodeBehind="AddProductPage.aspx.cs" Inherits="PostTraining.Views.Admin.AddProductPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Add Product Page</h1>
    <div>
        <div>
            <asp:Label ID="label_name" runat="server" Text="Name"></asp:Label>
            <asp:TextBox ID="textbox_name" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="label_tier" runat="server" Text="Tier"></asp:Label>
            <asp:TextBox ID="textbox_tier" runat="server" TextMode="Number"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="label_price" runat="server" Text="Price"></asp:Label>
            <asp:TextBox ID="textbox_price" runat="server" TextMode="Number"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="label_description" runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="textbox_description" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="label_type" runat="server" Text="Type"></asp:Label>
            <asp:TextBox ID="textbox_type" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="label_stock" runat="server" Text="Stock"></asp:Label>
            <asp:TextBox ID="textbox_stock" runat="server" TextMode="Number"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="label_error" runat="server"></asp:Label>
        </div>
        <div>
            <asp:Button ID="button_cancel" runat="server" Text="Cancel" OnClick="button_cancel_Click" />
            <asp:Button ID="button_add" runat="server" Text="Add" OnClick="button_add_Click" />
        </div>
    </div>
</asp:Content>
