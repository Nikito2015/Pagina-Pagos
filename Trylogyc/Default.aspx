<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Trylogyc.Master" CodeBehind="Default.aspx.vb" Inherits="Trylogyc._Default" %>
<%@ MasterType 
    virtualpath="~/Trylogyc.master" 
%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .badge-bg-red {
            vertical-align: middle !important;
            padding-top: 10px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <%-- asp:ImageButton ID="btnPagar" CommandName="btnPagarMP" runat="server"  ImageUrl="~/Images/mercadoPago.jpg" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                            Visible='<%# If(Eval("Pagada").ToString() = False, True, False) %>' /--%><%--asp:ImageButton ID="btnPagarMacro" CommandName="btnPagarMacro" runat="server" ImageUrl="~/Images/macroclick.png" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                            Visible='<%# If(Eval("Pagada").ToString() = False, True, False) %>' /--%>
    <div class="row">
        <div class="col-md-4 col-xs-12">
            <div class="box box-success">
                <div class="box-header with-border">
                    <h3 class="box-title"><span><i class="fa fa-plug"></i></span>Mis Conexiones</h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <asp:ListBox ID="LstConexiones" runat="server" CssClass="form-control" AutoPostBack="True" Height="100px"></asp:ListBox>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon">Nombre</span>
                            <asp:TextBox ID="txtNombre" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon">Dirección</span>
                            <asp:TextBox ID="txtDireccion" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon">Localidad</span>
                            <asp:TextBox ID="txtLocalidad" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="text-center">
                            <a href="relaciones.aspx" id="Button1" class="btn btn-default" />  Agregar Relación </a> 
                               
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-8 col-xs-12">
            <div class="box box-success">
                <div class="box-header with-border">
                    <h3 class="box-title"><span><i class="fa fa-clipboard"></i></span>Mis Facturas</h3>
                    <h3 class="box-title box-title-info"><span><i class="fa fa-info"></i></span>ESTA APLICACIÓN SÓLO PERMITE REIMPRIMIR LAS ULTIMAS FACTURAS ADEUDADAS, NO CONSTITUYE UN INFORME DE DEUDA; SI DESEA CONOCER EL ESTADO COMPLETO DE SU DEUDA POR SERVICIOS PRESTADOS, DEBERA REQUERIR INFORME EN LAS OFICINAS DE LA COOPERATIVA</h3>
                </div>
                <div class="box-body">
                    <div class="table-responsive">
                        <div class="alert alert-danger alert-dismissible" id="divError" runat="server">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                            <h4><i class="icon fa fa-ban"></i>Atención!</h4>
                            <asp:Label ID="lblError" runat="server" Text="Label" Font-Bold="True" Font-Names="Calibri"></asp:Label>
                        </div>

                        <asp:GridView ID="GridView1" runat="server" Caption="Facturas" EmptyDataText="No hay Facturas para Mostrar" Font-Size="Smaller"  CssClass="table table-bordered table-hover no-margin grid-table" PageSize="50" HorizontalAlign="Left" CaptionAlign="Top" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="cmdPDF2" CommandName="cmdPDF2" runat="server" Text="" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                            Width="20px" OnClick="cmdPDF2_Click" ImageUrl="~/Images/pdf.jpg" />
                                    </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pagar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Visible='<%# If(Eval("Pagada").ToString() = False, True, False) %>' />
                                        <%-- asp:ImageButton ID="btnPagar" CommandName="btnPagarMP" runat="server"  ImageUrl="~/Images/mercadoPago.jpg" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                            Visible='<%# If(Eval("Pagada").ToString() = False, True, False) %>' /--%>                                        
                                        <%--asp:ImageButton ID="btnPagarMacro" CommandName="btnPagarMacro" runat="server" ImageUrl="~/Images/macroclick.png" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                            Visible='<%# If(Eval("Pagada").ToString() = False, True, False) %>' /--%>                                        
                                    </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Factura" DataField="Nro_Factura" />
                                <asp:BoundField HeaderText="Pto Venta" DataField="Pto_Venta" />
                                <asp:BoundField HeaderText="Letra" DataField="Letra" />
                                <asp:BoundField HeaderText="Período" DataField="Periodo" />
                                <asp:BoundField HeaderText="Fecha Emisión" DataField="Fecha_Emision" />
                                <asp:BoundField HeaderText="Fecha Vto." DataField="Fecha_Vto" />
                                <asp:BoundField HeaderText="Grupo" DataField="Grupo_Fact" />
                                <asp:BoundField HeaderText="Importe" DataField="Importe" />
                                <asp:BoundField HeaderText="Conexion" DataField="Conexion" Visible="true" />
                                <asp:BoundField HeaderText="Pagada" DataField="Pagada" Visible="false" />
                                <asp:BoundField HeaderText="IdPreferenciaPago" DataField="IdPreferenciaPago" Visible="false" />
                                <asp:BoundField HeaderText="Socio" DataField="Socio" Visible="true" />

                                <asp:BoundField DataField="codigoBarra" HeaderText="codigoBarra" Visible="false" />

                            </Columns>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<RowStyle HorizontalAlign="Center"></RowStyle>
                        </asp:GridView>
                    </div>
                    <br />
                    <div class="box-footer with-border">
                        <div class="row">
                            <div class="col-md-3">
                                <div style="width: 20%; float:left">
                                   <asp:ImageButton ID = "BtnPagarMP" runat = "server" ImageUrl = "~/Images/mercadoPago.jpg"/>
                                    <asp:ImageButton ID="BtnPagarMacro" runat="server" ImageUrl="~/Images/macroclick.png" Width="57px" Height="27px"/>
                                    <asp:ImageButton ID="BtnPagarFacil" runat="server" ImageUrl="~/Images/pagofacil.png" Width="57px" Height="27px"/>
                                    
                                </div>
                            
                          
                            </div>
                            <div class="col-md-6" style="margin-left: 70px">
                                <div style="float: right">
                                    <span>
                                        <img alt="" src="images/logo.png" width="85" />
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
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:SqlDataSource ID="sqlFacturas" runat="server" ConnectionString="<%$ ConnectionStrings:TrylogycContext %>" SelectCommand="SEL_FACTURAS_SOCIO_CONEXION" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="XmlSocio" SessionField="xmlSocio" Type="Int32" />
            <asp:ControlParameter ControlID="lstConexiones" Name="XmlConexion" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:LinqDataSource ID="LinqDataSource1" runat="server">
    </asp:LinqDataSource>
    <asp:HiddenField ID="hidden_dtSocios" runat="server" />
</a>
</asp:Content>
