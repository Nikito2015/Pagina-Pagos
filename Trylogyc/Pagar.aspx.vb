Imports MercadoPago
Imports MercadoPago.Resources
Imports MercadoPago.DataStructures.Preference
Imports MercadoPago.Common
Imports System.Globalization
Imports Trylogyc.DAL
Imports TrylogycWebsite.Common.ServiceRequests
Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Threading.Tasks
Imports TrylogycWebsite.Common.ServiceResponses
Imports System.Net
Imports Helpers
Imports CommonTrylogycWebsite.ServiceResponses
Imports CommonTrylogycWebsite.ServiceRequests

Public Class Pagar
    Inherits System.Web.UI.Page

    Private _myUri As String
    Private _myHost As String
    Private _isSandbox As Boolean
    Private _sandboxPostBackUrl As String
    Private _productionPostBackUrl As String
    Protected originalUri As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            defaultUrl.Value = HttpContext.Current.Request.UrlReferrer.ToString()
            _isSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings("isSandbox"))
            _sandboxPostBackUrl = ConfigurationManager.AppSettings("sandboxPostbackUrl")
            _productionPostBackUrl = ConfigurationManager.AppSettings("productionPostbackUrl")
            _myUri = HttpContext.Current.Request.Url.AbsoluteUri
            _myHost = HttpContext.Current.Request.Url.Host
            ReEvaluarYSetearMercadoPagoToken()
            Dim nroFactura As String = "" '= Request.Params("numfact")
            'Dim strImport As String = Request.Params("importe")
            'Dim idSocio As Int32 = Request.Params("idSocio")
            'Dim idConexion As Int32 = Request.Params("idConexion")
            'Nos aseguramos que nadie cambie los valores de la query string manualmente. Si lo hacen lo redirigimos a default.

            If IsNothing(HttpContext.Current.Request.UrlReferrer) Then
                Response.Redirect("~/Default.aspx")
            Else
                If Not IsNothing(HttpContext.Current.Request.UrlReferrer.AbsolutePath) Then
                    If HttpContext.Current.Request.UrlReferrer.AbsolutePath.Equals("/Pagar") Then
                        Response.Redirect("~/Default.aspx")
                    End If
                End If
            End If

            Dim facturaList As New List(Of TrylogycWebsite.Common.DTO.DTOFactura)
            Dim importeaEnviar As Decimal
            facturaList = Session("facturasSeleccionadas")
            For Each list As TrylogycWebsite.Common.DTO.DTOFactura In facturaList
                importeaEnviar += list.ImporteFactura
            Next
            'Seteamos el importe en decimal

            'If (Decimal.TryParse(Request.Params("importe"),
            'NumberStyles.AllowDecimalPoint,
            'CultureInfo.CreateSpecificCulture("fr-FR"),
            'importeaEnviar) = False) Then
            'Response.Redirect("~/Default.aspx")
            'End If

            'Dim preferencia As String = GenerarPreferenciaPagoMercadoPago(nroFactura, importeFactura, Nothing)
            'Crear Pago
            Dim idPlataforma As Int32 = CrearRegistroPagoEnCurso(nroFactura, facturaList)
            If idPlataforma <= 0 Then
                Response.Cookies.Remove("txtError")
                Response.Cookies.Remove("codError")
                login.CreateCookie("txtError", "Se produjo un error durante el proceso de pago y su pago no pudo ser realizado.", False)
                Try
                    Response.Redirect("~/Error.aspx")
                Catch ex As Exception
                    Throw ex
                End Try
            End If
            'Crear preferencia
            If facturaList.Count > 1 Then
                nroFactura = facturaList.Count & " Comprobantes Seleccionados: "
            Else
                nroFactura = facturaList.Count & " Comprobante Seleccionado: "
            End If

            Dim preferencia As String = GenerarPreferenciaPagoMercadoPago(nroFactura, importeaEnviar, idPlataforma)
            'Actualizar Pago con IdPreferencia
            Dim pagoActualizado As Boolean = ActualizarPagoPreferencia(idPlataforma, preferencia)
            If pagoActualizado = False Then
                login.CreateCookie("txtError", "Se produjo un error durante el proceso de pago y su pago no pudo ser realizado.", False)
                Try
                    Response.Redirect("~/Error.aspx")
                Catch ex As Exception
                    Throw ex
                End Try
            End If

        Catch ex As Exception
            If Response.Cookies("txtError") Is Nothing Then
                Response.Redirect("~/Default.aspx")
            Else
                Response.Redirect("~/Error.aspx")
            End If

        End Try

    End Sub
    Private Sub ReEvaluarYSetearMercadoPagoToken()
        Dim token As String
        If (MercadoPago.SDK.AccessToken Is Nothing) Then
            If _isSandbox = True Then
                token = ConfigurationManager.AppSettings("tokenMpagoSandBox")
            Else
                token = ConfigurationManager.AppSettings("tokenMpagoProduccion")
            End If
            MercadoPago.SDK.SetAccessToken(token)

        End If
    End Sub
    Private Function GenerarPreferenciaPagoMercadoPago(nroFactura As String, importeFactura As Decimal, ByVal idPago As String) As String
        Dim myPreference As New Preference()
        'myPreference.ClientId = "580564279"
        'myPreference.Payer = RegistrarDatosClienteMercadoPago()
        Dim itemToAdd As New Item()
        itemToAdd.Title = nroFactura
        itemToAdd.Quantity = 1
        itemToAdd.CurrencyId = CurrencyId.ARS
        itemToAdd.UnitPrice = importeFactura
        myPreference.Items.Add(itemToAdd)
        Dim _backUrls As New BackUrls()
        _backUrls.Success = Request.Url.GetLeftPart(UriPartial.Path).Replace("/Pagar", "/PagoExitoso.aspx")
        _backUrls.Failure = Request.Url.GetLeftPart(UriPartial.Path).Replace("/Pagar", "/PagoRechazado.aspx")
        _backUrls.Pending = Request.Url.GetLeftPart(UriPartial.Path).Replace("/Pagar", "/PagoPendiente.aspx")
        myPreference.BackUrls = _backUrls
        myPreference.AutoReturn = AutoReturnType.all
        myPreference.BinaryMode = True

        myPreference.ExternalReference = idPago.ToString()
        myPreference.Save()
        If _isSandbox = True Then
            preferenceID.Value = myPreference.SandboxInitPoint
        Else
            preferenceID.Value = myPreference.InitPoint
        End If
        'preferenceID.Value = myPreference.SandboxInitPoint
        Return myPreference.Id
    End Function

    Private Function RegistrarDatosClienteMercadoPago() As Payer
        Dim payer As New Payer()
        payer.Email = Request.Cookies("userEmail")?.Value?.ToString()
        payer.Name = Request.Cookies("nomUsuario")?.Value?.ToString()
        Return payer
    End Function

    Private Function CrearRegistroPagoEnCurso(nroFactura As String, facturaList As List(Of TrylogycWebsite.Common.DTO.DTOFactura)) As Int32
        '  Dim myContext As New TrylogycContext
        ' Dim pagoRegistrado As Int32 = myContext.CrearPago(nroFactura, importe, Nothing, idSocio, idConexion)
        ' Return pagoRegistrado

        '1.Setear Endpoint
        Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "RegisterPayment")
        '2.Crear clase Request
        Dim Request As New RegisterPaymentRequest() With
            {.nroFactura = nroFactura,
             .idMedioPago = 0,
             .Facturas = facturaList}

        Dim serializedLoginRequest As String = JsonConvert.SerializeObject(Request)

        '3.Invocar Servicio
        Dim userApiResponse As HttpResponseMessage = Task.Run(Function()
                                                                  Return APIHelpers.PostAsync(apiEndpoint, serializedLoginRequest, 60, Nothing)
                                                              End Function).Result
        '4. Deserializar respuesta
        Dim Response As RegisterPaymentResponse = JsonConvert.DeserializeObject(Of RegisterPaymentResponse)(userApiResponse.Content.ReadAsStringAsync().Result)

        '5 EValuar Respuesta
        If Response.StatusCode = HttpStatusCode.OK Then
            Return Response.idPlataforma
        Else
            Return 0
        End If


    End Function

    Private Function ActualizarPagoPreferencia(ByVal idPago As Int32, ByVal preference As String) As Boolean
        '  Dim myContext As New TrylogycContext
        ' Dim pagoActualizado As Boolean = myContext.ActualizarPagoPorId(idPago, preference)
        ' Return pagoActualizado
        '1.Setear Endpoint
        Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "UpdatePayment")
        '2.Crear clase Request
        Dim Request As New UpdatePaymentRequest()

        Request.idPlataforma = idPago
        Request.preference = preference
        Request.TransaccionComercioId = TransaccionComercioId

        Dim serializedLoginRequest As String = JsonConvert.SerializeObject(Request)

        '3.Invocar Servicio
        Dim userApiResponse As HttpResponseMessage = Task.Run(Function()
                                                                  Return APIHelpers.PostAsync(apiEndpoint, serializedLoginRequest, 60, Nothing)
                                                              End Function).Result
        '4. Deserializar respuesta
        Dim Response As UpdatePaymentResponse = JsonConvert.DeserializeObject(Of UpdatePaymentResponse)(userApiResponse.Content.ReadAsStringAsync().Result)

        '5 EValuar Respuesta
        If Response.StatusCode = HttpStatusCode.OK Then
            Return True
        Else
            Return False
        End If

    End Function
End Class