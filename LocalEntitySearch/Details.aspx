<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="LocalEntitySearch.Local" %>

<!DOCTYPE html>
<style type="text/css">
    #QRcode { position: absolute;left: 400px;top:305px}
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <asp:Image ID="QRcode" Width="200" Height="200" runat="server"/>
    <div>
        <%--<iframe name="myIframe" id="myIframe" width="1500px" height="800px" runat =server src=""></iframe>--%>
        <iframe name="myIframe" id="myIframe" style="width:100%;height: 900px" runat =server src=""></iframe>
        
    </div>
        
    </form>
</body>
</html>
