Imports Trylogyc.DAL
Imports System.IO
Imports System.Net
Imports MercadoPago.Resources
Imports MercadoPago.DataStructures.Preference
Imports MercadoPago.Common
Imports System.Globalization
Imports System.Linq
Imports CommonTrylogycWebsite.ServiceRequests
Imports CommonTrylogycWebsite.ServiceResponses
Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Helpers

Public Class _Default
    Inherits System.Web.UI.Page
    Dim myContext = New TrylogycContext
    Dim sumtotal As Double
    Public preferenceIDMP As String

    Private _dtSocios As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If login.ValidateSessionCookie() = True Then

            RefreshMasterData()

            '1.Setear endpoint
            Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "GetUserAssociates")
            '2.Crear clase Request.
            Dim userAssociatesRequest As New UserAssociatesAndBalancesRequest()

            userAssociatesRequest.UserId = Convert.ToInt32(Request.Cookies("IDUsuario").Value)
            Dim serializedRequest As String = JsonConvert.SerializeObject(userAssociatesRequest)

            '3.Invocar Servicio
            Dim responseMessage As HttpResponseMessage = Task.Run(Function()
                                                                      Return APIHelpers.PostAsync(apiEndpoint, serializedRequest, 60, Nothing)
                                                                  End Function).Result
            '4. Deserializar respuesta
            Dim apiResponse As UserAssociatesResponse = JsonConvert.DeserializeObject(Of UserAssociatesResponse)(responseMessage.Content.ReadAsStringAsync().Result)
            hidden_dtSocios.Value = responseMessage.Content.ReadAsStringAsync().Result
            Me.divError.Visible = False
            If Not Page.IsPostBack Then

                ''traer listado de socio-conexion de la tabla
                'traer listado de conexiones para el socio

                LstConexiones.Items.Insert(0, " Todas")
                LstConexiones.Items.Item(0).Value = 0
                'Recuperar tabla de socios para el idUsuario en el servicio wcf
                Dim dtSocio As DataTable = login.CreateDataTableAssociates(apiResponse?.Associates?.ToArray())
                DtSocios = dtSocio
                CargarComboConexiones(dtSocio)
                readFacturas(Request.Cookies("xmlSocio").Value, LstConexiones.SelectedValue)
                'Me.txtDeudatotal.Text = Format((sumtotal), "0.00")


                GridView1.DataBind()
                'Ocultar/Mostrar columna "Pagar"
                Dim displayPagar As Boolean = Convert.ToBoolean(ConfigurationManager.AppSettings("displayPagar"))
                CType(GridView1.Columns.Cast(Of DataControlField)().Where(Function(fld) fld.HeaderText = "Pagar") _
                    .SingleOrDefault(), DataControlField).Visible = displayPagar
                Me.BtnPagarMacro.Visible = Convert.ToBoolean(ConfigurationManager.AppSettings("botonMacroClick"))
                Me.BtnPagarMP.Visible = Convert.ToBoolean(ConfigurationManager.AppSettings("botonMercadoPago"))
            Else
                SelectedPreferenceId = String.Empty
                If login.ValidateSessionCookie() = True Then
                Else
                    Response.Redirect("~/login.aspx")
                End If

            End If
        Else
            Response.Redirect("~/login.aspx")
        End If

    End Sub


