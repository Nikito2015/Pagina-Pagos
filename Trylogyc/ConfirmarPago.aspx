<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConfirmarPago.aspx.vb" Inherits="Trylogyc.ConfirmarPago" MasterPageFile="~/Trylogyc.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h2>Atención!</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-success">
                <div class="box-header with-border">
                    <h3 class="box-title"><span><i class="fa fa-success"></i></span>Ya existe un pago en proceso de acreditación para el Comprobante seleccionado</h3>
                </div>
                <div class="box-body with-border">
                    <div class="row">
                        <div class="col-md-12 col-xs-12">
                            <div class="form-group">
                                <h4 style="color: darkslategray">Recuerde que los pagos se acreditan luego de las 24 horas hábiles.</h4>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-footer with-border">
                    <div class="row">
                        <div class="col-sm-6">
                            <asp:Button ID="btnVolver" runat="server" Text="Regresar" class="btn btn-default"  />
                            <asp:Button ID="btnContinuar" runat="server" Text="Continuar" class="btn btn-primary" Visible="False" />
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>
</asp:Content>
