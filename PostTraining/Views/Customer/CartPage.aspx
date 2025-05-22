<%@ Page Title="Cyberware Store" Language="C#" MasterPageFile="~/Views/Layouts/Header.Master" AutoEventWireup="true" CodeBehind="CartPage.aspx.cs" Inherits="PostTraining.Views.Customer.CartPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1 id="header" runat="server"></h1>
        <asp:GridView ID="gridview_cart" runat="server" AutoGenerateColumns="False" OnRowCommand="gridview_cart_RowCommand">
            <Columns>
                <asp:BoundField DataField="ProductId" HeaderText="Product Id" SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Tier" HeaderText="Tier" SortExpression="Tier" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                <asp:BoundField DataField="Desc" HeaderText="Description" SortExpression="Desc" />
                <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />

                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Button ID="btnDecrease" runat="server" Text="-" CommandName="Decrease" CommandArgument='<%# Eval("ProductId") %>' />
                        <asp:Label ID="label_quantity" runat="server" Text='<%# Eval("Quantity") %>' />
                        <asp:Button ID="btnIncrease" runat="server" Text="+" CommandName="Increase" CommandArgument='<%# Eval("ProductId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <br />

        <div>
            <asp:Label ID="label_error" runat="server" Text="Error"></asp:Label>
        </div>
    </div>
</asp:Content>
