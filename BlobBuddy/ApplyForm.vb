Imports System.IO
Imports System.Xml
Imports System.Text.RegularExpressions

Public Class ApplyForm
    Dim XmlCtrl As New XmlDocument
    Public Shared SelectedDate As Date
    Dim EventXML = Application.UserAppDataPath + "\events.xml"

    Private Sub ApplyForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = "Schedule for " & SelectedDate.ToString("MM/dd/yyyy")
        ReloadEvents()
        If String.IsNullOrEmpty(ComboBox1.Text) Then
            ComboBox1.Text = "12:00"
        End If

        If String.IsNullOrEmpty(ComboBox2.Text) Then
            ComboBox2.Text = "AM"
        End If

        If String.IsNullOrEmpty(ComboBox4.Text) Then
            ComboBox4.Text = "12:00"
        End If

        If String.IsNullOrEmpty(ComboBox3.Text) Then
            ComboBox3.Text = "AM"
        End If

        If String.IsNullOrEmpty(TextBox2.Text) Then
            TextBox2.Text = SelectedDate.ToShortDateString()
        End If
    End Sub

    Private dateFormat As Regex = New Regex("^\d{1,2}\/\d{1,2}\/\d{4}$")

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not dateFormat.IsMatch(TextBox2.Text) Then
            Form1.Caine.Play("FastExplain")
            Form1.Caine.Speak("You must a valid a reminder date before you can save the appointment.")
            Form1.Caine.Play("Blink")
            Return
        End If

        If String.IsNullOrEmpty(TextBox1.Text) Then
            Form1.Caine.Play("FastExplain")
            Form1.Caine.Speak("You must enter some text for the appointment before you can save it.")
            Form1.Caine.Play("Blink")
        Else
            EnsureXmlFileExists()
            Dim xmlDoc = New XmlDocument()
            xmlDoc.Load(EventXML)
            Dim root As XmlNode = xmlDoc.DocumentElement

            Dim newEvent = xmlDoc.CreateElement("Event")

            Dim eventDate = xmlDoc.CreateElement("EventDate")
            eventDate.InnerText = SelectedDate.ToShortDateString()
            newEvent.AppendChild(eventDate)

            Dim eventTime = xmlDoc.CreateElement("EventTime")
            eventTime.InnerText = ComboBox1.Text
            newEvent.AppendChild(eventTime)

            Dim eventAMPM = xmlDoc.CreateElement("EventAMPM")
            eventAMPM.InnerText = ComboBox2.Text
            newEvent.AppendChild(eventAMPM)

            Dim eventDesc = xmlDoc.CreateElement("EventDesc")
            eventDesc.InnerText = TextBox1.Text
            newEvent.AppendChild(eventDesc)

            Dim eventReminder = xmlDoc.CreateElement("Reminder")
            eventReminder.InnerText = CheckBox1.Checked.ToString()
            newEvent.AppendChild(eventReminder)

            Dim RemindDate = xmlDoc.CreateElement("RemindDate")
            RemindDate.InnerText = TextBox2.Text
            newEvent.AppendChild(RemindDate)

            Dim eventRemindTime = xmlDoc.CreateElement("RemindTime")
            eventRemindTime.InnerText = ComboBox4.Text
            newEvent.AppendChild(eventRemindTime)

            Dim eventRemindAMPM = xmlDoc.CreateElement("RemindAMPM")
            eventRemindAMPM.InnerText = ComboBox3.Text
            newEvent.AppendChild(eventRemindAMPM)
            root.AppendChild(newEvent)
            xmlDoc.Save(EventXML)
            ReloadEvents()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Not dateFormat.IsMatch(TextBox2.Text) Then
            Form1.Caine.Play("FastExplain")
            Form1.Caine.Speak("You must a valid reminder date before you can save the appointment.")
            Form1.Caine.Play("Blink")
            Return
        End If

        If ListView1.SelectedItems.Count > 0 Then
            Dim selectedItem = ListView1.SelectedItems(0)
            Dim selectedTime = selectedItem.SubItems(0).Text

            If Not String.IsNullOrEmpty(TextBox1.Text) Then
                Dim xmlDoc = New XmlDocument()
                xmlDoc.Load(EventXML)
                Dim eventNode = xmlDoc.SelectSingleNode("Events/Event[EventDate='" & SelectedDate.ToShortDateString() & "'][EventTime='" & selectedTime.Substring(0, selectedTime.Length - 3) & "'][EventAMPM='" & selectedTime.Substring(selectedTime.Length - 2) & "'][EventDesc='" & selectedItem.SubItems(1).Text & "']")

                If TypeOf eventNode Is Object Then
                    eventNode("EventTime").InnerText = ComboBox1.Text
                    eventNode("EventAMPM").InnerText = ComboBox2.Text
                    eventNode("EventDesc").InnerText = TextBox1.Text
                    eventNode("Reminder").InnerText = CheckBox1.Checked.ToString()
                    eventNode("RemindDate").InnerText = TextBox2.Text
                    eventNode("RemindTime").InnerText = ComboBox4.Text
                    eventNode("RemindAMPM").InnerText = ComboBox3.Text
                    xmlDoc.Save(EventXML)
                    ReloadEvents()
                End If
            Else
                Form1.Caine.Play("FastExplain")
                Form1.Caine.Speak("You must enter some text for the appointment before you can save it.")
                Form1.Caine.Play("Blink")
                Return
            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ListView1.SelectedItems.Count > 0 Then
            If MessageBox.Show("Delete selected appointment?", "BonziBUDDY", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Dim selectedItem = ListView1.SelectedItems(0)
                Dim xmlDoc = New XmlDocument()
                xmlDoc.Load(EventXML)
                Dim selectedTime = selectedItem.SubItems(0).Text
                Dim eventNode = xmlDoc.SelectSingleNode("Events/Event[EventDate='" & SelectedDate.ToShortDateString() & "'][EventTime='" & selectedTime.Substring(0, selectedTime.Length - 3) & "'][EventAMPM='" & selectedTime.Substring(selectedTime.Length - 2) & "'][EventDesc='" & selectedItem.SubItems(1).Text & "']")

                If TypeOf eventNode Is Object Then
                    eventNode.ParentNode.RemoveChild(eventNode)
                    xmlDoc.Save(EventXML)
                    ReloadEvents()
                End If
            End If
        Else
            Form1.Caine.Play("Decline")
            Form1.Caine.Speak("You'll need to choose an appointment to delete from the Appointments list first. Just click on the appointment you wish to delete, and then click on the Delete button.")
            Form1.Caine.Play("Blink")
        End If
    End Sub

    Private Sub ReloadEvents()
        ListView1.Items.Clear()
        If File.Exists(EventXML) Then
            XmlCtrl.Load(EventXML)
            For Each EventNode As XmlNode In XmlCtrl.SelectNodes("/Events/Event")
                Dim EventDate = EventNode("EventDate").InnerText
                Dim EventTime = EventNode("EventTime").InnerText
                Dim EventAMPM = EventNode("EventAMPM").InnerText
                Dim Description = EventNode("EventDesc").InnerText

                If Equals(EventDate, SelectedDate.ToShortDateString()) Then
                    Dim item As ListViewItem = New ListViewItem
                    item.Text = EventTime & " " & EventAMPM
                    item.SubItems.Add(Description)
                    ListView1.Items.Add(item)
                End If
            Next
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        If File.Exists(EventXML) Then
            XmlCtrl.Load(EventXML)
            If ListView1.SelectedItems.Count > 0 Then
                Dim selectedItem = ListView1.SelectedItems(0)
                Dim selectedTime = selectedItem.SubItems(0).Text
                Dim eventNode = XmlCtrl.SelectSingleNode("Events/Event[EventDate='" & SelectedDate.ToShortDateString & "'][EventTime='" & selectedTime.Substring(0, selectedTime.Length - 3) & "'][EventAMPM='" & selectedTime.Substring(selectedTime.Length - 2) & "'][EventDesc='" & selectedItem.SubItems(1).Text & "']")

                If Not eventNode Is Nothing Then
                    If eventNode("Reminder").InnerText = "True" Then
                        CheckBox1.Checked = True
                    End If

                    TextBox1.Text = ListView1.SelectedItems(0).SubItems(1).Text
                    ComboBox1.Text = selectedTime.Substring(0, selectedTime.Length - 3)
                    ComboBox2.Text = selectedTime.Substring(selectedTime.Length - 2)
                    TextBox2.Text = eventNode("RemindDate").InnerText
                    ComboBox4.Text = eventNode("RemindTime").InnerText
                    ComboBox3.Text = eventNode("RemindAMPM").InnerText
                End If
            End If
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        TextBox1.Text = Nothing
        ComboBox1.Text = "12:00"
        ComboBox2.Text = "AM"
        CheckBox1.Checked = False
        ComboBox4.Text = "12:00"
        ComboBox3.Text = "AM"
        ListView1.SelectedItems(0).Selected = False
    End Sub

    Private Sub EnsureXmlFileExists()
        If Not File.Exists(EventXML) Then
            Dim xmlDoc = New XmlDocument()
            Dim xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
            Dim root = xmlDoc.CreateElement("Events")
            xmlDoc.AppendChild(root)
            xmlDoc.InsertBefore(xmlDeclaration, root)
            xmlDoc.Save(EventXML)
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Close()
    End Sub
End Class