#Region "Eventos"
    Protected Sub LstConexiones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstConexiones.SelectedIndexChanged
        Me.divError.Visible = False
        Dim intSocio As Int32
        Dim intConexion As Int32
        If LstConexiones.SelectedValue <> "0" Then
            intSocio = Convert.ToInt32(LstConexiones.SelectedItem.Text.Substring(0, 6))
            intConexion = Convert.ToInt32(LstConexiones.SelectedItem.Text.Substring(7, 4))
        Else
            intSocio = 0
            intConexion = 0
        End If

        readFacturas(intSocio, intConexion)
        Me.GridView1.Caption = "Facturas Conexión " & LstConexiones.SelectedItem.ToString
        Dim bufferedApiResponse As UserAssociatesResponse = JsonConvert.DeserializeObject(Of UserAssociatesResponse)(hidden_dtSocios.Value)
        Dim dv As DataView = New DataView(login.CreateDataTableAssociates(bufferedApiResponse?.Associates?.ToArray()))
        Me.txtNombre.Text = ""
        Me.txtDireccion.Text = ""
        Me.txtLocalidad.Text = ""
        For Each row As DataRowView In dv
            If row("SOCIO") = intSocio And row("CONEXION") = intConexion Then
                Me.txtNombre.Text = row("NOMBRE")
                Me.txtDireccion.Text = row("DIRECION")
                Me.txtLocalidad.Text = row("LOCALIDAD")
                'Me.txtDeudatotal.Text = Format((sumtotal), "0.00")
                Exit For
            End If
        Next
        LstConexiones.Focus()
    End Sub
    Protected Sub cmdPDF_Click(sender As Object, e As ImageClickEventArgs)

    End Sub
    Protected Sub cmdPDF2_Click(sender As Object, e As ImageClickEventArgs)

    End Sub

    Protected Sub gridview1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType <> DataControlRowType.Footer And GridView1.Rows.Count > 0 Then

            e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(13).Visible = False

        End If

        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = "Imprime"
            e.Row.Cells(1).Text = "Pagar"
            e.Row.Cells(2).Text = "Factura"
            e.Row.Cells(3).Text = "Pto Venta"
            e.Row.Cells(4).Text = "Socio/Con"
            e.Row.Cells(5).Text = "Período"
            e.Row.Cells(6).Text = "Fecha Emisión"
            e.Row.Cells(7).Text = "Fecha Vto"
            e.Row.Cells(8).Text = "Grupo"
            e.Row.Cells(9).Text = "Importe"
            e.Row.Cells(10).ForeColor = Drawing.Color.Transparent
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(12).Text = "Info Pago"
            e.Row.Cells(13).Visible = False

        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(9).CssClass = "badge bg-red money"
            e.Row.Cells(10).ForeColor = Drawing.Color.Transparent
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(13).Visible = False
            'Crear 1 preferencia de pago por fila:

        End If

        ''Ocultar/Mostrar botón Macro Click
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim botonMacroClick As Boolean = Convert.ToBoolean(ConfigurationManager.AppSettings("botonMacroClick"))
        '    Dim botonMercadoPago As Boolean = Convert.ToBoolean(ConfigurationManager.AppSettings("botonMercadoPago"))

        '    If botonMacroClick = False Then
        '        Dim botonPagoMacro As Button = CType(e.Row.FindControl("btnPagarMacro"), Button)
        '        botonPagoMacro.Style.Add("display", "none")
        '    End If
        '    If botonMercadoPago = False Then
        '        Dim botonMPago As Button = CType(e.Row.FindControl("btnPagarMP"), Button)
        '        botonMPago.Style.Add("display", "none")
        '    End If
        'End If

    End Sub
    Protected Sub gridview1_rowcommand(sender As Object, e As CommandEventArgs) Handles GridView1.RowCommand
        If (e.CommandName = "cmdPDF2") Then
            Dim index1 As Integer = Convert.ToInt32(e.CommandArgument) 'captura el valor del índice de la fila
            Dim row1 As GridViewRow = GridView1.Rows(index1) 'crea un objeto row que contiene la fila del botón presionado.
            Dim socio As Int32 = Convert.ToInt32(CType(row1.Cells(13), DataControlFieldCell).Text)
            Dim conexion As Int32 = Convert.ToInt32(CType(row1.Cells(10), DataControlFieldCell).Text)
            Dim numFact = CType(row1.Cells(2), DataControlFieldCell).Text

            'Recuperar pdf en formato bytes de base64 del wcf
            '1.Setear endpoint
            Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "RecuperarPdfFactura")
            '2.Crear clase Request.
            Dim pdfRequest As New InvoicePdfRequest()

            pdfRequest.AssociateId = socio
            pdfRequest.ConnectionId = conexion
            pdfRequest.InvoiceNumber = numFact
            pdfRequest.RetrieveFromFTP = Convert.ToBoolean(ConfigurationManager.AppSettings("retrievePdfFromFTP").ToString())
            Dim serializedRequest As String = JsonConvert.SerializeObject(pdfRequest)


            '3.Invocar Servicio
            Try
                '3.Invocar Servicio
                Dim responseMessage As HttpResponseMessage = Task.Run(Function()
                                                                          Return APIHelpers.PostAsync(apiEndpoint, serializedRequest, 60, Nothing)
                                                                      End Function).Result
                '4. Deserializar respuesta
                Dim pdfResponse As InvoicePdfResponse = JsonConvert.DeserializeObject(Of InvoicePdfResponse)(responseMessage.Content.ReadAsStringAsync().Result)


                If pdfResponse.StatusCode = HttpStatusCode.OK Then

                    'Convertir array base64 en archivo .pdf en ruta temporal. Luego el mismo es recuperado por nombre desde la vista showPdf.aspx

                    Dim targetPath As String = Server.MapPath("~/_tmp/" & LTrim(RTrim(numFact)) & ".pdf")
                    Dim bytes As Byte() = Convert.FromBase64String(pdfResponse.InvoicePdf)
                    If File.Exists(targetPath) Then File.Delete(targetPath)
                    Dim stream As System.IO.FileStream = New FileStream(targetPath, FileMode.CreateNew)
                    Dim writer As System.IO.BinaryWriter = New BinaryWriter(stream)
                    writer.Write(bytes, 0, bytes.Length)
                    writer.Close()
                    writer.Dispose()
                    stream.Close()
                    stream.Dispose()
                    login.CreateCookie("fileName", LTrim(RTrim(numFact)) & ".pdf", False)
                    Response.Redirect("~/showPdf.aspx")

                Else
                    Me.lblError.Text = pdfResponse.Message
                    Me.divError.Visible = True
                End If
            Catch ex As Exception
                Me.lblError.Text = "No se pudo recuperar el documento PDF."
                Me.divError.Visible = True
            End Try
        Else
            Dim index1 As Integer = Convert.ToInt32(e.CommandArgument) 'captura el valor del índice de la fila
            Dim row1 As GridViewRow = GridView1.Rows(index1) 'crea un objeto row que contiene la fila del botón presionado.
            Dim socio As Int32 = Convert.ToInt32(CType(row1.Cells(13), DataControlFieldCell).Text)
            Dim numfact As String = CType(row1.Cells(2), DataControlFieldCell).Text
            Dim importe As String = CType(row1.Cells(9), DataControlFieldCell).Text
            importeFactura = ConvertirCampoImporteADecimal(importe)
            Dim Master As Trylogyc = CType(Me.Master, Trylogyc)
            Dim idSocio As String = socio
            Dim idConexion As String = CType(row1.Cells(10), DataControlFieldCell).Text
            Dim pagoEnProceso As Boolean = VerificarPagoEnProceso(idSocio, idConexion, numfact, importeFactura)

            If (e.CommandName.Equals("btnPagarMP")) Then
                If pagoEnProceso = True Then
                    Response.Redirect("~/ConfirmarPago.aspx?numfact=" & LTrim(RTrim(numfact)) & "&importe=" & importe & "&idSocio=" & idSocio & "&idConexion=" & idConexion)
                Else
                    Response.Redirect("~/Pagar.aspx?numfact=" & LTrim(RTrim(numfact)) & "&importe=" & importe & "&idSocio=" & idSocio & "&idConexion=" & idConexion)
                End If
            ElseIf (e.CommandName.Equals("btnPagarMacro")) Then
                If pagoEnProceso = True Then
                    Response.Redirect("~/ConfirmarPago.aspx?numfact=" & LTrim(RTrim(numfact)) & "&importe=" & importe & "&idSocio=" & idSocio & "&idConexion=" & idConexion)
                Else
                    Response.Redirect("~/PagarMacro.aspx?numfact=" & LTrim(RTrim(numfact)) & "&importe=" & importe & "&idSocio=" & idSocio & "&idConexion=" & idConexion)
                End If
            End If
        End If


    End Sub
