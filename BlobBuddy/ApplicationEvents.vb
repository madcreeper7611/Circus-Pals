Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Sub MyApplication_UnhandledException(ByVal sender As System.Object, ByVal e As System.UnhandledExceptionEventArgs)
            Form1.Pomni.Play("Surprised")
            Form1.Pomni.Speak("Gasp! My program almost crashed! Can you report the error to \ctx=""Email""\circuspals@w10.site?")
            Form1.Pomni.Play("Blink")
        End Sub
    End Class
End Namespace

