Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System.IO
Imports Janus.Windows.GridEX
Public Class Pr_ReporteMovimientoBanco

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

        tbBanco.ReadOnly = True
        tbBanco.Enabled = False
        CheckTodosBancos.CheckValue = True


    End Sub
    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)

        If (CheckTodosBancos.Checked) Then
            _dt = L_prReporteTodosMovimientoBancos(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
        End If


        If (CheckUnBanco.Checked) Then
            If (tbBanco.SelectedIndex >= 0) Then
                _dt = L_prReporteUnMovimientoBancos(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), tbBanco.Value)
            End If
        End If


    End Sub

    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then



            Dim objrep As New R_MovimientoBancos
            objrep.SetDataSource(_dt)
            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)
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

    Private Sub Pr_ReporteMovimientoBanco_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
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

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
    End Sub

    Private Sub CheckUnBanco_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckUnBanco.CheckValueChanged
        If (CheckUnBanco.Checked) Then
            CheckTodosBancos.CheckValue = False
            tbBanco.Enabled = True
            tbBanco.BackColor = Color.White
            tbBanco.Focus()
            tbBanco.ReadOnly = False
            _prCargarComboBanco(tbBanco)
            If (CType(tbBanco.DataSource, DataTable).Rows.Count > 0) Then
                tbBanco.SelectedIndex = 0

            End If
        End If
    End Sub

    Private Sub CheckTodosBancos_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckTodosBancos.CheckValueChanged
        If (CheckTodosBancos.Checked) Then
            CheckUnBanco.CheckValue = False
            tbBanco.Enabled = True
            tbBanco.BackColor = Color.Gainsboro
            tbBanco.ReadOnly = True
            _prCargarComboBanco(tbBanco)
            CType(tbBanco.DataSource, DataTable).Rows.Clear()
            tbBanco.SelectedIndex = -1

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

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class