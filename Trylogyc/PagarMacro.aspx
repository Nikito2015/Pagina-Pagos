<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PagarMacro.aspx.vb" Inherits="Trylogyc.PagarMacro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="height:100%">
<head runat="server">	
		<meta name="referrer" content="no-referrer"/>
		<title></title>
		<link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
		<style type="text/css">
			.block {
				display: block;
				width: 50%;
				border: none;
				padding: 14px 28px;
				font-size: 16px;
				cursor: pointer;
				text-align: center;
			}
		</style>
</head>
    <body style="height:100%">	
        <form id="form" action="#" method="post" runat="server">	
			<asp:HiddenField runat="server" ID="urlEnvio"></asp:HiddenField>
			<asp:HiddenField runat="server" ID="defaultUrl"></asp:HiddenField>

			<input type="hidden" id="CallbackSuccess" value='<%= CallbackSuccess.value() %>' runat="server"/> 
			<input type="hidden" id="CallbackCancel" value='<%= CallbackCancel.value() %>' runat="server"/>
			<input type="hidden" id="CallbackPending" value='<%= CallbackPending.value() %>' runat="server"/>
			<input type="hidden" id="Comercio" value='<%= Comercio.value() %>' runat="server"/>
			<input type="hidden" id="SucursalComercio" value='<%= SucursalComercio.value() %>' runat="server"/>
			<input type="hidden" id="Hash" value='<%= Hash.value() %>' runat="server"/>
			<input type="hidden" id="TransaccionComercioID" value='<%= TransaccionComercioID.value() %>' runat="server"/>						
		
			<input type="hidden" id="Monto" value='<%= Monto.value() %>' runat="server"/>	
			<input type="hidden" name="Producto[0]" id="Producto" value='<%= Producto.value() %>' runat="server" /> 
			<input type="hidden" name="MontoProducto[0]" id="Importe" value='<%= Importe.value() %>' runat="server"/>	
			<button type="submit" name='btnEnviar' value='Enviar' id='btnEnviar' style="display:none"></button>

			<script src="scripts/jquery-3.1.1.js" type="text/javascript"></script>
			<script src="js/bootstrap.min.js" type="text/javascript"></script>
			<script type="text/javascript">                   
                var action = '<%= urlEnvio.Value() %>';
                $("#form").attr("action", action);
			</script> 		
			 <script type="text/javascript">                   
				 document.getElementById('btnEnviar').click()		
		     </script> 
		</form>
    </body>
</html>