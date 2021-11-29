Imports Trylogyc.DAL
Imports System.Net.Mail
Imports TrylogycWebsite.Common.ServiceRequests
Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Threading.Tasks
Imports TrylogycWebsite.Common.ServiceResponses
Imports Helpers
Imports System.Net

Public Class Reestablecer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim myContext As New TrylogycContext
        Dim email As String = Request.Form("email")
        Dim codigo = Request.Form("codigo")

        Try
            '1.Setear endpoint
            Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "RetrievePassword")
            '2.Crear clase Request.
            Dim retrievePasswordRequest As New RetrievePasswordRequest
            retrievePasswordRequest.Email = email
            retrievePasswordRequest.CGP = codigo


            Dim serializedRequest As String = JsonConvert.SerializeObject(retrievePasswordRequest)

            '3.Invocar Servicio
            Dim responseMessage As HttpResponseMessage = Task.Run(Function()
                                                                      Return APIHelpers.PostAsync(apiEndpoint, serializedRequest, 60, Nothing)
                                                                  End Function).Result
            '4. Deserializar respuesta
            Dim registerResponse As RetrievePasswordResponse = JsonConvert.DeserializeObject(Of RetrievePasswordResponse)(responseMessage.Content.ReadAsStringAsync().Result)

            If registerResponse.StatusCode = HttpStatusCode.OK Then
                lblError.Text = "Datos correctos.Recibirá sus claves por email."
                lblError.ForeColor = Drawing.Color.Green
                lblError.Visible = True
            Else
                lblError.ForeColor = Drawing.Color.Red
                lblError.Text = registerResponse.Message
                lblError.Visible = True
            End If
        Catch ex As Exception
            lblError.ForeColor = Drawing.Color.Red
            lblError.Text = "Ocurrieron Errores durante la operación"
            lblError.Visible = True
        End Try



    End Sub



End Class