<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home-Attribute-Injection.aspx.cs" Inherits="MaH.IOC.SampleWeb.HomeAttributeInjection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            In this page we used Inject attribute to resolve services
        </div>
        <div>
            <asp:Repeater ID="StringRepeater" runat="server">
                <ItemTemplate>
                    <p><%# Container.DataItem %></p>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
