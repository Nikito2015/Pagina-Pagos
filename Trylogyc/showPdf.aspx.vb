Public Class showPdf
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim fileName As String = Request.Cookies("fileName")?.Value
        Response.Headers.Remove("Server")
        iframepdf.Attributes("src") = "~/_tmp/" & fileName
    End Sub

End Class