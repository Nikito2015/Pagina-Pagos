<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PagoPendiente.aspx.vb" Inherits="Trylogyc.PagoPendiente" MasterPageFile="~/Trylogyc.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h2>Su pago se encuentra demorado.</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-warning">
                <div class="box-header with-border">
                    <h3 class="box-title"><span><i class="fa fa-warning"></i></span>Pago Demorado</h3>
                </div>
                <div class="box-body with-border">
                    <div class="row">
                        <div class="col-md-12 col-xs-12">
                            <div class="form-group">
                                <h4 style="color: darkslategray">Su pago se encuentra demorado. Agende el siguiente nro. de comrpobante de reclamo:</h4>
                                <h4 style="color: darkslategray"><strong><asp:Label ID="IdComprobante" runat="server"></asp:Label></strong></h4>
                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
