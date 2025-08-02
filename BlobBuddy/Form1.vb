Option Explicit On

Imports System
Imports AgentObjects
Imports AxAgentObjects
Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Media
Imports System.Diagnostics
Imports AgentServerObjects
Imports System.Management
Imports Microsoft.VisualBasic.CompilerServices

Public Class Form1
    Dim EventXML = Application.UserAppDataPath + "\events.xml"
    Private WithEvents processCheckTimer As Timer
    Private XmlCtrl As New XmlDocument
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
        EventTimer.Start()
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
        UtilPanel2.Hide()
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
        UtilPanel2.Hide()
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
        UtilPanel2.Hide()
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
        Caine.Speak("Your current version is 2.2.1! Let's launch my web page to see if there's an update...")
        Dim webAddress As String = "http://circuspals.w10.site/update.html?version=2.2.1"
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
            Caine.Speak("Be careful! This site contains a \emp\lot of Content Farms!")
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
            Caine.Speak("Be careful! There is some crazy stuff on this website!")
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
        If URLTextBox.Text = "http://kinitopet.com" OrElse URLTextBox.Text = "https://kinitopet.com" Then
            Caine.Speak("A complete ripoff, but everybody wants one.")
        End If
        If URLTextBox.Text = "http://myspace.com" OrElse URLTextBox.Text = "https://myspace.com" Then
            Caine.Play("Alert")
            Caine.Speak("You gotta be old in order to know about this one!")
            Caine.Play("RestPose")
        End If
        If URLTextBox.Text = "http://meta.com/quest" OrElse URLTextBox.Text = "https://meta.com/quest" Then
            Caine.Play("Alert")
            Caine.Speak("Great! Now you can come and join the crew in our awesome adventures and games!")
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
                    Caine.Play("Sad")
                    Caine.Speak("Aw shoot, I couldn't get rid of the malware.")
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
                    Caine.Play("Sad")
                    Caine.Speak("Aw shoot, I couldn't get rid of the malware.")
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
                    Caine.Play("Sad")
                    Caine.Speak("Aw shoot, I couldn't get rid of the malware.")
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
                    Caine.Play("Sad")
                    Caine.Speak("Aw shoot, I couldn't get rid of the malware.")
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
                    Caine.Play("Sad")
                    Caine.Speak("Aw shoot, I couldn't get rid of the malware.")
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
                    Caine.Play("Sad")
                    Caine.Speak("Aw shoot, I couldn't get rid of the malware.")
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
                    Caine.Play("Sad")
                    Caine.Speak("Aw shoot, I couldn't get rid of the malware.")
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
                    Caine.Play("Sad")
                    Caine.Speak("Aw shoot, I couldn't get rid of the malware.")
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
                    Caine.Play("Sad")
                    Caine.Speak("Aw shoot, I couldn't get rid of the malware.")
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
                    Caine.Play("Sad")
                    Caine.Speak("Aw shoot, I couldn't get rid of the malware.")
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
        Dim rnd As New Random()
        Select Case rnd.Next(1, 23)
            Case 1
                Caine.Play("Explain")
                Caine.Speak(My.Settings.Name + "! Where did the time go?")
                Caine.Play("Pleased")
                Caine.Speak("Can't you just feel us getting closer with every new day!")
                Caine.Play("Blink")
                Exit Select
            Case 2
                Caine.Play("Idle2_1")
                Caine.Speak("Don't mind me, I'm just playing along with Bubble.")
                Caine.Play("Restpose")
                Exit Select
            Case 3
                Caine.Play("Confused")
                Caine.Speak("Hey! Where did you go?")
                Caine.Play("Restpose")
                Exit Select
            Case 4
                Caine.Play("Pleased")
                Caine.Speak(My.Settings.Name + "! I've noticed you've been looking \emp\sharp as a tack these days!")
                Caine.Play("Blink")
                Exit Select
            Case 5
                Caine.Play("Blink")
                Caine.Speak("Hmm, I should check up on my superstars.")
                Exit Select
            Case 6
                Caine.Play("Pleased")
                Caine.Speak("Ah! What a nice day to do nothing!")
                Caine.Play("Blink")
                Exit Select
            Case 7
                Caine.Play("Explain")
                Caine.Speak("Why not listen to some music? Or watch some Mash files? They're \emp\ sure to entertain you!")
                Caine.Play("Blink")
                Exit Select
            Case 8
                Caine.Play("Think")
                Caine.Speak("Do you ever wonder if Joel would eventually feature me?")
                Caine.Play("Blink")
                Exit Select
            Case 9
                Caine.Play("GestureDown")
                Caine.Speak("Hey, what are you doing?")
                Caine.Play("Blink")
                Exit Select
            Case 10
                Caine.Play("Pleased")
                Caine.Speak("You're so fun to be around, " + My.Settings.Name + "!")
                Caine.Play("Blink")
                Exit Select
            Case 11
                Caine.Play("Wave")
                Caine.Speak("Hey " + My.Settings.Name + ", you're looking quite nice today!")
                Caine.Play("Blink")
                Exit Select
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
                Exit Select
            Case 13
                Caine.Play("DoMagic1")
                Caine.Speak("\Map=""Ala-kuh-blam!""=""Alakablam!""\Summon me my superstars!")
                Caine.Play("DoMagic2")
                Caine.Speak("...")
                Caine.Speak("...")
                Caine.Play("Uncertain")
                Caine.Speak("Darn it!")
                Caine.Play("Blink")
                Exit Select
            Case 14
                Caine.Play("FastExplain")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=104\\Spd=130\doe \Pit=117\ray \Pit=131\me \Pit=139\fah \Pit=156\so \Pit=175\lah \Pit=196\tea \Pit=208\doe""=""do re mi fa so la ti do""\")
                Caine.Play("Blink")
                Caine.Speak("\Chr=""Normal""\What? I'm practicing my singing!")
                Exit Select
            Case 15
                Caine.Think("Think")
                Caine.Speak("Well I \emp\hope you're not making any weird pictures of Pomni right now.")
                Caine.Play("Blink")
                Exit Select
            Case 16
                Caine.Play("Alert")
                Caine.Speak("\Pit=400\" + My.Settings.Name + "? This is what I would sound like if I were only one quarter of my actual size!\rst\")
                Caine.Play("Blink")
                Exit Select
            Case 17
                Caine.MoveTo(0, 480)
                Exit Select
            Case 18
                Caine.MoveTo(640, 480)
                Exit Select
            Case 19
                Caine.MoveTo(320, 240)
                Exit Select
            Case 20
                HandlePictureBox4ClickEvent()
                Exit Select
            Case 21
                HandlePictureBox5ClickEvent()
                Exit Select
            Case 22
                SingIntro()
                SingMain(rnd.Next(1, 26))
                Exit Select
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
                Caine.Speak("Alrighty " + My.Settings.Name + ", heres a fact for you.")
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
                Caine.Speak("Its learning time! Did you know that the Video Home System, more commonly known as VHS, was invented in 1976? It was the competitor to Betamax, and it ended up winning!")
                Caine.Speak("Go VHS!")
            Case 19
                Caine.Speak("Did you know that the Atari 2600 was the first super popular game console? It wasn't \emp\ always like that, though. It didn't really gain popularity until the release of Space Invaders for the system, which is considered by many to be the killer app.")
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
        Caine.Speak("Your current version is 2.2.1! Let's launch my web page to see if there's an update...")
        Dim webAddress As String = "http://circuspals.w10.site/update.html?version=2.2.1"
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

    Private Sub SingIntro()
        Dim rnd = New Random()

        Select Case rnd.Next(1, 4)
            Case 1
                Caine.Play("Explain")
                Caine.Speak("Here's one Queenie used to sing to Kinger. I hope you like it.")
                Caine.Play("FastExplain")
                Caine.Balloon.Style = &H31C000D
                Exit Select
            Case 2
                Caine.Play("Explain")
                Caine.Speak("OK, here goes.")
                Caine.Play("FastExplain")
                Caine.Balloon.Style = &H31C000D
                Exit Select
            Case 3
                Caine.Play("FastExplain")
                Caine.Balloon.Style = &H31C000D
                Exit Select
        End Select
    End Sub

    Public Sub SingMain(ByVal SongId As Integer)
        Select Case SongId
            Case 1
                Caine.Speak("\chr=""Monotone""\\spd=60\\pit=120\Dai \pit=100\zee, \pit=80\Dai \pit=60\zee")
                Caine.Speak("\chr=""Monotone""\\spd=120\\pit=68\Give \pit=74\me \pit=80\your \pit=68\an, \pit=80\ser \pit=60\true.")
                Caine.Speak("\chr=""Monotone""\\spd=50\\pit=92\I'm \pit=120\half\pit=100\cray \pit=80\zee,")
                Caine.Speak("\chr=""Monotone""\\spd=115\\pit=68\All \pit=74\for \pit=80\the \pit=80\love \pit=100\of \pit=92\you.")
                Caine.Speak("\chr=""Monotone""\\spd=60\\spd=80\\pit=100\It \pit=112\won't \pit=110\be \pit=92\a \pit=120\sty-\pit=100\lish \pit=92\mare\pit=80\rege.")
                Caine.Speak("\chr=""Monotone""\\spd=100\\pit=92\I \pit=100\can't \pit=80\a \pit=68\ford, \pit=80\a \pit=68\care- \pit=60\ridge.")
                Caine.Speak("\chr=""Monotone""\\spd=100\\pit=60\But \pit=80\you'll \pit=100\look \pit=92\sweet,")
                Caine.Speak("\chr=""Monotone""\\spd=90\\pit=60\Up \pit=80\on \pit=100\the \pit=92\seat.")
                Caine.Speak("\chr=""Monotone""\\spd=100\\pit=100\Of \pit=112\a \pit=120\by \pit=100\sic \pit=80\cull \pit=92\built, \pit=60\for \pit=80\two.")
                Exit Select
            Case 2
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=110\\Spd=120\I've got \Pit=104\a \Pit=110\lit \Pit=104\tle \Pit=110\bai\Pit=104\bee \Pit=110\but \Pit=131\she's \Pit=147\out \Pit=110\of sight.\Pit=87\I talk to her a\Pit=82\cross \Pit=87\the \Pit=82\tel \Pit=87\leh \Pit=117\phone. \Pit=110\I've \Pit=117\nev \Pit=110\er \Pit=117\seen \Pit=110\my \Pit=117\hon \Pit=110\nee \Pit=117\but \Pit=131\she's\Pit=165\mine \Pit=117\all right,\Pit=110\so \Pit=98\take \Pit=92\my \Pit=98\tip \Pit=87\and \Pit=98\leave \Pit=87\this \Pit=98\gal \Pit=104\a \Pit=110\lone.""=""I've got a little baby but she's out of sight. I talk to her across the telephone. I've never seen my honey but she's mine all right, so take my tip and leave this gal alone.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=110\\Spd=120\Ev \Pit=104\'ree\Pit=110\sin \Pit=98\gle \Pit=110\mor \Pit=98\ning \Pit=110\you \Pit=131\will \Pit=147\hear \Pit=110\me\Pit=110\yell,\Pit=110\Hey Central \Pit=104\fix \Pit=110\me \Pit=73\up \Pit=147\along the line. \Pit=147\He connects me \Pit=139\with \Pit=147\my \Pit=165\honey\Pit=147\then I \Pit=131\ring \Pit=110\the \Pit=117\bell,\Pit=110\and \Pit=98\this \Pit=110\is \Pit=98\what \Pit=110\I \Pit=98\say \Pit=110\to \Pit=98\bai \Pit=147\bee \Pit=131\mine.""=""Every single morning you will hear me yell, ""Hey Central fix me up along the line."" he connects me with my honey, then I ring the bell and this is what I say to baby mine.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=120\Hel \Pit=147\lo \Pit=131\my \Pit=147\bai \Pit=131\bee,\Pit=110\Hel \Pit=117\lo \Pit=110\my \Pit=117\hon \Pit=110\nee, \Pit=98\Hel \Pit=110\lo \Pit=98\my \Pit=110\rag \Pit=98\time \Pit=73\gahl.""=""""Hello my baby, hello my honey, hello my ragtime gal.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=110\\Spd=120\Send \Pit=98\me \Pit=92\a \Pit=110\kiss \Pit=98\by \Pit=65\wire,\Pit=87\bai \Pit=110\bee \Pit=131\my \Pit=165\heart's \Pit=147\on \Pit=131\fire.""=""Send me a kiss by wire, baby my heart's on fire.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=120\If \Pit=147\you \Pit=131\re \Pit=147\fuse \Pit=131\me\Pit=110\hon \Pit=117\nee \Pit=110\you'll \Pit=117\lose \Pit=110\me \Pit=98\then \Pit=110\you'll \Pit=98\be \Pit=110\left \Pit=98\a \Pit=73\lone.""=""If you refuse me, honey you'll lose me, then you'll be left alone.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=73\\Spd=120\Oh \Pit=82\bai \Pit=87\bee \Pit=110\tele\Pit=98\phone \Pit=92\and \Pit=110\tell \Pit=98\me \Pit=110\I'm\Pit=98\your \Pit=87\own.\Pit=131\Hel \Pit=147\lo \Pit=131\Hel \Pit=147\lo \Pit=131\Hel \Pit=147\lo \Pit=131\there!""=""Oh baby telephone and tell me I'm your own. Hello Hello Hello there!""""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=110\\Spd=120\This mor\Pit=104\ning \Pit=110\through \Pit=104\the  \Pit=110\phone \Pit=104\she \Pit=110\said \Pit=131\her \Pit=147\name \Pit=110\was Bess,\Pit=87\and now I kind of\Pit=82\know \Pit=87\where \Pit=82\I \Pit=87\am \Pit=117\at. \Pit=110\I'm \Pit=117\sat \Pit=110\is \Pit=117\fied be\Pit=110\cause \Pit=117\I \Pit=110\got \Pit=117\my \Pit=131\bai \Pit=165\bees \Pit=117\address \Pit=110\here \Pit=98\paste \Pit=92\ed \Pit=98\in \Pit=87\the \Pit=98\line \Pit=87\ing \Pit=98\of \Pit=104\my \Pit=110\hat.""=""This morning through the phone she said her name was Bess, and now I kind od know where I am at. I'm satisfied because I got my babies address here pasted in the lining of my hat.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=110\\Spd=120\I \Pit=104\am \Pit=110\might \Pit=98\ee \Pit=110\scared be\Pit=98\cause \Pit=110\if \Pit=131\the \Pit=147\wires \Pit=110\get\Pit=110\crossed,\Pit=110\'twill sepa \Pit=104\rate \Pit=110\me \Pit=73\from \Pit=147\my baby mine, \Pit=147\then some other \Pit=139\man \Pit=147\will \Pit=165\win her\Pit=147\and my \Pit=131\game \Pit=110\is \Pit=117\lost,\Pit=110\and \Pit=98\so \Pit=110\each \Pit=98\day \Pit=110\I \Pit=98\shout \Pit=110\a \Pit=98\long \Pit=147\the \Pit=131\line.""=""I'm mighty scared because if the wires get crossed, 'twill seperate me from my baby mine, then some other man will win her and my game is lost, so each day I shout along the line.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=120\Hel \Pit=147\lo \Pit=131\my \Pit=147\bai \Pit=131\bee,\Pit=110\Hel \Pit=117\lo \Pit=110\my \Pit=117\hon \Pit=110\nee, \Pit=98\Hel \Pit=110\lo \Pit=98\my \Pit=110\rag \Pit=98\time \Pit=73\gahl.""=""""Hello my baby, hello my honey, hello my ragtime gal.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=110\\Spd=120\Send \Pit=98\me \Pit=92\a \Pit=110\kiss \Pit=98\by \Pit=65\wire,\Pit=87\bai \Pit=110\bee \Pit=131\my \Pit=165\heart's \Pit=147\on \Pit=131\fire.""=""Send me a kiss by wire, baby my heart's on fire.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=120\If \Pit=147\you \Pit=131\re \Pit=147\fuse \Pit=131\me\Pit=110\hon \Pit=117\nee \Pit=110\you'll \Pit=117\lose \Pit=110\me \Pit=98\then \Pit=110\you'll \Pit=98\be \Pit=110\left \Pit=98\a \Pit=73\lone.""=""If you refuse me, honey you'll lose me, then you'll be left alone.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=73\\Spd=120\Oh \Pit=82\bai \Pit=87\bee \Pit=110\tele\Pit=98\phone \Pit=92\and \Pit=110\tell \Pit=98\me \Pit=110\I'm\Pit=98\your \Pit=87\own.""=""Oh baby telephone and tell me I'm your own.""""\")
                Exit Select
            Case 3
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=132\You \Pit=247\ought \Pit=220\a see \Pit=185\my \Pit=165\Cin \Pit=147\dee, she \Pit=247\lives \Pit=220\a \Pit=185\way \Pit=220\down south.\Pit=165\Now \Pit=247\she's \Pit=220\so sweet \Pit=185\the hon \Pit=147\nee bees, \Pit=165\they \Pit=185\swarm \Pit=165\a \Pit=147\round \Pit=123\her \Pit=147\mouth.""=""You oughta see my Cindy, she lives away down south. Now she's so sweet the honey bees, they swarm around her mouth.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=132\The \Pit=247\first \Pit=220\I seen \Pit=185\my \Pit=165\Cin \Pit=147\dee, she \Pit=247\stand \Pit=220\n' \Pit=185\in \Pit=220\the door.\Pit=165\Her \Pit=247\shoes \Pit=220\and stock \Pit=185\ings in \Pit=147\her hand, \Pit=165\her \Pit=185\feet \Pit=165\spread' \Pit=147\round \Pit=123\the \Pit=147\floor.""=""The first I've seen my Cindy, she standin' in the door. Her shoes and stockings in her hand, her feet spread 'round the floor.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=185\\Spd=132\Get a \Pit=147\long \Pit=123\home, \Pit=147\Cin dee Cin dee, \Pit=185\get a \Pit=147\long \Pit=110\home, \Pit=147\Cin dee Cin dee,\Pit=185\get a \Pit=147\long \Pit=123\home, \Pit=147\Cin dee Cin dee,\Pit=165\I'll \Pit=185\marry \Pit=165\you some \Pit=147\day.""=""Get along home, Cindy, Cindy, get along home, Cindy, Cindy, get along home, Cindy, Cindy, I'll marry you some day.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=132\I \Pit=247\wish \Pit=220\I was \Pit=185\an \Pit=165\app \Pit=147\pel, a \Pit=247\hang \Pit=220\in' \Pit=185\on \Pit=220\a tree.\Pit=165\And \Pit=247\every \Pit=220\time that \Pit=185\Cin \Pit=147\dee passed, \Pit=165\She'd \Pit=185\take \Pit=165\a \Pit=147\bite \Pit=123\of \Pit=147\me.""=""I wish I was an apple, a hangin' on a tree. And every time that Cindy passed, she'd take a bite of me.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=132\If \Pit=247\I \Pit=220\were\Pit=185\made \Pit=165\of su\Pit=147\gar a \Pit=247\stand \Pit=220\in' \Pit=185\in \Pit=220\the town.\Pit=165\Then \Pit=247\every \Pit=220\time my \Pit=185\Cin \Pit=147\dee passed, \Pit=165\I'd \Pit=185\shake \Pit=165\some \Pit=147\su \Pit=123\gar \Pit=147\down.""=""If I were made of sugar a standin' in the town. Then every time my Cindy passed, I'd shake some sugar down.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=185\\Spd=132\Get a \Pit=147\long \Pit=123\home, \Pit=147\Cin dee Cin dee, \Pit=185\get a \Pit=147\long \Pit=110\home, \Pit=147\Cin dee Cin dee,\Pit=185\get a \Pit=147\long \Pit=123\home, \Pit=147\Cin dee Cin dee,\Pit=165\I'll \Pit=185\marry \Pit=165\you some \Pit=147\day.""=""Get along home, Cindy, Cindy, get along home, Cindy, Cindy, get along home, Cindy, Cindy, I'll marry you some day.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=132\My \Pit=247\Cin \Pit=220\dee\Pit=185\got \Pit=165\rel\Pit=147\igion, she\Pit=247\had \Pit=220\it \Pit=185\once \Pit=220\ before.\Pit=165\But \Pit=247\when \Pit=220\she heard \Pit=185\my \Pit=147\old banjo, \Pit=165\she \Pit=185\leaped \Pit=165\up \Pit=147\on \Pit=123\the \Pit=147\floor.""=""My Cindy got religion, she had it once before. But when she heard my old banjo, she leaped up on the floor.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=132\She \Pit=247\kissed \Pit=220\me\Pit=185\and \Pit=165\she\Pit=147\hugged me, she\Pit=247\called \Pit=220\me \Pit=185\sugar \Pit=220\plum. \Pit=165\She \Pit=247\hugged \Pit=220\me so \Pit=185\tight I \Pit=147\hardly breathed, \Pit=165\I \Pit=185\thought \Pit=165\my \Pit=147\time \Pit=123\had \Pit=147\come.""=""She kissed me and she hugged me, she called me sugar plum. She hugged so tight I hardly breathed, I thought my time had come.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=185\\Spd=132\Get a \Pit=147\long \Pit=123\home, \Pit=147\Cin dee Cin dee, \Pit=185\get a \Pit=147\long \Pit=110\home, \Pit=147\Cin dee Cin dee,\Pit=185\get a \Pit=147\long \Pit=123\home, \Pit=147\Cin dee Cin dee,\Pit=165\I'll \Pit=185\marry \Pit=165\you some \Pit=147\day.""=""Get along home, Cindy, Cindy, get along home, Cindy, Cindy, get along home, Cindy, Cindy, I'll marry you some day.""\")
                Exit Select
            Case 4
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=73\\Spd=90\Young \Pit=98\john nie steele \Pit=92\has \Pit=82\an \Pit=92\olds mo beel, \Pit=73\he \Pit=131\loves a dear \Pit=123\lit \Pit=117\tle \Pit=123\girl.""=""Young Johnny Steele has an oldsmobile, he loves a dear little girl.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=73\\Spd=90\She \Pit=98\is the queen \Pit=92\of \Pit=82\his \Pit=92\gas machine,\Pit=73\she \Pit=131\has his heart \Pit=123\in \Pit=110\a \Pit=98\whirl.""=""She is the queen of his gas machine, she has his heart in a whirl.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=123\\Spd=90\Now, \Pit=110\when they go \Pit=123\for \Pit=139\a \Pit=147\spin, \Pit=110\you know,\Pit=123\she \Pit=110\tries \Pit=82\to learn \Pit=123\the \Pit=110\augh \Pit=92\toe\Pit=92\so \Pit=123\he \Pit=110\lets her steer \Pit=123\while \Pit=139\he \Pit=165\gets \Pit=147\her \Pit=139\ear\Pit=123\and \Pit=110\whis \Pit=104\pers \Pit=98\soft \Pit=82\and \Pit=73\low,""=""Now, when they go for a spin, you know, she tries to learn the auto so he lets her steel while he gets her ear and whispers soft and low.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=90\Come \Pit=131\a \Pit=123\way \Pit=73\with \Pit=123\me \Pit=73\Lu \Pit=123\ceel\Pit=131\in \Pit=123\my \Pit=110\mehr \Pit=82\ree \Pit=110\Olds \Pit=82\mo \Pit=110\beel.""=""""Come away with me Lucile in my merry Oldsmobile.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=110\\Spd=90\Down \Pit=98\the \Pit=92\road \Pit=147\of life we'll fly, \Pit=73\augh \Pit=82\toe \Pit=87\mo \Pit=98\bubb \Pit=147\ling you and I.""=""Down the road of life we'll fly, auto-mo-bubbling you and I.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=90\To \Pit=131\the \Pit=123\church \Pit=73\we'll \Pit=123\swift \Pit=73\lee \Pit=123\steer,\Pit=131\then \Pit=123\our \Pit=110\wedd \Pit=82\ing \Pit=110\bells \Pit=82\will \Pit=110\peal.""=""To the church we'll swiftly steer, then our wedding bells will peal.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=110\\Spd=90\You \Pit=98\can \Pit=87\go \Pit=147\as far \Pit=139\as \Pit=147\you \Pit=165\like \Pit=147\with \Pit=123\me\Pit=110\in \Pit=98\my \Pit=110\meh. \Pit=104\ree \Pit=110\Olds. \Pit=123\mo \Pit=98\beel.""=""You can go as far as you like with me in my merry Oldsmobile.""""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=73\\Spd=90\They \Pit=98\love to spark \Pit=92\in \Pit=82\the \Pit=92\dark old park \Pit=73\as \Pit=131\they go fly \Pit=123\ing \Pit=117\a \Pit=123\long.""=""They love to spark in the dark old park, as they go flying along.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=73\\Spd=90\She \Pit=98\says she knows \Pit=92\why \Pit=82\the \Pit=92\motor goes,\Pit=73\the \Pit=131\sparklers aw\Pit=123\ful \Pit=110\lee \Pit=98\strong.""=""She says she knows why the motor goes, the sparklers awfully strong.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=123\\Spd=90\Each \Pit=110\day, they spoon \Pit=123\to \Pit=139\the \Pit=147\en \Pit=110\gehns tune,\Pit=123\their \Pit=110\hon \Pit=82\nymoon \Pit=123\will \Pit=110\hap \Pit=92\pen\Pit=92\soon. \Pit=123\He'll \Pit=110\win lucile \Pit=123\with \Pit=139\his \Pit=165\Olds \Pit=147\mo \Pit=139\beel\Pit=123\and \Pit=110\then \Pit=104\they'll \Pit=98\fond \Pit=82\lee \Pit=73\croon.""=""Each day they spoon to the engine's tune, their honeymoon will happen soon. He'll win Lucile with his merry Oldsmobile""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=90\Come \Pit=131\a \Pit=123\way \Pit=73\with \Pit=123\me \Pit=73\Lu \Pit=123\ceel\Pit=131\in \Pit=123\my \Pit=110\mehr \Pit=82\ree \Pit=110\Olds \Pit=82\mo \Pit=110\beel.""=""""Come away with me Lucile in my merry Oldsmobile.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=110\\Spd=90\Down \Pit=98\the \Pit=92\road \Pit=147\of life we'll fly, \Pit=73\augh \Pit=82\toe \Pit=87\mo \Pit=98\bubb \Pit=147\ling you and I.""=""Down the road of life we'll fly, auto-mo-bubbling you and I.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=90\To \Pit=131\the \Pit=123\church \Pit=73\we'll \Pit=123\swift \Pit=73\lee \Pit=123\steer,\Pit=131\then \Pit=123\our \Pit=110\wedd \Pit=82\ing \Pit=110\bells \Pit=82\will \Pit=110\peal.""=""To the church we'll swiftly steer, then our wedding bells will peal.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=110\\Spd=90\You \Pit=98\can \Pit=87\go \Pit=147\as far \Pit=139\as \Pit=147\you \Pit=165\like \Pit=147\with \Pit=123\me\Pit=110\in \Pit=98\my \Pit=110\meh. \Pit=104\ree \Pit=110\Olds. \Pit=123\mo \Pit=98\beel.""=""You can go as far as you like with me in my merry Oldsmobile.""""\")
                Exit Select
            Case 5
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=120\Oh \Pit=220\my \Pit=247\golden slip \Pit=220\pers \Pit=196\they \Pit=220\are \Pit=247\laid away \Pit=196\I \Pit=220\was \Pit=247\saving them \Pit=220\un \Pit=247\til \Pit=262\my \Pit=247\wedd \Pit=220\ing cay \Pit=185\And \Pit=196\my \Pit=220\long tail coat \Pit=185\that \Pit=196\I \Pit=220\love so well \Pit=185\I \Pit=196\will \Pit=220\wear up \Pit=262\in the\Pit=247\chariot in the \Pit=196\morn.""=""Oh my golden slippers they are laid away. I was saving them until my wedding cay. And my long tail coat that I love so well I will wear up in the chariot in the morn.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=120\And \Pit=220\my \Pit=247\long white robe \Pit=196\that \Pit=220\had \Pit=247\bought last june\Pit=196\I \Pit=220\am \Pit=247\gonna change \Pit=220\it \Pit=247\'cause \Pit=262\it \Pit=247\fits \Pit=220\too soon.\Pit=185\And \Pit=196\the \Pit=220\old gray horse \Pit=185\that \Pit=196\I \Pit=220\used to drive\Pit=185\I \Pit=196\will \Pit=220\hitch \Pit=262\in the\Pit=247\chariot in the \Pit=196\morn.""==""And my long white robe that had bought last june I am gonna change it 'cause it fits too soon. And the old gray horse that I used to drive I will hitch in the chariot in the morn.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\Oh \Pit=196\dem \Pit=247\gold \Pit=220\en \Pit=196\slip \Pit=147\pers,\Pit=165\Oh \Pit=220\dem \Pit=262\gold \Pit=220\en \Pit=196\slip pers,""=""Oh dem golden slippers, Oh dem golden slippers,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=165\\Spd=120\golden slippers \Pit=196\I'm \Pit=220\gonna wear \Pit=185\be \Pit=196\cause \Pit=185\they \Pit=196\look \Pit=220\so \Pit=247\neat.""=""golden slippers I'm gonna wear because they look so neat.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\Oh \Pit=196\dem \Pit=247\gold \Pit=220\en \Pit=196\slip \Pit=147\pers,\Pit=165\Oh \Pit=220\dem \Pit=262\gold \Pit=220\en \Pit=196\slip pers,""=""Oh dem golden slippers, Oh dem golden slippers,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=165\\Spd=120\golden slippers \Pit=196\I'm \Pit=220\gonna \Pit=262\wear to \Pit=247\walk \Pit=262\the \Pit=247\gold \Pit=220\en \Pit=196\street.""=""golden slippers I'm gonna wear to walk the golden street.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=120\Oh \Pit=220\my \Pit=247\old ban \Pit=220\jo \Pit=196\hangs \Pit=220\up \Pit=247\on the wall \Pit=196\'cause \Pit=220\it \Pit=247\ain't \Pit=220\been \Pit=247\tuned \Pit=262\since \Pit=247\last \Pit=220\fall.\Pit=185\But \Pit=196\the \Pit=220\folks say \Pit=185\we'll \Pit=196\have \Pit=220\a real good time\Pit=185\when \Pit=196\we \Pit=220\ride up \Pit=262\in the\Pit=247\chariot in the \Pit=196\morn.""=""Oh my old banjo hangs up on the wall 'cause it ain't been tuned since last fall. But the folks say we'll have a real good time when we ride up in the chariot in the morn.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=120\There's \Pit=220\my \Pit=247\brother Ben, \Pit=196\and \Pit=220\my \Pit=247\sister Luce, \Pit=196\they \Pit=220\will \Pit=247\get the \Pit=220\news \Pit=247\to \Pit=262\uncle \Pit=247\Bacco \Pit=220\juice.\Pit=185\What \Pit=196\a \Pit=220\great camp meet \Pit=185\ing \Pit=196\we'll \Pit=220\have that day\Pit=185\when \Pit=196\we \Pit=220\ride up \Pit=262\in the\Pit=247\chariot in the \Pit=196\morn.""=""There's my brother Ben, and my sister Luce, they will get the news to uncle Bacco juice. What a great camp meeting we'll have that day when we ride up in the chariot in the morn.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\Oh \Pit=196\dem \Pit=247\gold \Pit=220\en \Pit=196\slip \Pit=147\pers,\Pit=165\Oh \Pit=220\dem \Pit=262\gold \Pit=220\en \Pit=196\slip pers,""=""Oh dem golden slippers, Oh dem golden slippers,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=165\\Spd=120\golden slippers \Pit=196\I'm \Pit=220\gonna wear \Pit=185\be \Pit=196\cause \Pit=185\they \Pit=196\look \Pit=220\so \Pit=247\neat.""=""golden slippers I'm gonna wear because they look so neat.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\Oh \Pit=196\dem \Pit=247\gold \Pit=220\en \Pit=196\slip \Pit=147\pers,\Pit=165\Oh \Pit=220\dem \Pit=262\gold \Pit=220\en \Pit=196\slip pers,""=""Oh dem golden slippers, Oh dem golden slippers,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=165\\Spd=120\golden slippers \Pit=196\I'm \Pit=220\gonna \Pit=262\wear to \Pit=247\walk \Pit=262\the \Pit=247\gold \Pit=220\en \Pit=196\street.""=""golden slippers I'm gonna wear to walk the golden street.""\")
                Exit Select
            Case 6
                Caine.Speak("\Chr=""Monotone""\\Pit=147\\Spd=130\And \Pit=165\come \Pit=185\tell me Sean \Pit=165\\map=""O \Pit=185\Fair \Pit=220\ul""=""O'Farrell""\ tell \Pit=185\me why \Pit=165\you hurry so, \Pit=220\\Spd=130\hush \map=""m \Pit=247\buh \Pit=196\chail""=""mbuachaill""\ \Pit=294\hush \Pit=277\and \Pit=220\\map=""lii \Pit=185\sen""=""listen""\ and \Pit=147\his \Pit=165\\Spd=200\cheeks \Pit=147\\Spd=130\were all aglow, I \Pit=165\bear \Pit=185\orders from \Pit=165\the \Pit=185\\map=""cap \Pit=220\tun""=""captain""\ get \Pit=185\you \map=""reh \Pit=165\dee""=""ready""\ quick and soon, \Pit=220\\Spd=130\for the \Pit=247\\Spd=200\pikes \Pit=196\\Spd=130\must \Pit=294\be \Pit=277\\map=""to \Pit=220\gheh \Pit=185\\Spd=200\fer""=""together""\ by \Pit=147\\Spd=130\the \Pit=165\\Spd=200\\map=""ri \Pit=147\\Spd=130\sing""=""rising""\ of the moon,")
                Caine.Speak("\Chr=""Monotone""\\Pit=147\\Spd=130\and \Pit=165\come \Pit=185\tell me Sean \Pit=165\\map=""O \Pit=185\Fair \Pit=220\ul""=""O'Farrell""\ where \Pit=185\the \map=""gather \Pit=165\ing's""=""gathering is""\ to be, \Pit=220\\Spd=130\at the \Pit=247\old \Pit=196\spot \Pit=294\by \Pit=277\the \Pit=220\\map=""rih \Pit=185\fer""=""river""\ quite \Pit=147\well \Pit=165\\Spd=200\known \Pit=147\\Spd=130\to you and me, one \Pit=165\more \Pit=185\word for \map=""sig \Pit=165\nal""=""signal""\ \Pit=185\\map=""toe \Pit=220\ken""=""token""\ \map=""wih \Pit=185\sul""=""whistle""\ out \Pit=165\the marching tune, \Pit=220\\Spd=130\with your \Pit=247\\Spd=200\pike \Pit=196\\Spd=130\\map=""up \Pit=294\on""=""upon""\ \Pit=277\your \Pit=220\\map=""shul \Pit=185\\Spd=200\der""=""shoulder""\ at \Pit=147\\Spd=130\the \Pit=165\\Spd=200\\map=""ri \Pit=147\\Spd=130\sing""=""rising""\ of the moon!")
                Exit Select
            Case 7
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=151\There's a time \Pit=147\in \Pit=196\each \Pit=175\year that we al \Pit=147\ways \Pit=196\hold \Pit=175\dear,\Pit=233\good \Pit=262\old \Pit=233\sum \Pit=196\mer \Pit=175\time.""=""There's a time in each year that we always hold dear, good old summer time.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=151\With the birds \Pit=147\and \Pit=196\the \Pit=175\tree \Pit=147\ees \Pit=196\and\Pit=175\sweet \Pit=147\scent \Pit=156\ed \Pit=175\breez \Pit=196\es,\Pit=220\good old sum \Pit=196\mer \Pit=262\time.""=""With the birds and the trees and the sweet scented breezes, good old summer time.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=151\When your day's \Pit=147\work \Pit=196\is \Pit=175\o \Pit=147\ver \Pit=196\then \Pit=175\you \Pit=147\are \Pit=87\in\Pit=175\clo \Pit=147\ver \Pit=87\and\Pit=196\life \Pit=185\is \Pit=196\one \Pit=233\beeu \Pit=196\tee \Pit=233\ful \Pit=175\rhyme.""=""When your day's work is over then you are in clover and life is one beautiful rhyme.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=151\No \Pit=196\truh \Pit=220\bull \Pit=196\an \Pit=175\noy \Pit=147\ing,\Pit=175\each \Pit=196\one \Pit=220\is \Pit=233\en \Pit=196\joy \Pit=175\ing, \Pit=196\the good old \Pit=233\sum \Pit=196\mer \Pit=262\time.""=""No trouble annoying, each one is enjoying, the good old summer time.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=151\In \Pit=196\the \Pit=175\good \Pit=147\old \Pit=175\sum \Pit=233\mer \Pit=294\time,\Pit=262\in \Pit=233\the good \Pit=262\old \Pit=233\sum \Pit=196\mer \Pit=175\time \Pit=147\stroll \Pit=175\ing \Pit=233\through \Pit=262\the \Pit=294\shade \Pit=262\ee \Pit=233\lanes \Pit=262\with \Pit=294\your\Pit=262\bai \Pit=294\bee \Pit=262\mine.""=""In the good old summer time, in the good old summer time strolling through the shady lanes with your baby mine.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=151\You \Pit=147\hold \Pit=175\her \Pit=233\hand \Pit=262\and \Pit=294\she \Pit=262\holds \Pit=233\yours and that's\Pit=262\a \Pit=233\vehr \Pit=196\ee \Pit=233\good \Pit=175\sign \Pit=175\that \Pit=147\she's \Pit=175\your \Pit=233\toot \Pit=262\see \Pit=294\woot \Pit=262\see \Pit=233\in\Pit=175\the \Pit=196\good \Pit=233\old sum \Pit=262\mer \Pit=233\time.""=""You hold her hand and she holds yours and that's a very good sign that she's your tootsee wootsee in the good old summer time.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=151\Oh to swim \Pit=147\in \Pit=196\the \Pit=175\pool you'd play hook\Pit=147\ee  \Pit=196\from  \Pit=175\school,\Pit=233\good \Pit=262\old \Pit=233\sum \Pit=196\mer \Pit=175\time.""=""Oh to swim in the pool you'd play hooky from school, good old summer time.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=151\You'd play ring \Pit=147\a \Pit=196\ro \Pit=175\see \Pit=147\with \Pit=196\Jim\Pit=175\Kate \Pit=156\and \Pit=175\Jo \Pit=196\see,\Pit=220\good old sum \Pit=196\mer \Pit=262\time.""=""You'd play ring a rosie with Jim Kate and Josie, good old summer time.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=151\Those are days \Pit=147\full \Pit=196\of \Pit=175\pleh \Pit=147\sure, \Pit=196\we \Pit=175\now \Pit=147\fond \Pit=87\lee\Pit=175\treh \Pit=147\sure \Pit=87\when\Pit=196\we \Pit=185\nev \Pit=196\er \Pit=233\thought \Pit=196\it \Pit=233\a \Pit=175\crime.""=""Those are days full of pleasure, we now fondly treasure when we never thought it a crime.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=151\To \Pit=196\go \Pit=220\steal \Pit=196\ing \Pit=175\chair \Pit=147\rees,\Pit=175\with \Pit=196\face \Pit=220\brown \Pit=233\as \Pit=196\bear \Pit=175\rees, \Pit=196\In the good old \Pit=233\sum \Pit=196\mer \Pit=262\time.""=""To go stealing cherries, with face brown as berries, In the good old summer time.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=151\In \Pit=196\the \Pit=175\good \Pit=147\old \Pit=175\sum \Pit=233\mer \Pit=294\time,\Pit=262\in \Pit=233\the good \Pit=262\old \Pit=233\sum \Pit=196\mer \Pit=175\time \Pit=147\stroll \Pit=175\ing \Pit=233\through \Pit=262\the \Pit=294\shade \Pit=262\ee \Pit=233\lanes \Pit=262\with \Pit=294\your\Pit=262\bai \Pit=294\bee \Pit=262\mine.""=""In the good old summer time, in the good old summer time strolling through the shady lanes with your baby mine.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=151\You \Pit=147\hold \Pit=175\her \Pit=233\hand \Pit=262\and \Pit=294\she \Pit=262\holds \Pit=233\yours and that's\Pit=262\a \Pit=233\vehr \Pit=196\ee \Pit=233\good \Pit=175\sign \Pit=175\that \Pit=147\she's \Pit=175\your \Pit=233\toot \Pit=262\see \Pit=294\woot \Pit=262\see \Pit=233\in\Pit=175\the \Pit=196\good \Pit=233\old sum \Pit=262\mer \Pit=233\time.""=""You hold her hand and she holds yours and that's a very good sign that she's your tootsee wootsee in the good old summer time.""\")
                Exit Select
            Case 8
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=156\\Spd=113\Beeu \Pit=147\tee \Pit=156\ful \Pit=117\dream \Pit=98\er, \Pit=87\wake \Pit=82\un \Pit=87\to \Pit=131\me,\Pit=117\star \Pit=147\light \Pit=131\and dew \Pit=117\drops \Pit=104\are with \Pit=98\in \Pit=87\for \Pit=98\thee.""=""Beautiful dreamer, wake unto me, starlight and dew drops are within for thee.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=156\\Spd=113\Sounds \Pit=147\of \Pit=156\the \Pit=117\rude \Pit=98\world \Pit=87\heard \Pit=82\n \Pit=87\the \Pit=131\day,\Pit=117\lulled \Pit=147\by \Pit=131\the moon \Pit=117\light \Pit=104\have all \Pit=98\passed \Pit=175\a \Pit=78\way.""=""Sounds of the rude world heard n the day, lulled by the moonlight have all passed away.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=113\Beeu \Pit=104\tee \Pit=87\ful \Pit=73\dream \Pit=131\er, queen \Pit=117\of \Pit=98\my \Pit=78\song,\Pit=156\list \Pit=147\while \Pit=156\I \Pit=131\woo \Pit=175\thee \Pit=156\with \Pit=147\soft \Pit=156\mel \Pit=131\o \Pit=117\dee.""=""Beautiful dreamer, queen of my song, list while I woo thee with soft melodee.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=156\\Spd=113\Gone \Pit=147\are \Pit=156\the \Pit=117\cares \Pit=98\of \Pit=87\life's \Pit=82\bis \Pit=87\ee \Pit=131\throng.\Pit=117\Beeu \Pit=147\tee \Pit=131\ful dream \Pit=117\er, \Pit=110\wake \Pit=98\un \Pit=87\to \Pit=98\me.""=""Gone are the cares of lifes busy throng, Beautiful dreamer, wake unto me.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=113\Beeu \Pit=147\tee \Pit=156\ful dream \Pit=117\er, \Pit=104\wake \Pit=98\un \Pit=87\to \Pit=78\me.""=""Beautiful dreamer, wake unto me.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=156\\Spd=113\Beeu \Pit=147\tee \Pit=156\ful \Pit=117\dream \Pit=98\er, \Pit=87\out \Pit=82\on \Pit=87\the \Pit=131\sea,\Pit=117\Mer \Pit=147\maids \Pit=131\are chan \Pit=117\ting \Pit=104\the wild \Pit=98\lor \Pit=87\aj\Pit=98\lei.""=""Beautiful dreamer, out on the sea, Mermaids are chanting the wild lorelei.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=156\\Spd=113\O \Pit=147\ver \Pit=156\the \Pit=117\stream \Pit=98\let \Pit=87\vae \Pit=82\pors \Pit=87\are \Pit=131\borne.\Pit=117\Wait \Pit=147\ing \Pit=131\to fade \Pit=117\on \Pit=104\the bright \Pit=98\com \Pit=175\ing \Pit=78\morn.""=""Over the steamlet vapors are borne. Waiting to fade on the bright coming morn.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=113\Beeu \Pit=104\tee \Pit=87\ful \Pit=73\dream \Pit=131\er, beam \Pit=117\of \Pit=98\my \Pit=78\heart.\Pit=156\e'en \Pit=147\as \Pit=156\the \Pit=131\morn \Pit=175\on \Pit=156\the \Pit=147\stream \Pit=156\let \Pit=131\and \Pit=117\sea.""=""Beautiful dreamer, beam of my heart, e'en as the morn on the streamlet and sea.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=156\\Spd=113\Then \Pit=147\with \Pit=156\all \Pit=117\clouds \Pit=98\of \Pit=87\sar \Pit=82\row \Pit=87\de \Pit=131\part.\Pit=117\Beeu \Pit=147\tee \Pit=131\ful dream \Pit=117\er, \Pit=110\wake \Pit=98\un \Pit=87\to \Pit=98\me.""=""Then with all clouds of sorrow depart. Beautiful dreamer, wake unto me.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=113\Beeu \Pit=147\tee \Pit=156\ful dream \Pit=117\er, \Pit=104\wake \Pit=98\un \Pit=87\to \Pit=78\me.""=""Beautiful dreamer, wake unto me.""\")
                Exit Select
            Case 9
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=123\\Spd=120\Twinkle \Pit=196\twinkle \Pit=220\little \Pit=196\star, \Pit=175\how I \Pit=165\wonder \Pit=147\what you \Pit=131\are,""=""Twinkle, twinkle little star, how I wonder what you are,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=120\up a \Pit=175\buv the \Pit=165\world so \Pit=147\high \Pit=196\like a \Pit=175\diamond \Pit=165\in the \Pit=147\sky.""=""up above the world so high, like a diamond in the sky.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=123\\Spd=120\Twinkle \Pit=196\twinkle \Pit=220\little \Pit=196\star, \Pit=175\how I \Pit=165\wonder \Pit=147\what you \Pit=131\are,""=""Twinkle, twinkle little star, how I wonder what you are.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=123\\Spd=120\When the \Pit=196\blazing \Pit=220\sun is \Pit=196\gone, \Pit=175\when he \Pit=165\nothing \Pit=147\shines uh \Pit=131\pon,""=""When the blazing sun is gone, when he nothing shines upon.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=120\Then you \Pit=175\show your \Pit=165\little \Pit=147\light \Pit=196\twinkle \Pit=175\twinkle \Pit=165\all the \Pit=147\night.""=""Then you show your little light twinkle, twinkle all the night.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=123\\Spd=120\Twinkle \Pit=196\twinkle \Pit=220\little \Pit=196\star, \Pit=175\how I \Pit=165\wonder \Pit=147\what you \Pit=131\are,""=""Twinkle, twinkle little star, how I wonder what you are.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=123\\Spd=120\Then the \Pit=196\traveler \Pit=220\in the \Pit=196\dark, \Pit=175\thanks you \Pit=165\for your \Pit=147\tiny \Pit=131\spark,""=""Then the traveler in the dark, thanks you for your tiny spark.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=120\He could \Pit=175\not see \Pit=165\where to \Pit=147\go, \Pit=196\if you \Pit=175\did not \Pit=165\twinkle \Pit=147\so.""=""He could not see where to go if you did not twinkle so.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=123\\Spd=120\Twinkle \Pit=196\twinkle \Pit=220\little \Pit=196\star, \Pit=175\how I \Pit=165\wonder \Pit=147\what you \Pit=131\are,""=""Twinkle, twinkle little star, how I wonder what you are.""\")
                Exit Select
            Case 10
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=130\Tell me \Pit=147\the \Pit=165\tales, that \Pit=175\to \Pit=196\me \Pit=220\were \Pit=196\so \Pit=165\dear\Pit=196\long \Pit=175\long \Pit=165\uh \Pit=147\go \Pit=175\long \Pit=165\long \Pit=147\uh \Pit=131\go""=""Tell me the tales that to me were so dear long, long ago, long, long ago.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=130\Sing me \Pit=147\the \Pit=165\songs, that \Pit=175\I \Pit=196\delight \Pit=220\ed \Pit=196\to \Pit=165\hear\Pit=196\long \Pit=175\long \Pit=165\uh \Pit=147\go \Pit=165\long, \Pit=147\uh \Pit=131\go.""=""Sing me the songs I delighted to hear long, long ago, long ago.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Now, \Pit=175\you \Pit=165\have \Pit=147\come, all my \Pit=175\grief \Pit=165\is \Pit=147\re \Pit=131\moved.""=""Now you have come all my grief is removed.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Let, \Pit=175\me \Pit=165\for \Pit=147\get, that so \Pit=175\long \Pit=165\you \Pit=147\have \Pit=131\roved.""=""Let me forget that so long you have roved.""\""")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=130\Let me \Pit=147\be \Pit=165\lieve, that \Pit=175\you \Pit=196\loved \Pit=220\as \Pit=196\you \Pit=165\loved\Pit=196\long \Pit=175\long \Pit=165\uh \Pit=147\go \Pit=165\long, \Pit=147\uh \Pit=131\go.""=""Let me believe that you loved as you loved long, long ago, long ago.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=130\Do you \Pit=147\re \Pit=165\member \Pit=175\the \Pit=196\path \Pit=220\where \Pit=196\we \Pit=165\met\Pit=196\long \Pit=175\long \Pit=165\uh \Pit=147\go \Pit=175\long \Pit=165\long \Pit=147\uh \Pit=131\go""=""Do you remember the path where we met long, long ago, long, long ago.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=130\Ah yes \Pit=147\you \Pit=165\told me \Pit=175\you \Pit=196\never \Pit=220\would \Pit=196\for \Pit=165\get \Pit=196\long \Pit=175\long \Pit=165\uh \Pit=147\go \Pit=165\long, \Pit=147\uh \Pit=131\go.""=""Ah! yes you told me you ne'er would forget long, long ago, long ago.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Then, \Pit=175\to \Pit=165\all \Pit=147\others, my \Pit=175\smile \Pit=165\you \Pit=147\pre \Pit=131\furred.""=""Then to all others my smile you preferred.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Love \Pit=175\when \Pit=165\you \Pit=147\spoke gave a \Pit=175\charm \Pit=165\to \Pit=147\each \Pit=131\word.""=""Love when you spoke gave a charm to each word.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=130\Still the \Pit=147\heart \Pit=165\treasures \Pit=175\the \Pit=196\praee \Pit=220\sehs \Pit=196\I \Pit=165\heard\Pit=196\long \Pit=175\long \Pit=165\uh \Pit=147\go \Pit=165\long, \Pit=147\uh \Pit=131\go.""=""Still the heart treasures the praises I heard long, long ago, long ago.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=130\Though by\Pit=147\your \Pit=165\kindness \Pit=175\my \Pit=196\fond \Pit=220\hopes \Pit=196\were \Pit=165\raised \Pit=196\long \Pit=175\long \Pit=165\uh \Pit=147\go \Pit=175\long \Pit=165\long \Pit=147\uh \Pit=131\go""=""Though by your kindness my fond hopes were raised long, long ago, long, long ago.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=130\You by \Pit=147\more \Pit=165\eloquent \Pit=175\lips \Pit=196\have \Pit=220\been \Pit=196\per \Pit=165\aised \Pit=196\long \Pit=175\long \Pit=165\uh \Pit=147\go \Pit=165\long, \Pit=147\uh \Pit=131\go.""=""You by more eloquent lips have been praised long, long ago, long ago.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\But, \Pit=175\by \Pit=165\your \Pit=147\absence your\Pit=175\truth \Pit=165\has \Pit=147\been \Pit=131\tried.""=""But by your absense your truth has been tried.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Still \Pit=175\to \Pit=165\your \Pit=147\accents I\Pit=175\lis \Pit=165\in \Pit=147\with \Pit=131\pride.""=""Still to your accents I listen with pride.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=130\Blest as \Pit=147\I \Pit=165\was when\Pit=175\I \Pit=196\sat \Pit=220\by \Pit=196\your \Pit=165\side \Pit=196\long \Pit=175\long \Pit=165\uh \Pit=147\go \Pit=165\long, \Pit=147\uh \Pit=131\go.""=""Blest as I was when I sat by your side long, long ago, long ago.""\")
                Exit Select
            Case 11
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=120\In \Pit=185\the \Pit=220\ree \Pit=185\june,\Pit=220\where \Pit=185\the \Pit=220\rose \Pit=208\es \Pit=196\all \Pit=131\ways \Pit=165\bloom.\Pit=247\Breeth \Pit=220\ing \Pit=247\out \Pit=220\up \Pit=247\on \Pit=220\the \Pit=247\air\Pit=277\their \Pit=294\sweet \Pit=185\per \Pit=220\fume.""=""In the region where the roses always bloom. Breathing out upon the air their sweet perfume.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=120\Lives \Pit=185\a \Pit=220\dus \Pit=185\kee \Pit=220\maid \Pit=185\I \Pit=220\long \Pit=247\to\Pit=277\\Pit=220\my \Pit=165\own.\Pit=277\For \Pit=247\I \Pit=277\know \Pit=247\my \Pit=277\love \Pit=247\for \Pit=277\her \Pit=247\will\Pit=220\nev \Pit=165\er \Pit=220\diiie.""=""Lives  a dusky maid I long to my own. For I know my love for her will never die.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=120\When \Pit=185\the \Pit=220\sun \Pit=185\is \Pit=220\sink \Pit=185\ing \Pit=220\in \Pit=208\the\Pit=196\gold \Pit=139\en \Pit=165\west.\Pit=247\Lit \Pit=220\tle \Pit=247\rob \Pit=220\in \Pit=247\Red \Pit=220\Breast \Pit=247\gone\Pit=277\to \Pit=294\seek \Pit=185\their \Pit=220\nest.""=""When the sun is sinking in that golden west. Little robin red Breast gone to seek their nest.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=120\Then \Pit=185\I \Pit=220\sneak \Pit=185\down \Pit=220\to \Pit=185\that \Pit=220\place\Pit=247\I \Pit=277\love \Pit=220\the \Pit=330\best.\Pit=277\Ev \Pit=247\ree \Pit=277\eve \Pit=247\ning \Pit=277\there \Pit=123\uh \Pit=277\lone \Pit=123\I \Pit=220\sigh.""=""Then I sneak down to that place I love the best. Every evening there alone I sigh.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=120\I \Pit=247\da, \Pit=220\sweet as \Pit=247\app \Pit=262\pel \Pit=277\ci \Pit=196\der,\Pit=247\sweet \Pit=277\er than \Pit=330\all \Pit=277\I \Pit=220\know.""=""Ida, sweet as apple cider, sweeter than all I know.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=120\Come \Pit=220\out \Pit=233\in the \Pit=247\silv \Pit=185\ree moon \Pit=165\light,\Pit=165\of \Pit=156\love \Pit=165\we'll \Pit=185\whis \Pit=165\per so \Pit=156\soft \Pit=165\and \Pit=220\low.""=""Come out in the silvery moonlight, of love we'll whisper so soft and low.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=120\Seems \Pit=247\though \Pit=220\can't \Pit=247\live \Pit=262\with \Pit=277\out \Pit=196\you.\Pit=247\Lis \Pit=277\en, oh \Pit=330\hon \Pit=277\nee \Pit=220\do.""=""Seems though can't live without you. Listen, oh honey dew.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=120\I \Pit=220\duh, \Pit=233\I \Pit=247\I \Pit=185\dull lies \Pit=165\you.\Pit=247\I \Pit=294\love \Pit=247\you \Pit=294\I \Pit=247\duh \Pit=294\deed \Pit=330\I \Pit=294\do.""=""Ida, I idolize you. I love you ida, 'deed I do.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=120\When \Pit=185\the \Pit=220\moon \Pit=185\comes,\Pit=220\steal \Pit=185\ing \Pit=220\up \Pit=208\be \Pit=196\hind \Pit=131\the \Pit=165\hill.\Pit=247\Ev \Pit=220\ree \Pit=247\thing \Pit=220\uh \Pit=247\round \Pit=220\me \Pit=247\seems\Pit=277\so \Pit=294\calm \Pit=185\and \Pit=220\still.""=""When the moon comes stealing up behind the hill. Everything around me seems so calm and still.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=120\Save \Pit=185\the \Pit=220\gen \Pit=185\tle \Pit=220\\Pit=185\ing \Pit=220\of \Pit=247\the\Pit=277\whip \Pit=220\poor \Pit=165\will.\Pit=277\Then \Pit=247\I \Pit=277\long \Pit=247\to \Pit=277\hold \Pit=247\her \Pit=277\lit \Pit=247\tle\Pit=220\hand \Pit=165\in \Pit=220\mine.""=""Save a gentle calling of the whippoorwill. Then I long to hold her little hand in mine.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=120\Through \Pit=185\the \Pit=220\wind \Pit=185\the \Pit=220\trees \Pit=185\are \Pit=220\sigh \Pit=208\ing\Pit=196\soft \Pit=139\and \Pit=165\low.\Pit=247\Seem \Pit=220\to \Pit=247\come \Pit=220\and \Pit=247\whis \Pit=220\per \Pit=247\that\Pit=277\your \Pit=294\love \Pit=185\is \Pit=220\true.""=""Through the wind the trees are sighing soft and low. Seem to come and whisper that your love is true.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=120\Come \Pit=185\I \Pit=220\and \Pit=185\be \Pit=220\my \Pit=185\own \Pit=220\now,\Pit=247\sweet \Pit=277\heart \Pit=220\do, \Pit=330\oh\Pit=277\do. \Pit=247\Then \Pit=277\my \Pit=247\life \Pit=277\will \Pit=123\seem \Pit=277\all \Pit=123\most \Pit=220\devine.""=""Come and be my own now, sweatheart do, oh do. Then my life will seem almost devine.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=120\I \Pit=247\da, \Pit=220\sweet as \Pit=247\app \Pit=262\pel \Pit=277\ci \Pit=196\der,\Pit=247\sweet \Pit=277\er than \Pit=330\all \Pit=277\I \Pit=220\know.""=""Ida, sweet as apple cider, sweeter than all I know.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=120\Come \Pit=220\out \Pit=233\in the \Pit=247\silv \Pit=185\ree moon \Pit=165\light,\Pit=165\of \Pit=156\love \Pit=165\we'll \Pit=185\whis \Pit=165\per so \Pit=156\soft \Pit=165\and \Pit=220\low.""=""Come out in the silvery moonlight, of love we'll whisper so soft and low.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=120\Seems \Pit=247\though \Pit=220\can't \Pit=247\live \Pit=262\with \Pit=277\out \Pit=196\you.\Pit=247\Lis \Pit=277\en, oh \Pit=330\hon \Pit=277\nee \Pit=220\do.""=""Seems though can't live without you. Listen, oh honey dew.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=120\I \Pit=220\duh, \Pit=233\I \Pit=247\I \Pit=185\dull lies \Pit=165\you.\Pit=247\I \Pit=294\love \Pit=247\you \Pit=294\I \Pit=247\duh \Pit=294\deed \Pit=330\I \Pit=294\do.""=""Ida, I idolize you. I love you ida, 'deed I do.""\")
                Exit Select
            Case 12
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=120\Now \Pit=131\how I \Pit=165\came \Pit=196\to \Pit=262\get this hat tis vehr \Pit=196\ree \Pit=175\strange \Pit=165\and fun \Pit=147\nee.""=""Now how I came to get this hat 'tis very strange and funny.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\Grandfather, passed \Pit=220\and \Pit=294\left to me \Pit=262\his \Pit=247\prop \Pit=196\er \Pit=220\tee \Pit=247\and \Pit=220\mun \Pit=196\ee.""=""Grandfather passed and left to me his property and money.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=262\\Spd=120\And \Pit=330\when \Pit=262\the \Pit=294\will \Pit=262\was \Pit=220\read \Pit=165\they \Pit=175\told \Pit=196\me \Pit=220\straight\Pit=262\and \Pit=147\flat.""=""And when the will was read they told me straight and flat,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=165\\Spd=120\If \Pit=131\I would \Pit=165\have \Pit=196\his \Pit=262\money I \Pit=196\must \Pit=165\all \Pit=175\ways \Pit=165\wear \Pit=147\his\Pit=131\hat.""=""if I would have his money I must always wear his hat.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=120\Where \Pit=165\did \Pit=196\you \Pit=262\get that hat Where did \Pit=196\you \Pit=165\get \Pit=131\that \Pit=147\tile?""=""""Where did you get that hat, where did you get that tile?""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\Isn't \Pit=175\it \Pit=220\a \Pit=294\nobby one \Pit=262\and \Pit=247\just \Pit=196\the \Pit=220\prop \Pit=247\er \Pit=196\style?""=""Isn't it a nobby one and just the proper style?""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=330\\Spd=120\I \Pit=262\should \Pit=294\like \Pit=247\to \Pit=262\have \Pit=196\one \Pit=220\just \Pit=262\the \Pit=196\same \Pit=165\as\Pit=147\that.""=""""I should like to have one just the same as that.""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\Where \Pit=131\er I \Pit=165\go \Pit=196\they \Pit=220\shout Hel \Pit=196\lo \Pit=175\where \Pit=165\did \Pit=196\you \Pit=175\get\Pit=147\that\Pit=131\hat.""=""Where e'er I go they shout, ""Hello, where did you get that hat?""""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=120\If \Pit=131\I go \Pit=165\to \Pit=196\the \Pit=262\opera house amid \Pit=196\the \Pit=175\opp \Pit=165\ruh sea \Pit=147\son.""=""If I go to the op'ra house amid the op'ra season.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\There's someone sure \Pit=220\to \Pit=294\shout at me \Pit=262\with \Pit=247\out \Pit=196\the \Pit=220\slight \Pit=247\est \Pit=220\rea \Pit=196\son.""=""There's someone sure to shout at me without the slightest reason.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=262\\Spd=120\If \Pit=330\I \Pit=262\go \Pit=294\to \Pit=262\chow \Pit=220\der \Pit=196\club \Pit=165\to \Pit=175\have \Pit=196\the \Pit=220\part\Pit=262\ee \Pit=147\spree.""=""If I go to chowder club to have the party spree,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=165\\Spd=120\There's \Pit=131\someone \Pit=165\in \Pit=196\the \Pit=262\party that's \Pit=165\sure \Pit=175\to \Pit=165\shout \Pit=147\at\Pit=131\me.""=""there's someone in that party that's sure to shout at me.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=120\Where \Pit=165\did \Pit=196\you \Pit=262\get that hat Where did \Pit=196\you \Pit=165\get \Pit=131\that \Pit=147\tile?""=""""Where did you get that hat, where did you get that tile?""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\Isn't \Pit=175\it \Pit=220\a \Pit=294\nobby one \Pit=262\and \Pit=247\just \Pit=196\the \Pit=220\prop \Pit=247\er \Pit=196\style?""=""Isn't it a nobby one and just the proper style?""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=330\\Spd=120\I \Pit=262\should \Pit=294\like \Pit=247\to \Pit=262\have \Pit=196\one \Pit=220\just \Pit=262\the \Pit=196\same \Pit=165\as\Pit=147\that.""=""""I should like to have one just the same as that.""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\Where \Pit=131\er I \Pit=165\go \Pit=196\they \Pit=220\shout Hel \Pit=196\lo \Pit=175\where \Pit=165\did \Pit=196\you \Pit=175\get\Pit=147\that\Pit=131\hat.""=""Where e'er I go they shout, ""Hello, where did you get that hat?""""\")
                Exit Select
            Case 13
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=132\I've \Pit=131\been \Pit=175\wor \Pit=131\king \Pit=175\on \Pit=196\the \Pit=220\rail \Pit=175\road\Pit=233\all the \Pit=175\liv \Pit=196\long \Pit=220\day.""=""I've been working on the railroad all the live long day.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=132\I've \Pit=131\been \Pit=175\wor \Pit=131\king \Pit=175\on \Pit=196\the \Pit=220\rail \Pit=175\road\Pit=220\to pass \Pit=196\the time \Pit=220\a \Pit=196\way.""=""I've been working on the railroad to pass the time away.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=132\Don't you \Pit=185\hear \Pit=196\the \Pit=220\whih \Pit=196\stle \Pit=175\blow, \Pit=131\ing\Pit=233\rise up so \Pit=175\early \Pit=196\in the \Pit=220\morn.""=""Don't you hear the whistle blowing, rise up so early in the morn.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=132\Don't \Pit=165\you \Pit=175\hear \Pit=165\the \Pit=175\cap \Pit=147\tain \Pit=131\shout \Pit=175\ing,\Pit=220\Di \Pit=233\nuh \Pit=220\blow \Pit=196\your \Pit=175\horn.""=""Don't you hear the captain shouting ""Dinah, blow your horn!""""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=132\Dinah, won't you \Pit=175\blow \Pit=147\Dinah, won't you \Pit=196\blow \Pit=165\Dinah, won't you \Pit=147\blow \Pit=165\your \Pit=175\ho \Pit=196\ho \Pit=220\horn""=""Dinah won't you blow, Dinah won't you blow, Dinah won't you blow your horn.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=130\Dinah, won't you \Pit=175\blow \Pit=147\Dinah, won't you \Pit=196\blow \Pit=165\Dinah, won't you \Pit=147\blow \Pit=165\your \Pit=175\horn.""=""Dinah won't you blow, Dinah won't you blow, Dinah won't you blow your horn.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=132\Some ones in the \Pit=175\kitchen with \Pit=131\Di \Pit=175\nah! \Pit=220\Some ones in the \Pit=175\kitchen I \Pit=196\know \Pit=165\oh \Pit=131\oh \Pit=110\oh!""=""Someone's in the kitchen with Dinah! Someone's in the kitchen I know!""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=132\Some ones in the \Pit=175\kitchen with \Pit=233\Di \Pit=294\nah \Pit=262\strummin' on the \Pit=233\old \Pit=196\ban \Pit=175\jo.""=""Someone's in the kitchen with Dinah, strummin' on the old banjo.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=132\Fee, \Pit=175\Fee, \Pit=131\fiddle \Pit=147\ee \Pit=165\i \Pit=175\ho\Pit=220\Fee \Pit=175\Fee \Pit=175\fiddle ee i \Pit=196\ho \Pit=165\ho \Pit=147\ho \Pit=131\ho""=""Fee Fee fiddle-ee i ho Fee Fee fiddle-ee i ho ho ho ho""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=132\Fee, \Pit=175\Fee, \Pit=247\fiddle ee i \Pit=294\ho, \Pit=262\Strummin' on the  \Pit=247\old \Pit=196\ban \Pit=175\jo!""=""Fee Fee fiddle-ee i ho Strummin' on the old banjo!""\")
                Exit Select
            Case 14
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\If \Pit=156\you \Pit=175\lis \Pit=196\en \Pit=220\I'll \Pit=233\sing \Pit=294\you \Pit=262\a sweet \Pit=233\lit \Pit=196\tle\Pit=233\song \Pit=147\of \Pit=156\a \Pit=175\floww \Pit=196\er \Pit=233\that's \Pit=262\now \Pit=294\drooped \Pit=262\and dead.""=""If you listen I'll sing you a sweet little song of a flower that's now drooped and dead.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\Yet \Pit=156\it's \Pit=175\dear \Pit=196\er \Pit=220\to \Pit=233\me \Pit=294\yes \Pit=262\than all \Pit=233\of \Pit=196\it's\Pit=233\mates \Pit=147\all \Pit=156\though \Pit=175\each \Pit=196\holds \Pit=233\a \Pit=262\loft \Pit=233\it's \Pit=220\proud \Pit=233\head.""=""Yet it's dearer to me, yes, than all of it's mates although each holds aloft it's proud head ""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=294\\Spd=120\T'was \Pit=262\give \Pit=294\en \Pit=311\to me \Pit=294\by \Pit=262\a \Pit=247\girl \Pit=294\I \Pit=311\know\Pit=294\since we've met \Pit=262\faith \Pit=247\I've \Pit=196\known \Pit=220\no \Pit=247\re \Pit=262\pose.""=""T'was given to me by a girl I know, since we've met, faith I've known no repose.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\She \Pit=156\is \Pit=175\dear \Pit=196\er \Pit=220\by \Pit=233\far \Pit=294\than \Pit=262\the world's \Pit=233\bright \Pit=196\est\Pit=233\star \Pit=147\and \Pit=156\I \Pit=175\\Pit=196\her \Pit=233\my \Pit=262\wild \Pit=233\I \Pit=220\rish \Pit=233\rose.""=""She is dearer by far than the world's most brightest star and I her my wild Irish rose.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=120\My \Pit=294\wild I \Pit=262\rish \Pit=233\rose, the sweetest \Pit=220\flower \Pit=196\that \Pit=175\grows.""=""My wild Irish rose, the sweetest flower that grows.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\You may  \Pit=156\search \Pit=175\ev \Pit=196\ree \Pit=175\where \Pit=147\but \Pit=156\none \Pit=175\can \Pit=196\com \Pit=175\pare\Pit=220\with  \Pit=247\my \Pit=196\wild \Pit=294\I \Pit=262\rish rose.""=""You may search everywhere but none can compare with my wild Irish rose.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=120\My \Pit=294\wild I \Pit=262\rish \Pit=247\rose, the dearest \Pit=220\flower \Pit=196\that \Pit=175\grows.""=""My wild Irish rose, the dearest flower that grows.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\And some \Pit=156\day \Pit=175\for \Pit=196\my \Pit=175\sake \Pit=147\she \Pit=156\may \Pit=175\let \Pit=196\me \Pit=175\take\Pit=175\the \Pit=196\bloom, \Pit=220\from \Pit=233\my wild \Pit=262\I \Pit=233\rish rose.""=""And someday for my sake she may let me take the bloom of my wild Irish rose.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\They \Pit=156\may \Pit=175\sing \Pit=196\of \Pit=220\their \Pit=233\ro \Pit=294\ses \Pit=262\which by \Pit=233\uth \Pit=196\er\Pit=233\names \Pit=147\they \Pit=156\would \Pit=175\smell \Pit=196\just \Pit=233\as \Pit=262\sweet \Pit=294\lee \Pit=262\they say.""=""They may sing of their roses which by other names they would smell just as sweetly they say.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\But \Pit=156\I \Pit=175\know \Pit=196\my \Pit=220\dear \Pit=233\rose \Pit=294\would \Pit=262\nev \Pit=233\er \Pit=196\con\Pit=233\sent \Pit=147\to \Pit=156\have \Pit=175\that \Pit=196\sweet \Pit=233\name \Pit=262\take \Pit=233\en \Pit=220\a \Pit=233\way.""=""But I know that my dear rose would never consent to have that sweet name taken away.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=294\\Spd=120\Her \Pit=262\glan \Pit=294\ces \Pit=311\are shy \Pit=294\when \Pit=262\e'er \Pit=247\I \Pit=294\pass \Pit=311\by,\Pit=294\at the bow \Pit=262\er \Pit=247\where \Pit=196\my \Pit=220\true \Pit=247\love \Pit=262\grows.""=""Her glances are shy just whene'er I pass by, at the bower where my true love grows.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\And \Pit=156\my \Pit=175\one \Pit=196\wish \Pit=220\has \Pit=233\been \Pit=294\that \Pit=262\someday \Pit=233\I \Pit=196\may \Pit=233\win \Pit=147\the \Pit=156\dear \Pit=175\heart \Pit=196\of \Pit=233\my \Pit=262\wild \Pit=233\I \Pit=220\rish \Pit=233\rose.""=""And my one wish has been that someday I may win the dear heart of my wild irish rose.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=120\My \Pit=294\wild I \Pit=262\rish \Pit=233\rose, the sweetest \Pit=220\flower \Pit=196\that \Pit=175\grows.""=""My wild Irish rose, the sweetest flower that grows.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\You may  \Pit=156\search \Pit=175\ev \Pit=196\ree \Pit=175\where \Pit=147\but \Pit=156\none \Pit=175\can \Pit=196\com \Pit=175\pare\Pit=220\with  \Pit=247\my \Pit=196\wild \Pit=294\I \Pit=262\rish rose.""=""You may search everywhere but none can compare with my wild Irish rose.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=120\My \Pit=294\wild I \Pit=262\rish \Pit=247\rose, the dearest \Pit=220\flower \Pit=196\that \Pit=175\grows.""=""My wild Irish rose, the dearest flower that grows.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=147\\Spd=120\And some \Pit=156\day \Pit=175\for \Pit=196\my \Pit=175\sake \Pit=147\she \Pit=156\may \Pit=175\let \Pit=196\me \Pit=175\take\Pit=175\the \Pit=196\bloom, \Pit=220\from \Pit=233\my wild \Pit=262\I \Pit=233\rish rose.""=""And someday for my sake she may let me take the bloom of my wild Irish rose.""\")
                Exit Select
            Case 15
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\Now \Pit=262\lou \Pit=294\is \Pit=247\came \Pit=262\home \Pit=220\to \Pit=247\the \Pit=165\flat.""=""Now Louis came home to the flat.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\He \Pit=262\hung \Pit=294\up \Pit=247\his \Pit=262\coat \Pit=220\and \Pit=247\his \Pit=175\hat.""=""He hung up his coat and his hat.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=165\\Spd=184\He \Pit=147\gazed \Pit=165\all \Pit=175\a \Pit=196\round \Pit=220\but \Pit=247\no \Pit=294\wife \Pit=262\ee \Pit=220\he\Pit=196\found so \Pit=220\he \Pit=247\said \Pit=233\where \Pit=247\could \Pit=262\floss \Pit=247\ee \Pit=220\be \Pit=294\at?""=""He gazed all around but no wifey he found so he said, ""Where could Floss e be at?""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\A \Pit=262\ note \Pit=294\on \Pit=247\the \Pit=262\tae \Pit=220\bel \Pit=247\he \Pit=165\spied.""=""A note on the table he spied.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\He \Pit=262\read \Pit=294\it \Pit=247\just \Pit=262\once \Pit=196\than \Pit=208\he \Pit=220\cried.""=""He read it just once than he cried.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=184\It ran \Pit=208\Lou \Pit=220\is \Pit=262\dear \Pit=294\it's \Pit=311\too \Pit=330\slow \Pit=262\for \Pit=220\me \Pit=196\here so \Pit=220\I \Pit=247\think \Pit=233\I \Pit=247\will \Pit=262\go \Pit=247\for \Pit=220\a \Pit=294\ride.""=""It ran Lousi, dear it's too slow for me here, so I think I will go for a ride.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=184\Meet \Pit=147\me \Pit=165\in \Pit=175\Saint \Pit=196\Lou \Pit=330\is \Pit=262\Lou \Pit=196\is,\Spd=184\meet \Pit=262\me \Pit=247\at \Pit=220\the \Pit=196\fair.""=""Meet me in Saint Louis, Louis, meet me at the fair.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=184\Don't \Pit=147\tell \Pit=165\me \Pit=175\the \Pit=196\lights \Pit=330\are \Pit=262\shine \Pit=196\ing \Pit=220\an \Pit=294\nee place \Pit=330\than \Pit=294\there.""=""Don't tell me the lights are shining any place than there.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=294\\Spd=184\We \Pit=311\will \Pit=330\dance \Pit=294\the \Pit=262\hooch \Pit=247\ee \Pit=330\kooch \Pit=220\ee \Pit=208\I \Pit=220\will \Pit=294\be \Pit=262\your \Pit=247\toot \Pit=220\see \Pit=294\woot \Pit=196\see.""=""We will dance the hoochie koochie. I will be your tootsie wootsie.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=184\Won't \Pit=147\you \Pit=131\meet \Pit=147\me \Pit=165\in \Pit=175\saint \Pit=196\lou \Pit=330\is \Pit=262\lou \Pit=196\is,\Pit=220\meet \Pit=262\me \Pit=294\at \Pit=196\the \Pit=262\fair.""=""Won't you meet me in Saint Louis, Louis meet me at the fair.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\The \Pit=262\dress \Pit=294\es \Pit=247\that \Pit=262\hung \Pit=220\in \Pit=247\the \Pit=165\hall.""=""The dresses that hung in the hall.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\Were \Pit=262\gone \Pit=294\she \Pit=247\had \Pit=262\take \Pit=220\en \Pit=247\them \Pit=175\all.""=""Were gone she had taken them all.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=165\\Spd=184\She \Pit=147\took \Pit=165\all \Pit=175\his \Pit=196\rings \Pit=220\and \Pit=247\the \Pit=294\rest \Pit=262\of \Pit=220\his\Pit=196\things and \Pit=220\the \Pit=247\pic \Pit=233\ture \Pit=247\he \Pit=262\missed \Pit=247\from \Pit=220\the \Pit=294\wall.""=""She took all of his rings and the rest of his things and the picture he missed from his wall.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\What \Pit=262\ move \Pit=294\ing \Pit=247\the \Pit=262\jan \Pit=220\it \Pit=247\or \Pit=165\said.""=""""What moving!"" the janitor said.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\Your \Pit=262\rent \Pit=294\is \Pit=247\paid \Pit=262\three \Pit=196\months \Pit=208\uh \Pit=220\head.""=""""Your rent is paid tree months ahead!""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=220\\Spd=184\What good\Pit=208\is \Pit=220\the \Pit=262\flat \Pit=294\said \Pit=311\poor \Pit=330\Lou \Pit=262\is \Pit=220\Read that \Pit=196\And the \Pit=220\jan \Pit=247\it \Pit=233\or \Pit=247\smile \Pit=262\duh \Pit=247\as \Pit=220\he \Pit=294\read.""=""""What good is the flat?"" said poor Louis, ""Read that."" and the janitor smiled as he read.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=184\Meet \Pit=147\me \Pit=165\in \Pit=175\Saint \Pit=196\Lou \Pit=330\is \Pit=262\Lou \Pit=196\is,\Spd=184\meet \Pit=262\me \Pit=247\at \Pit=220\the \Pit=196\fair.""=""Meet me in Saint Louis, Louis, meet me at the fair.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=184\Don't \Pit=147\tell \Pit=165\me \Pit=175\the \Pit=196\lights \Pit=330\are \Pit=262\shine \Pit=196\ing \Pit=220\an \Pit=294\nee place \Pit=330\than \Pit=294\there.""=""Don't tell me the lights are shining any place than there.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=294\\Spd=184\We \Pit=311\will \Pit=330\dance \Pit=294\the \Pit=262\hooch \Pit=247\ee \Pit=330\kooch \Pit=220\ee \Pit=208\I \Pit=220\will \Pit=294\be \Pit=262\your \Pit=247\toot \Pit=220\see \Pit=294\woot \Pit=196\see.""=""We will dance the hoochie koochie. I will be your tootsie wootsie.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=175\\Spd=184\Won't \Pit=147\you \Pit=131\meet \Pit=147\me \Pit=165\in \Pit=175\saint \Pit=196\lou \Pit=330\is \Pit=262\lou \Pit=196\is,\Pit=220\meet \Pit=262\me \Pit=294\at \Pit=196\the \Pit=262\fair.""=""Won't you meet me in Saint Louis, Louis meet me at the fair.""\")
                Exit Select
            Case 16
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=184\Down \Pit=294\in \Pit=220\front \Pit=247\of \Pit=196\Cae, \Pit=247\sees. \Pit=196\Old brown \Pit=220\woo \Pit=196\ood \Pit=165\en\Pit=196\stoop.""=""Down in front of Casey's old brown wooden stoop.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\On a \Pit=220\sum \Pit=165\mers \Pit=147\eve \Pit=196\ning \Pit=247\we formed \Pit=220\a \Pit=165\mehr \Pit=247\ree \Pit=220\group.""=""On a summer's evening we formed a merry group.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=184\Boys \Pit=294\and \Pit=220\girls \Pit=247\to \Pit=196\geth \Pit=247\er \Pit=196\we would \Pit=220\sing \Pit=165\and \Pit=196\waltz.""=""Boys and girls together, we would sing and waltz,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\While Tone \Pit=220\ee \Pit=196\played \Pit=147\or \Pit=196\gan \Pit=262\on \Pit=247\the side \Pit=220\walks\Pit=165\of \Pit=185\New \Pit=196\York.""=""While Tony played organ on the sidewalks of New York.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=294\\Spd=184\East, \Pit=247\side, \Pit=220\West, \Pit=196\side, \Pit=220\all \Pit=196\a \Pit=165\round \Pit=185\the \Pit=196\town.""=""East side, west side, all around the town,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\the tots sang \Pit=220\ring \Pit=165\a \Pit=147\ro \Pit=196\see \Pit=262\Lon \Pit=247\don bridge \Pit=220\is fall \Pit=165\ing \Pit=220\down.""=""the tots sang ring rosie, London bridge is falling down.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=184\Boys \Pit=294\and \Pit=220\girls \Pit=247\to \Pit=196\geth \Pit=247\er, \Pit=196\me and \Pit=220\Mam \Pit=165\me \Pit=196\O'Rourke,""=""Boys and girls together, me and Mamie O'Rourke,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\tripped the \Pit=220\light \Pit=165\fan \Pit=147\tas \Pit=196\tic, \Pit=262\on \Pit=247\the side \Pit=220\walks \Pit=165\of \Pit=185\New\Pit=196\York.""=""tripped the light fantastic on the sidewalks of New York.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=184\That's \Pit=294\were \Pit=220\John \Pit=247\nee \Pit=196\Cae, \Pit=247\see. \Pit=196\little \Pit=220\Jim \Pit=165\ee\Pit=196\Crow.""=""That's where Johnny Casey, little Jimmy Crow,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\Jakey \Pit=220\Krause \Pit=165\the \Pit=147\bae \Pit=196\ker \Pit=247\who all \Pit=220\ways \Pit=165\had \Pit=247\the \Pit=220\dough.""=""Jakey Krause the baker who always had the dough.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=184\Prit \Pit=294\ee \Pit=220\Nel \Pit=247\lee \Pit=196\Shan \Pit=247\non \Pit=196\dude as \Pit=220\light \Pit=165\as a\Pit=196\cork.""=""Pretty Nellie Shannon, dude as light as a cork,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\she first \Pit=220\picked \Pit=196\up \Pit=165\the \Pit=147\waltz \Pit=196\step \Pit=262\on \Pit=247\the side \Pit=220\walks\Pit=165\of \Pit=185\New \Pit=196\York.""=""she first picked up the waltz step on the sidewalks of New York.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=294\\Spd=184\East, \Pit=247\side, \Pit=220\West, \Pit=196\side, \Pit=220\all \Pit=196\a \Pit=165\round \Pit=185\the \Pit=196\town.""=""East side, west side, all around the town,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\the tots sang \Pit=220\ring \Pit=165\a \Pit=147\ro \Pit=196\see \Pit=262\Lon \Pit=247\don bridge \Pit=220\is fall \Pit=165\ing \Pit=220\down.""=""the tots sang ring rosie, London bridge is falling down.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=184\Boys \Pit=294\and \Pit=220\girls \Pit=247\to \Pit=196\geth \Pit=247\er, \Pit=196\me and \Pit=220\Mam \Pit=165\me \Pit=196\O'Rourke,""=""Boys and girls together, me and Mamie O'Rourke,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\tripped the \Pit=220\light \Pit=165\fan \Pit=147\tas \Pit=196\tic, \Pit=262\on \Pit=247\the side \Pit=220\walks \Pit=165\of \Pit=185\New\Pit=196\York.""=""tripped the light fantastic on the sidewalks of New York.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=184\Things \Pit=294\have \Pit=220\changed \Pit=247\since \Pit=196\that, \Pit=247\time. \Pit=196\Some are \Pit=220\uh \Pit=196\up \Pit=165\in\Pit=196\G.""=""Things have changed since that time, some are uh up in ""G"",""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\others \Pit=220\they \Pit=165\are \Pit=147\wander \Pit=196\ing \Pit=247\but they all\Pit=220\feel \Pit=165\just \Pit=247\like \Pit=220\me.""=""others they are wandering but they all feel just like me.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=184\They'd \Pit=294\part \Pit=220\with \Pit=247\all \Pit=196\they've \Pit=247\got \Pit=196\could they \Pit=220\once \Pit=165\more \Pit=196\walk.""=""They'd part with all they've got could they once more walk,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\with their \Pit=220\best \Pit=196\girl\Pit=165\and \Pit=147\have \Pit=196\a \Pit=262\twirl \Pit=247\on the Side \Pit=220\walks\Pit=165\of \Pit=185\New \Pit=196\York.""=""with their best girl and have a twirl on the Sidewalks of New York.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=294\\Spd=184\East, \Pit=247\side, \Pit=220\West, \Pit=196\side, \Pit=220\all \Pit=196\a \Pit=165\round \Pit=185\the \Pit=196\town.""=""East side, west side, all around the town,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\the tots sang \Pit=220\ring \Pit=165\a \Pit=147\ro \Pit=196\see \Pit=262\Lon \Pit=247\don bridge \Pit=220\is fall \Pit=165\ing \Pit=220\down.""=""the tots sang ring rosie, London bridge is falling down.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=184\Boys \Pit=294\and \Pit=220\girls \Pit=247\to \Pit=196\geth \Pit=247\er, \Pit=196\me and \Pit=220\Mam \Pit=165\me \Pit=196\O'Rourke,""=""Boys and girls together, me and Mamie O'Rourke,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=184\tripped the \Pit=220\lights \Pit=165\fan \Pit=147\tas \Pit=196\tic, \Pit=262\on \Pit=247\the side \Pit=220\walks \Pit=165\of \Pit=185\New\Pit=196\York.""=""tripped the lights fantastic on the sidewalks of New York.""\")
                Exit Select
            Case 17
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\Now once \Pit=117\I \Pit=131\was \Pit=147\hap \Pit=156\ee \Pit=147\but now \Pit=131\I'm \Pit=98\for \Pit=131\lorn.""=""Now once I was happy but now I'm forlorn.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\Like \Pit=110\an \Pit=117\old \Pit=131\coat \Pit=147\that \Pit=131\is tat \Pit=117\erd \Pit=73\and \Pit=87\torn,""=""Like an old coat that is tattered and torn,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\and left \Pit=117\in \Pit=131\this \Pit=147\wide \Pit=156\world \Pit=147\to fret \Pit=131\and \Pit=98\to \Pit=131\mourn,""=""and left in this wide world to fret and to mourn,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=184\be \Pit=110\trayed \Pit=104\by \Pit=110\a \Pit=147\maid in \Pit=131\her \Pit=117\teens.""=""betrayed by a maid in her teens.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=184\Now this girl \Pit=131\that \Pit=117\I \Pit=110\love \Pit=117\she \Pit=110\is \Pit=98\hand \Pit=73\some""=""Now this girl that I love, she is handsome""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=184\and I tried \Pit=131\all \Pit=117\I \Pit=110\knew \Pit=117\her \Pit=110\to \Pit=98\please.""=""and I tried all I knew her to please.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=110\\Spd=184\But \Pit=117\I \Pit=131\could \Pit=117\never \Pit=110\please \Pit=117\her \Pit=110\one \Pit=98\quart \Pit=73\er so \Pit=147\well\Pit=131\as \Pit=117\the \Pit=131\man \Pit=117\on \Pit=110\the \Pit=117\fly \Pit=110\ing trap \Pit=98\eze.""=""But I could never please her one quarter so well as the man on the flying trapeze.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\Oh \Pit=78\he \Pit=73\floats \Pit=87\through \Pit=117\the \Pit=147\air \Pit=156\with \Pit=147\the great \Pit=131\est \Pit=98\of\Pit=131\ease,""=""Oh he floats through the air with the greatest of ease,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\the dare \Pit=110\ing \Pit=117\young \Pit=131\man \Pit=147\on \Pit=131\the fly \Pit=117\ing \Pit=73\trap \Pit=87\eze.""=""the daring young man on the flying trapeze.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\His move \Pit=117\ments \Pit=131\are \Pit=147\grace \Pit=156\ful \Pit=147\all girls \Pit=131\he \Pit=98\does \Pit=131\please""=""His movements are graceful, all girls he does please""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=184\and \Pit=110\my \Pit=104\love \Pit=110\he has \Pit=147\purloined \Pit=131\a \Pit=117\way.""=""and my love he has purloined away.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\One \Pit=117\night \Pit=131\I \Pit=147\as \Pit=156\usual \Pit=147\went \Pit=131\to \Pit=98\her \Pit=131\home.""=""One night I as usual went to her home.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\Found \Pit=110\there \Pit=117\her \Pit=131\muth \Pit=147\er \Pit=131\and fauth \Pit=117\er \Pit=73\a \Pit=87\lone.""=""Found there her mother and father alone.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\I asked \Pit=117\for \Pit=131\my \Pit=147\dear \Pit=156\love \Pit=147\and soon \Pit=131\twas \Pit=98\made \Pit=131\known,""=""I asked for my dear love and soon 'twas made known,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=184\oh \Pit=110\hor \Pit=104\rors \Pit=110\she \Pit=147\just ran \Pit=131\a \Pit=117\way.""=""oh horror she just ran away.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=184\Packed her \Pit=131\bags \Pit=117\and \Pit=110\ee \Pit=117\loped \Pit=110\after \Pit=98\mid \Pit=73\night,""=""Packed her bags and eloped after midnight,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=184\and with \Pit=131\him \Pit=117\the \Pit=110\great \Pit=117\est \Pit=110\of \Pit=98\ease.""=""and with him the greatest of ease.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=110\\Spd=184\Up \Pit=117\from \Pit=131\two \Pit=117\stories \Pit=110\high \Pit=117\he \Pit=110\had \Pit=98\low \Pit=73\erd her \Pit=147\down\Pit=131\to \Pit=117\the \Pit=131\ground \Pit=117\with \Pit=110\his \Pit=117\fly \Pit=110\ing trap \Pit=98\eze.""=""Up from two stories high he had lowered her down to the ground with his flying trapeze.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\Oh \Pit=78\he \Pit=73\floats \Pit=87\through \Pit=117\the \Pit=147\air \Pit=156\with \Pit=147\the great \Pit=131\est \Pit=98\of\Pit=131\ease,""=""Oh he floats through the air with the greatest of ease,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\the dare \Pit=110\ing \Pit=117\young \Pit=131\man \Pit=147\on \Pit=131\the fly \Pit=117\ing \Pit=73\trap \Pit=87\eze.""=""the daring young man on the flying trapeze.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\His move \Pit=117\ments \Pit=131\are \Pit=147\grace \Pit=156\ful \Pit=147\all girls \Pit=131\he \Pit=98\does \Pit=131\please""=""His movements are graceful, all girls he does please""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=184\and \Pit=110\my \Pit=104\love \Pit=110\he has \Pit=147\purloined \Pit=131\a \Pit=117\way.""=""and my love he has purloined away.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\Some \Pit=117\months \Pit=131\af \Pit=147\ter \Pit=156\I \Pit=147\went in \Pit=131\to \Pit=98\a \Pit=131\hall.""=""Some months after I went into a hall.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\To \Pit=110\my \Pit=117\sur \Pit=131\prise \Pit=147\I \Pit=131\found there \Pit=117\on \Pit=73\a \Pit=87\wall.""=""To my surprise I found there on a wall.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\A bill \Pit=117\in \Pit=131\red \Pit=147\let \Pit=156\ters \Pit=147\which did \Pit=131\my \Pit=98\heart \Pit=131\gall""=""A bill in red letters which did my heart gall""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=184\that \Pit=110\she \Pit=104\was \Pit=110\a \Pit=147\pearing \Pit=131\with \Pit=117\him.""=""that she was appearing with him.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=184\He taught \Pit=131\her \Pit=117\gymnastics  \Pit=110\and\Pit=117\dressed \Pit=110\her \Pit=98\in \Pit=73\tights.""=""He taught her gymnastics and dressed her in tights,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=184\she would\Pit=131\help \Pit=117\him to \Pit=110\live \Pit=117\at \Pit=110\his \Pit=98\ease.""=""she would help him to live at his ease.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=110\\Spd=184\He \Pit=117\had \Pit=131\him \Pit=117\assume \Pit=110\a \Pit=117\more \Pit=110\mas \Pit=98\cu \Pit=73\lin \Pit=147\name\Pit=131\and \Pit=117\by \Pit=131\now \Pit=117\she \Pit=110\went \Pit=117\on \Pit=110\the trap \Pit=98\eze.""=""He had him assume a more masculine name and by now she went on the trapeze.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\Oh \Pit=78\she \Pit=73\floats \Pit=87\through \Pit=117\the \Pit=147\air \Pit=156\with \Pit=147\the great \Pit=131\est \Pit=98\of\Pit=131\ease,""=""Oh she floats through the air with the greatest of ease.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\You'd think \Pit=110\her \Pit=117\a \Pit=131\man \Pit=147\on \Pit=131\the fly \Pit=117\ing \Pit=73\trap \Pit=87\eze.""=""you'd think her man on the flying trapeze.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=87\\Spd=184\She does \Pit=117\all \Pit=131\his \Pit=147\hard \Pit=156\work \Pit=147\while he \Pit=131\takes \Pit=98\his \Pit=131\ease,""=""She does all his hard work while he takes his ease,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=117\\Spd=184\that \Pit=110\is \Pit=104\what \Pit=110\has be \Pit=147\come of \Pit=131\my \Pit=117\love.""=""that is what has become of my love.""\")
                Exit Select
            Case 18
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=262\\Spd=130\Lah lah lah \Pit=247\lah \Pit=220\lah \Pit=196\lah \Pit=165\lah \Pit=131\lah \Pit=147\lah \Pit=165\lah lah \Pit=175\lah \Pit=165\lah \Pit=147\lah \Pit=131\lah""=""""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Over the rih \Pit=165\ver \Pit=175\and \Pit=196\through the woods to \Pit=262\grandfather's \Pit=247\house \Pit=220\we \Pit=196\go.""=""Over the river and through the woods to grandfathers house we go.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\The \Pit=175\horse knows the way to \Pit=165\carry the sleigh through \Pit=147\the white and drift \Pit=165\ing \Pit=147\snow.""=""The horse knows the way to carry the sleigh through the white and drifting snow.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Over the rih \Pit=165\ver \Pit=175\and \Pit=196\through the woods oh \Pit=262\how the \Pit=247\wind \Pit=220\does \Pit=196\blow.""=""Over the river and through the woods oh how the wind does blow.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\It \Pit=262\stings the \Pit=247\nose \Pit=220\and \Pit=196\bites \Pit=165\the \Pit=131\toes \Pit=147\as \Pit=165\over \Pit=175\the \Pit=165\ground \Pit=147\we \Pit=131\go.""=""It stings the nose and bites the toes as over the ground we go.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Over the rih \Pit=165\ver \Pit=175\and \Pit=196\through the woods to \Pit=262\have a \Pit=247\first \Pit=220\rate \Pit=196\play.""=""Over the river and through the woods to have a first rate play.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Oh \Pit=175\hear the bells ring \Pit=165\ting a ling ling \Pit=147\Hurrah for Thanks \Pit=165\giving \Pit=147\day.""=""Oh hear the bells ring ting-a-ling-ling. Hurrah! for Thanksgiving day.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Over the rih \Pit=165\ver \Pit=175\and \Pit=196\through the woods trot \Pit=262\fast my \Pit=247\dap \Pit=220\pull \Pit=196\gray.""=""Over the river and through the woods trot fast my dapple gray.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Spring over\Pit=262\the \Pit=247\ground \Pit=220\like \Pit=196\a \Pit=165\hunting \Pit=131\hound \Pit=147\for \Pit=165\this \Pit=175\is \Pit=165\Thanks \Pit=147\giving \Pit=131\day.""=""Spring over the ground like a hunting hound for this is Thanksgiving day.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Over the rih \Pit=165\ver \Pit=175\and \Pit=196\through the woods and \Pit=262\straight through the\Pit=247\barn \Pit=220\yard \Pit=196\gate.""=""Over the river and through the woods and straight through the barnyard gate.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\We \Pit=175\seem to go \Pit=165\extremely slow \Pit=147\it is so hard \Pit=165\to \Pit=147\wait.""=""We seem to go extremely slow. It is so hard to wait.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Over the rih \Pit=165\ver \Pit=175\and \Pit=196\through the woods now \Pit=262\grandmother's \Pit=247\cap \Pit=220\I \Pit=196\spy.""=""Over the river and through the woods now grandmother's cap I spy.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=130\Hurrah \Pit=262\for the \Pit=247\fun \Pit=220\is \Pit=196\the \Pit=165\pudding \Pit=131\done \Pit=147\Hurrah \Pit=165\for \Pit=175\the \Pit=165\pump \Pit=147\kin \Pit=131\pie.""=""Hurrah! for the fun! Is the pudding done! Hurrah! for the pumpkin pie!""\")
                Exit Select
            Case 19
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=152\When \Pit=247\I was \Pit=196\young I \Pit=185\used \Pit=220\to wait, on pappy \Pit=185\and \Pit=220\hand him \Pit=196\his plate,""=""When I was young I used to wait on pappy and hand him his plate,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=152\and \Pit=247\pass the \Pit=196\bottle when \Pit=185\he \Pit=220\got dry, and brush a \Pit=185\way \Pit=220\the blue \Pit=196\tail fly,""=""and pass the bottle when he got dry, and brush away the blue-tail fly.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=152\Jimmy crack \Pit=196\corn and I \Pit=185\don't \Pit=220\care, Jimmy crack \Pit=185\corn and I \Pit=196\don't \Pit=247\care,""=""Jimmy crack corn and I don't care! Jimmy crack corn and I don't care!""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=152\Jimmy crack corn and I \Pit=262\don't \Pit=330\care, my \Pit=294\pappy's \Pit=262\gone \Pit=185\a \Pit=196\way.""=""Jimmy crack corn and I don't care, my pappy's gone away.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=152\And \Pit=247\when he'd \Pit=196\ride in \Pit=185\the \Pit=220\afternoon, I'd follow \Pit=185\after \Pit=220\with a \Pit=196\hickory broom.""=""And when he'd ride in the afternoon, I'd follow after with a hickory broom.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=152\The \Pit=247\pony \Pit=196\being \Pit=185\like \Pit=220\to shy, when bitten \Pit=185\by \Pit=220\the blue \Pit=196\tail fly,""=""The pony being like to shy when bitten by the blue-tail fly.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=152\Jimmy crack \Pit=196\corn and I \Pit=185\don't \Pit=220\care, Jimmy crack \Pit=185\corn and I \Pit=196\don't \Pit=247\care,""=""Jimmy crack corn and I don't care! Jimmy crack corn and I don't care!""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=152\Jimmy crack corn and I \Pit=262\don't \Pit=330\care, my \Pit=294\pappy's \Pit=262\gone \Pit=185\a \Pit=196\way.""=""Jimmy crack corn and I don't care, my pappy's gone away.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=152\One \Pit=247\day he'd \Pit=196\ride a \Pit=185\round \Pit=220\the farm, The flies so\Pit=185\num \Pit=220\erus they \Pit=196\did swarm.""=""One day he'd ride around the farm. The flies so numerous they did swarm.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=152\One \Pit=247\chanced to \Pit=196\bite him \Pit=185\on \Pit=220\the thigh, The devil \Pit=185\take \Pit=220\the blue \Pit=196\tail fly,""=""One chanced to bite him on the thigh. The devil take the blue-tail fly.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=152\Jimmy crack \Pit=196\corn and I \Pit=185\don't \Pit=220\care, Jimmy crack \Pit=185\corn and I \Pit=196\don't \Pit=247\care,""=""Jimmy crack corn and I don't care! Jimmy crack corn and I don't care!""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=152\Jimmy crack corn and I \Pit=262\don't \Pit=330\care, my \Pit=294\pappy's \Pit=262\gone \Pit=185\a \Pit=196\way.""=""Jimmy crack corn and I don't care, my pappy's gone away.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=152\The \Pit=247\pony \Pit=196\one he \Pit=185\jump \Pit=220\he pitch, He threw my\Pit=185\pap \Pit=220\ee in \Pit=196\the ditch.""=""The pony one he jump he pitch. He threw my pappy in the ditch.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=152\He's \Pit=247\gone and \Pit=196\the jury \Pit=185\won \Pit=220\dered why, The verdict \Pit=185\was \Pit=220\the blue \Pit=196\tail fly,""=""He's gone and the jury wondered why. The verdict was the blue-tail fly.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=152\Jimmy crack \Pit=196\corn and I \Pit=185\don't \Pit=220\care, Jimmy crack \Pit=185\corn and I \Pit=196\don't \Pit=247\care,""=""Jimmy crack corn and I don't care! Jimmy crack corn and I don't care!""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=152\Jimmy crack corn and I \Pit=262\don't \Pit=330\care, my \Pit=294\pappy's \Pit=262\gone \Pit=185\a \Pit=196\way.""=""Jimmy crack corn and I don't care, my pappy's gone away.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=152\They \Pit=247\laid him \Pit=196\under \Pit=185\a \Pit=220\simmon tree, His epih \Pit=185\taph \Pit=220\is there \Pit=196\to see.""=""They laid him under a simmon tree. His epitaph is there to see.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=196\\Spd=152\Be \Pit=247\neath this \Pit=196\stone I'm \Pit=185\forced \Pit=220\to lie, A victim \Pit=185\of \Pit=220\the blue \Pit=196\tail fly,""=""Beneath this stone I'm forced to lie, a victim of the blue-tail fly.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=152\Jimmy crack \Pit=196\corn and I \Pit=185\don't \Pit=220\care, Jimmy crack \Pit=185\corn and I \Pit=196\don't \Pit=247\care,""=""Jimmy crack corn and I don't care! Jimmy crack corn and I don't care!""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=247\\Spd=152\Jimmy crack corn and I \Pit=262\don't \Pit=330\care, my \Pit=294\pappy's \Pit=262\gone \Pit=185\a \Pit=196\way.""=""Jimmy crack corn and I don't care, my pappy's gone away.""\")
                Exit Select
            Case 20
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=82\\Spd=120\Sweet \Pit=92\est \Pit=110\lit \Pit=139\tle \Pit=165\fellow,""=""Sweetest little fellow,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=82\\Spd=120\Ev \Pit=92\ree \Pit=110\bud \Pit=139\ee \Pit=165\knows,""=""Everybody knows,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=165\\Spd=120\don't \Pit=185\know \Pit=165\what \Pit=139\to \Pit=123\\Pit=110\him \Pit=123\but \Pit=139\he's \Pit=123\mighty like a \Pit=165\rose""=""don't know what to him but he's mighty like a rose.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=82\\Spd=120\Lookin \Pit=92\at \Pit=110\his \Pit=139\mom \Pit=165\ee,""=""Lookin' at his mommy""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=82\\Spd=120\With \Pit=92\eyes \Pit=110\so \Pit=139\shiny \Pit=165\blue,""=""with eyes so shiny blue,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=165\\Spd=120\Makes \Pit=185\you \Pit=165\think \Pit=139\that \Pit=110\heaven \Pit=123\is \Pit=92\comin' \Pit=104\close \Pit=110\to you.""=""makes you think that heaven is comin' close to you.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=139\\Spd=120\When he's there a \Pit=147\sleepin, in \Pit=117\his \Pit=123\lit \Pit=131\tle \Pit=139\place,""=""When he's there a sleepin' in his little place,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=139\\Spd=120\think I see the \Pit=147\angels, look \Pit=117\ing \Pit=123\through \Pit=131\the \Pit=139\lace.""=""think I see the angels looking through his lace.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=139\\Spd=120\When \Pit=185\the \Pit=139\dark \Pit=185\is \Pit=165\fall \Pit=139\in \Pit=165\fall \Pit=139\in \Pit=139\when \Pit=110\the \Pit=139\shad \Pit=110\owes \Pit=139\creep.""=""When the dark is fallin', fallin', when the shadows creep,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=139\\Spd=120\then \Pit=185\they \Pit=139\come \Pit=185\on \Pit=165\tip \Pit=139\toe to kiss \Pit=110\him in \Pit=98\his sleep.""=""then they come on tip-toe to kiss him in his sleep.""\")

                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=82\\Spd=120\Sweet \Pit=92\est \Pit=110\lit \Pit=139\tle \Pit=165\fellow,""=""Sweetest little fellow,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=82\\Spd=120\Ev \Pit=92\ree \Pit=110\bud \Pit=139\ee \Pit=165\knows,""=""Everybody knows,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=165\\Spd=120\don't \Pit=185\know \Pit=165\what \Pit=139\to \Pit=123\\Pit=110\him \Pit=123\but \Pit=139\he's \Pit=123\mighty like a \Pit=165\rose""=""don't know what to him but he's mighty like a rose.""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=82\\Spd=120\Lookin \Pit=92\at \Pit=110\his \Pit=139\mom \Pit=165\ee,""=""Lookin' at his mommy""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=82\\Spd=120\With \Pit=92\eyes \Pit=110\so \Pit=139\shiny \Pit=165\blue,""=""with eyes so shiny blue,""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=165\\Spd=120\Makes \Pit=185\you \Pit=165\think \Pit=139\that \Pit=110\heaven \Pit=123\is \Pit=92\comin' \Pit=104\close \Pit=110\to you.""=""makes you think that heaven is comin' close to you.""\")
                Exit Select
            Case 21
                Caine.Speak("\Chr=""Monotone""\\Pit=139\\Spd=50\When \Pit=147\the \Pit=165\\Spd=130\light \Spd=100\is \Spd=50\running \Spd=130\low")
                Caine.Speak("\Chr=""Monotone""\\Pit=110\\Spd=50\and \Pit=139\\Spd=100\the \Pit=123\\Spd=130\shadows start \Pit=110\\Spd=50\to \Pit=123\\Spd=130\grow")
                Caine.Speak("\Chr=""Monotone""\\Pit=139\\Spd=50\and \Pit=123\\Spd=100\the \Pit=110\\Spd=130\places \Spd=70\that \Spd=100\you \Spd=130\know")
                Caine.Speak("\Chr=""Monotone""\\Pit=208\\Spd=100\seem \Pit=220\like\Map=""\Pit=208\\Spd=50\fan \Pit=165\tuh \Pit=139\see...""=""fantasy...""\")
                Caine.Speak("\Chr=""Monotone""\\Pit=139\\Spd=50\There's \Pit=50\a \Pit=165\\Spd=50\light \Spd=100\inside \Spd=100\your \Spd=130\soul")
                Caine.Speak("\Chr=""Monotone""\\Pit=110\\Spd=150\that's \Pit=139\\Spd=100\still \Pit=123\\Spd=100\shining in\Pit=110\\Spd=50\the \Pit=123\\Spd=130\cold")
                Caine.Speak("\Chr=""Monotone""\\Pit=139\\Spd=50\with \Pit=123\\Spd=100\the \Pit=110\\Spd=130\truth!")
                Caine.Speak("\Chr=""Monotone""\\Pit=82\\Spd=100\The\Map=""\Pit=139\prah \Pit=147\\Spd=150\miss""=""promise""\\Pit=123\\Spd=50\in \Pit=110\\Spd=150\our \Spd=50\hearts...")
                Caine.Speak("...")
                Caine.Speak("\Chr=""Monotone""\\Pit=139\Don't\Map=""\Pit=123\for \Pit=110\get!'""=""forget!""\")
                Caine.Speak("\Chr=""Monotone""\\Pit=82\\Spd=100\I'm \Pit=139\\Spd=50\with \Pit=147\\Spd=100\you \Pit=123\\Spd=50\in \Pit=110\the dark...!")
                Exit Select
            Case 22
                Caine.Speak("\Chr=""Monotone""\\Pit=220\\Spd=50\Of \Pit=175\all \Pit=147\\Spd=130\the \Map=""muh \Pit=131\\Spd=100\nee""=""money""\ \Pit=147\\Spd=130\that \Pit=175\\Spd=100\ehr \Pit=196\I \Spd=130\had,")
                Caine.Speak("\Chr=""Monotone""\\Pit=196\\Spd=100\I \Pit=220\\Spd=50\spend \Pit=247\\Spd=130\it \Pit=220\\Spd=50\in \Pit=175\good \Map=""\Pit=220\\Spd=100\com \Pit=131\\Spd=130\puh nee!""=""company!""\")
                Caine.Speak("\Chr=""Monotone""\\Pit=220\\Spd=50\And\Pit=175\all \Pit=147\\Spd=130\the \Spd=100\harm \Pit=147\\Spd=130\I've \Map=""\Pit=175\\Spd=100\eh \Pit=196\ver""=""ever""\ \Spd=130\done,")
                Caine.Speak("\Chr=""Monotone""\\Pit=196\\Map=""\Pit=196\\Spd=100\Uh \Pit=220\\Spd=50\lass""=""Alas""\ \Pit=294\it \Pit=262\was \Pit=196\to \Pit=175\none \Pit=147\but me!")
                Caine.Speak("\Chr=""Monotone""\\Pit=262\\Spd=100\And \Spd=50\all \Pit=294\I've \Pit=262\\Spd=100\done,")
                Caine.Speak("\Chr=""Monotone""\\Pit=262\\Spd=100\For \Spd=50\want \Pit=294\of \Pit=262\\Spd=100\wit,")
                Caine.Speak("\Chr=""Monotone""\\Pit=220\\Spd=100\To \Map=""\Pit=233\\Spd=50\mem \Pit=220\\Spd=100\ree""=""memory""\ \Pit=196\\Spd=50\now \Pit=175\\Spd=100\I \Pit=220\\Spd=50\can't \Pit=131\recall!")
                Caine.Speak("\Chr=""Monotone""\\Pit=220\\Spd=50\So\Pit=175\fill\Pit=147\\Spd=100\to \Spd=50\me \Pit=147\\Spd=50\the \Map=""\Pit=175\\Spd=100\par \Pit=196\ting""=""parting""\ \Spd=50\glass!")
                Caine.Speak("\Chr=""Monotone""\\Pit=196\\Spd=100\Good \Pit=220\\Spd=50\night \Pit=294\and \Pit=262\joy \Pit=196\be \Pit=175\to \Pit=147\you all!")
                Exit Select
            Case 23
                Caine.Speak("\Chr=""Monotone""\\Pit=220\\Spd=130\doo \Pit=147\duh \Pit=196\\Spd=60\doo \Pit=175\\Spd=200\doo, \Pit=131\\Spd=130\doo \Pit=147\duh \Pit=220\doo \Pit=147\duh \Pit=196\\Spd=60\doo \Pit=175\\Spd=130\doo!")
                Caine.Speak("\Chr=""Monotone""\\Pit=220\\Spd=130\doo \Pit=147\duh \Pit=196\\Spd=60\doo \Pit=175\\Spd=200\doo, \Pit=131\\Spd=130\doo \Pit=147\duh \Pit=220\doo \Pit=147\duh \Pit=196\\Spd=60\doo \Pit=175\\Spd=130\doo, \Pit=131\\Spd=120\I'm \map=""\Pit=262\\Spd=50\sii\Pit=220\\Spd=120\nin""=""singing""\ \Pit=196\\Spd=120\in\Pit=175\\Spd=120\the\Pit=147\\Spd=58\ rain,")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\just \map=""\Pit=175\\Spd=63\sii\Pit=175\\Spd=120\nin""=""singing""\ \Pit=196\\Spd=120\in\Pit=220\\Spd=120\the\Pit=262\\Spd=65\ rain.")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\What\Pit=147\\Spd=120\ a \map=""\Pit=175\\Spd=51\glor\Pit=196\\Spd=120\e\Pit=220\\Spd=120\us""=""glorious""\ \Pit=262\\Spd=67\ \map=""feel\Pit=220\\Spd=120\ing,""=""feeling,""\ \Pit=262\\Spd=120\I'm \map=""\Pit=262\\Spd=52\hap\Pit=220\\Spd=120\pee""=""happy""\ \map=""\Pit=196\\Spd=120\ a\Pit=147\\Spd=66\gain.""=""again.""\")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\I'm  \map=""\Pit=262\\Spd=51\laugh\Pit=220\\Spd=120\ing""=""laughing""\\Pit=196\\Spd=120\ at\Pit=147\\Spd=52\ clouds")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\so\Pit=262\\Spd=67\ dark\Pit=220\\Spd=82\ up \map=""\Pit=196\\Spd=120\uh\Pit=147\\Spd=61\buff.""=""above.""\")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\The\Pit=262\\Spd=50\ sun's\Pit=220\\Spd=120\in\Pit=147\\Spd=120\ my\Pit=165\\Spd=57\ heart, \Pit=220\\Spd=120\and\Pit=147\\Spd=120\ I'm \map=""\Pit=175\\Spd=50\reh\Pit=147\\Spd=120\dee""=""ready""\ \Pit=175\\Spd=120\ for\Pit=147\\Spd=55\ love.")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\Let\Pit=147\\Spd=120\the \map=""\Pit=175\\Spd=51\storm\Pit=196\\Spd=120\e""=""stormy""\ \Pit=220\\Spd=120\ clouds\Pit=262\\Spd=59\ chase")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=120\ev\Pit=147\\Spd=120\ree\Pit=175\\Spd=120\one""=""everyone""\ \Pit=196\\Spd=120\from\Pit=220\\Spd=120\the\Pit=262\\Spd=52\ place.")
                Caine.Speak("\Chr=""Monotone""\\Pit=175\\Spd=120\Come\Pit=175\\Spd=120\ on\Pit=196\\Spd=120\ with\Pit=220\\Spd=120\the\Pit=262\\Spd=67\ rain, \Pit=220\\Spd=120\I have\Pit=262\\Spd=120\ a\Pit=262\\Spd=50\ smile\Pit=220\\Spd=120\ on\Pit=196\\Spd=120\ my\Pit=147\\Spd=68\ face.")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\I'll\Pit=262\\Spd=70\ walk\Pit=220\\Spd=80\ down\Pit=196\\Spd=120\the\Pit=147\\Spd=120\ lane,\Pit=131\\Spd=120\ with\Pit=131\\Spd=120\ a\Pit=262\\Spd=50\ \map=""hap\Pit=220\\Spd=120\pee""=""happy""\ \map=""\Pit=196\\Spd=120\re \Pit=147\\Spd=52\frain,""=""refrain,""\")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\and \map=""\Pit=262\\Spd=120\singing,""=""singin',""\  \Pit=131\\Spd=120\just \map=""\Pit=165\\Spd=120\singing,""=""singin'""\ \Pit=220\\Spd=104\in\Pit=131\\Spd=120\the\Pit=175\\Spd=52\ rain.")
                Caine.Speak("\Chr=""Monotone""\\Pit=175\\Spd=88\Why\Pit=156\\Spd=120\ am\Pit=139\\Spd=120\I \map=""\Pit=139\\Spd=120\ smile\Pit=156\\Spd=69\ing,""=""smiling,""\ \Pit=175\\Spd=120\and\Pit=175\\Spd=120\ why\Pit=147\\Spd=120\ do\Pit=175\\Spd=120\ I\Pit=147\\Spd=52\ \map=""sing,""=""sing?""\ \Pit=175\\Spd=68\Why\Pit=156\\Spd=120\ does \map=""\Pit=139\\Spd=120\De\Pit=139\\Spd=120\cem\Pit=156\\Spd=120\ber""=""December""\ \Pit=175\\Spd=120\seem \map=""\Pit=175\\Spd=120\sun\Pit=147\\Spd=120\e""=""sunny""\ \Pit=175\\Spd=120\as\Pit=147\\Spd=52\ spring?")
                Caine.Speak("\Chr=""Monotone""\\Pit=262\\Spd=120\Why\Pit=220\\Spd=120\ do\Pit=196\\Spd=120\ I\Pit=196\\Spd=120\ get\Pit=220\\Spd=120\ up\Pit=262\\Spd=120\ each \map=""\Pit=294\\Spd=120\mor\Pit=220\\Spd=120\ning""=""morning""\ \Pit=220\\Spd=120\to \Pit=220\\Spd=52\start, \Map=""\Pit=294\\Spd=68\hap\Pit=247\\Spd=120\pee""=""happy""\ \Pit=220\\Spd=120\and\Pit=220\\Spd=120\ het\Pit=247\\Spd=69\ up\Pit=262\\Spd=120\ with\Pit=294\\Spd=120\ joy\Pit=196\\Spd=120\in\Pit=196\\Spd=120\ my\Pit=196\\Spd=52\ heart?")
                Caine.Speak("\Chr=""Monotone""\\Pit=233\\Spd=67\Why\Pit=208\\Spd=120\ is\Pit=185\\Spd=120\ each\Pit=185\\Spd=120\ new\Pit=208\\Spd=68\ task\Pit=233\\Spd=120\ a\Pit=233\\Spd=120\ \map=""tri\Pit=175\\Spd=120\full""=""trifle""\ \Pit=175\\Spd=120\to \map=""\Pit=175\\Spd=52\do,""=""do?""\ \map=""\Pit=175\\Spd=120\Be\Pit=175\\Spd=68\cause""=""Because""\ \Pit=156\\Spd=120\I\Pit=139\\Spd=120\ am \map=""\Pit=139\\Spd=120\liv\Pit=156\\Spd=120\ing""=""living""\ \Pit=175\\Spd=120\a\Pit=208\\Spd=120\ life\Pit=175\\Spd=120\ full\Pit=208\\Spd=120\ of\Pit=262\\Spd=52\ you.")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\I'm \map=""\Pit=262\\Spd=50\sii\Pit=220\\Spd=120\nin""=""singing""\ \Pit=196\\Spd=120\in\Pit=175\\Spd=120\the\Pit=147\\Spd=58\ rain,")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\just \map=""\Pit=175\\Spd=63\sii\Pit=175\\Spd=120\nin""=""singing""\ \Pit=196\\Spd=120\in\Pit=220\\Spd=120\the\Pit=262\\Spd=65\ rain.")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\What\Pit=147\\Spd=120\ a \map=""\Pit=175\\Spd=51\glor\Pit=196\\Spd=120\e\Pit=220\\Spd=120\us""=""glorious""\ \Pit=262\\Spd=67\ \map=""feel\Pit=220\\Spd=120\ing,""=""feeling,""\ \Pit=262\\Spd=120\I'm \map=""\Pit=262\\Spd=52\hap\Pit=220\\Spd=120\pee""=""happy""\ \map=""\Pit=196\\Spd=120\ a\Pit=147\\Spd=66\gain.""=""again.""\")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\I'm  \map=""\Pit=262\\Spd=51\laugh\Pit=220\\Spd=120\ing""=""laughing""\\Pit=196\\Spd=120\ at\Pit=147\\Spd=52\ clouds")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\so\Pit=262\\Spd=67\ dark\Pit=220\\Spd=82\ up \map=""\Pit=196\\Spd=120\uh\Pit=147\\Spd=61\buff.""=""above.""\")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\The\Pit=262\\Spd=50\ sun's\Pit=220\\Spd=120\in\Pit=147\\Spd=120\ my\Pit=165\\Spd=57\ heart, \Pit=220\\Spd=120\and\Pit=147\\Spd=120\ I'm \map=""\Pit=175\\Spd=50\reh\Pit=147\\Spd=120\dee""=""ready""\ \Pit=175\\Spd=120\ for\Pit=147\\Spd=55\ love.")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\Let\Pit=147\\Spd=120\the \map=""\Pit=175\\Spd=51\storm\Pit=196\\Spd=120\e""=""stormy""\ \Pit=220\\Spd=120\ clouds\Pit=262\\Spd=59\ chase")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=131\\Spd=120\ev\Pit=147\\Spd=120\ree\Pit=175\\Spd=120\one""=""everyone""\ \Pit=196\\Spd=120\from\Pit=220\\Spd=120\the\Pit=262\\Spd=52\ place.")
                Caine.Speak("\Chr=""Monotone""\\Pit=175\\Spd=120\Come\Pit=175\\Spd=120\ on\Pit=196\\Spd=120\ with\Pit=220\\Spd=120\the\Pit=262\\Spd=67\ rain, \Pit=220\\Spd=120\I have\Pit=262\\Spd=120\ a\Pit=262\\Spd=50\ smile\Pit=220\\Spd=120\ on\Pit=196\\Spd=120\ my\Pit=147\\Spd=68\ face.")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\I'll\Pit=262\\Spd=70\ walk\Pit=220\\Spd=80\ down\Pit=196\\Spd=120\the\Pit=147\\Spd=120\ lane,\Pit=131\\Spd=120\ with\Pit=131\\Spd=120\ a\Pit=262\\Spd=50\ \map=""hap\Pit=220\\Spd=120\pee""=""happy""\ \map=""\Pit=196\\Spd=120\re \Pit=147\\Spd=52\frain,""=""refrain,""\")
                Caine.Speak("\Chr=""Monotone""\\Pit=131\\Spd=120\and \map=""\Pit=262\\Spd=120\singing,""=""singin',""\  \Pit=131\\Spd=120\just \map=""\Pit=165\\Spd=120\singing,""=""singin'""\ \Pit=220\\Spd=104\in\Pit=131\\Spd=120\the\Pit=175\\Spd=52\ rain.")
                Exit Select
            Case 24
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=139\\Spd=200\lah \Pit=147\lah \Pit=156\lah""=""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=233\\Spd=130\lah \Pit=196\lah lah \Pit=175\lah \Pit=156\lah!""=""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=233\\Spd=130\lah \Pit=196\lah lah \Pit=175\\Spd=200\lah lah \Pit=196\\Spd=130\lah!""=""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=233\\Spd=130\lah \Pit=196\lah lah \Pit=175\lah \Pit=156\lah \Pit=131\lah \Pit=156\lah \Pit=175\lah \Pit=233\lah \Pit=117\lah \Pit=156\lah \Pit=175\lah \Pit=156\lah!""=""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=233\\Spd=130\lah \Pit=196\lah lah \Pit=175\lah \Pit=156\lah!""=""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=233\\Spd=130\lah \Pit=196\lah lah \Pit=175\\Spd=200\lah lah \Pit=196\\Spd=130\lah!""=""""\")
                Caine.Speak("\Chr=""Monotone""\\Chr=""Monotone""\\Map=""\Pit=233\\Spd=130\lah \Pit=196\lah lah \Pit=175\lah \Pit=156\lah \Pit=131\lah \Pit=156\lah \Pit=175\lah \Pit=233\lah \Pit=117\lah \Pit=156\lah \Pit=175\lah""=""""\\Spd=130\\Map=""\Pit=196\Gang \Pit=156\gle,""=""Gangle,""\ \Pit=110\then \Map=""\Pit=196\Zoo \Pit=156\bull,""=""Zooble,""\ \Pit=110\and \Spd=100\\Map=""\Pit=156\King \Pit=131\er""=""Kinger""\ \Pit=110\\Spd=130\too.")
                Caine.Speak("\Chr=""Monotone""\\Spd=130\\Map=""\Pit=196\Ra \Pit=156\guh \Pit=110\fuh""=""Ragatha,""\ \Pit=196\Jax \Pit=156\and \Pit=110\there's \Pit=131\Kauf \Spd=100\mo, \Pit=147\woo \Pit=175\\Spd=130\hoo!")
                Caine.Speak("\Chr=""Monotone""\\Pit=196\\Spd=130\Day \Map=""\Pit=156\af \Pit=110\tur""=""after""\ \Pit=196\day \Map=""\Pit=156\af \Pit=110\tur""=""after""\ \Pit=156\\Spd=100\day \Pit=131\we \Pit=110\\Spd=130\fly")
                Caine.Speak("\Chr=""Monotone""\\Pit=110\\Spd=130\Past \Pit=131\the \Pit=196\moon, \Pit=156\and \Pit=110\the \Pit=196\sun, \Pit=175\and \Pit=156\we \Pit=175\\Spd=100\don't know \Pit=185\\Spd=130\why!")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=139\\Spd=200\lah \Pit=147\lah \Pit=156\lah""=""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=233\\Spd=130\lah \Pit=196\lah lah \Pit=175\lah \Pit=156\lah!""=""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=233\\Spd=130\lah \Pit=196\lah lah \Pit=175\\Spd=200\lah lah \Pit=196\\Spd=130\lah!""=""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=233\\Spd=130\lah \Pit=196\lah lah \Pit=175\lah \Pit=156\lah \Pit=131\lah \Pit=156\lah \Pit=175\lah \Pit=233\lah \Pit=117\lah \Pit=156\lah \Pit=175\lah \Pit=156\lah!""=""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=233\\Spd=130\lah \Pit=196\lah lah \Pit=175\lah \Pit=156\lah!""=""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=233\\Spd=130\lah \Pit=196\lah lah \Pit=175\\Spd=200\lah lah \Pit=196\\Spd=130\lah!""=""""\")
                Caine.Speak("\Chr=""Monotone""\\Map=""\Pit=233\\Spd=130\lah \Pit=196\lah lah \Pit=175\lah \Pit=156\lah \Pit=131\lah \Pit=156\lah \Pit=175\lah \Pit=233\lah \Pit=117\lah \Pit=156\lah \Pit=175\lah \Pit=156\lah!""=""""\")
                Exit Select
            Case 25
                Caine.Speak("\Chr=""Monotone""\\Pit=196\\Spd=130\There \Pit=131\once was a ship \Pit=156\that \Pit=196\put to sea, the \Pit=208\name \Pit=175\of the ship \Pit=208\was the \Pit=262\Billy \Pit=196\O' Tea")
                Caine.Speak("\Chr=""Monotone""\\Pit=196\\Spd=130\The \Pit=131\winds blew up \Pit=156\her \Pit=196\bow dipped down, blow \Pit=175\my \Pit=156\bully \Pit=147\boys \Pit=131\blow!")
                Caine.Speak("\Chr=""Monotone""\\Pit=262\\Spd=130\Soon may \Pit=208\the \map=""\Pit=233\Weller \Pit=196\man""=""Wellerman""\ come to \Pit=208\bring \Pit=175\us sugar \Pit=208\and \Pit=196\tea \Pit=156\and \Pit=131\rum,")
                Caine.Speak("\Chr=""Monotone""\\Pit=262\\Spd=130\One day \Pit=208\when the \Pit=233\tonguing \Pit=196\is done we'll take \Pit=175\our \Pit=156\leave \Pit=147\and \Pit=131\go!")
                Caine.Speak("\Chr=""Monotone""\\Pit=196\\Spd=130\As \Pit=131\far as I've heard \Pit=156\the \Pit=196\fight is on, the \Pit=208\line's \Pit=175\not cut \Pit=208\and the \Pit=262\whale's \Pit=196\not gone")
                Caine.Speak("\Chr=""Monotone""\\Pit=196\\Spd=130\The \Pit=131\Wellarman makes \Pit=156\his \Pit=196\regular call to encourage\Pit=175\the \Pit=156\captain \Pit=147\crew and \Pit=131\all!")
                Caine.Speak("\Chr=""Monotone""\\Pit=262\\Spd=130\Soon may \Pit=208\the \map=""\Pit=233\Weller \Pit=196\man""=""Wellerman""\ come to \Pit=208\bring \Pit=175\us sugar \Pit=208\and \Pit=196\tea \Pit=156\and \Pit=131\rum,")
                Caine.Speak("\Chr=""Monotone""\\Pit=262\\Spd=130\One day \Pit=208\when the \Pit=233\tonguing \Pit=196\is done we'll take \Pit=175\our \Pit=156\leave \Pit=147\and \Pit=131\go!")
                Caine.Speak("\Chr=""Monotone""\\Pit=262\\Spd=130\Soon may \Pit=208\the \map=""\Pit=233\Weller \Pit=196\man""=""Wellerman""\ come to \Pit=208\bring \Pit=175\us sugar \Pit=208\and \Pit=196\tea \Pit=156\and \Pit=131\rum,")
                Caine.Speak("\Chr=""Monotone""\\Pit=262\\Spd=130\One day \Pit=208\when the \Pit=233\tonguing \Pit=196\is done we'll \Spd=80\take \Pit=175\our \Pit=156\leave \Pit=147\and \Pit=131\go!")
                Exit Select
        End Select

        Caine.Play("Acknowledge")
        Caine.Speak("\Chr=""Normal""\")
        Caine.Balloon.Style = &H21C000F
    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        UtilPanel2.Hide()
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        ApplyForm.SelectedDate = MonthCalendar1.SelectionRange.Start()
        ApplyForm.Show()
    End Sub

    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click
        UtilPanel1.Hide()
        UtilPanel2.Show()
        UtilPanel3.Hide()
    End Sub

    Private Sub EventTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EventTimer.Tick
        If File.Exists(EventXML) Then
            XmlCtrl.Load(EventXML)
            Dim eventNode As XmlNode = XmlCtrl.SelectSingleNode("/Events/Event[RemindDate='" & Date.Now.ToShortDateString & "'][RemindAMPM='" & Date.Now.ToString("tt") & "'][Reminder='True']")

            If Not eventNode Is Nothing AndAlso IsRemindHour(eventNode("RemindTime").InnerText) Then
                Dim Rnd As New Random

                Select Case Rnd.Next(1, 5)
                    Case 1
                        Caine.Speak("Don't forget!")
                        Exit Select
                    Case 2
                        Caine.Speak("Hey buddy, just to let you know,")
                        Exit Select
                    Case 3
                        Caine.Speak("Wow! I \emp\completely forgot!")
                        Exit Select
                    Case 4
                        Caine.Speak(My.Settings.Name & ", did you remember?")
                        Exit Select
                End Select

                eventNode("Reminder").InnerText = False
                XmlCtrl.Save(EventXML)

                Caine.Speak(eventNode("EventDesc").InnerText.ToString & " is scheduled for, or due on " & eventNode("EventDate").InnerText & " at " & eventNode("EventTime").InnerText & " " & eventNode("EventAMPM").InnerText & ".")
                MessageBox.Show("Hey, " & My.Settings.Name & ", it's " & Date.Now.ToShortTimeString() & ". You asked me to remind you about '" & eventNode("EventDesc").InnerText & "'", "Circus Pals")
            End If
        End If
    End Sub

    Private Function IsRemindHour(ByVal RemindTime As String)
        Try
            If Convert.ToDateTime(RemindTime & DateTime.Now.ToString("tt")) <= DateTime.Now Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
