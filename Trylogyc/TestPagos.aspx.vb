Imports MercadoPago
Imports MercadoPago.Resources
Imports MercadoPago.DataStructures.Preference
Imports MercadoPago.Common
Public Class TestPagos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (MercadoPago.SDK.AccessToken Is Nothing) Then
            MercadoPago.SDK.SetAccessToken("TEST-1038794855924875-061512-13c7f0558b15c628318c7ab5e1228751-584795990")
        End If

        Dim preference = New Preference()
        Dim item1 As New Item()
        item1.Title = "Mi producto"
        item1.Quantity = 1
        item1.UnitPrice = 5
        item1.CurrencyId = CurrencyId.ARS
        preference.Items.Add(item1)
        preference.Save()

        preferenceID.Value = preference.SandboxInitPoint
    End Sub

End Class