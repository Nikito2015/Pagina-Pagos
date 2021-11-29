<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PagoRechazado.aspx.vb" Inherits="Trylogyc.PagoRechazado" MasterPageFile="~/Trylogyc.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h2>Su pago ha sido rechazado.</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title"><span><i class="fa fa-danger"></i></span>Pago Rechazado</h3>
                </div>
                <div class="box-body with-border">
                    <div class="row">
                        <div class="col-md-12 col-xs-12">
                            <div class="form-group">
                                <h4 style="color: darkslategray">Su pago ha sido rechazado. </h4>
                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
