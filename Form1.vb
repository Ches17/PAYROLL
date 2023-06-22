Imports MySql.Data.MySqlClient

Public Class Form1
    Private Sub Login_Click(sender As Object, e As EventArgs) Handles Login.Click
        cmd = New MySqlCommand
        cmd.Connection = conn
        query = "SELECT * FROM hrlogin where Username='" & TextBox1.Text & "' and Password='" & TextBox2.Text & "'"
        da = New MySqlDataAdapter(query, conn)
        dt = New DataTable()

        Try
            conn.Open()
            da.Fill(dt)
            If dt.Rows.Count <= 0 Then
                MsgBox("Login Failed !", vbInformation)
            Else
                Form2.Show()
            End If
        Catch ex As Exception
            MessageBox.Show("ERROR :" & ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

End Class