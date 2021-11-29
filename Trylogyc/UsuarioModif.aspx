<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Trylogyc.Master" CodeBehind="UsuarioModif.aspx.vb" Inherits="Trylogyc.UsuarioModif" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="alert alert-danger alert-dismissible text-center" id="divError" runat="server">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
            <h4><i class="icon fa fa-ban"></i>Atención!</h4>
            <asp:Label ID="lblError" runat="server" Text="Label" Font-Bold="True" Font-Names="Calibri"></asp:Label>
        </div>
        <div class="alert alert-success alert-dismissible text-center" id="divSuccess" runat="server">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
            <h4><i class="icon fa fa-check"></i>OK!</h4>
            <asp:Label ID="lblSuccess" runat="server" Text="Label" Font-Bold="True" Font-Names="Calibri"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-info">
                <div class="box-header with-border">
                    <h3 class="box-title"><span><i class="fa fa-user"></i></span>Mis Datos de Usuario</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6 col-xs-12">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon">Contraseña Actual</span><span>
                                        <asp:TextBox ID="txtpassWordOld" runat="server" Enabled="true" CssClass="form-control" TextMode="Password"></asp:TextBox></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon">Nueva Contraseña</span><span>
                                        <asp:TextBox ID="txtPassWordNew" runat="server" Enabled="true" CssClass="form-control" TextMode="Password"></asp:TextBox></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon">Confirmar Contraseña</span><span>
                                        <asp:TextBox ID="txtPassWordNewCnf" runat="server" Enabled="true" CssClass="form-control" TextMode="Password"></asp:TextBox></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="input-group">
                                    <asp:CheckBox ID="aceptaEmail" runat="server" Text="&nbsp;&nbsp;&nbsp;Acepto recibir mis facturas por mail" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="box-footer with-border">
                    <asp:Button ID="btnSavePass" runat="server" Text="Guardar" class="btn btn-primary" />

                    <asp:Button ID="btnVolver" runat="server" Text="Volver" class="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
