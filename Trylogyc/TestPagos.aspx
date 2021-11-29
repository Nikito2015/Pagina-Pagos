<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TestPagos.aspx.vb" Inherits="Trylogyc.TestPagos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height:100%">
    <iframe id="frame" style="height:100%;width:100%"></iframe>
    <section id="pagar">
        <form runat="server">
            <asp:HiddenField runat="server" ID="preferenceID"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="defaultUrl"></asp:HiddenField>
        </form>
         <a id="mpRedirect" href="<%response.Write(preferenceID.Value) %>"></a>
    </section>
 
    <script src="scripts/jquery-3.1.1.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
      
            var iframe = $("#frame");
            $("#frame").attr("src", $("#mpRedirect").attr('href')); 
        });
        window.setTimeout(function () {
            window.location.href = '<%= defaultUrl.Value %>';
        }, 600000);

    </script>
</body></html>
