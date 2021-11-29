Imports Trylogyc.DAL
Imports System.Xml
Imports System.Linq
Imports System.IO
Imports CommonTrylogycWebsite.ServiceRequests
Imports Newtonsoft.Json
Imports System.Net.Http
Imports CommonTrylogycWebsite.ServiceResponses
Imports System.Net
Imports System.Threading.Tasks
Imports Helpers

Public Class relaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.txtcgp.Text = ""
            Me.txtcodsocio.Text = ""
            Me.divError.Visible = False
            Me.divSuccess.Visible = False
            Me.Form.DefaultButton = Me.btnrelacionar.UniqueID
        End If
    End Sub

    Protected Sub btnrelacionar_Click(sender As Object, e As EventArgs) Handles btnrelacionar.Click
        Dim myContext As New TrylogycContext

        Dim codigo = Me.txtcodsocio.Text
        Dim cgp = Me.txtcgp.Text

        Try
            '1.Setear Endpoint
            Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "AddRelation")
            '2.Crear clase Request
            Dim addRelationRequest As New AddRelationRequest()

            addRelationRequest.CGP = Me.txtcgp.Text
            addRelationRequest.AssociateCode = Me.txtcodsocio.Text
            addRelationRequest.UserId = Request.Cookies("IDUsuario")?.Value?.ToString()
            Dim serializedAddRelationRequest As String = JsonConvert.SerializeObject(addRelationRequest)

            '3.Invocar Servicio
            Dim addRelationResponseMessage As HttpResponseMessage = Task.Run(Function()
                                                                                 Return APIHelpers.PostAsync(apiEndpoint, serializedAddRelationRequest, 60, Nothing)
                                                                             End Function).Result
            '4. Deserializar respuesta
            Dim addRelationResponse As AddRelationResponse = JsonConvert.DeserializeObject(Of AddRelationResponse)(addRelationResponseMessage.Content.ReadAsStringAsync().Result)

            If addRelationResponse.StatusCode = HttpStatusCode.OK Then
                divSuccess.Visible = True
                divError.Visible = False
                lblSuccess.Text = "Relación Registrada Exitosamente. Vuelva a Ingresar al Sitio para ver los Cambios"
            Else
                divSuccess.Visible = False
                divError.Visible = True
                lblError.Text = addRelationResponse.Message
            End If
        Catch ex As Exception
            divSuccess.Visible = False
            divError.Visible = True
            lblError.Text = "Ocurrieron errores al crear la relación."
        End Try


    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        If lblSuccess.Text = "Relación Registrada Exitosamente. Vuelva a Ingresar al Sitio para ver los Cambios" Then
            Response.Redirect("~/login.aspx")
        Else
            Response.Redirect("~/Default.aspx")
        End If
    End Sub
End Class