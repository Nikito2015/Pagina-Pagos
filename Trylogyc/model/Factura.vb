Imports System.Collections.Generic
Imports System
Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class Factura
        Private _IDFactura As Int32
        <Key>
        Public Property IDFactura() As Int32
            Get
                Return _IDFactura
            End Get
            Set(ByVal value As Int32)
                _IDFactura = value
            End Set
        End Property

        Private _IDConexion As Int32
        Public Property IDConexion() As Int32
            Get
                Return _IDConexion
            End Get
            Set(ByVal value As Int32)
                _IDConexion = value
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

        Private _periodo As String
        Public Property periodo() As String
            Get
                Return _periodo
            End Get
            Set(ByVal value As String)
                _periodo = value
            End Set
        End Property

        Private _fechaEmision As Date
        Public Property fechaEmision() As Date
            Get
                Return _fechaEmision
            End Get
            Set(ByVal value As Date)
                _fechaEmision = value
            End Set
        End Property

        Private _fechaVencimiento As Date
        Public Property fechaVencimiento() As Date
            Get
                Return _fechaVencimiento
            End Get
            Set(ByVal value As Date)
                _fechaVencimiento = value
            End Set
        End Property

        Private _importe As Double
        Public Property importe() As Double
            Get
                Return _importe
            End Get
            Set(ByVal value As Double)
                _importe = value
            End Set
        End Property
        Private _numero As String
        Public Property numero() As String
            Get
                Return _numero
            End Get
            Set(ByVal value As String)
                _numero = value
            End Set
        End Property

        Private _grupo As String
        Public Property grupo() As String
            Get
                Return _grupo
            End Get
            Set(ByVal value As String)
                _grupo = value
            End Set
        End Property
        Private _FacLetra As String
        Public Property FacLetra() As String
            Get
                Return _FacLetra
            End Get
            Set(ByVal value As String)
                _FacLetra = value
            End Set
        End Property
        Private _FacPtoVenta As String
        Public Property FacPtoVenta() As String
            Get
                Return _FacPtoVenta
            End Get
            Set(ByVal value As String)
                _FacPtoVenta = value
            End Set
        End Property


    End Class
End Namespace
