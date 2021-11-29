Imports Trylogyc.DAL
Imports Trylogyc.DAL.TrylogycContext

Imports TrylogycWebsite.Common.ServiceRequests
Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Threading.Tasks
Imports TrylogycWebsite.Common.ServiceResponses
Imports System.Net
Imports Helpers
Imports CommonTrylogycWebsite.ServiceResponses
Imports CommonTrylogycWebsite.ServiceRequests
Imports System.IO

Public Class PagoExitoso
    Inherits System.Web.UI.Page

    Public IdComprobante As String
    Public Class JsonMacroClick
        Public Property TransaccionComercioId As String
        Public Property TransaccionPlataformaId As String
    End Class
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (Request.Params("idPago") <> "") Then
                Dim idPago As Int32 = Request.Params("idPago")
                Dim TransaccionComercioId As String = Request.Params("TransaccionComercioID")

                Dim pagoRegistrado As Boolean = ActualizarEstadoPago(idPago, "", EstadosPagos.Aprobado)
                lblComprobante.Text = TransaccionComercioId
            Else
                Dim collection As String = Request.Params("collection_id")
                Dim merchantOrder As String = Request.Params("merchant_order_id")
                Dim preference As String = Request.Params("preference_id")
                Dim myContext As New TrylogycContext
                ' Dim pagoRegistrado As Boolean = myContext.ActualizarPagoPorPreferencia(preference, EstadosPagos.Aprobado, collection, merchantOrder)

                Dim pagoRegistrado As Boolean = ActualizarPagoPorPreferencia(preference, EstadosPagos.Aprobado, collection, merchantOrder)
                lblComprobante.Text = collection
            End If
        End If
    End Sub

    Private Function ActualizarEstadoPago(ByVal idPago As Int32, ByVal transaccionPlataformaId As String, ByVal estadoPago As Int32) As Boolean

        '1.Setear Endpoint
        Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "UpdateStatusPayment")
        '2.Crear clase Request
        Dim Request As New UpdateStatusPaymentRequest()

        Request.idPago = idPago
        Request.transaccionPlataformaId = transaccionPlataformaId
        Request.estadoPago = estadoPago

        Dim serializedLoginRequest As String = JsonConvert.SerializeObject(Request)

        '3.Invocar Servicio
        Dim userApiResponse As HttpResponseMessage = Task.Run(Function()
                                                                  Return APIHelpers.PostAsync(apiEndpoint, serializedLoginRequest, 60, Nothing)
                                                              End Function).Result
        '4. Deserializar respuesta
        Dim Response As UpdateStatusPaymentResponse = JsonConvert.DeserializeObject(Of UpdateStatusPaymentResponse)(userApiResponse.Content.ReadAsStringAsync().Result)

        '5 EValuar Respuesta
        If Response.StatusCode = HttpStatusCode.OK Then
            Return True
        Else
            Return False
        End If

    End Function


    Private Function ActualizarPagoPorPreferencia(ByVal preferenceId As String, ByVal estado As Int32, ByVal collection As String, ByVal merchantOrder As String) As Boolean

        '1.Setear Endpoint
        Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "UpdatePaymentMP")
        '2.Crear clase Request
        Dim Request As New UpdatePaymentMPRequest()

        Request.preferenceId = preferenceId
        Request.estado = estado
        Request.collection = collection
        Request.merchantOrder = merchantOrder


        Dim serializedLoginRequest As String = JsonConvert.SerializeObject(Request)

        '3.Invocar Servicio
        Dim userApiResponse As HttpResponseMessage = Task.Run(Function()
                                                                  Return APIHelpers.PostAsync(apiEndpoint, serializedLoginRequest, 60, Nothing)
                                                              End Function).Result
        '4. Deserializar respuesta
        Dim Response As UpdateStatusPaymentResponse = JsonConvert.DeserializeObject(Of UpdateStatusPaymentResponse)(userApiResponse.Content.ReadAsStringAsync().Result)

        '5 EValuar Respuesta
        If Response.StatusCode = HttpStatusCode.OK Then
            Return True
        Else
            Return False
        End If

    End Function

End Class