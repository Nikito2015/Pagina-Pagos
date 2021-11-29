<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Cliente.Master" CodeBehind="Ficha_Equipos_Report.aspx.vb" Inherits="MTPlanner.Ficha_Equipos_Report" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;">
        <tr>
            <td>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="#CCCCCC" 
                    Font-Names="Verdana" Font-Size="8pt" Height="518px" 
                    InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" 
                    WaitMessageFont-Size="14pt" Width="757px">
                    <LocalReport ReportPath="js\Report2.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                    SelectMethod="GetData" 
                    TypeName="MTPlanner.MantenimientoDataSetTableAdapters.EquiposTableAdapter">
                </asp:ObjectDataSource>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
