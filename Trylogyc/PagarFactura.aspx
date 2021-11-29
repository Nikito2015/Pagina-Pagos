<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Trylogyc.Master" CodeBehind="PagarFactura.aspx.vb" Inherits="Trylogyc.PagarFactura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <div class="row">
        <div class="col-md-4">
            <asp:HiddenField runat="server" ID="preferenceID"></asp:HiddenField>
            <asp:Button runat="server" ID="btnRegresar" CssClass="btn btn-lg btn-primary block" Text="Regresar" />
        </div>
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section id="pagarScroll" style="color: transparent !important">
        <script
            src="https://www.mercadopago.com.ar/integrations/v1/web-payment-checkout.js"
            data-preference-id="<%response.Write(preferenceID.Value) %>">
        </script>


        <script type="text/javascript">
            $(document).ready(function () {
                $(".mercadopago-button").css('width', '1');
                $(".mercadopago-button").css('height', '1');
                $(".mercadopago-button").click();
            });


        </script>
    </section>
</asp:Content>
