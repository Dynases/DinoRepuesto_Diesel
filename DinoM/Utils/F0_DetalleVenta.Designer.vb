<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F0_DetalleVenta
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GPanelProductos = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.grProductos = New Janus.Windows.GridEX.GridEX()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnActualizar = New DevComponents.DotNetBar.ButtonX()
        Me.btnAgregar = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.tbProducto = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grProductoSeleccionado = New Janus.Windows.GridEX.GridEX()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupPanel3 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.grVentasCliente = New Janus.Windows.GridEX.GridEX()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.grVentasGral = New Janus.Windows.GridEX.GridEX()
        Me.GPanelProductos.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.grProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.grProductoSeleccionado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GroupPanel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.grVentasCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.grVentasGral, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GPanelProductos
        '
        Me.GPanelProductos.CanvasColor = System.Drawing.SystemColors.Control
        Me.GPanelProductos.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GPanelProductos.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Magenta
        Me.GPanelProductos.Controls.Add(Me.Panel7)
        Me.GPanelProductos.DisabledBackColor = System.Drawing.Color.Empty
        Me.GPanelProductos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GPanelProductos.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GPanelProductos.Location = New System.Drawing.Point(3, 3)
        Me.GPanelProductos.Name = "GPanelProductos"
        Me.GPanelProductos.Size = New System.Drawing.Size(856, 271)
        '
        '
        '
        Me.GPanelProductos.Style.BackColor = System.Drawing.Color.DodgerBlue
        Me.GPanelProductos.Style.BackColor2 = System.Drawing.Color.DodgerBlue
        Me.GPanelProductos.Style.BackColorGradientAngle = 90
        Me.GPanelProductos.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GPanelProductos.Style.BorderBottomWidth = 1
        Me.GPanelProductos.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GPanelProductos.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GPanelProductos.Style.BorderLeftWidth = 1
        Me.GPanelProductos.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GPanelProductos.Style.BorderRightWidth = 1
        Me.GPanelProductos.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GPanelProductos.Style.BorderTopWidth = 1
        Me.GPanelProductos.Style.CornerDiameter = 4
        Me.GPanelProductos.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GPanelProductos.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GPanelProductos.Style.TextColor = System.Drawing.Color.White
        Me.GPanelProductos.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GPanelProductos.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GPanelProductos.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GPanelProductos.TabIndex = 4
        Me.GPanelProductos.Text = "LISTADO PRODUCTOS"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.Controls.Add(Me.grProductos)
        Me.Panel7.Controls.Add(Me.Panel3)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(850, 248)
        Me.Panel7.TabIndex = 0
        '
        'grProductos
        '
        Me.grProductos.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grProductos.BackColor = System.Drawing.Color.GhostWhite
        Me.grProductos.ColumnAutoResize = True
        Me.grProductos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grProductos.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid
        Me.grProductos.Font = New System.Drawing.Font("Calibri", 9.2!)
        Me.grProductos.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grProductos.HeaderFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.grProductos.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grProductos.Location = New System.Drawing.Point(0, 41)
        Me.grProductos.Name = "grProductos"
        Me.grProductos.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grProductos.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grProductos.RecordNavigator = True
        Me.grProductos.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.grProductos.SelectedFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grProductos.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.grProductos.Size = New System.Drawing.Size(850, 207)
        Me.grProductos.TabIndex = 0
        Me.grProductos.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnActualizar)
        Me.Panel3.Controls.Add(Me.btnAgregar)
        Me.Panel3.Controls.Add(Me.LabelX9)
        Me.Panel3.Controls.Add(Me.tbProducto)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(850, 41)
        Me.Panel3.TabIndex = 1
        '
        'btnActualizar
        '
        Me.btnActualizar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnActualizar.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.btnActualizar.Font = New System.Drawing.Font("Calibri", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnActualizar.Image = Global.DinoM.My.Resources.Resources.refresh_1
        Me.btnActualizar.ImageFixedSize = New System.Drawing.Size(30, 30)
        Me.btnActualizar.Location = New System.Drawing.Point(544, 2)
        Me.btnActualizar.Margin = New System.Windows.Forms.Padding(2)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(150, 32)
        Me.btnActualizar.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014
        Me.btnActualizar.TabIndex = 374
        Me.btnActualizar.Text = "Actualizar"
        '
        'btnAgregar
        '
        Me.btnAgregar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAgregar.BackColor = System.Drawing.Color.White
        Me.btnAgregar.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.btnAgregar.Font = New System.Drawing.Font("Calibri", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregar.Image = Global.DinoM.My.Resources.Resources.atras1
        Me.btnAgregar.ImageFixedSize = New System.Drawing.Size(30, 30)
        Me.btnAgregar.Location = New System.Drawing.Point(388, 2)
        Me.btnAgregar.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(152, 32)
        Me.btnAgregar.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014
        Me.btnAgregar.TabIndex = 372
        Me.btnAgregar.Text = "Confirmar / Salir"
        '
        'LabelX9
        '
        Me.LabelX9.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX9.Location = New System.Drawing.Point(9, 11)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX9.Size = New System.Drawing.Size(113, 23)
        Me.LabelX9.TabIndex = 226
        Me.LabelX9.Text = "Buscar Producto:"
        '
        'tbProducto
        '
        Me.tbProducto.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbProducto.Border.Class = "TextBoxBorder"
        Me.tbProducto.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbProducto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbProducto.Location = New System.Drawing.Point(134, 13)
        Me.tbProducto.Name = "tbProducto"
        Me.tbProducto.PreventEnterBeep = True
        Me.tbProducto.Size = New System.Drawing.Size(249, 22)
        Me.tbProducto.TabIndex = 3
        '
        'GroupPanel1
        '
        Me.GroupPanel1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Magenta
        Me.GroupPanel1.Controls.Add(Me.Panel1)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel1.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel1.Location = New System.Drawing.Point(3, 280)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(856, 120)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor = System.Drawing.Color.DodgerBlue
        Me.GroupPanel1.Style.BackColor2 = System.Drawing.Color.DodgerBlue
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderBottomWidth = 1
        Me.GroupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderLeftWidth = 1
        Me.GroupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderRightWidth = 1
        Me.GroupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderTopWidth = 1
        Me.GroupPanel1.Style.CornerDiameter = 4
        Me.GroupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColor = System.Drawing.Color.White
        Me.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel1.StyleMouseOver.BackColor = System.Drawing.Color.MediumVioletRed
        Me.GroupPanel1.StyleMouseOver.BackColor2 = System.Drawing.SystemColors.Control
        Me.GroupPanel1.StyleMouseOver.BackColorGradientAngle = 30
        Me.GroupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel1.TabIndex = 5
        Me.GroupPanel1.Text = "PRODUCTOS SELECCIONADOS"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.grProductoSeleccionado)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(850, 97)
        Me.Panel1.TabIndex = 0
        '
        'grProductoSeleccionado
        '
        Me.grProductoSeleccionado.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grProductoSeleccionado.BackColor = System.Drawing.Color.GhostWhite
        Me.grProductoSeleccionado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grProductoSeleccionado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grProductoSeleccionado.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grProductoSeleccionado.HeaderFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.grProductoSeleccionado.Location = New System.Drawing.Point(0, 0)
        Me.grProductoSeleccionado.Name = "grProductoSeleccionado"
        Me.grProductoSeleccionado.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grProductoSeleccionado.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grProductoSeleccionado.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.grProductoSeleccionado.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.grProductoSeleccionado.Size = New System.Drawing.Size(850, 97)
        Me.grProductoSeleccionado.TabIndex = 0
        Me.grProductoSeleccionado.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GPanelProductos, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupPanel1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(862, 505)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.GroupPanel3, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupPanel2, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 406)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(856, 96)
        Me.TableLayoutPanel2.TabIndex = 6
        '
        'GroupPanel3
        '
        Me.GroupPanel3.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel3.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Magenta
        Me.GroupPanel3.Controls.Add(Me.Panel4)
        Me.GroupPanel3.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel3.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel3.Location = New System.Drawing.Point(431, 3)
        Me.GroupPanel3.Name = "GroupPanel3"
        Me.GroupPanel3.Size = New System.Drawing.Size(422, 90)
        '
        '
        '
        Me.GroupPanel3.Style.BackColor = System.Drawing.Color.DodgerBlue
        Me.GroupPanel3.Style.BackColor2 = System.Drawing.Color.DodgerBlue
        Me.GroupPanel3.Style.BackColorGradientAngle = 90
        Me.GroupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderBottomWidth = 1
        Me.GroupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderLeftWidth = 1
        Me.GroupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderRightWidth = 1
        Me.GroupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderTopWidth = 1
        Me.GroupPanel3.Style.CornerDiameter = 4
        Me.GroupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel3.Style.TextColor = System.Drawing.Color.White
        Me.GroupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel3.TabIndex = 7
        Me.GroupPanel3.Text = "Últimas 10 Ventas (Cliente)"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.Controls.Add(Me.grVentasCliente)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(416, 67)
        Me.Panel4.TabIndex = 0
        '
        'grVentasCliente
        '
        Me.grVentasCliente.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grVentasCliente.BackColor = System.Drawing.Color.GhostWhite
        Me.grVentasCliente.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grVentasCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grVentasCliente.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grVentasCliente.HeaderFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.grVentasCliente.Location = New System.Drawing.Point(0, 0)
        Me.grVentasCliente.Name = "grVentasCliente"
        Me.grVentasCliente.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grVentasCliente.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grVentasCliente.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.grVentasCliente.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.grVentasCliente.Size = New System.Drawing.Size(416, 67)
        Me.grVentasCliente.TabIndex = 0
        Me.grVentasCliente.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupPanel2
        '
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Magenta
        Me.GroupPanel2.Controls.Add(Me.Panel2)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel2.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel2.Location = New System.Drawing.Point(3, 3)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(422, 90)
        '
        '
        '
        Me.GroupPanel2.Style.BackColor = System.Drawing.Color.DodgerBlue
        Me.GroupPanel2.Style.BackColor2 = System.Drawing.Color.DodgerBlue
        Me.GroupPanel2.Style.BackColorGradientAngle = 90
        Me.GroupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderBottomWidth = 1
        Me.GroupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderLeftWidth = 1
        Me.GroupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderRightWidth = 1
        Me.GroupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderTopWidth = 1
        Me.GroupPanel2.Style.CornerDiameter = 4
        Me.GroupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel2.Style.TextColor = System.Drawing.Color.White
        Me.GroupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel2.TabIndex = 6
        Me.GroupPanel2.Text = "Últimas 10 Ventas (General)"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.grVentasGral)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(416, 67)
        Me.Panel2.TabIndex = 0
        '
        'grVentasGral
        '
        Me.grVentasGral.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grVentasGral.BackColor = System.Drawing.Color.GhostWhite
        Me.grVentasGral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grVentasGral.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grVentasGral.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grVentasGral.HeaderFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.grVentasGral.Location = New System.Drawing.Point(0, 0)
        Me.grVentasGral.Name = "grVentasGral"
        Me.grVentasGral.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grVentasGral.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grVentasGral.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.grVentasGral.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.grVentasGral.Size = New System.Drawing.Size(416, 67)
        Me.grVentasGral.TabIndex = 0
        Me.grVentasGral.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'F0_DetalleVenta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 505)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "F0_DetalleVenta"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Top"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GPanelProductos.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        CType(Me.grProductos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.GroupPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.grProductoSeleccionado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.GroupPanel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.grVentasCliente, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.grVentasGral, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GPanelProductos As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents grProductos As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel3 As Panel
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbProducto As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents grProductoSeleccionado As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnAgregar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnActualizar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents GroupPanel3 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents grVentasCliente As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents grVentasGral As Janus.Windows.GridEX.GridEX
End Class
