Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Public Class Pr_ReporteIngresoEgreso2

    Private Sub _prIniciarTodo()

        Me.Text = "R E P O R T E   P A R T E   D I A R I O   D E  C A J A"
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
            .DropDownList.Columns.Add("aabdes").Width = 300
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
        'Dim _dt As New DataTable
        '_prInterpretarDatos(_dt)

        Dim dt2 As DataTable = L_prIngresoEgresoSaldo2(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), cbSucursal.Value)
        If dt2.Rows.Count > 0 Then

            Dim objrep As New R_ReporteIngresosEgresos2
            objrep.SetDataSource(dt2)

            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("sucursal", cbSucursal.Text)
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
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        If cbSucursal.SelectedIndex < 0 Then
            ToastNotification.Show(Me, "DEBE ELEGIR UNA SUCURSAL..!!!",
                                      My.Resources.INFORMATION, 2000,
                                      eToastGlowColor.Blue,
                                      eToastPosition.TopCenter)
        Else
            _prCargarReporte()
        End If


    End Sub

    Private Sub Pr_ReporteIngresoEgreso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub
End Class