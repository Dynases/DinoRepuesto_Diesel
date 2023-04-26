Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controlsdyna
Public Class Pr_ReporteIngresoEgreso

    Private Sub _prIniciarTodo()

        Me.Text = "R E P O R T E   I N G R E S O S / E G R E S O S"
        _prCargarComboLibreriaSucursal(cbSucursal)
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
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

    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)


        _dt = L_prIngresoEgresoEntreFecha(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), 1, cbSucursal.Value)



    End Sub
    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)
        'If (_dt.Rows.Count > 0) Then
        Dim saldoBs, SaldoSus As Decimal

            Dim dt2 As DataTable = L_prIngresoEgresoSaldo(tbFechaI.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
            If dt2.Rows.Count > 0 Then
                saldoBs = dt2.Rows(0).Item("ccTotalBs")
                SaldoSus = dt2.Rows(0).Item("ccTotalSus")
            Else
                saldoBs = 0
                SaldoSus = 0
            End If
            Dim objrep As New R_ReporteIngresosEgresos
            objrep.SetDataSource(_dt)
            Dim caja As String = "1"
            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("sucursal", cbSucursal.Text)
            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)
            objrep.SetParameterValue("caja", caja)
            objrep.SetParameterValue("saldoBs", saldoBs)
            objrep.SetParameterValue("saldoSus", SaldoSus)
            MReportViewer.ReportSource = objrep
            MReportViewer.Show()
            MReportViewer.BringToFront()

        'Else
        '    ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
        '                               My.Resources.INFORMATION, 2000,
        '                               eToastGlowColor.Blue,
        '                               eToastPosition.BottomLeft)
        '    MReportViewer.ReportSource = Nothing
        'End If





    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
    End Sub

    Private Sub Pr_ReporteIngresoEgreso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub
End Class