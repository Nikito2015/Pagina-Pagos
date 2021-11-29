Imports System.Collections.Generic
Imports System
Imports System.ComponentModel.DataAnnotations
Imports System.Web
Imports Trylogyc.Models
Imports Trylogyc.DAL
Imports System.Data.Entity.Migrations
Imports System.Linq


Namespace Models
    Public Class Usuario
        Private _IDUsuario As Int32
        <Key()>
        Public Property IDUsuario() As Int32
            Get
                Return _IDUsuario
            End Get
            Set(ByVal value As Int32)
                _IDUsuario = value
            End Set
        End Property
        Private _XmlSocio As Int32
        Public Property XmlSocio() As Int32
            Get
                Return _XmlSocio
            End Get
            Set(ByVal value As Int32)
                _XmlSocio = value
            End Set
        End Property
        Private _email As String
        Public Property email() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property
        Private _userName As String
        Public Property username() As String
            Get
                Return _userName
            End Get
            Set(ByVal value As String)
                _userName = value
            End Set
        End Property
        Private _passWord As String
        Public Property passWord() As String
            Get
                Return _passWord
            End Get
            Set(ByVal value As String)
                _passWord = value
            End Set
        End Property
        Private _foto As String
        Public Property foto() As String
            Get
                Return _foto
            End Get
            Set(ByVal value As String)
                _foto = value
            End Set
        End Property
        Private _ruta As String
        Public Property ruta() As String
            Get
                Return _ruta
            End Get
            Set(ByVal value As String)
                _ruta = value
            End Set
        End Property

        Private _enviarFacturaEmail As Boolean
        Public Property EnviarFacturaEmail() As Boolean
            Get
                Return _enviarFacturaEmail
            End Get
            Set(ByVal value As Boolean)
                _enviarFacturaEmail = value
            End Set
        End Property
        'Public Function GetUsuario(ByVal IDUsuario As Int32) As Usuario
        '    Dim myContext As New TrylogycContext
        '    Dim users = myContext.GetUsuario
        '    Dim miUsuario As New Usuario
        '    For u = 0 To users.Count - 1
        '        If users.Item(u).IDUsuario = IDUsuario Then
        '            With miUsuario
        '                .IDUsuario = IDUsuario
        '                .IDSocio = users.Item(u).IDSocio
        '                .email = users.Item(u).email
        '                .foto = users.Item(u).foto
        '                .username = users.Item(u).username
        '                .passWord = users.Item(u).passWord
        '            End With
        '            Return miUsuario
        '            Exit For


        '        Else

        '        End If
        '    Next
        '    Return miUsuario
        'End Function
    End Class
End Namespace
