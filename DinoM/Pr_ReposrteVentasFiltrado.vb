Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica

Public Class Pr_ReposrteVentasFiltrado

    Dim marca As Integer
    Dim producto As Integer
    Private Sub iniciarComponentes()

        tbFechaI.Text = Date.Now.ToString
        tbFechaF.Text = Date.Now.ToString
        cbAlmacenTodos.Checked = True
        cbMarcaTodos.Checked = True
        cbProvTodos.Checked = True
        CheckTodosProducto.Checked = True
        cbProv.Enabled = False
        tbMarca.Enabled = False
        cbPrograma.Enabled = False
        tbMarca.ReadOnly = True
        tbProducto.Enabled = False
        _prCargarComboLibreriaDeposito(cbPrograma)
        _prCargarComboProveedores(cbProv)

    End Sub

    Private Function _prInterpretarDatos() As DataTable
        'Dim dt As DataTable
        'If cbAlmacenTodos.Checked = True And cbMarcaTodos.Checked = True And cbProvTodos.Checked = True Then
        '    dt = L_prVentasTodos(tbFechaI.Value.ToString("yyyy-MM-dd"), tbFechaF.Value.ToString("yyyy-MM-dd"))
        '    Return dt
        'End If
        'If cbAlmacenTodos.Checked = False And cbMarcaTodos.Checked = True And cbProvTodos.Checked = True Then
        '    dt = L_prVentasAlmacen(tbFechaI.Value.ToString("yyyy-MM-dd"), tbFechaF.Value.ToString("yyyy-MM-dd"), cbPrograma.Value)
        '    Return dt
        'End If
        'If cbAlmacenTodos.Checked = True And cbMarcaTodos.Checked = False And cbProvTodos.Checked = True Then
        '    dt = L_prVentasMarca(tbFechaI.Value.ToString("yyyy-MM-dd"), tbFechaF.Value.ToString("yyyy-MM-dd"), marca)
        '    Return dt
        'End If
        'If cbAlmacenTodos.Checked = False And cbMarcaTodos.Checked = False And cbProvTodos.Checked = True Then
        '    dt = L_prVentasAlmacenMarca(tbFechaI.Value.ToString("yyyy-MM-dd"), tbFechaF.Value.ToString("yyyy-MM-dd"), cbPrograma.Value, marca)
        '    Return dt
        'End If
        'If cbAlmacenTodos.Checked = True And cbMarcaTodos.Checked = True And cbProvTodos.Checked = False Then
        '    dt = L_prVentasProveedor(tbFechaI.Value.ToString("yyyy-MM-dd"), tbFechaF.Value.ToString("yyyy-MM-dd"), cbProv.Value)
        '    Return dt
        'End If
        'If cbAlmacenTodos.Checked = False And cbMarcaTodos.Checked = True And cbProvTodos.Checked = False Then
        '    dt = L_prVentasProveedorAlmacen(tbFechaI.Value.ToString("yyyy-MM-dd"), tbFechaF.Value.ToString("yyyy-MM-dd"), cbProv.Value, cbPrograma.Value)
        '    Return dt
        'End If
        'If cbAlmacenTodos.Checked = True And cbMarcaTodos.Checked = False And cbProvTodos.Checked = False Then
        '    dt = L_prVentasProveedorMarca(tbFechaI.Value.ToString("yyyy-MM-dd"), tbFechaF.Value.ToString("yyyy-MM-dd"), cbProv.Value, marca)
        '    Return dt
        'End If
        'If cbAlmacenTodos.Checked = False And cbMarcaTodos.Checked = False And cbProvTodos.Checked = False Then
        Dim dt As DataTable = L_prVentasProveedorMarcaAlmacen(tbFechaI.Value.ToString("yyyy-MM-dd"), tbFechaF.Value.ToString("yyyy-MM-dd"), IIf(cbProvUno.Checked, cbProv.Value, -1), IIf(cbMarcaUno.Checked, marca, -1), IIf(cbAlmacenUno.Checked, cbPrograma.Value, -1), IIf(CheckTodosProducto.Checked = True, -1, producto))
        Return dt
        'End If
    End Function
    Private Sub _prCargarComboProveedores(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarCategoriaProducto()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ydcod").Width = 60
            .DropDownList.Columns("ydcod").Caption = "COD"
            .DropDownList.Columns.Add("yddesc").Width = 500
            .DropDownList.Columns("yddesc").Caption = "CATEGORIA"
            .ValueMember = "ydcod"
            .DisplayMember = "yddesc"
            .DataSource = dt
            .Refresh()
        End With

        If dt.Rows.Count > 0 Then
            mCombo.SelectedIndex = 0
        End If

    End Sub

    Private Sub _prCargarComboLibreriaDeposito(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarDepositos()
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

        If dt.Rows.Count > 0 Then
            mCombo.SelectedIndex = 0
        End If
    End Sub

    Private Function validarCampos() As Boolean
        If cbMarcaUno.Checked = True Then
            If tbMarca.Text = "" Then
                Return False
            End If
        End If
        Return True
    End Function
    Private Sub cargarReporte()
        If validarCampos() = False Then
            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

            ToastNotification.Show(Me, "Debe llenar todos los campos para el reporte.".ToUpper,
                                                  img, 2000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.BottomLeft)
            Exit Sub
        End If
        Dim dt As DataTable
        dt = _prInterpretarDatos()
        If dt.Rows.Count = 0 Then
            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

            ToastNotification.Show(Me, "No hay datos para los parametros seleccionados.".ToUpper,
                                                  img, 2000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.BottomLeft)
        Else
            Dim objrep As New R_VentasFiltradas
            objrep.SetDataSource(dt)
            Dim FechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim FechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")

            objrep.SetParameterValue("FechaI", FechaI)
            objrep.SetParameterValue("FechaF", FechaF)
            objrep.SetParameterValue("logo", gb_UbiLogo)
            objrep.SetParameterValue("almacen", IIf(cbAlmacenTodos.Checked, "TODOS", cbPrograma.Text))
            MReportViewer.ReportSource = objrep
            MReportViewer.Show()
            MReportViewer.BringToFront()
        End If

    End Sub
    Private Sub LabelX1_Click(sender As Object, e As EventArgs) Handles LabelX1.Click

    End Sub

    Private Sub Pr_ReposrteVentasFiltrado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iniciarComponentes()
    End Sub

    Private Sub cbProvUno_CheckedChanged(sender As Object, e As EventArgs) Handles cbProvUno.CheckedChanged
        If cbProvUno.Checked = True Then
            If cbProvTodos.Checked = True Then
                cbProvTodos.Checked = False
                cbProv.Enabled = True

            End If
        Else
            If cbProvTodos.Checked = False Then
                cbProvUno.Checked = True
            End If
        End If
    End Sub

    Private Sub cbProvTodos_CheckedChanged(sender As Object, e As EventArgs) Handles cbProvTodos.CheckedChanged
        If cbProvTodos.Checked = True Then
            If cbProvUno.Checked = True Then
                cbProvUno.Checked = False
                cbProv.Enabled = False
            End If
        Else
            If cbProvUno.Checked = False Then
                cbProvTodos.Checked = True
            End If
        End If
    End Sub

    Private Sub cbMarcaTodos_CheckedChanged(sender As Object, e As EventArgs) Handles cbMarcaTodos.CheckedChanged
        If cbMarcaTodos.Checked = True Then
            If cbMarcaUno.Checked = True Then
                cbMarcaUno.Checked = False
                tbMarca.Enabled = False
            End If
        Else
            If cbMarcaUno.Checked = False Then
                cbMarcaTodos.Checked = True
            End If
        End If
    End Sub

    Private Sub cbMarcaUno_CheckedChanged(sender As Object, e As EventArgs) Handles cbMarcaUno.CheckedChanged
        If cbMarcaUno.Checked = True Then
            If cbMarcaTodos.Checked = True Then
                cbMarcaTodos.Checked = False
                tbMarca.Enabled = True

            End If
        Else
            If cbMarcaTodos.Checked = False Then
                cbMarcaUno.Checked = True
            End If
        End If
    End Sub

    Private Sub cbAlmacenTodos_CheckedChanged(sender As Object, e As EventArgs) Handles cbAlmacenTodos.CheckedChanged
        If cbAlmacenTodos.Checked = True Then
            If cbAlmacenUno.Checked = True Then
                cbAlmacenUno.Checked = False
                cbPrograma.Enabled = False
            End If
        Else
            If cbAlmacenUno.Checked = False Then
                cbAlmacenTodos.Checked = True
            End If
        End If
    End Sub

    Private Sub cbAlmacenUno_CheckedChanged(sender As Object, e As EventArgs) Handles cbAlmacenUno.CheckedChanged
        If cbAlmacenUno.Checked = True Then
            If cbAlmacenTodos.Checked = True Then
                cbAlmacenTodos.Checked = False
                cbPrograma.Enabled = True

            End If
        Else
            If cbAlmacenTodos.Checked = False Then
                cbAlmacenUno.Checked = True
            End If
        End If
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        cargarReporte()
    End Sub

    Private Sub tbMarca_KeyDown(sender As Object, e As KeyEventArgs) Handles tbMarca.KeyDown
        If (cbMarcaUno.Checked) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim dt As DataTable
                dt = L_fnListarMarcas()

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("yccod3", True, "CODIGO", 50))
                listEstCeldas.Add(New Modelo.Celda("ycdes3", True, "MARCA", 200))

                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione Marca".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        tbMarca.Focus()
                        Return
                    End If
                    tbMarca.Text = Row.Cells("ycdes3").Value
                    marca = CInt(Row.Cells("yccod3").Value)
                    'btnGenerar.Focus()
                End If

            End If

        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub tbProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles tbProducto.KeyDown
        If (CheckUnoProducto.Checked) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim dt As DataTable
                dt = L_fnListarProductosFiltro()

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("yfnumi", False))
                listEstCeldas.Add(New Modelo.Celda("yfCodAux1", True, "ITEM NUEVO", 50))
                listEstCeldas.Add(New Modelo.Celda("yfcprod", True, "COD. FABRICA", 120))
                listEstCeldas.Add(New Modelo.Celda("yfdetprod", True, "DESCRIPCION", 600))
                listEstCeldas.Add(New Modelo.Celda("ycdes3", True, "MARCA", 120))

                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione un Producto".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        tbProducto.Focus()
                        Return
                    End If
                    tbProducto.Text = Row.Cells("yfdetprod").Value
                    producto = CInt(Row.Cells("yfnumi").Value)
                    'btnGenerar.Focus()
                End If

            End If

        End If
    End Sub

    Private Sub CheckTodosProducto_CheckedChanged(sender As Object, e As EventArgs) Handles CheckTodosProducto.CheckedChanged
        If CheckTodosProducto.Checked = True Then
            If CheckUnoProducto.Checked = True Then
                CheckUnoProducto.Checked = False
                tbProducto.Enabled = False
            End If
        Else
            If CheckUnoProducto.Checked = False Then
                CheckTodosProducto.Checked = True
                tbProducto.Enabled = True
            End If
        End If
    End Sub

    Private Sub CheckUnoProducto_CheckedChanged(sender As Object, e As EventArgs) Handles CheckUnoProducto.CheckedChanged
        If CheckUnoProducto.Checked = True Then
            If CheckTodosProducto.Checked = True Then
                CheckTodosProducto.Checked = False
                tbProducto.Enabled = True
            End If
        Else
            If CheckTodosProducto.Checked = False Then
                CheckUnoProducto.Checked = True
                tbProducto.Enabled = False
            End If
        End If
    End Sub
End Class