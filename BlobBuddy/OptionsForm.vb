Public Class OptionsForm
    Public allowClose

    Private Sub OptionsForm_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosed
        Form1.Caine.MoveTo(320, 240)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to reset the program's settings? Your data cannot be recovered unless you have a backup of your settings. This won't affect your highscore on the game or the custom songs. Do you wish to proceed?", "Circus Pals", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.Yes Then
            My.Settings.isFirstRun = True
            My.Settings.isGoogleSearch = False
            My.Settings.IsVista = False
            My.Settings.isRandomSpeech = True
            My.Settings.MinimizeOnClose = True
            My.Settings.TrackBarValue = 2
            My.Settings.Name = "User"
            My.Settings.Theme = 2
            Form1.BackgroundImage = My.Resources.CircusBackground
            Form1.Label2.Show()
            My.Settings.is64bit = 2
            My.Settings.Save()
            My.Settings.InteractHelp = False
            My.Settings.MashTalk = True
            My.Settings.AudioTalk = True
            MsgBox("Circus Pals settings reset. IsFirstRun from FALSE to TRUE, Build string disabled, Search Engine set to Wiby, Circus set as default theme. Bit settings reset. Random speech settings set to default. Minimize on Close enabled. Name has been reset! Please close and re-open the application!", MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        My.Settings.Save()
        If RadioButton2.Checked = True Then
            My.Settings.isGoogleSearch = True
            My.Settings.Save()
        Else
            If RadioButton2_3.Checked = True Then
                My.Settings.IsVista = True
                My.Settings.Save()
            Else
                My.Settings.isGoogleSearch = False
                My.Settings.IsVista = False
                My.Settings.Save()
            End If
        End If
        If RadioButton4.Checked = True Then
            Form1.Label2.Show()
            Form1.ComputerUpdate.Show()
            Form1.BackgroundImage = My.Resources.computerbg2
            Form1.JungleUpdate.Hide()
            Form1.ComputerBackgroundOptimizer.Show()
            Form1.JungleBGModule.Hide()
            My.Settings.Theme = 1
            My.Settings.Save()
        Else
            Form1.ComputerUpdate.Hide()
            Form1.JungleUpdate.Show()
            Form1.BackgroundImage = My.Resources.CircusBackground
            Form1.ComputerBackgroundOptimizer.Hide()
            Form1.JungleBGModule.Show()
            Form1.Label2.Hide()

            My.Settings.Theme = 2
            My.Settings.Save()
        End If

        My.Settings.Name = TextBox1.Text
        My.Settings.Save()
        If CheckBox1.Checked Then
            Form1.BuildNumber.Show()
            My.Settings.BuildString = True
            My.Settings.Save()
        Else
            Form1.BuildNumber.Hide()
            My.Settings.BuildString = False
            My.Settings.Save()
        End If
        If RadioButton32.Checked = True Then
            My.Settings.is64bit = 0
            My.Settings.Save()
        End If
        If RadioButton64.Checked = True Then
            My.Settings.is64bit = 1
            My.Settings.Save()
        End If
        ' RANDOM TALK BAR
        If RandomTalkBar.Visible = False Then
            Form1.RandomSpeechTimer.Dispose()
            My.Settings.isRandomSpeech = False
            My.Settings.Save()
        Else
            Form1.RandomSpeechTimer.Start()
            My.Settings.isRandomSpeech = True
            My.Settings.Save()

        End If
        ' Random TALK BAR VALUE
        If RandomTalkBar.Value = 3 Then
            Form1.RandomSpeechTimer.Interval = 300000
            My.Settings.TrackBarValue = 3
            My.Settings.Save()
        End If
        If RandomTalkBar.Value = 2 Then
            Form1.RandomSpeechTimer.Interval = 120000
            My.Settings.TrackBarValue = 2
            My.Settings.Save()
        End If
        If RandomTalkBar.Value = 1 Then
            Form1.RandomSpeechTimer.Interval = 60000
            My.Settings.TrackBarValue = 1
            My.Settings.Save()
        End If

        MsgBox("Your settings have been applied!", MessageBoxIcon.Information)
    End Sub

    Private Sub OptionsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim num As Integer = MyBase.Location.X - 20
        Dim num2 As Integer = MyBase.Location.Y - 120
        Form1.Caine.MoveTo(CShort(num), CShort(num2))
        ' RANDOM SPEECH CHECKBOX
        If My.Settings.isRandomSpeech = False Then
            CheckBox2.Checked = True
        End If
        'RANDOM SPEECH SLIDER
        If My.Settings.TrackBarValue = 1 Then
            RandomTalkBar.Value = 1
        End If
        If My.Settings.TrackBarValue = 2 Then
            RandomTalkBar.Value = 2
        End If
        If My.Settings.TrackBarValue = 3 Then
            RandomTalkBar.Value = 3
        End If
        'EVERYTHING ELSE
        If My.Settings.is64bit = 1 Then
            RadioButton64.Checked = True
        End If
        If My.Settings.is64bit = 0 Then
            RadioButton32.Checked = True
        End If
        If My.Settings.isGoogleSearch = True Then
            RadioButton2.Checked = True
        Else
            If My.Settings.IsVista = True Then
                RadioButton2_3.Checked = True
            Else
                RadioButton1.Checked = True
            End If
        End If
        If My.Settings.Theme = 1 Then
            RadioButton4.Checked = True
        Else
            RadioButton3.Checked = True
        End If
        TextBox1.Text = My.Settings.Name
        If My.Settings.BuildString = True Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
        If My.Settings.MinimizeOnClose = True Then
            CheckBox3.Checked = True
        Else
            CheckBox3.Checked = False
        End If
        If My.Settings.InteractHelp = True Then
            CheckBox4.Checked = True
        End If
        If My.Settings.InteractHelp = False Then
            CheckBox4.Checked = False
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox2.Text = "85"
    End Sub


    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            RandomTalkBar.Visible = False
        Else
            RandomTalkBar.Visible = True
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            My.Settings.MinimizeOnClose = True
            allowClose = False
            My.Settings.Save()
        Else
            My.Settings.MinimizeOnClose = False
            allowClose = True
            My.Settings.Save()
        End If
    End Sub

    Public Sub AgentAdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AgentAdButton.Click
        Form1.AxAgent1.PropertySheet.Visible = True
        Dim num As Integer = Form1.AxAgent1.PropertySheet.Left - 20
        Dim num2 As Integer = Form1.AxAgent1.PropertySheet.Top - 120
        Form1.Caine.Stop()
        Form1.Caine.GestureAt(Form1.AxAgent1.PropertySheet.Left, Form1.AxAgent1.PropertySheet.Top)
        Form1.Caine.Speak("Hey! That looks like the Microsoft Agent advanced options window!")
        Form1.Caine.MoveTo(CShort(num), CShort(num2))
        Form1.Caine.Play("Blink")
        Form1.Caine.Speak("You can change many ways we agents interact with you here.")
    End Sub
    Private Sub Auto()
        If Form1.AxAgent1.PropertySheet.Visible = False Then
            Dim num As Integer = MyBase.Location.X - 20
            Dim num2 As Integer = MyBase.Location.Y - 120
            Form1.Caine.MoveTo(CShort(num), CShort(num2))
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            My.Settings.InteractHelp = True
            Form1.Caine.Play("Pleased")
            Form1.Caine.Speak("You want my help again? That is amazing!|That's the spirit!")
            Form1.Caine.Play("Blink")
            My.Settings.Save()
        Else
            My.Settings.InteractHelp = False
            Form1.Caine.Play("Sad")
            Form1.Caine.Speak("Okay, but I'm going to miss helping you when you load an audio or Mash file..|Fine, I'll shut the heck up...")
            Form1.Caine.Play("Blink")
            My.Settings.Save()
        End If
    End Sub
End Class