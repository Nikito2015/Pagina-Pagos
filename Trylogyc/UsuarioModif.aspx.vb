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

Public Class UsuarioModif
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        divError.Visible = False
        divSuccess.Visible = False
        If Not Page.IsPostBack Then
            aceptaEmail.Checked = Convert.ToBoolean(Request.Cookies("aceptaFacturaMail").Value)

        End If

    End Sub

    Protected Sub btnSavePass_Click(sender As Object, e As EventArgs) Handles btnSavePass.Click
        '1.Setear Endpoint
        Dim apiEndpoint As String = String.Format("{0}/{1}", ConfigurationManager.AppSettings("WebsiteAPIEndpoint").ToString(), "UpdateUserData")
        '2.Crear clase Request
        Dim userRequest As New UsuarioModifRequest()

        userRequest.OldPassword = Me.txtpassWordOld.Text
        userRequest.UserName = Request.Cookies("nomUsuario").Value
        userRequest.NewPassword = Me.txtPassWordNew.Text
        userRequest.NewPasswordConfirm = Me.txtPassWordNewCnf.Text
        userRequest.SendInvoiceEmail = Me.aceptaEmail.Checked

        Dim serializedLoginRequest As String = JsonConvert.SerializeObject(userRequest)

        '3.Invocar Servicio
        Dim userApiResponse As HttpResponseMessage = Task.Run(Function()
                                                                  Return APIHelpers.PostAsync(apiEndpoint, serializedLoginRequest, 60, Nothing)
                                                              End Function).Result
        '4. Deserializar respuesta
        Dim userResponse As UsuarioModifResponse = JsonConvert.DeserializeObject(Of UsuarioModifResponse)(userApiResponse.Content.ReadAsStringAsync().Result)

        '5 EValuar Respuesta
        If userResponse.StatusCode = HttpStatusCode.OK Then
            login.CreateCookie("aceptaFacturaMail", Me.aceptaEmail.Checked, False)
            divError.Visible = False
            divSuccess.Visible = True
            lblSuccess.Text = userResponse.Message
            txtPassWordNew.Text = ""
            txtPassWordNewCnf.Text = ""
            txtpassWordOld.Text = ""
        Else
            lblError.Text = userResponse.Message
            divError.Visible = True
            divSuccess.Visible = False
        End If

    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("~/Default.aspx")
    End Sub
End Class