
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports GMap.NET.MapProviders
Imports GMap.NET
Imports GMap.NET.WindowsForms.Markers
Imports GMap.NET.WindowsForms
Imports GMap.NET.WindowsForms.ToolTips
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text

Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Data.OleDb

Public Class F0_ImportarPrecios
    Dim _Inter As Integer = 0

    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim PreciosImport As New DataTable
#Region "Variables Globales"
    Dim precio As DataTable
    Public _nameButton As String
    Public _modulo As SideNavItem
    Public _tab As SuperTabItem
#End Region
#Region "MEtodos Privados"
    Private Sub _IniciarTodo()

        'Me.WindowState = FormWindowState.Maximized



        _prAsignarPermisos()
        Me.Text = "PRECIOS"
        Dim blah As New Bitmap(New Bitmap(My.Resources.precio), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
    End Sub
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


    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnGeneralSucursales()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 70
            .DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("aabdes").Width = 200
            .DropDownList.Columns("aabdes").Caption = "DESCRIPCION"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With

    End Sub
    Private Sub _prInhabiliitar()


        btnModificar.Enabled = True
        btnGrabar.Enabled = False

    End Sub
    Private Sub _prhabilitar()

        btnGrabar.Enabled = True
    End Sub


    Public Function _fnAccesible()
        Return btnModificar.Enabled = False
    End Function

    Private Function _FSiguienteLetra(palabra As String) As String
        Dim alfabeto As New List(Of String)
        alfabeto.Add("A")
        alfabeto.Add("B")
        alfabeto.Add("C")
        alfabeto.Add("D")
        alfabeto.Add("E")
        alfabeto.Add("F")
        alfabeto.Add("G")
        alfabeto.Add("H")
        alfabeto.Add("I")
        alfabeto.Add("J")
        alfabeto.Add("K")
        alfabeto.Add("L")
        alfabeto.Add("M")
        alfabeto.Add("N")
        alfabeto.Add("O")
        alfabeto.Add("P")
        alfabeto.Add("Q")
        alfabeto.Add("R")
        alfabeto.Add("S")
        alfabeto.Add("T")
        alfabeto.Add("U")
        alfabeto.Add("V")
        alfabeto.Add("W")
        alfabeto.Add("X")
        alfabeto.Add("Y")
        alfabeto.Add("Z")
        Dim letra As String
        If palabra.Length = 1 Then
            letra = palabra(0)
            '26 letras en el alphabeto
            If alfabeto.IndexOf(letra) = 25 Then
                palabra = "AA"
            Else
                palabra = alfabeto(alfabeto.IndexOf(letra) + 1)
            End If
        Else
            letra = palabra(1)
            If alfabeto.IndexOf(letra) = 25 Then
                palabra = ""
            Else
                palabra = palabra(0) + alfabeto(alfabeto.IndexOf(letra) + 1)
            End If
        End If
        Return palabra
    End Function


    Public Sub _prLimpiar()
        grprecio.DataSource = TraerPrecioProducto(-1, 0, 0, 0, 0)
    End Sub









    Public Function _fnSiguienteNumero(num As Integer)
        Return num + 1
    End Function

#End Region


#Region "MEtodoso Formulario"
    Private Sub F0_Precios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
        _prInhabiliitar()
    End Sub
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        _prhabilitar()
        btnModificar.Enabled = False

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If (_fnAccesible()) Then
            _prInhabiliitar()
        Else
            _modulo.Select()
            Me.Close()
        End If
    End Sub
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs)



    End Sub
    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs)
        Dim tb As TextBoxX = CType(sender, TextBoxX)
        If tb.Text = String.Empty Then

        Else
            tb.BackColor = Color.White
            MEP.SetError(tb, "")
        End If
    End Sub
    Private Sub grprecio_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grprecio.CellEdited
        If (_fnAccesible()) Then
            'Habilitar solo las columnas de Precio, %, Monto y Observación
            'If (e.Column.Index > 1) Then
            '    Dim data As String = grprecio.GetValue(e.Column.Index - 1).ToString.Trim 'En esta columna obtengo un protocolo que me indica el estado del precio 0= no insertado 1= ya insertado , a la ves con un '-' me indica la posicion de ese dato en el Datatable que envio para grabarlo que esta en 'precio' Ejemplo:1-15 -> estado=1 posicion=15
            '    Dim estado As String = data.Substring(0, 1).Trim
            '    Dim pos As String = data.Substring(2, data.Length - 2)




            'End If

        End If
    End Sub

    Private Sub grprecio_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grprecio.EditingCell
        'e.Cancel = True
        If btnGrabar.Enabled = False Then
            e.Cancel = True
        Else
            If (e.Column.Index = grprecio.RootTable.Columns("costo").Index) Or e.Column.Index = grprecio.RootTable.Columns("taller").Index Or e.Column.Index = grprecio.RootTable.Columns("publico").Index Or e.Column.Index = grprecio.RootTable.Columns("gdb").Index Then 'Or e.Column.Index = grprecio.RootTable.Columns("73").Index
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
        'If (_fnAccesible() And IsNothing(grprecio.DataSource) = False) Then
        '    'Deshabilitar la columna de Productos y solo habilitar la de los precios
        '    If (e.Column.Index = grprecio.RootTable.Columns("yfcdprod1").Index) Then 'Or e.Column.Index = grprecio.RootTable.Columns("73").Index
        '        e.Cancel = True
        '    Else
        '        e.Cancel = False
        '    End If
        'Else
        '    e.Cancel = True
        'End If
    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        For i = 0 To CType(grprecio.DataSource, DataTable).Rows.Count - 1
            Dim codPro As Integer = CType(grprecio.DataSource, DataTable).Rows(i).Item("yfnumi")
            Dim costo As Double = CType(grprecio.DataSource, DataTable).Rows(i).Item("costo")
            Dim taller As Double = CType(grprecio.DataSource, DataTable).Rows(i).Item("taller")
            Dim publico As Double = CType(grprecio.DataSource, DataTable).Rows(i).Item("publico")
            Dim gdb As Double = CType(grprecio.DataSource, DataTable).Rows(i).Item("gdb")
            ActualizarPrecioProducto(codPro, costo, taller, publico, gdb)
        Next
        Dim grabar As Boolean = True
        If grabar Then
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Precios actualizados con exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            _prLimpiar()


            _prInhabiliitar()

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La categoria no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If

    End Sub



    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

    End Sub
