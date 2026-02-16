Imports System.IO
Imports System.Diagnostics
Imports System.Windows.Forms
Public Class BlobSingsForm
    Private filePaths As New Dictionary(Of String, String)
    Private customNames As New Dictionary(Of String, String)
    Private Sub BlobSingsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Form1.Pomni.Stop()
        Dim num As Integer = MyBase.Location.X - 20
        Dim num2 As Integer = MyBase.Location.Y - 120
        Form1.Pomni.MoveTo(CShort(num), CShort(num2))
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SingIntro()

        Select Case CaineSongList.SelectedItems(0).Text
            Case "Daisy Bell"
                Form1.SingMain(1)
                Exit Select
            Case "Hello Ma Baby"
                Form1.SingMain(2)
                Exit Select
            Case "Cindy"
                Form1.SingMain(3)
                Exit Select
            Case "In My Merry Oldsmobile"
                Form1.SingMain(4)
                Exit Select
            Case "Oh, Dem Golden Slippers"
                Form1.SingMain(5)
                Exit Select
            Case "Rising of the Moon"
                Form1.SingMain(6)
                Exit Select
            Case "In The Good Old Summertime"
                Form1.SingMain(7)
                Exit Select
            Case "Beautiful Dreamer"
                Form1.SingMain(8)
                Exit Select
            Case "Twinkle Twinkle Little Star"
                Form1.SingMain(9)
                Exit Select
            Case "Long, Long Ago"
                Form1.SingMain(10)
                Exit Select
            Case "Ida"
                Form1.SingMain(11)
                Exit Select
            Case "Where Did You Get That Hat"
                Form1.SingMain(12)
                Exit Select
            Case "I've Been Working On the Railroad"
                Form1.SingMain(13)
                Exit Select
            Case "My Wild Irish Rose"
                Form1.SingMain(14)
                Exit Select
            Case "Meet Me In St Louis, Louis"
                Form1.SingMain(15)
                Exit Select
            Case "Sidewalks of New York"
                Form1.SingMain(16)
                Exit Select
            Case "The Man On the Flying Trapeze"
                Form1.SingMain(17)
                Exit Select
            Case "Over the River and Through the Woods"
                Form1.SingMain(18)
                Exit Select
            Case "Blue-Tail Fly"
                Form1.SingMain(19)
                Exit Select
            Case "Mighty Lak' A Rose"
                Form1.SingMain(20)
            Case "Don't Forget"
                Form1.SingMain(21)
            Case "Parting Glass"
                Form1.SingMain(22)
            Case "Singin' in the Rain"
                Form1.SingMain(23)
            Case "Main Theme"
                Form1.SingMain(24)
            Case "Wellerman"
                Form1.SingMain(25)
                Exit Select
        End Select
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form1.Pomni.StopAll()
        Form1.Pomni.Speak("\Chr=""Normal""\")
        Form1.Pomni.Play("Blink")
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Form1.Pomni.Speak("\rst\\Chr=""Normal""\")
        Form1.Pomni.TTSModeID = "{CA141FD0-AC7F-11D1-97A3-006008273000}"
        Form1.Pomni.Speak("Voice reset! Use this button again if Microsoft Agent or Double Agent glitch out or switch to SAPI 5 voice again! Click the Goodbye button if it still somehow continues to glitch out or use SAPI 5 voice.")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Form1.Pomni.Speak("Having trouble? Let me help you.")
        Form1.Pomni.Play("Explain")
        Form1.Pomni.Speak("From this screen, I can sing any song you like. Simply select the song from the list and click on the Sing button.")
        Form1.Pomni.Play("Acknowledge")
    End Sub
    Private Sub BlobSingsForm_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosed
        Form1.Pomni.MoveTo(320, 240)
    End Sub

    Private Sub SingIntro()
        Dim rnd = New Random()

        Select Case rnd.Next(1, 4)
            Case 1
                Form1.Pomni.Play("Explain")
                Form1.Pomni.Speak("Here's one Queenie used to sing to Kinger. I hope you like it.")
                Form1.Pomni.Play("FastExplain")
                Form1.Pomni.Balloon.Style = &H31C000D
                Exit Select
            Case 2
                Form1.Pomni.Play("Explain")
                Form1.Pomni.Speak("OK, here goes.")
                Form1.Pomni.Play("FastExplain")
                Form1.Pomni.Balloon.Style = &H31C000D
                Exit Select
            Case 3
                Form1.Pomni.Play("FastExplain")
                Form1.Pomni.Balloon.Style = &H31C000D
                Exit Select
        End Select
    End Sub
End Class