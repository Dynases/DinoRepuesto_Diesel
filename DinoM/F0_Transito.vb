﻿Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Data.OleDb
Imports System.Data
Public Class F0_Transito
    Dim _Inter As Integer = 0

#Region "Variables Globales"
    Dim _CodProveedor As Integer = 0
    Dim _numiCatCosto As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Public Table_Producto As DataTable
    Dim _PorcentajeUtil As Double = 0 '' En esta varible obtendre de la libreria el porcentaje de utilidad para la venta 
    Dim _estadoPor As Integer ''En esta variable me dira si sera visible o no las columnas de porcentaje de utilidad y precio de venta
    Dim Lote As Boolean = False
    Public _detalleCompras As DataTable 'Almacena el detalle para insertar a la tabla TPA001 del BDDiconDinoEco
    Dim dtProductoGoblal As DataTable = Nothing
    Public ProductosImport As New DataTable
    Public BanderaImport As Boolean = False
    Public cont As Integer = 0
#End Region

#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prValidarLote()
        _prCargarComboLibreriaSucursal(cbSucursal)
        _prObtenerPorcentajeUtilidad()
        'Me.WindowState = FormWindowState.Maximized
        _prCargarComboMoneda(cbMoneda)
        _prCargarComboConcepto(cbConcepto)

        _prCargarCompra()
        If (grCompra.RowCount > 0) Then
            _prCargarGastos(CInt(tbCodigo.Text))
        End If
        _prInhabiliitar()

        grCompra.Focus()
        _prAsignarPermisos()
        Me.Text = "COMPRAS"
        PanelDetalle.Height = 250
        MSuperTabControl.SelectedTabIndex = 0
        btnImportar.Visible = False
    End Sub

    Private Sub _prCargarGastos(cod As Integer)
        Dim dt As New DataTable
        dt = L_fnGeneralGastos(cod)
        grGastos.DataSource = dt
        grGastos.RetrieveStructure()
        grGastos.AlternatingColors = True


        With grGastos.RootTable.Columns("codigo")
            .Width = 60
            .Caption = "Nº"
            .Visible = True
        End With
        With grGastos.RootTable.Columns("concepto")
            .Width = 150
            .Visible = True
            .Caption = "CONCEPTO"
        End With
        With grGastos.RootTable.Columns("tipo")
            .Width = 150
            .Visible = False
        End With
        With grGastos.RootTable.Columns("monto")
            .Width = 90
            .Visible = True
            .Caption = "MONTO"
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grGastos.RootTable.Columns("observacion")
            .Width = 90
            .Visible = True
            .Caption = "OBSERVACION"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grGastos.RootTable.Columns("img")
            .Width = 90
            .Caption = "ELIMINAR"
            .TextAlignment = TextAlignment.Center
            .Visible = False
        End With
        With grGastos
            .ColumnAutoResize = True
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub
    Public Sub _prValidarLote()
        Dim dt As DataTable = L_fnPorcUtilidad()
        If (dt.Rows.Count > 0) Then
            Dim lot As Integer = dt.Rows(0).Item("VerLote")
            If (lot = 1) Then
                Lote = True
            Else
                Lote = False
            End If

        End If
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
    Public Sub _prObtenerPorcentajeUtilidad()
        ''''En este procedimiento obtendre el porcentaje de utilidad que esta en la tabla de configuraciones
        Dim dt As DataTable = L_fnPorcUtilidad()
        If (dt.Rows.Count > 0) Then
            _PorcentajeUtil = dt.Rows(0).Item("PorcUtil")
            _estadoPor = dt.Rows(0).Item("VerPorc")
        End If

    End Sub
    Private Sub _prCargarComboLibreriaSucursal(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarSucursales()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 60
            .DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("aabdes").Width = 500
            .DropDownList.Columns("aabdes").Caption = "SUCURSAL"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub _prCargarComboMoneda(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarMoneda()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 60
            .DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("aabdes").Width = 500
            .DropDownList.Columns("aabdes").Caption = "MONEDA"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With

        cbMoneda.SelectedIndex = 2
    End Sub

    Private Sub _prCargarComboConcepto(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnGeneralDetalleLibrerias(10, 1)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 60
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 500
            .DropDownList.Columns("ycdes3").Caption = "CONCEPTO"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prInhabiliitar()
        tbCodigo.ReadOnly = True
        tbProveedor.ReadOnly = True
        tbCodProv.ReadOnly = True
        tbObservacion.ReadOnly = True
        tbFechaVenta.IsInputReadOnly = True
        tbFechaVenta.Enabled = False
        tbFechaVenc.IsInputReadOnly = True
        tbFechaVenc.Enabled = False
        cbSucursal.ReadOnly = True
        SwProforma.IsReadOnly = True
        ButtonX1.Enabled = True
        cbMoneda.ReadOnly = True
        swTipoVenta.IsReadOnly = True

        tbNitProv.ReadOnly = True
        swEmision.IsReadOnly = True
        swConsigna.IsReadOnly = True
        swRetencion.IsReadOnly = True
        swMoneda.IsReadOnly = True
        tbTipoCambio.IsInputReadOnly = True

        tbNFactura.ReadOnly = True
        'tbNAutorizacion.ReadOnly = True
        'tbCodControl.ReadOnly = True
        'tbNDui.ReadOnly = True
        'tbSACF.ReadOnly = True

        ''''''''''
        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True

        tbSubtotalC.IsInputReadOnly = True
        tbMdesc.IsInputReadOnly = True
        tbtotal.IsInputReadOnly = True

        grCompra.Enabled = True
        PanelNavegacion.Enabled = True
        grdetalle.RootTable.Columns("img").Visible = False
        If (GPanelProductos.Visible = True) Then
            _DesHabilitarProductos()
        End If
        btnAgregar.Visible = False
        btnImportar.Visible = False
    End Sub
    Private Sub _prhabilitar()
        grCompra.Enabled = False
        tbCodigo.ReadOnly = False
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False
        tbObservacion.ReadOnly = False
        tbFechaVenta.IsInputReadOnly = False
        tbFechaVenc.IsInputReadOnly = False
        tbFechaVenc.Enabled = True
        SwProforma.IsReadOnly = False
        ButtonX1.Enabled = False
        'If (tbCodigo.Text.Length > 0) Then
        '    cbSucursal.ReadOnly = True
        'Else
        '    cbSucursal.ReadOnly = False
        'End If
        cbMoneda.ReadOnly = False
        swTipoVenta.IsReadOnly = False
        btnGrabar.Enabled = True


        tbNitProv.ReadOnly = False
        'swEmision.IsReadOnly = False
        swConsigna.IsReadOnly = False
        swRetencion.IsReadOnly = False
        tbNFactura.ReadOnly = False
        'tbNAutorizacion.ReadOnly = False
        'tbCodControl.ReadOnly = False
        'tbNDui.ReadOnly = False
        'tbSACF.ReadOnly = False

        tbMdesc.IsInputReadOnly = False

        swMoneda.IsReadOnly = False
        tbTipoCambio.IsInputReadOnly = False



        btnAgregar.Visible = True

    End Sub
    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarCompra()
        If grCompra.RowCount > 0 Then
            _Mpos = 0
            grCompra.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _Limpiar()
        tbCodigo.Clear()
        tbProveedor.Clear()
        tbNitProv.Clear()
        tbObservacion.Clear()
        If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = 0
        Else
            cbSucursal.SelectedIndex = -1
        End If
        swTipoVenta.Value = False
        _CodProveedor = 0
        tbFechaVenta.Value = Now.Date
        tbFechaVenc.Value = Now.Date
        tbFechaVenc.Visible = True
        lbCredito.Visible = True
        tbCodProv.Clear()
        swEmision.Value = False
        swConsigna.Value = False
        swRetencion.Value = False
        swMoneda.Value = True
        tbTipoCambio.Value = 0
        cbMoneda.Value = 2
        tbNFactura.Clear()
        'tbNAutorizacion.Clear()
        'tbCodControl.Clear()
        'tbNDui.Clear()
        'tbSACF.Clear()

        _prCargarDetalleVenta(-1)
        _prCargarGastos(-1)
        CType(grdetalle.DataSource, DataTable).Rows.Clear()
        MSuperTabControl.SelectedTabIndex = 0

        tbPdesc.Value = 0
        tbMdesc.Value = 0
        tbtotal.Value = 0
        tbSubtotalC.Value = 0
        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With
        With grGastos.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With
        _prAddDetalleVenta()
        If (GPanelProductos.Visible = True) Then
            GPanelProductos.Visible = False
            PanelTotal.Visible = True
            PanelInferior.Visible = True
        End If
        tbProveedor.Focus()
        Table_Producto = Nothing



        'Validar si es recibo o factura
        If swEmision.Value = False Then
            lbNFactura.Text = "Nro. Documento:"
            'GroupPanelFactura2.Text = "DOCUMENTO"
            'lbNAutoriz.Visible = False
            'tbNAutorizacion.Visible = False
            'lbCodCtrl.Visible = False
            'tbCodControl.Visible = False
            'lbNDui.Visible = False
            'tbNDui.Visible = False
            'lbSACF.Visible = False
            'tbSACF.Visible = False
        Else
            lbNFactura.Text = "Nro. Factura:"
            'GroupPanelFactura2.Text = "DATOS FACTURACIÓN"
            'lbNAutoriz.Visible = True
            'tbNAutorizacion.Visible = True
            'lbCodCtrl.Visible = True
            'tbCodControl.Visible = True
            'lbNDui.Visible = True
            'tbNDui.Visible = True
            'lbSACF.Visible = True
            'tbSACF.Visible = True
        End If

        If swMoneda.Value = True Then
            lbTipoCambio.Visible = False
            tbTipoCambio.Visible = False
            tbTipoCambio.Value = 1
        Else
            lbTipoCambio.Visible = True
            tbTipoCambio.Visible = True
        End If

        If cbMoneda.Value = 1 Then
            lbTipoCambio.Visible = False
            tbTipoCambio.Visible = False
            tbTipoCambio.Value = 1
        ElseIf cbMoneda.Value = 0 Then
            lbTipoCambio.Visible = True
            tbTipoCambio.Visible = True
        End If


        If (gi_userSuc > 0) Then
            Dim dt As DataTable = CType(cbSucursal.DataSource, DataTable)
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1

                If (dt.Rows(i).Item("aanumi") = gi_userSuc) Then
                    cbSucursal.SelectedIndex = i
                End If
            Next
        End If
        SwProforma.Value = False
        btnBuscarProforma.Enabled = False
        tbProforma.Clear()
        'If (SwProforma.Value = True) Then
        '    btnBuscarProforma.Enabled = True
        'Else
        '    btnBuscarProforma.Enabled = False
        'End If
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        '' grVentas.Row = _N
        'a.canumi ,a.caalm ,a.cafdoc ,a.caty4prov ,proveedor .yddesc as proveedor ,a.catven ,a.cafvcr ,
        'a.camon ,IIF(camon=1,'Boliviano','Dolar') as moneda,a.caest ,a.caobs ,
        'a.cadesc ,a.cafact ,a.cahact ,a.cauact,(Sum(b.cbptot)-a.cadesc ) as total
        With grCompra

            tbCodigo.Text = .GetValue("tanumi")
            tbFechaVenta.Value = .GetValue("tafdoc")
            _CodProveedor = .GetValue("taty4prov")
            swTipoVenta.Value = .GetValue("tatven")
            tbProveedor.Text = .GetValue("proveedor")
            cbSucursal.Value = .GetValue("taalm")
            tbObservacion.Text = .GetValue("taobs")
            tbCodProv.Text = .GetValue("taty4prov").ToString + "-" + .GetValue("ydcod").ToString
            swEmision.Value = .GetValue("taemision")
            tbNFactura.Text = .GetValue("tanumemis")
            tbNitProv.Text = .GetValue("yddctnum")
            swConsigna.Value = .GetValue("taconsigna")
            swRetencion.Value = .GetValue("taretenc")
            cbMoneda.Value = .GetValue("tamon")
            tbTipoCambio.Value = .GetValue("tatipocambio")
            tbProforma.Text = .GetValue("taProforma").ToString
            BanderaImport = .GetValue("EsImportacion")
            If .GetValue("taProforma") > 0 Then
                SwProforma.Value = 1
            Else
                SwProforma.Value = 0
                'btnTraspaso.Enabled = False
            End If

            If .GetValue("taMovimiento") = 0 And SwProforma.Value = True Then
                btnTraspaso.Enabled = True
                btnModificar.Enabled = False
                btnEliminar.Enabled = True

            ElseIf .GetValue("taMovimiento") > 0 And SwProforma.Value = True Then
                btnTraspaso.Enabled = False
                btnModificar.Enabled = False
                btnEliminar.Enabled = False

            ElseIf .GetValue("taMovimiento") = 0 And SwProforma.Value = False Then
                'btnTraspaso.Enabled = False
                'btnModificar.Enabled = True
                'btnEliminar.Enabled = True
            End If

            'If (swTipoVenta.Value = False) Then

            tbFechaVenc.Value = .GetValue("tafvcr")
            'Else
            '    lbCredito.Visible = False
            '    tbFechaVenc.Visible = False
            'End If

            lbFecha.Text = CType(.GetValue("tafact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("tahact").ToString
            lbUsuario.Text = .GetValue("tauact").ToString


        End With
        _prCargarGastos(CInt(tbCodigo.Text))
        _prCargarDetalleVenta2(tbCodigo.Text)
        tbMdesc.Value = grCompra.GetValue("tadesc")
        If swRetencion.Value = False Then
            _prCalcularPrecioTotal()
        Else
            tbtotal.Value = grCompra.GetValue("total")
            tbSubtotalC.Value = tbtotal.Value + tbMdesc.Value
        End If

        LblPaginacion.Text = Str(grCompra.Row + 1) + "/" + grCompra.RowCount.ToString

        If swEmision.Value = True Then
            _prCargarFacturacion(tbCodigo.Text)

            lbNFactura.Text = "Nro. Factura:"
            'GroupPanelFactura2.Text = "DATOS FACTURACIÓN"
            'lbNAutoriz.Visible = True
            'tbNAutorizacion.Visible = True
            'lbCodCtrl.Visible = True
            'tbCodControl.Visible = True
            'lbNDui.Visible = True
            'tbNDui.Visible = True
            'lbSACF.Visible = True
            'tbSACF.Visible = True
        Else
            lbNFactura.Text = "Nro. Documento:"
            'GroupPanelFactura2.Text = "DOCUMENTO"
            'lbNAutoriz.Visible = False
            'tbNAutorizacion.Visible = False
            'lbCodCtrl.Visible = False
            'tbCodControl.Visible = False
            'lbNDui.Visible = False
            'tbNDui.Visible = False
            'lbSACF.Visible = False
            'tbSACF.Visible = False
        End If

        'If swMoneda.Value = True Then
        '    lbTipoCambio.Visible = False
        '    tbTipoCambio.Visible = False
        'Else
        '    lbTipoCambio.Visible = True
        '    tbTipoCambio.Visible = True
        'End If

        If cbMoneda.Value = 1 Then
            lbTipoCambio.Visible = False
            tbTipoCambio.Visible = False
        ElseIf cbMoneda.Value = 0 Then
            lbTipoCambio.Visible = True
            tbTipoCambio.Visible = True
            ConvertirSus()

        End If

    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable

        dt = L_fnDetalleTransito2(_numi)


        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        '      a.cbnumi ,a.cbtv1numi ,a.cbty5prod ,b.yfcdprod1 as producto,a.cbest ,a.cbcmin 
        ',a.cbumin ,Umin .ycdes3 as unidad,a.cbpcost,a.cblote ,a.cbfechavenc ,a.cbptot 
        ',a.cbutven ,a.cbprven   ,a.cbobs ,
        'a.cbfact ,a.cbhact ,a.cbuact,1 as estado,Cast(null as Image) as img,a.cbpcost as costo,a.cbprven as venta
        If (Lote = True) Then
            With grdetalle.RootTable.Columns("cblote")
                .Width = 150
                .Caption = "LOTE"
                .Visible = True
                .MaxLength = 50
            End With
            With grdetalle.RootTable.Columns("cbfechavenc")
                .Width = 120
                .Caption = "FECHA VENC."
                .Visible = True
                .FormatString = "dd/MM/yyyy"
            End With
        Else
            With grdetalle.RootTable.Columns("cblote")
                .Width = 150
                .Caption = "LOTE"
                .Visible = False
                .MaxLength = 50
            End With
            With grdetalle.RootTable.Columns("cbfechavenc")
                .Width = 120
                .Caption = "FECHA VENC."
                .Visible = False
                .FormatString = "dd/MM/yyyy"
            End With
        End If
        With grdetalle.RootTable.Columns("yfCodAux1")
            .Width = 100
            .Caption = "Item Nuevo"
            .Visible = True
            .TextAlignment = 2
        End With
        With grdetalle.RootTable.Columns("yfCodAux2")
            .Width = 100
            .Caption = "Item Antiguo"
            .Visible = True
            .TextAlignment = 2
        End With
        With grdetalle.RootTable.Columns("cbnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("cbtv1numi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbty5prod")
            .Width = 90
            .Caption = "Item"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("CodigoFabrica")
            .Caption = "Cod.Fabrica"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = True
            .TextAlignment = 2

        End With
        With grdetalle.RootTable.Columns("CodigoMarca")
            .Caption = "Cod.Marca"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = True
            .TextAlignment = 2
        End With

        With grdetalle.RootTable.Columns("Medida")
            .Caption = "Medida"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = False

        End With

        With grdetalle.RootTable.Columns("Marca")
            .Caption = "Marca"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("Procedencia")
            .Caption = "Procedencia"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("producto")
            .Caption = "Descripcion"
            .Width = 400
            .WordWrap = True
            .MaxLines = 3
            .Visible = True

        End With
        With grdetalle.RootTable.Columns("cbest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("cbcmin")
            .Width = 110
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0"
            .Caption = "Cantidad"
        End With
        With grdetalle.RootTable.Columns("cbumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("unidad")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "Unidad".ToUpper
        End With
        With grdetalle.RootTable.Columns("gasto")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "Costo".ToUpper
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("cbpcost")
            .Width = 140
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0.000"
            .Caption = "P.CostoUn."
        End With
        If (_estadoPor = 1) Then
            With grdetalle.RootTable.Columns("cbutven")
                .Width = 110
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Utilidad (%)".ToUpper
            End With
            With grdetalle.RootTable.Columns("cbprven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Precio Venta".ToUpper
            End With
        Else
            With grdetalle.RootTable.Columns("cbutven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Utilidad.".ToUpper
            End With
            With grdetalle.RootTable.Columns("cbprven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Precio Venta.".ToUpper
            End With
        End If
        With grdetalle.RootTable.Columns("PP")
            .Width = 120
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0.00"
            .Caption = "PP"

        End With
        With grdetalle.RootTable.Columns("PPA")
            .Width = 120
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0.00"
            .Caption = "PPA"
        End With
        With grdetalle.RootTable.Columns("cbptot")
            .Width = 120
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Sub Total"
        End With
        With grdetalle.RootTable.Columns("cbobs")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .FormatString = "yyyy/MM/dd"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbuact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("costo")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("gasto")
            .Width = 50
            .Caption = "Costo"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("yftcam")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("venta")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbpFacturado")
            .Width = 140
            .TextAlignment = 2
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.Publico"
        End With
        With grdetalle.RootTable.Columns("cbpPublico")
            .Width = 140
            .TextAlignment = 2
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.Taller"
        End With
        With grdetalle.RootTable.Columns("cbpMecanico")
            .Width = 140
            .TextAlignment = 2
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.GDB"
        End With
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub

    Private Sub _prCargarDetalleVenta2(_numi As String)
        Dim dt As New DataTable

        dt = L_fnDetalleTransito2(_numi)


        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        '      a.cbnumi ,a.cbtv1numi ,a.cbty5prod ,b.yfcdprod1 as producto,a.cbest ,a.cbcmin 
        ',a.cbumin ,Umin .ycdes3 as unidad,a.cbpcost,a.cblote ,a.cbfechavenc ,a.cbptot 
        ',a.cbutven ,a.cbprven   ,a.cbobs ,
        'a.cbfact ,a.cbhact ,a.cbuact,1 as estado,Cast(null as Image) as img,a.cbpcost as costo,a.cbprven as venta
        If (Lote = True) Then
            With grdetalle.RootTable.Columns("cblote")
                .Width = 150
                .Caption = "LOTE"
                .Visible = True
                .MaxLength = 50
            End With
            With grdetalle.RootTable.Columns("cbfechavenc")
                .Width = 120
                .Caption = "FECHA VENC."
                .Visible = True
                .FormatString = "dd/MM/yyyy"
            End With
        Else
            With grdetalle.RootTable.Columns("cblote")
                .Width = 150
                .Caption = "LOTE"
                .Visible = False
                .MaxLength = 50
            End With
            With grdetalle.RootTable.Columns("cbfechavenc")
                .Width = 120
                .Caption = "FECHA VENC."
                .Visible = False
                .FormatString = "dd/MM/yyyy"
            End With
        End If
        With grdetalle.RootTable.Columns("yfCodAux1")
            .Width = 100
            .Caption = "Item Nuevo"
            .Visible = True
            .TextAlignment = 2
        End With
        With grdetalle.RootTable.Columns("yfCodAux2")
            .Width = 100
            .Caption = "Item Antiguo"
            .Visible = True
            .TextAlignment = 2
        End With
        With grdetalle.RootTable.Columns("cbnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("cbtv1numi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbty5prod")
            .Width = 90
            .Caption = "Item"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("CodigoFabrica")
            .Caption = "Cod.Fabrica"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = True
            .TextAlignment = 2

        End With
        With grdetalle.RootTable.Columns("CodigoMarca")
            .Caption = "Cod.Marca"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = True
            .TextAlignment = 2
        End With

        With grdetalle.RootTable.Columns("Medida")
            .Caption = "Medida"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = False

        End With

        With grdetalle.RootTable.Columns("Marca")
            .Caption = "Marca"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("Procedencia")
            .Caption = "Procedencia"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("producto")
            .Caption = "Descripcion"
            .Width = 400
            .WordWrap = True
            .MaxLines = 3
            .Visible = True

        End With
        With grdetalle.RootTable.Columns("cbest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("cbcmin")
            .Width = 110
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0"
            .Caption = "Cantidad"
        End With
        With grdetalle.RootTable.Columns("cbumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("unidad")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "Unidad".ToUpper
        End With
        With grdetalle.RootTable.Columns("gasto")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "Costo".ToUpper
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("cbpcost")
            .Width = 140
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0.000"
            .Caption = "P.CostoUn."
        End With
        If (_estadoPor = 1) Then
            With grdetalle.RootTable.Columns("cbutven")
                .Width = 110
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Utilidad (%)".ToUpper
            End With
            With grdetalle.RootTable.Columns("cbprven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Precio Venta".ToUpper
            End With
        Else
            With grdetalle.RootTable.Columns("cbutven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Utilidad.".ToUpper
            End With
            With grdetalle.RootTable.Columns("cbprven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Precio Venta.".ToUpper
            End With
        End If
        With grdetalle.RootTable.Columns("PP")
            .Width = 120
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0.00"
            .Caption = "PP"

        End With
        With grdetalle.RootTable.Columns("PPA")
            .Width = 120
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0.00"
            .Caption = "PPA"
        End With
        With grdetalle.RootTable.Columns("cbptot")
            .Width = 120
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Sub Total"
        End With
        With grdetalle.RootTable.Columns("cbobs")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .FormatString = "yyyy/MM/dd"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbuact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("costo")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("gasto")
            .Width = 50
            .Caption = "Costo"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("yftcam")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("venta")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbpFacturado")
            .Width = 140
            .TextAlignment = 2
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.Publico"
        End With
        With grdetalle.RootTable.Columns("cbpPublico")
            .Width = 140
            .TextAlignment = 2
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.Taller"
        End With
        With grdetalle.RootTable.Columns("cbpMecanico")
            .Width = 140
            .TextAlignment = 2
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.GDB"
        End With
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub
    Private Sub _prCargarFacturacion(_numi As String)
        Dim dtC As New DataTable
        dtC = L_prCompraComprobanteGeneralPorNumi(_numi)

        'If dtC.Rows.Count = 0 Then
        '    tbNAutorizacion.Text = ""
        '    tbCodControl.Text = ""
        '    tbNDui.Text = ""
        '    tbSACF.Text = ""
        'Else
        '    tbNAutorizacion.Text = dtC.Rows(0).Item("fcaautoriz")
        '    tbCodControl.Text = dtC.Rows(0).Item("fcaccont")
        '    tbNDui.Text = dtC.Rows(0).Item("fcandui")
        '    tbSACF.Text = tbtotal.Text - dtC.Rows(0).Item("fcanscf")
        'End If

    End Sub

    Private Sub _prCargarCompra()
        Dim dt As New DataTable
        dt = L_fnGeneralTransito(0)
        grCompra.DataSource = dt
        grCompra.RetrieveStructure()
        grCompra.AlternatingColors = True


        With grCompra.RootTable.Columns("tanumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True
        End With
        With grCompra.RootTable.Columns("taalm")
            .Width = 90
            .Visible = False
        End With
        With grCompra.RootTable.Columns("tafdoc")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
        End With

        With grCompra.RootTable.Columns("taty4prov")
            .Width = 160
            .Visible = False
        End With
        With grCompra.RootTable.Columns("ydcod")
            .Width = 160
            .Visible = False
        End With
        With grCompra.RootTable.Columns("proveedor")
            .Width = 250
            .Visible = True
            .Caption = "proveedor".ToUpper
        End With
        With grCompra.RootTable.Columns("yddctnum")
            .Width = 100
            .Visible = False
            .Caption = "Ci/Nit".ToUpper
        End With

        With grCompra.RootTable.Columns("tatven")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grCompra.RootTable.Columns("tafvcr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        '     a.canumi ,a.caalm ,a.cafdoc ,a.caty4prov ,proveedor .yddesc as proveedor ,a.catven ,a.cafvcr ,
        'a.camon ,IIF(camon=1,'Boliviano','Dolar') as moneda,a.caest ,a.caobs ,
        'a.cadesc ,a.cafact ,a.cahact ,a.cauact,(Sum(b.cbptot)-a.cadesc ) as total



        With grCompra.RootTable.Columns("tamon")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grCompra.RootTable.Columns("moneda")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "MONEDA"
        End With
        With grCompra.RootTable.Columns("taobs")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "OBSERVACION"
        End With
        With grCompra.RootTable.Columns("tadesc")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grCompra.RootTable.Columns("taest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grCompra.RootTable.Columns("tafact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grCompra.RootTable.Columns("tahact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grCompra.RootTable.Columns("tauact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grCompra.RootTable.Columns("total")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grCompra.RootTable.Columns("taemision")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "Emision"

        End With
        With grCompra.RootTable.Columns("tanumemis")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "NRO. DOCUMENTO"
        End With
        With grCompra.RootTable.Columns("taconsigna")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "Consigna"
        End With
        With grCompra.RootTable.Columns("taretenc")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "Retención"
        End With
        With grCompra.RootTable.Columns("tatipocambio")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grCompra.RootTable.Columns("taProforma")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grCompra.RootTable.Columns("taMovimiento")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grCompra
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        If (dt.Rows.Count <= 0) Then
            _prCargarDetalleVenta(-1)
        End If
    End Sub
    Private Sub _prCargarProductos(_cliente As String)

        Dim dtname As DataTable = L_fnNameLabel()

        If (cbSucursal.SelectedIndex < 0) Then
            Return
        End If


        If (IsNothing(dtProductoGoblal)) Then
            dtProductoGoblal = L_fnListarProductosCompra(cbSucursal.Value, 73)
        End If


        Dim frm As F0_DetalleCompras
        frm = New F0_DetalleCompras(dtProductoGoblal, CType(grdetalle.DataSource, DataTable), dtname)


        frm.ShowDialog()
        dtProductoGoblal = frm.dtProductoAll
        grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))

        'Dim dtProd As DataTable = frm.dtDetalle
        'For i As Integer = 0 To dtProd.Rows.Count - 1 Step 1
        '    InsertarProductosSinLote(dtProd, i)
        'Next
        'dtMovimiento.Rows.Clear()

        '''1=Almacen  73=Cat Precio Costo
        'grProductos.DataSource = dt
        'grProductos.RetrieveStructure()
        'grProductos.AlternatingColors = True

        ' yfnumi	Categoria	CodigoFabrica	Marca	Medida	yfcdprod1	grupo1	grupo2	yhprecio	venta	stock

        'With grProductos.RootTable.Columns("yfnumi")
        '    .Width = 100
        '    .Caption = "Item"
        '    .Visible = False

        'End With
        'With grProductos.RootTable.Columns("Categoria")
        '    .Width = 150
        '    .Caption = "Categoria"
        '    .Visible = True

        'End With
        'With grProductos.RootTable.Columns("CodigoFabrica")
        '    .Width = 150
        '    .Visible = True
        '    .Caption = "Codigo Fabrica"
        'End With
        'With grProductos.RootTable.Columns("Marca")
        '    .Width = 150
        '    .Visible = True
        '    .Caption = "Marca"
        'End With
        'With grProductos.RootTable.Columns("Medida")
        '    .Width = 150
        '    .Visible = True
        '    .Caption = "Medida"
        'End With

        '' yfnumi	Categoria	CodigoFabrica	Marca	Medida	yfcdprod1	grupo1	grupo2	yhprecio	venta	stock

        'With grProductos.RootTable.Columns("yfcdprod1")
        '    .Width = 200
        '    .Visible = True
        '    .Caption = "Producto"
        'End With

        'With grProductos.RootTable.Columns("grupo1")
        '    .Width = 150
        '    .Visible = True
        '    .Caption = "Marca"
        'End With
        'With grProductos.RootTable.Columns("grupo2")
        '    .Width = 150
        '    .Visible = True
        '    .Caption = "Procedencia"
        'End With

        'With grProductos.RootTable.Columns("yhprecio")
        '    .Width = 120
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '    .Visible = True
        '    .Caption = "PRECIO COSTO"
        '    .FormatString = "0.00"
        'End With

        'With grProductos.RootTable.Columns("venta")
        '    .Width = 120
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '    .Visible = True
        '    .Caption = "PRECIO VENTA"
        '    .FormatString = "0.00"
        'End With
        'With grProductos.RootTable.Columns("stock")
        '    .Width = 100
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '    .Visible = True
        '    .Caption = "STOCK"
        '    .FormatString = "0.00"
        'End With

        'With grProductos
        '    .DefaultFilterRowComparison = FilterConditionOperator.Contains
        '    .FilterMode = FilterMode.Automatic
        '    .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
        '    .GroupByBoxVisible = False
        '    'diseño de la grilla
        '    .VisualStyle = VisualStyle.Office2007
        'End With
    End Sub
    Private Sub _prAddDetalleVenta()
        '     a.cbnumi ,a.cbtv1numi ,a.cbty5prod ,b.yfcdprod1 as producto,a.cbest ,a.cbcmin 
        ',a.cbumin ,Umin .ycdes3 as unidad,a.cbpcost,a.cblote ,a.cbfechavenc ,a.cbptot 
        ',a.cbutven ,a.cbprven   ,a.cbobs ,
        'a.cbfact ,a.cbhact ,a.cbuact,1 as estado,Cast(null as Image) as img,a.cbpcost as costo,a.cbprven as venta
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add("", "", "", "", _fnSiguienteNumi() + 1, 0, 0, "", "", "", "", 0, 0, 0, "",
                                                        0, 0, "20500101", CDate("2050/01/01"), 0, 0, 0, 0, 0, "", Now.Date, "", "", 0, 0, 0, 0, Bin.GetBuffer, 0, 0)
    End Sub

    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("cbnumi=MAX(cbnumi)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("cbnumi")
        End If
        Return 1
    End Function
    Public Function _fnAccesible()
        Return tbFechaVenta.IsInputReadOnly = False
    End Function
    Private Sub _HabilitarProductos()
        ''GPanelProductos.Height = 300
        'GPanelProductos.Visible = True
        'PanelTotal.Visible = False
        'PanelInferior.Visible = False
        _prCargarProductos(73) ''''Aqui poner el Primer Precio de Costo
        'grProductos.Focus()
        'grProductos.MoveTo(grProductos.FilterRow)
        'grProductos.Col = 2
        'PanelDetalle.Height = 370
        'GPanelProductos.Height = 260
        'grProductos.Height = 260

    End Sub
    Private Sub _DesHabilitarProductos()
        GPanelProductos.Visible = False
        PanelTotal.Visible = True
        PanelInferior.Visible = True


        grdetalle.Select()
        grdetalle.Col = 5
        grdetalle.Row = grdetalle.RowCount - 1

    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

    Public Function _fnExisteProducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbty5prod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function
    Public Sub P_PonerTotal(rowIndex As Integer)
        If (rowIndex < grdetalle.RowCount) Then

            Dim lin As Integer = grdetalle.GetValue("cbnumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, lin)
            Dim cant As Double = grdetalle.GetValue("cbcmin")
            Dim uni As Double = grdetalle.GetValue("cbpcost")
            'Dim pFacturado As Double

            If (pos >= 0) Then
                Dim TotalUnitario As Double = cant * uni
                'grDetalle.SetValue("lcmdes", montodesc)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = TotalUnitario
                grdetalle.SetValue("cbptot", TotalUnitario)
                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = (uni + (uni * (grdetalle.GetValue("cbutven") / 100))) * tbTipoCambio.Value
                grdetalle.SetValue("cbprven", (uni + (uni * (grdetalle.GetValue("cbutven") / 100))) * tbTipoCambio.Value)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta") = (uni + (uni * (grdetalle.GetValue("cbutven") / 100))) * tbTipoCambio.Value
                grdetalle.SetValue("cbprven", (uni + (uni * (grdetalle.GetValue("cbutven") / 100))) * tbTipoCambio.Value)
                If btnGrabar.Enabled = True Then
                    'Dim dt As DataTable
                    'dt = _lftraerPrecioCostoStock(CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbty5prod").ToString, gi_userSuc)
                    'For j = 0 To dt.Rows.Count - 1 Step 1
                    '    Dim act As Double = dt.Rows(j).Item("iccven") * dt.Rows(j).Item("yfPrecioCosto")
                    '    Dim can As Double = dt.Rows(j).Item("iccven") + cant
                    '    Dim sum As Double = TotalUnitario + act
                    '    Dim total As Double = sum / can
                    '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("PP") = total.ToString("0.00")
                    CalcularPonderado(cant, uni)
                    ' Next

                End If
            End If

            '_prCalcularPrecioTotal()
        End If



    End Sub

    Public Sub _prCalcularPrecioTotal()
        Dim ret As Double

        Dim montodesc As Double = tbMdesc.Value
        Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum))
        tbPdesc.Value = pordesc
        tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum) - montodesc
        'Agregado para que Muestre el Subtotal de la compra
        tbSubtotalC.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum)


        If swRetencion.Value = True Then
            ret = tbSubtotalC.Value * 0.08
            tbSubtotalC.Text = tbSubtotalC.Value - ret
            tbtotal.Text = tbSubtotalC.Text
        End If
    End Sub

    Public Function RevisarTablaVacia(dt As DataTable) As Boolean
        Dim Res As Boolean
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            If dt.Rows(i).Item("estado").ToString <> -2 Then
                Res = True
                Exit For
            End If
            Res = False
        Next
        Return Res
    End Function
    Public Sub _prEliminarFila()
        If (grdetalle.Row >= 0) Then
            If (grdetalle.RowCount >= 1) Then
                Dim estado As Integer = grdetalle.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grdetalle.GetValue("cbnumi")
                _fnObtenerFilaDetalle(pos, lin)
                If (estado = 0) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -2

                End If
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -1
                End If
                grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))
                _prCalcularPrecioTotal()
                grdetalle.Select()
                grdetalle.Col = 5
                Dim Ver As Boolean = RevisarTablaVacia(CType(grdetalle.DataSource, DataTable))
                If Ver Then
                Else
                    _prAddDetalleVenta()
                End If
                grdetalle.Row = grdetalle.RowCount - 1
            End If
        End If


    End Sub
    Public Function _ValidarCampos() As Boolean
        If (_CodProveedor <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Proveedor con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbProveedor.Focus()
            Return False

        End If

        If (grdetalle.RowCount = 1) Then
            grdetalle.Row = grdetalle.RowCount - 1
            If (grdetalle.GetValue("cbty5prod") = 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If

        End If
        If (cbSucursal.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione una Sucursal".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbProveedor.Focus()
            Return False
        End If
        If (SwProforma.Value = True) Then
            If tbProforma.Text = String.Empty Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione una Proforma".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                btnBuscarProforma.Focus()
                Return False
            End If
        End If
        If swEmision.Value = True Then
            If (tbNFactura.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por favor debe llenar el ".ToUpper + lbNFactura.Text.ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbNFactura.Focus()
                Return False
            End If
            'If (tbNAutorizacion.Text = String.Empty) Then
            '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            '    ToastNotification.Show(Me, "Por Favor debe llenar el número de autorización".ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            '    tbNAutorizacion.Focus()
            '    Return False
            'End If
            'If (tbCodControl.Text = String.Empty) Then
            '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            '    ToastNotification.Show(Me, "Por Favor debe llenar el código de control".ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            '    tbCodControl.Focus()
            '    Return False
            'End If
        Else
            If (tbNFactura.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por favor debe llenar el ".ToUpper + lbNFactura.Text.ToUpper, img, 3000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbNFactura.Focus()
                Return False
            End If
        End If
        If (tbFechaVenc.Value < tbFechaVenta.Value) Then

            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "La Fecha de Venc. del Crédito no puede ser menor a la Fecha de la Compra".ToUpper, img, 2500, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbProveedor.Focus()
            Return False
        End If
        If cbMoneda.Value = 0 Then
            If tbTipoCambio.Value <= 0 Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "El tipo de cambio debe ser mayor a 0".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If

        End If

        ''Controla que no se metan un mismo producto con el mismo lote y fecha de vencimiento
        Dim dt1 As DataTable = CType(grdetalle.DataSource, DataTable)
        For i As Integer = 0 To grdetalle.RowCount - 1 Step 1
            Dim _idprod As String = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbty5prod")
            Dim _Lote As String = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cblote")
            Dim _Fecha As String = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbfechavenc")
            Dim _estado As String = 0

            Dim query = dt1.Select("cbty5prod='" + _idprod + "' And cblote='" + _Lote + "' And cbfechavenc='" + _Fecha + "' And estado>='" + _estado + "'")

            If query.Count >= 2 Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "No puede registrar mas de un producto con el mismo lote y fecha de vencimiento, favor modificar".ToUpper, img, 4000, eToastGlowColor.Red, eToastPosition.BottomCenter)

                Return False
            End If
        Next

        Return True
    End Function

    Public Sub _GuardarNuevo()
        Try
            'If swMoneda.Value = False Then
            '    ConvertirBs()
            'End If
            '' RecuperarDatosTFC001()  'Recupera datos para grabar en la BDDiconDino en la Tabla TFC001


            'For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            '    Dim dat As Date = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbfechavenc")
            '    dat = Format(dat, "yyyy/MM/dd")
            '    CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbfechavenc") = dat
            '    dat = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbfact")
            '    CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbfact") = dat
            'Next
            Dim res As Integer = L_fnGrabarTransito("", cbSucursal.Value, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodProveedor, IIf(swTipoVenta.Value = True, 1, 0),
                                                  IIf(swTipoVenta.Value = True, Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")),
                                                  cbMoneda.Value, tbObservacion.Text, tbMdesc.Value, tbtotal.Value,
                                                  CType(grdetalle.DataSource, DataTable), _detalleCompras, IIf(swEmision.Value = True, 1, 0), tbNFactura.Text,
                                                  IIf(swConsigna.Value = True, 1, 0), IIf(swRetencion.Value = True, 1, 0),
                                                  IIf(cbMoneda.Value = 1, 1, tbTipoCambio.Value), 0, IIf(SwProforma.Value = True, tbProforma.Text, 0), IIf(BanderaImport = False, 0, 1))
            If res <> 0 Then

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Compra ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter
                                          )
                Dim totalCosto As Double = grGastos.GetTotal(grGastos.RootTable.Columns("monto"), AggregateFunction.Sum)
                _GuadarCostos(res, _CodProveedor, totalCosto, CType(grGastos.DataSource, DataTable))
                _Limpiar()
                _prInhabiliitar()
                _prCargarCompra()

                'btnImportar.Visible = False
                cbMoneda.ReadOnly = False
                cont = 0
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "La Compra no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Public Sub RecuperarDatosTFC001()
        _detalleCompras = L_prCompraComprobanteGeneralPorNumi(-1)
        '_ValidarDatosFacturacion()
        Dim ffec As String
        Dim fnit As String
        Dim frsocial As String
        Dim fnro As String
        Dim fndui As String
        Dim fautoriz As String
        Dim fmonto As String
        Dim fccont As String
        Dim sujetoCreditoFiscal As String
        Dim nosujetoCreditoFiscal As String
        Dim subTotal As String
        Dim fdesc As String
        Dim importeBaseCreditoFiscal As String
        Dim creditoFiscal As String

        If swEmision.Value = True Then
            ffec = tbFechaVenta.Value.ToString("yyyy/MM/dd")
            fnit = tbNitProv.Text
            frsocial = tbProveedor.Text
            fnro = tbNFactura.Text
            'If tbNDui.Text = String.Empty Then
            '    tbNDui.Text = 0
            'End If
            'fndui = tbNDui.Text

            'fautoriz = tbNAutorizacion.Text
            fmonto = tbtotal.Value.ToString + tbMdesc.Value
            'If tbSACF.Text = String.Empty Then
            '    tbSACF.Text = fmonto
            'End If
            ' sujetoCreditoFiscal = tbSACF.Text

            'If sujetoCreditoFiscal = String.Empty Then
            '    sujetoCreditoFiscal = fmonto
            'End If
            nosujetoCreditoFiscal = tbtotal.Value.ToString - sujetoCreditoFiscal
            subTotal = fmonto - nosujetoCreditoFiscal
            'If tbMdesc.Value = String.Empty Then
            '    tbMdesc.Value = 0
            'End If
            fdesc = tbMdesc.Value.ToString
            'tbImporteBaseCreditoFiscal.Value = TbSubTotal.Value - TbdDescuento.Value
            importeBaseCreditoFiscal = fmonto - fdesc
            creditoFiscal = importeBaseCreditoFiscal * 0.13
            'fccont = tbCodControl.Text
            Dim numi As String = ""

            _detalleCompras.Rows.Add(1, ffec, fnit, frsocial, fnro, fndui, fautoriz, fmonto, nosujetoCreditoFiscal, subTotal, fdesc, importeBaseCreditoFiscal, creditoFiscal, fccont, 1, 0, 0)

        Else
            ffec = tbFechaVenta.Value.ToString("yyyy/MM/dd")
            fnit = tbNitProv.Text
            frsocial = tbProveedor.Text
            fnro = tbNFactura.Text
            fndui = 0
            fautoriz = 0
            fmonto = tbtotal.Value.ToString
            'sujetoCreditoFiscal = tbSACF.Text
            nosujetoCreditoFiscal = 0
            subTotal = fmonto
            fdesc = tbMdesc.Value.ToString
            importeBaseCreditoFiscal = fmonto - fdesc
            creditoFiscal = 0
            fccont = 0
            Dim numi As String = ""

            _detalleCompras.Rows.Add(1, ffec, fnit, frsocial, fnro, fndui, fautoriz, fmonto, nosujetoCreditoFiscal, subTotal, fdesc, importeBaseCreditoFiscal, creditoFiscal, fccont, 1, 0, 0)

        End If

    End Sub



    Private Sub _prGuardarModificado()
        ''RecuperarDatosTFC001()
        Dim res As Boolean = L_fnModificarCompra(tbCodigo.Text, cbSucursal.Value, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodProveedor,
                                                 IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True, Now.Date.ToString("yyyy/MM/dd"),
                                                 tbFechaVenc.Value.ToString("yyyy/MM/dd")), cbMoneda.Value, tbObservacion.Text, tbMdesc.Value,
                                                 tbtotal.Value, CType(grdetalle.DataSource, DataTable), _detalleCompras, IIf(swEmision.Value = True, 1, 0),
                                                 tbNFactura.Text, IIf(swConsigna.Value = True, 1, 0), IIf(swRetencion.Value = True, 1, 0),
                                                 IIf(cbMoneda.Value = 1, 1, tbTipoCambio.Value), 0)
        If res Then

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Compra ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            _prCargarCompra()

            _prSalir()


        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Compra no pudo ser Modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()
            If grCompra.RowCount > 0 Then
                _prMostrarRegistro(0)
            End If
        Else
            If gs_ComVenPro > 0 Then
                gs_ComVenPro = 0
                Me.Close()
            Else
                '  Public _modulo As SideNavItem
                Me.Close()
                _modulo.Select()
            End If
        End If
    End Sub
    Public Sub _prCargarIconELiminar()
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim Bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img.Save(Bin, Imaging.ImageFormat.Png)
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
            grdetalle.RootTable.Columns("img").Visible = True
        Next
        For i As Integer = 0 To CType(grGastos.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim Bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img.Save(Bin, Imaging.ImageFormat.Png)
            CType(grGastos.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
            grGastos.RootTable.Columns("img").Visible = True
        Next

    End Sub
    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grCompra.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grCompra.Row = _MPos
        End If
    End Sub

    Private Sub P_GenerarReporteCompra()
        Dim dt As DataTable = L_fnNotaTransito(tbCodigo.Text)
        'Dim dt2 = L_DatosEmpresa("1")
        Dim _TotalLi As Decimal
        Dim _Literal, _TotalDecimal, _TotalDecimal2, moneda As String

        'Literal 
        _TotalLi = dt.Rows(0).Item("total")
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        If cbMoneda.Value = 1 Then
            moneda = "Bolivianos"
        ElseIf cbMoneda.Value = 0 Then
            moneda = "Dólares"
        End If

        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + "  " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 " + moneda


        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        P_Global.Visualizador = New Visualizador

        Dim objrep As New R_NotaTransito
        objrep.SetDataSource(dt)

        objrep.SetParameterValue("Literal", _Literal)
        objrep.SetParameterValue("logo", gb_UbiLogo)
        objrep.SetParameterValue("documento", tbNFactura.Text)
        objrep.SetParameterValue("usuario", gs_user)



        P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        P_Global.Visualizador.ShowDialog() 'Comentar
        P_Global.Visualizador.BringToFront()

        'P_Global.Visualizador.CRV1.ReportSource = objrep
        'P_Global.Visualizador.Show()
        'P_Global.Visualizador.BringToFront()
    End Sub

    Private Sub CargarDatosExcel(dt As DataTable)
        Dim dt2 As DataTable = cargarDatos(dt, gi_userSuc)

        grdetalle.DataSource = dt2
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        '      a.cbnumi ,a.cbtv1numi ,a.cbty5prod ,b.yfcdprod1 as producto,a.cbest ,a.cbcmin 
        ',a.cbumin ,Umin .ycdes3 as unidad,a.cbpcost,a.cblote ,a.cbfechavenc ,a.cbptot 
        ',a.cbutven ,a.cbprven   ,a.cbobs ,
        'a.cbfact ,a.cbhact ,a.cbuact,1 as estado,Cast(null as Image) as img,a.cbpcost as costo,a.cbprven as venta
        If (Lote = True) Then
            With grdetalle.RootTable.Columns("cblote")
                .Width = 150
                .Caption = "LOTE"
                .Visible = True
                .MaxLength = 50
            End With
            With grdetalle.RootTable.Columns("cbfechavenc")
                .Width = 120
                .Caption = "FECHA VENC."
                .Visible = True
                .FormatString = "dd/MM/yyyy"
            End With
        Else
            With grdetalle.RootTable.Columns("cblote")
                .Width = 150
                .Caption = "LOTE"
                .Visible = False
                .MaxLength = 50
            End With
            With grdetalle.RootTable.Columns("cbfechavenc")
                .Width = 120
                .Caption = "FECHA VENC."
                .Visible = False
                .FormatString = "dd/MM/yyyy"
            End With
        End If
        With grdetalle.RootTable.Columns("yfCodAux1")
            .Width = 100
            .Caption = "Item Nuevo"
            .Visible = True
            .TextAlignment = 2
        End With
        With grdetalle.RootTable.Columns("yfCodAux2")
            .Width = 100
            .Caption = "Item Antiguo"
            .Visible = True
            .TextAlignment = 2
        End With
        With grdetalle.RootTable.Columns("cbnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("gasto")
            .Width = 100
            .Caption = "Costo"
            .Visible = True
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("cbtv1numi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbty5prod")
            .Width = 90
            .Caption = "Item"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("CodigoFabrica")
            .Caption = "Cod.Fabrica"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = True
            .TextAlignment = 2

        End With
        With grdetalle.RootTable.Columns("CodigoMarca")
            .Caption = "Cod.Marca"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = True
            .TextAlignment = 2
        End With

        With grdetalle.RootTable.Columns("Medida")
            .Caption = "Medida"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = False

        End With

        With grdetalle.RootTable.Columns("Marca")
            .Caption = "Marca"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("Procedencia")
            .Caption = "Procedencia"
            .Width = 120
            .WordWrap = True
            .MaxLines = 2
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("producto")
            .Caption = "Descripcion"
            .Width = 400
            .WordWrap = True
            .MaxLines = 3
            .Visible = True

        End With
        With grdetalle.RootTable.Columns("cbest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("cbcmin")
            .Width = 110
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0"
            .Caption = "Cantidad"
        End With
        With grdetalle.RootTable.Columns("cbumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("unidad")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "Unidad".ToUpper
        End With
        With grdetalle.RootTable.Columns("gasto")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "Costo".ToUpper
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("cbpcost")
            .Width = 140
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0.000"
            .Caption = "P.CostoUn."
        End With
        If (_estadoPor = 1) Then
            With grdetalle.RootTable.Columns("cbutven")
                .Width = 110
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Utilidad (%)".ToUpper
            End With
            With grdetalle.RootTable.Columns("cbprven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Precio Venta".ToUpper
            End With
        Else
            With grdetalle.RootTable.Columns("cbutven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Utilidad.".ToUpper
            End With
            With grdetalle.RootTable.Columns("cbprven")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = False
                .FormatString = "0.00"
                .Caption = "Precio Venta.".ToUpper
            End With
        End If
        With grdetalle.RootTable.Columns("PP")
            .Width = 120
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0.00"
            .Caption = "PP"

        End With
        With grdetalle.RootTable.Columns("PPA")
            .Width = 120
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0.00"
            .Caption = "PPA"
        End With
        With grdetalle.RootTable.Columns("cbptot")
            .Width = 120
            .TextAlignment = 2
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Sub Total"
        End With
        With grdetalle.RootTable.Columns("cbobs")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .FormatString = "yyyy/MM/dd"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbuact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("costo")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("yftcam")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("venta")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("cbpFacturado")
            .Width = 140
            .TextAlignment = 2
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.Publico"
        End With
        With grdetalle.RootTable.Columns("cbpPublico")
            .Width = 140
            .TextAlignment = 2
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.Taller"
        End With
        With grdetalle.RootTable.Columns("cbpMecanico")
            .Width = 140
            .TextAlignment = 2
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.GDB"
        End With
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        VerificarOrden(dt)
    End Sub

    Private Sub VerificarOrden(dt As DataTable)
        Dim dt2 As DataTable = CType(grdetalle.DataSource, DataTable)
        grdetalle.DataSource = L_fnDetalleCompra(-1)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            For Each fil As DataRow In dt2.Rows
                If fil.Item("yfCodAux1") = dt.Rows(i).Item("ItemNuevo") Then
                    CType(grdetalle.DataSource, DataTable).ImportRow(fil)
                End If
            Next
        Next
    End Sub
    Private Sub CargarEliminar()
        For i As Integer = 0 To grdetalle.RowCount - 1 Step 1
            Dim Bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img.Save(Bin, Imaging.ImageFormat.Png)
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
        Next
    End Sub
    Private Sub ImportarExcel()
        Try
            Dim folder As String = ""
            Dim doc As String = "Hoja1"
            Dim openfile1 As OpenFileDialog = New OpenFileDialog()

            If openfile1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                folder = openfile1.FileName
            End If

            If True Then
                Dim pathconn As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & folder & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"

                Dim con As OleDbConnection = New OleDbConnection(pathconn)
                Dim MyDataAdapter As OleDbDataAdapter = New OleDbDataAdapter("Select * from [" & doc & "$]", con)
                con.Open()

                MyDataAdapter.Fill(ProductosImport)
                CargarDatosExcel(ProductosImport)
                CargarEliminar()
                con.Close()

            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
#End Region


#Region "Eventos Formulario"
    Private Sub F0_Ventas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
        btnNuevo.PerformClick()
        SwProforma.Value = False
        If (SwProforma.Value = True) Then
            btnBuscarProforma.Enabled = True
        Else
            btnBuscarProforma.Enabled = False
        End If
        If (gs_ComVenPro > 0) Then
            If btnGrabar.Enabled = True Then
                Dim bandera As Boolean = True
                If (bandera = True) Then
                    _prInhabiliitar()
                    btnNuevo.Visible = False
                    btnEliminar.Visible = False
                    btnModificar.Visible = False
                    btnGrabar.Visible = False
                    btnVerPagos.Visible = True
                    If grCompra.RowCount > 0 Then
                        Dim pos As Integer
                        Dim cont As Integer = 0
                        For Each fila As GridEXRow In grCompra.GetRows()
                            If (CInt(fila.Cells("canumi").Value.ToString) = gs_ComVenPro) Then
                                pos = fila.Position
                            Else
                                cont += 1
                            End If
                        Next
                        grCompra.Row = pos
                        _prMostrarRegistro(0)

                    End If
                End If
            End If
        End If
    End Sub
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _Limpiar()
        _prhabilitar()


        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelNavegacion.Enabled = False
        btnImportar.Visible = True



    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        cont = 0
        _prSalir()

    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

    End Sub
    Private Sub MostrarMensajeOk(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.OK,
                               5000,
                               eToastGlowColor.Green,
                               eToastPosition.TopCenter)
    End Sub
    Private Sub tbProveedor_KeyDown(sender As Object, e As KeyEventArgs) Handles tbProveedor.KeyDown
        Try
            If (_fnAccesible()) Then
                If e.KeyData = Keys.Control + Keys.Enter Then

                    Dim dt As DataTable

                    dt = L_fnListarProveedores()
                    '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
                    ',a.ydtelf1 ,a.ydfnac 
                    If dt.Rows.Count = 0 Then
                        Throw New Exception("Lista de proveedores vacia")
                    End If
                    Dim listEstCeldas As New List(Of Modelo.Celda)
                    listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "COD ORIG.", 90))
                    listEstCeldas.Add(New Modelo.Celda("ydcod", True, "COD PROV.", 90))
                    listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
                    listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
                    listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
                    listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
                    listEstCeldas.Add(New Modelo.Celda("ydfnac", False, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
                    Dim ef = New Efecto
                    ef.tipo = 3
                    ef.dt = dt
                    ef.SeleclCol = 2
                    ef.listEstCeldas = listEstCeldas
                    ef.alto = 50
                    ef.ancho = 350
                    ef.Context = "Seleccione Proveedor".ToUpper
                    ef.ShowDialog()
                    Dim bandera As Boolean = False
                    bandera = ef.band
                    If (bandera = True) Then
                        Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

                        _CodProveedor = Row.Cells("ydnumi").Value
                        tbProveedor.Text = Row.Cells("yddesc").Value
                        'tbCodProv.Text = (Row.Cells("ydnumi").Value + " ' - '" + Row.Cells("ydcod").Value).ToString
                        tbCodProv.Text = Row.Cells("ydnumi").Text + "-" + Row.Cells("ydcod").Text
                        tbNitProv.Text = Row.Cells("yddctnum").Value
                        tbObservacion.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub swTipoVenta_ValueChanged(sender As Object, e As EventArgs) Handles swTipoVenta.ValueChanged
        If (swTipoVenta.Value = False) Then
            lbCredito.Visible = True
            tbFechaVenc.Visible = True
            tbFechaVenc.Value = Now.Date
        Else
            lbCredito.Visible = False
            tbFechaVenc.Visible = False
        End If
    End Sub

    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        If (_fnAccesible()) Then

            If (_estadoPor = 0) Then
                If (e.Column.Index = grdetalle.RootTable.Columns("cbcmin").Index Or e.Column.Index = grdetalle.RootTable.Columns("cbpcost").Index) Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            Else
                If (e.Column.Index = grdetalle.RootTable.Columns("cbcmin").Index Or e.Column.Index = grdetalle.RootTable.Columns("cbpcost").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("cbprven").Index Or e.Column.Index = grdetalle.RootTable.Columns("cbutven").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("cblote").Index Or e.Column.Index = grdetalle.RootTable.Columns("cbfechavenc").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("cbpFacturado").Index Or e.Column.Index = grdetalle.RootTable.Columns("cbpPublico").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("cbpMecanico").Index) Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            End If

        Else
            e.Cancel = True
        End If

    End Sub

    Private Sub grdetalle_Enter(sender As Object, e As EventArgs) Handles grdetalle.Enter

        If (_fnAccesible()) Then
            If (_CodProveedor <= 0) Then
                ToastNotification.Show(Me, "           Antes de Continuar Por favor Seleccione un Proveedor!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                tbProveedor.Focus()
                Return
            End If

            If (tbTipoCambio.Value <= 0) Then
                ToastNotification.Show(Me, "           Antes de continuar por favor introduzca Tipo de Cambio mayor a 0!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                tbProveedor.Focus()
                Return
            End If
            'grdetalle.Select()
            'grdetalle.Col = 2
            'grdetalle.Row = 0
        End If


    End Sub
    Private Sub grdetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles grdetalle.KeyDown
        If (Not _fnAccesible()) Then
            Return
        End If
        If (e.KeyData = Keys.Enter) Then
            Dim f, c As Integer
            c = grdetalle.Col
            f = grdetalle.Row

            If (grdetalle.Col = grdetalle.RootTable.Columns("cbcmin").Index) Then
                If (grdetalle.GetValue("producto") <> String.Empty) Then

                    _HabilitarProductos()
                Else
                    ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
            If (grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
                If (grdetalle.GetValue("producto") <> String.Empty) Then

                    _HabilitarProductos()
                Else
                    ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
salirIf:
        End If

        If (e.KeyData = Keys.Control + Keys.Enter And grdetalle.Row >= 0 And
            grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
            Dim indexfil As Integer = grdetalle.Row
            Dim indexcol As Integer = grdetalle.Col
            _HabilitarProductos()

        End If
        If (e.KeyData = Keys.Escape And grdetalle.Row >= 0) Then

            _prEliminarFila()


        End If


    End Sub
    Private Sub grProductos_KeyDown(sender As Object, e As KeyEventArgs) Handles grProductos.KeyDown
        If (Not _fnAccesible()) Then
            Return
        End If
        If (e.KeyData = Keys.Enter) Then
            Dim f, c As Integer
            c = grProductos.Col
            f = grProductos.Row
            If (f >= 0) Then
                Dim pos As Integer = -1
                grdetalle.Row = grdetalle.RowCount - 1
                _fnObtenerFilaDetalle(pos, grdetalle.GetValue("cbnumi"))
                Dim existe As Boolean = _fnExisteProducto(grProductos.GetValue("yfnumi"))
                If (pos >= 0) Then ''And (Not existe))
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbty5prod") = grProductos.GetValue("yfnumi")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = grProductos.GetValue("yfcdprod1")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbumin") = grProductos.GetValue("yfumin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = grProductos.GetValue("UnidMin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost") = grProductos.GetValue("yhprecio")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = grProductos.GetValue("yhprecio")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbcmin") = 1

                    Dim PrecioVenta As Double = IIf(IsDBNull(grProductos.GetValue("venta")), 0, grProductos.GetValue("venta"))
                    If (PrecioVenta > 0) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = PrecioVenta
                        Dim montodesc As Double = PrecioVenta - grProductos.GetValue("yhprecio")
                        Dim precio As Integer = IIf(IsDBNull(grProductos.GetValue("yhprecio")), 0, grProductos.GetValue("yhprecio"))
                        If (precio = 0) Then
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbutven") = 100
                        Else
                            Dim pordesc As Double = ((montodesc * 100) / grProductos.GetValue("yhprecio"))
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbutven") = pordesc
                        End If


                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbutven") = _PorcentajeUtil
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = (grProductos.GetValue("yhprecio") + ((grProductos.GetValue("yhprecio")) * (_PorcentajeUtil / 100)))


                    End If


                    _prCalcularPrecioTotal()
                    PanelDetalle.Height = 250
                    _DesHabilitarProductos()
                    'Else
                    '    If (existe) Then
                    '        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    '        ToastNotification.Show(Me, "El producto ya existe en el detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    '    End If
                End If
            End If
        End If
        If e.KeyData = Keys.Escape Then
            _DesHabilitarProductos()
        End If
    End Sub
    Private Sub grdetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged
        If tbTipoCambio.Value > 0 Then
            Dim lin As Integer = grdetalle.GetValue("cbnumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, lin)
            If (e.Column.Index = grdetalle.RootTable.Columns("cbcmin").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("cbcmin")) Or grdetalle.GetValue("cbcmin").ToString = String.Empty) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                Else
                    If (grdetalle.GetValue("cbcmin") > 0) Then
                        Dim rowIndex As Integer = grdetalle.Row
                        P_PonerTotal(rowIndex)
                    Else

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbcmin") = 1
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                        _prCalcularPrecioTotal()
                    End If
                End If

            End If

            ''''''''''''''''''''''COSTO  ''''''''''''''''''''''''''''''''''''''''''
            If (e.Column.Index = grdetalle.RootTable.Columns("cbpcost").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("cbpcost")) Or grdetalle.GetValue("cbpcost").ToString = String.Empty) Then
                    Dim cantidad As Double = grdetalle.GetValue("cbcmin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = cantidad * CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = _PorcentajeUtil * CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")

                Else
                    If (grdetalle.GetValue("cbpcost") > 0) Then
                        Dim rowIndex As Integer = grdetalle.Row
                        P_PonerTotal(rowIndex)
                        '--------------------ULTIMA PARTE COMENTADA------------------------------------------------
                        'Dim pFacturado As Double
                        'Dim uni As Double = grdetalle.GetValue("cbpcost")
                        '''Cálculo de los demás precios
                        'pFacturado = ((uni + (uni * 0.25) + (uni * 0.16)) * 2) * 7

                        'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpFacturado") = Math.Round(pFacturado, 2)
                        'grdetalle.SetValue("cbpFacturado", Math.Round(pFacturado, 2))
                        'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpPublico") = Math.Round(pFacturado - (pFacturado * 0.1), 2)
                        'grdetalle.SetValue("cbpPublico", Math.Round(pFacturado - (pFacturado * 0.1), 2))
                        'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpMecanico") = Math.Round(pFacturado - (pFacturado * 0.15), 2)
                        'grdetalle.SetValue("cbpMecanico", Math.Round(pFacturado - (pFacturado * 0.15), 2))
                        '----------------------------------------------------------------------------------------------
                        'If btnGrabar.Enabled = True Then
                        '    Dim dt As DataTable
                        '    dt = _lftraerPrecioCostoStock(CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbty5prod").ToString, gi_userSuc)
                        '    For j = 0 To dt.Rows.Count - 1 Step 1
                        '        Dim ant As Double = dt.Rows(j).Item("iccven") * dt.Rows(j).Item("yfPrecioCosto")
                        '        Dim act As Double = (CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbcmin")) * uni
                        '        Dim cant As Double = dt.Rows(j).Item("iccven") + CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbcmin")
                        '        Dim sum As Double = ant + act
                        '        Dim total As Double = sum / cant
                        '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("PP") = total.ToString("0.00")

                        '    Next

                        'End If
                    Else

                        Dim cantidad As Double = grdetalle.GetValue("cbcmin")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbptot") = cantidad * CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = _PorcentajeUtil * CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                    End If
                End If
            End If

            ''''''''''''''''''''''PRECIO FACTURADO'''''''''''''''''''''''
            'If (e.Column.Index = grdetalle.RootTable.Columns("cbpFacturado").Index) Then
            '    If (Not IsNumeric(grdetalle.GetValue("cbpFacturado")) Or grdetalle.GetValue("cbpFacturado").ToString = String.Empty) Then

            '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpFacturado") = 0
            '        grdetalle.SetValue("cbpFacturado", 0.00)
            '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpPublico") = 0
            '        grdetalle.SetValue("cbpPublico", 0)
            '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpMecanico") = 0
            '        grdetalle.SetValue("cbpMecanico", 0)

            '    Else
            '        If (grdetalle.GetValue("cbpFacturado") > 0) Then
            '            Dim pFacturado As Double

            '            ''Cálculo de los demás precios
            '            pFacturado = grdetalle.GetValue("cbpFacturado")

            '            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpPublico") = pFacturado - (pFacturado * 0.1)
            '            grdetalle.SetValue("cbpPublico", pFacturado - (pFacturado * 0.1))
            '            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpMecanico") = pFacturado - (pFacturado * 0.15)
            '            grdetalle.SetValue("cbpMecanico", pFacturado - (pFacturado * 0.15))
            '        Else
            '            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpFacturado") = 0
            '            grdetalle.SetValue("cbpFacturado", 0)
            '            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpPublico") = 0
            '            grdetalle.SetValue("cbpPublico", 0)
            '            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpMecanico") = 0
            '            grdetalle.SetValue("cbpMecanico", 0)
            '        End If
            '    End If
            'End If


            '''''''''''''''''''''''PRECIO PÚBLICO'''''''''''''''''''''''
            'If (e.Column.Index = grdetalle.RootTable.Columns("cbpPublico").Index) Then
            '    If (Not IsNumeric(grdetalle.GetValue("cbpPublico")) Or grdetalle.GetValue("cbpPublico").ToString = String.Empty) Then
            '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpPublico") = 0
            '        grdetalle.SetValue("cbpPublico", 0)
            '    End If
            'End If
            '''''''''''''''''''''''PRECIO MECÁNICO'''''''''''''''''''''''
            'If (e.Column.Index = grdetalle.RootTable.Columns("cbpMecanico").Index) Then
            '    If (Not IsNumeric(grdetalle.GetValue("cbpMecanico")) Or grdetalle.GetValue("cbpMecanico").ToString = String.Empty) Then
            '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpMecanico") = 0
            '        grdetalle.SetValue("cbpMecanico", 0)

            '    End If
            'End If

            '''''''''''''''''''PRECIO VENTA '''''''''   CONTINUARA  '''''''''''''
            ''Habilitar solo las columnas de Precio, %, Monto y Observación
            ''     a.cbnumi ,a.cbtv1numi ,a.cbty5prod ,b.yfcdprod1 as producto,a.cbest ,a.cbcmin ,a.cbumin ,Umin .ycdes3 as unidad,a.cbpcost 
            '',a.cbutven ,a.cbprven  ,a.cbptot ,a.cbobs ,
            ''a.cbfact ,a.cbhact ,a.cbuact,1 as escado,Cast(null as Image) as img,costo,venta
            'If (e.Column.Index = grdetalle.RootTable.Columns("cbprven").Index) Then
            '    If (Not IsNumeric(grdetalle.GetValue("cbprven")) Or grdetalle.GetValue("cbprven").ToString = String.Empty) Then

            '        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta")
            '        Dim montodesc As Double = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta") - grdetalle.GetValue("cbpcost")
            '        Dim pordesc As Double = ((montodesc * 100) / CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost"))
            '    Else
            '        If (grdetalle.GetValue("cbprven") > 0) Then

            '            'Dim montodesc As Double = grdetalle.GetValue("cbprven") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
            '            'Dim pordesc As Double = ((montodesc * 100) / CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost"))
            '            'grdetalle.SetValue("cbutven", pordesc)

            '            Dim montodesc As Double = grdetalle.GetValue("cbprven") - (grdetalle.GetValue("cbpcost") * tbTipoCambio.Value)
            '            Dim pordesc As Double = ((montodesc * 100) / (grdetalle.GetValue("cbpcost") * tbTipoCambio.Value))
            '            pordesc = Format(pordesc, "0.00")
            '            grdetalle.SetValue("cbutven", pordesc)

            '        Else

            '            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta")
            '            Dim montodesc As Double = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
            '            Dim pordesc As Double = ((montodesc * 100) / CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost"))

            '        End If
            '    End If
            'End If



            ''''''''''''''''''PORCENTAJE PRECIO VENTA '''''''''   CONTINUARA  '''''''''''''
            'Habilitar solo las columnas de Precio, %, Monto y Observación
            '     a.cbnumi ,a.cbtv1numi ,a.cbty5prod ,b.yfcdprod1 as producto,a.cbest ,a.cbcmin ,a.cbumin ,Umin .ycdes3 as unidad,a.cbpcost 
            ',a.cbutven ,a.cbprven  ,a.cbptot ,a.cbobs ,
            'a.cbfact ,a.cbhact ,a.cbuact,1 as escado,Cast(null as Image) as img,costo,venta
            If (e.Column.Index = grdetalle.RootTable.Columns("cbutven").Index) Then

                Dim venta As Double = IIf(IsDBNull(CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta")), 0, CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta"))
                Dim PrecioCosto As Double = IIf(IsDBNull(grdetalle.GetValue("cbpcost")), 0, (grdetalle.GetValue("cbpcost") * tbTipoCambio.Value))
                If (Not IsNumeric(grdetalle.GetValue("cbutven")) Or grdetalle.GetValue("cbutven").ToString = String.Empty) Then

                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta")
                    Dim montodesc As Double = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta") - grdetalle.GetValue("cbpcost")

                    Dim pordesc As Double = ((montodesc * 100) / CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost"))

                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbutven") = pordesc
                Else
                    If (grdetalle.GetValue("cbutven") > 0) Then

                        Dim porcentaje As Double = grdetalle.GetValue("cbutven")

                        Dim monto As Double = ((grdetalle.GetValue("cbpcost") * tbTipoCambio.Value) * (porcentaje / 100))
                        Dim precioventa As Double = monto + (grdetalle.GetValue("cbpcost") * tbTipoCambio.Value)
                        grdetalle.SetValue("cbprven", precioventa)

                    Else

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbprven") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta")
                        Dim montodesc As Double = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("venta") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost")
                        Dim pordesc As Double = ((montodesc * 100) / CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbpcost"))
                        pordesc = Format(pordesc, "0.00")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cbutven") = pordesc
                    End If
                End If
            End If
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
            If (estado = 1) Then
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
            End If
            '_CalcularPrecioPonderado()
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "El tipo de cambio debe ser mayor a 0".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If


    End Sub
    Private Sub tbPdesc_ValueChanged(sender As Object, e As EventArgs) Handles tbPdesc.ValueChanged
        If (tbPdesc.Focused) Then
            If (Not tbPdesc.Text = String.Empty And Not tbtotal.Text = String.Empty) Then
                If (tbPdesc.Value = 0 Or tbPdesc.Value > 100) Then
                    tbPdesc.Value = 0
                    tbMdesc.Value = 0

                    _prCalcularPrecioTotal()

                Else

                    Dim porcdesc As Double = tbPdesc.Value
                    Dim montodesc As Double = (grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum) * (porcdesc / 100))
                    tbMdesc.Value = montodesc
                    tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum) - montodesc
                End If


            End If
            If (tbPdesc.Text = String.Empty) Then
                tbMdesc.Value = 0

            End If
        End If
    End Sub

    Private Sub tbMdesc_ValueChanged(sender As Object, e As EventArgs) Handles tbMdesc.ValueChanged
        If (tbMdesc.Focused) Then

            Dim total As Double = tbtotal.Value
            If (Not tbMdesc.Text = String.Empty And Not tbMdesc.Text = String.Empty) Then
                If (tbMdesc.Value = 0 Or tbMdesc.Value > total) Then
                    tbMdesc.Value = 0
                    tbPdesc.Value = 0
                    _prCalcularPrecioTotal()
                Else
                    Dim montodesc As Double = tbMdesc.Value
                    Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum))
                    tbPdesc.Value = pordesc
                    tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum) - montodesc

                End If

            End If

            If (tbMdesc.Text = String.Empty) Then
                tbMdesc.Value = 0

            End If
        End If

    End Sub


    Private Sub grdetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellEdited
        If (e.Column.Index = grdetalle.RootTable.Columns("cbcmin").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("cbcmin")) Or grdetalle.GetValue("cbcmin").ToString = String.Empty) Then
                grdetalle.SetValue("cbcmin", 1)
                grdetalle.SetValue("cbptot", grdetalle.GetValue("cbpcost"))
            Else
                If (grdetalle.GetValue("cbcmin") > 0) Then

                Else
                    grdetalle.SetValue("cbcmin", 1)
                    grdetalle.SetValue("cbptot", grdetalle.GetValue("cbpcost"))
                End If
            End If
        End If
        _prCalcularPrecioTotal()
    End Sub
    Private Sub grdetalle_MouseClick(sender As Object, e As MouseEventArgs) Handles grdetalle.MouseClick
        If (Not _fnAccesible()) Then
            Return
        End If

        Try
            If (grdetalle.RowCount >= 1) Then
                If (grdetalle.CurrentColumn.Index = grdetalle.RootTable.Columns("img").Index) Then
                    _prEliminarFila()
                End If
            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub ConvertirPonderado()
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("PP") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("PP") * tbTipoCambio.Value
        Next
    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        If _ValidarCampos() = False Then
            Exit Sub
        End If
        If cbMoneda.Value = 0 Then
            ConvertirBs()
            ConvertirPonderado()
        End If
        If (tbCodigo.Text = String.Empty) Then
            _GuardarNuevo()
        Else
            If (tbCodigo.Text <> String.Empty) Then
                _prGuardarModificado()
                ''    _prInhabiliitar()

            End If
        End If

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Dim res As Boolean = L_fnVerificarSiSeContabilizo(tbCodigo.Text)
        If res Then
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Compra no puede ser Modificada porque ya fue contabilizada".ToUpper, img, 3500, eToastGlowColor.Red, eToastPosition.TopCenter)
        Else
            If (grCompra.RowCount > 0) Then
                _prhabilitar()
                btnNuevo.Enabled = False
                btnModificar.Enabled = False
                btnEliminar.Enabled = False
                btnGrabar.Enabled = True

                PanelNavegacion.Enabled = False
                _prCargarIconELiminar()
            End If
        End If
    End Sub
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        'Dim result As Boolean = L_fnVerificarSiSeContabilizo(tbCodigo.Text)
        'If result Then
        '    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        '    ToastNotification.Show(Me, "La Compra no puede ser Eliminada porque ya fue contabilizada".ToUpper, img, 3500, eToastGlowColor.Red, eToastPosition.TopCenter)
        'Else
        '    Dim ef = New Efecto
        '    ef.tipo = 2
        '    ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        '    ef.Header = "mensaje principal".ToUpper
        '    ef.ShowDialog()
        '    Dim bandera As Boolean = False
        '    bandera = ef.band
        '    If (bandera = True) Then
        '        Dim mensajeError As String = ""
        '        Dim res As Boolean = L_fnEliminarCompra(tbCodigo.Text, mensajeError)
        '        If res Then

        '            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        '            ToastNotification.Show(Me, "Código de Compra ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
        '                                      img, 2000,
        '                                      eToastGlowColor.Green,
        '                                      eToastPosition.TopCenter)

        '            _prFiltrar()

        '        Else
        '            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        '            ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '        End If
        '    End If
        'End If
        If (swTipoVenta.Value = False) Then
            Dim res1 As Boolean = L_fnVerificarPagosCompras(tbCodigo.Text)
            If res1 Then
                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
                ToastNotification.Show(Me, "No se puede eliminar la Compra con código ".ToUpper + tbCodigo.Text + ", porque tiene pagos realizados, por favor primero elimine los pagos correspondientes a esta compra".ToUpper,
                                          img, 5000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                Exit Sub
            End If
        End If


        Dim result As Boolean = L_fnVerificarSiSeContabilizo(tbCodigo.Text)
        If result Then
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Compra no puede ser Eliminada porque ya fue contabilizada".ToUpper, img, 4500, eToastGlowColor.Red, eToastPosition.TopCenter)
        End If
        Dim ef = New Efecto
        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_fnEliminarCompra(tbCodigo.Text, mensajeError)
            If res Then

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Compra ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                _prFiltrar()

            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If

    End Sub

    Private Sub grVentas_SelectionChanged(sender As Object, e As EventArgs) Handles grCompra.SelectionChanged
        If (grCompra.RowCount >= 0 And grCompra.Row >= 0) Then
            _prMostrarRegistro(grCompra.Row)
        End If


    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grCompra.Row
        If _pos < grCompra.RowCount - 1 And _pos >= 0 Then
            _pos = grCompra.Row + 1
            '' _prMostrarRegistro(_pos)
            grCompra.Row = _pos
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grCompra.Row
        If grCompra.RowCount > 0 Then
            _pos = grCompra.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grCompra.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grCompra.Row
        If _MPos > 0 And grCompra.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grCompra.Row = _MPos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub
    Private Sub grVentas_KeyDown(sender As Object, e As KeyEventArgs) Handles grCompra.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grdetalle.Focus()

        End If
    End Sub


    Private Sub cbSucursal_KeyDown(sender As Object, e As KeyEventArgs) Handles cbSucursal.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Enter Then
                grdetalle.Focus()
                'grdetalle.Select()
                'grdetalle.Col = 3
                'grdetalle.Row = 0
            End If
        End If

    End Sub

    Private Sub tbtotal_ValueChanged(sender As Object, e As EventArgs) Handles tbtotal.ValueChanged
        ' tbSACF.Text = tbtotal.Text
    End Sub

    Private Sub swEmision_ValueChanged(sender As Object, e As EventArgs) Handles swEmision.ValueChanged
        If swEmision.Value = False Then
            lbNFactura.Text = "Nro. Recibo:"
            'GroupPanelFactura2.Text = "DATOS RECIBO"
            'lbNAutoriz.Visible = False
            'tbNAutorizacion.Visible = False
            'lbCodCtrl.Visible = False
            'tbCodControl.Visible = False
            'lbNDui.Visible = False
            'tbNDui.Visible = False
            'lbSACF.Visible = False
            'tbSACF.Visible = False
        Else
            lbNFactura.Text = "Nro. Factura:"
            'GroupPanelFactura2.Text = "DATOS FACTURACIÓN"
            'lbNAutoriz.Visible = True
            'tbNAutorizacion.Visible = True
            'lbCodCtrl.Visible = True
            'tbCodControl.Visible = True
            'lbNDui.Visible = True
            'tbNDui.Visible = True
            'lbSACF.Visible = True
            'tbSACF.Visible = True

        End If
    End Sub

    Private Sub tbNFactura_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbNFactura.KeyPress
        g_prValidarTextBox(1, e)
    End Sub

    Private Sub tbNAutorizacion_KeyPress(sender As Object, e As KeyPressEventArgs)
        g_prValidarTextBox(1, e)
    End Sub

    Private Sub tbNDui_KeyPress(sender As Object, e As KeyPressEventArgs)
        g_prValidarTextBox(1, e)
    End Sub

    Private Sub tbSACF_KeyPress(sender As Object, e As KeyPressEventArgs)
        g_prValidarTextBox(1, e)
    End Sub

    Private Sub swRetencion_ValueChanged(sender As Object, e As EventArgs) Handles swRetencion.ValueChanged
        If swRetencion.Value = False Or swRetencion.Value = True Then
            _prCalcularPrecioTotal()
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Maximized

        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub

    Private Function VerificarGrilla() As Boolean

        For I As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            If CType(grdetalle.DataSource, DataTable).Rows(I).Item("yfCodAux1") = "" Then
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub ConvertirBs()

        If CType(grdetalle.DataSource, DataTable).Rows.Count > 0 Then
            If VerificarGrilla() Then
                For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1

                    Dim tcam As Double = CType(grdetalle.DataSource, DataTable).Rows(i).Item("yftcam")
                    If BanderaImport = False Then
                        CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpcost") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpcost") * tbTipoCambio.Value
                    End If
                    'CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpFacturado") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpFacturado") * tbTipoCambio.Value
                    'CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpPublico") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpPublico") * tbTipoCambio.Value
                    'CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpMecanico") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpMecanico") * tbTipoCambio.Value
                    CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbptot") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbcmin") * CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpcost")
                    CType(grdetalle.DataSource, DataTable).Rows(i).Item("PPA") = (CType(grdetalle.DataSource, DataTable).Rows(i).Item("PPA")) * (CType(grdetalle.DataSource, DataTable).Rows(i).Item("yftcam"))

                Next
            End If
            'tbSubtotalC.Value = tbSubtotalC.Value * tbTipoCambio.Value
            'tbMdesc.Value = 0
            'tbtotal.Value = tbSubtotalC.Value
            _prCalcularPrecioTotal()
        End If
    End Sub
    Private Sub ConvertirSus()
        If CType(grdetalle.DataSource, DataTable).Rows.Count > 0 Then
            If VerificarGrilla() Then
                For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
                    If btnNuevo.Enabled = True Then
                        If BanderaImport = False Then
                            CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpcost") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpcost") / tbTipoCambio.Value
                        End If
                        CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbptot") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbcmin") * CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpcost")
                        CType(grdetalle.DataSource, DataTable).Rows(i).Item("PPA") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("PPA") / CType(grdetalle.DataSource, DataTable).Rows(i).Item("yftcam")
                    Else
                        If BanderaImport = False Then
                            CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpcost") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpcost") / tbTipoCambio.Value
                        End If
                        'CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpFacturado") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpFacturado") / tbTipoCambio.Value
                        'CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpPublico") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpPublico") / tbTipoCambio.Value
                        'CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpMecanico") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpMecanico") / tbTipoCambio.Value
                        CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbptot") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbcmin") * CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpcost")
                        CType(grdetalle.DataSource, DataTable).Rows(i).Item("PPA") = (CType(grdetalle.DataSource, DataTable).Rows(i).Item("PPA")) / (CType(grdetalle.DataSource, DataTable).Rows(i).Item("yftcam"))

                    End If
                Next
            End If
            'tbSubtotalC.Value = tbSubtotalC.Value / tbTipoCambio.Value
            'tbMdesc.Value = 0
            'tbtotal.Value = tbSubtotalC.Value
            _prCalcularPrecioTotal()
        End If
    End Sub
    Private Sub swMoneda_ValueChanged(sender As Object, e As EventArgs) Handles swMoneda.ValueChanged
        'If grdetalle.RowCount > 0 Then
        '    If btnGrabar.Enabled = True Then
        '        If swMoneda.Value = True Then
        '            lbTipoCambio.Visible = False
        '            tbTipoCambio.Visible = False

        '            ConvertirBs()
        '            tbTipoCambio.Value = 1
        '            _CalcularPrecioPonderado()
        '        Else

        '            Dim ef = New Efecto
        '            ef.tipo = 7
        '            ef.tipo1 = 1
        '            ef.ShowDialog()
        '            Dim bandera As Boolean = False
        '            bandera = ef.band
        '            If (bandera = True) Then

        '                tbTipoCambio.Text = ef.Cantidad
        '                ConvertirSus()
        '                _CalcularPrecioPonderado()
        '            Else
        '                ToastNotification.Show(Me, "Debe ingresar una tipo de cambio", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)



        '            End If
        '        End If
        '        lbTipoCambio.Visible = True
        '        tbTipoCambio.Visible = True
        '        'tbTipoCambio.Value = 0

        '    End If
        'End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If tbCodigo.Text <> String.Empty Then
            P_GenerarReporteCompra()
        End If

    End Sub
    Private Sub CalcularPonderado(cant As Double, uni As Double)
        Dim dt As DataTable
        'Dim cant As Double = grdetalle.GetValue("cbcmin")
        Dim TotalUnitario As Double = uni * cant
        dt = _lftraerPrecioCostoStock(grdetalle.GetValue("cbty5prod").ToString, gi_userSuc)

        Dim act As Double
            If cbMoneda.Value = 0 Then
            act = dt.Rows(0).Item("iccven") * dt.Rows(0).Item("yfPrecioCosto") / grdetalle.GetValue("yftcam")
        Else
            act = dt.Rows(0).Item("iccven") * dt.Rows(0).Item("yfPrecioCosto")

        End If
        Dim can As Double = dt.Rows(0).Item("iccven") + cant
        Dim sum As Double = TotalUnitario + act
            Dim total As Double = sum / can
            grdetalle.SetValue("PP", total.ToString("0.00"))

    End Sub
    Private Sub _CalcularPrecioPonderado()
        If CType(grdetalle.DataSource, DataTable).Rows.Count > 0 Then
            If CType(grdetalle.DataSource, DataTable).Rows(0).Item("yfCodAux1") <> "" Then
                For i As Integer = 0 To grdetalle.RowCount - 1 Step 1
                    Dim dt As DataTable
                    Dim cant As Double = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbcmin")
                    Dim TotalUnitario As Double = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpcost") * CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbcmin")
                    dt = _lftraerPrecioCostoStock(CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbty5prod").ToString, gi_userSuc)
                    For j = 0 To dt.Rows.Count - 1 Step 1
                        Dim act As Double
                        If cbMoneda.Value = 0 Then
                            act = dt.Rows(j).Item("iccven") * dt.Rows(j).Item("yfPrecioCosto") / CType(grdetalle.DataSource, DataTable).Rows(i).Item("yftcam")
                        Else
                            act = dt.Rows(j).Item("iccven") * dt.Rows(j).Item("yfPrecioCosto")

                        End If
                        Dim can As Double = dt.Rows(j).Item("iccven") + cant
                        Dim sum As Double = TotalUnitario + act
                        Dim total As Double = sum / can
                        CType(grdetalle.DataSource, DataTable).Rows(i).Item("PP") = total.ToString("0.00")
                    Next
                Next
            End If
        End If
    End Sub

    Private Function ValidarMoneda() As Boolean
        If cbMoneda.Value = 2 Then

            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
            ToastNotification.Show(Me, "SELECCIONE UN MONEDA PARA LA COMPRA".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

            Return False
        Else
            Return True
        End If
    End Function
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If ValidarMoneda() Then
            BanderaImport = False
            If cbMoneda.Value = 0 Then
                ConvertirBs()
            End If
            _HabilitarProductos()
            If cbMoneda.Value = 0 Then
                ConvertirSus()
            End If
            _CalcularPrecioPonderado()
            _prCalcularPrecioTotal()
        End If

    End Sub

    Private Sub btnBuscarProforma_Click(sender As Object, e As EventArgs) Handles btnBuscarProforma.Click
        If (_fnAccesible()) Then
            Dim dt As DataTable

            dt = L_fnGeneralProformaNoUtilizada()

            Dim listEstCeldas As New List(Of Modelo.Celda)
            listEstCeldas.Add(New Modelo.Celda("pcnumi,", True, "NRO PROFORMA", 120))
            listEstCeldas.Add(New Modelo.Celda("pcfdoc", True, "FECHA", 120, "dd/MM/yyyy"))
            listEstCeldas.Add(New Modelo.Celda("pcty4prov", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("ydcod", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("proveedor", True, "PROVEEDOR".ToUpper, 150))
            listEstCeldas.Add(New Modelo.Celda("yddctnum", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("pcest", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("pcobs", True, "OBSERVACION", 150))
            listEstCeldas.Add(New Modelo.Celda("pcdesc", False, "DESCUENTO".ToUpper, 120, "0.00"))
            listEstCeldas.Add(New Modelo.Celda("total", True, "TOTAL".ToUpper, 120, "0.00"))
            listEstCeldas.Add(New Modelo.Celda("pcfact", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("pchact", False, "", 50))
            listEstCeldas.Add(New Modelo.Celda("pcuact", False, "", 50))

            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 2
            ef.listEstCeldas = listEstCeldas
            ef.alto = 50
            ef.ancho = 350
            ef.Context = "Seleccione PROFORMA".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

                tbProforma.Text = Row.Cells("pcnumi").Value
                _CodProveedor = Row.Cells("pcty4prov").Value
                tbCodProv.Text = Row.Cells("pcty4prov").Text + "-" + Row.Cells("ydcod").Text
                tbProveedor.Text = Row.Cells("proveedor").Value
                tbNitProv.Text = Row.Cells("yddctnum").Value
                tbObservacion.Text = Row.Cells("pcobs").Value

                tbSubtotalC.Value = Row.Cells("pcdesc").Value + Row.Cells("total").Value
                tbMdesc.Value = Row.Cells("pcdesc").Value
                tbtotal.Value = Row.Cells("total").Value

                _prCargarProductoDeLaProforma(Row.Cells("pcnumi").Value)

            End If

        End If
    End Sub

    Sub _prCargarProductoDeLaProforma(numiProforma As Integer)
        Dim dt As DataTable = L_fnDetalleProformaCompra(numiProforma)

        If (dt.Rows.Count > 0) Then
            CType(grdetalle.DataSource, DataTable).Rows.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim numiproducto As Integer = dt.Rows(i).Item("pdty5prod")
                Dim nameproducto As String = dt.Rows(i).Item("producto")
                Dim lote As String = ""
                Dim FechaVenc As Date = Now.Date
                Dim cant As Double = dt.Rows(i).Item("pdcmin")

                _prAddDetalleVenta()
                grdetalle.Row = grdetalle.RowCount - 1

                CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbty5prod") = numiproducto
                grdetalle.SetValue("cbty5prod", numiproducto)
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("yfCodAux1") = dt.Rows(i).Item("yfCodAux1")
                grdetalle.SetValue("CodigoFabrica", dt.Rows(i).Item("yfCodAux1"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("yfCodAux2") = dt.Rows(i).Item("yfCodAux2")
                grdetalle.SetValue("CodigoFabrica", dt.Rows(i).Item("yfCodAux2"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("CodigoFabrica") = dt.Rows(i).Item("CodigoFabrica")
                grdetalle.SetValue("CodigoFabrica", dt.Rows(i).Item("CodigoFabrica"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("CodigoMarca") = dt.Rows(i).Item("CodigoMarca")
                grdetalle.SetValue("CodigoMarca", dt.Rows(i).Item("CodigoMarca"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("Medida") = dt.Rows(i).Item("Medida")
                grdetalle.SetValue("Medida", dt.Rows(i).Item("Medida"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("Marca") = dt.Rows(i).Item("Marca")
                grdetalle.SetValue("Marca", dt.Rows(i).Item("Marca"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("Procedencia") = dt.Rows(i).Item("Procedencia")
                grdetalle.SetValue("Procedencia", dt.Rows(i).Item("Procedencia"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("producto") = nameproducto
                grdetalle.SetValue("producto", nameproducto)
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbcmin") = cant
                grdetalle.SetValue("cbcmin", cant)
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbumin") = dt.Rows(i).Item("pdumin")
                grdetalle.SetValue("cbumin", dt.Rows(i).Item("pdumin"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("unidad") = dt.Rows(i).Item("unidad")
                grdetalle.SetValue("unidad", dt.Rows(i).Item("unidad"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpcost") = dt.Rows(i).Item("pdpcost")
                grdetalle.SetValue("cbpcost", dt.Rows(i).Item("pdpcost"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("cblote") = dt.Rows(i).Item("pdlote")
                grdetalle.SetValue("cblote", dt.Rows(i).Item("pdlote"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbfechavenc") = dt.Rows(i).Item("pdfechavenc")
                grdetalle.SetValue("cbfechavenc", dt.Rows(i).Item("pdfechavenc"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbptot") = dt.Rows(i).Item("pdptot")
                grdetalle.SetValue("cbptot", dt.Rows(i).Item("pdptot"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbutven") = dt.Rows(i).Item("pdutven")
                grdetalle.SetValue("cbutven", dt.Rows(i).Item("pdutven"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbprven") = dt.Rows(i).Item("pdprven")
                grdetalle.SetValue("cbprven", dt.Rows(i).Item("pdprven"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpFacturado") = dt.Rows(i).Item("pdpFacturado")
                grdetalle.SetValue("cbpFacturado", dt.Rows(i).Item("pdpFacturado"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpPublico") = dt.Rows(i).Item("pdpPublico")
                grdetalle.SetValue("cbpPublico", dt.Rows(i).Item("pdpPublico"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbpMecanico") = dt.Rows(i).Item("pdpMecanico")
                grdetalle.SetValue("cbpMecanico", dt.Rows(i).Item("pdpMecanico"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("costo") = dt.Rows(i).Item("costo")
                grdetalle.SetValue("costo", dt.Rows(i).Item("costo"))
                CType(grdetalle.DataSource, DataTable).Rows(i).Item("venta") = dt.Rows(i).Item("venta")
                grdetalle.SetValue("venta", dt.Rows(i).Item("venta"))
                P_PonerTotal(i)

            Next

            'grdetalle.Select()
            _prCalcularPrecioTotal()
        End If
    End Sub

    Private Sub SwProforma_ValueChanged(sender As Object, e As EventArgs) Handles SwProforma.ValueChanged
        If (_fnAccesible()) Then
            If (SwProforma.Value = True) Then
                btnBuscarProforma.Enabled = True
            Else
                btnBuscarProforma.Enabled = False
                tbProforma.Clear()
            End If
        End If
    End Sub

    Private Sub btnTraspaso_Click(sender As Object, e As EventArgs) Handles btnTraspaso.Click
        Dim frm As New F1_Traspaso

        With frm.cbSucOrigen
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 60
            .DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("aabdes").Width = 500
            .DropDownList.Columns("aabdes").Caption = "SUCURSAL"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = cbSucursal.DataSource
            .Refresh()
        End With
        frm.cbSucOrigen.Value = cbSucursal.Value
        frm.dt = CType(grdetalle.DataSource, DataTable).Copy
        frm.IdCompra = tbCodigo.Text
        frm.ShowDialog()

        Dim bandera As Boolean = False
        bandera = frm.banderaTraspaso
        If (bandera = True) Then

            ToastNotification.Show(Me, "Traspaso realizado con éxito.".ToUpper, My.Resources.GRABACION_EXITOSA, 4000, eToastGlowColor.Green, eToastPosition.TopCenter)
        Else
            ToastNotification.Show(Me, "El Traspaso no se pudo realizar", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)

        End If

    End Sub

    Private Sub btnVerPagos_Click(sender As Object, e As EventArgs) Handles btnVerPagos.Click
        Dim frm As New F0_PagosCreditoCompraUlt
        frm._nameButton = DinoM.P_Principal.btInvMovimiento.Name
        'frm._modulo = FP_COMPRAS
        frm.Show()
    End Sub

    Private Sub P_GenerarReporteCompra2()
        Dim dt As DataTable = L_fnNotaCompras(tbCodigo.Text)
        'Dim dt2 = L_DatosEmpresa("1")

        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        P_Global.Visualizador = New Visualizador

        Dim objrep As New R_RevisionLlegada
        objrep.SetDataSource(dt)

        'objrep.SetParameterValue("logo", gb_UbiLogo)
        objrep.SetParameterValue("usuario", gs_user)


        P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        P_Global.Visualizador.ShowDialog() 'Comentar
        P_Global.Visualizador.BringToFront()

        'P_Global.Visualizador.CRV1.ReportSource = objrep
        'P_Global.Visualizador.Show()
        'P_Global.Visualizador.BringToFront()
    End Sub
    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        P_GenerarReporteCompra2()
    End Sub

    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        If ValidarMoneda() Then
            ProductosImport.Clear()
            ImportarExcel()

            BanderaImport = True
            If cbMoneda.Value = 0 Then
                ConvertirSus()
            End If
            _CalcularPrecioPonderado()
            _prCalcularPrecioTotal()
        End If
    End Sub

    Private Sub cbMoneda_ValueChanged(sender As Object, e As EventArgs) Handles cbMoneda.ValueChanged

        If grdetalle.RowCount > 0 Then
            If btnGrabar.Enabled = True Then
                If cont = 0 Then
                    If cbMoneda.Value = 1 Then
                        lbTipoCambio.Visible = False
                        tbTipoCambio.Visible = False

                        ConvertirBs()
                        tbTipoCambio.Value = 1
                        _CalcularPrecioPonderado()
                    ElseIf cbMoneda.Value = 0 Then

                        Dim ef = New Efecto
                        ef.tipo = 7
                        ef.tipo1 = 1
                        ef.ShowDialog()
                        Dim bandera As Boolean = False
                        bandera = ef.band
                        If (bandera = True) Then

                            tbTipoCambio.Text = ef.Cantidad
                            ConvertirSus()
                            _CalcularPrecioPonderado()
                        Else
                            ToastNotification.Show(Me, "Debe ingresar una tipo de cambio", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)



                        End If
                        lbTipoCambio.Visible = True
                        tbTipoCambio.Visible = True
                        'tbTipoCambio.Value = 0
                    Else

                    End If
                    cont = 1
                    cbMoneda.ReadOnly = True
                End If


            End If
        End If
    End Sub

    Private Sub GroupPanel3_Click(sender As Object, e As EventArgs) Handles GroupPanel3.Click

    End Sub








#End Region

#Region "Costos de Importacion"
    Public Sub CalucloCostoImportacion()
        'Dim costoTotal As Integer = 0
        'For i As Integer = 0 To grGastos.RowCount - 1 Step 1
        '    costoTotal = costoTotal + CDbl(CType(grGastos.DataSource, DataTable).Rows(i).Item("gastos"))
        'Next
        Dim costoTotal As Integer = grGastos.GetTotal(grGastos.RootTable.Columns("gastos"), AggregateFunction.Sum)
        Dim unidades As Integer = grdetalle.GetTotal(grGastos.RootTable.Columns("tbcmin"), AggregateFunction.Sum)

        Dim CostoProducto As Double = costoTotal / unidades
        For i As Integer = 0 To grdetalle.RowCount - 1 Step 1
            Dim TotalProducto As Double = CostoProducto * CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbcmin")
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("PP") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("PP") + CostoProducto
        Next
    End Sub
    Private Function ValidarCostos() As Boolean

        If cbConcepto.SelectedIndex < 0 Then
            ToastNotification.Show(Me, "Ingrese un concepto valido, registre un nuevo concepto", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
            Return False
        End If
        If Not IsNumeric(tbMonto.Text) Then 'Or CDbl(tbMonto.Text) < 0 Then
            ToastNotification.Show(Me, "Ingrese un monto valido mayor a 0", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
            Return False
        End If
        If CDbl(tbMonto.Text) <= 0 Then
            ToastNotification.Show(Me, "Ingrese un monto valido mayor a 0", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
            Return False
        End If
        Return True
    End Function

    Private Sub ButtonX3_Click(sender As Object, e As EventArgs) Handles ButtonX3.Click
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        If ValidarCostos() Then
            CType(grGastos.DataSource, DataTable).Rows.Add(grGastos.RowCount + 1, cbConcepto.Value, cbConcepto.Text, tbMonto.Value, tbGlosaObs.Text, Bin.GetBuffer)
            cbConcepto.Text = ""
            tbMonto.Text = "0.00"
            tbGlosaObs.Text = ""
        End If
    End Sub

    Private Sub CalcularGastos()
        If btnNuevo.Enabled = False And grdetalle.DataSource IsNot Nothing Then
            If CType(grGastos.DataSource, DataTable).Rows.Count > 0 And CType(grdetalle.DataSource, DataTable).Rows.Count > 0 Then
                Dim totalCompra As Double = CType(grdetalle.DataSource, DataTable).Compute("sum(cbptot)", "estado <> -2")
                'grdetalle.GetTotal(grdetalle.RootTable.Columns("cbptot"), AggregateFunction.Sum)
                If CType(grdetalle.DataSource, DataTable).Rows(0).Item("yfCodAux1") <> "" Then
                    Dim totalGastos As Double = grGastos.GetTotal(grGastos.RootTable.Columns("monto"), AggregateFunction.Sum)
                    If CType(grdetalle.DataSource, DataTable).Rows.Count > 1 Then
                        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
                            If CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado") <> -2 Then
                                Dim porcentaje As Double = CType(grdetalle.DataSource, DataTable).Rows(i).Item("cbptot") * 100 / totalCompra
                                Dim gastoUni As Double = totalGastos * porcentaje / 100

                                CType(grdetalle.DataSource, DataTable).Rows(i).Item("gasto") = gastoUni.ToString("0.00")
                                CType(grdetalle.DataSource, DataTable).Rows(i).Item("PP") = CType(grdetalle.DataSource, DataTable).Rows(i).Item("PP") + CType(grdetalle.DataSource, DataTable).Rows(i).Item("gasto")
                            End If
                        Next
                    End If
                End If
            End If
            End If
    End Sub

    Private Sub grGastos_RowCountChanged(sender As Object, e As EventArgs) Handles grGastos.RowCountChanged
        If btnNuevo.Enabled = False Then
            CalcularGastos()
        End If
    End Sub

    Private Sub grdetalle_RowCountChanged(sender As Object, e As EventArgs) Handles grdetalle.RowCountChanged
        If btnNuevo.Enabled = False Then
            CalcularGastos()
        End If
    End Sub

    Private Sub cbConcepto_ValueChanged(sender As Object, e As EventArgs) Handles cbConcepto.ValueChanged
        If cbConcepto.SelectedIndex < 0 And cbConcepto.Text <> String.Empty Then
            btConcepto.Visible = True
        Else
            btConcepto.Visible = False
        End If
    End Sub

    Private Sub btConcepto_Click(sender As Object, e As EventArgs) Handles btConcepto.Click
        Dim numi As String = ""
        If L_prLibreriaGrabar(numi, "10", "1", cbConcepto.Text, "") Then
            _prCargarComboConcepto(cbConcepto)
            cbConcepto.SelectedIndex = CType(cbConcepto.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub grGastos_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grGastos.EditingCell
        e.Cancel = True
    End Sub

    Private Sub _prEliminarCosto()
        Dim cod As Integer = 1
        Dim num As Integer = CInt(grGastos.GetValue("codigo")) - 1
        CType(grGastos.DataSource, DataTable).Rows(num).Delete()
        'CType(grGastos.DataSource, DataTable).AcceptChanges()

        For Each dr As DataRow In CType(grGastos.DataSource, DataTable).Rows
            If dr.RowState = DataRowState.Deleted Then
                'no procesa el registro eliminado
            Else
                dr.Item("codigo") = cod
                cod = cod + 1
            End If
        Next
    End Sub
    Private Sub grGastos_MouseClick(sender As Object, e As MouseEventArgs) Handles grGastos.MouseClick
        If btnNuevo.Enabled = False Then
            If (grGastos.RowCount >= 1) Then
                If (grGastos.CurrentColumn.Index = grGastos.RootTable.Columns("img").Index) Then
                    _prEliminarCosto()
                    grGastos.Refetch()
                    grGastos.Refresh()
                    CType(grGastos.DataSource, DataTable).AcceptChanges()
                End If
            End If
        End If
    End Sub
#End Region




End Class