<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Diff.aspx.cs" Inherits="LocalEntitySearch.Diff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Local Entity Diff</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="SearchPanel" runat="server" DefaultButton="Search">
        <asp:HyperLink ID="Home" runat="server" Visible="True" NavigateUrl="/Diff.aspx">Home</asp:HyperLink>
        <span style="margin: 0px 0px 0px 20px;"></span>
        <asp:HyperLink ID="LocalEntitySearch" runat="server" Visible="True" NavigateUrl="/Test.aspx">Local Entity Search</asp:HyperLink>
        <span style="margin: 0px 0px 0px 20px;"></span>
        <br/>
        <asp:Label ID="lblMarket" runat="server" Text="Market:"></asp:Label>
        <span style="margin: 0px 0px 0px 20px;"></span>
        
        <asp:DropDownList ID="Market" runat="server">
            <asp:ListItem Selected="True">zh-HK</asp:ListItem>
            <asp:ListItem>zh-TW</asp:ListItem>
        </asp:DropDownList>
        <span style="margin: 0px 0px 0px 20px;"></span>
            
        <asp:CheckBox ID="cbRandom" runat="server" Text="Random" />
        <span style="margin: 0px 0px 0px 20px;"></span>
        
        <asp:Button ID="Search" runat="server" Text="View Diff" OnClick="Search_Click" />
            <asp:CheckBoxList ID="columnsCheckBoxList" runat="server">
                <asp:ListItem Selected="True">EntityName</asp:ListItem>
                <asp:ListItem Selected="True">CombinedAddress</asp:ListItem>
                <asp:ListItem>LatLong</asp:ListItem>
                <asp:ListItem>PhoneNumber</asp:ListItem>
                <asp:ListItem>Website</asp:ListItem>
                <asp:ListItem>Odptitle</asp:ListItem>
                <asp:ListItem>Odpdescription</asp:ListItem>
                <asp:ListItem>FeedsMulti8</asp:ListItem>
                <asp:ListItem>FeedsMulti9</asp:ListItem>
                <asp:ListItem>AUTB</asp:ListItem>
            </asp:CheckBoxList>
        </asp:Panel>
    </div>
        <br/>
        <br/>
        <asp:Label ID="SearchResultCnt" runat="server" Text=""></asp:Label>
        
        <asp:HyperLink ID="Begin" runat="server" Visible="False" style="margin: 0px 0px 0px 250px;">|<</asp:HyperLink>
        <span style="margin: 0px 0px 0px 20px;"></span>
        <asp:HyperLink ID="Previous" runat="server" Visible="False"><</asp:HyperLink>
        <span style="margin: 0px 0px 0px 20px;"></span>
        <asp:HyperLink ID="Next" runat="server" Visible="False">></asp:HyperLink>
        <span style="margin: 0px 0px 0px 20px;"></span>
        <asp:HyperLink ID="End" runat="server" Visible="False">>|</asp:HyperLink>

        <br/>
        <br/>
        <br/>
        <%--<asp:GridView ID="SearchResultTable" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="304px">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>--%>
        
        <asp:Table ID="SearchResultTable" runat="server" Caption="Entity Diff" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Left" Visible="False">
        </asp:Table>
    </form>
</body>
</html>