#End Region

#Region "Métodos Privados"

#Region "Métodos de Pago Mercado Pago (deben migrarse al servicio wcf)"

    Private Sub ReEvaluarYSetearMercadoPagoToken()
        If (MercadoPago.SDK.AccessToken Is Nothing) Then
            MercadoPago.SDK.SetAccessToken("TEST-803559796931547-060612-07b906157e8808e7d657beabf35fdc1d-229476782")
        End If
    End Sub
    Private Function GenerarPreferenciaPagoMercadoPago(nroFactura As String, importeFactura As Decimal) As String
        'Dim importeAPagar As Decimal
        Dim myPreference As New Preference()
        myPreference.ClientId = "580430370"
        Dim itemToAdd As New Item()
        itemToAdd.Title = nroFactura
        itemToAdd.Quantity = 1
        itemToAdd.CurrencyId = CurrencyId.ARS
        itemToAdd.UnitPrice = importeFactura
        myPreference.Items.Add(itemToAdd)
        Dim _backUrls As New BackUrls()
        _backUrls.Success = Request.Url.GetLeftPart(UriPartial.Path) & "/PagoExitoso.aspx"
        _backUrls.Failure = Request.Url.GetLeftPart(UriPartial.Path) & "/PagoRechazado.aspx"
        _backUrls.Pending = Request.Url.GetLeftPart(UriPartial.Path) & "/PagoPendiente.aspx"
        myPreference.BackUrls = _backUrls
        myPreference.AutoReturn = AutoReturnType.approved
        myPreference.Save()
        GenerarPreferenciaPagoMercadoPago = myPreference.Id
        Return myPreference.Id
    End Function
    Private Function ConvertirCampoImporteADecimal(ByVal cell As Object) As Decimal
        Dim importeFactura As Decimal

        If cell Is Nothing Or cell Is DBNull.Value Then
            login.CreateCookie("txtError", "Se produjo un error. Los montos de las facturas no tienen el formato correcto.", False)
            Response.Redirect("~/Error.aspx")
        Else
            If (Decimal.TryParse(cell,
            NumberStyles.AllowDecimalPoint,
            CultureInfo.CreateSpecificCulture("fr-FR"),
            importeFactura) = False) Then
                login.CreateCookie("txtError", "Se produjo un error. Los montos de las facturas no tienen el formato correcto.", False)
                Response.Redirect("~/Error.aspx")
            End If
        End If

        ConvertirCampoImporteADecimal = importeFactura.ToString("0.00")
        Return importeFactura
    End Function

    ''' <summary>
    ''' Verificars the pago en proceso.
    ''' </summary>
    ''' <param name="idSocio">The identifier socio.</param>
    ''' <param name="idConexion">The identifier conexion.</param>
    ''' <param name="numFact">The number fact.</param>
    ''' <param name="importe">The importe.</param>
    ''' <returns></returns>
    Private Function VerificarPagoEnProceso(ByVal idSocio As Int32, ByVal idConexion As Int32, ByVal numFact As String, ByVal importe As Decimal) As Boolean
        Dim myContext As New TrylogycContext
        'Dim existePago As Boolean = myContext.GetPago(idSocio, idConexion, numFact, importe)

        'Recuperar lista de saldos de la api
        '1.Setear endpoint
        Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "GetPago")
        '2.Crear clase Request.
        Dim getPagoRequest As New PagoRequest()
        getPagoRequest.idSocio = idSocio
        getPagoRequest.idConexion = idConexion
        getPagoRequest.numFact = numFact
        getPagoRequest.importe = importe

        Dim serializedRequest As String = JsonConvert.SerializeObject(getPagoRequest)

        '3.Invocar Servicio
        Dim responseMessage As HttpResponseMessage = Task.Run(Function()
                                                                  Return APIHelpers.PostAsync(apiEndpoint, serializedRequest, 60, Nothing)
                                                              End Function).Result
        '4. Deserializar respuesta
        Dim getPagoResponse As PagoResponse = JsonConvert.DeserializeObject(Of PagoResponse)(responseMessage.Content.ReadAsStringAsync().Result)

        If responseMessage.StatusCode = HttpStatusCode.OK Then
            If getPagoResponse.tienePagos Then
                existePago = True
            Else
                existePago = False
            End If
        Else
            existePago = False
        End If

        Return existePago
    End Function

