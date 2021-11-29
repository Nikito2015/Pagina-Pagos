Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Mail

Public Class EscuchaPagos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try

                Call SaveToLog(Request.RawUrl)
                Response.Write(Request.RawUrl)
                Response.StatusCode = HttpStatusCode.OK
                Response.Flush()
            Catch ex As Exception
                Response.StatusCode = HttpStatusCode.InternalServerError
                Response.Write(ex.ToString())
                Response.Flush()
            Finally

            End Try
        End If
    End Sub

    Private Sub SaveToLog(ByVal textToSave As String)
        Dim prmParametro As New SqlParameter
        Dim cn As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
        Dim daPagosNoProcesados As SqlDataAdapter = New SqlDataAdapter("", cn)
        Dim Command As SqlCommand = New SqlCommand("", cn)
        Dim sQuery As String = Nothing
        sQuery = " INSERT INTO Log(LogText) VALUES (@textToSave) "
        Command.CommandText = sQuery
        Command.Parameters.Clear()
        Command.Parameters.AddWithValue("@textToSave", textToSave)
        cn.Open()
        Command.ExecuteNonQuery()
        Command.Dispose()
        cn.Close()
    End Sub


End Class