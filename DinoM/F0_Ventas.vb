﻿Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports GMap.NET.MapProviders
Imports GMap.NET
Imports GMap.NET.WindowsForms.Markers
Imports GMap.NET.WindowsForms
Imports GMap.NET.WindowsForms.ToolTips
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing
Imports CrystalDecisions.Shared
Imports Facturacion

Imports Newtonsoft.Json
Imports DinoM.DBApi
Imports DinoM.RespTipoDoc1
Imports DinoM.EmisorResp1


Public Class F0_Ventas

#Region "Variables Globales"
    Dim _CodCliente As Integer = 0
    Dim _CodEmpleado As Integer = 0
    Dim OcultarFact As Integer = 0
    Dim _codeBar As Integer = 1
    Dim _dias As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim FilaSelectLote As DataRow = Nothing
    Dim Table_Producto As DataTable
    Dim G_Lote As Boolean = False '1=igual a mostrar las columnas de lote y fecha de Vencimiento
    Dim dtProductoGoblal As DataTable = Nothing

    Dim SucursalSeleccionada As Integer = 0
    Dim CategoriaPrecioSeleccionada As Integer = 0


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
#End Region

#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        MSuperTabControl.SelectedTabIndex = 0
        Me.WindowState = FormWindowState.Maximized

        _prValidarLote()
        _prCargarComboCliente(cbCliente)
        _prCargarComboLibreriaSucursal(cbSucursal)
        _prCargarComboLibreria(cbCambioDolar, 7, 1)
        cbCambioDolar.Value = 1
        _prCargarComboPrecio(cbPrecio)
        lbTipoMoneda.Visible = False
        swMoneda.Visible = False
        P_prCargarVariablesIndispensables()
        _prCargarVenta()
        _prInhabiliitar()

        grVentas.Focus()
        Me.Text = "DESPACHOS"
        Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prAsignarPermisos()
        P_prCargarParametro()
        _prValidadFactura()
        _prCargarNameLabel()


        tbFechaVenta.IsInputReadOnly = True

    End Sub
    Public Sub _prCargarNameLabel()
        Dim dt As DataTable = L_fnNameLabel()
        If (dt.Rows.Count > 0) Then
            _codeBar = 1 'dt.Rows(0).Item("codeBar")
        End If
    End Sub
    Sub _prValidadFactura()
        'If (OcultarFact = 1) Then
        '    GroupPanelFactura2.Visible = False
        '    GroupPanelFactura.Visible = False
        'Else
        '    GroupPanelFactura2.Visible = True
        '    GroupPanelFactura.Visible = True
        'End If

    End Sub
    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneral(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 70
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 200
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Public Sub _prValidarLote()
        Dim dt As DataTable = L_fnPorcUtilidad()
        If (dt.Rows.Count > 0) Then
            Dim lot As Integer = dt.Rows(0).Item("VerLote")
            OcultarFact = dt.Rows(0).Item("VerFactManual")
            If (lot = 1) Then
                G_Lote = True
            Else
                G_Lote = False
            End If

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

    Private Sub _prCargarComboPrecio(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarPrecios()
        dt.Rows.Add(50, "PRECIO VENTA MAYORISTA")




        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ygnumi").Width = 60
            .DropDownList.Columns("ygnumi").Caption = "COD"
            .DropDownList.Columns.Add("ygdesc").Width = 500
            .DropDownList.Columns("ygdesc").Caption = "Precios"
            .ValueMember = "ygnumi"
            .DisplayMember = "ygdesc"
            .DataSource = dt
            .Refresh()
        End With
        If (dt.Rows.Count > 0) Then
            mCombo.SelectedIndex = 0
        End If
    End Sub


    Private Sub _prCargarComboCliente(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarClientes()




        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ydnumi").Width = 60
            .DropDownList.Columns("ydnumi").Caption = "COD"
            .DropDownList.Columns.Add("yddesc").Width = 500
            .DropDownList.Columns("yddesc").Caption = "CLIENTES"
            .ValueMember = "ydnumi"
            .DisplayMember = "yddesc"
            .DataSource = dt
            .Refresh()
        End With
        If (dt.Rows.Count > 0) Then
            mCombo.SelectedItem = 0
        End If
    End Sub



    Private Sub _prCargarComboPrecioLimpiar(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarPrecios()
        dt.Rows.Add(50, "PRECIO VENTA MAYORISTA")

        Dim dt2 As DataTable = dt.Copy
        dt2.Rows.Clear()

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            If (dt.Rows(i).Item("ygnumi") = 80 And gs_VentaFacturado = 1) Then
                dt2.ImportRow(dt.Rows(i))
            End If
            If (dt.Rows(i).Item("ygnumi") = 70 And gs_VentaNormal = 1) Then
                dt2.ImportRow(dt.Rows(i))
            End If
            If (dt.Rows(i).Item("ygnumi") = 1100 And gs_VentaMecanico = 1) Then
                dt2.ImportRow(dt.Rows(i))
            End If
            If (dt.Rows(i).Item("ygnumi") = 50 And gs_VentaMayorista = 1) Then
                dt2.ImportRow(dt.Rows(i))
            End If

        Next



        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ygnumi").Width = 60
            .DropDownList.Columns("ygnumi").Caption = "COD"
            .DropDownList.Columns.Add("ygdesc").Width = 500
            .DropDownList.Columns("ygdesc").Caption = "Precios"
            .ValueMember = "ygnumi"
            .DisplayMember = "ygdesc"
            .DataSource = dt2
            .Refresh()
        End With
        If (dt.Rows.Count > 0) Then
            mCombo.SelectedIndex = 0
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
    Private Sub _prInhabiliitar()
        SwProforma.IsReadOnly = True
        tbCodigo.ReadOnly = True
        tbCliente.ReadOnly = True
        tbVendedor.ReadOnly = True
        tbObservacion.ReadOnly = True
        'tbFechaVenta.IsInputReadOnly = True
        tbFechaVenc.IsInputReadOnly = True
        swMoneda.IsReadOnly = True
        swTipoVenta.IsReadOnly = True
        btnSearchCliente.Visible = False
        'Datos facturacion
        tbNroAutoriz.ReadOnly = True
        tbNroFactura.ReadOnly = True
        tbCodigoControl.ReadOnly = True
        dtiFechaFactura.IsInputReadOnly = True
        dtiFechaFactura.ButtonDropDown.Enabled = False
        btnAgregar.Visible = False
        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True
        btnActualizar.Visible = True
        tbSubTotal.IsInputReadOnly = True
        tbIce.IsInputReadOnly = True
        tbtotal.IsInputReadOnly = True
        tbEnvio.IsInputReadOnly = True
        btnAdicionarCliente.Visible = False
        tbMontoBs.IsInputReadOnly = True
        tbMontoDolar.IsInputReadOnly = True
        tbMontoTarej.IsInputReadOnly = True
        cbCliente.ReadOnly = True
        grVentas.Enabled = True
        PanelNavegacion.Enabled = True
        grdetalle.RootTable.Columns("img").Visible = False
        grdetalle.RootTable.Columns("imgAdd").Visible = False
        If (GPanelProductos.Visible = True) Then
            _DesHabilitarProductos()
        End If

        TbNit.ReadOnly = True
        TbNombre1.ReadOnly = True
        TbNombre2.ReadOnly = True
        cbSucursal.ReadOnly = True
        FilaSelectLote = Nothing

        lbMDescuento.Visible = True
        lbPDescuento.Visible = True
        tbMdesc.Visible = True
        tbPdesc.Visible = True

        If btnGrabar.Enabled = True Then
            btnCobrar.Enabled = False
        Else
            btnCobrar.Enabled = True
        End If
    End Sub
    Private Sub _prhabilitar()
        SwProforma.IsReadOnly = False
        grVentas.Enabled = False
        tbCodigo.ReadOnly = False
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False
        tbObservacion.ReadOnly = False
        'tbFechaVenta.IsInputReadOnly = False
        cbCliente.ReadOnly = False
        tbFechaVenc.IsInputReadOnly = False
        swMoneda.IsReadOnly = False
        swTipoVenta.IsReadOnly = False
        btnGrabar.Enabled = True
        btnSearchCliente.Visible = False
        TbNit.ReadOnly = False
        TbNombre1.ReadOnly = False
        TbNombre2.ReadOnly = False
        btnAgregar.Visible = True
        'Datos facturacion
        tbNroAutoriz.ReadOnly = False
        tbNroFactura.ReadOnly = False
        tbCodigoControl.ReadOnly = False
        dtiFechaFactura.IsInputReadOnly = False
        tbEnvio.IsInputReadOnly = False
        btnAdicionarCliente.Visible = True
        tbMontoBs.IsInputReadOnly = False
        tbMontoDolar.IsInputReadOnly = False
        'tbMontoTarej.IsInputReadOnly = False

        If (tbCodigo.Text.Length > 0) Then
            cbSucursal.ReadOnly = True
        Else
            'cbSucursal.ReadOnly = False

        End If
        If (gi_DescuentoGeneral = 1) Then
            lbMDescuento.Visible = True
            lbPDescuento.Visible = True
            tbMdesc.Visible = True
            tbPdesc.Visible = True
        Else
            lbMDescuento.Visible = False
            lbPDescuento.Visible = False
            tbMdesc.Visible = False
            tbPdesc.Visible = False
        End If
        btnActualizar.Visible = False
        _prCargarComboPrecioLimpiar(cbPrecio)
    End Sub
    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarVenta()
        If grVentas.RowCount > 0 Then
            _Mpos = 0
            grVentas.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _Limpiar()
        tbProforma.Clear()
        tbCodigo.Clear()
        tbCliente.Clear()
        tbVendedor.Clear()
        tbObservacion.Clear()
        swMoneda.Value = True
        swTipoVenta.Value = True
        _CodCliente = 0
        _CodEmpleado = 0
        tbFechaVenta.Value = Now.Date
        tbFechaVenc.Value = Now.Date
        tbFechaVenc.Visible = False
        lbCredito.Visible = False
        _prCargarDetalleVenta(-1)
        cbCliente.Text = ""
        MSuperTabControl.SelectedTabIndex = 0
        tbSubTotal.Value = 0
        tbPdesc.Value = 0
        tbMdesc.Value = 0
        tbIce.Value = 0
        tbtotal.Value = 0
        tbEnvio.Value = 0
        tbMontoBs.Value = 0
        tbMontoDolar.Value = 0
        tbMontoTarej.Value = 0
        txtCambio1.Text = "0.00"
        txtMontoPagado1.Text = "0.00"
        chbTarjeta.Checked = False
        cbCambioDolar.Value = 1

        With grdetalle.RootTable.Columns("img")
            .Width = 100
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("imgAdd")
            .Width = 40
            .Caption = "Nuevo"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        _prAddDetalleVenta()
        If (GPanelProductos.Visible = True) Then
            GPanelProductos.Visible = False
            PanelTotal.Visible = True
            PanelInferior.Visible = True
        End If
        tbCliente.Focus()

        TbNit.Clear()
        TbNombre1.Clear()
        TbNombre2.Clear()

        tbNroAutoriz.Clear()
        tbNroFactura.Clear()
        tbCodigoControl.Clear()
        dtiFechaFactura.Value = Now.Date
        If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = 0
        End If
        FilaSelectLote = Nothing
        SwProforma.Value = False
        tbCliente.Focus()
        Table_Producto = Nothing
        If (gi_NumiVenedor > 0) Then

            Dim dt As DataTable
            dt = L_fnListarEmpleado()
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                If (dt.Rows(i).Item("ydnumi") = gi_NumiVenedor) Then
                    _CodEmpleado = dt.Rows(i).Item("ydnumi")
                    tbVendedor.Text = dt.Rows(i).Item("yddesc")
                End If

            Next

        End If
        If (gi_userSuc > 0) Then
            Dim dt As DataTable = CType(cbSucursal.DataSource, DataTable)
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1

                If (dt.Rows(i).Item("aanumi") = gi_userSuc) Then
                    cbSucursal.SelectedIndex = i
                End If

            Next
        End If
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        '' grVentas.Row = _N
        '     a.tanumi ,a.taalm ,a.tafdoc ,a.taven ,vendedor .yddesc as vendedor ,a.tatven ,a.tafvcr ,a.taclpr,
        'cliente.yddesc as cliente ,a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total,taproforma,taCatPrecio

        With grVentas
            cbSucursal.Value = .GetValue("taalm")
            tbCodigo.Text = .GetValue("tanumi")
            cbPrecio.Value = .GetValue("taCatPrecio")
            tbFechaVenta.Value = .GetValue("tafdoc")
            _CodEmpleado = .GetValue("taven")
            tbVendedor.Text = .GetValue("vendedor")
            swTipoVenta.Value = .GetValue("tatven")
            cbCliente.Value = .GetValue("taclpr")
            _CodCliente = .GetValue("taclpr")
            tbCliente.Text = .GetValue("cliente")
            swMoneda.Value = .GetValue("tamon")
            tbObservacion.Text = .GetValue("taobs")
            tbEnvio.Value = .GetValue("taEnvio")
            If (.GetValue("taEstadoV") = 2) Then
                btnCobrar.Enabled = False
            Else
                btnCobrar.Enabled = True
            End If
            If swTipoVenta.Value = False Then
                btnCobrar.Enabled = False
            End If
            Dim proforma As Integer = IIf(IsDBNull(.GetValue("taproforma")), 0, .GetValue("taproforma"))
            If (proforma = 0) Then
                SwProforma.Value = False
                tbProforma.Clear()

            Else
                tbProforma.Text = proforma
                SwProforma.Value = True
            End If
            tbFechaVenc.Value = .GetValue("tafvcr")


            'If (gb_FacturaEmite) Then
            Dim dt As DataTable = L_fnObtenerTabla("TFV001", "fvanitcli, fvadescli1, fvadescli2, fvaautoriz, fvanfac, fvaccont, fvafec", "fvanumi=" + tbCodigo.Text.Trim)
            If (dt.Rows.Count = 1) Then
                TbNit.Text = dt.Rows(0).Item("fvanitcli").ToString
                TbNombre1.Text = dt.Rows(0).Item("fvadescli1").ToString
                TbNombre2.Text = dt.Rows(0).Item("fvadescli2").ToString

                tbNroAutoriz.Text = dt.Rows(0).Item("fvaautoriz").ToString
                tbNroFactura.Text = dt.Rows(0).Item("fvanfac").ToString
                tbCodigoControl.Text = dt.Rows(0).Item("fvaccont").ToString
                dtiFechaFactura.Value = dt.Rows(0).Item("fvafec")
            Else
                TbNit.Clear()
                TbNombre1.Clear()
                TbNombre2.Clear()

                tbNroAutoriz.Clear()
                tbNroFactura.Clear()
                tbCodigoControl.Clear()
                dtiFechaFactura.Value = "2000/01/01"
            End If
            'End If

            lbFecha.Text = CType(.GetValue("tafact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("tahact").ToString
            lbUsuario.Text = .GetValue("tauact").ToString

        End With

        _prCargarDetalleVenta(tbCodigo.Text)
        tbMdesc.Value = grVentas.GetValue("tadesc")
        tbIce.Value = grVentas.GetValue("taice")
        _prCalcularPrecioTotal()

        'Calcular montos
        Dim tMonto As DataTable = L_fnMostrarMontos(tbCodigo.Text)
        If tMonto.Rows.Count > 0 Then

            tbMontoTarej.Value = tMonto.Rows(0).Item("tgMontTare").ToString
            cbCambioDolar.Text = tMonto.Rows(0).Item("tgCambioDol").ToString
            tbMontoBs.Value = tMonto.Rows(0).Item("tgMontBs").ToString
            tbMontoDolar.Value = tMonto.Rows(0).Item("tgMontDol").ToString

            txtMontoPagado1.Text = tbMontoBs.Value + (tbMontoDolar.Value * IIf(cbCambioDolar.Text = "", 0, Convert.ToDecimal(cbCambioDolar.Text))) + tbMontoTarej.Value

            If Convert.ToDecimal(tbtotal.Text) <> 0 And Convert.ToDecimal(txtMontoPagado1.Text) >= Convert.ToDecimal(tbtotal.Text) Then
                txtCambio1.Text = Convert.ToDecimal(txtMontoPagado1.Text) - Convert.ToDecimal(tbtotal.Text)
            Else
                txtCambio1.Text = "0.00"
            End If

            Banco = tMonto.Rows(0).Item("tgBanco")
            Glosa = tMonto.Rows(0).Item("tgGlosa")
            'CostoEnvio = tMonto.Rows(0).Item("tgCostoEnvio")
        Else
            tbMontoTarej.Value = 0
            cbCambioDolar.Text = "0.00"
            tbMontoBs.Value = 0
            tbMontoDolar.Value = 0
            txtMontoPagado1.Text = "0.00"
            txtCambio1.Text = "0.00"
        End If
        If tbMontoTarej.Value = 0 Then
            chbTarjeta.Checked = False
        Else
            chbTarjeta.Checked = True
        End If

        'If grVentas.GetValue("taEstadoV") = 2 Then
        '    Panel9.Visible = True
        '    Panel8.Visible = True
        'Else
        '    Panel9.Visible = False
        '    Panel8.Visible = False
        'End If


        LblPaginacion.Text = Str(grVentas.Row + 1) + "/" + grVentas.RowCount.ToString

    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleVenta(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        '      a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot,a.tbdesc ,a.tbobs ,
        'a.tbfact ,a.tbhact ,a.tbuact

        With grdetalle.RootTable.Columns("tbnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("tbtv1numi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbty5prod")
            .Width = 90
            .Visible = False
        End With
        'If _codeBar = 2 Then
        '    With grdetalle.RootTable.Columns("yfcbarra")
        '        .Caption = "Cod.Barra"
        '        .Width = 100
        '        .Visible = True

        '    End With
        'Else
        '    With grdetalle.RootTable.Columns("yfcbarra")
        '        .Caption = "Cod.Barra"
        '        .Width = 100
        '        .Visible = False
        '    End With
        'End If

        With grdetalle.RootTable.Columns("ItemNuevo")
            .Caption = "Item Nuevo"
            .Width = 130
            .Visible = True
            .TextAlignment = 2
        End With
        With grdetalle.RootTable.Columns("ItemAntiguo")
            .Caption = "Item Antiguo"
            .Width = 130
            .Visible = True
            .TextAlignment = 2
        End With
        With grdetalle.RootTable.Columns("Item")
            .Caption = "Item"
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("yfcbarra")
            .Caption = "C.Barra"
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("CodigoFabrica")
            .Caption = "Cod.Fabrica"
            .Width = 120
            .MaxLines = 100
            .TextAlignment = 2
            '.WordWrap = True
            .Visible = True

        End With
        With grdetalle.RootTable.Columns("CodigoMarca")
            .Caption = "Cod.Marca"
            .Width = 120
            .MaxLines = 100
            .TextAlignment = 2
            '.WordWrap = True
            .Visible = True
            '.AllowSort = False
        End With
        With grdetalle.RootTable.Columns("Medida")
            .Caption = "Medida"
            .Width = 90
            .MaxLines = 100
            .CellStyle.LineAlignment = TextAlignment.Near
            .WordWrap = True
            .Visible = False
            .AllowSort = False
        End With

        With grdetalle.RootTable.Columns("CategoriaProducto")
            .Caption = "Cat.Producto"
            .Width = 100
            .MaxLines = 100
            .CellStyle.LineAlignment = TextAlignment.Near
            .WordWrap = True
            .Visible = False
            .AllowSort = False

        End With
        With grdetalle.RootTable.Columns("Marca")
            .Caption = "Marca"
            .Width = 100
            .MaxLines = 100
            .CellStyle.LineAlignment = TextAlignment.Near
            .WordWrap = True
            .Visible = False
            .AllowSort = False

        End With
        With grdetalle.RootTable.Columns("Procedencia")
            .Caption = "Procedencia"
            .Width = 100
            .MaxLines = 100
            .CellStyle.LineAlignment = TextAlignment.Near
            .WordWrap = True
            .Visible = False
            .AllowSort = False
        End With
        With grdetalle.RootTable.Columns("producto")
            .Caption = "Descripción"
            .Width = 400
            .MaxLines = 200
            .CellStyle.LineAlignment = TextAlignment.Near
            .WordWrap = True
            .Visible = True
            .AllowSort = False

        End With
        With grdetalle.RootTable.Columns("tbest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbPrecioReferencia")
            .Width = 85
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "Pre. Fact."
            .AllowSort = False
        End With
        With grdetalle.RootTable.Columns("tbPorcentajeReferencia")
            .Width = 60
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "% Dif."
            .AllowSort = False
        End With

        With grdetalle.RootTable.Columns("tbcmin")
            .Width = 75
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .Visible = True
            .FormatString = "0"
            .Caption = "Cantidad"
            .AllowSort = False
        End With
        With grdetalle.RootTable.Columns("tbumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("unidad")
            .Width = 70
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .Visible = True
            .Caption = "Abreviatura"
        End With
        With grdetalle.RootTable.Columns("tbpbas")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Precio U."
            .AllowSort = True
        End With

        With grdetalle.RootTable.Columns("tbptot")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .AllowSort = False
            .Caption = "Sub Total"
        End With
        With grdetalle.RootTable.Columns("tbporc")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.Desc(%)"
            .AllowSort = False
        End With
        With grdetalle.RootTable.Columns("tbdesc")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "M.Desc"
            .AllowSort = False
        End With
        With grdetalle.RootTable.Columns("tbtotdesc")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Total"
            .AllowSort = False
        End With
        With grdetalle.RootTable.Columns("tbobs")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbpcos")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbptot2")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbuact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("img")
            .Width = 100
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("imgAdd")
            .Width = 40
            .Caption = "Nuevo"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        If (G_Lote = True) Then
            With grdetalle.RootTable.Columns("tblote")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
                .Caption = "LOTE"
                .AllowSort = False
            End With
            With grdetalle.RootTable.Columns("tbfechaVenc")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
                .Caption = "FECHA VENC."
                .FormatString = "yyyy/MM/dd"
            End With

        Else
            With grdetalle.RootTable.Columns("tblote")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "LOTE"
            End With
            With grdetalle.RootTable.Columns("tbfechaVenc")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "FECHA VENC."
                .FormatString = "yyyy/MM/dd"
            End With
        End If
        With grdetalle.RootTable.Columns("stock")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .RecordNavigator = True

        End With
    End Sub

    Private Sub _prCargarVenta()
        Dim dt As New DataTable
        dt = L_fnGeneralVenta(gi_userSuc)
        grVentas.DataSource = dt
        grVentas.RetrieveStructure()
        grVentas.AlternatingColors = True
        '   a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total

        With grVentas.RootTable.Columns("tanumi")
            .Width = 50
            .Caption = "CODIGO"
            .Visible = True

        End With

        With grVentas.RootTable.Columns("taalm")
            .Width = 90
            .Visible = False
        End With

        With grVentas.RootTable.Columns("taCatPrecio")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("aabdes")
            .Width = 90
            .Visible = True
            .Caption = "ALMACEN"
        End With
        With grVentas.RootTable.Columns("taEnvio")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "ENVIO"
            .FormatString = "0.00"
        End With
        With grVentas.RootTable.Columns("taproforma")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tafdoc")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
        End With

        With grVentas.RootTable.Columns("taven")
            .Width = 160
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vendedor")
            .Width = 250
            .Visible = True
            .Caption = "VENDEDOR".ToUpper
        End With


        With grVentas.RootTable.Columns("tatven")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tventa")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "TIPO VENTA"
        End With
        With grVentas.RootTable.Columns("tafvcr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("taclpr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("cliente")
            .Width = 250
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "CLIENTE"
        End With

        With grVentas.RootTable.Columns("tamon")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("moneda")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "MONEDA"
        End With
        With grVentas.RootTable.Columns("taobs")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "OBSERVACION"
        End With
        With grVentas.RootTable.Columns("tadesc")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("taest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("taice")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tafact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tahact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tauact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("total")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grVentas.RootTable.Columns("taEstadoV")
            .Visible = False
        End With
        With grVentas.RootTable.Columns("EstadoV")
            .Width = 110
            .Visible = True
            .Caption = "ESTADO VENTA"
        End With
        With grVentas
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

        End With
        _prAplicarCondiccionJanusEstadoVenta()

        If (dt.Rows.Count <= 0) Then
            _prCargarDetalleVenta(-1)
        End If
    End Sub


    Public Sub actualizarSaldoSinLote(ByRef dt As DataTable)
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 

        '      a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
        'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
        'Cast (0 as decimal (18,2)) as stock
        Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim sum As Integer = 0
            Dim codProducto As Integer = dt.Rows(i).Item("Item")
            For j As Integer = 0 To grdetalle.RowCount - 1 Step 1
                grdetalle.Row = j
                Dim estado As Integer = grdetalle.GetValue("estado")
                If (estado = 0) Then
                    If (codProducto = grdetalle.GetValue("tbty5prod")) Then
                        sum = sum + grdetalle.GetValue("tbcmin")
                    End If
                End If
            Next
            dt.Rows(i).Item("stock") = dt.Rows(i).Item("stock") - sum
        Next

    End Sub

    Private Sub _prCargarProductos(_cliente As String, idCategoria As Integer)
        If (cbSucursal.SelectedIndex < 0) Then
            Return
        End If
        If (cbPrecio.SelectedIndex < 0) Then
            Return
        End If
        Dim dtname As DataTable = L_fnNameLabel()
        Dim dt As New DataTable


        If (G_Lote = True) Then
            dtProductoGoblal = L_fnListarProductos(cbSucursal.Value, cbPrecio.Value, idCategoria)  ''1=Almacen
            Table_Producto = dt.Copy

        Else
            If (IsNothing(dtProductoGoblal)) Then
                dtProductoGoblal = L_fnListarProductosSinLote(cbSucursal.Value, cbPrecio.Value, idCategoria)  ''1=Almacen
                Table_Producto = dt.Copy

                SucursalSeleccionada = cbSucursal.Value
                CategoriaPrecioSeleccionada = cbPrecio.Value


            Else
                If (cbSucursal.Value <> SucursalSeleccionada Or cbPrecio.Value <> CategoriaPrecioSeleccionada) Then
                    dtProductoGoblal = L_fnListarProductosSinLote(cbSucursal.Value, cbPrecio.Value, idCategoria)  ''1=Almacen
                    Table_Producto = dt.Copy

                    SucursalSeleccionada = cbSucursal.Value
                    CategoriaPrecioSeleccionada = cbPrecio.Value
                End If
            End If


        End If

        dt = dtProductoGoblal

        Dim dtVenta As DataTable = dtProductoGoblal.Copy
        dtVenta.Rows.Clear()
        Dim detalle As DataTable = CType(grdetalle.DataSource, DataTable)
        For i As Integer = 0 To detalle.Rows.Count - 1

            If (detalle.Rows(i).Item("estado") >= 0) Then
                Dim codigoProducto As Integer = detalle.Rows(i).Item("tbty5prod")

                For j As Integer = 0 To dt.Rows.Count - 1 Step 1

                    If (dt.Rows(j).Item("Item") = codigoProducto) Then
                        dt.Rows(j).Item("Cantidad") = detalle.Rows(i).Item("tbcmin")
                        'dt.Rows(j).Item("yhprecio") = detalle.Rows(i).Item("tbpbas")
                        dtVenta.ImportRow(dt.Rows(j))
                    End If

                Next


            End If


        Next
        Dim frm As F0_DetalleVenta
        frm = New F0_DetalleVenta(dtProductoGoblal, dtVenta, dtname, cbPrecio.Value)
        frm.almacenId = cbSucursal.Value
        frm.precio = cbPrecio.Value
        frm.cliente = _cliente
        frm.ShowDialog()
        Dim dtProd As DataTable = frm.dtDetalle
        dtProductoGoblal = frm.dtProductoAll
        For i As Integer = 0 To dtProd.Rows.Count - 1 Step 1

            InsertarProductosSinLote(dtProd, i)
        Next

        dtVenta.Clear()
    End Sub

    Private Sub _prCargarProductosCodBarras(_cliente As String, idCategoria As Integer)
        If (cbSucursal.SelectedIndex < 0) Then
            Return
        End If
        If (cbPrecio.SelectedIndex < 0) Then
            Return
        End If
        Dim dtname As DataTable = L_fnNameLabel()
        Dim dt As New DataTable


        If (G_Lote = True) Then
            dtProductoGoblal = L_fnListarProductos(cbSucursal.Value, cbPrecio.Value, idCategoria)  ''1=Almacen
            Table_Producto = dt.Copy

        Else
            If (IsNothing(dtProductoGoblal)) Then
                dtProductoGoblal = L_fnListarProductosSinLote(cbSucursal.Value, cbPrecio.Value, idCategoria)  ''1=Almacen
                Table_Producto = dtProductoGoblal.Copy

                SucursalSeleccionada = cbSucursal.Value
                CategoriaPrecioSeleccionada = cbPrecio.Value


            Else
                If (cbSucursal.Value <> SucursalSeleccionada Or cbPrecio.Value <> CategoriaPrecioSeleccionada) Then
                    dtProductoGoblal = L_fnListarProductosSinLote(cbSucursal.Value, cbPrecio.Value, idCategoria)  ''1=Almacen
                    Table_Producto = dtProductoGoblal.Copy

                    SucursalSeleccionada = cbSucursal.Value
                    CategoriaPrecioSeleccionada = cbPrecio.Value
                End If
            End If


        End If
    End Sub
    Public Sub _prAplicarCondiccionJanusSinLote()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grProductos.RootTable.Columns("stock"), ConditionOperator.Between, -9998 And 0)
        'fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.ForeColor = Color.Red    'Color.Tan
        grProductos.RootTable.FormatConditions.Add(fc)
        Dim fr As GridEXFormatCondition
        fr = New GridEXFormatCondition(grProductos.RootTable.Columns("stock"), ConditionOperator.Equal, -9999)
        fr.FormatStyle.ForeColor = Color.BlueViolet
        grProductos.RootTable.FormatConditions.Add(fr)
    End Sub
    Public Sub _prAplicarCondiccionJanusEstadoVenta()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grVentas.RootTable.Columns("taEstadoV"), ConditionOperator.Equal, 1)
        fc.FormatStyle.ForeColor = Color.Red
        grVentas.RootTable.FormatConditions.Add(fc)

    End Sub

    Public Sub actualizarSaldo(ByRef dt As DataTable, CodProducto As Integer)
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 

        '      a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
        'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
        'Cast (0 as decimal (18,2)) as stock
        Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim lote As String = dt.Rows(i).Item("iclot")
            Dim FechaVenc As Date = dt.Rows(i).Item("icfven")
            Dim sum As Integer = 0
            For j As Integer = 0 To _detalle.Rows.Count - 1
                Dim estado As Integer = _detalle.Rows(j).Item("estado")
                If (estado = 0) Then
                    If (lote = _detalle.Rows(j).Item("tblote") And
                        FechaVenc = _detalle.Rows(j).Item("tbfechaVenc") And CodProducto = _detalle.Rows(j).Item("tbty5prod")) Then
                        sum = sum + _detalle.Rows(j).Item("tbcmin")
                    End If
                End If
            Next
            dt.Rows(i).Item("iccven") = dt.Rows(i).Item("iccven") - sum
        Next

    End Sub

    Private Sub _prCargarLotesDeProductos(CodProducto As Integer, nameProducto As String)
        If (cbSucursal.SelectedIndex < 0) Then
            Return
        End If
        Dim dt As New DataTable
        GPanelProductos.Text = nameProducto
        dt = L_fnListarLotesPorProductoVenta(cbSucursal.Value, CodProducto)  ''1=Almacen
        actualizarSaldo(dt, CodProducto)
        grProductos.DataSource = dt
        grProductos.RetrieveStructure()
        grProductos.AlternatingColors = True
        With grProductos.RootTable.Columns("yfcdprod1")
            .Width = 150
            .Visible = False

        End With
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 
        With grProductos.RootTable.Columns("iclot")
            .Width = 150
            .Caption = "LOTE"
            .Visible = True

        End With
        With grProductos.RootTable.Columns("icfven")
            .Width = 160
            .Caption = "FECHA VENCIMIENTO"
            .FormatString = "yyyy/MM/dd"
            .Visible = True

        End With

        With grProductos.RootTable.Columns("iccven")
            .Width = 150
            .Visible = True
            .Caption = "Stock"
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
        End With


        With grProductos
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
            .VisualStyle = VisualStyle.Office2007
        End With
        _prAplicarCondiccionJanusLote()

    End Sub
    Public Sub _prAplicarCondiccionJanusLote()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grProductos.RootTable.Columns("iccven"), ConditionOperator.Equal, 0)
        fc.FormatStyle.BackColor = Color.Gold
        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.ForeColor = Color.White
        grProductos.RootTable.FormatConditions.Add(fc)

        Dim fc2 As GridEXFormatCondition
        fc2 = New GridEXFormatCondition(grProductos.RootTable.Columns("icfven"), ConditionOperator.LessThanOrEqualTo, Now.Date)
        fc2.FormatStyle.BackColor = Color.Red
        fc2.FormatStyle.FontBold = TriState.True
        fc2.FormatStyle.ForeColor = Color.White
        grProductos.RootTable.FormatConditions.Add(fc2)
    End Sub
    Private Sub _prAddDetalleVenta()
        '        a.tbnumi , a.tbtv1numi, a.tbty5prod, b.yfnumi As Item, b.yfcprod As CodigoFabrica, b.yfcdprod1 As producto, a.tbest, a.tbcmin, a.tbumin, Umin.ycdes3 As unidad,
        'a.tbPrecioReferencia , a.tbpbas, a.tbPorcentajeReferencia, a.tbptot, a.tbporc, a.tbdesc, a.tbtotdesc, a.tbobs,
        '        a.tbpcos, a.tblote, a.tbfechaVenc, a.tbptot2, a.tbfact, a.tbhact, a.tbuact, 1 As estado, Cast(null As Image) As img,
        '        (Sum(inv.iccven) + a.tbcmin) as stock

        '    a.tbnumi , a.tbtv1numi, a.tbty5prod, b.yfnumi As Item, b.yfcprod As CodigoFabrica, b.yfCodigoMarca As CodigoMarca,
        'b.yfcdprod2 as Medida, gr5.ycdes3 As CategoriaProducto, b.yfcdprod1 As producto, a.tbest, a.tbcmin, a.tbumin, Umin.ycdes3 As unidad,
        '       a.tbPrecioReferencia , a.tbpbas, a.tbPorcentajeReferencia, a.tbptot, a.tbporc, a.tbdesc, a.tbtotdesc, a.tbobs,
        '    a.tbpcos, a.tblote, a.tbfechaVenc, a.tbptot2, a.tbfact, a.tbhact, a.tbuact, 1 As estado, Cast(null As Image) As img, Cast(null As Image) As imgAdd,
        '    (Sum(inv.iccven) + a.tbcmin) as stock
        Dim Bin As New MemoryStream
        Dim Bin02 As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        Dim img02 As New Bitmap(My.Resources.add, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        img02.Save(Bin02, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add("", "", 0, 0, _fnSiguienteNumi() + 1, 0, 0, 0, "", "", "", "", "", "", 0, 0, 0, "", 0, 0, 0, 0, 0, 0, 0, "", 0, "20500101", CDate("2050/01/01"), 0, Now.Date, "", "", 0, Bin.GetBuffer, Bin02.GetBuffer, 0)
    End Sub

    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("tbnumi=MAX(tbnumi)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("tbnumi")
        End If
        Return 1
    End Function
    Public Function _fnAccesible()
        If btnNuevo.Enabled = False Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub _HabilitarProductos(idCategoria As Integer)
        'GPanelProductos.Visible = True
        'PanelTotal.Visible = False
        'PanelInferior.Visible = False
        _prCargarProductos(Str(_CodCliente), idCategoria)
        'grProductos.Focus()
        'grProductos.MoveTo(grProductos.FilterRow)
        'grProductos.Col = 2
        'tbProducto.Clear()
        'tbProducto.Focus()

        'GPanelProductos.Height = 350
    End Sub
    Private Sub _HabilitarFocoDetalle(fila As Integer, idCategoria As Integer)
        _prCargarProductos(Str(_CodCliente), idCategoria)
        grdetalle.Focus()
        grdetalle.Row = fila
        grdetalle.Col = 2
    End Sub
    Private Sub _DesHabilitarProductos()
        'GPanelProductos.Visible = False
        'PanelTotal.Visible = True
        'PanelInferior.Visible = True


        grdetalle.Select()
        grdetalle.Col = 5
        grdetalle.Row = grdetalle.RowCount - 1

    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

    Public Sub _fnObtenerFilaDetalleProducto(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grProductos.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grProductos.DataSource, DataTable).Rows(i).Item("Item")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

    Public Function _fnExisteProducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbty5prod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function

    Public Function _fnExisteProductoConLote(idprod As Integer, lote As String, fechaVenci As Date) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbty5prod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            '          a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
            'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
            'Cast (0 as decimal (18,2)) as stock
            Dim _LoteDetalle As String = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tblote")
            Dim _FechaVencDetalle As Date = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbfechaVenc")
            If (_idprod = idprod And estado >= 0 And lote = _LoteDetalle And fechaVenci = _FechaVencDetalle) Then

                Return True
            End If
        Next
        Return False
    End Function
    Public Sub P_PonerTotal(rowIndex As Integer)
        If (rowIndex < grdetalle.RowCount) Then

            Dim lin As Integer = grdetalle.GetValue("tbnumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, lin)
            Dim cant As Double = grdetalle.GetValue("tbcmin")
            Dim uni As Double = grdetalle.GetValue("tbpbas")
            Dim cos As Double = grdetalle.GetValue("tbpcos")
            Dim MontoDesc As Double = grdetalle.GetValue("tbdesc")
            Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
            If (pos >= 0) Then
                Dim TotalUnitario As Double = cant * uni
                Dim TotalCosto As Double = cant * cos
                'grDetalle.SetValue("lcmdes", montodesc)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = TotalUnitario
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = TotalUnitario - MontoDesc
                grdetalle.SetValue("tbptot", TotalUnitario)
                grdetalle.SetValue("tbtotdesc", TotalUnitario - MontoDesc)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = TotalCosto
                grdetalle.SetValue("tbptot2", TotalCosto)

                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
            End If
            _prCalcularPrecioTotal()
        End If



    End Sub
    Public Sub _prCalcularPrecioTotal()


        Dim montodesc As Double = tbMdesc.Value
        Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum))
        tbPdesc.Value = pordesc
        tbSubTotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) + tbEnvio.Value
        tbIce.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbptot2"), AggregateFunction.Sum) * (gi_ICE / 100)
        If (gb_FacturaIncluirICE = True) Then
            tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc + tbIce.Value + tbEnvio.Value
        Else
            tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc + tbEnvio.Value
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
                Dim lin As Integer = grdetalle.GetValue("tbnumi")
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
        grdetalle.Refetch()
        grdetalle.Refresh()

    End Sub
    Public Function _ValidarCampos() As Boolean
        If (cbCliente.Value <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Cliente con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbCliente.Focus()
            Return False

        End If
        If (_CodEmpleado <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Vendedor con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbVendedor.Focus()
            Return False
        End If
        If (cbSucursal.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione una Sucursal".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbSucursal.Focus()
            Return False
        End If
        If (tbFechaVenc.Value < tbFechaVenta.Value) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "La Fecha de Venc. del Crédito no puede ser menor a la Fecha de la Venta".ToUpper, img, 2500, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        End If
        'Validar datos de factura
        'If (TbNit.Text = String.Empty) Then
        '    Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
        '    ToastNotification.Show(Me, "Por Favor ponga el nit del cliente.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '    tbVendedor.Focus()
        '    Return False
        'End If

        'If (TbNombre1.Text = String.Empty) Then
        '    Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
        '    ToastNotification.Show(Me, "Por Favor ponga la razon social del cliente.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '    tbVendedor.Focus()
        '    Return False
        'End If

        If (grdetalle.RowCount = 1) Then
            grdetalle.Row = grdetalle.RowCount - 1
            If (grdetalle.GetValue("tbty5prod") = 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If
        End If

        For i = 0 To grdetalle.RowCount - 1 Step 1
            If CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbcmin") <= 0 Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "El producto: '".ToUpper + CType(grdetalle.DataSource, DataTable).Rows(i).Item("producto") + "' no tiene una cantidad valida o no cuenta con stock disponible".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If
        Next
        'If swTipoVenta.Value = True Then
        '    If tbMontoBs.Value = 0 And tbMontoDolar.Value = 0 And tbMontoTarej.Value = 0 Then
        '        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
        '        ToastNotification.Show(Me, "Debe llenar la forma de pago".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '        Return False
        '    End If
        'End If
        'If swTipoVenta.Value = True Then
        '    If (chbTarjeta.Checked = True) Then
        '        Return True
        '    Else
        '        If tbMontoBs.Value > 0 Then
        '            If (Convert.ToDecimal(tbMontoBs.Text) < Convert.ToDecimal(tbtotal.Text)) Then
        '                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
        '                ToastNotification.Show(Me, "El monto Pagado en Bs. debe ser mayor o igual al Total General".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '                Return False
        '            End If
        '        Else
        '            If tbMontoDolar.Value > 0 Then
        '                If (Convert.ToDecimal(tbMontoDolar.Text) < Convert.ToDecimal(tbtotal.Text)) Then
        '                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
        '                    ToastNotification.Show(Me, "El monto Pagado en $ debe ser mayor o igual al Total General".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '                    Return False
        '                End If
        '            End If
        '        End If
        '    End If

        'End If

        Return True
    End Function



    Public Function ValidarStock(ByRef mensaje As String) As Boolean
        Dim tabla As DataTable = CType(grdetalle.DataSource, DataTable)
        tabla.Columns.Remove("ItemNuevo")
        Dim dt As DataTable = L_fnListarStockProductos(tabla, cbSucursal.Value)

        Dim bandera As Boolean = True

        mensaje = "Ya No Existe Stock Para Los Productos: " + Chr(13) + Chr(10)


        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            Dim CodProducto As Integer = dt.Rows(i).Item("iccprod")
            If (dt.Rows(i).Item("TieneStock") = 0) Then
                bandera = False
                For j As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
                    If (CodProducto = CType(grdetalle.DataSource, DataTable).Rows(j).Item("tbty5prod") And CType(grdetalle.DataSource, DataTable).Rows(j).Item("estado") >= 0) Then
                        CType(grdetalle.DataSource, DataTable).Rows(j).Item("stock") = dt.Rows(i).Item("Stock")
                        mensaje = mensaje + dt.Rows(i).Item("Producto") + " Stock Actual= " + Str(dt.Rows(i).Item("Stock")) + Chr(13) + Chr(10)
                    End If

                Next
            End If


        Next


        Return bandera


    End Function

    Private Function verificarPrecioCosto() As Boolean
        For i = 0 To grdetalle.RowCount - 1 Step 1
            If CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbpcos") > CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbpbas") Then

                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
                ToastNotification.Show(Me, "Existen productos con precio unitario por debajo del precio de costo".ToUpper,
                                      img, 4500,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
                Return False
            End If
        Next
        Return True
    End Function
    Public Sub _GuardarNuevo()

        If verificarPrecioCosto() Then
        Else
            Exit Sub
        End If
        Dim mensaje As String = ""
        If (Not ValidarStock(mensaje)) Then
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, mensaje, img, 9000, eToastGlowColor.Red, eToastPosition.TopCenter)

            Return
        End If

        Dim numi As String = ""
        Dim tabla As DataTable = L_fnMostrarMontosTV0014(0)
        _prInsertarMontoNuevo(tabla)
        Dim tabla1 As DataTable = CType(grdetalle.DataSource, DataTable)
        'tabla1.Columns.Remove("ItemNuevo")
        Dim res As Boolean = L_fnGrabarVenta(numi, "", tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodEmpleado, IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True, Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")), _CodCliente, IIf(swMoneda.Value = True, 1, 0), tbObservacion.Text, tbMdesc.Value, tbIce.Value, tbtotal.Value, tabla1, cbSucursal.Value, IIf(SwProforma.Value = True, tbProforma.Text, 0), cbPrecio.Value, tabla, tbEnvio.Value)

        If res Then
            'res = P_fnGrabarFacturarTFV001(numi)

            'If (gb_FacturaEmite) Then
            '    P_fnGenerarFactura(numi)
            'End If

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                      img, 4500,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            If swTipoVenta.Value = False Then
                _prImiprimirNotaVenta(numi)
            End If

            '---IMPRIMIR NOTA DE VENTA

            _prCargarVenta()

            _Limpiar()
            Table_Producto = Nothing

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Venta no pudo ser insertado".ToUpper, img, 4500, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If

    End Sub
    Private Sub _prInsertarMontoNuevo(ByRef tabla As DataTable)
        tabla.Rows.Add(0, tbMontoBs.Value, tbMontoDolar.Value, tbMontoTarej.Value, cbCambioDolar.Text, 0)
    End Sub
    Private Sub _prModificarMontos(ByRef tabla As DataTable)
        tabla.Rows.Add(0, TotalBs, TotalSus, TotalTarjeta, TipoCambio, 2)
    End Sub
    Public Sub _prImiprimirNotaVenta(numi As String)
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "MENSAJE PRINCIPAL".ToUpper
        ef.Header = "¿desea imprimir la nota de venta?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            P_GenerarReporte(numi)
        End If
    End Sub
    Public Sub _prImiprimirFacturaPreimpresa(numi As String)
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "MENSAJE PRINCIPAL".ToUpper
        ef.Header = "¿desea imprimir la factura Preimpresa?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            P_GenerarReporteFactura(numi)
        End If
    End Sub
    Private Sub _prGuardarModificado()
        Dim tabla As DataTable = CType(grdetalle.DataSource, DataTable)
        tabla.Columns.Remove("ItemNuevo")
        Dim res As Boolean = L_fnModificarVenta(tbCodigo.Text, tbFechaVenta.Value.ToString("yyyy/MM/dd"),
                             _CodEmpleado, IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True,
                             Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")),
                             _CodCliente, IIf(swMoneda.Value = True, 1, 0), tbObservacion.Text, tbMdesc.Value,
                             tbIce.Value, tbtotal.Value, tabla, cbSucursal.Value,
                             IIf(SwProforma.Value = True, tbProforma.Text, 0), cbPrecio.Value, tbEnvio.Value)
        If res Then

            'If (gb_FacturaEmite) Then
            '    L_fnEliminarDatos("TFV001", "fvanumi=" + tbCodigo.Text.Trim)
            '    L_fnEliminarDatos("TFV0011", "fvbnumi=" + tbCodigo.Text.Trim)
            '    P_fnGenerarFactura(tbCodigo.Text.Trim)
            'End If
            _prImiprimirNotaVenta(tbCodigo.Text)

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
                                      img, 4500,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )


            _prCargarVenta()

            ' _prSalir()


        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Venta no pudo ser Modificada".ToUpper, img, 4500, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
    End Sub

    Private Sub _prGuardarCobro()
        Dim tabla As DataTable = L_fnMostrarMontosTV0014(0)
        _prModificarMontos(tabla)
        _prAgregarCobro(tbCodigo.Text, 1, tbObservacion.Text, TotalBs, TotalSus, TotalTarjeta, cambio, Banco, Glosa, gi_userSuc, TipoCambio)

        If TotalTarjeta > 0 Then
            L_prMovimientoGrabar("", tbFechaVenta.Value.ToString("dd/MM/yyyy"), 1, gi_userSuc, Banco, "", "VENTA", TotalTarjeta, Glosa)
        End If
        Dim res As Boolean = L_fnModificarCobro(tbCodigo.Text, TipoVenta, FechaVenc.ToString("yyyy/MM/dd"), tabla, Banco, Glosa, tbEnvio.Text)
        If res Then



            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "El Cobro de la Venta: ".ToUpper + tbCodigo.Text + " fue grabado con éxito.".ToUpper,
                                      img, 4500,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )


            _prCargarVenta()
            '_prSalir()


        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "LEl Cobro de la Venta no pudo ser grabada".ToUpper, img, 4500, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
    End Sub
    Private Sub _prSalir()

        If btnGrabar.Enabled = True Then
            Dim ef = New Efecto
            ef.tipo = 2
            ef.Header = "¿Los Datos No Se Guardaron Debe Hacer Clic en el Boton Grabar. En Caso de Que no Quiera Guardarlo Confirme Este Mensaje?".ToUpper
            ef.Context = "mensaje principal".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                _prInhabiliitar()
                If grVentas.RowCount > 0 Then

                    _prMostrarRegistro(0)

                End If
            End If
        Else
            If gs_ComVenPro! = 0 Then
                _modulo.Select()
                If (Not IsNothing(_tab)) Then
                    _tab.Close()
                End If
            Else
                gs_ComVenPro = 0
                Me.Close()
            End If


        End If



    End Sub
    Public Sub _prCargarIconELiminar()
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim Bin As New MemoryStream
            Dim Bin02 As New MemoryStream
            Dim img02 As New Bitmap(My.Resources.add, 28, 28)
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img02.Save(Bin02, Imaging.ImageFormat.Png)
            img.Save(Bin, Imaging.ImageFormat.Png)
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
            grdetalle.RootTable.Columns("img").Visible = True
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("imgAdd") = Bin02.GetBuffer
            grdetalle.RootTable.Columns("imgAdd").Visible = False
        Next

    End Sub
    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grVentas.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub
    Public Sub InsertarProductosSinLote(dt As DataTable, fila As Integer)

        '        a.tbnumi , a.tbtv1numi, a.tbty5prod, b.yfnumi As Item, b.yfcprod As CodigoFabrica, b.yfcdprod1 As producto, a.tbest, a.tbcmin, a.tbumin, Umin.ycdes3 As unidad,
        'a.tbPrecioReferencia , a.tbpbas, a.tbPorcentajeReferencia, a.tbptot, a.tbporc, a.tbdesc, a.tbtotdesc, a.tbobs,
        '        a.tbpcos, a.tblote, a.tbfechaVenc, a.tbptot2, a.tbfact, a.tbhact, a.tbuact, 1 As estado, Cast(null As Image) As img,
        '        (Sum(inv.iccven) + a.tbcmin) as stock

        'If (dt.Rows(fila).Item("Stock") <= 0) Then
        '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
        '    ToastNotification.Show(Me, "El producto no tiene stock disponible".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '    Return
        'End If

        Dim pos As Integer = -1
        grdetalle.Row = grdetalle.RowCount - 1
        If (grdetalle.GetValue("tbty5prod") <> 0) Then
            _prAddDetalleVenta()
            grdetalle.Row = grdetalle.RowCount - 1
        End If

        _fnObtenerFilaDetalle(pos, grdetalle.GetValue("tbnumi"))
        Dim existe As Boolean = _fnExisteProducto(dt.Rows(fila).Item("Item"))
        If ((pos >= 0) And (Not existe)) Then
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ItemNuevo") = dt.Rows(fila).Item("ItemNuevo")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ItemAntiguo") = dt.Rows(fila).Item("ItemAntiguo")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = dt.Rows(fila).Item("Item")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Item") = dt.Rows(fila).Item("Item")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = dt.Rows(fila).Item("yfcbarra")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("CodigoFabrica") = dt.Rows(fila).Item("CodigoFabrica")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("CodigoMarca") = dt.Rows(fila).Item("Marca")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Medida") = dt.Rows(fila).Item("Medida")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Marca") = dt.Rows(fila).Item("grupo1")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Procedencia") = dt.Rows(fila).Item("grupo2")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("CategoriaProducto") = dt.Rows(fila).Item("Categoria")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = dt.Rows(fila).Item("yfcdprod1")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = dt.Rows(fila).Item("yfumin")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = dt.Rows(fila).Item("grupo4")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = dt.Rows(fila).Item("yhprecio")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbPrecioReferencia") = dt.Rows(fila).Item("PrecioReferencia")




            Dim PrecioReferencia As Double = dt.Rows(fila).Item("PrecioReferencia")
            Dim monto As Double = dt.Rows(fila).Item("yhprecio")
            Dim Porcentaje As Double
            If (PrecioReferencia = 0) Then
                Porcentaje = 0
            Else
                'Porcentaje = 100 - ((monto * 100) / PrecioReferencia) NICO
                Porcentaje = (PrecioReferencia - monto)
            End If


            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbPorcentajeReferencia") = Porcentaje

            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = dt.Rows(fila).Item("yhprecio") * dt.Rows(fila).Item("Cantidad")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = dt.Rows(fila).Item("yhprecio") * dt.Rows(fila).Item("Cantidad")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = dt.Rows(fila).Item("Cantidad")
            If (gb_FacturaIncluirICE) Then
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = dt.Rows(fila).Item("pcos") * dt.Rows(fila).Item("Cantidad")
            Else
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = dt.Rows(fila).Item("pcos")
            End If
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = dt.Rows(fila).Item("pcos") * dt.Rows(fila).Item("Cantidad")

            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = dt.Rows(fila).Item("stock")
            _prCalcularPrecioTotal()


            '_DesHabilitarProductos()
            tbProducto.Focus()
        Else
            'If (existe) Then
            '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            '    ToastNotification.Show(Me, "El producto ya existe en el detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            '    grProductos.RemoveFilters()
            '    grProductos.Focus()
            '    grProductos.MoveTo(grProductos.FilterRow)
            '    grProductos.Col = 1
            'End If
        End If
    End Sub
    Public Sub InsertarProductosConLote()
        Dim pos As Integer = -1
        grdetalle.Row = grdetalle.RowCount - 1
        _fnObtenerFilaDetalleProducto(pos, grProductos.GetValue("Item"))
        Dim posProducto As Integer = grProductos.Row
        FilaSelectLote = CType(grProductos.DataSource, DataTable).Rows(pos)


        If (grProductos.GetValue("stock") > 0) Then
            _prCargarLotesDeProductos(grProductos.GetValue("Item"), grProductos.GetValue("yfcdprod1"))
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "El Producto: ".ToUpper + grProductos.GetValue("yfcdprod1") + " NO CUENTA CON STOCK DISPONIBLE", img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            FilaSelectLote = Nothing
        End If

    End Sub
    Private Function P_fnGenerarFactura(numi As String) As Boolean
        Dim res As Boolean = False
        res = P_fnGrabarFacturarTFV001(numi) ' Grabar en la TFV001
        If (res) Then
            If (P_fnValidarFactura()) Then
                'Validar para facturar
                P_prImprimirFacturar(numi, True, True) '_Codigo de a tabla TV001
            Else
                'Volver todo al estada anterior
                ToastNotification.Show(Me, "No es posible facturar, vuelva a ingresar a la mesa he intente nuevamente!!!".ToUpper,
                                       My.Resources.OK,
                                       5 * 1000,
                                       eToastGlowColor.Red,
                                       eToastPosition.MiddleCenter)
            End If

            If (Not TbNit.Text.Trim.Equals("0")) Then
                L_Grabar_Nit(TbNit.Text.Trim, TbNombre1.Text.Trim, TbNombre2.Text.Trim)
            Else
                L_Grabar_Nit(TbNit.Text, "S/N", "")
            End If
        End If

        Return res
    End Function

    Private Function P_fnGrabarFacturarTFV001(numi As String) As Boolean
        Dim a As Double = CDbl(tbtotal.Value + tbMdesc.Value)
        Dim b As Double = CDbl(IIf(IsDBNull(tbIce.Value), 0, tbIce.Value)) 'Ya esta calculado el 55% del ICE
        Dim c As Double = CDbl("0")
        Dim d As Double = CDbl("0")
        Dim e As Double = a - b - c - d
        Dim f As Double = CDbl(tbMdesc.Value)
        Dim g As Double = e - f
        Dim h As Double = g * (gi_IVA / 100)

        Dim res As Boolean = False
        'Grabado de Cabesera Factura
        L_Grabar_Factura(numi,
                        dtiFechaFactura.Value.ToString("yyyy/MM/dd"),
                        IIf(Val(tbNroFactura.Text) = 0, "0", tbNroFactura.Text),
                        IIf(Val(tbNroAutoriz.Text) = 0, "0", tbNroAutoriz.Text),
                        "1",
                        TbNit.Text.Trim,
                        "0",
                        TbNombre1.Text,
                        "",
                        CStr(Format(a, "####0.00")),
                        CStr(Format(b, "####0.00")),
                        CStr(Format(c, "####0.00")),
                        CStr(Format(d, "####0.00")),
                        CStr(Format(e, "####0.00")),
                        CStr(Format(f, "####0.00")),
                        CStr(Format(g, "####0.00")),
                        CStr(Format(h, "####0.00")),
                        "",
                        Now.Date.ToString("yyyy/MM/dd"),
                        "''",
                        "0",
                        numi)

        'Grabado de Detalle de Factura
        grProductos.Update()

        'Dim s As String = ""
        For Each fil As GridEXRow In grdetalle.GetRows
            If (Not fil.Cells("tbcmin").Value.ToString.Trim.Equals("") And
                Not fil.Cells("tbty5prod").Value.ToString.Trim.Equals("0")) Then
                's = fil.Cells("codP").Value
                's = fil.Cells("des").Value
                's = fil.Cells("can").Value
                's = fil.Cells("imp").Value
                L_Grabar_Factura_Detalle(numi.ToString,
                                        fil.Cells("tbty5prod").Value.ToString.Trim,
                                        fil.Cells("producto").Value.ToString.Trim,
                                        fil.Cells("tbcmin").Value.ToString.Trim,
                                        fil.Cells("tbpbas").Value.ToString.Trim,
                                        numi)
                res = True
            End If
        Next
        Return res
    End Function

    Private Function P_fnValidarFactura() As Boolean
        Return True
    End Function

    Private Sub P_prImprimirFacturar(numi As String, impFactura As Boolean, grabarPDF As Boolean)
        Dim _Fecha, _FechaAl As Date
        Dim _Ds, _Ds1, _Ds2, _Ds3 As New DataSet
        Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
            _Literal, _TotalDecimal, _TotalDecimal2 As String
        Dim I, _NumFac, _numidosif, _TotalCC As Integer
        Dim ice, _Desc, _TotalLi As Decimal
        Dim _VistaPrevia As Integer = 0


        _Desc = CDbl(tbMdesc.Value)
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        _Fecha = Now.Date '.ToString("dd/MM/yyyy")
        _Hora = Now.Hour.ToString + ":" + Now.Minute.ToString
        _Ds1 = L_Dosificacion("1", "1", _Fecha)

        _Ds = L_Reporte_Factura(numi, numi)
        _Autorizacion = _Ds1.Tables(0).Rows(0).Item("sbautoriz").ToString
        _NumFac = CInt(_Ds1.Tables(0).Rows(0).Item("sbnfac")) + 1
        _Nit = _Ds.Tables(0).Rows(0).Item("fvanitcli").ToString
        _Fechainv = Microsoft.VisualBasic.Right(_Fecha.ToShortDateString, 4) +
                    Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 5), 2) +
                    Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 2)
        _Total = _Ds.Tables(0).Rows(0).Item("fvatotal").ToString
        ice = _Ds.Tables(0).Rows(0).Item("fvaimpsi")
        _numidosif = _Ds1.Tables(0).Rows(0).Item("sbnumi").ToString
        _Key = _Ds1.Tables(0).Rows(0).Item("sbkey")
        _FechaAl = _Ds1.Tables(0).Rows(0).Item("sbfal")

        Dim maxNFac As Integer = L_fnObtenerMaxIdTabla("TFV001", "fvanfac", "fvaautoriz = " + _Autorizacion)
        _NumFac = maxNFac + 1

        _TotalCC = Math.Round(CDbl(_Total), MidpointRounding.AwayFromZero)
        _Cod_Control = ControlCode.generateControlCode(_Autorizacion, _NumFac, _Nit, _Fechainv, CStr(_TotalCC), _Key)

        'Literal 
        _TotalLi = _Ds.Tables(0).Rows(0).Item("fvastot") - _Ds.Tables(0).Rows(0).Item("fvadesc")
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        'Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_Total) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Ds2 = L_Reporte_Factura_Cia("1")
        QrFactura.Text = _Ds2.Tables(0).Rows(0).Item("scnit").ToString + "|" + Str(_NumFac).Trim + "|" + _Autorizacion + "|" + _Fecha + "|" + _Total + "|" + _TotalLi.ToString + "|" + _Cod_Control + "|" + TbNit.Text.Trim + "|" + ice.ToString + "|0|0|" + Str(_Desc).Trim

        L_Modificar_Factura("fvanumi = " + CStr(numi),
                            "",
                            CStr(_NumFac),
                            CStr(_Autorizacion),
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            _Cod_Control,
                            _FechaAl.ToString("yyyy/MM/dd"),
                            "",
                            "",
                            CStr(numi))

        _Ds = L_Reporte_Factura(numi, numi)

        For I = 0 To _Ds.Tables(0).Rows.Count - 1
            _Ds.Tables(0).Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
        Next
        If (impFactura) Then
            _Ds3 = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación
            If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                P_Global.Visualizador = New Visualizador 'Comentar
            End If
            Dim objrep As Object = Nothing
            If (gi_FacturaTipo = 1) Then
                'objrep = New R_FacturaG
            ElseIf (gi_FacturaTipo = 2) Then
                objrep = New R_FacturaCarta
                If (Not _Ds.Tables(0).Rows.Count = gi_FacturaCantidadItems) Then
                    For index = _Ds.Tables(0).Rows.Count To gi_FacturaCantidadItems - 1
                        'Insertamos la primera fila con el saldo Inicial
                        Dim f As DataRow = _Ds.Tables(0).NewRow
                        f.ItemArray() = _Ds.Tables(0).Rows(0).ItemArray
                        f.Item("fvbcant") = -1
                        _Ds.Tables(0).Rows.Add(f)
                    Next
                End If
            End If

            objrep.SetDataSource(_Ds.Tables(0))
            objrep.SetParameterValue("Hora", _Hora)
            objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
            objrep.SetParameterValue("Telefonopr", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
            objrep.SetParameterValue("Literal1", _Literal)
            objrep.SetParameterValue("Literal2", " ")
            objrep.SetParameterValue("Literal3", " ")
            objrep.SetParameterValue("NroFactura", _NumFac)
            objrep.SetParameterValue("NroAutoriz", _Autorizacion)
            objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
            objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
            objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scpai").ToString)
            objrep.SetParameterValue("ESFC", _Ds1.Tables(0).Rows(0).Item("sbsfc").ToString)
            objrep.SetParameterValue("ENit", _Ds2.Tables(0).Rows(0).Item("scnit").ToString)
            objrep.SetParameterValue("EActividad", _Ds2.Tables(0).Rows(0).Item("scact").ToString)
            objrep.SetParameterValue("ESms", "''" + _Ds1.Tables(0).Rows(0).Item("sbnota").ToString + "''")
            objrep.SetParameterValue("ESms2", "''" + _Ds1.Tables(0).Rows(0).Item("sbnota2").ToString + "''")
            objrep.SetParameterValue("EDuenho", _Ds2.Tables(0).Rows(0).Item("scnom").ToString) '?
            objrep.SetParameterValue("URLImageLogo", gs_CarpetaRaiz + "\LogoFactura.jpg")
            objrep.SetParameterValue("URLImageMarcaAgua", gs_CarpetaRaiz + "\MarcaAguaFactura.jpg")

            If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
                P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
                P_Global.Visualizador.ShowDialog() 'Comentar
                P_Global.Visualizador.BringToFront() 'Comentar
            End If

            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString '"EPSON TM-T20II Receipt5 (1)"
                objrep.PrintToPrinter(1, False, 1, 1)
            End If

            If (grabarPDF) Then
                'Copia de Factura en PDF
                If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
                    Directory.CreateDirectory(gs_CarpetaRaiz + "\Facturas")
                End If
                objrep.ExportToDisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\Facturas\" + CStr(_NumFac) + "_" + CStr(_Autorizacion) + ".pdf")

            End If
        End If
        L_Actualiza_Dosificacion(_numidosif, _NumFac, numi)
    End Sub

    Public Function P_fnImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New System.IO.MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function


    Private Function P_fnValidarFacturaVigente() As Boolean
        Dim est As String = L_fnObtenerDatoTabla("TFV001", "fvaest", "fvanumi=" + tbCodigo.Text.Trim)
        Return (est.Equals("True") Or est = String.Empty)
    End Function

    Private Sub P_prCargarVariablesIndispensables()
        If (gb_FacturaEmite) Then
            gi_IVA = CDbl(IIf(L_fnGetIVA().Rows(0).Item("scdebfis").ToString.Equals(""), gi_IVA, L_fnGetIVA().Rows(0).Item("scdebfis").ToString))
            gi_ICE = CDbl(IIf(L_fnGetICE().Rows(0).Item("scice").ToString.Equals(""), gi_ICE, L_fnGetICE().Rows(0).Item("scice").ToString))
        End If

    End Sub

    Private Sub P_prCargarParametro()
        'El sistema factura?
        GroupPanelFactura.Visible = False 'gb_FacturaEmite

        'Si factura, preguntar si, Se incluye el Importe ICE / IEHD / TASAS?
        If (gb_FacturaEmite) Then
            lbIce.Visible = gb_FacturaIncluirICE
            tbIce.Visible = gb_FacturaIncluirICE
        Else
            lbIce.Visible = False
            tbIce.Visible = False
        End If

    End Sub
    Private Sub P_GenerarReporte(numi As String)
        Dim dt As DataTable = L_fnVentaNotaDeVenta(numi)
        If (gb_DetalleProducto) Then
            ponerDescripcionProducto(dt)
        End If
        'Dim total As Decimal = dt.Compute("SUM(Total)", "")
        Dim _TotalLi As Decimal
        Dim _Literal, _TotalDecimal, _TotalDecimal2, moneda As String
        Dim fechaven As String = dt.Rows(0).Item("Fechaventa")

        _TotalLi = dt.Rows(0).Item("totalven") + dt.Rows(0).Item("taEnvio") - dt.Rows(0).Item("Descuento")
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        If swMoneda.Value = True Then
            moneda = "Bolivianos"
        Else
            moneda = "Dólares"
        End If

        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + "  " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 " + moneda


        Dim dt2 As DataTable = L_fnNameReporte()

        P_Global.Visualizador = New Visualizador
        Dim _FechaAct As String
        Dim _FechaPar As String
        Dim _Fecha() As String
        Dim _Meses() As String = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}
        _FechaAct = fechaven
        _Fecha = Split(_FechaAct, "-")
        _FechaPar = "Cochabamba, " + _Fecha(0).Trim + " De " + _Meses(_Fecha(1) - 1).Trim + " Del " + _Fecha(2).Trim
        If (G_Lote = False) Then
            Dim objrep As New R_NotaVenta_7_21X14
            '' GenerarNro(_dt)
            ''objrep.SetDataSource(Dt1Kardex)

            objrep.SetDataSource(dt)
            objrep.SetParameterValue("Literal1", _Literal)
            If swTipoVenta.Value = True Then
                objrep.SetParameterValue("ENombre", "Nota de Entrega Nro. " + numi)
            Else
                objrep.SetParameterValue("ENombre", "Nota de Crédito Nro. " + numi)
            End If
            objrep.SetParameterValue("ECiudadPais", _FechaPar)
            objrep.SetParameterValue("Sucursal", cbSucursal.Text)
            objrep.SetParameterValue("Observacion", tbObservacion.Text)
            objrep.SetParameterValue("logo", gb_UbiLogo)
            P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim objrep As New R_NotaDeVenta
            'Dim objrep As New R_NotaDeVentaSinLote
            'GenerarNro(_dt)
            'objrep.SetDataSource(Dt1Kardex)
            'totald = Math.Round(totald, 2)
            objrep.SetDataSource(dt)
            'objrep.SetParameterValue("TotalBs", li)
            'objrep.SetParameterValue("TotalDo", lid)
            'objrep.SetParameterValue("TotalDoN", totald)
            'objrep.SetParameterValue("P_Fecha", _FechaPar)
            'objrep.SetParameterValue("P_Empresa", ParEmp1)
            'objrep.SetParameterValue("P_Empresa1", ParEmp2)
            'objrep.SetParameterValue("P_Empresa2", ParEmp3)
            'objrep.SetParameterValue("P_Empresa3", ParEmp4)
            objrep.SetParameterValue("usuario", gs_user)
            objrep.SetParameterValue("estado", 1)
            P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        End If

    End Sub

    Private Sub ponerDescripcionProducto(ByRef dt As DataTable)
        For Each fila As DataRow In dt.Rows
            Dim numi As Integer = fila.Item("codProducto")
            Dim dtDP As DataTable = L_fnDetalleProducto(numi)
            Dim des As String = fila.Item("producto") + vbNewLine + vbNewLine
            For Each fila2 As DataRow In dtDP.Rows
                des = des + fila2.Item("yfadesc").ToString + vbNewLine
            Next
            fila.Item("producto") = des
        Next
    End Sub

    Private Sub P_GenerarReporteFactura(numi As String)
        Dim dt As DataTable = L_fnVentaFactura(numi)
        Dim total As Double = dt.Compute("SUM(Total)", "")

        Dim ParteEntera As Long
        Dim ParteDecimal As Double
        ParteEntera = Int(total)
        ParteDecimal = total - ParteEntera
        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " con " +
        IIf(ParteDecimal.ToString.Equals("0"), "00", ParteDecimal.ToString) + "/100 Bolivianos"





        Dim objrep As New R_FacturaFarmacia
        '' GenerarNro(_dt)
        ''objrep.SetDataSource(Dt1Kardex)
        'imprimir
        If PrintDialog1.ShowDialog = DialogResult.OK Then
            objrep.SetDataSource(dt)
            objrep.SetParameterValue("TotalEscrito", li)
            objrep.SetParameterValue("nit", TbNit.Text)
            objrep.SetParameterValue("Total", total)
            objrep.SetParameterValue("cliente", TbNombre1.Text + " " + TbNombre2.Text)
            objrep.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName

            objrep.PrintToPrinter(1, False, 1, 1)
            objrep.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        End If
        'objrep.SetDataSource(dt)
        'objrep.SetParameterValue("TotalEscrito", li)
        'objrep.SetParameterValue("nit", TbNit.Text)
        'objrep.SetParameterValue("Total", total)
        'P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        'P_Global.Visualizador.Show() 'Comentar
        'P_Global.Visualizador.BringToFront() 'Comentar



    End Sub

    Sub _prCargarProductoDeLaProforma(numiProforma As Integer)
        Dim dt As DataTable = L_fnListarProductoProforma(numiProforma)

        '        a.pbnumi ,a.pbtp1numi ,a.pbty5prod ,producto ,a.pbcmin ,a.pbumin ,Umin .ycdes3 as unidad,
        'a.pbpbas ,a.pbptot,a.pbporc,a.pbdesc ,a.pbtotdesc,
        'stock, pcosto
        If (dt.Rows.Count > 0) Then
            CType(grdetalle.DataSource, DataTable).Rows.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim ItemNuevo As String = dt.Rows(i).Item("yfCodAux1")
                Dim numiproducto As Integer = dt.Rows(i).Item("pfty5prod")
                Dim CodFab As String = dt.Rows(i).Item("yfcprod")
                Dim CodMar As String = dt.Rows(i).Item("yfCodigoMarca")
                Dim nameproducto As String = dt.Rows(i).Item("producto")
                Dim lote As String = ""
                Dim FechaVenc As Date = Now.Date
                Dim cant As Double = dt.Rows(i).Item("pfcmin")
                Dim iccven As Double = 0
                _prPedirLotesProducto(lote, FechaVenc, iccven, numiproducto, nameproducto, cant)
                _prAddDetalleVenta()
                grdetalle.Row = grdetalle.RowCount - 1
                'If (lote <> String.Empty) Then
                If (cant <= iccven) Then

                    grdetalle.SetValue("tbptot", dt.Rows(i).Item("pfptot"))
                    grdetalle.SetValue("tbtotdesc", dt.Rows(i).Item("pftotdesc"))
                    grdetalle.SetValue("tbdesc", dt.Rows(i).Item("pfdesc"))
                    grdetalle.SetValue("tbcmin", cant)
                    grdetalle.SetValue("tbptot2", dt.Rows(i).Item("pcosto") * cant)

                Else
                    Dim tot As Double = dt.Rows(i).Item("pfpbas") * iccven
                    grdetalle.SetValue("tbptot", tot)
                    grdetalle.SetValue("tbtotdesc", tot)
                    grdetalle.SetValue("tbdesc", 0)
                    grdetalle.SetValue("tbcmin", iccven)
                    grdetalle.SetValue("tbptot2", dt.Rows(i).Item("pcosto") * iccven)
                End If
                grdetalle.SetValue("ItemNuevo", dt.Rows(i).Item("yfCodAux1"))
                grdetalle.SetValue("ItemAntiguo", dt.Rows(i).Item("yfCodAux2"))
                grdetalle.SetValue("CodigoFabrica", dt.Rows(i).Item("yfcprod"))
                grdetalle.SetValue("CodigoMarca", dt.Rows(i).Item("yfCodigoMarca"))
                grdetalle.SetValue("tbty5prod", numiproducto)
                grdetalle.SetValue("producto", nameproducto)
                grdetalle.SetValue("tbumin", dt.Rows(i).Item("pfumin"))
                grdetalle.SetValue("unidad", dt.Rows(i).Item("unidad"))
                grdetalle.SetValue("tbpbas", dt.Rows(i).Item("pfpbas"))


                If (gb_FacturaIncluirICE) Then
                    grdetalle.SetValue("tbpcos", dt.Rows(i).Item("pcosto"))

                Else
                    grdetalle.SetValue("tbpcos", 0)
                End If

                grdetalle.SetValue("tblote", lote)
                grdetalle.SetValue("tbfechaVenc", FechaVenc)
                grdetalle.SetValue("stock", iccven)

                'End If

                'grdetalle.Refetch()
                'grdetalle.Refresh()


            Next

            grdetalle.Select()
            _prCalcularPrecioTotal()
        End If
    End Sub
    Public Sub _prPedirLotesProducto(ByRef lote As String, ByRef FechaVenc As Date, ByRef iccven As Double, CodProducto As Integer, nameProducto As String, cant As Integer)
        Dim dt As New DataTable
        dt = L_fnListarLotesPorProductoVenta(cbSucursal.Value, CodProducto)  ''1=Almacen
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 
        'Dim listEstCeldas As New List(Of Modelo.Celda)
        'listEstCeldas.Add(New Modelo.Celda("yfcdprod1,", False, "", 150))
        'listEstCeldas.Add(New Modelo.Celda("iclot", True, "LOTE", 150))
        'listEstCeldas.Add(New Modelo.Celda("icfven", True, "FECHA VENCIMIENTO", 180, "dd/MM/yyyy"))
        'listEstCeldas.Add(New Modelo.Celda("iccven", True, "Stock".ToUpper, 150, "0.00"))
        'listEstCeldas.Add(New Modelo.Celda("stockMinimo", False, "Stock Min.".ToUpper, 150, "0.00"))
        'listEstCeldas.Add(New Modelo.Celda("fechaVencimiento", False, "Fecha Ven.".ToUpper, 150, "0.00"))
        'Dim ef = New Efecto
        'ef.tipo = 3
        'ef.dt = dt
        'ef.SeleclCol = 2
        'ef.listEstCeldas = listEstCeldas
        'ef.alto = 50
        'ef.ancho = 350
        'ef.Context = "Producto ".ToUpper + nameProducto + "  cantidad=" + Str(cant)
        'ef.ShowDialog()
        'Dim bandera As Boolean = False
        'bandera = ef.band
        ''b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 
        'If (bandera = True) Then

        lote = dt.Rows(0).Item("iclot")
        FechaVenc = dt.Rows(0).Item("icfven")
        iccven = dt.Rows(0).Item("iccven")
        'End If


    End Sub


#End Region


    'Token SIFAC
    Public tokenObtenido
    Public dtDetalle As DataTable
    Public dt As DataTable

    Public CodProducto As String
    Public Cantidad As Integer
    Public PrecioU As Double
    Public PrecioTot As Double
    Public NombreProd As String
    Public NroFact As Integer
    Public NroTarjeta As String

    Public _Fecha As Date

    Public QrUrl As String
    Public FactUrl As String
    Public SegundaLeyenda As String
    Public TerceraLeyenda As String
    Public Cudf As String


#Region "Eventos Formulario"
    Private Sub F0_Ventas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.SuspendLayout()

        _IniciarTodo()
        btnNuevo.PerformClick()
        'Me.ResumeLayout()

        'If btnGrabar.Enabled = True Then
        '    btnCobrar.Enabled = False
        'Else
        '    btnCobrar.Enabled = True
        'End If

        If gi_userCobrar = 1 Then
            btnCobrar.Visible = True
        Else
            btnCobrar.Visible = False

        End If
        If (gs_ComVenPro > 0) Then
            If btnGrabar.Enabled = True Then
                Dim bandera As Boolean = True
                If (bandera = True) Then
                    _prInhabiliitar()
                    btnEliminar.Visible = False
                    btnNuevo.Visible = False
                    btnModificar.Visible = False
                    btnGrabar.Visible = False
                    btnActualizar.Visible = False
                    btnCobrar.Visible = False
                    btnVerPagos.Visible = True
                    If grVentas.RowCount > 0 Then
                        Dim pos As Integer
                        Dim cont As Integer = 0
                        For Each fila As GridEXRow In grVentas.GetRows()
                            If (CInt(fila.Cells("tanumi").Value.ToString) = gs_ComVenPro) Then
                                pos = fila.Position
                            Else
                                cont += 1
                            End If
                        Next
                        grVentas.Row = pos
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

        'btnNuevo.Enabled = False
        'btnModificar.Enabled = False
        'btnEliminar.Enabled = False
        'GPanelProductos.Visible = False
        '_prhabilitar()

        '_Limpiar()


        'If btnGrabar.Enabled = True Then
        '    btnCobrar.Enabled = False
        'Else
        '    btnCobrar.Enabled = True
        'End If


    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()

    End Sub



    Private Sub tbCliente_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCliente.KeyDown
        'If (_fnAccesible()) Then
        '    If e.KeyData = Keys.Control + Keys.Enter Then

        '        Dim dt As DataTable

        '        dt = L_fnListarClientes()
        '        '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
        '        ',a.ydtelf1 ,a.ydfnac 

        '        Dim listEstCeldas As New List(Of Modelo.Celda)
        '        listEstCeldas.Add(New Modelo.Celda("ydnumi,", False, "ID", 50))
        '        listEstCeldas.Add(New Modelo.Celda("ydcod", True, "ID", 50))
        '        listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", True, "RAZON SOCIAL", 180))
        '        listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
        '        listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
        '        listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
        '        listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
        '        listEstCeldas.Add(New Modelo.Celda("ydfnac", True, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
        '        listEstCeldas.Add(New Modelo.Celda("ydnumivend,", False, "ID", 50))
        '        listEstCeldas.Add(New Modelo.Celda("vendedor,", False, "ID", 50))
        '        listEstCeldas.Add(New Modelo.Celda("yddias", False, "CRED", 50))
        '        Dim ef = New Efecto
        '        ef.tipo = 3
        '        ef.dt = dt
        '        ef.SeleclCol = 2
        '        ef.listEstCeldas = listEstCeldas
        '        ef.alto = 50
        '        ef.ancho = 350
        '        ef.Context = "Seleccione Cliente".ToUpper
        '        ef.ShowDialog()
        '        Dim bandera As Boolean = False
        '        bandera = ef.band
        '        If (bandera = True) Then
        '            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

        '            _CodCliente = Row.Cells("ydnumi").Value
        '            tbCliente.Text = Row.Cells("ydrazonsocial").Value
        '            _dias = Row.Cells("yddias").Value

        '            Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
        '            If (numiVendedor > 0) Then
        '                tbVendedor.Text = Row.Cells("vendedor").Value
        '                _CodEmpleado = Row.Cells("ydnumivend").Value

        '                grdetalle.Select()
        '                Table_Producto = Nothing
        '            Else
        '                tbVendedor.Clear()
        '                _CodEmpleado = 0
        '                tbVendedor.Focus()
        '                Table_Producto = Nothing

        '            End If
        '        End If

        '    End If

        'End If




    End Sub
    Private Sub tbVendedor_KeyDown(sender As Object, e As KeyEventArgs) Handles tbVendedor.KeyDown
        'If (_fnAccesible()) Then
        '    If e.KeyData = Keys.Control + Keys.Enter Then

        '        Dim dt As DataTable

        '        dt = L_fnListarEmpleado()
        '        '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
        '        ',a.ydtelf1 ,a.ydfnac 

        '        Dim listEstCeldas As New List(Of Modelo.Celda)
        '        listEstCeldas.Add(New Modelo.Celda("ydnumi,", False, "ID", 50))
        '        listEstCeldas.Add(New Modelo.Celda("ydcod", True, "ID", 50))
        '        listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
        '        listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
        '        listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
        '        listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
        '        listEstCeldas.Add(New Modelo.Celda("ydfnac", True, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
        '        Dim ef = New Efecto
        '        ef.tipo = 3
        '        ef.dt = dt
        '        ef.SeleclCol = 1
        '        ef.listEstCeldas = listEstCeldas
        '        ef.alto = 50
        '        ef.ancho = 350
        '        ef.Context = "Seleccione Vendedor".ToUpper
        '        ef.ShowDialog()
        '        Dim bandera As Boolean = False
        '        bandera = ef.band
        '        If (bandera = True) Then
        '            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
        '            If (IsNothing(Row)) Then
        '                tbVendedor.Focus()
        '                Return

        '            End If
        '            _CodEmpleado = Row.Cells("ydnumi").Value
        '            tbVendedor.Text = Row.Cells("yddesc").Value
        '            grdetalle.Select()

        '        End If

        '    End If

        'End If
    End Sub
    Private Sub swTipoVenta_ValueChanged(sender As Object, e As EventArgs) Handles swTipoVenta.ValueChanged
        If (swTipoVenta.Value = False) Then
            lbCredito.Visible = True
            tbFechaVenc.Visible = True
            tbFechaVenc.Value = DateAdd(DateInterval.Day, _dias, Now.Date)
            ''Deshabilitar formas de pago
            tbMontoBs.IsInputReadOnly = True
            tbMontoDolar.IsInputReadOnly = True
            tbMontoTarej.IsInputReadOnly = True
            chbTarjeta.Enabled = False
            cbCambioDolar.Enabled = False
        Else
            lbCredito.Visible = False
            tbFechaVenc.Visible = False
            ''Habilitar formas de pago
            tbMontoBs.IsInputReadOnly = False
            tbMontoDolar.IsInputReadOnly = False
            tbMontoTarej.IsInputReadOnly = False
            chbTarjeta.Enabled = True
            cbCambioDolar.Enabled = True
        End If
    End Sub
    ''VVV PERMITE MODIFICAR EN EL DATAGRIV  NICP
    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        If (_fnAccesible()) Then
            'Habilitar solo las columnas de Precio, %, Monto y Observación
            'If (e.Column.Index = grdetalle.RootTable.Columns("yfcbarra").Index Or
            If (gs_PuedeModificarPrecio = 1 And e.Column.Index = grdetalle.RootTable.Columns("tbpbas").Index) Then
                e.Cancel = False

            Else
                If (e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index Or
              e.Column.Index = grdetalle.RootTable.Columns("tbporc").Index Or
              e.Column.Index = grdetalle.RootTable.Columns("tbdesc").Index Or
              e.Column.Index = grdetalle.RootTable.Columns("tbdesc").Index Or
              e.Column.Index = grdetalle.RootTable.Columns("tbpbas").Index Or
                    e.Column.Index = grdetalle.RootTable.Columns("yfcbarra").Index) Then
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
            If (_CodCliente <= 0) Then
                ToastNotification.Show(Me, "           Antes de Continuar Por favor Seleccione un Cliente!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                cbCliente.Focus()

                Return
            End If
            If (_CodEmpleado <= 0) Then


                ToastNotification.Show(Me, "           Antes de Continuar Por favor Seleccione un Vendedor!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                tbVendedor.Focus()
                Return

            End If

            grdetalle.Select()
            If _codeBar = 1 Then
                If gb_CodigoBarra Then
                    grdetalle.Col = 4
                    grdetalle.Row = 0
                Else
                    grdetalle.Col = 5
                    grdetalle.Row = 0
                End If
            End If
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

            If (grdetalle.Col = grdetalle.RootTable.Columns("tbcmin").Index) Then
                If (grdetalle.GetValue("producto") <> String.Empty) Then

                    SeleccionarCategoria(True)

                Else
                    ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
            If (grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
                If (grdetalle.GetValue("producto") <> String.Empty) Then
                    SeleccionarCategoria(True)
                Else
                    ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
            'If (grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index) Then
            '    If (grdetalle.GetValue("yfcbarra").ToString().Trim() <> String.Empty) Then
            '        cargarProductos()
            '        If (grdetalle.Row = grdetalle.RowCount - 1) Then
            '            If (existeProducto(grdetalle.GetValue("yfcbarra").ToString)) Then
            '                If (Not verificarExistenciaUnica(grdetalle.GetValue("yfcbarra").ToString)) Then
            '                    ponerProducto(grdetalle.GetValue("yfcbarra").ToString)
            '                    _prAddDetalleVenta()
            '                Else
            '                    sumarCantidad(grdetalle.GetValue("yfcbarra").ToString)
            '                End If
            '            Else
            '                grdetalle.DataChanged = False
            '                ToastNotification.Show(Me, "El código de barra del producto no existe", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
            '            End If
            '        Else
            '            grdetalle.DataChanged = False
            '            grdetalle.Row = grdetalle.RowCount - 1
            '            grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index
            '            ToastNotification.Show(Me, "El cursor debe situarse en la ultima fila", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
            '        End If
            '    Else
            '        ToastNotification.Show(Me, "El código de barra no puede quedar vacio", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
            '    End If

            'End If
            'opcion para cargar la grilla con el codigo de barra
            'If (grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index) Then

            '    If (grdetalle.GetValue("yfcbarra") <> String.Empty) Then
            '        _buscarRegistro(grdetalle.GetValue("yfcbarra"))


            '        '_prAddDetalleVenta()
            '        '_HabilitarProductos()
            '        ' MsgBox("hola de la grilla" + grdetalle.GetValue("yfcbarra") + t.Container.ToString)
            '        'ojo
            '    Else
            '        ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
            '    End If

            'End If

            If (grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index) Then
                _prCargarProductosCodBarras(Str(_CodCliente), 0)
                Dim codigoBarras = grdetalle.GetValue("yfcbarra").ToString
                If (existeProducto(grdetalle.GetValue("yfcbarra").ToString)) Then
                    If (Not verificarExistenciaUnica(grdetalle.GetValue("yfcbarra").ToString)) Then
                        Dim resultado As Boolean = False
                        ponerProducto(grdetalle.GetValue("yfcbarra").ToString)
                        _prAddDetalleVenta()

                    Else
                        'If (grdetalle.GetValue("producto").ToString <> String.Empty) Then
                        sumarCantidad(grdetalle.GetValue("yfcbarra").ToString)
                        'Else
                        '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                        '    ToastNotification.Show(Me, "El Producto: NO CUENTA CON STOCK DISPONIBLE", img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                        '    FilaSelectLote = Nothing
                        'End If
                    End If
                Else
                    grdetalle.DataChanged = False
                    ToastNotification.Show(Me, "El código de barra del producto no existe", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If



            End If



salirIf:
        End If


        If (e.KeyData = Keys.Control + Keys.Enter And grdetalle.Row >= 0 And
            grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
            Dim indexfil As Integer = grdetalle.Row
            Dim indexcol As Integer = grdetalle.Col
            SeleccionarCategoria(True)

        End If
        If (e.KeyData = Keys.Escape And grdetalle.Row >= 0) Then

            _prEliminarFila()


        End If


    End Sub



    Private Function existeProducto(codigo As String) As Boolean
        Return (Table_Producto.Select("yfcbarra='" + codigo.Trim() + "'", "").Count > 0)
    End Function

    Private Function verificarExistenciaUnica(codigo As String) As Boolean
        Dim cont As Integer = 0
        For Each fila As GridEXRow In grdetalle.GetRows()
            If (fila.Cells("yfcbarra").Value.ToString.Trim = codigo.Trim) Then
                cont += 1
            End If
        Next
        Return (cont >= 1)
    End Function

    Private Sub ponerProducto(codigo As String)
        grdetalle.DataChanged = True
        CType(grdetalle.DataSource, DataTable).AcceptChanges()
        Dim fila As DataRow() = Table_Producto.Select("yfcbarra='" + codigo.Trim + "'", "")
        If (fila.Count > 0) Then
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, grdetalle.GetValue("tbnumi"))
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = fila(0).ItemArray(0)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Item") = fila(0).ItemArray(0)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = fila(0).ItemArray(1)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("CodigoFabrica") = fila(0).ItemArray(2)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("CodigoMarca") = fila(0).ItemArray(3)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Medida") = fila(0).ItemArray(4)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Marca") = fila(0).ItemArray(8)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Procedencia") = fila(0).ItemArray(10)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("CategoriaProducto") = fila(0).ItemArray(5)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = fila(0).ItemArray(6)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = fila(0).ItemArray(15)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = fila(0).ItemArray(16)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = fila(0).ItemArray(17)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbPrecioReferencia") = fila(0).ItemArray(20)


            Dim PrecioReferencia As Double = fila(0).ItemArray(20)
            Dim monto As Double = fila(0).ItemArray(17)
            Dim Porcentaje As Double
            If (PrecioReferencia = 0) Then
                Porcentaje = 0
            Else
                'Porcentaje = 100 - ((monto * 100) / PrecioReferencia)  NICO MODF
                Porcentaje = (PrecioReferencia - monto)
            End If


            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbPorcentajeReferencia") = Porcentaje

            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = fila(0).ItemArray(17) * fila(0).ItemArray(21)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = fila(0).ItemArray(17) * fila(0).ItemArray(21)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = fila(0).ItemArray(21)



            If (gb_FacturaIncluirICE) Then
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(18)
            Else
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = 0
            End If
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(18)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).ItemArray(18) * fila(0).ItemArray(21)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = fila(0).ItemArray(19)

            _prCalcularPrecioTotal()
        End If
    End Sub

    Private Sub sumarCantidad(codigo As String)
        Dim fila As DataRow() = CType(grdetalle.DataSource, DataTable).Select("yfcbarra='" + codigo.Trim + "'", "")
        If (fila.Count > 0) Then
            Dim pos1 As Integer = -1
            _fnObtenerFilaDetalle(pos1, fila(0).Item("tbnumi"))

            Dim cant As Integer = grdetalle.GetRow(pos1).Cells("tbcmin").Value + 1
            Dim stock As Integer = grdetalle.GetRow(pos1).Cells("stock").Value
            'If (cant > stock) Then
            Dim lin As Integer = grdetalle.GetRow(pos1).Cells("tbnumi").Value
            Dim pos2 As Integer = -1
            _fnObtenerFilaDetalle(pos2, lin)
            CType(grdetalle.DataSource, DataTable).Rows(pos2).Item("tbcmin") = cant
            CType(grdetalle.DataSource, DataTable).Rows(pos2).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos2).Item("tbpbas") * cant
            CType(grdetalle.DataSource, DataTable).Rows(pos2).Item("tbptot2") = grdetalle.GetRow(pos1).Cells("tbpcos").Value * cant
            CType(grdetalle.DataSource, DataTable).Rows(pos2).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos2).Item("tbpbas") * cant
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            'ToastNotification.Show(Me, "La cantidad de la venta no debe ser mayor al del stock" & vbCrLf &
            '        "Stock=" + Str(stock).ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            grdetalle.SetValue("yfcbarra", "")
            grdetalle.SetValue("tbcmin", 0)
            grdetalle.SetValue("tbptot", 0)
            grdetalle.SetValue("tbptot2", 0)
            grdetalle.DataChanged = True
            'grdetalle.Refetch()
            grdetalle.Refresh()
            '_prCalcularPrecioTotal()
            'Else
            '    If (cant = stock) Then
            '        'grdetalle.SelectedFormatStyle.ForeColor = Color.Blue
            '        'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle = New GridEXFormatStyle
            '        'grdetalle.CurrentRow.Cells(e.Column).FormatStyle.BackColor = Color.Pink
            '        'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.BackColor = Color.DodgerBlue
            '        'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.ForeColor = Color.White
            '        'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.FontBold = TriState.True
            '    End If
            'End If

            _prCalcularPrecioTotal()
        End If
    End Sub

    Private Sub _buscarRegistro(cbarra As String)
        Dim _t As DataTable
        _t = L_fnListarProductosC(cbarra)
        If _t.Rows.Count > 0 Then
            CType(grdetalle.DataSource, DataTable).Rows(0).Item("producto") = _t.Rows(0).Item("yfcdprod1")
            CType(grdetalle.DataSource, DataTable).Rows(0).Item("tbcmin") = 1
            CType(grdetalle.DataSource, DataTable).Rows(0).Item("unidad") = _t.Rows(0).Item("uni")

        Else
            MsgBox("Codigo de Producto No Exite")
        End If
        'CType(grdetalle.DataSource, DataTable).Rows(0).Item("tbpbas") =
        'CType(grdetalle.DataSource, DataTable).Rows(0).Item("tbumin") = 1
        'CType(grdetalle.DataSource, DataTable).Rows(0).Item("tbptot2") = grdetalle.GetValue("tbpcos") * 1
        'ojo 'Dim pos, lin As Integer
        'pos = grdetalle.Row
        'lin = grdetalle.Col

        'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
        'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")
        'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = grdetalle.GetValue("tbpcos") * 1


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

                If (IsNothing(FilaSelectLote)) Then
                    ''''''''''''''''''''''''
                    If (G_Lote = True) Then
                        InsertarProductosConLote()
                    Else
                        'InsertarProductosSinLote()
                    End If
                    '''''''''''''''
                Else

                    '_fnExisteProductoConLote()
                    Dim pos As Integer = -1
                    grdetalle.Row = grdetalle.RowCount - 1
                    _fnObtenerFilaDetalle(pos, grdetalle.GetValue("tbnumi"))
                    Dim numiProd = FilaSelectLote.Item("Item")
                    Dim lote As String = grProductos.GetValue("iclot")
                    Dim FechaVenc As Date = grProductos.GetValue("icfven")
                    If (Not _fnExisteProductoConLote(numiProd, lote, FechaVenc)) Then
                        'b.yfcdprod1, a.iclot, a.icfven, a.iccven
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = FilaSelectLote.Item("Item")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = FilaSelectLote.Item("yfcprod")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = FilaSelectLote.Item("yfcbarra")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = FilaSelectLote.Item("yfcdprod1")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = FilaSelectLote.Item("yfumin")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = FilaSelectLote.Item("UnidMin")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = FilaSelectLote.Item("yhprecio")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = FilaSelectLote.Item("yhprecio")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = FilaSelectLote.Item("yhprecio")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                        'If (gb_FacturaIncluirICE) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = FilaSelectLote.Item("pcos")
                        'Else
                        '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = 0
                        'End If
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = FilaSelectLote.Item("pcos")

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tblote") = grProductos.GetValue("iclot")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfechaVenc") = grProductos.GetValue("icfven")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("iccven")
                        _prCalcularPrecioTotal()
                        _DesHabilitarProductos()
                        FilaSelectLote = Nothing
                    Else
                        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                        ToastNotification.Show(Me, "El producto con el lote ya existe modifique su cantidad".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    End If



                End If

            End If
        End If
        If e.KeyData = Keys.Escape Then
            _DesHabilitarProductos()
            FilaSelectLote = Nothing
        End If
    End Sub
    Private Sub grdetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged


        If (e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index) Or (e.Column.Index = grdetalle.RootTable.Columns("tbpbas").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("tbcmin")) Or grdetalle.GetValue("tbcmin").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("tbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = 0

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos")
                _prCalcularPrecioTotal()
                'grdetalle.SetValue("tbcmin", 1)
                'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
            Else
                If (grdetalle.GetValue("tbcmin") > 0 And IsNumeric(grdetalle.GetValue("tbpbas"))) Then
                    Dim rowIndex As Integer = grdetalle.Row
                    Dim porcdesc As Double = grdetalle.GetValue("tbporc")
                    Dim montodesc As Double = ((grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin")) * (porcdesc / 100))
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                    grdetalle.SetValue("tbdesc", montodesc)
                    P_PonerTotal(rowIndex)


                    Dim PrecioReferencia As Double = grdetalle.GetValue("tbPrecioReferencia")
                    Dim monto As Double = grdetalle.GetValue("tbpbas")
                    Dim Porcentaje As Double
                    If (PrecioReferencia = 0) Then
                        Porcentaje = 0
                    Else
                        'Porcentaje = 100 - ((monto * 100) / PrecioReferencia) NICO
                        Porcentaje = (PrecioReferencia - monto)
                    End If

                    ''tbPorcentajeReferencia

                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbPorcentajeReferencia") = Porcentaje
                    grdetalle.SetValue("tbPorcentajeReferencia", Porcentaje)
                Else
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = 0
                    _prCalcularPrecioTotal()
                    'grdetalle.SetValue("tbcmin", 1)
                    'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                End If
            End If
        End If
        '''''''''''''''''''''PORCENTAJE DE DESCUENTO '''''''''''''''''''''
        If (e.Column.Index = grdetalle.RootTable.Columns("tbporc").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("tbporc")) Or grdetalle.GetValue("tbporc").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("tbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                'grdetalle.SetValue("tbcmin", 1)
                'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
            Else
                If (grdetalle.GetValue("tbporc") > 0 And grdetalle.GetValue("tbporc") <= gs_DescuentoProducto) Then

                    Dim porcdesc As Double = grdetalle.GetValue("tbporc")
                    Dim montodesc As Double = (grdetalle.GetValue("tbptot") * (porcdesc / 100))
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                    grdetalle.SetValue("tbdesc", montodesc)

                    Dim rowIndex As Integer = grdetalle.Row
                    P_PonerTotal(rowIndex)

                Else
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                    grdetalle.SetValue("tbporc", 0)
                    grdetalle.SetValue("tbdesc", 0)
                    grdetalle.SetValue("tbtotdesc", grdetalle.GetValue("tbptot"))
                    _prCalcularPrecioTotal()

                    ToastNotification.Show(Me, "El Porcentaje de Descuento es Mayor al Asignado al Usuario = " + Str(gs_DescuentoProducto), My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.BottomLeft)
                    'grdetalle.SetValue("tbcmin", 1)
                    'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                End If
            End If
        End If


        '''''''''''''''''''''MONTO DE DESCUENTO '''''''''''''''''''''
        If (e.Column.Index = grdetalle.RootTable.Columns("tbdesc").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("tbdesc")) Or grdetalle.GetValue("tbdesc").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("tbnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                'grdetalle.SetValue("tbcmin", 1)
                'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
            Else
                Dim montodesc As Double = grdetalle.GetValue("tbdesc")
                Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetValue("tbptot"))

                If (grdetalle.GetValue("tbdesc") > 0 And pordesc <= gs_DescuentoProducto) Then



                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = pordesc

                    grdetalle.SetValue("tbporc", pordesc)
                    Dim rowIndex As Integer = grdetalle.Row
                    P_PonerTotal(rowIndex)

                Else
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                    grdetalle.SetValue("tbporc", 0)
                    grdetalle.SetValue("tbdesc", 0)
                    grdetalle.SetValue("tbtotdesc", grdetalle.GetValue("tbptot"))
                    _prCalcularPrecioTotal()
                    Dim Monto As Double = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") * (gs_DescuentoProducto / 100)
                    ToastNotification.Show(Me, "El Monto de Descuento Es Mayor al Autorizado = " + Str(Monto), My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.BottomLeft)
                    'grdetalle.SetValue("tbcmin", 1)
                    'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                End If
            End If
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
                    Dim montodesc As Double = (grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) * (porcdesc / 100))
                    tbMdesc.Value = montodesc

                    tbIce.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbptot2"), AggregateFunction.Sum) * (gi_ICE / 100)

                    If (gb_FacturaIncluirICE = True) Then
                        tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc + tbIce.Value
                    Else
                        tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc
                    End If
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
                    Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum))
                    tbPdesc.Value = pordesc
                    tbIce.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbptot2"), AggregateFunction.Sum) * (gi_ICE / 100)
                    If (gb_FacturaIncluirICE = True) Then
                        tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc + tbIce.Value
                    Else
                        tbtotal.Value = grdetalle.GetTotal(grdetalle.RootTable.Columns("tbtotdesc"), AggregateFunction.Sum) - montodesc
                    End If

                End If

            End If

            If (tbMdesc.Text = String.Empty) Then
                tbMdesc.Value = 0

            End If
        End If

    End Sub

    Private Sub grdetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellEdited
        If (e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("tbcmin")) Or grdetalle.GetValue("tbcmin").ToString = String.Empty) Then

                grdetalle.SetValue("tbcmin", 0)
                grdetalle.SetValue("tbptot", 0)
                grdetalle.SetValue("tbporc", 0)
                grdetalle.SetValue("tbdesc", 0)
                grdetalle.SetValue("tbtotdesc", 0)


            Else
                If (grdetalle.GetValue("tbcmin") > 0) Then

                    Dim cant As Integer = grdetalle.GetValue("tbcmin")
                    Dim stock As Integer = grdetalle.GetValue("stock")
                    If (cant > stock) And stock <> -9999 Then
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = grdetalle.GetValue("tbpcos") * 1
                        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                        ToastNotification.Show(Me, "La cantidad de la venta no debe ser mayor al del stock" & vbCrLf &
                        "Stock=" + Str(stock).ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                        grdetalle.SetValue("tbcmin", 1)
                        grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
                        grdetalle.SetValue("tbptot2", grdetalle.GetValue("tbpcos") * 1)

                        _prCalcularPrecioTotal()
                    Else
                        If (cant = stock) Then


                            'grdetalle.SelectedFormatStyle.ForeColor = Color.Blue
                            'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle = New GridEXFormatStyle
                            'grdetalle.CurrentRow.Cells(e.Column).FormatStyle.BackColor = Color.Pink
                            'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.BackColor = Color.DodgerBlue
                            'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.ForeColor = Color.White
                            'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.FontBold = TriState.True
                        End If
                    End If

                Else

                    grdetalle.SetValue("tbcmin", 0)
                    grdetalle.SetValue("tbptot", 0)
                    grdetalle.SetValue("tbporc", 0)
                    grdetalle.SetValue("tbdesc", 0)
                    grdetalle.SetValue("tbtotdesc", 0)

                End If
            End If
        End If
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
            ' If (grdetalle.CurrentColumn.Index = grdetalle.RootTable.Columns("imgAdd").Index) Then
            'SeleccionarCategoria(True)
            ' End If
        Catch ex As Exception

        End Try


    End Sub
    Private Sub grdetalle_ColumnHeaderClick(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.ColumnHeaderClick

        Try
            grdetalle.Focus()

            grdetalle.Col = 1
        Catch ex As Exception

        End Try


    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If _ValidarCampos() = False Then
            Exit Sub
        End If
        If CDbl(tbPdesc.Text) > 5.0 Then
            Dim ef = New Efecto


            ef.tipo = 2
            ef.Context = "MENSAJE PRINCIPAL".ToUpper
            ef.Header = "¿Esta seguro que desea registrar la venta con un descuento mayor al 5%?".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                If (tbCodigo.Text = String.Empty) Then
                    _GuardarNuevo()
                Else
                    If (tbCodigo.Text <> String.Empty) Then
                        _prGuardarModificado()
                        ''    _prInhabiliitar() RODRIGO RLA

                    End If
                End If

            Else
                Return
            End If
        Else

            If (tbCodigo.Text = String.Empty) Then
                _GuardarNuevo()
            Else
                If (tbCodigo.Text <> String.Empty) Then
                    _prGuardarModificado()
                    ''    _prInhabiliitar() RODRIGO RLA

                End If
            End If

        End If

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If (grVentas.RowCount > 0) Then
            If (gb_FacturaEmite) Then
                If (Not P_fnValidarFacturaVigente()) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                    ToastNotification.Show(Me, "No se puede modificar la venta con codigo ".ToUpper + tbCodigo.Text + ", su factura esta anulada.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter)
                    Exit Sub
                End If
            End If

            _prhabilitar()
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True

            PanelNavegacion.Enabled = False
            _prCargarIconELiminar()
        End If
    End Sub
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        'If (gb_FacturaEmite) Then
        '    If (P_fnValidarFacturaVigente()) Then
        '        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

        '        ToastNotification.Show(Me, "No se puede eliminar la venta con codigo ".ToUpper + tbCodigo.Text + ", su factura esta vigente.".ToUpper,
        '                                  img, 2000,
        '                                  eToastGlowColor.Green,
        '                                  eToastPosition.TopCenter)
        '        Exit Sub
        '    End If
        'End If

        If (swTipoVenta.Value = False) Then
            Dim res1 As Boolean = L_fnVerificarPagos(tbCodigo.Text)
            If res1 Then
                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
                ToastNotification.Show(Me, "No se puede anular la venta con código ".ToUpper + tbCodigo.Text + ", porque tiene pagos realizados, por favor primero elimine los pagos correspondientes a esta venta".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                Exit Sub
            End If
        End If

        Dim res2 As Boolean = L_fnVerificarCierreCaja(tbCodigo.Text, "V")
        If res2 Then
            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

            ToastNotification.Show(Me, "No se puede anular la venta con código ".ToUpper + tbCodigo.Text + ", ya se hizo cierre de caja, por favor primero elimine cierre de caja".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
            Exit Sub
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
            Dim res As Boolean = L_fnEliminarVenta(tbCodigo.Text, mensajeError)
            If res Then


                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
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

    Private Sub grVentas_SelectionChanged(sender As Object, e As EventArgs) Handles grVentas.SelectionChanged
        If (grVentas.RowCount >= 0 And grVentas.Row >= 0) Then

            _prMostrarRegistro(grVentas.Row)
        End If


    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grVentas.Row
        If _pos < grVentas.RowCount - 1 And _pos >= 0 Then
            _pos = grVentas.Row + 1
            '' _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grVentas.Row
        If grVentas.RowCount > 0 Then
            _pos = grVentas.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grVentas.Row
        If _MPos > 0 And grVentas.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub
    Private Sub grVentas_KeyDown(sender As Object, e As KeyEventArgs) Handles grVentas.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grdetalle.Focus()

        End If
    End Sub

    Private Sub TbNit_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TbNit.Validating
        Dim nom1, nom2 As String
        nom1 = ""
        nom2 = ""
        If (TbNit.Text.Trim = String.Empty) Then
            TbNit.Text = "0"
        End If
        L_Validar_Nit(TbNit.Text.Trim, nom1, nom2)
        TbNombre1.Text = nom1
        TbNombre2.Text = nom2
    End Sub
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If (Not _fnAccesible()) Then
            P_GenerarReporte(tbCodigo.Text)

        End If
    End Sub

    Private Sub TbNit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TbNit.KeyPress
        g_prValidarTextBox(1, e)
    End Sub

    Private Sub swTipoVenta_KeyDown(sender As Object, e As KeyEventArgs) Handles swTipoVenta.KeyDown

    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        If (Not _fnAccesible()) Then
            P_GenerarReporte(tbCodigo.Text)

        End If
    End Sub

    Private Sub swTipoVenta_Leave(sender As Object, e As EventArgs) Handles swTipoVenta.Leave
        grdetalle.Select()
    End Sub

    Private Sub tbProforma_KeyDown(sender As Object, e As KeyEventArgs) Handles tbProforma.KeyDown
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable

                dt = L_fnListarProforma()
                '              a.panumi ,a.pafdoc ,a.paven ,vendedor .yddesc as vendedor,a.paclpr,
                'cliente.yddesc as cliente,a.patotal as total

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("panumi,", True, "NRO PROFORMA", 120))
                listEstCeldas.Add(New Modelo.Celda("pafdoc", True, "FECHA", 120, "dd/MM/yyyy"))
                listEstCeldas.Add(New Modelo.Celda("paven", False, "", 50))
                listEstCeldas.Add(New Modelo.Celda("vendedor", True, "VENDEDOR".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("paclpr", False, "", 50))
                listEstCeldas.Add(New Modelo.Celda("cliente", True, "CLIENTE", 220))
                listEstCeldas.Add(New Modelo.Celda("total", True, "TOTAL".ToUpper, 120, "0.00"))
                listEstCeldas.Add(New Modelo.Celda("paalm", False, "", 50))
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
                    _CodEmpleado = Row.Cells("peven").Value
                    _CodCliente = Row.Cells("peclpr").Value
                    'tbCliente.Text = Row.Cells("cliente").Value
                    cbCliente.Value = Row.Cells("peclpr").Value
                    tbVendedor.Text = Row.Cells("vendedor").Value
                    tbProforma.Text = Row.Cells("penumi").Value
                    cbSucursal.Value = Row.Cells("pealm").Value
                    _prCargarProductoDeLaProforma(Row.Cells("penumi").Value)

                End If

            End If

        End If

    End Sub

    Private Sub SwProforma_ValueChanged(sender As Object, e As EventArgs) Handles SwProforma.ValueChanged
        If (_fnAccesible()) Then
            If (SwProforma.Value = True) Then
                tbProforma.BackColor = Color.White
                tbProforma.ReadOnly = True
                tbProforma.Enabled = True
                tbProforma.Focus()

            Else
                tbProforma.BackColor = Color.LightGray
                tbProforma.ReadOnly = True
                tbProforma.Enabled = False
            End If
        End If
    End Sub

    Private Sub grProductos_FormattingRow(sender As Object, e As RowLoadEventArgs) Handles grProductos.FormattingRow

    End Sub

    Private Sub cbSucursal_ValueChanged(sender As Object, e As EventArgs) Handles cbSucursal.ValueChanged
        Table_Producto = Nothing
    End Sub
    Public Function SeleccionarCategoria(newItem As Boolean) As Integer

        _HabilitarProductos(0)
        'Dim dt As DataTable
        'Dim idCategoria As Integer = 0
        'Dim nombreCategoria As String
        'dt = L_fnListarCategoriaVentas()
        ''   yccod3,ycdes3 

        'Dim listEstCeldas As New List(Of Modelo.Celda)
        'listEstCeldas.Add(New Modelo.Celda("yccod3,", True, "Codigo", 100))
        'listEstCeldas.Add(New Modelo.Celda("ycdes3", True, "Nombre Categoria", 500))

        'Dim ef = New Efecto
        'ef.tipo = 3
        'ef.dt = dt
        'ef.SeleclCol = 2
        'ef.listEstCeldas = listEstCeldas
        'ef.alto = 50
        'ef.ancho = 800
        'ef.Context = "Seleccione Categoria".ToUpper
        'ef.ShowDialog()
        'Dim bandera As Boolean = False
        'bandera = ef.band
        'If (bandera = True) Then
        '    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
        '    ''yccod3,ycdes3 
        '    idCategoria = Row.Cells("yccod3").Value
        '    nombreCategoria = Row.Cells("ycdes3").Value
        '    If (idCategoria > 0) Then
        '        'If (newItem = True) Then
        '        '    _prAddDetalleVenta()
        '        'End If

        '        _HabilitarProductos(idCategoria)
        '    End If



        'End If
        'Return idCategoria
    End Function



    Private Sub btnSearchCliente_Click(sender As Object, e As EventArgs) Handles btnSearchCliente.Click
        Dim dt As DataTable

        dt = L_fnListarClientes()
        '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
        ',a.ydtelf1 ,a.ydfnac 

        Dim listEstCeldas As New List(Of Modelo.Celda)
        listEstCeldas.Add(New Modelo.Celda("ydnumi,", False, "ID", 50))
        listEstCeldas.Add(New Modelo.Celda("ydcod", True, "ID", 50))
        listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", True, "RAZON SOCIAL", 180))
        listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
        listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
        listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
        listEstCeldas.Add(New Modelo.Celda("ydfnac", False, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
        listEstCeldas.Add(New Modelo.Celda("ydnumivend,", False, "ID", 50))
        listEstCeldas.Add(New Modelo.Celda("vendedor,", False, "ID", 50))
        listEstCeldas.Add(New Modelo.Celda("yddias", False, "CRED", 50))
        listEstCeldas.Add(New Modelo.Celda("ygdesc", True, "TIPO DE PRECIO", 180))
        Dim ef = New Efecto
        ef.tipo = 3
        ef.dt = dt
        ef.SeleclCol = 2
        ef.listEstCeldas = listEstCeldas
        ef.alto = 50
        ef.ancho = 350
        ef.Context = "Seleccione Cliente".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

            _CodCliente = Row.Cells("ydnumi").Value
            tbCliente.Text = Row.Cells("yddesc").Value
            _dias = Row.Cells("yddias").Value

            Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
            If (numiVendedor > 0) Then
                tbVendedor.Text = Row.Cells("vendedor").Value
                _CodEmpleado = Row.Cells("ydnumivend").Value

                grdetalle.Select()
                Table_Producto = Nothing
            Else
                'tbVendedor.Clear()
                '_CodEmpleado = 0
                tbVendedor.Focus()
                Table_Producto = Nothing

            End If
        End If

    End Sub

    Private Sub grdetalle_DoubleClick(sender As Object, e As EventArgs) Handles grdetalle.DoubleClick
        If (GPanelProductos.Visible = True) Then
            _DesHabilitarProductos()
        End If


    End Sub

    Private Sub grProductos_DoubleClick(sender As Object, e As EventArgs) Handles grProductos.DoubleClick
        Dim f, c As Integer
        c = grProductos.Col
        f = grProductos.Row
        If (f >= 0) Then

            If (IsNothing(FilaSelectLote)) Then
                ''''''''''''''''''''''''
                If (G_Lote = True) Then
                    InsertarProductosConLote()
                Else
                    'InsertarProductosSinLote()
                End If
                '''''''''''''''
            Else

                '_fnExisteProductoConLote()
                Dim pos As Integer = -1
                grdetalle.Row = grdetalle.RowCount - 1
                _fnObtenerFilaDetalle(pos, grdetalle.GetValue("tbnumi"))
                Dim numiProd = FilaSelectLote.Item("Item")
                Dim lote As String = grProductos.GetValue("iclot")
                Dim FechaVenc As Date = grProductos.GetValue("icfven")
                If (Not _fnExisteProductoConLote(numiProd, lote, FechaVenc)) Then
                    If (grProductos.GetValue("Stock") <= 0) Then
                        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                        ToastNotification.Show(Me, "El producto no tiene stock disponible".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    End If
                    'b.yfcdprod1, a.iclot, a.icfven, a.iccven
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = FilaSelectLote.Item("Item")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = FilaSelectLote.Item("yfcprod")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = FilaSelectLote.Item("yfcbarra")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = FilaSelectLote.Item("yfcdprod1")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = FilaSelectLote.Item("yfumin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = FilaSelectLote.Item("UnidMin")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = FilaSelectLote.Item("yhprecio")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = FilaSelectLote.Item("yhprecio")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = FilaSelectLote.Item("yhprecio")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                    'If (gb_FacturaIncluirICE) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = FilaSelectLote.Item("pcos")
                    'Else
                    '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = 0
                    'End If
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = FilaSelectLote.Item("pcos")

                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tblote") = grProductos.GetValue("iclot")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfechaVenc") = grProductos.GetValue("icfven")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("iccven")
                    _prCalcularPrecioTotal()
                    _DesHabilitarProductos()
                    FilaSelectLote = Nothing
                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "El producto con el lote ya existe modifique su cantidad".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                End If



            End If

        End If
    End Sub

    Private Sub tbProducto_TextChanged(sender As Object, e As EventArgs) Handles tbProducto.TextChanged
        Dim dtProductoCopy As DataTable
        dtProductoCopy = dtProductoGoblal.Copy
        dtProductoCopy.Rows.Clear()
        Dim dt As DataTable = dtProductoGoblal.Copy

        Dim charSequence As String
        charSequence = tbProducto.Text.ToUpper
        If (charSequence.Trim <> String.Empty) Then
            Dim cantidad As Integer = 12
            Dim cont As Integer = 12

            'Split con array de delimitadores
            Dim delimitadores() As String = {" ", ".", ",", ";", "-"}
            Dim vectoraux() As String
            vectoraux = charSequence.Split(delimitadores, StringSplitOptions.None)

            'mostrar resultado
            'For Each item As String In vectoraux


            '    Console.WriteLine("'{0}'", item)
            'Next
            Dim cant As Integer = vectoraux.Length

            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                Dim nombre As String = dt.Rows(i).Item("yfcdprod1").ToString.ToUpper
                Select Case cant
                    Case 1

                        If (nombre.Trim.Contains(vectoraux(0))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If

                    Case 2
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 3
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 4
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 5
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 6
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If

                    Case 7

                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 8
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 9
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 10
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If

                    Case 11
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If

                    Case 12
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If


                    Case 13
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11)) And nombre.Trim.Contains(vectoraux(12))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 14
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11)) And nombre.Trim.Contains(vectoraux(12)) And nombre.Trim.Contains(vectoraux(13))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 15
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11)) And nombre.Trim.Contains(vectoraux(12)) And nombre.Trim.Contains(vectoraux(13)) And nombre.Trim.Contains(vectoraux(14))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 16
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11)) And nombre.Trim.Contains(vectoraux(12)) And nombre.Trim.Contains(vectoraux(13)) And nombre.Trim.Contains(vectoraux(14)) And nombre.Trim.Contains(vectoraux(15))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 17
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11)) And nombre.Trim.Contains(vectoraux(12)) And nombre.Trim.Contains(vectoraux(13)) And nombre.Trim.Contains(vectoraux(14)) And nombre.Trim.Contains(vectoraux(15)) And nombre.Trim.Contains(vectoraux(16))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If
                    Case 18
                        If (nombre.Trim.Contains(vectoraux(0)) And nombre.Trim.Contains(vectoraux(1)) And nombre.Trim.Contains(vectoraux(2)) And nombre.Trim.Contains(vectoraux(3)) And nombre.Trim.Contains(vectoraux(4)) And nombre.Trim.Contains(vectoraux(5)) And nombre.Trim.Contains(vectoraux(6)) And nombre.Trim.Contains(vectoraux(7)) And nombre.Trim.Contains(vectoraux(8)) And nombre.Trim.Contains(vectoraux(9)) And nombre.Trim.Contains(vectoraux(10)) And nombre.Trim.Contains(vectoraux(11)) And nombre.Trim.Contains(vectoraux(12)) And nombre.Trim.Contains(vectoraux(13)) And nombre.Trim.Contains(vectoraux(14)) And nombre.Trim.Contains(vectoraux(15)) And nombre.Trim.Contains(vectoraux(16)) And nombre.Trim.Contains(vectoraux(17))) Then
                            dtProductoCopy.ImportRow(dt.Rows(i))
                            cont += 1
                        End If



                End Select

            Next
            grProductos.DataSource = dtProductoCopy.Copy
        Else
            grProductos.DataSource = dtProductoGoblal.Copy
        End If



    End Sub

    Private Sub tbProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles tbProducto.KeyDown
        If e.KeyData = Keys.Escape Then
            _DesHabilitarProductos()
            FilaSelectLote = Nothing
        End If
        If e.KeyData = Keys.Down Then
            grProductos.Focus()
        End If

    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim a As Integer = _CodCliente
        If IsNumeric(cbCliente.Value) = False Then

            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
            ToastNotification.Show(Me, "Primero debe seleccionar el cliente ".ToUpper,
                                  img, 2000,
                                  eToastGlowColor.Green,
                                  eToastPosition.TopCenter)
            cbCliente.Text = ""
            cbCliente.Focus()
        ElseIf cbCliente.Value <= 0 Then
            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
            ToastNotification.Show(Me, "Primero debe seleccionar el cliente ".ToUpper,
                                  img, 2000,
                                  eToastGlowColor.Green,
                                  eToastPosition.TopCenter)
            cbCliente.Text = ""
            cbCliente.Focus()
        Else
            SeleccionarCategoria(True)
        End If

    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        _IniciarTodo()
    End Sub

    Private Sub tbMontoBs_ValueChanged(sender As Object, e As EventArgs) Handles tbMontoBs.ValueChanged
        If btnGrabar.Enabled = True Then
            tbMontoDolar.Value = 0
            tbMontoTarej.Value = 0

            If tbMontoBs.Value <> 0 And tbMontoBs.Text <> String.Empty Then
                txtMontoPagado1.Text = tbMontoBs.Value + (tbMontoDolar.Value * IIf(cbCambioDolar.Text = "", 0, Convert.ToDecimal(cbCambioDolar.Text))) + tbMontoTarej.Value
                If Convert.ToDecimal(tbtotal.Text) <> 0 And Convert.ToDecimal(txtMontoPagado1.Text) >= Convert.ToDecimal(tbtotal.Text) Then
                    txtCambio1.Text = Convert.ToDecimal(txtMontoPagado1.Text) - Convert.ToDecimal(tbtotal.Text)
                Else
                    txtCambio1.Text = "0.00"
                    txtMontoPagado1.Text = "0.00"
                End If
            End If
        End If

    End Sub

    Private Sub cbCambioDolar_ValueChanged(sender As Object, e As EventArgs) Handles cbCambioDolar.ValueChanged
        If cbCambioDolar.SelectedIndex < 0 And cbCambioDolar.Text <> String.Empty Then
            btAgregarTCambio.Visible = True
        Else
            btAgregarTCambio.Visible = False
        End If
    End Sub

    Private Sub btAgregarTCambio_Click(sender As Object, e As EventArgs) Handles btAgregarTCambio.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, "7", "1", cbCambioDolar.Text, "") Then
            _prCargarComboLibreria(cbCambioDolar, "7", "1")
            cbCambioDolar.SelectedIndex = CType(cbCambioDolar.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub chbTarjeta_CheckedChanged(sender As Object, e As EventArgs) Handles chbTarjeta.CheckedChanged
        If btnGrabar.Enabled = True Then
            If chbTarjeta.Checked Then
                'tbMontoBs.Value = 0
                'tbMontoDolar.Value = 0
                tbMontoTarej.Enabled = True
                tbMontoTarej.Value = Convert.ToDecimal(tbtotal.Text)
                If tbMontoTarej.Value <> 0 And tbMontoTarej.Text <> String.Empty Then
                    txtMontoPagado1.Text = tbMontoBs.Value + (tbMontoDolar.Value * IIf(cbCambioDolar.Text = "", 0, Convert.ToDecimal(cbCambioDolar.Text))) + tbMontoTarej.Value
                    If Convert.ToDecimal(tbtotal.Text) <> 0 And Convert.ToDecimal(txtMontoPagado1.Text) >= Convert.ToDecimal(tbtotal.Text) Then
                        txtCambio1.Text = Convert.ToDecimal(txtMontoPagado1.Text) - Convert.ToDecimal(tbtotal.Text)
                    Else
                        txtCambio1.Text = "0.00"
                    End If
                End If
                tbMontoBs.Enabled = False
                tbMontoDolar.Enabled = False
                tbMontoTarej.IsInputReadOnly = True
                tbMontoTarej.Focus()
            Else
                tbMontoBs.Enabled = True
                tbMontoDolar.Enabled = True
                tbMontoTarej.Value = 0
            End If
        End If
    End Sub

    Private Sub tbMontoDolar_ValueChanged(sender As Object, e As EventArgs) Handles tbMontoDolar.ValueChanged
        If btnGrabar.Enabled = True Then
            tbMontoBs.Value = 0
            tbMontoTarej.Value = 0
            If tbMontoDolar.Value <> 0 And tbMontoDolar.Text <> String.Empty Then
                txtMontoPagado1.Text = tbMontoBs.Value + (tbMontoDolar.Value * IIf(cbCambioDolar.Text = "", 0, Convert.ToDecimal(cbCambioDolar.Text))) + tbMontoTarej.Value
                If Convert.ToDecimal(tbtotal.Text) <> 0 And Convert.ToDecimal(txtMontoPagado1.Text) >= Convert.ToDecimal(tbtotal.Text) Then
                    txtCambio1.Text = Convert.ToDecimal(txtMontoPagado1.Text) - Convert.ToDecimal(tbtotal.Text)
                Else
                    txtCambio1.Text = "0.00"
                End If
            End If
        End If
    End Sub

    Private Sub btnCobrar_Click(sender As Object, e As EventArgs) Handles btnCobrar.Click
        If grVentas.GetValue("taEstadoV") = 1 Then
            Dim ef As F1_MontoPagar
            ef = New F1_MontoPagar

            ef.TotalVenta = Math.Round(tbtotal.Value, 2)
            ef.tipoVenta = IIf(swTipoVenta.Value = True, 1, 0)
            ef.Cobrado = False
            ef.cliente = _CodCliente
            ef.CostoEnvio = tbEnvio.Text
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
                _prGuardarCobro()
            Else
                ToastNotification.Show(Me, "No se realizó ninguna operación ".ToUpper, My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)

            End If
        Else
            Dim ef As F1_MontoPagar
            ef = New F1_MontoPagar

            ef.TotalVenta = Math.Round(tbtotal.Value, 2)
            ef.Cobrado = True

            ef.swTipoVenta.Value = swTipoVenta.Value
            ef.tbFechaVenc.Value = tbFechaVenc.Value

            ef.cbCambioDolar.Text = cbCambioDolar.Text
            ef.tbCostoEnvio.Value = CostoEnvio
            ef.tbMontoDolar.Value = tbMontoDolar.Value
            ef.tbMontoTarej.Value = tbMontoTarej.Value
            ef.tbMontoBs.Value = tbMontoBs.Value

            'ef.cbCambioDolar.Value = cbCambioDolar.Value

            If tbMontoTarej.Value = 0 Then
                ef.chbTarjeta.Checked = False
            Else
                ef.chbTarjeta.Checked = True
            End If

            ef.Banc = Banco
            ef.tbGlosa.Text = Glosa


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

                _prGuardarCobro()
            Else
                ToastNotification.Show(Me, "No se realizó ninguna operación ".ToUpper, My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)

            End If
        End If


    End Sub

    Private Sub btnVerPagos_Click(sender As Object, e As EventArgs) Handles btnVerPagos.Click
        Dim frm As New F0_PagosCreditoNuevo
        frm.Show()
    End Sub

    Private Sub btnAdicionarCliente_Click(sender As Object, e As EventArgs) Handles btnAdicionarCliente.Click
        gi_CodCliente = 1
        Timer1.Start()
        Dim frm As New F1_Clientes
        frm._nameButton = DinoM.P_Principal.btConfCliente.Name
        frm.Show()


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If gi_CodCliente = 2 Then
            Dim dt As DataTable
            dt = L_fnListarClientes()
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                If i = dt.Rows.Count - 1 Then
                    _CodCliente = dt.Rows(i).Item("ydnumi")
                    cbCliente.Value = dt.Rows(i).Item("ydnumi")
                    Timer1.Stop()
                    gi_CodCliente = 0
                    Exit For
                End If
            Next
        End If

    End Sub

    Private Sub tbCliente_TextChanged(sender As Object, e As EventArgs) Handles tbCliente.TextChanged
        Dim dt As DataTable = L_fnTraerTipoPrecio(_CodCliente)
        If dt.Rows.Count <> 0 Then
            Dim cod As Integer = dt.Rows(0).Item("ydcat")
            cbPrecio.Value = cod
        Else
            cbPrecio.SelectedIndex = 0
        End If
        If _fnAccesible() Then
            'If verificarCredito(_CodCliente) Then
            '    swTipoVenta.Value = False
            '    swTipoVenta.IsReadOnly = False
            'Else
            '    swTipoVenta.Value = True
            '    swTipoVenta.IsReadOnly = True
            'End If
        End If

    End Sub

    Private Sub cbPrecio_ValueChanged(sender As Object, e As EventArgs) Handles cbPrecio.ValueChanged

    End Sub

    Private Sub cbCliente_ValueChanged(sender As Object, e As EventArgs) Handles cbCliente.ValueChanged
        If IsNumeric(cbCliente.Value) Then
            If cbCliente.Value > 0 Then
                _CodCliente = cbCliente.Value
                Dim dt As DataTable = L_fnTraerTipoPrecio(_CodCliente)
                If dt.Rows.Count <> 0 Then
                    Dim cod As Integer = dt.Rows(0).Item("ydcat")
                    cbPrecio.Value = cod
                Else
                    cbPrecio.SelectedIndex = 0
                End If
                If _fnAccesible() Then
                    Dim dt2 As DataTable = verificarCredito(_CodCliente)
                    If dt2.Rows.Count > 0 Then
                        _dias = dt2.Rows(0).Item("yddias")
                        swTipoVenta.Value = False
                        swTipoVenta.IsReadOnly = False
                    Else
                        swTipoVenta.Value = True
                        swTipoVenta.IsReadOnly = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub swMoneda_ValueChanged(sender As Object, e As EventArgs) Handles swMoneda.ValueChanged

    End Sub



#End Region



#Region "Facturacion"

    Public Shared Function ObtToken()
        Dim api = New DBApi()

        Dim Lenvio = New LoginEnvio()
        Lenvio.email = "admin@gmail.com"
        Lenvio.password = "12345678"

        Dim url = "https://devsoftbo.com/siat2/public/api/auth/login"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "bearer "),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.Post(url, headers, parametros, Lenvio)
        Dim json = JsonConvert.SerializeObject(Lenvio)
        ''MsgBox(json)

        Dim result = JsonConvert.DeserializeObject(Of LoginResp1)(response)
        Dim Token As String
        Dim json1 = JsonConvert.SerializeObject(response)
        '' MsgBox(json1)
        Token = result.access_token.ToString
        Return Token
    End Function
    Public Function Emisor(tokenObtenido)

        Dim api = New DBApi()
        Dim Emenvio = New EmisorEnvio1()

        'Dim TMetPago = CbMetPago.SelectedIndex + 1 'obtiene el 'Codigo Metodo de pago' 
        Dim TDoc = CbTipoDoc.Value 'obtiene el 'Codigo Tipo de documento' 

        'Dim tabla As DataTable = rearmarDetalle()
        Dim dtDetalle As DataTable = CType(grdetalle.DataSource, DataTable)
        dtDetalle = dtDetalle.Select("estado=0").CopyToDataTable

        Dim array(dtDetalle.Rows.Count - 1) As Detalle
        Dim val = 0
        PrecioTot = 0
        For Each row In dtDetalle.Rows
            Dim EmenvioDetalle = New Detalle()

            'EmenvioDetalle.codigoProductoSin = (row("ygcodsin").ToString)
            EmenvioDetalle.codigoProducto = (row("tbty5prod").ToString)
            EmenvioDetalle.descripcion = (row("producto").ToString)
            'EmenvioDetalle.unidadMedida = Convert.ToInt32(row("ygcodu"))
            EmenvioDetalle.cantidad = Format((row("tbcmin")), "#.#0")
            EmenvioDetalle.precioUnitario = Format((row("tbpbas")), "#.#0")
            EmenvioDetalle.montoDescuento = Format((row("tbdesc")), "#.#0")
            'EmenvioDetalle.subTotal = Format((row("tbtotdesc")), "#.#0")

            PrecioTot = PrecioTot + Format((row("tbtotdesc")), "#.#0") 'total


            array(val) = EmenvioDetalle
            'vector = array
            val = val + 1

        Next
        Dim NumFactura As Integer
        Dim email As String
        Dim CodMetPago As Integer
        Dim NroTarjeta As String

        Dim _DsDosificacion As New DataSet
        Dim _Autorizacion As String
        Dim _NumFac As Integer

        If TbEmail.Text = String.Empty Then
            email = "edoradoes1@gmail.com"
            TbEmail.Text = email
        Else
            email = TbEmail.Text
        End If


        CodMetPago = 1
        NroTarjeta = ""



        'If cbTipoVenta.Value = 1 Then
        '    CodMetPago = 1
        '    NroTarjeta = ""
        'ElseIf cbTipoVenta.Value = 2 Then
        '    CodMetPago = 2
        '    NroTarjeta = tbNroTarjeta1.Text & tbNroTarjeta2.Text & tbNroTarjeta3.Text
        'ElseIf cbTipoVenta.Value = 0 Then
        '    CodMetPago = 6
        '    NroTarjeta = ""
        'Else
        '    CodMetPago = 1
        '    NroTarjeta = ""
        'End If



        Dim dtmax = L_fnObtenerMaxFact(cbSucursal.Value, Convert.ToInt32(Now.Date.Year))
        If dtmax.Rows.Count = 0 Then
            NumFactura = 1
        Else
            Dim maxNFac As Integer = dtmax.Rows(0).Item("fvanfac")
            NumFactura = maxNFac + 1
        End If



        '  Emenvio.numeroFactura = 282 'NumFactura
        Emenvio.razon_social_cliente = nombre1.Text.ToString()
        Emenvio.tipo_documento = TDoc
        Emenvio.numero_documento = TbNit.Text.ToString()
        Emenvio.complemento = "" '---------------------------------
        Emenvio.codigo_cliente = _CodCliente.ToString
        Emenvio.metodo_pago = CodMetPago
        Emenvio.cod_sucursal = 0
        Emenvio.punto_venta = 0
        'Emenvio.numeroTarjeta = NroTarjeta
        'Emenvio.codigoPuntoVenta = 0 'gs_NroCaja '--------------------
        'Emenvio.codigoDocumentoSector = 1 '-------------------
        'Emenvio.codigoMoneda = 1 'falta
        'Emenvio.tipoCambio = 1 'CDbl(cbCambioDolar.Text) '--------------------
        Emenvio.descuento_adicional = Format(tbMdesc.Value, "#.#0") '-------------------
        'Emenvio.montoTotal = Format((PrecioTot - Emenvio.descuentoAdicional), "#.#0")
        'Emenvio.montoTotalSujetoIva = Format((PrecioTot - Emenvio.descuentoAdicional), "#.#0")
        'Emenvio.montoTotalMoneda = Format((PrecioTot - Emenvio.descuentoAdicional), "#.#0")
        'Emenvio.montoGiftCard = 0 '----------------
        Emenvio.codigoExcepcion = 0 '---------------
        Emenvio.usuario = gs_user
        Emenvio.email = email
        'Emenvio.actividadEconomica = 471110 '477311 'Actividad de Farmacia
        Emenvio.detalle_productos = array
        Dim json = JsonConvert.SerializeObject(Emenvio)
        Dim url = "https://devsoftbo.com/siat2/public/api/siat/factura/solicitud"
        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.Post(url, headers, parametros, Emenvio)

        Dim result = JsonConvert.DeserializeObject(Of EmisorResp1)(response)
        Dim resultData = JsonConvert.DeserializeObject(Of RespEmisor11)(response)
        'Dim resultError = JsonConvert.DeserializeObject(Of Resp400)(response)

        Dim codigoRecepcion = resultData.data.codigo_recepcion
        Dim estado = result.status

        Dim xml As String
        If estado = "true" Then

            'NroFact = result.Data.numeroFactura
            'QrUrl = result.Data.qrUrl
            'FactUrl = result.Data.facturaUrl
            'SegundaLeyenda = result.Data.leyenda
            'TerceraLeyenda = result.Data.terceraLeyenda
            'Cudf = result.Data.cufd

            Dim notifi = New notifi

            notifi.tipo = 2
            notifi.Context = "".ToUpper
            notifi.Header = "Proceso Exitoso - Código: " + resultData.data.codigo_factura.ToString() & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Factura enviada ".ToUpper

            notifi.ShowDialog()
            facturaElectronica.WebBrowser1.Navigate("https://devsoftbo.com/siat2/public/factura-pdf/" + resultData.data.codigo_factura.ToString())
            'facturaElectronica.ShowDialog()
            'ElseIf estado = "false" Then

            '    Dim details = JsonConvert.SerializeObject(resultError.errors.details)
            '    Dim siat = JsonConvert.SerializeObject(resultError.errors.siat)
            '    Dim notifi = New notifi

            '    notifi.tipo = 2
            '    notifi.Context = "SIFAC".ToUpper
            '    notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & siat & vbCrLf & " " & vbCrLf & "La factura no pudo enviarse al Siat".ToUpper
            '    notifi.ShowDialog()
            'ElseIf codigo = 401 Or codigo = 404 Or codigo = 405 Or codigo = 422 Then
            '    Dim details = JsonConvert.SerializeObject(resultError.errors.details)
            '    Dim notifi = New notifi

            '    notifi.tipo = 2
            '    notifi.Context = "SIFAC".ToUpper
            '    notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "La factura no pudo enviarse al Siat".ToUpper
            '    notifi.ShowDialog()
        End If




        Return estado
    End Function

    Public Function CodTipoDocumento(tokenObtenido)

        Dim api = New DBApi()
        Dim Lenvio = New LoginEnvio()
        Lenvio.cod_sucursal = 0
        Lenvio.punto_venta = "0"
        Lenvio.opcion = "7"
        Dim url = "https://devsoftbo.com/siat2/public/api/siat/sincronizacion-lista"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.Post(url, headers, parametros, Lenvio)

        Dim result = JsonConvert.DeserializeObject(Of TipoDocumento)(response)

        With CbTipoDoc
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("codigoClasificador").Width = 70
            .DropDownList.Columns("codigoClasificador").Caption = "COD"
            .DropDownList.Columns.Add("descripcion").Width = 500
            .DropDownList.Columns("descripcion").Caption = "DESCRIPCION"
            .ValueMember = "codigoClasificador"
            .DisplayMember = "descripcion"
            .DataSource = result.data
            .Refresh()
        End With

        'Dim Codigoconn As String
        'Codigoconn = result.meta.code
        'Dim json = JsonConvert.SerializeObject(result)
        'msgBox(json)
        Return ""
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ObtToken()
    End Sub

    Private Sub TextBoxX1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbnit1.KeyPress
        g_prValidarTextBox(1, e)
    End Sub
#End Region
End Class