Imports System.Collections.Generic
Imports Trylogyc.Models
Imports Trylogyc.DAL
Imports System.Data.Entity.Migrations
Imports System.Linq
Imports Trylogyc.Models.Usuario
Public Class Trylogyc
    Inherits System.Web.UI.MasterPage

    Private _Usuario As Usuario
    Public Property Usuario() As Usuario
        Get
            Return _Usuario
        End Get
        Set(ByVal value As Usuario)
            _Usuario = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If login.ValidateSessionCookie() = True Then

        Else
            Response.Redirect("~/login.aspx")
        End If


    End Sub

End Class