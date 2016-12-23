<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vote.aspx.cs" Inherits="Vote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
        .auto-style2 {
            width: 111px;
        }
        .auto-style3 {
            height: 23px;
            width: 111px;
        }
        .auto-style4 {
            width: 912px;
        }
        .auto-style5 {
            height: 23px;
            width: 912px;
        }
        .auto-style6 {
        }
        .auto-style7 {
            width: 256px;
            height: 23px;
        }
        #Text1 {
            width: 613px;
            height: 40px;
        }
        #ButtonVote {
            width: 109px;
        }
        .auto-style8 {
            width: 229px;
        }
        .auto-style9 {
            height: 23px;
            width: 229px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style4">
                <asp:Image runat="server" ImageUrl="~/Content/logo.png" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style4">
                <table style="width: 70%;">
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td class="auto-style8">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6"><strong>От кого</strong></td>
                        <td class="auto-style8"><strong>Кому</strong></td>
                    </tr>
                    <tr>
                        <td class="auto-style7">
                            <asp:Label ID="LabelFromEmployeeName" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style9">
                            <asp:Label ID="LabelToEmployeeName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="LabelFromEmployeeDep" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style8">
                            <asp:Label ID="LabelToEmployeeDep" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Image ID="ImageFromEmployee" ImageUrl="" runat="server" Height="114px" style="text-align: center" Width="118px" />
                        </td>
                        <td class="auto-style8">
                            <asp:Image ID="ImageToEmployee" ImageUrl="" runat="server" Height="127px" Width="97px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            &nbsp;</td>
                        <td class="auto-style8">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
    
                            <asp:Panel ID="PanelValues" runat="server" style="text-align: center">
                                <asp:RadioButtonList runat="server" AutoPostBack="False" Height="64px" RepeatColumns="2" style="text-align: left" Width="314px">
                                    <asp:ListItem Value="v1">Командный дух</asp:ListItem>
                                    <asp:ListItem Value="v2">Инновации</asp:ListItem>
                                    <asp:ListItem Value="v3">Вовлечённость</asp:ListItem>
                                    <asp:ListItem Value="v4">Ответственность</asp:ListItem>
                                </asp:RadioButtonList>
                            </asp:Panel>
    
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                          
                            <asp:TextBox ID="TextBoxComment" runat="server" MaxLength="1000" TextMode="MultiLine" Width="623px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            
                            &nbsp;</td>
                    </tr>
                   <tr>
                        <td class="auto-style6" colspan="2">
                           
                            <asp:Button ID="ButtonVote" runat="server" Text="Vote" OnClick="ButtonVote_Click" Enabled="False" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3"></td>
            <td class="auto-style5"></td>
            <td class="auto-style1"></td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
