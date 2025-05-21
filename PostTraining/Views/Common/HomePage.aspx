<%@ Page Title="Cyberware Store" Language="C#" MasterPageFile="~/Views/Layouts/Header.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="PostTraining.Views.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1 id="header" runat="server"></h1>
        <asp:GridView ID="gridview_product" runat="server" AutoGenerateColumns="False" 
            OnRowDeleting="gridview_product_RowDeleting" OnRowEditing="gridview_product_RowEditing"
            OnRowCommand="gridview_product_RowCommand">

            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Product Id" SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Tier" HeaderText="Tier" SortExpression="Tier" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                <asp:BoundField DataField="Desc" HeaderText="Description" SortExpression="Desc" />
                <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                
                <asp:TemplateField HeaderText="Actions" Visible="false">
                    <ItemTemplate>
                        <asp:Button ID="button_edit" runat="server" Text="Edit"
                            UseSubmitBehavior="false" CommandName="Edit"/>
                        <asp:Button ID="button_delete" runat="server" Text="Delete" 
                            UseSubmitBehavior="false" CommandName="Delete"/>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Actions" Visible="false">
                    <ItemTemplate>
                        <asp:Button ID="button_add" runat="server" Text="Add to Cart"
                            UseSubmitBehavior="false" CommandName="Edit"/>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
