Imports Trylogyc.DAL
Imports System.Net.Mail
Imports System.Net
Imports Newtonsoft.Json
Imports CommonTrylogycWebsite.ServiceRequests
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Helpers
Imports CommonTrylogycWebsite.ServiceResponses

Public Class register
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.Form.DefaultButton = Me.btnRegister.UniqueID
        End If
    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click

        Dim email = Request.Form("email")
        Dim confEmail = Request.Form("confirmEmail")
        Dim codigo = Request.Form("codigo")
        Dim cgp = Request.Form("cgp")

        '1.Setear endpoint
        Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "Register")
        '2.Crear clase Request.
        Dim registerRequest As New RegisterRequest()
        registerRequest.Email = email
        registerRequest.EmailConfirm = confEmail
        registerRequest.Code = codigo
        registerRequest.CGP = cgp
        registerRequest.EmailInvoices = Convert.ToBoolean(aceptaEmail.Checked)

        Dim serializedRequest As String = JsonConvert.SerializeObject(registerRequest)

        '3.Invocar Servicio
        Dim responseMessage As HttpResponseMessage = Task.Run(Function()
                                                                  Return APIHelpers.PostAsync(apiEndpoint, serializedRequest, 60, Nothing)
                                                              End Function).Result
        '4. Deserializar respuesta
        Dim registerResponse As RegisterResponse = JsonConvert.DeserializeObject(Of RegisterResponse)(responseMessage.Content.ReadAsStringAsync().Result)

        If registerResponse.StatusCode = HttpStatusCode.OK Then
            lblError.Text = "Registro Exitoso! Recibirá sus claves por email"
            lblError.ForeColor = Drawing.Color.Green
            lblError.Visible = True
        Else
            lblError.Text = registerResponse.Message
            lblError.Visible = True
        End If
    End Sub

End Class