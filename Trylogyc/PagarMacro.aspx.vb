Imports SHA256EncryptNetFramework
Imports System.Globalization
Imports Trylogyc.DAL

Imports System.Xml
Imports System.Linq
Imports System.IO
Imports Trylogyc.Models
Imports TrylogycWebsite.Common.ServiceRequests
Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Threading.Tasks
Imports TrylogycWebsite.Common.ServiceResponses
Imports System.Net
Imports Helpers
Imports CommonTrylogycWebsite.ServiceResponses
Imports CommonTrylogycWebsite.ServiceRequests

Public Class PagarMacro
    Inherits System.Web.UI.Page

    Private _myUri As String
    Private _myHost As String
    Private _isSandbox As Boolean
    Private _sandboxPostBackUrl As String
    Private _productionPostBackUrl As String
    Protected originalUri As String

    Public Class DatosMacro
        Public Property preference As String
        Public Property TransaccionComercioId As String
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            defaultUrl.Value = HttpContext.Current.Request.UrlReferrer.ToString()
            _isSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings("isSandbox"))
            _sandboxPostBackUrl = ConfigurationManager.AppSettings("sandboxPostbackUrl")
            _productionPostBackUrl = ConfigurationManager.AppSettings("productionPostbackUrl")
            _myUri = HttpContext.Current.Request.Url.AbsoluteUri
            _myHost = HttpContext.Current.Request.Url.Host

            Dim nroFactura As String = "" 'Request.Params("numfact")
            'Dim strImport As String = Request.Params("importe")
            'Dim idSocio As Int32 = Request.Params("idSocio")
            'Dim idConexion As Int32 = Request.Params("idConexion")

            'Nos aseguramos que nadie cambie los valores de la query string manualmente. Si lo hacen lo redirigimos a default.
            If IsNothing(HttpContext.Current.Request.UrlReferrer) Then
                Response.Redirect("~/Default.aspx")
            Else
                If Not IsNothing(HttpContext.Current.Request.UrlReferrer.AbsolutePath) Then
                    If HttpContext.Current.Request.UrlReferrer.AbsolutePath.Equals("/PagarMacro") Then
                        Response.Redirect("~/Default.aspx")
                    End If
                End If
            End If

            Dim facturaList As New List(Of TrylogycWebsite.Common.DTO.DTOFactura)
            Dim importeaEnviar As Decimal, sumoFacturas As Int64
            facturaList = Session("facturasSeleccionadas")
            For Each list As TrylogycWebsite.Common.DTO.DTOFactura In facturaList
                importeaEnviar += list.ImporteFactura
                sumoFacturas += list.NroFactura
            Next

            'Seteamos el importe en decimal
            'Dim importeFactura As Decimal
            'If (Decimal.TryParse(Request.Params("importe"),
            'NumberStyles.AllowDecimalPoint,
            'CultureInfo.CreateSpecificCulture("fr-FR"),
            'importeFactura) = False) Then
            'Response.Redirect("~/Default.aspx")
            'End If

            'Crear Pago
            Dim idPlataforma As Int32 = CrearRegistroPagoEnCursoMacro(nroFactura, facturaList)
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
            nroFactura = Convert.ToString(sumoFacturas)
            Dim datosMacro As DatosMacro = GenerarPreferenciaPagoMacro(nroFactura, importeaEnviar, idPlataforma)

            'Actualizar Pago con IdPreferencia
            Dim pagoActualizado As Boolean = ActualizarPagoPreferenciaMacro(idPlataforma, datosMacro.preference, datosMacro.TransaccionComercioId)
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
    Private Function CrearRegistroPagoEnCursoMacro(nroFactura As String, facturaList As List(Of TrylogycWebsite.Common.DTO.DTOFactura)) As Int32

        '1.Setear Endpoint
        Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "RegisterPayment")

        '2.Crear clase Request
        Dim Request As New RegisterPaymentRequest() With
        {.nroFactura = nroFactura,
         .idMedioPago = 1,
         .Facturas = facturaList
        }
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
    Private Function ActualizarPagoPreferenciaMacro(ByVal idPago As Int32, ByVal preference As String, ByVal TransaccionComercioId As String) As Boolean

        '1.Setear Endpoint
        Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "UpdatePayment")
        '2.Crear clase Request
        Dim Request As New UpdatePaymentRequest() With {
            .idPlataforma = idPago,
            .preference = preference,
            .TransaccionComercioId = TransaccionComercioId
        }


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

    Private Function GenerarPreferenciaPagoMacro(nroFactura As String, importeFactura As Decimal, idPago As Int32) As DatosMacro

        Dim DatosMacro As New DatosMacro
        Dim secretkey As String
        'Dim Random As New Random()

        'obtemos la ip a encriptar
        Dim ip = System.Web.HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If (String.IsNullOrEmpty(ip)) Then
            ip = System.Web.HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
        End If

        'obtenemos los valores otorgados por Macro click de pagos
        If _isSandbox = True Then
            secretkey = ConfigurationManager.AppSettings("secretKeySandboxMacro")
            Comercio.Value = ConfigurationManager.AppSettings("guidComercioSandboxMacro")
            urlEnvio.Value = ConfigurationManager.AppSettings("urlEnvioSandbox")
        Else
            secretkey = ConfigurationManager.AppSettings("secretKeyProduccionMacro")
            Comercio.Value = ConfigurationManager.AppSettings("guidComercioProduccionMacro")
            urlEnvio.Value = ConfigurationManager.AppSettings("urlEnvioProduccion")
        End If


        'obtenemos el importe sin comas ni puntos
        Importe.Value = Convert.ToString(importeFactura).Replace(",", "")

        'asignamos un valor único por cada transacción para enviar en el campo TransaccionComercioID
        TransaccionComercioID.Value = Convert.ToString(idPago) + nroFactura '+ Convert.ToString(Random.Next(1, 1000))

        'generamos el hash
        Hash.Value = SHA256Hash.Generate(ip, secretkey, Comercio.Value, String.Empty, Importe.Value)

        'obtenemos la descripción que se mostrará al realizar el pago
        Producto.Value = ConfigurationManager.AppSettings("descClientePagoMacro")

        'asignamos valores a url de retorno 
        Dim callback_Success = Request.Url.GetLeftPart(UriPartial.Path).Replace("/PagarMacro", String.Format("/PagoExitoso.aspx?idPago=" & idPago & "&TransaccionComercioID=" & TransaccionComercioID.Value))
        Dim callback_Cancel = Request.Url.GetLeftPart(UriPartial.Path).Replace("/PagarMacro", String.Format("/PagoRechazado.aspx?idPago=" & idPago))
        Dim callback_Pending = Request.Url.GetLeftPart(UriPartial.Path).Replace("/PagarMacro", String.Format("/PagoPendiente.aspx?idPago=" & idPago & "&TransaccionComercioID=" & TransaccionComercioID.Value))

        'encriptamos los datos a enviar en el formulario post
        CallbackSuccess.Value = AESEncrypter.EncryptString(callback_Success, secretkey)
        CallbackCancel.Value = AESEncrypter.EncryptString(callback_Cancel, secretkey)
        CallbackPending.Value = AESEncrypter.EncryptString(callback_Pending, secretkey)
        Monto.Value = AESEncrypter.EncryptString(Importe.Value, secretkey)
        SucursalComercio.Value = AESEncrypter.EncryptString(String.Empty, secretkey)

        DatosMacro.preference = Hash.Value
        DatosMacro.TransaccionComercioId = TransaccionComercioID.Value

        Return DatosMacro

    End Function

End Class