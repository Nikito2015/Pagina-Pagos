Imports System.Collections.Generic
Imports System
Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class Conexion
        Private _IDConexion As Int32
        <Key()>
        Public Property IDConexion() As Int32
            Get
                Return _IDConexion
            End Get
            Set(ByVal value As Int32)
                _IDConexion = value
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
        Private _XmlConexion As Int32
        Public Property XmlConexion() As Int32
            Get
                Return _XmlConexion
            End Get
            Set(ByVal value As Int32)
                _XmlConexion = value
            End Set
        End Property
        Private _CGP As String
        Public Property CGP() As String
            Get
                Return _CGP
            End Get
            Set(ByVal value As String)
                _CGP = value
            End Set
        End Property
        Private _IDSocio As Int32
        Public Property IDSocio() As Int32
            Get
                Return _IDSocio
            End Get
            Set(ByVal value As Int32)
                _IDSocio = value
            End Set
        End Property

        Private _direccion As String
        Public Property direccion() As String
            Get
                Return _direccion
            End Get
            Set(ByVal value As String)
                _direccion = value
            End Set
        End Property

        Private _localidad As String
        Public Property localidad() As String
            Get
                Return _localidad
            End Get
            Set(ByVal value As String)
                _localidad = value
            End Set
        End Property

        Private _facturas As ICollection(Of Factura)
        Public Property facturas As ICollection(Of Factura)
            Get
                Return _facturas
            End Get
            Set(ByVal value As ICollection(Of Factura))
                _facturas = value
            End Set
        End Property
    End Class
End Namespace