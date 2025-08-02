
Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System.IO
Imports Janus.Windows.GridEX
Public Class F1_MovimientoBancos

#Region "Variable Globales"

    Dim _Inter As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Dim Modificado As Boolean = False
    Dim nameImg As String = "Default.jpg"
    Dim Socio As Boolean = False
    Dim NumiCuentaContable As Integer = 0

#End Region
#Region "METODOS PRIVADOS"
    Private Sub _prIniciarTodo()

        Me.Text = "MOVIMIENTO BANCO"
        _prCargarComboLibreria(cbConcepto, 9, 2)
        _prCargarComboLibreriaSucursal(cbSucursal)
        _prCargarComboBanco(cbBanco)
        _PMIniciarTodo()
        _prAsignarPermisos()
        _prCargarLengthTextBox()
        
        GroupPanelBuscador.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.TextColor = Color.White
        Dim blah As Bitmap = My.Resources.checked
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        JGrM_Buscador.RootTable.HeaderFormatStyle.FontBold = TriState.True
        JGrM_Buscador.AlternatingColors = True
        btnModificar.Enabled = True
        btnEliminar.Enabled = True
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
        If dt.Rows.Count > 0 Then
            mCombo.SelectedIndex = 0
        End If

    End Sub
    Public Sub _prCargarLengthTextBox()
        tbDescripcion.MaxLength = 200
        cbSucursal.MaxLength = 200
        tbObservacion.MaxLength = 300
        cbBanco.MaxLength = 200
        tbBoleta.MaxLength = 200
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

    Private Function _fnActionNuevo() As Boolean
        'Funcion que me devuelve True si esta en la actividad crear nuevo Tipo de Equipo
        Return tbcodigo.Text.ToString.Equals("") And tbDescripcion.ReadOnly = False
    End Function

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

    Private Sub _prCargarComboBanco(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarBanco()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("canumi").Width = 60
            .DropDownList.Columns("canumi").Caption = "COD"
            .DropDownList.Columns.Add("banco").Width = 500
            .DropDownList.Columns("banco").Caption = "SUCURSAL"
            .ValueMember = "canumi"
            .DisplayMember = "banco"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               4000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)
    End Sub

