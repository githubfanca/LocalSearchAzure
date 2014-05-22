<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Serp.aspx.cs" Inherits="LocalEntitySearch.Serp" %>

<!DOCTYPE html>
<style type="text/css">
    #QRcode { position: absolute;left: 960px;top:335px}
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <iframe name="myIframe" id="myIframe" style="width:100%;height: 900px" runat =server src=""></iframe>
        <asp:Image ID="QRcode" Width="150" Height="150" runat="server"/>
    </div>
    </form>
</body>
</html>
