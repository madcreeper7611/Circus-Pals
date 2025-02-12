<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GamesWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GamesWindow))
        Me.Game_01 = New System.Windows.Forms.PictureBox
        Me.Game_02 = New System.Windows.Forms.PictureBox
        Me.Game_03 = New System.Windows.Forms.PictureBox
        Me.Game_04 = New System.Windows.Forms.PictureBox
        CType(Me.Game_01, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Game_02, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Game_03, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Game_04, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Game_01
        '
        Me.Game_01.BackColor = System.Drawing.Color.Maroon
        Me.Game_01.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Game_01.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Game_01.Image = Global.CircusPals.My.Resources.Resources.game_gloinks
        Me.Game_01.Location = New System.Drawing.Point(77, 88)
        Me.Game_01.Name = "Game_01"
        Me.Game_01.Size = New System.Drawing.Size(240, 128)
        Me.Game_01.TabIndex = 1
        Me.Game_01.TabStop = False
        '
        'Game_02
        '
        Me.Game_02.BackColor = System.Drawing.Color.Maroon
        Me.Game_02.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Game_02.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Game_02.Image = Global.CircusPals.My.Resources.Resources.game_candy
        Me.Game_02.Location = New System.Drawing.Point(323, 88)
        Me.Game_02.Name = "Game_02"
        Me.Game_02.Size = New System.Drawing.Size(240, 128)
        Me.Game_02.TabIndex = 2
        Me.Game_02.TabStop = False
        '
        'Game_03
        '
        Me.Game_03.BackColor = System.Drawing.Color.Maroon
        Me.Game_03.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Game_03.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Game_03.Image = Global.CircusPals.My.Resources.Resources.game_haunt
        Me.Game_03.Location = New System.Drawing.Point(77, 222)
        Me.Game_03.Name = "Game_03"
        Me.Game_03.Size = New System.Drawing.Size(240, 128)
        Me.Game_03.TabIndex = 3
        Me.Game_03.TabStop = False
        '
        'Game_04
        '
        Me.Game_04.BackColor = System.Drawing.Color.Maroon
        Me.Game_04.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Game_04.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Game_04.Image = Global.CircusPals.My.Resources.Resources.game_spud
        Me.Game_04.Location = New System.Drawing.Point(323, 222)
        Me.Game_04.Name = "Game_04"
        Me.Game_04.Size = New System.Drawing.Size(240, 128)
        Me.Game_04.TabIndex = 4
        Me.Game_04.TabStop = False
        '
        'GamesWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.CircusPals.My.Resources.Resources.GameScreen
        Me.ClientSize = New System.Drawing.Size(632, 453)
        Me.Controls.Add(Me.Game_04)
        Me.Controls.Add(Me.Game_03)
        Me.Controls.Add(Me.Game_02)
        Me.Controls.Add(Me.Game_01)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "GamesWindow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pick a Game!"
        CType(Me.Game_01, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Game_02, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Game_03, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Game_04, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Game_01 As System.Windows.Forms.PictureBox
    Friend WithEvents Game_02 As System.Windows.Forms.PictureBox
    Friend WithEvents Game_03 As System.Windows.Forms.PictureBox
    Friend WithEvents Game_04 As System.Windows.Forms.PictureBox
End Class