#End Region
#Region "METODOS SOBREESCRITOS"
    Public Overrides Sub _PMOHabilitar()
        swTipo.IsReadOnly = False
        'dpFecha.Enabled = True
        tbDescripcion.ReadOnly = False
        cbSucursal.ReadOnly = False
        cbBanco.ReadOnly = False
        tbBoleta.ReadOnly = False
        tbMonto.IsInputReadOnly = False
        tbObservacion.ReadOnly = False
        cbConcepto.ReadOnly = False

    End Sub
    Public Overrides Sub _PMOInhabilitar()
        tbcodigo.ReadOnly = True
        swTipo.IsReadOnly = True
        dpFecha.Enabled = False
        tbDescripcion.ReadOnly = True
        cbSucursal.ReadOnly = True
        cbBanco.ReadOnly = True
        tbBoleta.ReadOnly = True
        tbMonto.IsInputReadOnly = True
        tbObservacion.ReadOnly = True
        cbConcepto.ReadOnly = True

    End Sub
    Public Overrides Sub _PMOHabilitarFocus()
        With MHighlighterFocus
            .SetHighlightOnFocus(tbDescripcion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(cbSucursal, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            '.SetHighlightOnFocus(cbBanco, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbBoleta, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbMonto, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbObservacion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbBoleta, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbcodigo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        End With
    End Sub
    Public Overrides Sub _PMOLimpiar()

        tbcodigo.Text = ""
        'tbIdCaja.Text = ""
        swTipo.Value = True
        dpFecha.Value = Now.Date
        tbDescripcion.Text = ""
        ' tbIdDevolucion.Text = "0"
        'cbConcepto.SelectedIndex = 0
        tbMonto.Value = 0
        tbObservacion.Text = ""
        tbBoleta.Text = ""
        'tbDescripcion.Focus()
        cbConcepto.SelectedIndex = 0
    End Sub
    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbcodigo.BackColor = Color.White

        tbDescripcion.BackColor = Color.White
        tbBoleta.BackColor = Color.White
        cbSucursal.BackColor = Color.White
        cbBanco.BackColor = Color.White
        tbMonto.BackColor = Color.White
        tbObservacion.BackColor = Color.White
    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbDescripcion.Text = String.Empty Then
            tbDescripcion.BackColor = Color.Red
            MEP.SetError(tbDescripcion, "ingrese Dato en el campo Descripcion !".ToUpper)
            _ok = False
        Else
            tbDescripcion.BackColor = Color.White
            MEP.SetError(tbDescripcion, "")
        End If
        If tbBoleta.Text = String.Empty Then
            tbBoleta.BackColor = Color.Red
            MEP.SetError(tbBoleta, "ingrese Dato en el campo Descripcion !".ToUpper)
            _ok = False
        Else
            tbDescripcion.BackColor = Color.White
            MEP.SetError(tbDescripcion, "")
        End If
        If (tbMonto.Value <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            MEP.SetError(tbMonto, "Por Favor introduzca monto !".ToUpper)
            _ok = False
        Else
            tbMonto.BackColor = Color.White
            MEP.SetError(tbMonto, "")
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)


        listEstCeldas.Add(New Modelo.Celda("manumi", True, "Codigo", 120))
        listEstCeldas.Add(New Modelo.Celda("maFecha", True, "Fecha", 100))
        listEstCeldas.Add(New Modelo.Celda("maTipo", False))
        listEstCeldas.Add(New Modelo.Celda("Tipo", True, "Tipo", 120))
        listEstCeldas.Add(New Modelo.Celda("maSucursal", False))
        listEstCeldas.Add(New Modelo.Celda("Sucursal", True, "Sucursal", 100))
        listEstCeldas.Add(New Modelo.Celda("maBanco", False))
        listEstCeldas.Add(New Modelo.Celda("banco", True, "Banco", 250))
        listEstCeldas.Add(New Modelo.Celda("maNboleta", True, "Nro Boleta", 100))
        listEstCeldas.Add(New Modelo.Celda("maDetalle", True, "Detalle", 100))
        listEstCeldas.Add(New Modelo.Celda("maMonto", True, "Monto Bs.", 150, "0.00"))
        listEstCeldas.Add(New Modelo.Celda("maObs", False))
        listEstCeldas.Add(New Modelo.Celda("maEstado", False))
        listEstCeldas.Add(New Modelo.Celda("mafact", False))
        listEstCeldas.Add(New Modelo.Celda("mahact", False))
        listEstCeldas.Add(New Modelo.Celda("mauact", False))
        listEstCeldas.Add(New Modelo.Celda("maconcep", False))


        Return listEstCeldas

    End Function
    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_prMovimientoBancoGeneral()
        Return dtBuscador
    End Function


    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        With JGrM_Buscador
            tbcodigo.Text = .GetValue("manumi").ToString
            dpFecha.Value = .GetValue("maFecha")
            swTipo.Value = .GetValue("maTipo")
            cbSucursal.Value = .GetValue("maSucursal")
            cbBanco.Value = .GetValue("maBanco")
            tbBoleta.Text = .GetValue("maNboleta").ToString
            tbDescripcion.Text = .GetValue("maDetalle").ToString
            tbMonto.Value = .GetValue("maMonto")
            tbObservacion.Text = .GetValue("maObs").ToString
            cbConcepto.Value = .GetValue("maconcep")
            lbFecha.Text = CType(.GetValue("mafact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("mahact").ToString
            lbUsuario.Text = .GetValue("mauact").ToString

            'diseño de la grilla para el Total
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
        End With
        With JGrM_Buscador.RootTable.Columns("maMonto")
            .AggregateFunction = AggregateFunction.Sum
        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub
    Public Overrides Function _PMOGrabarRegistro() As Boolean

        Dim tipo As Integer = IIf(swTipo.Value = True, 1, 0)
        Dim res As Boolean = L_prMovimientoGrabar(tbcodigo.Text, dpFecha.Value, tipo, cbSucursal.Value, cbBanco.Value, tbBoleta.Text, tbDescripcion.Text,
                                                     tbMonto.Value, tbObservacion.Text, cbConcepto.Value, CInt(tbIdDevolucion.Text))

        If res Then
            Modificado = False
            _PMOLimpiar()
            SupTabItemBusqueda.Visible = False
            ToastNotification.Show(Me, "Codigo de Movimiento Banco".ToUpper + tbcodigo.Text + " Grabado con éxito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function

    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean
        Dim tipo As Integer = IIf(swTipo.Value = True, 1, 0)
        If (Modificado = False) Then
            res = L_prMovimientoBancoModificar(tbcodigo.Text, dpFecha.Value, tipo, cbSucursal.Value, cbBanco.Value, tbBoleta.Text, tbDescripcion.Text,
                                                     tbMonto.Value, tbObservacion.Text, cbConcepto.Value)

        Else
            res = L_prMovimientoBancoModificar(tbcodigo.Text, dpFecha.Value, tipo, cbSucursal.Value, cbBanco.Value, tbBoleta.Text, tbDescripcion.Text,
                                                     tbMonto.Value, tbObservacion.Text, cbConcepto.Value)
        End If
        If res Then
            Modificado = False
            _PMInhabilitar()
            _PMPrimerRegistro()
            SupTabItemBusqueda.Visible = False
            ToastNotification.Show(Me, "Codigo de Movimiento Banco".ToUpper + tbcodigo.Text + " modificado con éxito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function

    Public Overrides Sub _PMOEliminarRegistro()

        Dim info As New TaskDialogInfo("¿esta seguro de eliminar el registro?".ToUpper, eTaskDialogIcon.Delete, "advertencia".ToUpper, "mensaje principal".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            If result = eTaskDialogResult.Yes Then
                Dim mensajeError As String = ""
            Dim res As Boolean = L_prMovimientoBancoBorrar(tbcodigo.Text, mensajeError)
            If res Then
                    ToastNotification.Show(Me, "Codigo de Ingreso/Egreso ".ToUpper + tbcodigo.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                    _PMFiltrar()
                Else
                    ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

        Else
            ToastNotification.Show(Me, "No puede Eliminar un Ingreso/Egreso, ya se hizo cierre de caja, por favor primero elimine cierre de caja".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Red, eToastPosition.TopCenter)
        End If
    End Sub

#End Region
#Region "EVENTOS"
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal

        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub F1_MovimientoBancos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMPrimerRegistro()

        Else
            Close()
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbDescripcion.Focus()


        ''Recupera y asigna la Sucursal a la que pertenece el usuario





    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click

    End Sub

    Private Sub LabelX3_Click(sender As Object, e As EventArgs) Handles LabelX3.Click

    End Sub
    Private Sub P_GenerarReporte(numi As String)
        P_Global.Visualizador = New Visualizador

        Dim objrep As New R_movimientoBanco
        Dim _TotalLi As Decimal
        Dim _Literal, _TotalDecimal, _TotalDecimal2 As String
        _TotalLi = tbMonto.Value
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + "  " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"

        Dim concep As String
        If swTipo.Value = 1 Then
            concep = "INGRESO"
        Else
            concep = "EGRESO"
        End If
        objrep.SetParameterValue("sucursal", cbSucursal.Text)
        objrep.SetParameterValue("concepto", concep)
        objrep.SetParameterValue("usuario", gs_user)
        objrep.SetParameterValue("glosa", tbObservacion.Text)
        objrep.SetParameterValue("fecha", dpFecha.Value.ToString("dd/MM/yyyy"))
        objrep.SetParameterValue("banco", cbBanco.Text)
        objrep.SetParameterValue("boleta", tbBoleta.Text)
        objrep.SetParameterValue("detalle", tbDescripcion.Text)
        objrep.SetParameterValue("monto", tbMonto.Text)
        objrep.SetParameterValue("hora", lbHora.Text)


        objrep.SetParameterValue("literal", _Literal)
        P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        P_Global.Visualizador.ShowDialog() 'Comentar
        P_Global.Visualizador.BringToFront()
    End Sub
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        P_GenerarReporte(tbcodigo.Text)
    End Sub

    Private Sub cbConcepto_ValueChanged(sender As Object, e As EventArgs) Handles cbConcepto.ValueChanged
        If cbConcepto.SelectedIndex < 0 And cbConcepto.Text <> String.Empty Then
            btConcepto.Visible = True
        Else
            btConcepto.Visible = False
        End If
        If IsNumeric(cbConcepto.Value) Then
            If cbConcepto.Value = 5 Or cbConcepto.Value = 6 Then 'Devolución
                If cbConcepto.Value = 5 Then
                    lbDevolucion.Visible = True
                    lbDevolucion.Text = "ID Devolución: "
                    tbIdDevolucion.Visible = True
                    btnBuscarDevolución.Visible = True
                ElseIf cbConcepto.Value = 6 Then 'Devolución
                    lbDevolucion.Visible = True
                    lbDevolucion.Text = "ID Transito: "
                    tbIdDevolucion.Visible = True
                    btnBuscarDevolución.Visible = True
                End If
            Else
                lbDevolucion.Visible = False
                tbIdDevolucion.Visible = False
                btnBuscarDevolución.Visible = False
            End If

        Else
            lbDevolucion.Visible = False
            tbIdDevolucion.Visible = False
            btnBuscarDevolución.Visible = False
        End If

    End Sub

    Private Sub btConcepto_Click(sender As Object, e As EventArgs) Handles btConcepto.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, "9", "2", cbConcepto.Text, "") Then
            _prCargarComboLibreria(cbConcepto, "9", "2")
            cbConcepto.SelectedIndex = CType(cbConcepto.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub lbDevolucion_Click(sender As Object, e As EventArgs) Handles lbDevolucion.Click

    End Sub

    Private Sub btnBuscarDevolución_Click(sender As Object, e As EventArgs) Handles btnBuscarDevolución.Click
        SupTabItemBusqueda.Visible = True
        SuperTabPrincipal.SelectedTabIndex = 1
        If cbConcepto.Value = 5 Then
            _prCargarDevolucion()
        Else
            _prCargarCostosImportacion()
        End If
    End Sub

    Private Sub tbIdDevolucion_TextChanged(sender As Object, e As EventArgs) Handles tbIdDevolucion.TextChanged

    End Sub
    Private Sub _prCargarCostosImportacion()
        Dim dt As New DataTable
        dt = L_fnGeneraCostoImportacion()
        grDevolucion.DataSource = dt
        grDevolucion.RetrieveStructure()
        grDevolucion.AlternatingColors = True

        With grDevolucion.RootTable.Columns("tanumi")
            .Width = 100
            .Caption = "ID TRANSITO"
            .Visible = True
        End With

        With grDevolucion.RootTable.Columns("tafdoc")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
        End With
        With grDevolucion.RootTable.Columns("yddesc")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "PROVEEDOR"
        End With
        With grDevolucion.RootTable.Columns("saldo")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With


        With grDevolucion
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
        End With

        'If (dt.Rows.Count <= 0) Then
        '    _prCargarDetalleVenta(-1)
        'End If
    End Sub

    Private Sub _prCargarDevolucion()
        Dim dt As New DataTable
        dt = L_fnGeneraDevolucionEgreso(gi_userSuc)
        grDevolucion.DataSource = dt
        grDevolucion.RetrieveStructure()
        grDevolucion.AlternatingColors = True

        With grDevolucion.RootTable.Columns("dbnumi")
            .Width = 100
            .Caption = "ID DEVOLUCIÓN"
            .Visible = True
        End With
        With grDevolucion.RootTable.Columns("dbtanumi")
            .Width = 90
            .Visible = True
            .Caption = "ID VENTA"
        End With
        With grDevolucion.RootTable.Columns("dbfdev")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
        End With
        With grDevolucion.RootTable.Columns("dbobs")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "OBSERVACION"
        End With
        With grDevolucion.RootTable.Columns("dbtotal")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grDevolucion.RootTable.Columns("taalm")
            .Width = 90
            .Visible = False
        End With
        With grDevolucion.RootTable.Columns("tafdoc")
            .Width = 160
            .Visible = False
        End With

        With grDevolucion.RootTable.Columns("vendedor")
            .Width = 250
            .Visible = False
            .Caption = "VENDEDOR".ToUpper
        End With
        With grDevolucion.RootTable.Columns("tatven")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grDevolucion.RootTable.Columns("tafvcr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grDevolucion.RootTable.Columns("taclpr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grDevolucion.RootTable.Columns("cliente")
            .Width = 250
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "CLIENTE"
        End With
        With grDevolucion.RootTable.Columns("taCatPrecio")
            .Width = 90
            .Visible = False
        End With

        With grDevolucion.RootTable.Columns("dbfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grDevolucion.RootTable.Columns("dbhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grDevolucion.RootTable.Columns("dbuact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grDevolucion
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
        End With

        'If (dt.Rows.Count <= 0) Then
        '    _prCargarDetalleVenta(-1)
        'End If
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

    End Sub

    Private Sub grDevolucion_KeyDown(sender As Object, e As KeyEventArgs) Handles grDevolucion.KeyDown
        If e.KeyData = Keys.Enter Then
            If cbConcepto.Value = 5 Then
                tbIdDevolucion.Text = grDevolucion.GetValue("dbnumi")
                tbMonto.Value = grDevolucion.GetValue("dbtotal")
            Else
                tbIdDevolucion.Text = grDevolucion.GetValue("tanumi")
                tbMonto.Value = grDevolucion.GetValue("saldo")
            End If
            SuperTabPrincipal.SelectedTabIndex = 0

        End If
    End Sub

#End Region

End Class