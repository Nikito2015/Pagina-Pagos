<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="Trylogyc.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html style="overflow: visible !important">
<head>
    
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
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

</head>
<body class="bodydefault2" style="overflow-x: hidden !important; background-image: url(../images/back/fondoLogin.png)">
    <div class="login-form" id="login-box">
        <div style="text-align: center; padding-bottom: 10px; height: 76px; width: 357px;">
            <asp:Image ID="Image3" ImageUrl="~/images/logo.png" runat="server" Height="77px" Width="178px" />
        </div>

        <div class="header"><i class="fa fa-user-circle fa-2x" style="vertical-align: middle; color: white">&nbsp</i>Acceso de Usuarios</div>
        <form runat="server">
            <div class="form-group">
                <asp:TextBox ID="txtemail" runat="server" class="form-control" placeholder="Email"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtpassword" runat="server" class="form-control" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
            </div>
            <%--                    <div class="form-group">
                        <input type="checkbox" name="remember_me"/>Recordarme
                    </div>--%>
            <div class="form-group">
                <asp:Label ID="lblError" runat="server" Text="Datos de Ingreso erróneos" CssClass="text-red" Visible="False"></asp:Label>
            </div>
            <div class="footer form-group">
                <asp:Button ID="btnlogin" class="btn bg-light-blue  btn-block btn1" runat="server" Text="Acceder" />
            </div>
            <div class="pull-left">
                <a href="register.aspx" class="text-center">Registrarme</a>
            </div>
            <div class="pull-right">
                <a href="reestablecer.aspx" class="text-center">Olvidé mi Contraseña</a>
            </div>
        </form>
    </div>
</body>
</html>
