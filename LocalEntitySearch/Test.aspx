<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="LocalEntitySearch.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LDP Entity Search</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Panel ID="SearchPanel" runat="server" DefaultButton="Search">
        <asp:HyperLink ID="Home" runat="server" Visible="True" NavigateUrl="/Test.aspx">Home</asp:HyperLink>
        <span style="margin: 0px 0px 0px 20px;"></span>
        <asp:HyperLink ID="LocalEntityDiff" runat="server" Visible="True" NavigateUrl="/Diff.aspx">Local Entity Diff</asp:HyperLink>
        <span style="margin: 0px 0px 0px 20px;"></span>
        <br/>
        <asp:Label ID="lblMarket" runat="server" Text="Market:"></asp:Label>
        <span style="margin: 0px 0px 0px 20px;"></span>
        
        <asp:DropDownList ID="Market" runat="server">
            <asp:ListItem Selected="True">zh-HK</asp:ListItem>
            <asp:ListItem>zh-TW</asp:ListItem>
            <asp:ListItem>en-SG</asp:ListItem>
            <asp:ListItem>id-ID</asp:ListItem>
            <asp:ListItem>zh-CN</asp:ListItem>
        </asp:DropDownList>
        <span style="margin: 0px 0px 0px 20px;"></span>
        <asp:Label ID="lblQuery" runat="server" Text="Name:"></asp:Label>
        <span style="margin: 0px 0px 0px 10px;"></span>
        

        <asp:TextBox ID="EntityName" runat="server" Width="200"></asp:TextBox>
        <span style="margin: 0px 0px 0px 20px;"></span>
        <asp:Label ID="lblAddress" runat="server" Text="Address:"></asp:Label>
        <span style="margin: 0px 0px 0px 10px;"></span>
        <asp:TextBox ID="EntityAddress" runat="server" Width="100"></asp:TextBox>
        <span style="margin: 0px 0px 0px 20px;"></span>
            <asp:Label ID="lblCategory" runat="server" Text="Category:"></asp:Label>
        <span style="margin: 0px 0px 0px 10px;"></span>
        <asp:TextBox ID="EntityCategory" runat="server" Width="100"></asp:TextBox>
        <span style="margin: 0px 0px 0px 20px;"></span>
        <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click" />
        </asp:Panel>
        <br/>
        <br/>
        <asp:Label ID="SearchResultCnt" runat="server" Text=""></asp:Label>
        
        
        <asp:HyperLink ID="Begin" runat="server" Visible="False" style="margin: 0px 0px 0px 500px;">|<</asp:HyperLink>
        <span style="margin: 0px 0px 0px 20px;"></span>
        <asp:HyperLink ID="Previous" runat="server" Visible="False"><</asp:HyperLink>
        <span style="margin: 0px 0px 0px 20px;"></span>
        <asp:HyperLink ID="Next" runat="server" Visible="False">></asp:HyperLink>
        <span style="margin: 0px 0px 0px 20px;"></span>
        <asp:HyperLink ID="End" runat="server" Visible="False">>|</asp:HyperLink>

        <br/>
        <asp:Table ID="SearchResultTable" runat="server" Caption="Entity Search Result" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Left" Visible="False">
        </asp:Table>
        <br/>
        
    </form>
</body>
</html>
