Imports MercadoPago
Imports MercadoPago.Resources
Imports MercadoPago.DataStructures.Preference
Imports MercadoPago.Common
Imports System.Globalization

Public Class PagarFactura
    Inherits System.Web.UI.Page

    Private _myUri As String
    Private _myHost As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _myUri = HttpContext.Current.Request.Url.AbsoluteUri
            _myHost = HttpContext.Current.Request.Url.Host
            ReEvaluarYSetearMercadoPagoToken()
            Dim nroFactura As String = Request.Params("numfact")
            Dim strImport As String = Request.Params("importe")
            Dim importeFactura As Decimal
            If (Decimal.TryParse(Request.Params("importe"),
                                 NumberStyles.AllowDecimalPoint,
                                 CultureInfo.CreateSpecificCulture("fr-FR"),
                                 importeFactura) = False) Then
                Response.Redirect("~/Default.aspx")
            End If
            GenerarPreferenciaPagoMercadoPago(nroFactura, importeFactura)

        Catch ex As Exception
            Response.Redirect("~/Default.aspx")
        End Try
    End Sub
    Private Sub ReEvaluarYSetearMercadoPagoToken()
        If (MercadoPago.SDK.AccessToken Is Nothing) Then
            MercadoPago.SDK.SetAccessToken("TEST-803559796931547-060612-07b906157e8808e7d657beabf35fdc1d-229476782")
        End If
    End Sub
    Private Sub GenerarPreferenciaPagoMercadoPago(nroFactura As String, importeFactura As Decimal)
        'Dim importeAPagar As Decimal
        Dim myPreference As New Preference()
        myPreference.ClientId = "1"

        Dim itemToAdd As New Item()
        itemToAdd.Title = nroFactura
        itemToAdd.Quantity = 1
        itemToAdd.CurrencyId = CurrencyId.ARS
        itemToAdd.UnitPrice = 1
        myPreference.Items.Add(itemToAdd)
        Dim _backUrls As New BackUrls()
        _backUrls.Success = "www.google.com.ar"
        _backUrls.Failure = "www.google.com.ar"
        _backUrls.Pending = "www.mercadolibre.com.ar"
        myPreference.BackUrls = _backUrls
        myPreference.AutoReturn = AutoReturnType.approved
        myPreference.Save()
        preferenceID.Value = myPreference.Id
    End Sub

    Protected Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect("~/Default.aspx")
    End Sub
End Class