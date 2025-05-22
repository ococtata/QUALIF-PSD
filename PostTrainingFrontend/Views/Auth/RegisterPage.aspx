<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="PostTrainingFrontend.Views.Auth.RegisterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cyberware Store</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Register Page</h1>
        <div>
            <div>
                <asp:Label ID="label_name" runat="server" Text="Name"></asp:Label>
                <asp:TextBox ID="textbox_name" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="label_email" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="textbox_email" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="label_password" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="textbox_password" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="label_error" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <asp:Button ID="button_register" OnClick="button_register_Click" runat="server" Text="Register" />
            </div>
            <div>
                <asp:Label ID="label_llogin" runat="server" Text="Already have an account?"></asp:Label>
                <asp:LinkButton ID="lbutton_login" runat="server" OnClick="lbutton_login_Click">Login Here!</asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>