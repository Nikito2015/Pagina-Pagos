<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Trylogyc.Master" CodeBehind="Error.aspx.vb" Inherits="Trylogyc._Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h2>Se ha Producido un Error</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-info">
                <div class="box-header with-border">
                    <h3 class="box-title"><span><i class="fa fa-warning"></i></span>Código de Error</h3>
                </div>
                <div class="box-body with-border">
                    <div class="row">
                        <div class="col-md-12 col-xs-12">
                            <div class="form-group">
                                <h4 style="color: #CC0000">Texto del Error</h4>
                                <asp:Label ID="Label1" runat="server" Text="Se ha producido un error. Por favor intente nuevamente." Style="font-weight: bold; color: #CC0000"></asp:Label>
                                <asp:Label ID="Label2" runat="server" Text="Se ha producido un error. Por favor intente nuevamente." Style="color: #CC0000; display: none"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
