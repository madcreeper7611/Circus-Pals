Option Explicit On

Imports AgentObjects
Imports AxAgentObjects
Imports System.IO
Imports System.Text
Imports System.Media
Imports System.Diagnostics
Imports AgentServerObjects
Imports System.Management
Imports Microsoft.VisualBasic.CompilerServices

Public Class Form1
    Private WithEvents processCheckTimer As Timer
    Private notifyIcon As NotifyIcon
    Public allowClose
    Public Caine As IAgentCtlCharacterEx
    Shared Random As New Random
    Dim i As Integer
    Private player As New SoundPlayer()
    Public WithEvents AgentControl As Agent
    Const DATAPATH = "Caine.acs"

    Private Sub NotifyIcon_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RandomSpeechTimer.Start()
        If My.Settings.TrackBarValue = 1 Then
            Me.RandomSpeechTimer.Interval = 60000
        End If
        If My.Settings.TrackBarValue = 2 Then
            Me.RandomSpeechTimer.Interval = 120000
        End If
        If My.Settings.TrackBarValue = 3 Then
            Me.RandomSpeechTimer.Interval = 300000
        End If
        If My.Settings.isRandomSpeech = False Then
            RandomSpeechTimer.Dispose()
        End If
        StartProcessCheckTimer()

        If My.Settings.BuildString = False Then
            BuildNumber.Hide()
        Else
            BuildNumber.Show()
        End If
        If My.Settings.Theme = 1 Then
            Me.Label2.Show()
            Me.WackyWatch.Show()
            Me.ComputerUpdate.Hide()
            Me.JungleUpdate.Show()
            Me.BackgroundImage = My.Resources.PomniBackground
            Me.ComputerBackgroundOptimizer.Show()
            Me.JungleBGModule.Hide()
        End If
        If My.Settings.Theme = 2 Then
            Me.WackyWatch.Show()
            Me.JungleUpdate.Show()
            Me.ComputerUpdate.Hide()
            Me.BackgroundImage = My.Resources.CircusBackground
            Me.ComputerBackgroundOptimizer.Hide()
            Me.JungleBGModule.Show()
            Me.Label2.Hide()
        End If

        If My.Settings.MinimizeOnClose Then
            allowClose = False
        End If

        'THIS IS THE FIRST RUN

        If My.Settings.isFirstRun Then
            My.Settings.isFirstRun = False
            My.Settings.Save()
            Try
                AxAgent1.Characters.Load("Caine", DATAPATH)
                Caine = AxAgent1.Characters("Caine")
                Caine.LanguageID = 1033
                Timer1.Enabled = True ' Enable the Timer control
                My.Computer.Audio.Play(My.Resources.circuspals_firsttime, AudioPlayMode.Background)
                Caine.MoveTo(320, 240)
                Caine.TTSModeID = "{CA141FD0-AC7F-11D1-97A3-006008273000}"
                Caine.Show()
                Me.Show()
                Caine.Speak("\Vol=65535\\Pit=136\Welcome! \rst\\Vol=65535\\Spd=150\To The \emp\Amazing Digital Desktop!")
                Caine.Play("Restpose")
                Caine.Speak("\Vol=65535\I don't believe we've been properly introduced.")
                Caine.Play("Greet")
                Caine.Speak("\Vol=65535\My name is Caine!")
                Caine.Play("Restpose")
                Caine.Speak("\Vol=65535\What is \emp\ your name?")
            Catch Ex As Exception
                My.Computer.Audio.Play(My.Resources.msagent_error, AudioPlayMode.Background)
                Dim ErrorBox As DialogResult = MsgBox("You need to install MSAgent or Caine.acs to use the program, otherwise Caine won't show up." & vbCrLf & "" & vbCrLf & "Do you want to close the application? (NOTE: Clicking ""No"" will result in even more errors without MSAgent)", 65556, "Whoops!")
                If ErrorBox = DialogResult.Yes Then
                    allowClose = True
                    Me.Close()
                End If
            End Try
        Else
            Try
                AxAgent1.Characters.Load("Caine", DATAPATH)
                Caine = AxAgent1.Characters("Caine")
                Caine.LanguageID = 1033
                Caine.MoveTo(320, 240)
                Caine.TTSModeID = "{CA141FD0-AC7F-11D1-97A3-006008273000}"
                Caine.Show()
            Catch Ex As Exception
                My.Computer.Audio.Play(My.Resources.msagent_error, AudioPlayMode.Background)
                Dim ErrorBox As DialogResult = MsgBox("You need to install MSAgent or Caine.acs to use the program, otherwise Caine won't show up." & vbCrLf & "" & vbCrLf & "Do you want to close the application? (NOTE: Clicking ""No"" will result in even more errors without MSAgent)", 65556, "Whoops!")
                If ErrorBox = DialogResult.Yes Then
                    allowClose = True
                    Me.Close()
                End If
            End Try

            If CurrentDate.Text.Contains("Date") Then
                CurrentDate.Text = DateTime.Now.Date
            End If

            'THIS IS NOT THE FIRST RUN

            ' TIME CHECK
            Dim currentTime As DateTime = DateTime.Now
            Dim startTime As TimeSpan
            Dim endTime As TimeSpan
            startTime = TimeSpan.Parse("21:00")
            endTime = TimeSpan.Parse("06:00")
            If currentTime.TimeOfDay >= TimeSpan.Parse("21:00") OrElse currentTime.TimeOfDay < TimeSpan.Parse("06:00") Then
                ' Time between 9 PM and 6 AM
                Me.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
                Dim random As New Random()
                Dim randomNumber64 As Integer = random.Next(1, 6)

                Select Case randomNumber64
                    Case 1
                        Caine.Play("Uncertain")
                        Caine.Speak("\Vol=65535\Do you ever sleep, " + My.Settings.Name + "?")
                        Caine.Play("Blink")
                    Case 2
                        Caine.Play("Confused")
                        Caine.Speak("\Vol=65535\Wait! Why are you up so late?")
                        Caine.Play("Blink")
                    Case 3
                        Caine.Play("Alert")
                        Caine.Speak("\Vol=65535\I \emp\ really think you should be in bed, but okay, I'm here to entertain you.")
                        Caine.Play("Blink")
                    Case 4
                        Caine.Play("Surprised")
                        Caine.Speak("\Vol=65535\Surprised to see you up this late, " + My.Settings.Name + "!")
                        Caine.Play("Blink")
                    Case 5
                        Caine.Play("Uncertain")
                        Caine.Speak("\Vol=65535\It's getting dark, " + My.Settings.Name + ". Can't it wait?")
                        Caine.Play("Blink")
                End Select
            Else
                Me.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"

                Dim random As New Random()
                Dim randomNumber As Integer = random.Next(1, 6)

                Select Case randomNumber
                    Case 1
                        Caine.Play("Wave")
                        Caine.Speak("\Vol=65535\Welcome back, my \Map=""me-ah-wing""=""meowing""\ milkmaid!")
                        Caine.Play("Blink")
                    Case 2
                        Caine.Play("Wave")
                        Caine.Speak("\Vol=65535\Welcome back my little hard-shelled hamburger!")
                        Caine.Play("Blink")
                    Case 3
                        Caine.Play("Wave")
                        Caine.Speak("\Vol=65535\Nice to see you again, " + My.Settings.Name + "!")
                        Caine.Play("Blink")
                    Case 4
                        Caine.Play("Wave")
                        Caine.Speak("\Vol=65535\Hello, " + My.Settings.Name + "! Ready for another adventure?")
                        Caine.Play("Blink")
                    Case 5
                        Caine.Play("Wave")
                        Caine.Speak("\Vol=65535\Welcome back to the circus, " + My.Settings.Name + "! We've missed you!")
                        Caine.Play("Blink")
                End Select
            End If
            If currentTime.Month = 8 And currentTime.Day = 31 Then
                Caine.Play("Announce")
                Caine.Speak("\Vol=65535\Also Happy Birthday to Laser Boy!")
            End If
        End If
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Caine.Hide()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If Caine.Visible Then
            allowClose = True
            Timer3.Start()
            Caine.StopAll()

            Dim random As New Random()
            Dim randomNumber32 As Integer = random.Next(1, 11)

            Select Case randomNumber32
                Case 1
                    Caine.Play("Wave")
                    Caine.Speak("It hurts me to say goodbye, " + My.Settings.Name + ".")
                    Caine.Hide()
                Case 2
                    Caine.Play("Wave")
                    Caine.Speak("Until next time my friend!")
                    Caine.Hide()
                Case 3
                    Caine.Play("Wave")
                    Caine.Speak("Until next time, " + My.Settings.Name + ".")
                    Caine.Hide()
                Case 4
                    Caine.Play("Acknowledge")
                    Caine.Speak("Well, I guess I am done for today. Bye for now.")
                    Caine.Hide()
                Case 5
                    Caine.Play("Acknowledge")
                    Caine.Speak("It looks like my work here is done. See you later.")
                    Caine.Hide()
                Case 6
                    Caine.Play("wave")
                    Caine.Speak("I hope to see you again soon, " + My.Settings.Name + ".")
                    Caine.Hide()
                Case 7
                    Caine.Play("Surprised")
                    Caine.Speak("Oh no! Not the exit!")
                    Caine.Hide()
                Case 8
                    Caine.Play("RestPose")
                    Caine.Speak("Oh, guess I'll instantly disappear")
                    Caine.Play("Silly")
                Case 9
                    Caine.Play("Explain")
                    Caine.Speak("Well, I'm going to go drink water! It's been a \emp\while since I've done that.")
                    Caine.Hide()
                Case 10
                    Caine.Play("Restpose")
                    Caine.Speak("\emp\I am such a good \Pit=400\\Spd=250\\Map=""bah-ah-ah-ah-ah-ah-ah-ah-ah-ah-\rst\sahtmtornmhsrp;h;xhgzodpsosoaaah,""=""bo-""\")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Think("...")
                    Caine.Think("...")
                    Caine.Think("...")
                    Caine.Hide()
            End Select
        Else
            allowClose = True
            Me.Close()
        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        AboutBoxNew.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim random128 As New Random()
        Dim randomNumber As Integer = random128.Next(1, 4)

        Dim webAddress As String = "http://circuspals.w10.site"
        Select Case randomNumber
            Case 1
                Caine.Play("Alert")
                Caine.Speak("Home sweet home, here we come!")
            Case 2
                Caine.Play("Alert")
                Caine.Speak("OK! There's plenty of \emp\amazing things happening at our home!")
            Case 3
                Caine.Play("Alert")
                Caine.Speak("It's always nice to visit our home!")
        End Select
        Process.Start(webAddress)
    End Sub


    Private Sub SpeakPic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpeakPic.Click
        UtilPanel1.Show()
        UtilPanel3.Hide()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If SpeakBox.Text = "" Then
            Caine.Speak("You'll have to type something first.")
        Else
            Caine.Speak("\Chr=""Normal""\" + SpeakBox.Text)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Caine.Stop()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        UtilPanel1.Hide()
    End Sub

    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
        HandlePictureBox4ClickEvent()
    End Sub


    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        If SpeakBox.Text = "" Then
            Caine.Speak("You'll have to type something first.")
        Else
            Caine.Speak("\Chr=""Whisper""\" + SpeakBox.Text)
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        If SpeakBox.Text = "" Then
            Caine.Speak("You'll have to type something first.")
        Else
            Caine.Think(SpeakBox.Text)
        End If
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Caine.Speak("\rst\\Chr=""Normal""\")
        Caine.TTSModeID = "{CA141FD0-AC7F-11D1-97A3-006008273000}"
        Caine.Speak("Voice reset! Use this button again if Microsoft Agent or Double Agent glitch out or switch to SAPI 5 voice again! Click the Goodbye button if it still somehow continues to glitch out or use SAPI 5 voice.")
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        If SpeakBox.Text = "" Then
            Caine.Speak("You'll have to type something first.")
        Else
            Caine.Speak("\Chr=""Monotone""\" + SpeakBox.Text)
        End If
    End Sub


    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click
        HandlePictureBox5ClickEvent()

    End Sub

    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
        player.Stop()
        Caine.StopAll()
        Caine.Play("Blink")
    End Sub

    Private Sub PictureBox9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox9.Click
        UtilPanel1.Hide()
        UtilPanel3.Show()
      
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        If Caine.Visible Then
            Caine.Speak("Sorry my friend. I am already here!")
        Else
            Caine.Show()
        End If
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        If ComboBox1.Text = "" Then

            Caine.Speak("You'll have to select one first.")
        Else
            Caine.Play(ComboBox1.Text)
        End If

    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        UtilPanel1.Hide()

        UtilPanel3.Hide()
    End Sub

    Private Sub AxAgent1_ClickEvent(ByVal sender As Object, ByVal e As AxAgentObjects._AgentEvents_ClickEvent) Handles AxAgent1.ClickEvent
        Me.Show()
        NotifyIcon1.Visible = False
    End Sub

    Private Sub AxAgent1_DblClick(ByVal sender As Object, ByVal e As AxAgentObjects._AgentEvents_DblClickEvent) Handles AxAgent1.DblClick
        Dim random5 As New Random()
        Dim randomNumber As Integer = random5.Next(1, 20)
        Caine.StopAll()
        Select Case randomNumber
            Case 1
                Caine.Play("Surprised")
                Caine.Speak("Hey! Don't double click me!")
            Case 2
                Caine.Play("Confused")
                Caine.Speak("Uh, can I help you?")
            Case 3
                Caine.Play("Uncertain")
                Caine.Speak("Please don't double click, I'm a bit sensitive.")
            Case 4
                Caine.Play("Surprised")
                Caine.Speak("Owie! Careful with that thing!")
            Case 5
                Caine.Play("Decline")
                Caine.Speak("I'm gonna eventually put you in the \Map=""time out""=""timeout""\ corner if you keep double clicking!")
            Case 6
                Caine.Play("Uncertain")
                Caine.Speak("A bit clicky today, aren't we?")
            Case 7
                Caine.Play("Acknowledge")
                Caine.Speak("I'm at your service.")
            Case 8
                Caine.Play("Uncertain")
                Caine.Speak("Thanks for pointing that out.")
            Case 9
                Caine.Play("Surprised")
                Caine.Speak("Watch it with that mouse pointer!")
            Case 10
                Caine.Play("Uncertain")
                Caine.Speak("Yes?")
            Case 11
                Caine.Play("Uncertain")
                Caine.Speak("Are you pointing at me?")
            Case 12
                Caine.Play("Alert")
                Caine.Speak("Somebody's a bit fresh with the mouse pointer!")
            Case 13
                Caine.Play("Giggle")
                Caine.Speak("Ha ha! That tickles.")
            Case 14
                Caine.Play("Uncertain")
                Caine.Speak("What can I do for you, " + My.Settings.Name + "?")
            Case 15
                Caine.Play("Uncertain")
                Caine.Speak("The Circus Pals window has my interact menu.")
            Case 16
                Caine.Play("Uncertain")
                Caine.Speak("The Circus Pals window contains my search and interact menus.")
            Case 17
                Caine.Play("Uncertain")
                Caine.Speak("Try the Circus Pals window.")
            Case 18
                Caine.Play("Pleased")
                Caine.Speak("Heh, I don't mind you using the cursor as a back scratcher.")
            Case 19
                Caine.Play("Uncertain")
                Caine.Speak("If you're trying to pet me, the cursor is too sharp and pointy for that.")
        End Select
        Caine.Play("Blink")
    End Sub

    Private Sub AxAgent1_DragStart(ByVal sender As Object, ByVal e As AxAgentObjects._AgentEvents_DragStartEvent) Handles AxAgent1.DragStart
        Caine.Stop()
        Caine.Play("Surprised")
        Caine.Play("Blink")
    End Sub

    Private Sub AxAgent1_DragComplete(ByVal sender As Object, ByVal e As AxAgentObjects._AgentEvents_DragCompleteEvent) Handles AxAgent1.DragComplete
        Dim random5 As New Random()
        Dim randomNumber As Integer = random5.Next(1, 26)
        Select Case randomNumber
            ' temp solution, maybe perm if it just works?
            Case 1
                Caine.Speak("Hey " + My.Settings.Name + "! What gives?")
            Case 2
                Caine.Speak("Excuse me?")
            Case 3
                Caine.Speak("Are you doing this because you hate when I move on my own?")
            Case 4
                Caine.Speak("Woah! Put me down!")
            Case 5
                Caine.Speak("Huh. Not bad.")
            Case 6
                Caine.Speak("Oooooh!")
            Case 7
                Caine.Speak("OK, I guess this is my temporary spot now.")
            Case 8
                Caine.Speak("Uh, what?")
            Case 9
                Caine.Speak("Oh, is this where you wanted me?")
            Case 10
                Caine.Speak("I'll have a \emp\much better view from here.")
            Case 11
                Caine.Speak("Now, what was wrong with being over there?")
            Case 12
                Caine.Speak("Thanks for putting me in my place.")
            Case 13
                Caine.Speak("I've finally arrived!")
            Case 14
                Caine.Speak("I'm not sure I like this spot!")
            Case 15
                Caine.Speak("This is a nice spot.")
            Case 16
                Caine.Speak("Change is always good.")
            Case 17
                Caine.Speak("OK OK! I'll stay over here.")
            Case 18
                Caine.Speak("This is a nicer spot anyway.")
            Case 19
                Caine.Speak("Sorry, didn't realize I was in your way.")
            Case 20
                Caine.Speak("Was I in your way?")
            Case 21
                Caine.Speak("That was a \emp\wild ride!")
            Case 22
                Caine.Speak("What a fresh perspective!")
            Case 23
                Caine.Speak("Moving is always a drag.")
            Case 24
                Caine.Speak("Really? Is this because of me moving Pomni?")
            Case 25
                Caine.Speak("Now why did you move me?")
        End Select
        Caine.Play("Blink")
    End Sub


    Private Sub MashButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MashButton.Click
        If My.Settings.MashTalk = True Or My.Settings.InteractHelp = True Then
            Caine.Speak("Select the name of the Mash script you want to open! When you're done, click the \emp\Open button.")
            Caine.Speak("If it has me in it, I recommend you hide me first before opening it!")
            My.Settings.MashTalk = False
        End If
        If My.Settings.is64bit = 2 Then
            Dim result As MsgBoxResult = MsgBox("Is your computer 64-bit?", MsgBoxStyle.YesNo Or MessageBoxIcon.Question, "MaxALERT Rewritten")


            If result = MsgBoxResult.Yes Then
                My.Settings.is64bit = 1
                My.Settings.Save()
            ElseIf result = MsgBoxResult.No Then
                My.Settings.is64bit = 0
                My.Settings.Save()
            End If
        End If
        If My.Settings.is64bit = 1 Then
            Dim openFileDialog1 As New OpenFileDialog()
            openFileDialog1.Filter = "Microsoft Agent Scripting Helper files|*.msh"
            Dim programFilesDir As String
            programFilesDir = Path.Combine("C:\Program Files (x86)", "bellcraft.com\MASH\")
            If openFileDialog1.ShowDialog() = DialogResult.OK Then
                Dim selectedFilePath As String = openFileDialog1.FileName
                Dim executablePath As String = "C:\Program Files (x86)\BellCraft.com\MASH\MASHPlay.exe"
                Process.Start(executablePath, selectedFilePath)
            End If
        End If
        If My.Settings.is64bit = 0 Then
            Dim openFileDialog1 As New OpenFileDialog()
            openFileDialog1.Filter = "Microsoft Agent Scripting Helper files|*.msh"
            Dim programFilesDir As String
            programFilesDir = Path.Combine("C:\Program Files", "bellcraft.com\MASH\")
            If openFileDialog1.ShowDialog() = DialogResult.OK Then
                Dim selectedFilePath As String = openFileDialog1.FileName
                Dim executablePath As String = "C:\Program Files\BellCraft.com\MASH\MASHPlay.exe"
                Process.Start(executablePath, selectedFilePath)
            End If
        End If

    End Sub

    Private Sub PictureBox10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox10.Click
        If My.Settings.AudioTalk = True Or My.Settings.InteractHelp = True Then
            Caine.Speak("Select the name of the file you want to open! When you're done, click the \emp\Open button!")
            My.Settings.AudioTalk = False
        End If
        OpenFileDialog2.Filter = ".WAV files|*.wav"
        OpenFileDialog2.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        If OpenFileDialog2.ShowDialog() = DialogResult.OK Then

            player.SoundLocation = OpenFileDialog2.FileName
            player.Play()
        End If
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        OptionsForm.Show()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False

        Dim form2 As New NameForm()
        form2.Show()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        If My.Settings.Name = "Pomni" Or My.Settings.Name = "Gangle" Or My.Settings.Name = "Zooble" Then
            My.Settings.Name = Environment.UserName
            Me.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
            My.Settings.Save()
            Caine.Play("Decline")
            Caine.Speak("\Vol=65535\Sorry, but that name is already taken.")
            Caine.Play("Blink")
            Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name = "Kinger" Or My.Settings.Name = "Ragatha" Or My.Settings.Name = "Jax" Then
            My.Settings.Name = Environment.UserName
            Me.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
            My.Settings.Save()
            Caine.Play("Decline")
            Caine.Speak("\Vol=65535\Sorry, but that name is already taken.")
            Caine.Play("Blink")
            Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Dylan") Or My.Settings.Name.Contains("dylan") Or My.Settings.Name.Contains("Breather") Or My.Settings.Name.Contains("breather") Then
            TimerOfDOOM.Start()
            Caine.StopAll()
            Caine.Play("RestPose")
            Caine.Speak("\Vol=65535\" + My.Settings.Name + "? Not so fast! Activating Self-Defense Mechanism!")
            Caine.Play("Giggle")
            Caine.Play("Decline")
            Caine.Speak("\Vol=65535\Don't worry, I didn't do anything stupid this time.")
            Caine.Play("Blink")
            Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("pager") Then
            Caine.Play("Think")
            Caine.Speak("\Vol=65535\" + My.Settings.Name + "? As in the real Pager? Odd to see you interested.")
            Caine.Play("Blink")
            Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("expand dong") Or My.Settings.Name.Contains("Expand Dong") Then
            My.Settings.Name = Environment.UserName
            Me.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
            My.Settings.Save()
            Caine.Play("Mad")
            Caine.Speak("\Vol=65535\Expand \emp\What?! I shall call you \emp\no such thing! I will refer to you as " + My.Settings.Name + " from now on!")
            Caine.Play("Blink")
            Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Vinesauce") Or My.Settings.Name.Contains("Joel") Then
            Caine.Play("Surprised")
            Caine.Speak("\Vol=65535\Is it really you, " + My.Settings.Name + "? I can't believe it, the legend is true!")
            Caine.Play("Blink")
            Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Mario") Or My.Settings.Name.Contains("mario") Then
            Caine.Play("Think")
            Caine.Speak("\Vol=65535\Why do I have a feeling this program might get a cease and desist?")
            Caine.Play("Blink")
            Caine.Speak("\Vol=65535\Oh whatever.")
        End If
        If My.Settings.Name.Contains("Luigi") Or My.Settings.Name.Contains("luigi") Then
            Caine.Play("Confused")
            Caine.Speak("\Vol=65535\Um... Can we talk about this..?")
            Caine.Play("Mad")
            Caine.Speak("\Vol=65535\Oh forget it!")
        End If
        If My.Settings.Name.Equals("Big") Or My.Settings.Name.Equals("big") Then
            Caine.Play("Surprised")
            Caine.Height = 256
            Caine.Width = 256
            Caine.Speak("\Vol=65535\Oh no what have you done! Good thing this is temporary! Alright, back to my regular script!")
        End If
        If My.Settings.Name.Equals("Small") Or My.Settings.Name.Equals("small") Then
            Caine.Play("Surprised")
            Caine.Height = 64
            Caine.Width = 64
            Caine.Speak("\Vol=65535\Oh no what have you done! Good thing this is temporary! Alright, back to my regular script!")
        End If
        If My.Settings.Name.Equals("Gargantuan") Or My.Settings.Name.Equals("gargantuan") Then
            Caine.Play("Surprised")
            Caine.Height = 384
            Caine.Width = 384
            Caine.Speak("\Vol=65535\Oh no what have you done! Good thing this is temporary! Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Crazy Dave") Or My.Settings.Name.Contains("crazy dave") Then
            Caine.Play("Think")
            Caine.Speak("\Vol=65535\You're like Kinger, right? You came here because there are zombies in the Mildenhall Manor?")
            Caine.Play("Explain")
            Caine.Speak("\Vol=65535\We'll talk later after this, OK?")
            Caine.Play("Blink")
            Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Clippy") Or My.Settings.Name.Contains("Clippit") Then
            Caine.Play("Alert")
            Caine.Speak("\Vol=65535\" + My.Settings.Name + "! Here's a message from your browser! \Chr=""Whisper""\Seizure warning!")
            Caine.Play("Giggle")
            Try
                Dim idiot As String = "http://piv.pivpiv.dk/"
                Process.Start(idiot)
            Catch ex As Exception
                Caine.Speak("\Chr=""Normal""\There was an error executing the Easter egg command.")
            End Try
            Caine.Play("Blink")
            Caine.Speak("\Chr=""Normal""\\Vol=65535\Alright, back to my regular script!")
        End If
        Caine.Play("Acknowledge")
        Caine.Speak("\Vol=65535\" + My.Settings.Name + "! An \emp\interesting choice.|\Vol=65535\Nice to meet you, " + My.Settings.Name + "!")
        Caine.Play("Explain")
        Caine.Speak("\Vol=65535\Since this is the first time we have met, I'd like to tell you a little about myself.")
        Caine.Play("Announce")
        Caine.Speak("\Vol=65535\I am your friend, ringmaster and part of the Circus Pals lineup! I have the ability to learn from and entertain you. The more we browse, search, and travel the internet together, the smarter I'll become!")
        Caine.Play("Silly")
        Caine.Speak("\Vol=65535\Well, not that I'm not \emp\ already smart.")
        Caine.Play("Explain")
        Caine.Speak("\Vol=65535\Because the Internet can feel like a circus at times, I can help you find what you are looking for and even make suggestions as to where we should go to find it! The more time we spend together, the closer we'll become!")
        Caine.Play("Sad")
        Caine.Speak("\Vol=65535\I may be one of the smallest friends you have " + My.Settings.Name + ", but I will always try and make up for that with my big heart!")
        Caine.Play("DoMagic1")
        Caine.Speak("\Vol=65535\When you feel bored and feel like going on an adventure, I have a couple games preinstalled and ready to go. Adventures \emp\always keep you sane!")
        Caine.Play("DoMagic2")
        Caine.Play("Think")
        Caine.Speak("\Vol=65535\Am I rambling?")
        Caine.Play("Acknowledge")
        Caine.Speak("\Vol=65535\Alrighty then " + My.Settings.Name + ", feel free to look around.")
    End Sub

    Private Sub Timer3_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Me.Close()
    End Sub
    Private Sub CheckBandicam()
        Dim processes() As Process = Process.GetProcessesByName("Bandicam")

        If processes.Length > 0 Then
            Caine.Speak("Heya, if you're going to record a demonstration of my program, at \emp\least use something else other than Bandicam.")
        End If
    End Sub

    Private Sub NotifyIcon1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = MouseButtons.Left Then
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            NotifyIcon1.Visible = False
        End If
        If e.Button = MouseButtons.Right Then
            ExitToolStripMenu.Show()
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub ComputerUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComputerUpdate.Click
        Caine.Speak("Your current version is 2.0! Let's launch my web page to see if there's an update...")
        Dim webAddress As String = "http://circuspals.w10.site/update.html?version=2.0.0"
        Process.Start(webAddress)
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Caine.Play("Search")
        If My.Settings.isGoogleSearch = True Then
            Dim webAddress As String = "http://frogfind.com/?q=" + KeywordTextBox.Text
            Process.Start(webAddress)
        Else
            If My.Settings.IsVista = True Then
                Dim webAddress As String = "http://oldavista.com/search?l=english&q=" + KeywordTextBox.Text
                Process.Start(webAddress)
            Else
                Dim webAddress As String = "http://wiby.me/?q=" + KeywordTextBox.Text
                Process.Start(webAddress)
            End If
        End If
    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        If URLTextBox.Text = "http://nhc.noaa.gov" OrElse URLTextBox.Text = "https://nhc.noaa.gov" Then
            Caine.Speak("Hurricanes are scary!")
        End If
        If URLTextBox.Text = "http://google.com" OrElse URLTextBox.Text = "https://google.com" Then
            Caine.Speak("Ok, lets Google! Remember not to look yourself up!")
        End If
        If URLTextBox.Text = "http://tmafe.com" OrElse URLTextBox.Text = "https://tmafe.com" Then
            Caine.Speak("What an awesome website!")
        End If
        If URLTextBox.Text = "http://youtube.com" OrElse URLTextBox.Text = "https://youtube.com" Then
            Caine.Play("Alert")
            Caine.Speak("Becareful! This site contains a \emp\lot of Content Farms!")
            Caine.Play("RestPose")
        End If
        If URLTextBox.Text = "http://bitview.com" OrElse URLTextBox.Text = "http://bitview.com" Then
            Caine.Speak("Gonna watch some videos, huh?")
        End If
        If URLTextBox.Text = "http://apple.com" OrElse URLTextBox.Text = "https://apple.com" Then
            Caine.Speak("Interesting!")
        End If
        If URLTextBox.Text = "http://microsoft.com" OrElse URLTextBox.Text = "https://microsoft.com" Then
            Caine.Speak("Oh wow, the people who bought \Map=""Mine-Craft!""=""MINECRAFT!""\ Anyways.")
        End If
        If URLTextBox.Text = "http://amazon.com" OrElse URLTextBox.Text = "https://amazon.com" Then
            Caine.Speak("Hey! Buy me some props!")
        End If
        If URLTextBox.Text = "http://digitalcircus.store" OrElse URLTextBox.Text = "http://digitalcircus.store" Then
            Caine.Speak("Remember! All merch sales go right back into funding the show!")
        End If
        If URLTextBox.Text = "http://reddit.com" OrElse URLTextBox.Text = "https://reddit.com" Then
            Caine.Speak("Sure, I guess i'll do it.")
        End If
        If URLTextBox.Text = "http://bonzi.link" OrElse URLTextBox.Text = "https://bonzi.link" Then
            Caine.Play("Surprised")
            Caine.Speak("What are you doing!? This is too dangerous!")
        End If
        If URLTextBox.Text = "http://bing.com" OrElse URLTextBox.Text = "https://bing.com" Then
            Caine.Speak("Huh. It rhymes with Ping. Anyways,")
        End If
        If URLTextBox.Text = "http://quora.com" OrElse URLTextBox.Text = "https://quora.com" Then
            Caine.Speak("Alright.")
        End If
        If URLTextBox.Text = "http://tiktok.com" OrElse URLTextBox.Text = "https://tiktok.com" Then
            Caine.Speak("Wait, isn't TikTok banned?")
        End If
        If URLTextBox.Text = "http://discord.com" OrElse URLTextBox.Text = "https://discord.com" Then
            Caine.Play("Alert")
            Caine.Speak("Becareful! There is some crazy stuff on this website!")
            Caine.Play("RestPose")
        End If
        If URLTextBox.Text = "http://laserboy.neocities.org" OrElse URLTextBox.Text = "http://laserboy.neocities.org" Then
            Caine.Speak("The boy with the laser!")
        End If
        If URLTextBox.Text = "http://mugmanfr.neocities.org" OrElse URLTextBox.Text = "http://mugmanfr.neocities.org" Then
            Caine.Speak("The mug, the man, the legend!")
        End If
        If URLTextBox.Text = "http://alexparr.net" OrElse URLTextBox.Text = "http://alexparr.net" Then
            Caine.Play("Alert")
            Caine.Speak("POTATO!")
            Caine.Play("RestPose")
        End If
        Caine.Play("Search")
        Dim webAddress As String = URLTextBox.Text
        Process.Start(webAddress)
    End Sub
    Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As DrawItemEventArgs) Handles TabControl1.DrawItem
        Dim tabPage As TabPage = TabControl1.TabPages(e.Index)
        Dim tabBounds As Rectangle = TabControl1.GetTabRect(e.Index)
        Dim paddedBounds As Rectangle = tabBounds

        ' Modify the tab bounds to create padding
        paddedBounds.Inflate(-2, -2)

        ' Fill the background
        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(255, 192, 128)), tabBounds)

        ' Set the text color
        Dim textColor As Color = Color.Black

        ' Draw the tab text
        TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, paddedBounds, textColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.WordEllipsis)

    End Sub

    Private Sub PictureBox13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox13.Click
        GamesWindow.Show()
    End Sub


    Private Sub StartProcessCheckTimer()
        processCheckTimer = New Timer()
        processCheckTimer.Interval = 1000 ' Check every second
        processCheckTimer.Start()
    End Sub

    Private Sub processCheckTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles processCheckTimer.Tick
        Dim processes As Process() = Process.GetProcessesByName("BonziBuddy432")
        Dim processes2 As Process() = Process.GetProcessesByName("BonziBDY")
        Dim processes3 As Process() = Process.GetProcessesByName("BBSetup")
        Dim processes4 As Process() = Process.GetProcessesByName("Bonzi")
        Dim processes5 As Process() = Process.GetProcessesByName("Bonzify")
        Dim processes6 As Process() = Process.GetProcessesByName("BonziKill")
        Dim processes7 As Process() = Process.GetProcessesByName("BonzaiKill")
        Dim processes8 As Process() = Process.GetProcessesByName("BonziBDY_2")
        Dim processes9 As Process() = Process.GetProcessesByName("BonziBDY_35")
        Dim processes10 As Process() = Process.GetProcessesByName("BonziBDY_4")

        If processes.Length > 0 Then
            For Each process As Process In processes
                Try
                    Caine.Play("DoMagic1")
                    Caine.Speak("Oops! Looks like one of \emp\those programs made it through!")
                    Caine.Play("DoMagic2")
                    process.Kill()
                    Caine.Speak("I know you \emp\love your desktop buddies " + My.Settings.Name + ", but if \emp\I start losing track of who's the OG Bonzi and who's the virus-free Bonzi,")
                    Caine.Play("Uncertain")
                    Caine.Speak("Who \emp\knows what could happen...")
                    ThreatDetected.Show()
                Catch ex As Exception

                End Try
            Next
        End If
        If processes2.Length > 0 Then
            For Each process As Process In processes
                Try
                    Caine.Play("DoMagic1")
                    Caine.Speak("Oops! Looks like one of \emp\those programs made it through!")
                    Caine.Play("DoMagic2")
                    process.Kill()
                    Caine.Speak("I know you \emp\love your desktop buddies " + My.Settings.Name + ", but if \emp\I start losing track of who's the OG Bonzi and who's the virus-free Bonzi,")
                    Caine.Play("Uncertain")
                    Caine.Speak("Who \emp\knows what could happen...")
                    ThreatDetected.Show()
                Catch ex As Exception

                End Try
            Next
        End If
        If processes3.Length > 0 Then
            For Each process As Process In processes
                Try
                    Caine.Play("DoMagic1")
                    Caine.Speak("Oops! Looks like one of \emp\those programs made it through!")
                    Caine.Play("DoMagic2")
                    process.Kill()
                    Caine.Speak("I know you \emp\love your desktop buddies " + My.Settings.Name + ", but if \emp\I start losing track of who's the OG Bonzi and who's the virus-free Bonzi,")
                    Caine.Play("Uncertain")
                    Caine.Speak("Who \emp\knows what could happen...")
                    ThreatDetected.Show()
                Catch ex As Exception

                End Try
            Next
        End If
        If processes4.Length > 0 Then
            For Each process As Process In processes
                Try
                    Caine.Play("DoMagic1")
                    Caine.Speak("Oops! Looks like one of \emp\those programs made it through!")
                    Caine.Play("DoMagic2")
                    process.Kill()
                    Caine.Speak("I know you \emp\love your desktop buddies " + My.Settings.Name + ", but if \emp\I start losing track of who's the OG Bonzi and who's the virus-free Bonzi,")
                    Caine.Play("Uncertain")
                    Caine.Speak("Who \emp\knows what could happen...")
                    ThreatDetected.Show()
                Catch ex As Exception

                End Try
            Next
        End If
        If processes5.Length > 0 Then
            For Each process As Process In processes
                Try
                    Caine.Play("DoMagic1")
                    Caine.Speak("Oops! Looks like one of \emp\those programs made it through!")
                    Caine.Play("DoMagic2")
                    process.Kill()
                    Caine.Speak("I know you \emp\love your desktop buddies " + My.Settings.Name + ", but if \emp\I start losing track of who's the OG Bonzi and who's the virus-free Bonzi,")
                    Caine.Play("Uncertain")
                    Caine.Speak("Who \emp\knows what could happen...")
                    ThreatDetected.Show()
                Catch ex As Exception

                End Try
            Next
        End If
        If processes6.Length > 0 Then
            For Each process As Process In processes
                Try
                    Caine.Play("DoMagic1")
                    Caine.Speak("Oops! Looks like one of \emp\those programs made it through!")
                    Caine.Play("DoMagic2")
                    process.Kill()
                    Caine.Speak("I know you \emp\love your desktop buddies " + My.Settings.Name + ", but if \emp\I start losing track of who's the OG Bonzi and who's the virus-free Bonzi,")
                    Caine.Play("Uncertain")
                    Caine.Speak("Who \emp\knows what could happen...")
                    ThreatDetected.Show()
                Catch ex As Exception

                End Try
            Next
        End If
        If processes7.Length > 0 Then
            For Each process As Process In processes
                Try
                    Caine.Play("DoMagic1")
                    Caine.Speak("Oops! Looks like one of \emp\those programs made it through!")
                    Caine.Play("DoMagic2")
                    process.Kill()
                    Caine.Speak("I know you \emp\love your desktop buddies " + My.Settings.Name + ", but if \emp\I start losing track of who's the OG Bonzi and who's the virus-free Bonzi,")
                    Caine.Play("Uncertain")
                    Caine.Speak("Who \emp\knows what could happen...")
                    ThreatDetected.Show()
                Catch ex As Exception

                End Try
            Next
        End If
        If processes8.Length > 0 Then
            For Each process As Process In processes
                Try
                    Caine.Play("DoMagic1")
                    Caine.Speak("Oops! Looks like one of \emp\those programs made it through!")
                    Caine.Play("DoMagic2")
                    process.Kill()
                    Caine.Speak("I know you \emp\love your desktop buddies " + My.Settings.Name + ", but if \emp\I start losing track of who's the OG Bonzi and who's the virus-free Bonzi,")
                    Caine.Play("Uncertain")
                    Caine.Speak("Who \emp\knows what could happen...")
                    ThreatDetected.Show()
                Catch ex As Exception

                End Try
            Next
        End If
        If processes9.Length > 0 Then
            For Each process As Process In processes
                Try
                    Caine.Play("DoMagic1")
                    Caine.Speak("Oops! Looks like one of \emp\those programs made it through!")
                    Caine.Play("DoMagic2")
                    process.Kill()
                    Caine.Speak("I know you \emp\love your desktop buddies " + My.Settings.Name + ", but if \emp\I start losing track of who's the OG Bonzi and who's the virus-free Bonzi,")
                    Caine.Play("Uncertain")
                    Caine.Speak("Who \emp\knows what could happen...")
                    ThreatDetected.Show()
                Catch ex As Exception

                End Try
            Next
        End If
        If processes10.Length > 0 Then
            For Each process As Process In processes
                Try
                    Caine.Play("DoMagic1")
                    Caine.Speak("Oops! Looks like one of \emp\those programs made it through!")
                    Caine.Play("DoMagic2")
                    process.Kill()
                    Caine.Speak("I know you \emp\love your desktop buddies " + My.Settings.Name + ", but if \emp\I start losing track of who's the OG Bonzi and who's the virus-free Bonzi,")
                    Caine.Play("Uncertain")
                    Caine.Speak("Who \emp\knows what could happen...")
                    ThreatDetected.Show()
                Catch ex As Exception

                End Try
            Next
        End If
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Convert.ToBoolean(Operators.AndObject(My.Settings.MinimizeOnClose, Operators.CompareObjectEqual(allowClose, False, False))) Then
            e.Cancel = True
            Me.Hide()
            NotifyIcon1.Visible = True
        End If

        If processCheckTimer IsNot Nothing Then
            processCheckTimer.Stop()
            processCheckTimer.Dispose()
        End If
    End Sub

    Private Sub RandomSpeechTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles RandomSpeechTimer.Tick
        Dim random128 As New Random()
        Dim randomNumber As Integer = random128.Next(1, 22)
        Select Case randomNumber
            Case 1
                Caine.Play("Explain")
                Caine.Speak(My.Settings.Name + "! Where did the time go?")
                Caine.Play("Pleased")
                Caine.Speak("Can't you just feel us getting closer with every new day!")
                Caine.Play("Blink")
            Case 2
                Caine.Play("Idle2_1")
                Caine.Speak("Don't mind me, I'm just playing along with Bubble.")
                Caine.Play("Restpose")
            Case 3
                Caine.Play("Confused")
                Caine.Speak("Hey! Where did you go?")
                Caine.Play("Restpose")
            Case 4
                Caine.Play("Pleased")
                Caine.Speak(My.Settings.Name + "! I've noticed you've been looking sharp as a tack these days!")
                Caine.Play("Blink")
            Case 5
                Caine.Play("Blink")
                Caine.Speak("Hmm, I should check up on my superstars.")
            Case 6
                Caine.Play("Pleased")
                Caine.Speak("Ah! What a nice day to do nothing!")
                Caine.Play("Blink")
            Case 7
                Caine.Play("Explain")
                Caine.Speak("Why not listen to some music? Or watch some Mash files? They're \emp\ sure to entertain you!")
                Caine.Play("Blink")
            Case 8
                Caine.Play("Think")
                Caine.Speak("Do you ever wonder if Joel would eventually feature me?")
                Caine.Play("Blink")
            Case 9
                Caine.Play("GestureDown")
                Caine.Speak("Hey, what are you doing?")
                Caine.Play("Blink")
            Case 10
                Caine.Play("Pleased")
                Caine.Speak("You're so fun to be around, " + My.Settings.Name + "!")
                Caine.Play("Blink")
            Case 11
                Caine.Play("Wave")
                Caine.Speak("Hey " + My.Settings.Name + ", you're looking quite nice today!")
                Caine.Play("Blink")
                ' temp solution, maybe perm again? its working so...
            Case 12
                Caine.Play("Alert")
                Caine.Speak("" + My.Settings.Name + ", guess what! I can see people's search history!")
                Caine.Speak("Let me start with Jax!")
                Caine.Play("Search")
                Caine.Play("Surprised")
                Caine.Speak("\Pit=400\\Spd=194\OH MY GOODNESS!")
                Caine.Play("Idle3_1")
                Caine.Play("Uncertain")
                Caine.Speak("\Pit=123\\Spd=107\Ah, much better..")
            Case 13
                Caine.Play("DoMagic1")
                Caine.Speak("\Map=""Ala-kuh-blam!""=""Alakablam!""\Summon me my superstars!")
                Caine.Play("DoMagic2")
                Caine.Speak("...")
                Caine.Speak("...")
                Caine.Play("Uncertain")
                Caine.Speak("Darn it!")
                Caine.Play("Blink")
            Case 14
                Caine.Play("FastExplain")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=104\\Spd=130\doe \Pit=117\ray \Pit=131\me \Pit=139\fah \Pit=156\so \Pit=175\lah \Pit=196\tea \Pit=208\doe""=""do re mi fa so la ti do""\")
                Caine.Play("Blink")
                Caine.Speak("\Chr=""Normal""\What? I'm practicing my singing!")
            Case 15
                Caine.Think("Think")
                Caine.Speak("Well I \emp\hope you're not making any weird pictures of Pomni right now.")
                Caine.Play("Blink")
            Case 16
                Caine.Play("Alert")
                Caine.Speak("\Pit=400\" + My.Settings.Name + "? This is what I would sound like if I were only one quarter of my actual size!\rst\")
                Caine.Play("Blink")
            Case 17
                Caine.MoveTo(0, 480)
            Case 18
                Caine.MoveTo(640, 480)
            Case 19
                Caine.MoveTo(320, 240)
            Case 20
                HandlePictureBox4ClickEvent()
            Case 21
                HandlePictureBox5ClickEvent()
        End Select
    End Sub
    Private Sub HandlePictureBox4ClickEvent()
        Dim random2 As New Random()
        Dim randomNumber As Integer = random2.Next(1, 33)

        Select Case randomNumber
            Case 1
                Caine.Play("Acknowledge")
                Caine.Speak("A joke? Sure, why not?")
                Caine.Play("Explain")
                Caine.Speak("Did you hear about the human cannonball? \pau=2000\He got fired!")
                Caine.Play("Giggle")
                Caine.Speak("Get it, because he was blasted out of a cannon?")
            Case 2
                Caine.Play("Acknowledge")
                Caine.Speak("Okay, if you're sure.")
                Caine.Play("Explain")
                Caine.Speak("Why did the clown throw his clock out of the window? \pau=2000\He wanted to see time fly!")
                Caine.Play("Giggle")
                Caine.Play("Pleased")
                Caine.Speak("It took me a while to get that one, but when I did I could not stop laughing!")
            Case 3
                Caine.Play("Acknowledge")
                Caine.Speak("A joke? Sure, I've got a funny one.")
                Caine.Play("Explain")
                Caine.Speak("What happened when the magician got mad? \pau=2000\He pulled his \emp\ hare out!")
                Caine.Play("Giggle")
                Caine.Speak("First time I heard that, I nearly fell off my chair laughing!")
            Case 4
                Caine.Play("Acknowledge")
                Caine.Speak("If you insist.")
                Caine.Play("Explain")
                Caine.Speak("Why don't some fishes go to school? \pau=2000\Because they don't give a \emp\carp about it!")
                Caine.Play("Giggle")
                Caine.Speak("I learned that from the french!")
            Case 5
                Caine.Play("Alert")
                Caine.Speak(My.Settings.Name + "? I didn't know you liked my jokes so much.")
                Caine.Play("Explain")
                Caine.Speak("How do you raise a baby elephant? \pau=2000\With a fork lift!")
                Caine.Play("Congratulate2")
                Caine.Speak("Well, of \emp\ course I'm fork lift certified.")
            Case 6
                Caine.Play("Acknowledge")
                Caine.Speak("Alright, here we go.")
                Caine.Play("Explain")
                Caine.Speak("What do you and a lemon have in common? \pau=2000\You're both sour!")
                Caine.Play("Uncertain")
                Caine.Speak("Some of these jokes were written by Jax. Blame him.")
            Case 7
                Caine.Play("Acknowledge")
                Caine.Speak("Ok, " + My.Settings.Name + ", this one's sure to make you laugh!")
                Caine.Play("Explain")
                Caine.Speak("Where should a monkey go when he loses his tail? \pau=2000\To a retailer!")
                Caine.Play("Giggle")
                Caine.Play("Think")
                Caine.Speak("Come to think of it, does Son \Map=""Go-koo""=""Goku""\ count as one?")
            Case 8
                Caine.Play("Announce")
                Caine.Speak("You asked for it, you got it!")
                Caine.Play("Explain")
                Caine.Speak("Why is Merlin a wizard? \pau=2000\Because he sucks at everything else!")
                Caine.Play("Mad")
                Caine.Speak("Jax wrote that one. Send all complaints to him.")
            Case 9
                Caine.Play("Acknowledge")
                Caine.Speak("This one's for all of you un-abstracted humans out there!")
                Caine.Play("Explain")
                Caine.Speak("What does an abstraction feel like? \pau=2000\Darkness consuming you!")
                Caine.Play("Giggle")
                Caine.Speak("There's a problem I \emp\ know I can handle!")
            Case 10
                Caine.Play("Acknowledge")
                Caine.Speak("Ok, if you're sure.")
                Caine.Play("Explain")
                Caine.Speak("Why does Gangle wear a comedy mask? \pau=2000\To hide his miserable life!")
                Caine.Play("Uncertain")
                Caine.Speak("That was one of Jax's jokes. Sorry.")
            Case 11
                Caine.Play("Acknowledge")
                Caine.Speak("Ok, here goes.")
                Caine.Play("Explain")
                Caine.Speak("A clown opened the door for me the other day. \pau=2000\It was a lovely jester!")
                Caine.Play("Giggle")
                Caine.Speak("I'm talking about Pomni, our \emp\ current member.")
            Case 12
                Caine.Play("Pleased")
                Caine.Speak("I've been waiting to do this one, " + My.Settings.Name + "!")
                Caine.Play("Explain")
                Caine.Speak("Why are my performers always stressed? \pau=2000\Because their job is in tents!")
                Caine.Play("Restpose")
                Caine.Speak("There's plenty more where \emp\ that came from, " + My.Settings.Name + "!")
            Case 13
                Caine.Play("Surprised")
                Caine.Speak("I didn't think you liked my jokes \emp\ this much, " + My.Settings.Name + "!")
                Caine.Play("Explain")
                Caine.Speak("And what's the deal with keyboards? These aren't keys, I can't unlock \emp\ anything with this thing! I guess the \emp\ inventor of keyboards thought that they'd be the key to typing.")
                Caine.Play("Giggle")
                Caine.Speak("That was one of Max's jokes. Thank him for that!")
            Case 14
                Caine.Play("Acknowledge")
                Caine.Speak("Let's go!")
                Caine.Play("Explain")
                Caine.Speak("What do you call an escaped prisoner in the metaverse? \pau=2000\A Meta Runner!")
                Caine.Play("Giggle")
                Caine.Speak("Careful not to let him steal your bitcoins!")
            Case 15
                Caine.Play("Acknowledge")
                Caine.Speak("Okay, " + My.Settings.Name + ", I've got one.")
                Caine.Play("Explain")
                Caine.Speak("What do you call a group of small copters that deliver packages from the stores? \pau=2000\Worker Drones!")
                Caine.Play("Giggle")
                Caine.Speak("Get it, because these and the blue-eyed robots have the same name?")
            Case 16
                Caine.Play("Acknowledge")
                Caine.Speak("Anything for you, " + My.Settings.Name + "!")
                Caine.Play("Explain")
                Caine.Speak("Why couldn't the plush doll eat her dinner? \pau=2000\Because she's been stuffed for years!")
                Caine.Play("Pleased")
                Caine.Speak("I saved this joke for one of my human superstars!")
            Case 17
                Caine.Play("Acknowledge")
                Caine.Speak("This one's sure to make you laugh!")
                Caine.Play("Explain")
                Caine.Speak("What's the perfect detective for your computer? \pau=2000\Microsoft Agents!")
                Caine.Play("Acknowledge")
                Caine.Speak("Hey! I fall under that category!")
            Case 18
                Caine.Play("Acknowledge")
                Caine.Speak("A joke? Sure, I got a ton of them.")
                Caine.Play("Explain")
                Caine.Speak("What does a mix-and-match toy say to an adventure she doesn't feel like going on? \pau=2000\'Buzz off'!")
                Caine.Play("Giggle")
                Caine.Speak("That one's a classic, even when censored!")
            Case 19
                Caine.Play("Acknowledge")
                Caine.Speak("Sure thing, " + My.Settings.Name + ".")
                Caine.Play("Explain")
                Caine.Speak("What is Kinger's greatest fear? \pau=2000\The exit!")
                Caine.Play("Uncertain")
                Caine.Speak("That was another one of Jax's jokes. Sorry.")
            Case 20
                Caine.Play("Acknowledge")
                Caine.Speak("OK, I've got a good one for you.")
                Caine.Play("Explain")
                Caine.Speak("Knock knock! Who's there? Orange! Orange who? Orange you glad this is all a dream?")
                Caine.Play("Uncertain")
                Caine.Speak("That was once again a Jax joke, I'm sorry.")
            Case 21
                Caine.Play("Acknowledge")
                Caine.Speak("OK, here goes.")
                Caine.Play("Explain")
                Caine.Speak("What is the proper way to respond to a scary monster? \pau=2000\''Zoinks!''")
                Caine.Play("Giggle")
                Caine.Speak("That was one of Jax's jokes, and believe it or not, I actually kinda chuckled at that one!")
            Case 22
                Caine.Play("Acknowledge")
                Caine.Speak("A joke? Sure thing, " + My.Settings.Name + "!")
                Caine.Play("Explain")
                Caine.Speak("I saw the sun go down in the bright orange sky. \pau=2000\I considered it a Sunset Paradise!")
                Caine.Play("Restpose")
                Caine.Speak("So, you're saying Pomni sounds familiar?")
            Case 23
                Caine.Play("Acknowledge")
                Caine.Speak("Ok, " + My.Settings.Name + ", I've got one for you.")
                Caine.Play("Explain")
                Caine.Speak("How do you deal with an aligator? \pau=2000\Zap!")
                Caine.Play("Giggle")
                Caine.Speak("Hey, don't panic. It's not that NPC you're emotionally connected to.")
            Case 24
                Caine.Play("Acknowledge")
                Caine.Speak("A joke? Sure thing!")
                Caine.Play("Explain")
                Caine.Speak("Why couldn't the kid get into the pirate movie? \pau=2000\Because it was rated arr!")
                Caine.Play("Giggle")
                Caine.Speak("Get it, because it's about pirates?")
            Case 25
                Caine.Play("Acknowledge")
                Caine.Speak("A joke? Why not!")
                Caine.Play("Explain")
                Caine.Speak("What's the best way to troll a ragdoll? \pau=2000\Deliver centipedes!")
                Caine.Play("Uncertain")
                Caine.Speak("That was yet another Jax joke. I'm really sorry.")
            Case 26
                Caine.Play("Acknowledge")
                Caine.Speak("A joke? Okie-Dokie!")
                Caine.Play("Explain")
                Caine.Speak("What is the devil's greatest fear? \pau=2000\Angels!")
                Caine.Play("Uncertain")
                Caine.Speak("I tried my best on that one.")
            Case 27
                Caine.Play("Alert")
                Caine.Speak("" + My.Settings.Name + ", Stop! I got a very funny \emp\joke for you!")
                Caine.Play("Explain")
                Caine.Speak("What causes a massive demon hotel leak? \pau=2000\Nick the Marriage!")
                Caine.Play("Giggle")
                Caine.Speak("It's funny because it's true!")
            Case 28
                Caine.Play("Acknowledge")
                Caine.Speak("OK, here goes.")
                Caine.Play("Explain")
                Caine.Speak("Where should you go buy a propane tank? \pau=2000\at a Gaslight District!")
                Caine.Play("Giggle")
                Caine.Speak("Ah yes, the end times \emp\are near.")
            Case 29
                Caine.Play("Acknowledge")
                Caine.Speak("If you insist.")
                Caine.Play("Explain")
                Caine.Speak("What did the Spudsy's burger say to the other burger at the party? \pau=2000\'You're a bit saucy today, aren't you?'")
                Caine.Play("Giggle")
                Caine.Speak("...")
                Caine.Play("Sad")
                Caine.Speak("Why aren't you laughing?")
            Case 30
                Caine.Play("Announce")
                Caine.Speak("You asked for it, you got it!")
                Caine.Play("Explain")
                Caine.Speak("What do you call a snowman eating french fries at Spudsy's? \pau=2000\A \Map=""chilly-dog!""=""chilli-dog!""\")
                Caine.Play("Blink")
                Caine.Speak("You know " + My.Settings.Name + ", a good friend always laughs at your jokes, even if they're bad.")
            Case 31
                Caine.Play("Acknowledge")
                Caine.Speak("OK, I've got a good one for you.")
                Caine.Play("Explain")
                Caine.Speak("What is Orbsman's least favorite shape? \pau=2000\Blocks!")
                Caine.Play("Giggle")
                Caine.Speak("Get it, because he's all but spheres?")
            Case 32
                Caine.Play("Acknowledge")
                Caine.Speak("A joke? Why not!")
                Caine.Play("Explain")
                Caine.Speak("What's the best antivirus for your computer? \pau=2000\The knights of Guinevere!")
                Caine.Play("Giggle")
                Caine.Speak("...")
                Caine.Play("Sad")
                Caine.Speak("Why aren't you laughing?")
        End Select
    End Sub
    Private Sub HandlePictureBox5ClickEvent()
        Dim randomF As New Random()
        Dim randomFact As Integer = randomF.Next(1, 28)
        Dim randomE As New Random()
        Dim randomEnd As Integer = randomE.Next(1, 16)

        Caine.Play("Read")
        Select Case randomFact
            Case 1
                Caine.Speak("Whoa, this is crazy! Did you know that Windows 2000 was never meant for consumers? The closest thing was Windows ME, which was more like a buggy Windows 98.")
            Case 2
                Caine.Speak("Here's some knowledge for you. ")
                Caine.Speak("Did you know that the oldest versions of Windows were DOS based? It went on like this until NT for businesses, and XP for everyone else.")
            Case 3
                Caine.Speak("Did you know that the first digital computer was invented in 1946?")
                Caine.Speak("I wouldn't imagine they were able to do much back then.")
            Case 4
                Caine.Speak("Here's something that might interest you! Did you know that in the 1980s, the Commodore 64 was the best selling computer of all time?")
                Caine.Speak("It makes sense, because not only are the games good, but Dang do those soundtracks slap!")
            Case 5
                Caine.Speak("Here's a fun fact! Did you know that the computer mouse was named after its resemblance to the rodent of the same name? It's mostly in the wire, which looks like a tail.")
            Case 6
                Caine.Speak("Did you know that the first game console, the Brown Box, was made in 1967? It wasn't released publicly though. The first \emp\ publicly released game console, the Magnavox Odyssey, was released in 1972!")
                Caine.Speak("Five years! That's a long gap!")
            Case 7
                Caine.Speak("Here's an interesting one.")
                Caine.Speak("Did you know that this layout used to be a custom skin for BlobBUDDY?")
                Caine.Speak("Maybe I'm \emp\ related to that creature!")
            Case 8
                Caine.Speak("Did you know that the USB was invented in 1996? That's an entire year after the release of Windows 95!")
            Case 9
                Caine.Speak("It's learning time!")
                Caine.Speak("Did you know that the first polygonal computer animation was in 1972? It featured a hand, a heart valve, and even a face!")
                Caine.Speak("I even have said hand on my book!")
            Case 10
                Caine.Speak("Did you know that Windows 1.0 wasn't publicly released? The first publicly released version was Windows 1.01.")
            Case 11
                Caine.Speak("Well this is an interesting one, " + My.Settings.Name + "! Did you know that the invention of HDMI goes all the way back to 2002! Who knew that high definition existed for that long?")
            Case 12
                Caine.Speak("Alrighty " + My.Settings.Name + ", here’s a fact for you.")
                Caine.Speak("Did you know that the Fairchild Channel F was the first game console to truly utilize game cartridges?")
                Caine.Speak("\emp\Really makes you wonder if the Digital Circus runs through a floppy disk.")
            Case 13
                Caine.Speak("Do you like playing games, " + My.Settings.Name + "? If so, this fact is all about such! ")
                Caine.Speak("Did you know that the first arcade video game, Computer Space, was released in 1971?")
                Caine.Speak("That's a year before the first game console for consumers, the Magnavox Odyssey!")
            Case 14
                Caine.Speak("Did you know that Baldi's Basics made its debut in 1999?")
                Caine.Speak("No, actually it was released in 2018.")
            Case 15
                Caine.Speak("Did you know that the Binary Runtime Environment for Wireless, often shortened to Brew, started development in 1999? It was officially introduced 2 years later, in 2001!")
                Caine.Speak("Speaking of Brew, I could use one right now!")
            Case 16
                Caine.Speak("Did you know that Palm OS was introduced in 1996? That's \emp\ way before J2ME and Brew were introduced!")
            Case 17
                Caine.Speak("This one's sure to impress you as much as it impressed me!")
                Caine.Speak("Did you know that composite video was invented in 1954! Yes, you heard that right!")
                Caine.Speak("And this whole time, I thought it was invented in the 80's!")
            Case 18
                Caine.Speak("It’s learning time! Did you know that the Video Home System, more commonly known as VHS, was invented in 1976? It was the competitor to Betamax, and it ended up winning!")
                Caine.Speak("Go VHS!")
            Case 19
                Caine.Speak("Did you know that the Atari 2600 was the first super popular game console? It wasn't \emp\ always like that, though. It didn't really gain popularity until the release of Space Invaders for the system, which is considered by many to be the ‘killer app’.")
            Case 20
                Caine.Speak("It's time for an amazing fact!")
                Caine.Speak("Did you know that the DVD was invented in 1995? It didn't get an official release until a year later, though.")
            Case 21
                Caine.Speak("Here's a fact about Pomni.")
                Caine.Speak("Did you know that she originally has a different color scheme in the first trailer?")
                Caine.Speak("Ah, good times!")
            Case 22
                Caine.Speak("Do you like object shows, " + My.Settings.Name + "? If so, this fact is all about such!")
                Caine.Speak("Did you know that the furthest you can go in Battle For Dream Island Again 5b is 2763 kilometers? It's even the famous number in the Battle For Dream Island series!")
            Case 23
                Caine.Speak("It's \emp\fact time.")
                Caine.Speak("\Map=""Doom""=""DOOM""\ is a \emp\violent game. But did you know there's a family friendly version of it called 'Chex Quest'!")
                Caine.Speak("You don't kill the monsters, you send them back home!")
            Case 24
                Caine.Speak("Here's some knowledge for you. ")
                Caine.Speak("Did you know that there's a toy company called ''Playtime Co.''? It was on business from 1930 to 1995.")
                Caine.Speak("To be frank " + My.Settings.Name + ", you and I don't wanna know why they went out of business.")
            Case 25
                Caine.Speak("Whoa! This is crazy!")
                Caine.Speak("Apparently Glitch Productions has a \Map=""Blue Sky""=""Bluesky""\ account now! You can now follow them for future Digital Circus episodes!")
            Case 26
                Caine.Speak("Woah! Apparently you can download a virus-free version of BonziBUDDY! But back then, I was still suspicious of the gorilla, so I would often terminate anything related to him, including the virus-free one.")
                Caine.Speak("Thankfully after the conclusion of his vlogging series, I've decided to spare BonziBUDDY Rewritten!")
            Case 27
                Caine.Speak("Whoa! This is crazy!")
                Caine.Speak("Apparently there was an incident where Laser Boy received an SSD failure, losing the Source Code in the process.")
                Caine.Speak("Thankfully a shipper rebuilt the code and we can work on Circus Pals once more!")
        End Select
        Caine.Play("ReadContinued")
        Select Case randomEnd
            Case 1
                Caine.Speak("I wonder how many people in the world knew that?")
            Case 2
                Caine.Speak("Don't you just love these little tid bits?")
            Case 3
                Caine.Speak("I contain more knowledge than Blob, Max, or even Bonzi!")
            Case 4
                Caine.Speak("Don't worry, " + My.Settings.Name + ", there's plenty more where \emp\that came from!")
            Case 5
                Caine.Speak("Who knew.")
            Case 6
                Caine.Speak("Was that trivial? Or trivia!")
            Case 7
                Caine.Speak("One of Digital Circus's \emp\many mysteries revealed!")
            Case 8
                Caine.Speak("We just keep getting smarter every day, " + My.Settings.Name + "!")
            Case 9
                Caine.Speak("I'm just loaded with knowledge!")
            Case 10
                Caine.Speak("I just keep getting smarter every day!")
            Case 11
                Caine.Speak("Truth is, I'm a portable Wikipedia.")
            Case 12
                Caine.Speak("What will we learn next?")
            Case 13
                Caine.Speak("Just thought you'd like to know.")
            Case 14
                Caine.Speak("You're probably wondering how I know so much! I'm the ringmaster, duh.")
            Case 15
                Caine.Speak("But I bet you already knew that, " + My.Settings.Name + ".")
        End Select
        Caine.Play("ReadReturn")
    End Sub

    Private Sub JungleUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JungleUpdate.Click
        Caine.Speak("Your current version is 2.0! Let's launch my web page to see if there's an update...")
        Dim webAddress As String = "http://circuspals.w10.site/update.html?version=2.0.0"
        Process.Start(webAddress)
    End Sub

    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Caine.Speak("Having trouble? Let me help you.")
        Caine.Play("Explain")
        Caine.Speak("From the Utility Panel, I can play any animation you want. Simply select any animation from the drop down menu and click on the play button.")
        Caine.Play("Suggest")
        Caine.Speak("And remember " + My.Settings.Name + ", if the animation is looping, you can simply click the stop button to stop it!")
        Caine.Play("Blink")
    End Sub

    Private Sub Button8_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Caine.Speak("Having trouble? Let me help you.")
        Caine.Play("Explain")
        Caine.Speak("From the Utility Panel, I can say anything you like. Simply enter what you would like me to say and click on the Think, Whisper, Monotone, or Speak buttons.")
        Caine.Play("Decline")
        Caine.Speak("Oh and remember " + My.Settings.Name + ", keep it clean, cause the filter isn't working at the moment.")
        Caine.Play("Acknowledge")
    End Sub

    Private Sub WackyWatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WackyWatch.Click
        Caine.Speak("The Wacky Watch tells you about the current events of the Digital Circus! Let's launch its web page to see if there's an update.")
        Dim webAddress As String = "https://thewackywatch.com"
        Process.Start(webAddress)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Dim processes() As Process = Process.GetProcessesByName("MashPlay")

        For Each process As Process In processes
            process.Kill()
        Next
        If Caine.Visible Then
            allowClose = True
            Timer3.Start()
            Caine.StopAll()

            Dim random As New Random()
            Dim randomNumber32 As Integer = random.Next(1, 11)

            Select Case randomNumber32
                Case 1
                    Caine.Play("Wave")
                    Caine.Speak("It hurts me to say goodbye, " + My.Settings.Name + ".")
                    Caine.Hide()
                Case 2
                    Caine.Play("Wave")
                    Caine.Speak("Until next time my friend!")
                    Caine.Hide()
                Case 3
                    Caine.Play("Wave")
                    Caine.Speak("Until next time, " + My.Settings.Name + ".")
                    Caine.Hide()
                Case 4
                    Caine.Play("Acknowledge")
                    Caine.Speak("Well, I guess I am done for today. Bye for now.")
                    Caine.Hide()
                Case 5
                    Caine.Play("Acknowledge")
                    Caine.Speak("It looks like my work here is done. See you later.")
                    Caine.Hide()
                Case 6
                    Caine.Play("wave")
                    Caine.Speak("I hope to see you again soon, " + My.Settings.Name + ".")
                    Caine.Hide()
                Case 7
                    Caine.Play("Surprised")
                    Caine.Speak("Oh no! Not the exit!")
                    Caine.Hide()
                Case 8
                    Caine.Play("RestPose")
                    Caine.Speak("Oh, guess I'll instantly disappear")
                    Caine.Play("Silly")
                Case 9
                    Caine.Play("Explain")
                    Caine.Speak("Well, I'm going to go drink water! It's been a \emp\while since I've done that.")
                    Caine.Hide()
                Case 10
                    Caine.Play("Restpose")
                    Caine.Speak("\emp\I am such a good \Pit=400\\Spd=250\\Map=""bah-ah-ah-ah-ah-ah-ah-ah-ah-ah-\rst\sahtmtornmhsrp;h;xhgzodpsosoaaah,""=""bo-""\")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Play("Idle3_1")
                    Caine.Think("...")
                    Caine.Think("...")
                    Caine.Think("...")
                    Caine.Hide()
            End Select
        Else
            allowClose = True
            Me.Close()
        End If
    End Sub

    Private Sub TimerOfDOOM_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerOfDOOM.Tick
        Dim webAddress As String = "http://www.youtube.com/watch?v=YRl0o0e2OYM"
        Process.Start(webAddress)
        TimerOfDOOM.Stop()
    End Sub

    Private Sub SongButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SongButton.Click
        BlobSingsForm.Show()
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        If RadioButton1.Checked = True Then
            Caine.Play("Pleased")
            Caine.Speak("Ah yes! The old ways of sharing particular files.")
            Caine.Play("RestPose")
            Dim webAddress As String = "mailto:info@example.com?&subject=&cc=&bcc=&body=http://circuspals.w10.site"
            Process.Start(webAddress)
        End If
        If RadioButton2.Checked = True Then
            Caine.Play("Congratulate2")
            Caine.Speak("Upvotes, please!")
            Caine.Play("RestPose")
            Dim webAddress As String = "http://old.reddit.com/submit?url=https%3A%2F%2Fcircuspals.w10.site%2F&title=Check%20out%20this%20awesome%20program!%20"
            Process.Start(webAddress)
        End If
        If RadioButton3.Checked = True Then
            Caine.Play("Congratulate2")
            Caine.Speak("Alright, lets share it!")
            Caine.Play("RestPose")
            Dim webAddress As String = "https://bsky.app/intent/compose?text=Check%20out%20this%20awesome%20program!%20%20https%3A//circuspals.w10.site%20via%20Laser%20Boy%20Studios!"
            Process.Start(webAddress)
        End If
        If RadioButton4.Checked = True Then
            Caine.Play("Congratulate2")
            Caine.Speak("What's App, dog!")
            Caine.Play("RestPose")
            Dim webAddress As String = "https://api.whatsapp.com/send?text=Check out this awesome program%3A https%3A%2F%2Fcircuspals.w10.site%2Fdownload.html"
            Process.Start(webAddress)
        End If
    End Sub

    Private Sub Button10_Hover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.MouseHover
        Button10.BackColor = Color.White
    End Sub
    Private Sub Button1_Hover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.MouseHover
        Button1.BackColor = Color.White
    End Sub
    Private Sub Button20_Hover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.MouseHover
        Button20.BackColor = Color.White
    End Sub
    Private Sub Button16_Hover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.MouseHover
        Button16.BackColor = Color.White
    End Sub
    Private Sub Button6_Hover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.MouseHover
        Button6.BackColor = Color.White
    End Sub
    Private Sub Button5_Hover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.MouseHover
        Button5.BackColor = Color.White
    End Sub

    Private Sub Button10_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.MouseLeave
        Button10.BackColor = Color.DodgerBlue
    End Sub
    Private Sub Button1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.MouseLeave
        Button1.BackColor = Color.DodgerBlue
    End Sub
    Private Sub Button20_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.MouseLeave
        Button20.BackColor = Color.DodgerBlue
    End Sub
    Private Sub Button16_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.MouseLeave
        Button16.BackColor = Color.DodgerBlue
    End Sub
    Private Sub Button6_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.MouseLeave
        Button6.BackColor = Color.DodgerBlue
    End Sub
    Private Sub Button5_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.MouseLeave
        Button5.BackColor = Color.DodgerBlue
    End Sub
End Class

