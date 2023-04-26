Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar

Public Class F1_TraspasoDinero

    Private Sub iniciarTodo()

        Me.Text = "T R A S P A S O  D E  D I N E R O"
        _prCargarComboLibreriaSucursal(cbSucursal)

        btnModificar.Visible = False
        btnEliminar.Visible = False

        tbCodigo.ReadOnly = True
        tbUsuario.ReadOnly = True
        tbFecha.Value = Now.Date
        tbFecha.Enabled = False
        If (gi_NumiVenedor > 0) Then

            Dim dt As DataTable
            dt = L_fnListarEmpleado()
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                If (dt.Rows(i).Item("ydnumi") = gi_NumiVenedor) Then
                    tbUsuario.Text = dt.Rows(i).Item("yddesc")
                End If

            Next

        End If
    End Sub
    Private Sub F1_TraspasoDinero_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iniciarTodo()
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

    Public Function GuardarNuevo() As Boolean

        L_prTraspasoGrabar(tbFecha.Value, gi_userSuc, cbSucursal.Value, tbMonto.Text, tbObservacion.Text)

        ToastNotification.Show(Me, "Codigo de Ingreso/Egreso".ToUpper + tbCodigo.Text + " Grabado con éxito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)


    End Function
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        GuardarNuevo()
    End Sub
End Class