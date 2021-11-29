<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Trylogyc_Int.Master" CodeBehind="Login_Int.aspx.vb" Inherits="Trylogyc.Login_Int" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">

        .loader2 {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('images/loader.gif') 50% 50% no-repeat rgb(249,249,249);
            opacity: 0.9;
            display:none;
        }
 
    #numericInput{
    position:relative;
}
    #numBox{
    width:100%;
    height:100%;
    text-align:right;
    border:none;
    cursor:text;
    overflow:visible !important

}
#keypad{
    width:100%;
    height:400px;
    border:solid 1px black;
    display:none;
    position:absolute;
    top:40px;
    left:1px;
    opacity:0.95;
     overflow:visible !important
}
.key{
    border:solid 2px black;
    background-color:#582C69;
    text-align:center;
    font-weight:bold;
    color:white;
    cursor:pointer;
    width:33.3%;
    height:20% !important;
    font-size:large
}
.key:hover{
    background-color:LightBlue;
}
.key:active{
    background-color:white;
}
.filaNum {
   overflow:visible !important
}
tr td .btn{
    border:solid 1px black;
    text-align:center;
    font-weight:bold;
    color:black;
    cursor:pointer;
    width:90%;
    

}
.btn:hover{
    background-color:DarkGray;
}
.btn:active{
    background-color:LightGray;
}
#btnlogin {
         -webkit-transition: all 0.10s ease-in-out;
    -moz-transition: all 0.10s ease-in-out;
    -ms-transition: all 0.10s ease-in-out;
    -o-transition: all 0.10s ease-in-out;
    }

#btnlogin:hover{
    background-color:#69477E;
}
.form-box {
    min-width:45%;
 
}
</style>

<div class="bodydefault2" style="height:80%; opacity:1; background-image: url(../images/back/abstract.jpg)">
<div class="loader2" id="loader2"></div>
<div style="width:100%; height:90%;margin-top:-5%; opacity:1; min-height:450px; overflow-y:visible !important">
 
        <div class="form-box" id="login-box">
                       <div style="text-align:center; padding-bottom:10px" >
        <asp:Image ID="Image3" ImageUrl="~/images/logo.png" runat="server" Height="48px" Width="178px" />
    </div> 
            <div class="header" style="background-color:#582C69"><i class="fa fa-user-circle fa-2x" style=" vertical-align:middle; color:white">&nbsp</i>Acceso de Usuarios</div>
            <div id="login_form" class="login_form">
                 
                <div class="body bg-gray" id="mybody">
                    <div class="form-group">
                       <%-- <asp:DropDownList Width="100%" Height="35px" ID="DropDownList1" runat="server" style="font-family: verdana; font-size: large; font-weight: bold" CssClass="ddl1">
                            <asp:ListItem Value="0">DNI</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">Código de Socio</asp:ListItem>
                        </asp:DropDownList>--%>
                        <asp:ListBox ID="DropDownList1" runat="server" Width="100%" Height="60" style="font-family: verdana; font-size: x-large; font-weight: bold" CssClass="ddl1">
                            <asp:ListItem Value="0">DNI</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">Código de Socio</asp:ListItem>
                        </asp:ListBox>
                    </div>
                    
                    <div class="form-group">
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <div id="numericInput" class="form-control" style="overflow:visible !important">
                        <span class="hidnumBox" ID="numBox2" style="font-family: verdana; font-size: large; font-weight: bold"></span>
                     
     <table id="keypad" style=" overflow:visible !important">
        <tr class="filaNum">
            <td class="key">1</td>
            <td class="key">2</td>
            <td class="key">3</td>
        </tr>
        <tr class="filaNum">
            <td class="key">4</td>
            <td class="key">5</td>
            <td class="key">6</td>
        </tr>
        <tr class="filaNum">
            <td class="key">7</td>
            <td class="key">8</td>
            <td class="key">9</td>
        </tr>
        <tr class="filaNum">
            <td class="key"><button style="height:100%;width:100%" type="button" class="btn">BORRAR</button></td>
            <td class="key">0</td>
            <td class="key"><button style="height:100%;width:100%" type="button" class="btn">LIMPIAR</button></td>
        </tr>
        <tr class="filaNum" style="background-color:lightgray  ;overflow:visible !important">
            <td class="btnlogin key" colspan="3" style="border:1px solid gray;background-color:lightgray;padding:5px; overflow:visible !important">
                <asp:Button  class="btnlogin btn bg-navy btn-flat" ID="btnlogin" runat="server" Text="Acceder" Height="40px"  />
            
            </td>
        </tr>
    </table>
                        </div>
                    </div>     
                     <span>
                        <asp:Label ID="lblError" runat="server" Text="Datos de Ingreso erróneos" CssClass="text-red" Visible="False"></asp:Label>
                    </span>
                   
                </div>
                   
            </div>
             <script type="text/javascript">
         $(document).ready(function () {
        
             $(".btnlogin").click(function () {
               
                 $(".loader2").fadeIn("slow");
                 //var list = $("#numBox2").text();
                 //$('[id$=HiddenField1]').val(list);

             });

             $("#numericInput").click(function () {
                 $('#keypad').show('fast');
                 event.stopPropagation();

             });

             $(".key").click(function () {
                 var mystring = $("#numBox2").text() + this.innerHTML;
                 $("#numBox2").text(mystring);
                 $("#hidden1").val(mystring);
                 $('[id$=HiddenField1]').val(mystring);
                 event.stopPropagation();
             });

             $('.btn').click(function () {
                 if (this.innerHTML == 'BORRAR') {
                     
                     if ($("#numBox2").text().length > 0) {
                         var newstring = $("#numBox2").text().substring(0, $("#numBox2").text().length - 1);
                         $("#numBox2").text(newstring);
                         $("#hidden1").val(newstring);
                     }
                 }
                 else {
                     $("#numBox2").text("");
                     $("#hidden1").val("");
                 }

                 event.stopPropagation();
             });
             $("#numericInput").click();
           
         });

     </script>
        </div>
</div>
 </div>

</asp:Content>

