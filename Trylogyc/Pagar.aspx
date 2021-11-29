<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Pagar.aspx.vb" Inherits="Trylogyc.Pagar" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml" style="height:100%">


<head runat="server">

    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
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
</body>

</html>

