Imports Trylogyc.Models
Imports Trylogyc.DAL
Imports System.IO
Imports System.Xml
Imports System.Net
Imports System.Reflection
Imports CommonTrylogycWebsite.ServiceResponses
Imports CommonTrylogycWebsite.ServiceRequests
Imports Helpers
Imports Newtonsoft.Json
Imports System.Net.Http
Imports CommonTrylogycWebsite.Models
Imports System.Threading.Tasks

Public Class login
    Inherits System.Web.UI.Page

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'login.ValidateSessionCookie() = False
        HttpContext.Current.Request.Cookies.Remove("SessionId")
        HttpContext.Current.Request.Cookies.Remove("IDUsuario")
        HttpContext.Current.Request.Cookies.Remove("xmlSocio")
        HttpContext.Current.Request.Cookies.Remove("nomUsuario")
        HttpContext.Current.Request.Cookies.Remove("userEmail")
        HttpContext.Current.Request.Cookies.Remove("aceptaFacturaMail")
        HttpContext.Current.Request.Cookies.Remove("conCount")
        HttpContext.Current.Request.Cookies.Remove("Foto")
        HttpContext.Current.Request.Cookies.Remove("Token")
        HttpContext.Current.Request.Cookies.Remove("txtError")

    End Sub

    Protected Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click

        '1.Setear Endpoint
        Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "login")
        '2.Crear clase Request
        Dim loginRequest As New LoginRequest()

        loginRequest.Password = Me.txtpassword.Text
        loginRequest.Email = Me.txtemail.Text
        Dim serializedLoginRequest As String = JsonConvert.SerializeObject(loginRequest)

        '3.Invocar Servicio
        Dim loginResponseMessage As HttpResponseMessage = Task.Run(Function()
                                                                       Return APIHelpers.PostAsync(apiEndpoint, serializedLoginRequest, 60, Nothing)
                                                                   End Function).Result
        '4. Deserializar respuesta
        Dim loginApiResponse As LoginResponse = JsonConvert.DeserializeObject(Of LoginResponse)(loginResponseMessage.Content.ReadAsStringAsync().Result)

        If loginApiResponse.StatusCode = HttpStatusCode.OK Then

            CreateSessionCookie()
            CreateCookie("IDUsuario", loginApiResponse.User.Id, False)
            CreateCookie("xmlSocio", loginApiResponse.User.Associates.FirstOrDefault().Id, False)
            CreateCookie("nomUsuario", loginApiResponse.User.UserName, False)
            CreateCookie("userEmail", loginApiResponse.User.Email, False)
            CreateCookie("aceptaFacturaMail", loginApiResponse.User.EmailInvoice, False)
            CreateCookie("conCount", loginApiResponse.User.TotalConnections, False)
            CreateCookie("Foto", loginApiResponse.User.Picture, False)
            CreateCookie("Token", loginApiResponse.Token, False)
            Response.Redirect(loginApiResponse.User.Route)
        Else
            lblError.Text = loginApiResponse.Message
            lblError.Visible = True
        End If

    End Sub
#End Region

