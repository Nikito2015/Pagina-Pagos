<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Reestablecer.aspx.vb" Inherits="Trylogyc.Reestablecer" %>

<!DOCTYPE html>

<html style="overflow: visible !important">
<head>
    
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>Acceso Usuarios</title>

    <!-- jQuery 2.0.2 -->

    <!-- jQuery UI 1.10.3 -->

    <!-- Page specific script -->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
    <script src="js/main.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/jquery-ui-1.10.3.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.10.3.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->

    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->

    <!-- Bootstrap -->
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link href="css/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="css/footer-distributed-with-contact-form.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").hide(0).delay(10).fadeIn(300)
        });
</script>
    <style type="text/css">
        .btn1 {
            background-color: coral;
            color: white;
            -webkit-transition: 0.2s;
            -moz-transition: 0.2s;
            -ms-transition: 0.2s;
            -o-transition: 0.2s;
            transition: 0.2s;
        }

            .btn1:hover {
                background-color: orange;
            }
    </style>
</head>
<body class="bodydefault2" style="overflow-x: hidden !important; background-image: url(../images/back/abstract3.jpg)">

    <div class="login-form" id="login-box">
        <div class="header" style="background-color: coral"><i class="fa fa-envelope fa-2x" style="vertical-align: middle; color: white; background-color: coral">&nbsp</i>Recuperar Contraseña</div>
        <form id="contact_form" method="post" runat="server">
            <div class="form-group">
                <input name="codigo" class="form-control" type="number" placeholder="Código de Cliente" required />
            </div>
            <div class="form-group">
                <input type="email" name="email" class="form-control" placeholder="Email" title="email" required />
            </div>
            <div class="form-group">
                <asp:Label ID="lblError" runat="server" Text="No Existe Usuario" CssClass="text-red" Visible="False"></asp:Label>

            </div>
            <div class="footer">
                <asp:Button ID="btnRegister" CssClass="btn btn-block btn1" runat="server" Text="Enviar"></asp:Button>
            </div>
            <div class="pull-left">
                <a href="login.aspx" class="text-center">Volver a Login</a>
            </div>
        </form>


    </div>
    <asp:SqlDataSource ID="sqlexito" runat="server"></asp:SqlDataSource>
</body>
</html>
