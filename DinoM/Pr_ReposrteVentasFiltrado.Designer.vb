<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Pr_ReposrteVentasFiltrado
    Inherits Modelo.ModeloR0
    'Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pr_ReposrteVentasFiltrado))
        Dim cbPrograma_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbProv_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.tbFechaI = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.tbFechaF = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.cbPrograma = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.cbAlmacenTodos = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.cbAlmacenUno = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.cbProvUno = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.cbProvTodos = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.cbProv = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.tbMarca = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.cbMarcaUno = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.cbMarcaTodos = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.CheckUnoProducto = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.CheckTodosProducto = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.tbProducto = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabPrincipal.SuspendLayout()
        Me.SuperTabControlPanelRegistro.SuspendLayout()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelToolBar1.SuspendLayout()
        Me.PanelPrincipal.SuspendLayout()
        Me.PanelUsuario.SuspendLayout()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MGPFiltros.SuspendLayout()
        Me.PanelIzq.SuspendLayout()
        CType(Me.MPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFechaI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFechaF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbPrograma, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbProv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SuperTabPrincipal
        '
        '
        '
        '
        '
        '
        '
        Me.SuperTabPrincipal.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.SuperTabPrincipal.ControlBox.MenuBox.Name = ""
        Me.SuperTabPrincipal.ControlBox.Name = ""
        Me.SuperTabPrincipal.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SuperTabPrincipal.ControlBox.MenuBox, Me.SuperTabPrincipal.ControlBox.CloseBox})
        Me.SuperTabPrincipal.Size = New System.Drawing.Size(800, 450)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelBuscador, 0)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelRegistro, 0)
        '
        'SuperTabControlPanelBuscador
        '
        Me.SuperTabControlPanelBuscador.Size = New System.Drawing.Size(768, 450)
        '
        'SuperTabControlPanelRegistro
        '
        Me.SuperTabControlPanelRegistro.Size = New System.Drawing.Size(768, 450)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelIzq, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelPrincipal, 0)
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.Yellow
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.Khaki
        Me.PanelSuperior.Style.BackgroundImage = CType(resources.GetObject("PanelSuperior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        '
        'PanelInferior
        '
        Me.PanelInferior.Location = New System.Drawing.Point(0, 414)
        Me.PanelInferior.Size = New System.Drawing.Size(768, 36)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.Gold
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.Gold
        Me.PanelInferior.Style.BackgroundImage = CType(resources.GetObject("PanelInferior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelInferior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelInferior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelInferior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelInferior.Style.GradientAngle = 90
        '
        'BubbleBarUsuario
        '
        '
        '
        '
        Me.BubbleBarUsuario.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BackColor = System.Drawing.Color.Transparent
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderBottomWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderLeftWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderRightWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderTopWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingBottom = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingLeft = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingRight = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingTop = 3
        Me.BubbleBarUsuario.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight
        Me.BubbleBarUsuario.SelectedTabColors.BorderColor = System.Drawing.Color.Black
        '
        'btnSalir
        '
        '
        'btnGenerar
        '
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Size = New System.Drawing.Size(405, 414)
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Location = New System.Drawing.Point(568, 0)
        '
        'MReportViewer
        '
        Me.MReportViewer.Size = New System.Drawing.Size(405, 414)
        Me.MReportViewer.ToolPanelWidth = 200
        '
        'MGPFiltros
        '
        Me.MGPFiltros.Controls.Add(Me.LabelX6)
        Me.MGPFiltros.Controls.Add(Me.CheckUnoProducto)
        Me.MGPFiltros.Controls.Add(Me.CheckTodosProducto)
        Me.MGPFiltros.Controls.Add(Me.tbProducto)
        Me.MGPFiltros.Controls.Add(Me.LabelX5)
        Me.MGPFiltros.Controls.Add(Me.LabelX4)
        Me.MGPFiltros.Controls.Add(Me.LabelX3)
        Me.MGPFiltros.Controls.Add(Me.LabelX2)
        Me.MGPFiltros.Controls.Add(Me.LabelX1)
        Me.MGPFiltros.Controls.Add(Me.cbMarcaUno)
        Me.MGPFiltros.Controls.Add(Me.cbMarcaTodos)
        Me.MGPFiltros.Controls.Add(Me.tbMarca)
        Me.MGPFiltros.Controls.Add(Me.cbProvUno)
        Me.MGPFiltros.Controls.Add(Me.cbProvTodos)
        Me.MGPFiltros.Controls.Add(Me.cbProv)
        Me.MGPFiltros.Controls.Add(Me.cbAlmacenUno)
        Me.MGPFiltros.Controls.Add(Me.cbAlmacenTodos)
        Me.MGPFiltros.Controls.Add(Me.cbPrograma)
        Me.MGPFiltros.Controls.Add(Me.tbFechaF)
        Me.MGPFiltros.Controls.Add(Me.tbFechaI)
        Me.MGPFiltros.Size = New System.Drawing.Size(363, 342)
        '
        '
        '
        Me.MGPFiltros.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.MGPFiltros.Style.BackColorGradientAngle = 90
        Me.MGPFiltros.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.MGPFiltros.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderBottomWidth = 1
        Me.MGPFiltros.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.MGPFiltros.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderLeftWidth = 1
        Me.MGPFiltros.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderRightWidth = 1
        Me.MGPFiltros.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderTopWidth = 1
        Me.MGPFiltros.Style.CornerDiameter = 4
        Me.MGPFiltros.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.MGPFiltros.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.MGPFiltros.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.MGPFiltros.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.MGPFiltros.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.MGPFiltros.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        'PanelIzq
        '
        Me.PanelIzq.Size = New System.Drawing.Size(363, 414)
        Me.PanelIzq.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.PanelIzq.Controls.SetChildIndex(Me.MGPFiltros, 0)
        '
        'tbFechaI
        '
        '
        '
        '
        Me.tbFechaI.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbFechaI.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbFechaI.ButtonDropDown.Visible = True
        Me.tbFechaI.IsPopupCalendarOpen = False
        Me.tbFechaI.Location = New System.Drawing.Point(150, 250)
        '
        '
        '
        '
        '
        '
        Me.tbFechaI.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbFechaI.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.MonthCalendar.DisplayMonth = New Date(2023, 1, 1, 0, 0, 0, 0)
        Me.tbFechaI.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.MonthCalendar.TodayButtonVisible = True
        Me.tbFechaI.Name = "tbFechaI"
        Me.tbFechaI.Size = New System.Drawing.Size(150, 22)
        Me.tbFechaI.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaI.TabIndex = 0
        '
        'tbFechaF
        '
        '
        '
        '
        Me.tbFechaF.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbFechaF.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbFechaF.ButtonDropDown.Visible = True
        Me.tbFechaF.IsPopupCalendarOpen = False
        Me.tbFechaF.Location = New System.Drawing.Point(150, 290)
        '
        '
        '
        '
        '
        '
        Me.tbFechaF.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbFechaF.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.MonthCalendar.DisplayMonth = New Date(2023, 1, 1, 0, 0, 0, 0)
        Me.tbFechaF.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.MonthCalendar.TodayButtonVisible = True
        Me.tbFechaF.Name = "tbFechaF"
        Me.tbFechaF.Size = New System.Drawing.Size(150, 22)
        Me.tbFechaF.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaF.TabIndex = 1
        '
        'cbPrograma
        '
        Me.cbPrograma.AutoComplete = False
        Me.cbPrograma.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        Me.cbPrograma.ControlThemedAreas = Janus.Windows.GridEX.ControlThemedAreas.Button
        cbPrograma_DesignTimeLayout.LayoutString = resources.GetString("cbPrograma_DesignTimeLayout.LayoutString")
        Me.cbPrograma.DesignTimeLayout = cbPrograma_DesignTimeLayout
        Me.cbPrograma.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPrograma.Location = New System.Drawing.Point(9, 151)
        Me.cbPrograma.Name = "cbPrograma"
        Me.cbPrograma.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbPrograma.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbPrograma.SelectedIndex = -1
        Me.cbPrograma.SelectedItem = Nothing
        Me.cbPrograma.Size = New System.Drawing.Size(220, 21)
        Me.cbPrograma.TabIndex = 32
        Me.cbPrograma.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cbAlmacenTodos
        '
        Me.cbAlmacenTodos.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.cbAlmacenTodos.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.cbAlmacenTodos.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cbAlmacenTodos.Location = New System.Drawing.Point(292, 151)
        Me.cbAlmacenTodos.Name = "cbAlmacenTodos"
        Me.cbAlmacenTodos.Size = New System.Drawing.Size(100, 23)
        Me.cbAlmacenTodos.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cbAlmacenTodos.TabIndex = 33
        Me.cbAlmacenTodos.Text = "Todos"
        '
        'cbAlmacenUno
        '
        Me.cbAlmacenUno.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.cbAlmacenUno.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.cbAlmacenUno.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cbAlmacenUno.Location = New System.Drawing.Point(235, 151)
        Me.cbAlmacenUno.Name = "cbAlmacenUno"
        Me.cbAlmacenUno.Size = New System.Drawing.Size(55, 23)
        Me.cbAlmacenUno.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cbAlmacenUno.TabIndex = 34
        Me.cbAlmacenUno.Text = "Uno"
        '
        'cbProvUno
        '
        Me.cbProvUno.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.cbProvUno.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.cbProvUno.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cbProvUno.Location = New System.Drawing.Point(235, 39)
        Me.cbProvUno.Name = "cbProvUno"
        Me.cbProvUno.Size = New System.Drawing.Size(55, 23)
        Me.cbProvUno.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cbProvUno.TabIndex = 37
        Me.cbProvUno.Text = "Uno"
        '
        'cbProvTodos
        '
        Me.cbProvTodos.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.cbProvTodos.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.cbProvTodos.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cbProvTodos.Location = New System.Drawing.Point(292, 39)
        Me.cbProvTodos.Name = "cbProvTodos"
        Me.cbProvTodos.Size = New System.Drawing.Size(100, 23)
        Me.cbProvTodos.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cbProvTodos.TabIndex = 36
        Me.cbProvTodos.Text = "Todos"
        '
        'cbProv
        '
        Me.cbProv.AutoComplete = False
        Me.cbProv.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        Me.cbProv.ControlThemedAreas = Janus.Windows.GridEX.ControlThemedAreas.Button
        cbProv_DesignTimeLayout.LayoutString = resources.GetString("cbProv_DesignTimeLayout.LayoutString")
        Me.cbProv.DesignTimeLayout = cbProv_DesignTimeLayout
        Me.cbProv.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProv.Location = New System.Drawing.Point(9, 39)
        Me.cbProv.Name = "cbProv"
        Me.cbProv.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbProv.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbProv.SelectedIndex = -1
        Me.cbProv.SelectedItem = Nothing
        Me.cbProv.Size = New System.Drawing.Size(220, 21)
        Me.cbProv.TabIndex = 35
        Me.cbProv.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'tbMarca
        '
        '
        '
        '
        Me.tbMarca.Border.Class = "TextBoxBorder"
        Me.tbMarca.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMarca.Location = New System.Drawing.Point(9, 95)
        Me.tbMarca.Name = "tbMarca"
        Me.tbMarca.PreventEnterBeep = True
        Me.tbMarca.Size = New System.Drawing.Size(220, 22)
        Me.tbMarca.TabIndex = 38
        '
        'cbMarcaUno
        '
        Me.cbMarcaUno.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.cbMarcaUno.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.cbMarcaUno.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cbMarcaUno.Location = New System.Drawing.Point(235, 94)
        Me.cbMarcaUno.Name = "cbMarcaUno"
        Me.cbMarcaUno.Size = New System.Drawing.Size(55, 23)
        Me.cbMarcaUno.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cbMarcaUno.TabIndex = 40
        Me.cbMarcaUno.Text = "Uno"
        '
        'cbMarcaTodos
        '
        Me.cbMarcaTodos.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.cbMarcaTodos.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.cbMarcaTodos.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cbMarcaTodos.Location = New System.Drawing.Point(292, 94)
        Me.cbMarcaTodos.Name = "cbMarcaTodos"
        Me.cbMarcaTodos.Size = New System.Drawing.Size(100, 23)
        Me.cbMarcaTodos.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cbMarcaTodos.TabIndex = 39
        Me.cbMarcaTodos.Text = "Todos"
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX1.Location = New System.Drawing.Point(38, 250)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(100, 23)
        Me.LabelX1.TabIndex = 41
        Me.LabelX1.Text = "Fecha Inicial:"
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX2.Location = New System.Drawing.Point(38, 290)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(100, 23)
        Me.LabelX2.TabIndex = 42
        Me.LabelX2.Text = "Fecha Final:"
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX3.Location = New System.Drawing.Point(9, 13)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(100, 23)
        Me.LabelX3.TabIndex = 43
        Me.LabelX3.Text = "Categoria:"
        '
        'LabelX4
        '
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX4.Location = New System.Drawing.Point(9, 66)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(100, 23)
        Me.LabelX4.TabIndex = 44
        Me.LabelX4.Text = "Marca:"
        '
        'LabelX5
        '
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX5.Location = New System.Drawing.Point(9, 123)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(100, 23)
        Me.LabelX5.TabIndex = 45
        Me.LabelX5.Text = "Almacen:"
        '
        'CheckUnoProducto
        '
        Me.CheckUnoProducto.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.CheckUnoProducto.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckUnoProducto.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.CheckUnoProducto.Location = New System.Drawing.Point(235, 203)
        Me.CheckUnoProducto.Name = "CheckUnoProducto"
        Me.CheckUnoProducto.Size = New System.Drawing.Size(55, 23)
        Me.CheckUnoProducto.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckUnoProducto.TabIndex = 48
        Me.CheckUnoProducto.Text = "Uno"
        '
        'CheckTodosProducto
        '
        Me.CheckTodosProducto.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.CheckTodosProducto.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckTodosProducto.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.CheckTodosProducto.Location = New System.Drawing.Point(292, 203)
        Me.CheckTodosProducto.Name = "CheckTodosProducto"
        Me.CheckTodosProducto.Size = New System.Drawing.Size(100, 23)
        Me.CheckTodosProducto.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckTodosProducto.TabIndex = 47
        Me.CheckTodosProducto.Text = "Todos"
        '
        'tbProducto
        '
        '
        '
        '
        Me.tbProducto.Border.Class = "TextBoxBorder"
        Me.tbProducto.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbProducto.Location = New System.Drawing.Point(9, 204)
        Me.tbProducto.Name = "tbProducto"
        Me.tbProducto.PreventEnterBeep = True
        Me.tbProducto.Size = New System.Drawing.Size(220, 22)
        Me.tbProducto.TabIndex = 46
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX6.Location = New System.Drawing.Point(9, 178)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(100, 23)
        Me.LabelX6.TabIndex = 49
        Me.LabelX6.Text = "Producto:"
        '
        'Pr_ReposrteVentasFiltrado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "Pr_ReposrteVentasFiltrado"
        Me.Opacity = 0.99R
        Me.Text = "Pr_ReposrteVentasFiltrado"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Controls.SetChildIndex(Me.SuperTabPrincipal, 0)
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabPrincipal.ResumeLayout(False)
        Me.SuperTabControlPanelRegistro.ResumeLayout(False)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelToolBar1.ResumeLayout(False)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        Me.MPanelUserAct.ResumeLayout(False)
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MGPFiltros.ResumeLayout(False)
        Me.MGPFiltros.PerformLayout()
        Me.PanelIzq.ResumeLayout(False)
        CType(Me.MPicture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFechaI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFechaF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbPrograma, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbProv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tbFechaI As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents tbFechaF As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cbMarcaUno As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents cbMarcaTodos As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents tbMarca As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents cbProvUno As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents cbProvTodos As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents cbProv As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents cbAlmacenUno As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents cbAlmacenTodos As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents cbPrograma As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents CheckUnoProducto As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents CheckTodosProducto As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents tbProducto As DevComponents.DotNetBar.Controls.TextBoxX
End Class
