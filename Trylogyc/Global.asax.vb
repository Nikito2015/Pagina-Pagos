Imports System.IO
Imports System.Web.Optimization
Imports System.Xml
Public Class Global_asax
    Inherits HttpApplication
    Protected Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Dim directoryName As String = Server.MapPath("/_tmp")
        If (System.IO.Directory.Exists(Server.MapPath("~/_tmp"))) Then
            For Each deleteFile In Directory.GetFiles(directoryName, "*.*", SearchOption.TopDirectoryOnly)
                File.Delete(deleteFile)
            Next
        End If

    End Sub


    Sub Application_Start(sender As Object, e As EventArgs)
        ' Se desencadena al iniciar la aplicación
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        'Dim xmlFile As XmlReader
        'Dim xmlFile2 As XmlReader
        'Dim ds As New DataSet()
        'Dim ds2 As New DataSet()
        'xmlFile = XmlReader.Create(Server.MapPath("/xmlfiles/SOCIOS.xml"), New XmlReaderSettings())
        'xmlFile2 = XmlReader.Create(Server.MapPath("/xmlfiles/SALDOS.xml"), New XmlReaderSettings())
        'ds.ReadXml(xmlFile)
        'ds2.ReadXml(xmlFile2)
        'Application("dtSocios") = ds.Tables(0)
        'Application("dtSaldos") = ds2.Tables(0)

    End Sub

    '<AttributeUsage(AttributeTargets.[Class] Or AttributeTargets.Method)>
    'Public Class NoDirectAccessAttribute
    '    Inherits ActionFilterAttribute

    '    Public Overrides Sub OnActionExecuting(ByVal filterContext As ActionExecutingContext)
    '        If filterContext.HttpContext.Request.UrlReferrer Is Nothing OrElse filterContext.HttpContext.Request.Url.Host <> filterContext.HttpContext.Request.UrlReferrer.Host Then
    '            filterContext.Result = New RedirectToRouteResult(New RouteValueDictionary(New With {Key .controller = "Home", Key .action = "Login", Key .area = ""}))
    '        End If
    '    End Sub
    'End Class
End Class