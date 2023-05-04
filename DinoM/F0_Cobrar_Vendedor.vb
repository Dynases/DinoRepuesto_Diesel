﻿Imports GMap.NET.WindowsForms
Imports GMap.NET.WindowsForms.ToolTips
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports GMap.NET.MapProviders
Imports GMap.NET
Imports GMap.NET.WindowsForms.Markers
Imports System.Reflection
Imports System.Runtime.InteropServices
Public Class F0_Cobrar_Vendedor
#Region "Variables Globales"
    Dim precio As DataTable
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim Bin As New MemoryStream
    Dim _inter As Integer = 0

    ''Modo de Pago
    Public TotalBs As Double = 0
    Public TotalSus As Double = 0
    Public TotalTarjeta As Double = 0
    Public TipoCambio As Double = 0
    Public TipoVenta As Integer = 1
    Public FechaVenc As Date
    Public Banco As Integer = 0
    Public Glosa As String
    Public CostoEnvio As Double = 0
    Public cambio As Double = 0
    Dim _CodCliente As Integer = 0

    Dim Saldo As Double = 0
    Dim Total As Double = 0

#End Region
#Region "METODOS PRIVADOS"

    Private Sub _IniciarTodo()


        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        'Me.WindowState = FormWindowState.Maximized
        _prAsignarPermisos()
        Me.Text = "PAGO CLIENTE POR VENDEDOR"
        Dim blah As New Bitmap(New Bitmap(My.Resources.cobro), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prCargarTablaPagos2(-1)
        tbCodigo.ReadOnly = True
        tbNombre.ReadOnly = True
        tbFechaVenta.Value = Now.Date
        tbFechaFactura.Value = Now.Date
        tbCodigo.Focus()

    End Sub
    Private Sub _prCargarTablaPagos2(_numi As Integer)

        Dim dt As New DataTable
        dt = L_fnObtenerLasVentasCreditoPorVendedorFecha(_numi, tbFechaFactura.Value.ToString("yyyy/MM/dd"))

        '_prCargarIconDelete(dt)
        gr_detalle.DataSource = dt
        gr_detalle.RetrieveStructure()
        gr_detalle.AlternatingColors = True
        '      ' a.tcnumi, NroDoc,as factura,a.tctv1numi ,a.tcty4clie ,  cliente,a.tcty4vend, vendedor,a.tcfdoc
        ',a.tcfvencre,totalfactura, pendiente, PagoAc, Pagar
        'With gr_detalle.RootTable.Columns("factura")
        '    .Width = 100
        '    .Visible = False
        'End With
        With gr_detalle.RootTable.Columns("tctv1numi")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("tcty4vend")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("vendedor")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("tcnumi")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("NroDoc")
            .Width = 120
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .Caption = "Nro documento"
        End With

        With gr_detalle.RootTable.Columns("tcty4clie")
            .Width = 150
            .Visible = True
            .Caption = "Codigo Cliente"
        End With
        With gr_detalle.RootTable.Columns("cliente")
            .Width = 200
            .Visible = True
            .Caption = "Cliente"
        End With

        With gr_detalle.RootTable.Columns("tcfdoc")
            .Caption = "Fecha Factura"
            .Width = 120
            .TextAlignment = TextAlignment.Center
            .Visible = True
        End With

        With gr_detalle.RootTable.Columns("tcfvencre")
            .Caption = "Fecha Vencimiento"
            .TextAlignment = TextAlignment.Center
            .Width = 160
            .Visible = True
        End With
        With gr_detalle.RootTable.Columns("totalfactura")
            .Caption = "Monto Total"
            .Width = 120
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
            .Visible = True
        End With
        With gr_detalle.RootTable.Columns("pendiente")
            .Caption = "Saldo"
            .Width = 120
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
            .Visible = True
        End With

        With gr_detalle.RootTable.Columns("PagoAc")
            .Caption = "Total Pagado"
            .Width = 180
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
            .Visible = True
        End With

        With gr_detalle.RootTable.Columns("Pagar")
            .Width = 100
            .Visible = True
            .Caption = "Pagar!"
        End With



        With gr_detalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007


            .VisualStyle = VisualStyle.Office2007


            .RowHeaders = InheritableBoolean.True
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
        End With
        _prAplicarCondiccionJanus()
        _prCalcularTotal()
    End Sub

    Private Sub _Limpiar()
        tbCodigo.Clear()
        tbFechaVenta.Value = Now.Date
        tbNombre.Clear()
        tbTotalCobrado.Text = 0
        tbTotalCobrar.Text = 0
        tbSaldo.Text = 0
        tbCodigo.Clear()
        tbNombre.Clear()
        tbFechaFactura.Value = Now.Date
        _prCargarTablaPagos2(-1)
        '_prAddDetalle()
        tbCodigo.Focus()



    End Sub
    Private Sub _prAsignarPermisos()

        Dim dtRolUsu As DataTable = L_prRolDetalleGeneral(gi_userRol, _nameButton)

        Dim show As Boolean = dtRolUsu.Rows(0).Item("ycshow")
        Dim add As Boolean = dtRolUsu.Rows(0).Item("ycadd")
        Dim modif As Boolean = dtRolUsu.Rows(0).Item("ycmod")
        Dim del As Boolean = dtRolUsu.Rows(0).Item("ycdel")

        If add = False Then
            btnNuevo.Visible = False
        End If
        If modif = False Then
            btnModificar.Visible = False
        End If
        If del = False Then
            btnEliminar.Visible = False
        End If
    End Sub
    Private Sub _prCargarTablaPagos(_numi As Integer)

        Dim dt As New DataTable
        dt = L_fnObtenerLasVentasCreditoPorVendedorFecha(_numi, tbFechaFactura.Value.ToString("yyyy/MM/dd"))
        If (dt.Rows.Count <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "No Hay Datos Para Mostrar".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return
        End If
        '_prCargarIconDelete(dt)
        gr_detalle.DataSource = dt
        gr_detalle.RetrieveStructure()
        gr_detalle.AlternatingColors = True
        '      ' a.tcnumi, NroDoc,as factura,a.tctv1numi ,a.tcty4clie ,  cliente,a.tcty4vend, vendedor,a.tcfdoc
        ',a.tcfvencre,totalfactura, pendiente, PagoAc, Pagar
        With gr_detalle.RootTable.Columns("factura")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("tctv1numi")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("tcty4vend")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("vendedor")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("tcnumi")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("NroDoc")
            .Width = 120
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .Caption = "Nro documento"
        End With

        With gr_detalle.RootTable.Columns("tcty4clie")
            .Width = 150
            .Visible = True
            .Caption = "Codigo Cliente"
        End With
        With gr_detalle.RootTable.Columns("cliente")
            .Width = 200
            .Visible = True
            .Caption = "Cliente"
        End With

        With gr_detalle.RootTable.Columns("tcfdoc")
            .Caption = "Fecha Factura"
            .Width = 120
            .TextAlignment = TextAlignment.Center
            .Visible = True
            .FormatString = "dd/MM/yyyy"
        End With

        With gr_detalle.RootTable.Columns("tcfvencre")
            .Caption = "Fecha Vencimiento"
            .TextAlignment = TextAlignment.Center
            .Width = 160
            .Visible = True

        End With
        With gr_detalle.RootTable.Columns("totalfactura")
            .Caption = "Monto Total"
            .Width = 120
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
            .Visible = True
        End With
        With gr_detalle.RootTable.Columns("pendiente")
            .Caption = "Saldo"
            .Width = 120
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
            .Visible = True
        End With

        With gr_detalle.RootTable.Columns("PagoAc")
            .Caption = "Total Pagado"
            .Width = 180
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
            .Visible = True
        End With

        With gr_detalle.RootTable.Columns("Pagar")
            .Width = 100
            .Visible = True
            .Caption = "Pagar!"
        End With



        With gr_detalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007


            .VisualStyle = VisualStyle.Office2007


            .RowHeaders = InheritableBoolean.True
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
        End With
        _prAplicarCondiccionJanus()
        _prCalcularTotal()
    End Sub
    Public Sub _prAplicarCondiccionJanus()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(gr_detalle.RootTable.Columns("pendiente"), ConditionOperator.Equal, 0)
        fc.FormatStyle.BackColor = Color.Green
        gr_detalle.RootTable.FormatConditions.Add(fc)
    End Sub


    Private Sub F0_Cobrar_Cliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub

    Private Sub tbCodigo_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCodigo.KeyDown
        'If (_fnAccesible()) Then
        If e.KeyData = Keys.Control + Keys.Enter Then
            _Limpiar()
            Dim dt As DataTable

            dt = L_fnListarClientesTodos()
            '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
            ',a.ydtelf1 ,a.ydfnac 

            Dim listEstCeldas As New List(Of Modelo.Celda)
            listEstCeldas.Add(New Modelo.Celda("ydnumi,", False, "ID", 50))
            listEstCeldas.Add(New Modelo.Celda("codigo,", False, "ID", 50))
            listEstCeldas.Add(New Modelo.Celda("ydcod", True, "CODIGO", 80))
            listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", False, "RAZON SOCIAL", 180))
            listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 350))
            listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
            listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
            listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
            listEstCeldas.Add(New Modelo.Celda("ydfnac", False, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
            listEstCeldas.Add(New Modelo.Celda("ydnumivend,", False, "ID", 50))
            listEstCeldas.Add(New Modelo.Celda("vendedor,", False, "ID", 50))
            listEstCeldas.Add(New Modelo.Celda("yddias", False, "CRED", 50))
            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 2
            ef.listEstCeldas = listEstCeldas
            ef.alto = 50
            ef.ancho = 350
            'ef.NameLabel = "CLIENTE :"
            'ef.NamelColumna = "yddesc"
            ef.Context = "Seleccione Cliente".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Try
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    _CodCliente = Row.Cells("ydnumi").Value
                    tbCodigo.Text = Row.Cells("codigo").Value
                    tbNombre.Text = Row.Cells("yddesc").Value
                    _prCargarTablaPagos(Row.Cells("ydnumi").Value)
                Catch ex As Exception

                End Try
            Else
                _CodCliente = 0
                tbCodigo.Clear()
                tbNombre.Clear()
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "Los Datos Del Cliente No Existe en el sistema".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If

        End If
        'End If
    End Sub

    Private Sub Bt1Generar_Click(sender As Object, e As EventArgs) Handles Bt1Generar.Click
        If (tbCodigo.Text <> String.Empty) Then
            tbSaldo.Value = 0
            tbTotalCobrado.Value = 0
            tbTotalCobrar.Value = 0
            _prCargarTablaPagos(tbCodigo.Text)
        End If
    End Sub

    Private Sub gr_detalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles gr_detalle.EditingCell
        If (e.Column.Index = gr_detalle.RootTable.Columns("Pagar").Index) Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub gr_detalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles gr_detalle.CellValueChanged
        ''Dim rowIndex As Integer = gr_detalle.CurrentRow.RowIndex
        Dim rowIndex As Integer = gr_detalle.Row
        'Columna de Precio Venta



        If (e.Column.Index = gr_detalle.RootTable.Columns("Pagar").Index) Then
            Dim ob As Boolean = gr_detalle.GetValue("Pagar")

            If (ob = True) Then
                If Saldo > 0 And Total > 0 Then
                    If gr_detalle.GetValue("pendiente") < Saldo And gr_detalle.GetValue("pendiente") < Total Then
                        'pendiente, PagoAc, Pagar
                        tbTotalCobrado.Value = tbTotalCobrado.Value + gr_detalle.GetValue("pendiente")
                        tbSaldo.Value = tbSaldo.Value - gr_detalle.GetValue("pendiente")

                        gr_detalle.SetValue("PagoAc", gr_detalle.GetValue("pendiente"))
                        gr_detalle.SetValue("pendiente", 0)
                        Saldo = Saldo - gr_detalle.GetValue("PagoAc")
                        Total = Total - gr_detalle.GetValue("PagoAc")

                    Else
                        tbTotalCobrado.Value = tbTotalCobrado.Value + Total
                        tbSaldo.Value = tbSaldo.Value - Saldo
                        gr_detalle.SetValue("PagoAc", Total)
                        gr_detalle.SetValue("pendiente", gr_detalle.GetValue("pendiente") - Total)
                        Saldo = 0
                        Total = 0
                    End If
                Else
                    gr_detalle.SetValue("Pagar", False)
                End If

            Else
                'If gr_detalle.GetValue("pendiente") > 0 Then
                tbTotalCobrado.Value = tbTotalCobrado.Value - gr_detalle.GetValue("PagoAc")
                tbSaldo.Value = tbSaldo.Value + gr_detalle.GetValue("PagoAc")
                Saldo = Saldo + gr_detalle.GetValue("PagoAc")
                Total = Total + gr_detalle.GetValue("PagoAc")
                gr_detalle.SetValue("pendiente", gr_detalle.GetValue("pendiente") + gr_detalle.GetValue("PagoAc"))
                    gr_detalle.SetValue("PagoAc", 0)

            End If
                '_prCalcularTotal()
            End If


    End Sub
    Public Sub _prCalcularTotal()


        tbSaldo.Text = gr_detalle.GetTotal(gr_detalle.RootTable.Columns("pendiente"), AggregateFunction.Sum)
        tbTotalCobrar.Text = gr_detalle.GetTotal(gr_detalle.RootTable.Columns("PagoAc"), AggregateFunction.Sum) + gr_detalle.GetTotal(gr_detalle.RootTable.Columns("pendiente"), AggregateFunction.Sum)
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _modulo.Select()
        _tab.Close()
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click
        _modulo.Select()
        _tab.Close()
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs)
        Dim numi As String = ""
        Dim img2 As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        If (tbCodigo.Text = String.Empty) Then
            ToastNotification.Show(Me, "No existen datos validos".ToUpper, img2, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return

        End If
        If (CType(gr_detalle.DataSource, DataTable).Rows.Count <= 0) Then
            ToastNotification.Show(Me, "No existen datos validos".ToUpper, img2, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return

        End If
        Dim dtCobro As DataTable = L_fnCobranzasObtenerLosPagos(-1)
        Dim bandera As Boolean = False
        _prInterpretarDatosCobranza(dtCobro, bandera)
        If (bandera = False) Then
            ToastNotification.Show(Me, "Seleccione un detalle de la lista de pendientes".ToUpper, img2, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return
        End If
        Dim res As Boolean = L_fnGrabarCobranza2(numi, tbFechaVenta.Value.ToString("yyyy/MM/dd"), 0, "", dtCobro)


        If res Then

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "El Pago Ha Sido ".ToUpper + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )


            _Limpiar()

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Compra no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
    End Sub

#End Region

#Region "Eventos Formulario"
    Sub _prInterpretarDatosCobranza(ByRef dt As DataTable, ByRef bandera As Boolean)


        '       numidetalle, NroDoc, factura, numiCredito, numiCobranza, A.tctv1numi
        ',a.tcty4clie ,cliente,detalle.tdfechaPago, PagoAc, NumeroRecibo, DescBanco, banco, detalle.tdnrocheque,
        'img,estado,pendiente
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        Dim dtcobro As DataTable = CType(gr_detalle.DataSource, DataTable)
        For i As Integer = 0 To dtcobro.Rows.Count - 1 Step 1
            Dim pago As Double = dtcobro.Rows(i).Item("PagoAc")
            Dim estado As Boolean = dtcobro.Rows(i).Item("Pagar")
            If (estado = True) Then
                '             td.tdtv12numi ,@tenumi ,td.tdnrodoc ,@newFecha ,td.tdmonto ,td.tdnrorecibo ,td.tdty3banco,
                'td.tdnrocheque, @newFecha  ,@newHora  ,@teuact

                '              a.tcnumi, NroDoc,as factura, a.tctv1numi, a.tcty4clie, cliente, a.tcty4vend, vendedor, a.tcfdoc
                ',a.tcfvencre,totalfactura, pendiente, PagoAc, Pagar
                If (pago > 0) Then
                    dt.Rows.Add(0, dtcobro.Rows(i).Item("tcnumi"), 0, dtcobro.Rows(i).Item("NroDoc"),
                                            Now.Date, pago, 0, 1, 0, Now.Date,
                                            "", "", Bin.ToArray, 0)
                    bandera = True
                End If

            End If

        Next
    End Sub


    Private Sub ButtonX1_Click_1(sender As Object, e As EventArgs) Handles ButtonX1.Click
        Dim numi As String = ""
        Dim img2 As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        If (tbCodigo.Text = String.Empty) Then
            ToastNotification.Show(Me, "No existen datos validos".ToUpper, img2, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return

        End If
        If (CType(gr_detalle.DataSource, DataTable).Rows.Count <= 0) Then
            ToastNotification.Show(Me, "No existen datos validos".ToUpper, img2, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return

        End If
        Dim dtCobro As DataTable = L_fnCobranzasObtenerLosPagos(-1)
        Dim bandera As Boolean = False
        Dim Notas As String = ""
        _prInterpretarDatosCobranza(dtCobro, bandera)
        If (bandera = False) Then
            ToastNotification.Show(Me, "Seleccione un detalle de la lista de pendientes".ToUpper, img2, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return
        End If

        Dim res As Boolean = L_fnGrabarCobranza(numi, tbFechaVenta.Value.ToString("yyyy/MM/dd"), tbCodigo.Text, "", gi_userSuc, dtCobro)




        If res Then

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "El Pago Ha Sido ".ToUpper + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )

            _prGuardarCobro(dtCobro)
            _Limpiar()

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Compra no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
    End Sub

    Private Function _fnIsALl()
        Dim dt As DataTable = CType(gr_detalle.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            If (CType(gr_detalle.DataSource, DataTable).Rows(i).Item("Pagar") = False) Then
                Return False
            End If



        Next
        Return True
    End Function

    Private Sub ButtonX3_Click(sender As Object, e As EventArgs) Handles ButtonX3.Click
        Dim Saldo As Double = 0

        Dim dt As DataTable = CType(gr_detalle.DataSource, DataTable)
        If (dt.Rows.Count > 0) Then
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                If (CType(gr_detalle.DataSource, DataTable).Rows(i).Item("Pagar") = False) Then
                    If Total > 0 Then
                        If CType(gr_detalle.DataSource, DataTable).Rows(i).Item("pendiente") < Total Then
                            CType(gr_detalle.DataSource, DataTable).Rows(i).Item("Pagar") = True
                            Total = Total - CType(gr_detalle.DataSource, DataTable).Rows(i).Item("pendiente")
                            CType(gr_detalle.DataSource, DataTable).Rows(i).Item("PagoAc") = CType(gr_detalle.DataSource, DataTable).Rows(i).Item("pendiente")
                            CType(gr_detalle.DataSource, DataTable).Rows(i).Item("pendiente") = 0
                            tbTotalCobrado.Value = tbTotalCobrado.Value + CType(gr_detalle.DataSource, DataTable).Rows(i).Item("PagoAc")
                            tbSaldo.Value = 0
                        Else
                            CType(gr_detalle.DataSource, DataTable).Rows(i).Item("Pagar") = True
                            CType(gr_detalle.DataSource, DataTable).Rows(i).Item("PagoAc") = Total
                            CType(gr_detalle.DataSource, DataTable).Rows(i).Item("pendiente") = CType(gr_detalle.DataSource, DataTable).Rows(i).Item("pendiente") - Total
                            tbTotalCobrado.Value = tbTotalCobrado.Value + CType(gr_detalle.DataSource, DataTable).Rows(i).Item("PagoAc")
                            Total = 0
                            Saldo = Saldo = CType(gr_detalle.DataSource, DataTable).Rows(i).Item("pendiente")
                        End If
                    End If
                    If Total = 0 Then
                        Saldo = Saldo + CType(gr_detalle.DataSource, DataTable).Rows(i).Item("pendiente")
                    End If
                    End If
            Next
        End If
        tbSaldo.Value = Saldo
        'Dim b As Boolean = False
        'Dim b2 As Boolean = _fnIsALl()
        'If (tbTotalCobrado.Value >= 0) Then
        '    b = True
        'Else
        '    b = False
        'End If
        'If (dt.Rows.Count > 0) Then
        '    For i As Integer = 0 To dt.Rows.Count - 1 Step 1
        '        If (b = True) Then
        '            If (CType(gr_detalle.DataSource, DataTable).Rows(i).Item("Pagar") = False) Then
        '                CType(gr_detalle.DataSource, DataTable).Rows(i).Item("Pagar") = True

        '                CType(gr_detalle.DataSource, DataTable).Rows(i).Item("PagoAc") = CType(gr_detalle.DataSource, DataTable).Rows(i).Item("pendiente")
        '                CType(gr_detalle.DataSource, DataTable).Rows(i).Item("pendiente") = 0
        '                tbTotalCobrado.Value = tbTotalCobrado.Value + CType(gr_detalle.DataSource, DataTable).Rows(i).Item("PagoAc")
        '                tbSaldo.Value = 0
        '            End If



        '        End If
        '        If (b2) Then
        '            CType(gr_detalle.DataSource, DataTable).Rows(i).Item("Pagar") = False
        '            tbTotalCobrado.Value = 0
        '            tbSaldo.Value = tbTotalCobrar.Value

        '            CType(gr_detalle.DataSource, DataTable).Rows(i).Item("pendiente") = CType(gr_detalle.DataSource, DataTable).Rows(i).Item("PagoAc")
        '            CType(gr_detalle.DataSource, DataTable).Rows(i).Item("PagoAc") = 0
        '        End If
        '    Next

        'End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _inter = _inter + 1
        If _inter = 1 Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub
    Private Sub _prModificarMontos(ByRef tabla As DataTable)
        tabla.Rows.Add(0, TotalBs, TotalSus, TotalTarjeta, TipoCambio, 2)
    End Sub
    Private Sub _prGuardarCobro(dtCobro As DataTable)
        Dim Notas As String = ""
        For i As Integer = 0 To dtCobro.Rows.Count - 1 Step 1
            Dim x As Integer = InStr(1, dtCobro.Rows(i).Item("tdnrodoc"), "-")
            Notas = Notas + dtCobro.Rows(i).Item("tdnrodoc").ToString.Substring(0, x)
        Next
        _prAgregarCobro(0, 2, "VENTA CREDITO Nº " + Notas, TotalBs, TotalSus, TotalTarjeta, cambio, Banco, Glosa, gi_userSuc)
        If TotalTarjeta > 0 Then
            L_prMovimientoGrabar("", tbFechaVenta.Value.ToString("dd/MM/yyyy"), 1, gi_userSuc, Banco, "", "CUENTA POR COBRAR", TotalTarjeta, Glosa)
        End If

    End Sub
    Private Sub btnCobrar_Click(sender As Object, e As EventArgs) Handles btnCobrar.Click
        Dim ef As F1_MontoPagar
        ef = New F1_MontoPagar

        'ef.TotalVenta = Math.Round(tbtotal.Value, 2)
        'ef.tipoVenta = IIf(swTipoVenta.Value = True, 1, 0)
        'ef.Cobrado = False
        'ef.CostoEnvio = tbEnvio.Text
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.Bandera
        If (bandera = True) Then

            TotalBs = ef.TotalBs
            TotalSus = ef.TotalSus
            TotalTarjeta = ef.TotalTarjeta
            TipoCambio = ef.TipoCambio
            TipoVenta = ef.tipoVenta
            FechaVenc = ef.tbFechaVenc.Value
            Banco = ef.cbBanco.Value
            Glosa = ef.tbGlosa.Text
            CostoEnvio = ef.tbCostoEnvio.Value
            cambio = Convert.ToDouble(ef.txtCambio1.Text)

            tbMonto.Text = CStr(CDbl(TotalBs + TotalTarjeta + (TotalSus * TipoCambio)))
            Saldo = CDbl(TotalBs + TotalTarjeta + (TotalSus * TipoCambio))
            Total = CDbl(TotalBs + TotalTarjeta + (TotalSus * TipoCambio))

        Else
            ToastNotification.Show(Me, "No se realizó ninguna operación ".ToUpper, My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)

        End If

    End Sub



#End Region

End Class