<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="DataDrivenApp._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="questionNumLabel" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="questionLabel" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <br />
        <asp:Button ID="nextBtn" runat="server" onclick="nextQuestion" Text="Next" />
        <br />
        <br />
        <asp:GridView ID="testGridView" runat="server">
        </asp:GridView>
        <br />
    
    </div>
    </form>
</body>
</html>
