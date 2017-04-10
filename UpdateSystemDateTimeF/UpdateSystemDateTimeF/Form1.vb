Public Class Form1

    ''' <summary>
    ''' フォームロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If MainModule.AppFlag = True Then
            ''自動更新の場合は更新処理を呼んでアプリケーション終了
            Dim ret As String = UpdateSystemDateTime(txtURL.Text)

            Application.Exit()
        End If

    End Sub

    ''' <summary>
    ''' ボタンクリック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim ret As String = UpdateSystemDateTime(txtURL.Text)

        txtResult.Text = ret

    End Sub

    ''' <summary>
    ''' システム日付更新
    ''' </summary>
    ''' <param name="strURL"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateSystemDateTime(ByVal strURL As String) As String

        Dim result As String = String.Empty

        Try
            Dim webreq As System.Net.HttpWebRequest = _
                        DirectCast(System.Net.WebRequest.Create(strURL), System.Net.HttpWebRequest)

            Dim webres As System.Net.HttpWebResponse = _
                        DirectCast(webreq.GetResponse(), System.Net.HttpWebResponse)

            '応答データを受信するためのStreamを取得
            Dim st As System.IO.Stream = webres.GetResponseStream()
            '文字コードを指定して、StreamReaderを作成
            Dim sr As New System.IO.StreamReader(st, System.Text.Encoding.UTF8)
            'データをすべて受信
            Dim html As String = sr.ReadToEnd()
            '閉じる
            sr.Close()
            st.Close()
            webres.Close()

            Dim dtServer As DateTime
            Dim year As String
            Dim time As String
            Dim day As String
            Dim month As String
            Dim week As String

            Dim tmp() As String
            tmp = html.Split(" ")

            week = tmp(0)
            month = tmp(1)
            day = tmp(2)
            time = tmp(3)
            year = tmp(4)

            Dim strDate As String = week & " " & month & " " & day & " " & time & " " & year

            ''日付型に変換
            Dim ci As New System.Globalization.CultureInfo("en-US")
            dtServer = DateTime.ParseExact(strDate, _
                                           "ddd MMM dd HH:mm:ss yyyy", _
                                           System.Globalization.DateTimeFormatInfo.InvariantInfo, _
                                           System.Globalization.DateTimeStyles.None)

            ''OSの時刻を更新
            Today = New DateTime(dtServer.Year, dtServer.Month, dtServer.Day, 0, 0, 0)

            'システム時刻を6:15:30にする
            TimeOfDay = New DateTime(dtServer.Year, dtServer.Month, dtServer.Day, dtServer.Hour, dtServer.Minute, dtServer.Second)

            ''結果を表示
            result = "日時を【" & dtServer.ToString & "】に更新しました。"

        Catch ex As Exception

            result = ex.Message

        End Try

        Return result

    End Function

    ''' <summary>
    ''' キャンセルボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ''アプリケーション終了
        Application.Exit()
    End Sub
End Class
