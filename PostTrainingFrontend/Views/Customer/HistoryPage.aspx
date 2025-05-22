<%@ Page Title="Cyberware Store" Language="C#" MasterPageFile="~/Layouts/Header.Master" AutoEventWireup="true" CodeBehind="HistoryPage.aspx.cs" Inherits="PostTrainingFrontend.Views.Customer.HistoryPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="repeater_transactions" runat="server">
        <ItemTemplate>
            <h3>Transaction ID: <%# Eval("TransactionId") %> — Date: <%# Eval("OrderDate") %></h3>
            <asp:GridView ID="gridview_items" runat="server" AutoGenerateColumns="False" DataSource='<%# Eval("Items") %>'>
                <Columns>
                    <asp:BoundField DataField="ProductName" HeaderText="Product" />
                    <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>
            <div>
                <asp:Label runat="server" Text='<%# "Total: " + string.Format("{0:C}", Eval("Total")) %>'></asp:Label>
            </div>
            <hr />
        </ItemTemplate>
    </asp:Repeater>
    <div>
        <asp:Label ID="label_error" runat="server" Text="Error"></asp:Label>
    </div>
</asp:Content>
