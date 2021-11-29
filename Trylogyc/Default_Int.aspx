<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Trylogyc_Int.Master" CodeBehind="Default_Int.aspx.vb" Inherits="Trylogyc.Default_Int" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">

        .printIcon {
            border:1px solid gray;
            box-shadow:1px 1px 1px 1px #666;
        }
        .printIcon:active {
  background-color: #3e8e41;
  box-shadow:0px 3px 3px 0 #666;
  transform: translateY(3px)

}
        table th {
            text-align: center !important;
            vertical-align: middle !important;
        }

        #GridView1 tbody {
            font-size: large !important;
            border: 0px solid transparent;
            vertical-align: middle !important;
        }

        #GridView1 tr {
            border: 0px solid transparent !important;
            vertical-align: middle !important;
        }

        #GridView1 td {
            border: 0px solid transparent !important;
            vertical-align: middle !important;
        }

        table {
            vertical-align: middle !important;
        }

        td {
            vertical-align: middle !important;
        }

        tr {
            vertical-align: middle !important;
        }

            tr td {
                vertical-align: middle !important;
            }

        .table-hover tr td {
            font-weight:bold !important;
            font-size:medium !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row" style="height:50px">
        
        <div class="col-md-3" style="background-color:darkslategray; height:50px;text-align:center;vertical-align:middle">
            <div style="padding-top:10px">
            <a href="Default_int.aspx" class="" 
        style="font-family: Constantia; font-style: oblique; font-size:large; font-weight: bold;vertical-align:middle;color:white">
                <!-- Add the class icon to your logo image or logo icon to add the margining -->
                <span><i class="fa fa-user">&nbsp</i><asp:Label ID="lblCliente" runat="server" Text="Label" Font-Size="medium" Font-Names="Calibri" Font-Italic="false"></asp:Label></span>
            </a>
                </div> 
        </div>
        <div class="col-md-6"  style="background-color:whitesmoke; height:50px">

        </div>
        <div class="col-md-3" style="float:right;background-color:whitesmoke; height:50px">
                <ul class="nav navbar-nav" style="float:right;background-color:whitesmoke">
             <li class="dropdown user user-menu"><a href="login_Int.aspx" class="btn" style="float:right; padding-right:5px; color:darkslategray;font-weight:bold">Cerrar Sesion</a></li>
              </ul>
        </div>
             </div>
    
     <div class="wrapper collapse-left">
     <aside class="right-side strech" style="overflow-x:visible; overflow-y:visible; overflow:visible;-ms-overflow-style:none">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                 <h1 id="header1" style="font-family: verdana !important; font-style: normal !important; color: #666666;">
                         <i id="hideshow" class="fa fa-chevron-circle-up" style="cursor:pointer; opacity:0.9">
                         <span style="cursor:pointer; font-style:normal !important;font-family: verdana !important; font-weight:normal !important; font-size:large"><strong>Centro de Información</strong></></span>
                         </i>
                         <button id="refresh" runat="server" class="btn btn-default" value="Actualizar"><span class="fa fa-refresh"></span></button>
   
                 </h1>
                       
                 </section>
                   <script type="text/javascript">
                       $("#hideshow").click(function () {
                           //                        assumes element with id='button'
                           $("#content").toggle(750);
                           $("#header1").find('i').toggleClass('fa-chevron-circle-down');
                       });

                   </script>
     <div id="content">
           <div runat="server" id="centroinfo" class="row" style="vertical-align: top; text-align: left; height:auto;display:block; ">
                        
               <div class="col-md-3" style="padding:20px">
                            <!-- small box -->
                                
                            <div id="divco1" runat="server" class="small-box bg-red">
                                <div class="inner">
                                    <h3>                                      
                     <span ID="lblco1" ></span>
                                    </h3>
                                    <p class="style1">
                                        <span ID="lblco1txt">Conexiones</span>
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-clipboard"></i>
                                </div>
                                <a class="small-box-footer">
                                 <asp:Button ID="cmdco1" runat="server" Text="" BackColor="Transparent" BorderStyle="None" 
                                                               ForeColor="White" CausesValidation=false /><i class="fa fa-arrow-circle-o-right"></i>
                                </a>
                            </div>
                        </div><!-- ./col -->
                <div class="col-md-3">

               </div>   
                <div class="col-md-3">

               </div>    
                      <div class="col-md-3" style="float:right">
                          <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logo.png" />
                      </div>
               
                        
        </div>  
           </div>
         <asp:ScriptManager runat="server">
    </asp:ScriptManager>
              <script type="text/javascript">
                 
                  $(document).ready(function () {
                      $("#pdf-iframe").load(function () {
                          window.frames['pdf-iframe'].focus();
                          //var myPDF3 = document.getElementById('pdf-iframe');
                          //setTimeout(function () { myPDF3.contentWindow.print(); }, 1000);
                          myPDF3.window.print();
                      });
                      if (/chrom(e|ium)/.test(navigator.userAgent.toLowerCase())) {
                          var me = this;
                          $(".table-hover tr td").click(function () {
                              var pdfFile = $(this).data('id');
                              $('#pdf-iframe').attr("src", pdfFile);
                              $.ajax({
                                  type: 'HEAD',
                                  url: '/'+ pdfFile,
                                  success: function () {
                                      var myPDF3 = document.getElementById('pdf-iframe');
                                      setTimeout(function () { myPDF3.contentWindow.print(); }, 1000);
                                     
                                  },
                                  error: function () {
                                      alert('No se ha encontrado el archivo.');
                                  }
                              });
                             

                          });


                       }
                      else {
                          $(".table-hover tr td").click(function () {
                              $('#pdf-iframe').attr("src", $(this).data('id'));
                              var windows = [];
                              var myDoc = $(this).data('id');
                              var myWindow = windows.push(window.open(myDoc, '_blank'));


                          });
                      };

                  });
                 
                  var prm = Sys.WebForms.PageRequestManager.getInstance();
                  prm.add_endRequest(function () {
                     
                      $(document).ready(function () {
       
                         
                          if (/chrom(e|ium)/.test(navigator.userAgent.toLowerCase())) {
                              $(".table-hover tr td").click(function () {
                                  var pdfFile = $(this).data('id');
                                  $('#pdf-iframe').attr("src", pdfFile);
                                  $.ajax({
                                      type: 'HEAD',
                                      url: '/' + pdfFile,
                                      success: function () {
                                          var myPDF3 = document.getElementById('pdf-iframe');
                                          setTimeout(function () { myPDF3.contentWindow.print(); }, 1000);

                                      },
                                      error: function () {
                                          alert('No se ha encontrado el archivo.');
                                      }
                                  });


                              });
                          }
                              else {
                                  $(".table-hover tr td").click(function () {
                                      $('#pdf-iframe').attr("src", $(this).data('id'));
                                      var windows = [];
                                      var myDoc = $(this).data('id');
                                      var myWindow = windows.push(window.open(myDoc, '_blank'));
                                     
                              
                                  });
                              };
                         
                        });

                  });


                 
                               
                  
                  
            </script>      
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" EnableViewState="true">
        <ContentTemplate>
           
    <div class="row" style="overflow-y:visible;width:100%; padding-left:10px !important" >
                <div class="col-sm-4" style="padding-left:20px !important">
                    <div class="box box-success with-border">
                        <div id="hideshow2" class="box-header with-border hideshow2" style="border-bottom: 1px solid silver">
                            <h3 class="box-title" style="font-family: verdana; font-size: large"><span><i class="fa fa-plug"></i></span>&nbsp&nbsp; Mis Conexiones</h3>
                        </div>

                        <div id="content2" class="box-body with-border">
                            <div class="row">
                                <%-- <div class="border col-xs-6">--%>
                                <div class="col-md-9">
                                <asp:ListBox ID="lstConexiones" runat="server" AutoPostBack="True" CssClass="listbox" Style="margin-left: 4%; width:100%; overflow:scroll !important ; -ms-overflow-style:scrollbar; height:210px" Font-Bold="True" Font-Size="x-Large"></asp:ListBox>
                                    </div>
                                <div class="col-md-3">
                                    <div style="padding-bottom:10px">
                                        <asp:ImageButton ID="ImageButton1" runat="server" Width="30px" ImageUrl="~/images/arrowUp.png" />
                                    </div>
                                      <div style="padding-top:10px">
                                     <asp:ImageButton ID="ImageButton2" runat="server" Width="30px" ImageUrl="~/images/arrowDown.png" />
                                    </div>
                                </div>
                            </div>

                            <%-- </div>
                            <div class="border col-xs-6">--%>
                            <br />
                            <div class="row">
                                <%--    <div class="col-md-2">

                            </div>--%>
                                <div class="col-md-12">
                                    <div class="input-group" style="margin-left: 0.5%">
                                        <span class="input-group-addon" style="width: 100px">Nombre</span><span>
                                            <asp:TextBox ID="txtNombre" runat="server" Enabled="false" CssClass="form-control" Style="width: 270px"></asp:TextBox></span>
                                    </div>
                                    <br />
                                    <div class="input-group" style="margin-left: 0.5%">
                                        <span class="input-group-addon" style="width: 100px">Dirección</span>
                                        <asp:TextBox ID="txtDireccion" runat="server" Enabled="false" CssClass="form-control" Style="width: 270px"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group" style="margin-left: 0.5%">
                                        <span class="input-group-addon" style="width: 100px">Localidad</span>
                                        <asp:TextBox ID="txtLocalidad" runat="server" Enabled="false" CssClass="form-control" Style="width: 270px"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="input-group" style="margin-left: 0.5%">
                                        <span class="input-group-addon" style="width: 100px">Deuda ($)</span>
                                        <asp:TextBox ID="txtDeudatotal" runat="server" Enabled="false" CssClass="form-control" Style="width: 270px; font-weight: bold; color: red"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="col-sm-8">
                    <div class="box box-success with-border">
                        <div class="box-header with-border" style="border-bottom: 1px solid silver">
                            <h3 class="box-title" style="font-family: verdana; font-size: large"><span><i class="fa fa-clipboard"></i></span>&nbsp&nbsp Mis Facturas</h3>
                        </div>
                        <div class="box-body with-border">
                            <div class="row" style="text-align: center">
                                <div class="box-body table-responsive no-padding" style="margin-left: 3%">
                                    <div class="alert alert-danger alert-dismissible" id="divError" runat="server" style="width: 95%; margin-left: 2% !important">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h4><i class="icon fa fa-ban"></i>Atención!&nbsp; <asp:Label ID="lblError" runat="server" Text="Label" Font-Bold="True" Font-Names="Calibri"></asp:Label></h4>
                                       
                                    </div>


                                    <div style="padding: 10px">
                                        <asp:GridView ID="GridView1" runat="server" Caption="Facturas" EmptyDataText="No hay Facturas para Mostrar" Font-Size="Small"
                                            Style="font-size: large !important; overflow-x: scroll; vertical-align: middle !important; min-width: 95%" HorizontalAlign="Center" CaptionAlign="Top" Width="99%" CssClass="table table-hover">
                                            <Columns>
                                                <asp:TemplateField>

                                                    <ItemTemplate>
                                                       <asp:ImageButton ID="cmdPDF2" CommandName="cmdPDF2" runat="server" Text="" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    Width="0px" OnClick="cmdPDF2_Click" ImageUrl="~/Images/imprime.png" />
                                                   <img alt="" class="printIcon" src="images/imprime.png" width="100px" style="cursor:pointer" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
           
                                        <style type="text/css">
                                            table th {
                                                text-align: center !important;
                                                vertical-align: middle !important;
                                            }

                                            #GridView1 tbody {
                                                font-size: large !important;
                                                vertical-align: middle !important;
                                            }

                                            #GridView1 tr {
                                                border: 0px solid transparent !important;
                                                vertical-align: middle !important;
                                            }

                                            #GridView1 td {
                                                border: 0px solid transparent !important;
                                                vertical-align: middle !important;
                                            }

                                            table {
                                                vertical-align: middle !important;
                                            }

                                            td {
                                                vertical-align: middle !important;
                                            }

                                            tr {
                                                vertical-align: middle !important;
                                                border: 1px solid gray;
                                            }

                                                tr td {
                                                    vertical-align: middle !important;
                                                    padding-top: 5px !important;
                                                }
                                        </style>
                                    </div>
                                    <br />
                                </div>
                            </div>
                            <br />
                            <div class="box-footer with-border">
                                <div class="row">
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-6" style="margin-left: 70px">
                                        <div style="float: right">
                                            <span>
                                                <img alt="" src="images/logo.jpg" width="100px" />
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                 
         </ContentTemplate>
    </asp:UpdatePanel>
        </aside>
