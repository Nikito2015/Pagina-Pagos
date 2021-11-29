<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PagoExitoso.aspx.vb" Inherits="Trylogyc.PagoExitoso" MasterPageFile="~/Trylogyc.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h2>Pago Exitoso!</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-success">
                <div class="box-header with-border">
                    <h3 class="box-title"><span><i class="fa fa-success"></i></span>Pago Procesado Exitosamente!</h3>
                </div>
                <div class="box-body with-border">
                    <div class="row">
                        <div class="col-md-12 col-xs-12">
                            <div class="form-group">
                                <h4 style="color: darkslategray">Su pago se procesó exitosamente. El mismo será acreditado e informado desde el día habil siguiente </h4>
                                <h4 style="color: darkslategray">Nro. de Comprobante: <strong><asp:Label runat="server" ID="lblComprobante" Text=""></asp:Label></strong></h4>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