#End Region

    Private Sub readFacturas(ByVal xmlsocio As String, ByVal conexion As Int32)

        Try
            'Recuperar lista de saldos de la api
            '1.Setear endpoint
            Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "GetUserBalances")
            '2.Crear clase Request.
            Dim userBalancesRequest As New UserAssociatesAndBalancesRequest()
            userBalancesRequest.UserId = Convert.ToInt32(Request.Cookies("IDUsuario").Value)

            Dim serializedRequest As String = JsonConvert.SerializeObject(userBalancesRequest)

            '3.Invocar Servicio
            Dim responseMessage As HttpResponseMessage = Task.Run(Function()
                                                                      Return APIHelpers.PostAsync(apiEndpoint, serializedRequest, 60, Nothing)
                                                                  End Function).Result
            '4. Deserializar respuesta
            Dim userBalancesResponse As UserBalancesResponse = JsonConvert.DeserializeObject(Of UserBalancesResponse)(responseMessage.Content.ReadAsStringAsync().Result)

            If userBalancesResponse.StatusCode = HttpStatusCode.OK Then
                Dim dvFacturas As New DataView(login.CreateDataTableBalances(userBalancesResponse.Balances.ToArray()))
                Dim dtFacturas As New DataTable
                Dim dvdtFacturas As New DataTable
                'Dim importeFactura As Decimal

                dtFacturas.Columns.Add("Nro_Factura")
                dtFacturas.Columns.Add("Pto_Venta")
                dtFacturas.Columns.Add("Letra")
                dtFacturas.Columns.Add("Periodo")
                dtFacturas.Columns.Add("Fecha_Emision")
                dtFacturas.Columns.Add("Fecha_Vto")
                dtFacturas.Columns.Add("Grupo_Fact")
                dtFacturas.Columns.Add("Importe")
                dtFacturas.Columns.Add("Factura")
                dtFacturas.Columns.Add("Conexion")
                dtFacturas.Columns.Add("Pagada")
                dtFacturas.Columns.Add("IdPreferenciaPago")
                dtFacturas.Columns.Add("Socio")

                If conexion = 0 Then 'TODAS
                    'dvFacturas.RowFilter = "Socio =" & xmlsocio
                    dvFacturas.Sort = "PERIODO DESC"
                    dvdtFacturas = dvFacturas.ToTable
                    For u = 0 To dvdtFacturas.Rows.Count - 1
                        dtFacturas.Rows.Add()
                        dtFacturas.Rows(u).Item("Nro_Factura") = Mid(dvdtFacturas.Rows(u).Item("Factura"), 11, (Len(dvdtFacturas.Rows(u).Item("Factura")) - 10))
                        dtFacturas.Rows(u).Item("Pto_Venta") = dvdtFacturas.Rows(u).Item("Pto_Venta")
                        dtFacturas.Rows(u).Item("Letra") = dvdtFacturas.Rows(u).Item("Socio") & "/" & dvdtFacturas.Rows(u).Item("Conexion")  'dvdtFacturas.Rows(u).Item("Letra")
                        dtFacturas.Rows(u).Item("Periodo") = dvdtFacturas.Rows(u).Item("Periodo")
                        dtFacturas.Rows(u).Item("Fecha_Emision") = dvdtFacturas.Rows(u).Item("Fecha_Emision")
                        dtFacturas.Rows(u).Item("Fecha_Vto") = dvdtFacturas.Rows(u).Item("Fecha_Vto")
                        dtFacturas.Rows(u).Item("Grupo_Fact") = dvdtFacturas.Rows(u).Item("Grupo_Fact")
                        Select Case ConfigurationManager.AppSettings("Empresa").ToString()
                            Case "SUNCHALES", "MONTECARLO", "REALICO"
                                dtFacturas.Rows(u).Item("Importe") = dvdtFacturas.Rows(u).Item("Importe")
                            Case Else
                                dtFacturas.Rows(u).Item("Importe") = Format(dvdtFacturas.Rows(u).Item("Importe"), "fixed")
                        End Select
                        dtFacturas.Rows(u).Item("Factura") = dvdtFacturas.Rows(u).Item("Factura")
                        dtFacturas.Rows(u).Item("Conexion") = dvdtFacturas.Rows(u).Item("Conexion")
                        dtFacturas.Rows(u).Item("Pagada") = dvdtFacturas.Rows(u).Item("Pagada")
                        dtFacturas.Rows(u).Item("Socio") = dvdtFacturas.Rows(u).Item("Socio")
                        'TODO: Para la fila 10, si la factura está en la tabla nueva a crear "Pagos", mostrar "Pago en proceso" y quizás darle un color.
                        sumtotal = sumtotal + Convert.ToDouble(dvdtFacturas.Rows(u).Item("Importe"))
                    Next
                    'Me.txtDeudatotal.Text = Format((sumtotal), "0.00")

                Else
                    dvFacturas.RowFilter = "Socio =" & xmlsocio & " AND Conexion =" & conexion
                    'dvFacturas.RowFilter = "Conexion =" & conexion
                    dvFacturas.Sort = "periodo DESC"
                    dvdtFacturas = dvFacturas.ToTable
                    For u = 0 To dvdtFacturas.Rows.Count - 1
                        dtFacturas.Rows.Add()
                        dtFacturas.Rows(u).Item("Nro_Factura") = Mid(dvdtFacturas.Rows(u).Item("Factura"), 11, (Len(dvdtFacturas.Rows(u).Item("Factura")) - 10))
                        dtFacturas.Rows(u).Item("Pto_Venta") = dvdtFacturas.Rows(u).Item("Pto_Venta")
                        dtFacturas.Rows(u).Item("Letra") = dvdtFacturas.Rows(u).Item("Socio") & "/" & dvdtFacturas.Rows(u).Item("Conexion") 'dvdtFacturas.Rows(u).Item("Letra")
                        dtFacturas.Rows(u).Item("Periodo") = dvdtFacturas.Rows(u).Item("Periodo")
                        dtFacturas.Rows(u).Item("Fecha_Emision") = dvdtFacturas.Rows(u).Item("Fecha_Emision")
                        dtFacturas.Rows(u).Item("Fecha_Vto") = dvdtFacturas.Rows(u).Item("Fecha_Vto")
                        dtFacturas.Rows(u).Item("Grupo_Fact") = dvdtFacturas.Rows(u).Item("Grupo_Fact")
                        Select Case ConfigurationManager.AppSettings("Empresa").ToString()
                            Case "SUNCHALES", "MONTECARLO", "REALICO"
                                dtFacturas.Rows(u).Item("Importe") = dvdtFacturas.Rows(u).Item("Importe")
                            Case Else
                                dtFacturas.Rows(u).Item("Importe") = Format(dvdtFacturas.Rows(u).Item("Importe"), "fixed")
                        End Select
                        dtFacturas.Rows(u).Item("Factura") = dvdtFacturas.Rows(u).Item("Factura")
                        dtFacturas.Rows(u).Item("Conexion") = dvdtFacturas.Rows(u).Item("Conexion")
                        dtFacturas.Rows(u).Item("Pagada") = dvdtFacturas.Rows(u).Item("Pagada")
                        dtFacturas.Rows(u).Item("Socio") = dvdtFacturas.Rows(u).Item("Socio")
                        'TODO: Para la fila 10, si la factura está en la tabla nueva a crear "Pagos", mostrar "Pago en proceso" y quizás darle un color.
                        sumtotal = sumtotal + Convert.ToDouble(dvdtFacturas.Rows(u).Item("Importe"))
                    Next


                End If

                Me.GridView1.DataSource = ""
                Me.GridView1.DataSource = dtFacturas
                Me.GridView1.DataBind()
            Else
                login.CreateCookie("txtError", userBalancesResponse?.Message, False)
                Response.Redirect("~/Error.aspx")
            End If

        Catch ex As Exception
            login.CreateCookie("txtError", "Ocurrieron errores durante la recuperación de sus facturas.", False)
            Response.Redirect("~/Error.aspx")
        End Try
    End Sub
    Private Sub CargarComboConexiones(ByVal dtSocio As DataTable)

        For r = 0 To dtSocio.Rows.Count - 1
            Dim intSocio As Int32 = Convert.ToInt32(dtSocio.Rows(r).Item("Socio"))
            Dim strSocio As String = intSocio.ToString().PadLeft(6, "0")
            Dim miItem As New ListItem
            Dim itConexion = dtSocio.Rows(r).Item("Conexion")
            Dim itcnt As Int32
            Dim strConexion As String = ""
            strConexion = itConexion.ToString.PadLeft(4, "0")
            itcnt = LstConexiones.Items.Count
            miItem.Value = strSocio & "-" & strConexion
            miItem.Text = strSocio & "-" & strConexion
            LstConexiones.Items.Insert(r + 1, miItem)
            'lstConexiones.Items.Item(r + 1).Value = strConexion
            'lstConexiones.Items.Item(r + 1).Text = strSocio & "-" & strConexion
        Next r
        LstConexiones.SelectedValue = 0
    End Sub
    Private Sub RefreshMasterData()
        If Not IsNothing(Request.Cookies("IDUsuario")) Then
            Me.Master.Usuario = New Models.Usuario()
            Me.Master.Usuario.IDUsuario = Convert.ToInt32(Request.Cookies("IDUsuario").Value)
            Me.Master.Usuario.XmlSocio = Convert.ToInt32(Request.Cookies("xmlSocio").Value)
            Me.Master.Usuario.email = Request.Cookies("userEmail")?.Value?.ToString()
            Me.Master.Usuario.username = Request.Cookies("nomUsuario")?.Value?.ToString()
            Me.Master.Usuario.foto = Request.Cookies("Foto")?.Value?.ToString()
            Dim lblcliente As Label = CType(Page.Master.FindControl("lblcliente"), Label)
            lblcliente.Text = Me.Master.Usuario.username

            Dim fotocliente2 As Image = CType(Page.Master.FindControl("fotocliente2"), Image)
            fotocliente2.ImageUrl = Me.Master.Usuario.foto

            Dim lblcliente2 As Label = CType(Page.Master.FindControl("lblcliente2"), Label)
            lblcliente2.Text = Me.Master.Usuario.username

            Dim lblco1 As Label = CType(Page.Master.FindControl("lblco1"), Label)
            lblco1.Text = Request.Cookies("conCount")?.Value?.ToString()

            Dim lblco1txt As Label = CType(Page.Master.FindControl("lblco1txt"), Label)
            lblco1txt.Text = "Conexiones"
        Else
            Response.Redirect("~/login.aspx")
        End If

    End Sub

    Protected Sub BtnPagarMP_Click(sender As Object, e As ImageClickEventArgs) Handles BtnPagarMP.Click
        Dim chk As CheckBox, pagoEnProceso As Boolean
        Dim socio As String = "" '= Convert.ToInt32(CType(row1.Cells(13), DataControlFieldCell).Text)
        Dim idConexion As String = "" '= CType(row1.Cells(10), DataControlFieldCell).Text
        Dim numfact As String = "" '= CType(row1.Cells(2), DataControlFieldCell).Text
        Dim importe As String = "", importeFactura As Decimal  '= CType(row1.Cells(9), DataControlFieldCell).Text
        Dim HayChequeados As Boolean
        Dim factura As TrylogycWebsite.Common.DTO.DTOFactura
        Dim facturaList As New List(Of TrylogycWebsite.Common.DTO.DTOFactura)


        'importeFactura = ConvertirCampoImporteADecimal(importe)

        For Each Fila As GridViewRow In GridView1.Rows

            chk = Fila.FindControl("chkSel")
            If chk.Checked Then
                HayChequeados = chk.Checked
                socio = Fila.Cells(13).Text
                idConexion = Fila.Cells(10).Text
                numfact = Fila.Cells(2).Text
                importe = Fila.Cells(9).Text
                importeFactura = ConvertirCampoImporteADecimal(importe)


                If VerificarPagoEnProceso(socio, idConexion, numfact, importeFactura) Then
                    pagoEnProceso = True
                Else
                    factura = New TrylogycWebsite.Common.DTO.DTOFactura With
                    {
                        .IdSocio = socio,
                        .IdConexion = idConexion,
                        .NroFactura = numfact,
                        .ImporteFactura = ConvertirCampoImporteADecimal(importe)
                    }
                    facturaList.Add(factura)
                End If
            End If
        Next
        If HayChequeados Then
            If pagoEnProceso Then
                Response.Redirect("~/ConfirmarPago.aspx?numfact=" & LTrim(RTrim(numfact)) & "&importe=" & importe & "&idSocio=" & socio & "&idConexion=" & idConexion)
            Else
                If Not Session("facturasSeleccionadas") Is Nothing Then
                    Session.Remove("facturasSeleccionadas")
                End If
                System.Web.HttpContext.Current.Session("facturasSeleccionadas") = facturaList
                Response.Redirect("~/Pagar.aspx")
            End If
        End If
    End Sub

    Protected Sub BtnPagarMacro_Click(sender As Object, e As ImageClickEventArgs) Handles BtnPagarMacro.Click
        Dim chk As CheckBox, pagoEnProceso As Boolean
        Dim socio As String = "" '= Convert.ToInt32(CType(row1.Cells(13), DataControlFieldCell).Text)
        Dim idConexion As String = "" '= CType(row1.Cells(10), DataControlFieldCell).Text
        Dim numfact As String = "" '= CType(row1.Cells(2), DataControlFieldCell).Text
        Dim importe As String = "", importeFactura As Decimal  '= CType(row1.Cells(9), DataControlFieldCell).Text
        Dim HayChequeados As Boolean
        Dim factura As TrylogycWebsite.Common.DTO.DTOFactura
        Dim facturaList As New List(Of TrylogycWebsite.Common.DTO.DTOFactura)


        For Each Fila As GridViewRow In GridView1.Rows

            chk = Fila.FindControl("chkSel")
            If chk.Checked Then
                HayChequeados = chk.Checked
                socio = Fila.Cells(13).Text
                idConexion = Fila.Cells(10).Text
                numfact = Fila.Cells(2).Text
                importe = Fila.Cells(9).Text
                importeFactura = ConvertirCampoImporteADecimal(importe)

                If VerificarPagoEnProceso(socio, idConexion, numfact, importeFactura) Then
                    pagoEnProceso = True
                Else
                    factura = New TrylogycWebsite.Common.DTO.DTOFactura With
                    {
                        .IdSocio = socio,
                        .IdConexion = idConexion,
                        .NroFactura = numfact,
                        .ImporteFactura = ConvertirCampoImporteADecimal(importe)
                    }
                    facturaList.Add(factura)
                End If
            End If
        Next
        If HayChequeados Then
            If pagoEnProceso Then
                Response.Redirect("~/ConfirmarPago.aspx?numfact=" & LTrim(RTrim(numfact)) & "&importe=" & importe & "&idSocio=" & socio & "&idConexion=" & idConexion)
            Else
                If Not Session("facturasSeleccionadas") Is Nothing Then
                    Session.Remove("facturasSeleccionadas")
                End If
                System.Web.HttpContext.Current.Session("facturasSeleccionadas") = facturaList
                Response.Redirect("~/PagarMacro.aspx")
            End If
        End If

    End Sub


#End Region
End Class