</div>
    <iframe id="pdf-iframe" style="width:1px;height:1px">
    </iframe>
    <asp:SqlDataSource ID="sqlFacturas" runat="server" ConnectionString="<%$ ConnectionStrings:TrylogycContext %>" SelectCommand="SEL_FACTURAS_SOCIO_CONEXION" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="XmlSocio" SessionField="xmlSocio" Type="Int32" />
            <asp:ControlParameter ControlID="lstConexiones" Name="XmlConexion" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#hideshow").click();
            var rowCount = $('.table-hover tr').length;
            var lstCount = $('.listbox option').length - 1;
            $("#lblco1").html(lstCount);
            //$(".table-hover tr td").click(function () {
            //    $('#pdf-iframe').attr("src", $(this).data('id')).load(function () {
            //        document.getElementById('pdf-iframe').contentWindow.print();
            //    });
            //});
            //$('#pdf-iframe').attr( "src", "r").load(function () {
            //    document.getElementById('pdf-iframe').contentWindow.print();

            //});
            $(".table-hover tbody tr .printIcon").click(function () {
                $(".table-hover tbody tr").css('background-color', 'transparent');
                $(this).closest('tr').css('background-color','rgba(173, 173, 173, 0.5)');
            });
        });
       

    </script>

    <asp:LinqDataSource ID="LinqDataSource1" runat="server">
    </asp:LinqDataSource>
   

</asp:Content>
