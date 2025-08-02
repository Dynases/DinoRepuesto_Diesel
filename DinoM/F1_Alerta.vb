Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar.Controls
Imports Modelo.MGlobal
Imports Logica.AccesoLogica
Public Class F1_Alerta
    Private Sub F1_Alerta_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        _PMIniciarTodo()
        iniciarComponentes()
    End Sub

    Private Sub iniciarComponentes()
        btnNuevo.Visible = False
        btnGrabar.Visible = False
        btnModificar.Visible = False
        btnEliminar.Visible = False


        With JGrM_Buscador.RootTable.Columns("Tipo")
            .Width = 200
            .Caption = "TIPO"
            .Visible = True

        End With
        With JGrM_Buscador.RootTable.Columns("Codigo")
            .Width = 70
            .TextAlignment = 3
            .Caption = "CODIGO"
            .Visible = True

        End With

        With JGrM_Buscador.RootTable.Columns("Detalle")
            .Width = 200
            .Caption = "DETALLE"
            .Visible = True

        End With
        With JGrM_Buscador.RootTable.Columns("saldo")
            .Width = 150
            .TextAlignment = 3
            .Caption = "SALDO"
            .Visible = True

        End With
        With JGrM_Buscador.RootTable.Columns("S_Minimo")
            .Width = 150
            .TextAlignment = 3
            .Caption = "S. MINIMO"
            .Visible = True

        End With
        With JGrM_Buscador.RootTable.Columns("nFactura")
            .Width = 150
            .TextAlignment = 3
            .Caption = "Nº FACTURA"
            .Visible = True

        End With
        With JGrM_Buscador.RootTable.Columns("FechaCompraVenta")
            .Width = 150
            .TextAlignment = 2
            .Caption = "FECHA"
            .Visible = True

        End With
        With JGrM_Buscador.RootTable.Columns("Vencimiento")
            .Width = 150
            .TextAlignment = 2
            .Caption = "VENCIMIENTO"
            .Visible = True

        End With
        With JGrM_Buscador.RootTable.Columns("Mora")
            .Width = 100
            .TextAlignment = 3
            .Caption = "MORA"
            .Visible = True

        End With

        Dim cont As Integer = 0
        For Each fila As GridEXRow In JGrM_Buscador.GetRows()
            If (fila.Position < JGrM_Buscador.RowCount - 1) Then
                'fila.RowStyle = New GridEXFormatStyle()
                'fila.Cells("Tipo").Row.Format.
            End If
            cont += 1
        Next
    End Sub

    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_prCargarAlertas()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)
        'a.ydnumi, a.ydcod, a.yddesc, a.ydzona, a.yddct, a.yddctnum, a.yddirec, a.ydtelf1, a.ydtelf2, a.ydcat,
        'a.ydest, a.ydlat, a.ydlongi, a.ydprconsu, a.ydobs, a.ydfnac, a.ydnomfac, a.ydtip, a.ydnit, a.ydfecing, a.ydultvent,
        'a.ydimg,
        'a.ydfact, a.ydhact, a.yduact

        listEstCeldas.Add(New Modelo.Celda("Tipo", True, "Tipo".ToUpper, 200))
        listEstCeldas.Add(New Modelo.Celda("codigo", True, "Codigo".ToUpper, 80))
        listEstCeldas.Add(New Modelo.Celda("ItemNuevo", True, "Item Nuevo".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("ItemAntiguo", True, "Item Antiguo".ToUpper, 100))
        listEstCeldas.Add(New Modelo.Celda("Detalle", True, "Detalle".ToUpper, 200))
        listEstCeldas.Add(New Modelo.Celda("saldo", True, "Saldo".ToUpper, 150, "0.00"))
        listEstCeldas.Add(New Modelo.Celda("S_Minimo", True, "S. Minimo".ToUpper, 150, "0.00"))
        listEstCeldas.Add(New Modelo.Celda("nFactura", True, "Factura".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("FechaCompraVenta", True, "Fecha".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("Vencimiento", True, "Vencimiento".ToUpper, 150,))
        listEstCeldas.Add(New Modelo.Celda("Mora", True, "Mora".ToUpper, 80))
        Return listEstCeldas
    End Function

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click, ButtonX1.Click
        Dim frm As New Pr_ReporteAlertas
        frm.Show()
    End Sub



    Private Sub JGrM_Buscador_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles JGrM_Buscador.MouseDoubleClick
        If (JGrM_Buscador.RowCount >= 0) Then
            If (JGrM_Buscador.CurrentColumn.Index = JGrM_Buscador.RootTable.Columns("Codigo").Index) Then
                Dim tipo As String = JGrM_Buscador.GetValue("Tipo")
                If tipo = "CUENTAS POR COBRAR" Then
                    Dim id As Integer = CInt(JGrM_Buscador.GetValue("Codigo"))
                    gs_ComVenPro = id
                    Dim frm As New F0_Ventas
                    frm._nameButton = DinoM.P_Principal.btVentVenta.Name

                    'frm._modulo = FP_VENTAS
                    frm.Show()
                End If
                If tipo = "CUENTAS POR PAGAR" Then
                    Dim id As Integer = CInt(JGrM_Buscador.GetValue("Codigo"))
                    gs_ComVenPro = id
                    Dim frm As New F0_MCompras
                    frm._nameButton = DinoM.P_Principal.btComCompra.Name

                    'frm._modulo = FP_VENTAS
                    frm.Show()
                End If
            End If
        End If
    End Sub

    Private Sub btnCobrar_Click(sender As Object, e As EventArgs) Handles btnCobrar.Click
        _PMIniciarTodo()
        iniciarComponentes()
    End Sub
End Class