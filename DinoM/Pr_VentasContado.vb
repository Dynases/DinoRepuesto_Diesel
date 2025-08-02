Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Public Class Pr_VentasContado
    Dim _Inter As Integer = 0

    'gb_FacturaIncluirICE


    Public _nameButton As String
    Public _tab As SuperTabItem
    Public Sub _prIniciarTodo()
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        _PMIniciarTodo()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        Me.Text = "REPORTE VENTAS AL CONTADO"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        _IniciarComponentes()
    End Sub
    Public Sub _IniciarComponentes()

        tbAlmacen.ReadOnly = True
        tbAlmacen.Enabled = False
        CheckTodosAlmacen.CheckValue = True
        _prCargarComboLibreriaTVenta(cbTipVenta)

    End Sub
    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)

        If (CheckTodosAlmacen.Checked) Then
            _dt = L_prVentasContadoTodosAlmacenes(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbTipVenta.Value)
        End If


        If (CheckUnaALmacen.Checked) Then
            If (tbAlmacen.SelectedIndex >= 0) Then
                _dt = L_prVentasContadoUnAlmacen(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), tbAlmacen.Value, cbTipVenta.Value)
            End If
        End If


    End Sub
    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then
            Dim contado As Double = 0
            Dim credito As Double = 0
            For i = 0 To _dt.Rows.Count - 1 Step 1
                If _dt.Rows(i).Item("tipo") = "CON" Then
                    contado = IIf((_dt.Compute("Sum(tbtotdesc)", "(tipo = 'CON')")) Is Nothing, 0, _dt.Compute("Sum(tbtotdesc)", "(tipo = 'CON')"))
                    Exit For
                End If
            Next
            For i = 0 To _dt.Rows.Count - 1 Step 1
                If _dt.Rows(i).Item("tipo") = "CRED" Then
                    credito = IIf((_dt.Compute("Sum(tbtotdesc)", "(tipo = 'CRED')")) Is Nothing, 0, _dt.Compute("Sum(tbtotdesc)", "(tipo = 'CRED')"))
                    Exit For
                End If
            Next

            Dim objrep As New R_VentasContado
            objrep.SetDataSource(_dt)
            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)
            objrep.SetParameterValue("contado", contado)
            objrep.SetParameterValue("credito", credito)
            MReportViewer.ReportSource = objrep
            MReportViewer.Show()
            MReportViewer.BringToFront()

        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                       My.Resources.INFORMATION, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            MReportViewer.ReportSource = Nothing
        End If





    End Sub
    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()

    End Sub

    Private Sub Pr_VentasAtendidas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

    End Sub




    Private Sub CheckUnaALmacen_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckUnaALmacen.CheckValueChanged
        If (CheckUnaALmacen.Checked) Then
            CheckTodosAlmacen.CheckValue = False
            tbAlmacen.Enabled = True
            tbAlmacen.BackColor = Color.White
            tbAlmacen.Focus()
            tbAlmacen.ReadOnly = False
            _prCargarComboLibreriaSucursal(tbAlmacen)
            If (CType(tbAlmacen.DataSource, DataTable).Rows.Count > 0) Then
                tbAlmacen.SelectedIndex = 0

            End If
        End If
    End Sub

    Private Sub CheckTodosAlmacen_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckTodosAlmacen.CheckValueChanged
        If (CheckTodosAlmacen.Checked) Then
            CheckUnaALmacen.CheckValue = False
            tbAlmacen.Enabled = True
            tbAlmacen.BackColor = Color.Gainsboro
            tbAlmacen.ReadOnly = True
            _prCargarComboLibreriaSucursal(tbAlmacen)
            CType(tbAlmacen.DataSource, DataTable).Rows.Clear()
            tbAlmacen.SelectedIndex = -1

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

    Private Sub _prCargarComboLibreriaTVenta(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt.Columns.Add("COD", GetType(Integer))
        dt.Columns.Add("TIPO", GetType(String))

        dt.Rows.Add(0, "CREDITO")
        dt.Rows.Add(1, "CONTADO")

        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("COD").Width = 60
            .DropDownList.Columns("COD").Caption = "COD"
            .DropDownList.Columns.Add("TIPO").Width = 500
            .DropDownList.Columns("TIPO").Caption = "SUCURSAL"
            .ValueMember = "COD"
            .DisplayMember = "TIPO"
            .DataSource = dt
            .Refresh()
        End With

        mCombo.SelectedIndex = 0
    End Sub



    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal

        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub CheckUnaALmacen_CheckedChanged(sender As Object, e As EventArgs) Handles CheckUnaALmacen.CheckedChanged

    End Sub
End Class