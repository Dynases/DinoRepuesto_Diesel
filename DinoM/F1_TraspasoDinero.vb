Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar

Public Class F1_TraspasoDinero

    Private Sub iniciarTodo()

        Me.Text = "T R A S P A S O  D E  D I N E R O"
        '_prCargarComboLibreriaSucursalDes(cbSucursal)
        '_prCargarComboLibreriaSucursal(cbOrigen)
        _prCargarComboMovimiento(cbMovimiento)
        'cbOrigen.Value = gi_userSuc
        'cbOrigen.ReadOnly = True
        _PMIniciarTodo()
        btnModificar.Visible = False
        btnEliminar.Visible = False
        Inhabilitar()
        tbCodigo.ReadOnly = True
        tbUsuario.ReadOnly = True
        tbFecha.Value = Now.Date
        tbFecha.Enabled = False
        'If (gi_NumiVenedor > 0) Then

        '    Dim dt As DataTable
        '    dt = L_fnListarEmpleado()
        '    For i As Integer = 0 To dt.Rows.Count - 1 Step 1
        '        If (dt.Rows(i).Item("ydnumi") = gi_NumiVenedor) Then
        '            tbUsuario.Text = dt.Rows(i).Item("yddesc")
        '        End If

        '    Next

        'End If
    End Sub

    Private Sub Inhabilitar()
        btnNuevo.Enabled = True
        btnGrabar.Enabled = False
        tbMonto.ReadOnly = True
        tbObservacion1.ReadOnly = True
        cbSucursal.ReadOnly = True
        PanelNavegacion.Enabled = True
        JGrM_Buscador.Enabled = True
        cbMovimiento.ReadOnly = True
        cbOrigen.ReadOnly = True
    End Sub

    Private Sub Habilitar()
        btnNuevo.Enabled = False
        btnGrabar.Enabled = True
        tbMonto.ReadOnly = False
        tbObservacion1.ReadOnly = False
        cbSucursal.ReadOnly = False
        PanelNavegacion.Enabled = False
        JGrM_Buscador.Enabled = False
        cbMovimiento.ReadOnly = False
        cbOrigen.ReadOnly = False
    End Sub
    Private Sub F1_TraspasoDinero_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iniciarTodo()
    End Sub

    Private Sub _prCargarComboLibreriaSucursal(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, tipo As Integer)
        Dim dt As New DataTable
        If tipo = 1 Or tipo = 2 Then
            dt = L_fnListarSucursales()
            With mCombo
                .DropDownList.Columns.Clear()
                .DropDownList.Columns.Add("aanumi").Width = 60
                .DropDownList.Columns("aanumi").Caption = "COD"
                .DropDownList.Columns.Add("aabdes").Width = 500
                .DropDownList.Columns("aabdes").Caption = "ORIGEN"
                .ValueMember = "aanumi"
                .DisplayMember = "aabdes"
                .DataSource = dt
                .Refresh()
                .ReadOnly = True
                .Value = gi_userSuc
            End With
        Else

            dt = L_fnListarBanco()

            With mCombo
                .DropDownList.Columns.Clear()
                .DropDownList.Columns.Add("canumi").Width = 60
                .DropDownList.Columns("canumi").Caption = "COD"
                .DropDownList.Columns.Add("banco").Width = 500
                .DropDownList.Columns("banco").Caption = "ORIGEN"
                .ValueMember = "canumi"
                .DisplayMember = "banco"
                .DataSource = dt
                .Refresh()
                .ReadOnly = False
                .SelectedIndex = 0
            End With

        End If
    End Sub

    Private Sub _prCargarComboMovimiento(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        'dt.Columns.Add("id")
        'dt.Columns.Add("desc")

        'dt.Rows.Add(1, "CAJA  -  CAJA")
        'dt.Rows.Add(2, "CAJA  -  BANCO")
        'dt.Rows.Add(3, "BANCO  -  CAJA")
        'dt.Rows.Add(4, "BANCO  -  BANCO")
        dt = TraerConceptosTraspaso()

        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("id").Width = 60
            .DropDownList.Columns("id").Caption = "COD"
            .DropDownList.Columns.Add("descr").Width = 500
            .DropDownList.Columns("descr").Caption = "CONCEPTO"
            .ValueMember = "id"
            .DisplayMember = "descr"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prCargarComboLibreriaSucursalDes(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, tipo As Integer)
        Dim dt As New DataTable
        If tipo = 1 Or tipo = 3 Then
            If btnGrabar.Enabled = True And tipo = 1 Then
                dt = L_fnListarSucursalesDes(gi_userSuc)
            Else
                dt = L_fnListarSucursales()
            End If
            With mCombo
                .DropDownList.Columns.Clear()
                .DropDownList.Columns.Add("aanumi").Width = 60
                .DropDownList.Columns("aanumi").Caption = "COD"
                .DropDownList.Columns.Add("aabdes").Width = 500
                .DropDownList.Columns("aabdes").Caption = "DESTINO"
                .ValueMember = "aanumi"
                .DisplayMember = "aabdes"
                .DataSource = dt
                .Refresh()
                .Clear()
            End With
        Else

            dt = L_fnListarBanco()

            With mCombo
                .DropDownList.Columns.Clear()
                .DropDownList.Columns.Add("canumi").Width = 60
                .DropDownList.Columns("canumi").Caption = "COD"
                .DropDownList.Columns.Add("banco").Width = 500
                .DropDownList.Columns("banco").Caption = "DESTINO"
                .ValueMember = "canumi"
                .DisplayMember = "banco"
                .DataSource = dt
                .Refresh()
                .Clear()
            End With
        End If

    End Sub

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)


        listEstCeldas.Add(New Modelo.Celda("tdnumi", True, "Codigo", 120))
        listEstCeldas.Add(New Modelo.Celda("tipo", True, "Tipo", 150))
        listEstCeldas.Add(New Modelo.Celda("tdfec", True, "Fecha", 100))
        listEstCeldas.Add(New Modelo.Celda("Tipo", True, "Tipo", 120))
        listEstCeldas.Add(New Modelo.Celda("idori", False, "Tipo", 120))
        listEstCeldas.Add(New Modelo.Celda("ori", True, "Almacen Origen", 250))
        listEstCeldas.Add(New Modelo.Celda("aanumi", False, "Almacen Origen", 250))
        listEstCeldas.Add(New Modelo.Celda("aabdes", True, "Almacen Destino", 250))
        listEstCeldas.Add(New Modelo.Celda("tdmont", True, "Monto", 150, "0.00"))
        listEstCeldas.Add(New Modelo.Celda("tdobs", True, "Observación", 250))
        listEstCeldas.Add(New Modelo.Celda("tdidven", False))
        listEstCeldas.Add(New Modelo.Celda("tdtipo", False))
        Return listEstCeldas

    End Function
    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_prTransferenciaDinero(gi_userSuc)
        Return dtBuscador
    End Function

    Public Overrides Function _PMOValidarCampos() As Boolean
        If cbSucursal.SelectedIndex < 0 Then
            ToastNotification.Show(Me, "Seleccione una sucursal de destino.".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            Return False
        End If
        If tbMonto.Text = "" Then
            ToastNotification.Show(Me, "Ingrese un monto para transferir.".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            Return False
        End If
        Return True
    End Function
    Private Function validarMonto() As Boolean
        Dim dt As DataTable = L_prSaldoAnterior(gi_userSuc)
        If dt.Rows.Count > 0 Then
            Dim saldo As Double = CDbl(dt.Rows(0).Item("Ingreso")) - CDbl(dt.Rows(0).Item("Egreso"))
            If saldo >= CDbl(tbMonto.Text) Then
                Return True
            Else
                ToastNotification.Show(Me, "No hay saldo suficiente para realizar la transferencia.".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                Return False
            End If
        End If
    End Function
    Public Function GuardarNuevo() As Boolean
        If _PMOValidarCampos() Then
            If validarMonto() Then
                Dim res As Boolean = L_prTraspasoGrabar(tbFecha.Value, cbOrigen.Value, cbSucursal.Value, tbMonto.Text, tbObservacion1.Text, gi_NumiVenedor, cbMovimiento.Value, gi_userSuc)
                If res Then
                    ToastNotification.Show(Me, "Transferencia Grabado con éxito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                    '_prCargarComboLibreriaSucursalDes(cbSucursal)
                    Return True
                Else
                    ToastNotification.Show(Me, "La Transferencia no pudo ser grabada.".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                    Return False

                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Overrides Sub _PMOLimpiar()

        tbCodigo.Text = ""
        tbFecha.Value = Date.Now
        tbMonto.Clear()
        tbObservacion1.Clear()
        tbUsuario.Clear()
        cbOrigen.Value = gi_userSuc
        cbSucursal.Clear()
        cbMovimiento.SelectedIndex = 0

    End Sub
    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        With JGrM_Buscador
            cbMovimiento.Value = .GetValue("tdtipo")
            tbCodigo.Text = .GetValue("tdnumi").ToString
            tbFecha.Value = .GetValue("tdfec")
            tbMonto.Text = .GetValue("tdmont")
            tbObservacion1.Text = .GetValue("tdobs")
            Dim dt As DataTable
            dt = L_fnListarEmpleado()
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                If (dt.Rows(i).Item("ydnumi") = .GetValue("tdidven")) Then
                    tbUsuario.Text = dt.Rows(i).Item("yddesc")
                End If

            Next
            cbSucursal.Value = .GetValue("aanumi")
            cbOrigen.Value = .GetValue("idori")

            'diseño de la grilla para el Total
            '.TotalRow = InheritableBoolean.True
            '.TotalRowFormatStyle.BackColor = Color.Gold
            '.TotalRowPosition = TotalRowPosition.BottomFixed
        End With


        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString
        '_prCargarComboLibreriaSucursal(cbSucursal)
    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        'If tbCodigo.Text = "" Then
        '    Inhabilitar()
        '    _PMPrimerRegistro()
        'End If
        '_prCargarComboLibreriaSucursalDes(cbSucursal)
    End Sub

    Private Sub LabelX1_Click(sender As Object, e As EventArgs) Handles LabelX1.Click

    End Sub

    Public Overrides Function _PMOGrabarRegistro() As Boolean

        Dim res As Boolean = GuardarNuevo()
        Return res
    End Function

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If btnGrabar.Enabled = True Then
            Inhabilitar()
            _PMPrimerRegistro()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Habilitar()
        '_prCargarComboLibreriaSucursalDes(cbSucursal)
        cbMovimiento.SelectedIndex = 0
        _PMOLimpiar()
        Dim dt As DataTable
        dt = L_fnListarEmpleado()
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            If (dt.Rows(i).Item("ydnumi") = gi_NumiVenedor) Then
                tbUsuario.Text = dt.Rows(i).Item("yddesc")
            End If

        Next
    End Sub

    Private Sub cbMovimiento_ValueChanged(sender As Object, e As EventArgs) Handles cbMovimiento.ValueChanged
        Dim conc As Integer = cbMovimiento.Value
        _prCargarComboLibreriaSucursal(cbOrigen, conc)
        _prCargarComboLibreriaSucursalDes(cbSucursal, conc)
        'Select Case conc
        '    Case 1
        '        _prCargarComboLibreriaSucursal(cbOrigen, conc)

        '    Case 2

        '    Case 3

        '    Case 4
        'End Select
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        GenerarImpresion()
    End Sub

    Private Sub GenerarImpresion()
        Dim dt As DataTable = L_prTraspasoNota(tbCodigo.Text)

        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If
        Dim total As Decimal = dt.Rows(0).Item("tdmont")
        Dim totald As Double = Math.Round(total / 6.96, 2)
        Dim ParteEntera As Long
        Dim ParteDecimal As Decimal
        Dim pDecimal() As String
        ParteEntera = Int(total)
        ParteDecimal = Math.Round(total - ParteEntera, 2)
        pDecimal = Split(ParteDecimal.ToString, ".")


        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " con " +
        IIf(pDecimal(1).ToString.Equals("0"), "00", pDecimal(1).ToString) + "/100 Bolivianos"

        ParteEntera = Int(totald)
        ParteDecimal = Math.Round(totald - ParteEntera, 2)
        pDecimal = Split(ParteDecimal.ToString, ".")

        Dim lid As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera))

        lid = lid + " con " +
        IIf(pDecimal(1).ToString.Equals("0"), "00", pDecimal(1).ToString) + "/100 Dolares"

        P_Global.Visualizador = New Visualizador

        Dim objrep As New R_TraspasoNota

        objrep.SetDataSource(dt)
        objrep.SetParameterValue("Literal1", li)
        objrep.SetParameterValue("logo", gb_UbiLogo)

        P_Global.Visualizador.CrGeneral.ReportSource = objrep
        P_Global.Visualizador.ShowDialog()
    End Sub
End Class