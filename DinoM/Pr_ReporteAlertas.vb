Imports Logica.AccesoLogica
Public Class Pr_ReporteAlertas
    Private Sub Pr_ReporteAlertas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim _dt As DataTable
        _dt = L_prCargarAlertas()
        Dim objrep As New R_ReporteAlertas
        objrep.SetDataSource(_dt)
        MReportViewer.ReportSource = objrep
        MReportViewer.Show()
        MReportViewer.BringToFront()
    End Sub
End Class