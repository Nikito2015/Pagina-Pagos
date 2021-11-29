Public Class ConfirmarPago
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnContinuar_Click(sender As Object, e As EventArgs) Handles btnContinuar.Click
        Dim nroFactura As String = Request.Params("numfact")
        Dim strImport As String = Request.Params("importe")
        Dim idSocio As Int32 = Request.Params("idSocio")
        Dim idConexion As Int32 = Request.Params("idConexion")
        Response.Redirect("~/Pagar.aspx?numfact=" & LTrim(RTrim(nroFactura)) & "&importe=" & strImport & "&idSocio=" & idSocio & "&idConexion=" & idConexion)
    End Sub
    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("~/Default.aspx")
    End Sub
End Class