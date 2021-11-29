Imports System.Data.SqlClient
Imports System.IO
Public Class Admin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack Then
            If login.ValidateSessionCookie() = True Then
            Else
                Response.Redirect("~/login.aspx")
            End If
        Else
            If login.ValidateSessionCookie() = True Then
            Else
                Response.Redirect("~/login.aspx")
            End If
        End If



    End Sub

    Protected Sub btnMigracion_Click(sender As Object, e As EventArgs) Handles btnMigracion.Click
        Dim ds As New DataSet
        Dim myPath = Server.MapPath("/xmlfiles")
        'ds.ReadXml(myPath & "/SOCIOS.xml")
        Dim myFile = myPath & "/SOCIOS.xml"
        Dim myFile2 = myPath & "/SALDOS.xml"
        Dim Xml As String = File.ReadAllText(myFile)
        Dim Xml2 As String = File.ReadAllText(myFile2)
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
        Dim command As New SqlCommand("", con)
        Dim i As Integer = 0
        Dim sql As String = Nothing

        With command
            .CommandType = CommandType.StoredProcedure
            .CommandText = "InsertXML"
            .Parameters.AddWithValue("@xml", Xml)
            .Parameters.AddWithValue("@xml2", Xml2)
        End With

        Try
            con.Open()
            command.ExecuteNonQuery()
            command.Dispose()
            con.Close()
        Catch ex As Exception
            Throw (ex)
        End Try
    End Sub

End Class