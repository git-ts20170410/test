Module MainModule

    Public AppFlag As Boolean = False

    Sub Main(ByVal arg() As String)

        If arg.Length > 0 AndAlso arg(0) = "1" Then
            ''コマンドライン引き数が"1"の場合はフラグをTrueにする
            AppFlag = True
        End If

        Application.Run(New Form1)

    End Sub

End Module
