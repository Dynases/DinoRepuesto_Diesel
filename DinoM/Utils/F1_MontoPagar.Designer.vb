<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F1_MontoPagar
    Inherits System.Windows.Forms.Form

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
        Dim cbCambioDolar_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F1_MontoPagar))
        Dim cbBanco_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnSalir = New DevComponents.DotNetBar.ButtonX()
        Me.btnContinuar = New DevComponents.DotNetBar.ButtonX()
        Me.txtMontoPagado1 = New DevComponents.DotNetBar.LabelX()
        Me.txtCambio1 = New DevComponents.DotNetBar.LabelX()
        Me.lbCambio = New DevComponents.DotNetBar.LabelX()
        Me.lbMontoPagado = New DevComponents.DotNetBar.LabelX()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btAddTrans = New DevComponents.DotNetBar.ButtonX()
        Me.grTransferencia = New Janus.Windows.GridEX.GridEX()
        Me.saldoT = New DevComponents.DotNetBar.LabelX()
        Me.SFavor = New DevComponents.Editors.DoubleInput()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.cbCambioDolar = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.cbBanco = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.lbGlosa = New DevComponents.DotNetBar.LabelX()
        Me.lbBanco = New DevComponents.DotNetBar.LabelX()
        Me.tbGlosa = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.btgrupo1 = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX20 = New DevComponents.DotNetBar.LabelX()
        Me.tbMontoTarej = New DevComponents.Editors.DoubleInput()
        Me.chbTarjeta = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.tbMontoBs = New DevComponents.Editors.DoubleInput()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.tbMontoDolar = New DevComponents.Editors.DoubleInput()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.tbCostoEnvio = New DevComponents.Editors.DoubleInput()
        Me.lbCostoEnvio = New DevComponents.DotNetBar.LabelX()
        Me.lbCredito = New DevComponents.DotNetBar.LabelX()
        Me.tbFechaVenc = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.swTipoVenta = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grTransferencia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFavor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbCambioDolar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbBanco, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMontoTarej, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMontoBs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMontoDolar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.tbCostoEnvio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFechaVenc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.BackgroundImage = Global.DinoM.My.Resources.Resources.fondo1
        Me.GroupBox2.Controls.Add(Me.BtnSalir)
        Me.GroupBox2.Controls.Add(Me.btnContinuar)
        Me.GroupBox2.Controls.Add(Me.txtMontoPagado1)
        Me.GroupBox2.Controls.Add(Me.txtCambio1)
        Me.GroupBox2.Controls.Add(Me.lbCambio)
        Me.GroupBox2.Controls.Add(Me.lbMontoPagado)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Gold
        Me.GroupBox2.Location = New System.Drawing.Point(0, 381)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(526, 137)
        Me.GroupBox2.TabIndex = 364
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Monto Venta/Cambio:"
        '
        'BtnSalir
        '
        Me.BtnSalir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.BtnSalir.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.BtnSalir.Font = New System.Drawing.Font("Calibri", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSalir.Image = Global.DinoM.My.Resources.Resources.cancel
        Me.BtnSalir.ImageFixedSize = New System.Drawing.Size(30, 30)
        Me.BtnSalir.Location = New System.Drawing.Point(262, 89)
        Me.BtnSalir.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnSalir.Name = "BtnSalir"
        Me.BtnSalir.Size = New System.Drawing.Size(110, 39)
        Me.BtnSalir.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014
        Me.BtnSalir.TabIndex = 371
        Me.BtnSalir.Text = "Salir"
        '
        'btnContinuar
        '
        Me.btnContinuar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnContinuar.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground
        Me.btnContinuar.Font = New System.Drawing.Font("Calibri", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinuar.Image = Global.DinoM.My.Resources.Resources.checked
        Me.btnContinuar.ImageFixedSize = New System.Drawing.Size(30, 30)
        Me.btnContinuar.Location = New System.Drawing.Point(383, 89)
        Me.btnContinuar.Margin = New System.Windows.Forms.Padding(2)
        Me.btnContinuar.Name = "btnContinuar"
        Me.btnContinuar.Size = New System.Drawing.Size(110, 39)
        Me.btnContinuar.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeMobile2014
        Me.btnContinuar.TabIndex = 3
        Me.btnContinuar.Text = "Continuar"
        '
        'txtMontoPagado1
        '
        Me.txtMontoPagado1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtMontoPagado1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtMontoPagado1.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoPagado1.ForeColor = System.Drawing.Color.YellowGreen
        Me.txtMontoPagado1.Location = New System.Drawing.Point(148, 21)
        Me.txtMontoPagado1.Name = "txtMontoPagado1"
        Me.txtMontoPagado1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.txtMontoPagado1.Size = New System.Drawing.Size(120, 40)
        Me.txtMontoPagado1.TabIndex = 369
        Me.txtMontoPagado1.Text = "0.00"
        Me.txtMontoPagado1.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'txtCambio1
        '
        Me.txtCambio1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtCambio1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtCambio1.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCambio1.ForeColor = System.Drawing.Color.YellowGreen
        Me.txtCambio1.Location = New System.Drawing.Point(383, 21)
        Me.txtCambio1.Name = "txtCambio1"
        Me.txtCambio1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.txtCambio1.Size = New System.Drawing.Size(110, 40)
        Me.txtCambio1.TabIndex = 368
        Me.txtCambio1.Text = "0.00"
        Me.txtCambio1.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'lbCambio
        '
        Me.lbCambio.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbCambio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbCambio.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCambio.ForeColor = System.Drawing.Color.White
        Me.lbCambio.Location = New System.Drawing.Point(296, 21)
        Me.lbCambio.Name = "lbCambio"
        Me.lbCambio.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbCambio.Size = New System.Drawing.Size(80, 40)
        Me.lbCambio.TabIndex = 367
        Me.lbCambio.Text = "Cambio Bs.:"
        '
        'lbMontoPagado
        '
        Me.lbMontoPagado.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbMontoPagado.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbMontoPagado.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbMontoPagado.ForeColor = System.Drawing.Color.White
        Me.lbMontoPagado.Location = New System.Drawing.Point(8, 24)
        Me.lbMontoPagado.Name = "lbMontoPagado"
        Me.lbMontoPagado.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbMontoPagado.Size = New System.Drawing.Size(130, 33)
        Me.lbMontoPagado.TabIndex = 366
        Me.lbMontoPagado.Text = "Total a Pagar Bs. :"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.BackgroundImage = Global.DinoM.My.Resources.Resources.fondo1
        Me.GroupBox1.Controls.Add(Me.btAddTrans)
        Me.GroupBox1.Controls.Add(Me.grTransferencia)
        Me.GroupBox1.Controls.Add(Me.saldoT)
        Me.GroupBox1.Controls.Add(Me.SFavor)
        Me.GroupBox1.Controls.Add(Me.LabelX2)
        Me.GroupBox1.Controls.Add(Me.cbCambioDolar)
        Me.GroupBox1.Controls.Add(Me.cbBanco)
        Me.GroupBox1.Controls.Add(Me.lbGlosa)
        Me.GroupBox1.Controls.Add(Me.lbBanco)
        Me.GroupBox1.Controls.Add(Me.tbGlosa)
        Me.GroupBox1.Controls.Add(Me.btgrupo1)
        Me.GroupBox1.Controls.Add(Me.LabelX20)
        Me.GroupBox1.Controls.Add(Me.tbMontoTarej)
        Me.GroupBox1.Controls.Add(Me.chbTarjeta)
        Me.GroupBox1.Controls.Add(Me.tbMontoBs)
        Me.GroupBox1.Controls.Add(Me.LabelX5)
        Me.GroupBox1.Controls.Add(Me.tbMontoDolar)
        Me.GroupBox1.Controls.Add(Me.LabelX6)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Gold
        Me.GroupBox1.Location = New System.Drawing.Point(0, 91)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(526, 290)
        Me.GroupBox1.TabIndex = 362
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Formas de Pago:"
        '
        'btAddTrans
        '
        Me.btAddTrans.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btAddTrans.BackColor = System.Drawing.Color.Transparent
        Me.btAddTrans.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.btAddTrans.Image = Global.DinoM.My.Resources.Resources.add
        Me.btAddTrans.ImageFixedSize = New System.Drawing.Size(25, 23)
        Me.btAddTrans.Location = New System.Drawing.Point(486, 109)
        Me.btAddTrans.Name = "btAddTrans"
        Me.btAddTrans.Size = New System.Drawing.Size(28, 23)
        Me.btAddTrans.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btAddTrans.TabIndex = 381
        Me.btAddTrans.Visible = False
        '
        'grTransferencia
        '
        Me.grTransferencia.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grTransferencia.Location = New System.Drawing.Point(149, 143)
        Me.grTransferencia.Name = "grTransferencia"
        Me.grTransferencia.Size = New System.Drawing.Size(348, 104)
        Me.grTransferencia.TabIndex = 380
        '
        'saldoT
        '
        Me.saldoT.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.saldoT.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.saldoT.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saldoT.ForeColor = System.Drawing.Color.White
        Me.saldoT.Location = New System.Drawing.Point(413, 23)
        Me.saldoT.Name = "saldoT"
        Me.saldoT.SingleLineColor = System.Drawing.SystemColors.Control
        Me.saldoT.Size = New System.Drawing.Size(87, 18)
        Me.saldoT.TabIndex = 379
        Me.saldoT.Text = "0.00"
        Me.saldoT.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'SFavor
        '
        '
        '
        '
        Me.SFavor.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.SFavor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.SFavor.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.SFavor.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SFavor.ForeColor = System.Drawing.Color.Black
        Me.SFavor.Increment = 1.0R
        Me.SFavor.Location = New System.Drawing.Point(296, 47)
        Me.SFavor.MinValue = 0R
        Me.SFavor.Name = "SFavor"
        Me.SFavor.Size = New System.Drawing.Size(187, 26)
        Me.SFavor.TabIndex = 378
        Me.SFavor.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.ForeColor = System.Drawing.Color.White
        Me.LabelX2.Location = New System.Drawing.Point(296, 21)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX2.Size = New System.Drawing.Size(111, 18)
        Me.LabelX2.TabIndex = 377
        Me.LabelX2.Text = "Saldo A Favor:"
        '
        'cbCambioDolar
        '
        Me.cbCambioDolar.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.cbCambioDolar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        cbCambioDolar_DesignTimeLayout.LayoutString = resources.GetString("cbCambioDolar_DesignTimeLayout.LayoutString")
        Me.cbCambioDolar.DesignTimeLayout = cbCambioDolar_DesignTimeLayout
        Me.cbCambioDolar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCambioDolar.Location = New System.Drawing.Point(149, 16)
        Me.cbCambioDolar.Name = "cbCambioDolar"
        Me.cbCambioDolar.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbCambioDolar.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbCambioDolar.SelectedIndex = -1
        Me.cbCambioDolar.SelectedItem = Nothing
        Me.cbCambioDolar.Size = New System.Drawing.Size(102, 22)
        Me.cbCambioDolar.TabIndex = 376
        Me.cbCambioDolar.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cbBanco
        '
        Me.cbBanco.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        cbBanco_DesignTimeLayout.LayoutString = resources.GetString("cbBanco_DesignTimeLayout.LayoutString")
        Me.cbBanco.DesignTimeLayout = cbBanco_DesignTimeLayout
        Me.cbBanco.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBanco.Location = New System.Drawing.Point(297, 110)
        Me.cbBanco.Name = "cbBanco"
        Me.cbBanco.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbBanco.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbBanco.SelectedIndex = -1
        Me.cbBanco.SelectedItem = Nothing
        Me.cbBanco.Size = New System.Drawing.Size(186, 22)
        Me.cbBanco.TabIndex = 375
        Me.cbBanco.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lbGlosa
        '
        Me.lbGlosa.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbGlosa.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbGlosa.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbGlosa.ForeColor = System.Drawing.Color.White
        Me.lbGlosa.Location = New System.Drawing.Point(14, 253)
        Me.lbGlosa.Name = "lbGlosa"
        Me.lbGlosa.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbGlosa.Size = New System.Drawing.Size(98, 18)
        Me.lbGlosa.TabIndex = 374
        Me.lbGlosa.Text = "Glosa:"
        '
        'lbBanco
        '
        Me.lbBanco.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbBanco.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbBanco.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbBanco.ForeColor = System.Drawing.Color.White
        Me.lbBanco.Location = New System.Drawing.Point(297, 86)
        Me.lbBanco.Name = "lbBanco"
        Me.lbBanco.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbBanco.Size = New System.Drawing.Size(66, 18)
        Me.lbBanco.TabIndex = 373
        Me.lbBanco.Text = "Banco:"
        '
        'tbGlosa
        '
        Me.tbGlosa.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbGlosa.Border.Class = "TextBoxBorder"
        Me.tbGlosa.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbGlosa.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbGlosa.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbGlosa.Location = New System.Drawing.Point(148, 253)
        Me.tbGlosa.MaxLength = 500
        Me.tbGlosa.Name = "tbGlosa"
        Me.tbGlosa.PreventEnterBeep = True
        Me.tbGlosa.Size = New System.Drawing.Size(349, 22)
        Me.tbGlosa.TabIndex = 372
        '
        'btgrupo1
        '
        Me.btgrupo1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btgrupo1.BackColor = System.Drawing.Color.Transparent
        Me.btgrupo1.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.btgrupo1.Image = Global.DinoM.My.Resources.Resources.add
        Me.btgrupo1.ImageFixedSize = New System.Drawing.Size(25, 23)
        Me.btgrupo1.Location = New System.Drawing.Point(257, 16)
        Me.btgrupo1.Name = "btgrupo1"
        Me.btgrupo1.Size = New System.Drawing.Size(28, 23)
        Me.btgrupo1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btgrupo1.TabIndex = 369
        Me.btgrupo1.Visible = False
        '
        'LabelX20
        '
        Me.LabelX20.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX20.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX20.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX20.ForeColor = System.Drawing.Color.White
        Me.LabelX20.Location = New System.Drawing.Point(14, 20)
        Me.LabelX20.Name = "LabelX20"
        Me.LabelX20.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX20.Size = New System.Drawing.Size(90, 18)
        Me.LabelX20.TabIndex = 370
        Me.LabelX20.Text = "Tipo Cambio:"
        '
        'tbMontoTarej
        '
        '
        '
        '
        Me.tbMontoTarej.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbMontoTarej.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMontoTarej.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbMontoTarej.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMontoTarej.ForeColor = System.Drawing.Color.Black
        Me.tbMontoTarej.Increment = 1.0R
        Me.tbMontoTarej.Location = New System.Drawing.Point(150, 110)
        Me.tbMontoTarej.MinValue = 0R
        Me.tbMontoTarej.Name = "tbMontoTarej"
        Me.tbMontoTarej.Size = New System.Drawing.Size(120, 26)
        Me.tbMontoTarej.TabIndex = 101
        Me.tbMontoTarej.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'chbTarjeta
        '
        Me.chbTarjeta.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.chbTarjeta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chbTarjeta.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chbTarjeta.Location = New System.Drawing.Point(14, 110)
        Me.chbTarjeta.Name = "chbTarjeta"
        Me.chbTarjeta.Size = New System.Drawing.Size(118, 30)
        Me.chbTarjeta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chbTarjeta.TabIndex = 279
        Me.chbTarjeta.Text = "Transferencia:"
        Me.chbTarjeta.TextColor = System.Drawing.Color.White
        '
        'tbMontoBs
        '
        '
        '
        '
        Me.tbMontoBs.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbMontoBs.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMontoBs.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbMontoBs.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMontoBs.ForeColor = System.Drawing.Color.Black
        Me.tbMontoBs.Increment = 1.0R
        Me.tbMontoBs.Location = New System.Drawing.Point(150, 47)
        Me.tbMontoBs.MinValue = 0R
        Me.tbMontoBs.Name = "tbMontoBs"
        Me.tbMontoBs.Size = New System.Drawing.Size(120, 26)
        Me.tbMontoBs.TabIndex = 2
        Me.tbMontoBs.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'LabelX5
        '
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.ForeColor = System.Drawing.Color.White
        Me.LabelX5.Location = New System.Drawing.Point(15, 51)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX5.Size = New System.Drawing.Size(98, 18)
        Me.LabelX5.TabIndex = 64
        Me.LabelX5.Text = "M.Pagado Bs.:"
        '
        'tbMontoDolar
        '
        '
        '
        '
        Me.tbMontoDolar.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbMontoDolar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMontoDolar.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbMontoDolar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMontoDolar.ForeColor = System.Drawing.Color.Black
        Me.tbMontoDolar.Increment = 1.0R
        Me.tbMontoDolar.Location = New System.Drawing.Point(150, 78)
        Me.tbMontoDolar.MinValue = 0R
        Me.tbMontoDolar.Name = "tbMontoDolar"
        Me.tbMontoDolar.Size = New System.Drawing.Size(120, 26)
        Me.tbMontoDolar.TabIndex = 100
        Me.tbMontoDolar.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.ForeColor = System.Drawing.Color.White
        Me.LabelX6.Location = New System.Drawing.Point(15, 82)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX6.Size = New System.Drawing.Size(60, 18)
        Me.LabelX6.TabIndex = 66
        Me.LabelX6.Text = "Mont. $:"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.BackgroundImage = Global.DinoM.My.Resources.Resources.fondo1
        Me.GroupBox3.Controls.Add(Me.tbCostoEnvio)
        Me.GroupBox3.Controls.Add(Me.lbCostoEnvio)
        Me.GroupBox3.Controls.Add(Me.lbCredito)
        Me.GroupBox3.Controls.Add(Me.tbFechaVenc)
        Me.GroupBox3.Controls.Add(Me.LabelX1)
        Me.GroupBox3.Controls.Add(Me.swTipoVenta)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Gold
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(526, 91)
        Me.GroupBox3.TabIndex = 365
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Tipo de Venta/Costos"
        '
        'tbCostoEnvio
        '
        '
        '
        '
        Me.tbCostoEnvio.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbCostoEnvio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCostoEnvio.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbCostoEnvio.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCostoEnvio.ForeColor = System.Drawing.Color.Black
        Me.tbCostoEnvio.Increment = 1.0R
        Me.tbCostoEnvio.Location = New System.Drawing.Point(114, 52)
        Me.tbCostoEnvio.MinValue = 0R
        Me.tbCostoEnvio.Name = "tbCostoEnvio"
        Me.tbCostoEnvio.Size = New System.Drawing.Size(120, 26)
        Me.tbCostoEnvio.TabIndex = 374
        Me.tbCostoEnvio.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'lbCostoEnvio
        '
        Me.lbCostoEnvio.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbCostoEnvio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbCostoEnvio.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCostoEnvio.ForeColor = System.Drawing.Color.White
        Me.lbCostoEnvio.Location = New System.Drawing.Point(10, 57)
        Me.lbCostoEnvio.Name = "lbCostoEnvio"
        Me.lbCostoEnvio.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbCostoEnvio.Size = New System.Drawing.Size(98, 18)
        Me.lbCostoEnvio.TabIndex = 375
        Me.lbCostoEnvio.Text = "Costo Envío:"
        '
        'lbCredito
        '
        Me.lbCredito.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbCredito.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbCredito.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lbCredito.ForeColor = System.Drawing.Color.White
        Me.lbCredito.Location = New System.Drawing.Point(243, 28)
        Me.lbCredito.Name = "lbCredito"
        Me.lbCredito.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbCredito.Size = New System.Drawing.Size(127, 18)
        Me.lbCredito.TabIndex = 373
        Me.lbCredito.Text = "Fecha Venc. Créd.:"
        '
        'tbFechaVenc
        '
        '
        '
        '
        Me.tbFechaVenc.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbFechaVenc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaVenc.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbFechaVenc.ButtonDropDown.Visible = True
        Me.tbFechaVenc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaVenc.ForeColor = System.Drawing.Color.Black
        Me.tbFechaVenc.IsPopupCalendarOpen = False
        Me.tbFechaVenc.Location = New System.Drawing.Point(376, 25)
        '
        '
        '
        '
        '
        '
        Me.tbFechaVenc.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaVenc.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbFechaVenc.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbFechaVenc.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaVenc.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.tbFechaVenc.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbFechaVenc.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbFechaVenc.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaVenc.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbFechaVenc.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaVenc.MonthCalendar.TodayButtonVisible = True
        Me.tbFechaVenc.Name = "tbFechaVenc"
        Me.tbFechaVenc.Size = New System.Drawing.Size(120, 22)
        Me.tbFechaVenc.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaVenc.TabIndex = 372
        Me.tbFechaVenc.Visible = False
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX1.ForeColor = System.Drawing.Color.White
        Me.LabelX1.Location = New System.Drawing.Point(11, 27)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(90, 18)
        Me.LabelX1.TabIndex = 371
        Me.LabelX1.Text = "Tipo Venta:"
        '
        'swTipoVenta
        '
        '
        '
        '
        Me.swTipoVenta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swTipoVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swTipoVenta.IsReadOnly = True
        Me.swTipoVenta.Location = New System.Drawing.Point(114, 24)
        Me.swTipoVenta.Name = "swTipoVenta"
        Me.swTipoVenta.OffBackColor = System.Drawing.Color.LawnGreen
        Me.swTipoVenta.OffText = "CREDITO"
        Me.swTipoVenta.OnBackColor = System.Drawing.Color.Gold
        Me.swTipoVenta.OnText = "CONTADO"
        Me.swTipoVenta.Size = New System.Drawing.Size(120, 22)
        Me.swTipoVenta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swTipoVenta.TabIndex = 233
        Me.swTipoVenta.Value = True
        Me.swTipoVenta.ValueObject = "Y"
        '
        'F1_MontoPagar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 518)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "F1_MontoPagar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "F1_MontoPagar"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grTransferencia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFavor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbCambioDolar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbBanco, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMontoTarej, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMontoBs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMontoDolar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.tbCostoEnvio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFechaVenc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents tbMontoTarej As DevComponents.Editors.DoubleInput
    Friend WithEvents chbTarjeta As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents tbMontoBs As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbMontoDolar As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtMontoPagado1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtCambio1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbCambio As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbMontoPagado As DevComponents.DotNetBar.LabelX
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents BtnSalir As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnContinuar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btgrupo1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX20 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents swTipoVenta As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents lbCredito As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaVenc As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents lbGlosa As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbBanco As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbGlosa As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents cbBanco As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents cbCambioDolar As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents tbCostoEnvio As DevComponents.Editors.DoubleInput
    Friend WithEvents lbCostoEnvio As DevComponents.DotNetBar.LabelX
    Friend WithEvents saldoT As DevComponents.DotNetBar.LabelX
    Friend WithEvents SFavor As DevComponents.Editors.DoubleInput
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btAddTrans As DevComponents.DotNetBar.ButtonX
    Friend WithEvents grTransferencia As Janus.Windows.GridEX.GridEX
End Class
