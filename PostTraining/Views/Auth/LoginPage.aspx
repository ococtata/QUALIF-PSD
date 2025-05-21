<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="PostTraining.Views.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cyberware Store</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Login</h1>
        <div>
            <div>
                <asp:Label ID="label_email" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="textbox_email" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="label_password" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="textbox_password" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                <asp:CheckBox ID="checkbox_remember" runat="server" Text="Remember me" />
            </div>
            <div>
                <asp:Label ID="label_error" runat="server" Text="Error"></asp:Label>
            </div>
            <div>
                <asp:Button ID="button_login" OnClick="button_login_Click" runat="server" Text="Login" />
            </div>
            <div>
                <asp:Label ID="label_lregister" runat="server" Text="Don't have an account?"></asp:Label>
                <asp:LinkButton ID="lbutton_register" runat="server" OnClick="lbutton_register_Click">Register Here!</asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
