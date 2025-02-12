Imports System.IO
Public Class BlobSingsForm
    Private filePaths As New Dictionary(Of String, String)
    Private customNames As New Dictionary(Of String, String)
    Private Sub BlobSingsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim num As Integer = MyBase.Location.X - 20
        Dim num2 As Integer = MyBase.Location.Y - 120
        Form1.Caine.MoveTo(CShort(num), CShort(num2))
        Dim directory1 As String = "C:\Program Files (x86)\BellCraft.com\MASH"
        Dim directory2 As String = "C:\Program Files\BellCraft.com\MASH"

        If Directory.Exists(directory1) Or Directory.Exists(directory2) Then
            ' Either directory exists, continue loading the form
        Else
            ' If the user doesn't have MASH, show message box
            Form1.Caine.Play("Surprised")
            Form1.Caine.Speak("Gasp! My program almost crashed! Is MASH installed properly?")
            Form1.Caine.Play("Blink")
            Dim result As DialogResult = MessageBox.Show("The Microsoft Agent Scripting Helper from Bellcraft.com is not installed. Would you like to install it now?", "Installation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                ' Launch the installer or provide instructions
                ' You can modify this to launch an installer if needed
                Dim mash As String = String.Format("{0}\\Runtimes\\mash_full_setup.exe", Environment.CurrentDirectory)
                If Directory.Exists(mash) Then
                    Process.Start(mash)
                Else
                    MessageBox.Show("Please visit Bellcraft.com to download and install the Microsoft Agent Scripting Helper.", "Installation Needed", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
            Me.Close()
        End If
        ' Defines the path to /blobsings/ 
        Dim directoryPath As String = Path.Combine(Application.StartupPath, "cainesongs")

        ' Defines custom display names for specific files
        customNames.Add("daisydaisy.msh", "Daisy Bell - Harry Dare")
        customNames.Add("deltarune.msh", "Don't Forget - Toby Foxx")
        customNames.Add("hellomababy.msh", "Hello Ma Baby - Joe Howard")
        customNames.Add("partingglass.msh", "The Parting Glass - Unknown")
        customNames.Add("risingmoon.msh", "Rising of the Moon - John Keegan Casey")
        customNames.Add("singintherain.msh", "Singing in the Rain - Arthur Freed")
        customNames.Add("tadc.msh", "Main Theme - Gooseworx")
        customNames.Add("wellerman.msh", "Wellerman - Unknown")

        ' Check if the directory exists
        If Directory.Exists(directoryPath) Then
            ' Gets all files in the specified path
            Dim files As String() = Directory.GetFiles(directoryPath)

            ' Adds each file name to the ListBox and keep track of the full paths
            For Each filePath As String In files
                Dim fileName As String = Path.GetFileName(filePath)
                Dim displayName As String
                If customNames.ContainsKey(fileName) Then
                    displayName = customNames(fileName)
                Else
                    displayName = fileName
                End If
                ListBox1.Items.Add(displayName)
                filePaths.Add(displayName, filePath)
            Next
        Else
            Form1.Caine.Play("Surprised")
            Form1.Caine.Speak("Gasp! My program almost crashed! Can you report the error to \ctx=""Email""\circuspals@w10.site?")
            Form1.Caine.Play("Blink")
            MessageBox.Show("The specified directory does not exist.")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Checking if an item is selected
        If ListBox1.SelectedIndex >= 0 Then
            ' Get the display name of the selected item
            Dim selectedDisplayName As String = ListBox1.SelectedItem.ToString()

            ' Using this to grab the full path of the selected file using the display name
            If filePaths.ContainsKey(selectedDisplayName) Then
                Dim selectedFilePath As String = filePaths(selectedDisplayName)

                ' Launches the file
                Try
                    Process.Start(selectedFilePath)
                Catch ex As Exception
                    Form1.Caine.Play("Surprised")
                    Form1.Caine.Speak("Gasp! My program almost crashed! Can you report the error to \ctx=""Email""\circuspals@w10.site?")
                    Form1.Caine.Play("Blink")
                    MessageBox.Show("Error launching file: " & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form1.Caine.StopAll()
        Form1.Caine.Play("Blink")
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Dim webAddress As String = "https://bellcraft.com/mash"
        Process.Start(webAddress)
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Dim webAddress As String = "https://tmafe.com/museum/mush"
        Process.Start(webAddress)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Form1.Caine.Speak("\rst\\Chr=""Normal""\")
        Form1.Caine.TTSModeID = "{CA141FD0-AC7F-11D1-97A3-006008273000}"
        Form1.Caine.Speak("Voice reset! Use this button again if Microsoft Agent or Double Agent glitch out or switch to SAPI 5 voice again! Click the Goodbye button if it still somehow continues to glitch out or use SAPI 5 voice.")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Form1.Caine.Speak("Having trouble? Let me help you.")
        Form1.Caine.Play("Explain")
        Form1.Caine.Speak("From this screen, I can sing any song you like. Simply select the song from the list and click on the Sing button.")
        Form1.Caine.Speak("You can \emp\also make your own custom songs in \Map=""Mash""=""MASH""\ or \Map=""Mush""=""MUSH""\ and place them in the \Map=""caine songs""=""""cainesongs""""\ folder in the root.")
        Form1.Caine.Play("Decline")
        Form1.Caine.Speak("Oh and remember " + My.Settings.Name + ", keep it clean, cause the filter isn't working at the moment.")
        Form1.Caine.Play("Acknowledge")
    End Sub
    Private Sub BlobSingsForm_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosed
        Form1.Caine.MoveTo(320, 240)
    End Sub
End Class