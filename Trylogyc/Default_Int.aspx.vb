Imports Trylogyc.DAL
Imports System.IO
Public Class Default_Int
    Inherits System.Web.UI.Page
    Dim myContext = New TrylogycContext
    Dim sumtotal As Double
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If login.ValidateSessionCookie() = True Then

            If Not Page.IsPostBack Then
                Me.divError.Visible = False
                Me.lblCliente.Text = "Socio nro. " & Session("xmlSocio")
                'traer listado de conexiones para el socio
                lstConexiones.Items.Insert(0, " Todas")
                lstConexiones.Items.Item(0).Value = 0
                Dim dtSocio As DataTable = Session("dtSocio")

                'Llenar el Combo
                Dim lenSocio = 6 - Len(Session("xmlSocio").ToString)
                Dim strSocio As String = ""
                For s = 0 To lenSocio - 1
                    strSocio = "0" & strSocio
                Next
                strSocio = strSocio & Session("xmlSocio").ToString

                For r = 0 To dtSocio.Rows.Count - 1
                    Dim miItem As New ListItem
                    Dim itConexion = dtSocio.Rows(r).Item("Conexion")
                    Dim itcnt As Int32
                    Dim strConexion As String = ""
                    Dim lenConexion = 4 - Len(itConexion.ToString)
                    For t = 0 To lenConexion - 1
                        strConexion = "0" & strConexion
                    Next
                    strConexion = strConexion & itConexion.ToString
                    itcnt = lstConexiones.Items.Count
                    miItem.Value = itConexion
                    miItem.Text = strSocio & "-" & strConexion
                    lstConexiones.Items.Insert(r + 1, miItem)
                    lstConexiones.Items.Item(r + 1).Value = itConexion
                    lstConexiones.Items.Item(r + 1).Text = strSocio & "-" & strConexion
                Next r
                lstConexiones.SelectedValue = 0
                readFacturas(Session("xmlSocio"), lstConexiones.SelectedValue)
                Me.txtDeudatotal.Text = Format((sumtotal), "0.00")
            Else
                If login.ValidateSessionCookie() = True Then
                Else
                    Response.Redirect("~/login_int.aspx")
                End If
            End If
        Else
            Response.Redirect("~/login_int.aspx")
        End If

    End Sub
    Private Sub readFacturas(ByVal xmlsocio As String, ByVal conexion As Int32)
        Try
            Dim dvFacturas As New DataView(Session("dtSaldo"))
            Dim dtFacturas As New DataTable
            Dim dvdtFacturas As New DataTable
            For t = 0 To 9
                dtFacturas.Columns.Add()
            Next
            If conexion = 0 Then
                'dvFacturas.RowFilter = "Socio =" & xmlsocio
                dvFacturas.Sort = "PERIODO DESC"
                dvdtFacturas = dvFacturas.ToTable
                For u = 0 To dvdtFacturas.Rows.Count - 1
                    dtFacturas.Rows.Add()
                    dtFacturas.Rows(u).Item(0) = Mid(dvdtFacturas.Rows(u).Item("Factura"), 11, (Len(dvdtFacturas.Rows(u).Item("Factura")) - 10))
                    dtFacturas.Rows(u).Item(1) = dvdtFacturas.Rows(u).Item("Pto_Venta")
                    dtFacturas.Rows(u).Item(2) = dvdtFacturas.Rows(u).Item("Letra")
                    dtFacturas.Rows(u).Item(3) = dvdtFacturas.Rows(u).Item("Periodo")
                    dtFacturas.Rows(u).Item(4) = dvdtFacturas.Rows(u).Item("Fecha_Emision")
                    dtFacturas.Rows(u).Item(5) = dvdtFacturas.Rows(u).Item("Fecha_Vto")
                    dtFacturas.Rows(u).Item(6) = dvdtFacturas.Rows(u).Item("Grupo_Fact")
                    dtFacturas.Rows(u).Item(7) = dvdtFacturas.Rows(u).Item("Importe")
                    dtFacturas.Rows(u).Item(8) = dvdtFacturas.Rows(u).Item("Factura")
                    dtFacturas.Rows(u).Item(9) = dvdtFacturas.Rows(u).Item("Conexion")
                    sumtotal = sumtotal + Convert.ToDouble(dvdtFacturas.Rows(u).Item("Importe"))
                Next
                Me.txtDeudatotal.Text = Format((sumtotal), "0.00")
                Me.GridView1.DataSource = ""
                Me.GridView1.DataSource = dtFacturas
                Me.GridView1.DataBind()
                GridView1.Columns(9).Visible = False
                GridView1.Columns(10).Visible = False

            Else
                dvFacturas.RowFilter = "Socio =" & xmlsocio
                dvFacturas.RowFilter = "Conexion =" & conexion
                dvFacturas.Sort = "periodo DESC"
                dvdtFacturas = dvFacturas.ToTable
                For u = 0 To dvdtFacturas.Rows.Count - 1
                    dtFacturas.Rows.Add()
                    dtFacturas.Rows(u).Item(0) = Mid(dvdtFacturas.Rows(u).Item("Factura"), 11, (Len(dvdtFacturas.Rows(u).Item("Factura")) - 10))
                    dtFacturas.Rows(u).Item(1) = dvdtFacturas.Rows(u).Item("Pto_Venta")
                    dtFacturas.Rows(u).Item(2) = dvdtFacturas.Rows(u).Item("Letra")
                    dtFacturas.Rows(u).Item(3) = dvdtFacturas.Rows(u).Item("Periodo")
                    dtFacturas.Rows(u).Item(4) = dvdtFacturas.Rows(u).Item("Fecha_Emision")
                    dtFacturas.Rows(u).Item(5) = dvdtFacturas.Rows(u).Item("Fecha_Vto")
                    dtFacturas.Rows(u).Item(6) = dvdtFacturas.Rows(u).Item("Grupo_Fact")
                    dtFacturas.Rows(u).Item(7) = dvdtFacturas.Rows(u).Item("Importe")
                    dtFacturas.Rows(u).Item(8) = dvdtFacturas.Rows(u).Item("Factura")
                    dtFacturas.Rows(u).Item(9) = dvdtFacturas.Rows(u).Item("Conexion")
                    sumtotal = sumtotal + Convert.ToDouble(dvdtFacturas.Rows(u).Item("Importe"))
                Next
                Me.GridView1.DataSource = ""
                Me.GridView1.DataSource = dtFacturas
                Me.GridView1.DataBind()
                GridView1.Columns(9).Visible = False
                GridView1.Columns(10).Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lstConexiones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstConexiones.SelectedIndexChanged
        Me.divError.Visible = False
        Dim intSocio As Int32
        Dim intConexion As Int32
        intSocio = Session("xmlSocio")
        intConexion = lstConexiones.SelectedValue
        readFacturas(intSocio, intConexion)
        Me.GridView1.Caption = "Facturas Conexión " & lstConexiones.SelectedItem.ToString
        Dim dv As DataView = New DataView(Session("dtsocio"))
        For Each row As DataRowView In dv
            If row("CONEXION") = intConexion Then
                Me.txtNombre.Text = row("NOMBRE")
                Me.txtDireccion.Text = row("DIRECION")
                Me.txtLocalidad.Text = row("LOCALIDAD")
                Me.txtDeudatotal.Text = Format((sumtotal), "0.00")
                Exit For
            End If
        Next
        lstConexiones.Focus()
    End Sub
    Protected Sub gridview1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType <> DataControlRowType.Footer And GridView1.Rows.Count > 0 Then
            'e.Row.Cells(8).CssClass = "badge bg-red"
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(0).HorizontalAlign = VerticalAlign.Middle
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).HorizontalAlign = VerticalAlign.Middle
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(4).HorizontalAlign = VerticalAlign.Middle
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(6).HorizontalAlign = VerticalAlign.Middle
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(8).HorizontalAlign = VerticalAlign.Middle
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(8).HorizontalAlign = VerticalAlign.Middle
            e.Row.Font.Size = FontSize.Large
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(7).Visible = False

            'e.Row.Cells(0).Width = "15"
            'e.Row.Cells(1).Width = "100"
            'e.Row.Cells(2).Width = "50"
            'e.Row.Cells(3).Width = "30"
            'e.Row.Cells(4).Width = "50"
            'e.Row.Cells(5).Width = "70"
            'e.Row.Cells(6).Width = "70"
            'e.Row.Cells(7).Width = "70"
            'e.Row.Cells(8).Width = "80"
        End If
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = "Imprimir"
            e.Row.Cells(1).Text = "Factura"
            e.Row.Cells(2).Text = "Pto Venta"
            e.Row.Cells(3).Text = "Letra"
            e.Row.Cells(4).Text = "Período"
            e.Row.Cells(5).Text = "Fecha Emisión"
            e.Row.Cells(6).Text = "Fecha Vto"
            e.Row.Cells(7).Text = "Grupo"
            e.Row.Cells(8).Text = "Importe"
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(7).Visible = False
            'e.Row.Cells(0).Width = "15"
            'e.Row.Cells(1).Width = "100"
            'e.Row.Cells(2).Width = "50"
            'e.Row.Cells(3).Width = "30"
            'e.Row.Cells(4).Width = "50"
            'e.Row.Cells(5).Width = "70"
            'e.Row.Cells(6).Width = "70"
            'e.Row.Cells(7).Width = "70"
            'e.Row.Cells(8).Width = "80"
            e.Row.Cells(9).ForeColor = Drawing.Color.Transparent
            e.Row.Cells(10).ForeColor = Drawing.Color.Transparent
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim numfact As String = CType(e.Row.Cells(9), DataControlFieldCell).Text
            Dim filePDF As String = "~/PDF/" & LTrim(RTrim(numfact)) & ".pdf"
            Dim filePDF2 As String = "PDF/" & LTrim(RTrim(numfact)) & ".pdf"
            e.Row.Cells(0).Attributes("data-id") = filePDF2
            e.Row.Cells(0).Attributes("class") = "botonpdf"
            'If (File.Exists(Server.MapPath(filePDF2))) Then
            '    Response.Redirect(filePDF)
            'Else
            '    Me.lblError.Text = "No se ha encontrado el archivo."
            '    Me.divError.Visible = True
            'End If
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(0).HorizontalAlign = VerticalAlign.Middle
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).HorizontalAlign = VerticalAlign.Middle
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(4).HorizontalAlign = VerticalAlign.Middle
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(6).HorizontalAlign = VerticalAlign.Middle
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(8).HorizontalAlign = VerticalAlign.Middle
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(8).HorizontalAlign = VerticalAlign.Middle
            e.Row.Font.Size = FontSize.Large
            e.Row.Cells(9).ForeColor = Drawing.Color.Transparent
            e.Row.Cells(10).ForeColor = Drawing.Color.Transparent
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(7).Visible = False
        End If

    End Sub
    Protected Sub gridview1_rowcommand(sender As Object, e As CommandEventArgs) Handles GridView1.RowCommand
        If (e.CommandName = "cmdPDF2") Then
            Dim index1 As Integer = Convert.ToInt32(e.CommandArgument) 'captura el valor del índice de la fila
            Dim row1 As GridViewRow = GridView1.Rows(index1) 'crea un objeto row que contiene la fila del botón presionado.
            Dim numfact As String = CType(row1.Cells(9), DataControlFieldCell).Text
            Dim filePDF As String = "~/PDF/" & LTrim(RTrim(numfact)) & ".pdf"
            Dim filePDF2 As String = "PDF/" & LTrim(RTrim(numfact)) & ".pdf"
            Me.divError.Visible = False
            If (File.Exists(Server.MapPath(filePDF2))) Then
                'Response.Redirect(filePDF)
                'SendToPrinter(filePDF)
                Dim myPrint As String
                myPrint = "$('#pdf-iframe').attr(" & "'" & "src" & "'" & "," & "'" & filePDF2 & "'" & ")" & ".load(function () {document.getElementById('pdf-iframe').contentWindow.print();});"
                ClientScript.RegisterStartupScript(GetType(Page), "a key", "<script type=""text/javascript"">" + myPrint + "</script>")
                'Response.Write(myPrint)
            Else

                Me.lblError.Text = "No se ha encontrado el archivo."
                Me.divError.Visible = True
            End If
        End If


    End Sub
    Private Sub SendToPrinter(ByVal filePDF As String)
        Dim info As New ProcessStartInfo()
        info.Verb = "print"
        info.FileName = Server.MapPath(filePDF)
        info.CreateNoWindow = True
        info.WindowStyle = ProcessWindowStyle.Hidden
        Dim p As New Process()
        p.StartInfo = info
        p.Start()

        p.WaitForInputIdle()
        System.Threading.Thread.Sleep(3000)
        If False = p.CloseMainWindow() Then
            p.Kill()
        End If
    End Sub

    Protected Sub cmdPDF2_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        If Me.lstConexiones.SelectedIndex < Me.lstConexiones.Items.Count - 1 Then
            Dim n As Int32
            n = Me.lstConexiones.SelectedIndex
            Me.lstConexiones.SelectedIndex = n + 1
            lstConexiones_SelectedIndexChanged(sender, e)
        End If

    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If Me.lstConexiones.SelectedIndex > 0 Then
            Dim n As Int32
            n = Me.lstConexiones.SelectedIndex
            Me.lstConexiones.SelectedIndex = n - 1
            lstConexiones_SelectedIndexChanged(sender, e)
        End If

    End Sub
End Class