#Region "Métodos Públicos"
    ''' <summary>
    ''' Creates the data table associates.
    ''' </summary>
    ''' <param name="associates">The associates.</param>
    ''' <returns></returns>
    Public Shared Function CreateDataTableAssociates(ByVal associates As WcfAssociate()) As DataTable
        Dim associatesList = associates?.[Select](Function(elem) New With {
        Key .Socio = elem.Id,
        Key .Conexion = elem.ConnectionId,
        Key .CGP = elem.CGP,
        Key .Nombre = elem.Name,
        Key .Direccion = elem.Address,
        Key .Localidad = elem.City,
        Key .Documento = elem.Document
    })

        Dim dtAssociates As New DataTable()
        dtAssociates.Columns.Add("Socio")
        dtAssociates.Columns.Add("Conexion")
        dtAssociates.Columns.Add("CGP")
        dtAssociates.Columns.Add("Nombre")
        dtAssociates.Columns.Add("Direcion")
        dtAssociates.Columns.Add("Localidad")
        dtAssociates.Columns.Add("Documento")

        For Each associate In associatesList
            dtAssociates.Rows.Add(New Object() {associate.Socio, associate.Conexion, associate.CGP, associate.Nombre, associate.Direccion, associate.Localidad, associate.Documento})
        Next
        CreateDataTableAssociates = dtAssociates

    End Function

    ''' <summary>
    ''' Creates the data table balances.
    ''' </summary>
    ''' <param name="balances">The balances.</param>
    ''' <returns></returns>
    Public Shared Function CreateDataTableBalances(ByVal balances As WcfBalance()) As DataTable
        Dim balancesList = balances?.[Select](Function(elem) New With {
            Key .Socio = elem.AssociateId,
            Key .Conexion = elem.ConnectionId,
            Key .Periodo = elem.Period,
            Key .Grupo_Fact = elem.InvoiceGroup,
            Key .Letra = elem.InvoiceLetter,
            Key .Pto_Venta = elem.InvoicePoint,
            Key .Numero = elem.InvoiceNumber,
            Key .Fecha_Emision = elem.InvoiceDate,
            Key .Fecha_Vto = elem.InvoiceExpirationDate,
            Key .Importe = elem.InvoiceAmmount,
            Key .Factura = elem.InvoiceTrackingNumber,
            Key .Pagada = elem.Paid,
            Key .codigoBarra = elem.CodigoBarra
        })

        Dim dtBalances As New DataTable()
        dtBalances.Columns.Add("Socio")
        dtBalances.Columns.Add("Conexion")
        dtBalances.Columns.Add("Periodo")
        dtBalances.Columns.Add("Grupo_Fact")
        dtBalances.Columns.Add("Letra")
        dtBalances.Columns.Add("Pto_Venta")
        dtBalances.Columns.Add("Numero")
        dtBalances.Columns.Add("Fecha_Emision")
        dtBalances.Columns.Add("Fecha_Vto")
        dtBalances.Columns.Add("Importe")
        dtBalances.Columns.Add("Factura")
        dtBalances.Columns.Add("Pagada")
        dtBalances.Columns.Add("codigoBarra")


        For Each balance In balancesList
            dtBalances.Rows.Add(New Object() {
            balance.Socio, balance.Conexion, balance.Periodo, balance.Grupo_Fact, balance.Letra, balance.Pto_Venta, balance.Numero, balance.Fecha_Emision,
            balance.Fecha_Vto, balance.Importe, balance.Factura, balance.Pagada, balance.codigoBarra
            })
        Next
        CreateDataTableBalances = dtBalances
    End Function
#End Region

#Region "Metodos privados"

    ''' <summary>
    ''' Creates the session cookie.
    ''' </summary>
    Private Sub CreateSessionCookie()
        CreateCookie("SessionId", Guid.NewGuid().ToString(), True)
    End Sub

    ''' <summary>
    ''' Validates the session cookie.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ValidateSessionCookie() As Boolean
        Dim sessionTimeOut As Int32 = Convert.ToInt32(ConfigurationManager.AppSettings("SessionTimeOut").ToString())
        Dim cookie As HttpCookie
        cookie = HttpContext.Current.Request.Cookies("SessionId")
        If cookie Is Nothing Then
            ValidateSessionCookie = False
        Else
            HttpContext.Current.Response.Cookies.Item("SessionId").Expires = DateTime.Now.AddMinutes(sessionTimeOut)
            ValidateSessionCookie = True
        End If
    End Function

    ''' <summary>
    ''' Creates the cookie.
    ''' </summary>
    ''' <param name="name">The name.</param>
    ''' <param name="value">The value.</param>
    Public Shared Sub CreateCookie(ByVal name As String, ByVal value As Object, expiracion As Boolean)
        HttpContext.Current.Response.Cookies.Remove(name)
        Dim sessionTimeOut As Int32 = Convert.ToInt32(ConfigurationManager.AppSettings("SessionTimeOut").ToString())
        Dim cookie As HttpCookie
        cookie = New HttpCookie(name)
        cookie.Value = value
        If expiracion Then cookie.Expires = DateTime.Now.AddMinutes(sessionTimeOut)
        HttpContext.Current.Response.Cookies.Add(cookie)
    End Sub
#End Region
End Class