
Imports System.Timers

Public Class NameForm

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        Dim num As Integer = MyBase.Location.X - 20
        Dim num2 As Integer = MyBase.Location.Y - 120
        Form1.Caine.MoveTo(CShort(num), CShort(num2))
    End Sub

    Private Delegate Sub CloseFormCallback()

    Private Sub CloseForm()

    End Sub

    Private Sub OnTimedEvent(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        '  CloseForm()
    End Sub

    Private Sub NameForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If TextBox1.Text = "" Then
            My.Settings.Name = Environment.UserName
            Form1.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
            My.Settings.Save()
            Form1.Caine.Play("Confused")
            Form1.Caine.Speak("\Vol=65535\Oh okay, I guess I'll take your name from your PC 3, 2, 1.")
            Form1.Caine.Play("Acknowledge")
            Form1.Caine.Speak("\Vol=65535\" + My.Settings.Name + "! An \emp\interesting choice.|\Vol=65535\Nice to meet you, " + My.Settings.Name + "!")
            Form1.Caine.Play("Explain")
            Form1.Caine.Speak("\Vol=65535\Since this is the first time we have met, I'd like to tell you a little about myself.")
            Form1.Caine.Play("Announce")
            Form1.Caine.Speak("\Vol=65535\I am your friend, ringmaster and part of the Circus Pals lineup! I have the ability to learn from and entertain you. The more we browse, search, and travel the internet together, the smarter I'll become!")
            Form1.Caine.Play("Silly")
            Form1.Caine.Speak("\Vol=65535\Well, not that I'm not \emp\ already smart.")
            Form1.Caine.Play("Explain")
            Form1.Caine.Speak("\Vol=65535\Because the Internet can feel like a circus at times, I can help you find what you are looking for and even make suggestions as to where we should go to find it! The more time we spend together, the closer we'll become!")
            Form1.Caine.Play("Sad")
            Form1.Caine.Speak("\Vol=65535\I may be one of the smallest friends you have " + My.Settings.Name + ", but I will always try and make up for that with my big heart!")
            Form1.Caine.Play("DoMagic1")
            Form1.Caine.Speak("\Vol=65535\When you feel bored and feel like going on an adventure, I have a couple games preinstalled and ready to go. Adventures \emp\always keep you sane!")
            Form1.Caine.Play("DoMagic2")
            Form1.Caine.Play("Think")
            Form1.Caine.Speak("\Vol=65535\Am I rambling?")
            Form1.Caine.Play("Acknowledge")
            Form1.Caine.Speak("\Vol=65535\\emp\Alrighty then " + My.Settings.Name + ", feel free to look around.")
            Form1.Caine.MoveTo(320, 240)
        Else
            My.Settings.Save()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        My.Settings.Name = TextBox1.Text
        Form1.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
        My.Settings.Save()
        Me.Close()
        If My.Settings.Name = "Pomni" Or My.Settings.Name = "Gangle" Or My.Settings.Name = "Zooble" Then
            My.Settings.Name = Environment.UserName
            Me.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
            My.Settings.Save()
            Form1.Caine.Play("Decline")
            Form1.Caine.Speak("\Vol=65535\Sorry, but that name is already taken.")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name = "Kinger" Or My.Settings.Name = "Ragatha" Or My.Settings.Name = "Jax" Then
            My.Settings.Name = Environment.UserName
            Me.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
            My.Settings.Save()
            Form1.Caine.Play("Decline")
            Form1.Caine.Speak("\Vol=65535\Sorry, but that name is already taken.")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name = "Kaumfo" Then
            My.Settings.Name = Environment.UserName
            Me.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
            My.Settings.Save()
            Form1.Caine.Play("Decline")
            Form1.Caine.Speak("\Vol=65535\Sorry, but that name is already taken.")
            Form1.Caine.Play("Silly")
            Form1.Caine.Speak("\Vol=65535\You think just because he's gone means his name is for sale?")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name = "Queenie" Then
            My.Settings.Name = Environment.UserName
            Me.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
            My.Settings.Save()
            Form1.Caine.Play("Decline")
            Form1.Caine.Speak("\Vol=65535\Sorry, but that name is already taken.")
            Form1.Caine.Play("Silly")
            Form1.Caine.Speak("\Vol=65535\You think just because she's gone means her name is for sale?")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name = "XDDCC" Then
            Me.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
            My.Settings.Save()
            Form1.Caine.Play("Think")
            Form1.Caine.Speak("\Vol=65535\Pretty odd that you would like to have that name!")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Dylan") Or My.Settings.Name.Contains("dylan") Or My.Settings.Name.Contains("Breather") Or My.Settings.Name.Contains("breather") Then
            Form1.TimerOfDOOM.Start()
            Form1.Caine.StopAll()
            Form1.Caine.Play("RestPose")
            Form1.Caine.Speak("\Vol=65535\" + My.Settings.Name + "? Not so fast! Activating Self-Defense Mechanism!")
            Form1.Caine.Play("Giggle")
            Form1.Caine.Play("Decline")
            Form1.Caine.Speak("\Vol=65535\Don't worry, I didn't do anything stupid this time.")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("pager") Then
            Form1.Caine.Play("Think")
            Form1.Caine.Speak("\Vol=65535\" + My.Settings.Name + "? As in the real Pager? Odd to see you interested.")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("expand dong") Or My.Settings.Name.Contains("Expand Dong") Then
            My.Settings.Name = Environment.UserName
            Me.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
            My.Settings.Save()
            Form1.Caine.Play("Mad")
            Form1.Caine.Speak("\Vol=65535\Expand \emp\What?! I shall call you \emp\no such thing! I will refer to you as " + My.Settings.Name + " from now on!")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("gond dnapxe") Or My.Settings.Name.Contains("gonD dnapxE") Then
            My.Settings.Name = Environment.UserName
            Me.Text = "Welcome to The Amazing Digital Circus, " + My.Settings.Name + "!"
            My.Settings.Save()
            Form1.Caine.Play("Giggle")
            Form1.Caine.Speak("\Vol=65535\Nice try " + My.Settings.Name + ", you really thought you could outsmart me there? I will spell it backwards to see what it says.")
            Form1.Caine.Play("Read")
            Form1.Caine.Play("ReadReturn")
            Form1.Caine.Play("Mad")
            Form1.Caine.Speak("\Vol=65535\Expand \emp\What?! I shall call you \emp\no such thing! I will refer to you as " + My.Settings.Name + " from now on!")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Vinesauce") Or My.Settings.Name.Contains("Joel") Then
            Form1.Caine.Play("Surprised")
            Form1.Caine.Speak("\Vol=65535\Is it really you, " + My.Settings.Name + "? I can't believe it, the legend is true!")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Mario") Or My.Settings.Name.Contains("mario") Then
            Form1.Caine.Play("Think")
            Form1.Caine.Speak("\Vol=65535\Why do I have a feeling this program might get a cease and desist?")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Oh whatever.")
        End If
        If My.Settings.Name.Contains("Luigi") Or My.Settings.Name.Contains("luigi") Then
            Form1.Caine.Play("Confused")
            Form1.Caine.Speak("\Vol=65535\Um... Can we talk about this..?")
            Form1.Caine.Play("Mad")
            Form1.Caine.Speak("\Vol=65535\Oh forget it!")
        End If
        If My.Settings.Name.Contains("Sonic") Or My.Settings.Name.Contains("sonic") Then
            Form1.Caine.Play("Think")
            Form1.Caine.Speak("\Vol=65535\Surely this one cannot be given a cease and desist unlike the other one.")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Oh whatever.")
        End If
        If My.Settings.Name.Equals("Big") Or My.Settings.Name.Equals("big") Then
            Form1.Caine.Play("Surprised")
            Form1.Caine.Height = 256
            Form1.Caine.Width = 256
            Form1.Caine.Speak("\Vol=65535\\Pit=85\Oh no what have you done! Good thing this is temporary! \Pit=165\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Equals("Small") Or My.Settings.Name.Equals("small") Then
            Form1.Caine.Play("Surprised")
            Form1.Caine.Height = 64
            Form1.Caine.Width = 64
            Form1.Caine.Speak("\Vol=65535\\Pit=375\Oh no what have you done! Good thing this is temporary! \Pit=165\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Equals("Gargantuan") Or My.Settings.Name.Equals("gargantuan") Then
            Form1.Caine.Play("Surprised")
            Form1.Caine.Height = 384
            Form1.Caine.Width = 384
            Form1.Caine.Speak("\Vol=65535\\Pit=50\Oh no what have you done! Good thing this is temporary! \Pit=165\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Micro") Or My.Settings.Name.Contains("micro") Then
            Form1.Caine.Play("Surprised")
            Form1.Caine.Height = 32
            Form1.Caine.Width = 32
            Form1.Caine.Speak("\Vol=65535\\Pit=400\Oh no what have you done! Good thing this is temporary! \Pit=165\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Crazy Dave") Or My.Settings.Name.Contains("crazy dave") Then
            Form1.Caine.Play("Think")
            Form1.Caine.Speak("\Vol=65535\You're like Kinger, right? You came here because there are zombies in the Mildenhall Manor?")
            Form1.Caine.Play("Explain")
            Form1.Caine.Speak("\Vol=65535\We'll talk later after this, OK?")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Clippy") Or My.Settings.Name.Contains("Clippit") Then
            Form1.Caine.Play("Alert")
            Form1.Caine.Speak("\Vol=65535\" + My.Settings.Name + "! Here's a message from your browser! \Chr=""Whisper""\Seizure warning!")
            Form1.Caine.Play("Giggle")
            Try
                Dim idiot As String = "http://piv.pivpiv.dk/"
                Process.Start(idiot)
            Catch ex As Exception
                Form1.Caine.Speak("\Chr=""Normal""\There was an error executing the Easter egg command.")
            End Try
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Chr=""Normal""\\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Bonzi") Or My.Settings.Name.Contains("bonzi") Then
            Form1.Caine.Play("Alert")
            Form1.Caine.Speak("\Vol=65535\" + My.Settings.Name + "?")
            Form1.Caine.Play("Mad")
            Form1.Caine.Speak("\Vol=65535\You're that purple monkey! We don't allow viruses to use this program, instead stare at this site!")
            Try
                Dim tk As String = "https://bonzibuddy.tk/"
                Process.Start(tk)
            Catch ex As Exception
                Form1.Caine.Speak("\Chr=""Normal""\There was an error executing the Easter egg command.")
            End Try
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Chr=""Normal""\\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Kinito") Or My.Settings.Name.Contains("kinito") Or My.Settings.Name.Contains("KinitoPET") Or My.Settings.Name.Contains("kinitooet") Then
            Form1.Caine.Play("Alert")
            Form1.Caine.Speak("\Vol=65535\" + My.Settings.Name + "?")
            Form1.Caine.Play("Mad")
            Form1.Caine.Speak("\Vol=65535\You're that pink axolotl! We don't allow viruses to use this program, instead stare at this site!")
            Try
                Dim tk As String = "https://kinitoafterstory.nekoweb.org/tk"
                Process.Start(tk)
            Catch ex As Exception
                Form1.Caine.Speak("\Chr=""Normal""\There was an error executing the Easter egg command.")
            End Try
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Chr=""Normal""\\Vol=65535\Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("MaxALERT") Or My.Settings.Name.Contains("maxalert") Or My.Settings.Name.Contains("Max") Or My.Settings.Name.Contains("max") Then
            Form1.Caine.Play("Alert")
            Form1.Caine.Speak("\Vol=65535\" + My.Settings.Name + "?")
            Form1.Caine.Play("Decline")
            Form1.Caine.Speak("\Vol=65535\Well you look a \emp\little suspicious.")
            Form1.Caine.Play("Blink")
            Form1.Caine.Speak("\Vol=65535\Eh, I'll let it pass for now. Alright, back to my regular script!")
        End If
        If My.Settings.Name.Contains("Blob") Or My.Settings.Name.Contains("blob") Then
            Form1.Caine.Play("Alert")
            Form1.Caine.Speak("\Vol=65535\" + My.Settings.Name + "?")
            Form1.Caine.Play("Giggle")
            Form1.Caine.Speak("\Vol=65535\Jax would \emp\definitely love to know you! Alright, back to my regular script!")
            Form1.Caine.Play("Blink")
        End If
        If My.Settings.Name.Contains("Lazer") Or My.Settings.Name.Contains("lazer") Or My.Settings.Name.Contains("Laser") Or My.Settings.Name.Contains("laser") Or My.Settings.Name.Contains("Logan") Or My.Settings.Name.Contains("logan") Or My.Settings.Name.Contains("Light Amplification by Stimulated Emission of Radiation") Then
            Form1.Caine.Play("Greet")
            Form1.Caine.Speak("\Vol=65535\You may come on in, master! Alright, back to my regular script!")
            Form1.Caine.Play("Blink")
        End If
        If My.Settings.Name.Contains("Merlin") Or My.Settings.Name.Contains("merlin") Or My.Settings.Name.Contains("Genie") Or My.Settings.Name.Contains("genie") Then
            Form1.Caine.Play("Explain")
            Form1.Caine.Speak("\Vol=65535\I can tell that you must really love magic. Alright, back to my regular script!")
            Form1.Caine.Play("Blink")
        End If
        If My.Settings.Name.Contains("Peedy") Then
            Form1.Caine.Play("LookLeft")
            Form1.Caine.Play("LookRight")
            Form1.Caine.Speak("\Chr=""whisper""\\Vol=65535\Have you seen a purple gorilla around here? \Chr=""normal""\ Alright, back to my regular script!")
            Form1.Caine.Play("Blink")
        End If
        Form1.Caine.Play("Acknowledge")
        Form1.Caine.Speak("\Vol=65535\" + My.Settings.Name + "! An \emp\interesting choice.|\Vol=65535\Nice to meet you, " + My.Settings.Name + "!")
        Form1.Caine.Play("Explain")
        Form1.Caine.Speak("\Vol=65535\Since this is the first time we have met, I'd like to tell you a little about myself.")
        Form1.Caine.Play("Announce")
        Form1.Caine.Speak("\Vol=65535\I am your friend, ringmaster and part of the Circus Pals lineup! I have the ability to learn from and entertain you. The more we browse, search, and travel the internet together, the smarter I'll become!")
        Form1.Caine.Play("Silly")
        Form1.Caine.Speak("\Vol=65535\Well, not that I'm not \emp\ already smart.")
        Form1.Caine.Play("Explain")
        Form1.Caine.Speak("\Vol=65535\Because the Internet can feel like a circus at times, I can help you find what you are looking for and even make suggestions as to where we should go to find it! The more time we spend together, the closer we'll become!")
        Form1.Caine.Play("Sad")
        Form1.Caine.Speak("\Vol=65535\I may be one of the smallest friends you have " + My.Settings.Name + ", but I will always try and make up for that with my big heart!")
        Form1.Caine.Play("DoMagic1")
        Form1.Caine.Speak("\Vol=65535\When you feel bored and feel like going on an adventure, I have a couple games preinstalled and ready to go. Adventures \emp\always keep you sane!")
        Form1.Caine.Play("DoMagic2")
        Form1.Caine.Play("Think")
        Form1.Caine.Speak("\Vol=65535\Am I rambling?")
        Form1.Caine.Play("Acknowledge")
        Form1.Caine.Speak("\Vol=65535\\emp\Alrighty then " + My.Settings.Name + ", feel free to look around.")
        Form1.Caine.MoveTo(320, 240)
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