#End Region



    Private Sub grcategoria_FormattingRow(sender As Object, e As RowLoadEventArgs)

    End Sub
    Public Function P_ExportarExcel(_ruta As String) As Boolean
        Dim _ubicacion As String
        'Dim _directorio As New FolderBrowserDialog

        If (1 = 1) Then 'If(_directorio.ShowDialog = Windows.Forms.DialogResult.OK) Then
            '_ubicacion = _directorio.SelectedPath
            _ubicacion = _ruta
            Try
                Dim _stream As Stream
                Dim _escritor As StreamWriter
                Dim _fila As Integer = grprecio.GetRows.Length
                Dim _columna As Integer = grprecio.RootTable.Columns.Count
                Dim _archivo As String = _ubicacion & "\ListaDePrecios_" & Now.Date.Day &
                    "." & Now.Date.Month & "." & Now.Date.Year & "_" & Now.Hour & "." & Now.Minute & "." & Now.Second & ".csv"
                Dim _linea As String = ""
                Dim _filadata = 0, columndata As Int32 = 0
                File.Delete(_archivo)
                _stream = File.OpenWrite(_archivo)
                _escritor = New StreamWriter(_stream, System.Text.Encoding.UTF8)

                For Each _col As GridEXColumn In grprecio.RootTable.Columns
                    If (_col.Visible) Then
                        _linea = _linea & _col.Caption & ";"
                    End If
                Next
                _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                _escritor.WriteLine(_linea)
                _linea = Nothing

                'Pbx_Precios.Visible = True
                'Pbx_Precios.Minimum = 1
                'Pbx_Precios.Maximum = Dgv_Precios.RowCount
                'Pbx_Precios.Value = 1

                For Each _fil As GridEXRow In grprecio.GetRows
                    For Each _col As GridEXColumn In grprecio.RootTable.Columns
                        If (_col.Visible) Then
                            Dim data As String = CStr(_fil.Cells(_col.Key).Value)
                            data = data.Replace(";", ",")
                            _linea = _linea & data & ";"
                        End If
                    Next
                    _linea = Mid(CStr(_linea), 1, _linea.Length - 1)
                    _escritor.WriteLine(_linea)
                    _linea = Nothing
                    'Pbx_Precios.Value += 1
                Next
                _escritor.Close()
                'Pbx_Precios.Visible = False
                Try
                    Dim ef = New Efecto
                    ef._archivo = _archivo

                    ef.tipo = 1
                    ef.Context = "Su archivo ha sido Guardado en la ruta: " + _archivo + vbLf + "DESEA ABRIR EL ARCHIVO?"
                    ef.Header = "PREGUNTA"
                    ef.ShowDialog()
                    Dim bandera As Boolean = False
                    bandera = ef.band
                    If (bandera = True) Then
                        Process.Start(_archivo)
                    End If

                    'If (MessageBox.Show("Su archivo ha sido Guardado en la ruta: " + _archivo + vbLf + "DESEA ABRIR EL ARCHIVO?", "PREGUNTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
                    '    Process.Start(_archivo)
                    'End If
                    Return True
                Catch ex As Exception
                    MsgBox(ex.Message)
                    Return False
                End Try
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End If
        Return False
    End Function
    Private Sub _prCrearCarpetaReportes()

        Dim rutaDestino As String = RutaGlobal + "\Reporte\Reporte Precios\"

        If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Precios\") = False Then
            If System.IO.Directory.Exists(RutaGlobal + "\Reporte") = False Then
                System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte")
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Precios") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Precios")
                End If
            Else
                If System.IO.Directory.Exists(RutaGlobal + "\Reporte\Reporte Precios") = False Then
                    System.IO.Directory.CreateDirectory(RutaGlobal + "\Reporte\Reporte Precios")

                End If
            End If
        End If
    End Sub
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        PreciosImport.Clear()
        ImportarExcel()
    End Sub

    Private Sub ImportarExcel()
        Try
            Dim folder As String = ""
            Dim doc As String = "Hoja1"
            Dim openfile1 As OpenFileDialog = New OpenFileDialog()

            If openfile1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                folder = openfile1.FileName
            End If

            If True Then
                Dim pathconn As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & folder & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"

                Dim con As OleDbConnection = New OleDbConnection(pathconn)
                Dim MyDataAdapter As OleDbDataAdapter = New OleDbDataAdapter("Select * from [" & doc & "$]", con)
                con.Open()

                MyDataAdapter.Fill(PreciosImport)
                CargarDatosExcel(PreciosImport)

                con.Close()

            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Function ArmarDatos(dt As DataTable) As DataTable
        Dim DtPrecios As DataTable = TraerPrecioProducto(-1, 0, 0, 0, 0)
        For i = 0 To dt.Rows.Count - 1 Step 1
            Dim fila As DataTable = TraerPrecioProducto(dt.Rows(i).Item("codigo"), dt.Rows(i).Item("costo"), dt.Rows(i).Item("taller"), dt.Rows(i).Item("publico"), dt.Rows(i).Item("gdb"))

            Dim filaAux As DataRow = fila.Rows(0) ' Obtener la fila que deseas copiar
            Dim nuevaFila As DataRow = DtPrecios.NewRow() ' Crear una nueva fila en dt2
            nuevaFila.ItemArray = filaAux.ItemArray ' Copiar los datos de la fila original
            'dt2.Rows.Add(nuevaFila)
            DtPrecios.Rows.Add(nuevaFila)


        Next

        Return DtPrecios
    End Function
    Private Sub CargarDatosExcel(dt As DataTable)
        Dim dt2 As DataTable = ArmarDatos(dt)

        grprecio.DataSource = dt2
        grprecio.RetrieveStructure()
        grprecio.AlternatingColors = True
        '      a.cbnumi ,a.cbtv1numi ,a.cbty5prod ,b.yfcdprod1 as producto,a.cbest ,a.cbcmin 
        ',a.cbumin ,Umin .ycdes3 as unidad,a.cbpcost,a.cblote ,a.cbfechavenc ,a.cbptot 
        ',a.cbutven ,a.cbprven   ,a.cbobs ,
        'a.cbfact ,a.cbhact ,a.cbuact,1 as estado,Cast(null as Image) as img,a.cbpcost as costo,a.cbprven as venta


        With grprecio.RootTable.Columns("yfnumi")
            .Width = 100
            .Caption = "Item Nuevo"
            .Visible = False
            .TextAlignment = 2
        End With
        With grprecio.RootTable.Columns("yfCodAux1")
            .Width = 100
            .Caption = "Item Nuevo"
            .Visible = True
            .TextAlignment = 2
        End With
        With grprecio.RootTable.Columns("yfCodAux2")
            .Width = 100
            .Caption = "Item Antiguo"
            .Visible = True
            .TextAlignment = 2
        End With
        With grprecio.RootTable.Columns("yfdetprod")
            .Width = 250
            .Caption = "Producto"
            .Visible = True
        End With
        With grprecio.RootTable.Columns("costo")
            .Width = 100
            .Caption = "P. Costo"
            .Visible = True
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grprecio.RootTable.Columns("taller")
            .Width = 100
            .Caption = "P. Taller"
            .Visible = True
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grprecio.RootTable.Columns("publico")
            .Width = 100
            .Caption = "P. Publico"
            .Visible = True
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grprecio.RootTable.Columns("gdb")
            .Width = 100
            .Caption = "P. GDB"
            .Visible = True
            .FormatString = "0.00"
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

        End With
        'With grdetalle.RootTable.Columns("CodigoMarca")
        '    .Caption = "Cod.Marca"
        '    .Width = 120
        '    .WordWrap = True
        '    .MaxLines = 2
        '    .Visible = True
        '    .TextAlignment = 2
        'End With

        'With grdetalle.RootTable.Columns("Medida")
        '    .Caption = "Medida"
        '    .Width = 120
        '    .WordWrap = True
        '    .MaxLines = 2
        '    .Visible = False

        'End With

        'With grdetalle.RootTable.Columns("Marca")
        '    .Caption = "Marca"
        '    .Width = 120
        '    .WordWrap = True
        '    .MaxLines = 2
        '    .Visible = False
        'End With
        'With grdetalle.RootTable.Columns("Procedencia")
        '    .Caption = "Procedencia"
        '    .Width = 120
        '    .WordWrap = True
        '    .MaxLines = 2
        '    .Visible = False
        'End With

        'With grdetalle.RootTable.Columns("producto")
        '    .Caption = "Descripcion"
        '    .Width = 400
        '    .WordWrap = True
        '    .MaxLines = 3
        '    .Visible = True

        'End With
        'With grdetalle.RootTable.Columns("cbest")
        '    .Width = 50
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        '    .Visible = False
        'End With

        'With grdetalle.RootTable.Columns("cbcmin")
        '    .Width = 110
        '    .TextAlignment = 2
        '    .Visible = True
        '    .FormatString = "0"
        '    .Caption = "Cantidad"
        'End With
        'With grdetalle.RootTable.Columns("cbumin")
        '    .Width = 50
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        '    .Visible = False
        'End With
        'With grdetalle.RootTable.Columns("unidad")
        '    .Width = 80
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        '    .Visible = False
        '    .Caption = "Unidad".ToUpper
        'End With
        'With grdetalle.RootTable.Columns("gasto")
        '    .Width = 80
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        '    .Visible = True
        '    .Caption = "Costo".ToUpper
        '    .FormatString = "0.00"
        'End With
        'With grdetalle.RootTable.Columns("cbpcost")
        '    .Width = 140
        '    .TextAlignment = 2
        '    .Visible = True
        '    .FormatString = "0.000"
        '    .Caption = "P.CostoUn."
        'End With
        'If (_estadoPor = 1) Then
        '    With grdetalle.RootTable.Columns("cbutven")
        '        .Width = 110
        '        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        '        .Visible = False
        '        .FormatString = "0.00"
        '        .Caption = "Utilidad (%)".ToUpper
        '    End With
        '    With grdetalle.RootTable.Columns("cbprven")
        '        .Width = 120
        '        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        '        .Visible = False
        '        .FormatString = "0.00"
        '        .Caption = "Precio Venta".ToUpper
        '    End With
        'Else
        '    With grdetalle.RootTable.Columns("cbutven")
        '        .Width = 120
        '        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        '        .Visible = False
        '        .FormatString = "0.00"
        '        .Caption = "Utilidad.".ToUpper
        '    End With
        '    With grdetalle.RootTable.Columns("cbprven")
        '        .Width = 120
        '        .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        '        .Visible = False
        '        .FormatString = "0.00"
        '        .Caption = "Precio Venta.".ToUpper
        '    End With
        'End If
        'With grdetalle.RootTable.Columns("PP")
        '    .Width = 120
        '    .TextAlignment = 2
        '    .Visible = True
        '    .FormatString = "0.00"
        '    .Caption = "PP"

        'End With
        'With grdetalle.RootTable.Columns("PPA")
        '    .Width = 120
        '    .TextAlignment = 2
        '    .Visible = True
        '    .FormatString = "0.00"
        '    .Caption = "PPA"
        'End With
        'With grdetalle.RootTable.Columns("cbptot")
        '    .Width = 120
        '    .TextAlignment = 2
        '    .Visible = True
        '    .FormatString = "0.00"
        '    .Caption = "Sub Total"
        'End With
        'With grdetalle.RootTable.Columns("cbobs")
        '    .Width = 50
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '    .Visible = False
        'End With
        'With grdetalle.RootTable.Columns("cbfact")
        '    .Width = 50
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '    .FormatString = "yyyy/MM/dd"
        '    .Visible = False
        'End With
        'With grdetalle.RootTable.Columns("cbhact")
        '    .Width = 50
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '    .Visible = False
        'End With
        'With grdetalle.RootTable.Columns("cbuact")
        '    .Width = 50
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '    .Visible = False
        'End With
        'With grdetalle.RootTable.Columns("estado")
        '    .Width = 50
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '    .Visible = False
        'End With
        'With grdetalle.RootTable.Columns("costo")
        '    .Width = 50
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        '    .Visible = False
        'End With
        'With grdetalle.RootTable.Columns("yftcam")
        '    .Width = 50
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        '    .Visible = False
        'End With
        'With grdetalle.RootTable.Columns("img")
        '    .Width = 80
        '    .Caption = "Eliminar".ToUpper
        '    .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
        '    .Visible = True
        'End With
        'With grdetalle.RootTable.Columns("venta")
        '    .Width = 50
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        '    .Visible = False
        'End With
        'With grdetalle.RootTable.Columns("cbpFacturado")
        '    .Width = 140
        '    .TextAlignment = 2
        '    .Visible = False
        '    .FormatString = "0.00"
        '    .Caption = "P.Publico"
        'End With
        'With grdetalle.RootTable.Columns("cbpPublico")
        '    .Width = 140
        '    .TextAlignment = 2
        '    .Visible = False
        '    .FormatString = "0.00"
        '    .Caption = "P.Taller"
        'End With
        'With grdetalle.RootTable.Columns("cbpMecanico")
        '    .Width = 140
        '    .TextAlignment = 2
        '    .Visible = False
        '    .FormatString = "0.00"
        '    .Caption = "P.GDB"
        'End With
        With grprecio
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With

        'VerificarOrden(dt)
    End Sub

    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

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
End Class