﻿Imports Modelo.MGlobal
Public Class ModeloF0
    Public Sub New()
        MyBase.New()

        Controls.Clear()

        InitializeComponent()

        Me.SuspendLayout()



        Me.ResumeLayout()
    End Sub

    Private Sub ModeloHor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress

        If (gs_mayusuculas <> 0) Then
            e.KeyChar = e.KeyChar.ToString.ToUpper
        End If

        If (e.KeyChar = ChrW(Keys.Enter)) Then
            e.Handled = True
            P_Moverenfoque()
        End If
    End Sub

    Private Sub P_Moverenfoque()
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub MFlyoutUsuario_PrepareContent(sender As Object, e As EventArgs) Handles MFlyoutUsuario.PrepareContent
        If sender Is BubbleBarUsuario And btnGrabar.Enabled = False Then
            MFlyoutUsuario.BorderColor = Color.FromArgb(&HC0, 0, 0)
            MFlyoutUsuario.Content = PanelUsuario
        End If
    End Sub

    Private Sub ModeloF0_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.WindowState = FormWindowState.Maximized
        TxtNombreUsu.Text = MGlobal.gs_usuario
        TxtNombreUsu.ReadOnly = True
        MPanelUserAct.Visible = False
    End Sub
End Class