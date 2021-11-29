Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System

Namespace Models
    Public Class Socios
        Private _IDSocio As Int32
        <Key()>
        Public Property IDSocio() As Int32
            Get
                Return _IDSocio
            End Get
            Set(ByVal value As Int32)
                _IDSocio = value
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
        Private _nombre As String
        Public Property nombre() As String
            Get
                Return _nombre
            End Get
            Set(ByVal value As String)
                _nombre = value
            End Set
        End Property
        Private _DNI As String
        Public Property DNI() As String
            Get
                Return _DNI
            End Get
            Set(ByVal value As String)
                _DNI = value
            End Set
        End Property

        Private _conexiones As ICollection(Of Conexion)
        Public Property conexiones As ICollection(Of Conexion)
            Get
                Return _conexiones
            End Get
            Set(ByVal value As ICollection(Of Conexion))
                _conexiones = value
            End Set
        End Property
    End Class
End Namespace