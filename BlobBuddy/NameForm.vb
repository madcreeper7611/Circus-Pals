
Imports System.Timers

Public Class NameForm

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)

    End Sub

    Private Delegate Sub CloseFormCallback()

    Private Sub CloseForm()

    End Sub

    Private Sub OnTimedEvent(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        '  CloseForm()
    End Sub

    Private Sub NameForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Form1.Timer2.Start()
        If TextBox1.Text = "" Then
            My.Settings.Name = Environment.UserName
            Form1.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
            My.Settings.Save()
        Else
            Form1.Timer2.Start()
            My.Settings.Save()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'NameDecided()
        Form1.Timer2.Start()
        My.Settings.Name = TextBox1.Text
        Form1.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
        My.Settings.Save()
        Me.Close()
    End Sub

    'Private Sub TextBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Enter
    '    NameDecided()
    'End Sub

    'Private Sub NameDecided()
    '    Form1.Timer2.Start()
    '    My.Settings.Name = TextBox1.Text
    '    Form1.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
    '    My.Settings.Save()
    '    Me.Close()
    'End Sub

    Private Sub NameForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim num As Integer = MyBase.Location.X - 20
        Dim num2 As Integer = MyBase.Location.Y - 120
        Form1.Caine.MoveTo(CShort(num), CShort(num2))
    End Sub
End Class