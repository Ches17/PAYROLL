Imports System.Data.SqlClient
Imports System.Windows
Imports System.Windows.Forms.DataFormats
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports Mysqlx

Public Class Form2
    Private Sub AddEmployee()
        query = "INSERT INTO salary (ID, FirstName, LastName, Position, Salary, CalculatorSalary) VALUES (@ID, @FirstName, @LastName, @Position, @Salary, @CalculatorSalary)"
        cmd = New MySqlCommand
        cmd.Connection = conn
        Using command As New MySqlCommand(query, conn)
            ' Set parameter values from textboxes
            command.Parameters.AddWithValue("@ID", TextBox1.Text)
            command.Parameters.AddWithValue("@FirstName", TextBox2.Text)
            command.Parameters.AddWithValue("@LastName", TextBox3.Text)
            command.Parameters.AddWithValue("@Position", ComboBox1.SelectedItem.ToString())
            command.Parameters.AddWithValue("@Salary", TextBox4.Text)
            command.Parameters.AddWithValue("@CalculatorSalary", TextBox9.Text)


            Try
                conn.Open()
                command.ExecuteNonQuery()
                MessageBox.Show("Employee inserted successfully.")

                ' Clear textboxes after successful insertion
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                ComboBox1.SelectedIndex = -1
                TextBox4.Clear()
            Catch ex As Exception
                MessageBox.Show("Error: " + ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AddEmployee()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim query As String = "SELECT * FROM salary WHERE ID = '" & TextBox1.Text & "'"
        Try
            conn.Open()
            Dim da As New MySqlDataAdapter(query, conn)

            'the MySqlDataAdapter here is used to retrieve data from database

            Dim dt As DataTable = New DataTable

            'the DataTable is a non-visible component to hold table data
            da.Fill(dt) 'this Fills the retrieve data from database to datatable
            DataGridView1.DataSource = dt
            'this set the datasource of datagridview1 as dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        conn.Close()
    End Sub

    Public Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            ' Get the selected row
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' Assuming the data in the DataGridView is bound to a DataTable,
            ' you can access the cell values using the column index or column name.

            ' Retrieve the values from the selected row
            Dim ID As Integer = Convert.ToInt32(selectedRow.Cells("ID").Value)
            Dim FirstName As String = selectedRow.Cells("FirstName").Value.ToString()
            Dim LastName As String = selectedRow.Cells("LastName").Value.ToString()
            Dim Position As String = selectedRow.Cells("Position").Value.ToString()
            Dim Salary As Decimal = Convert.ToDecimal(selectedRow.Cells("Salary").Value)


            ' Set the values in the textboxes and combobox
            TextBox1.Text = ID
            TextBox2.Text = FirstName
            TextBox3.Text = LastName.ToString()
            ComboBox1.SelectedItem = Position
            TextBox4.Text = Salary.ToString()
        End If
    End Sub
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize the ComboBoxPosition control and add positions
        ComboBox1.Items.AddRange({"CEO", "Director", "Manager", "Team Lead", "Regular", "Security", "Maintenance"})
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ' Uncheck other checkboxes
            CheckBox2.Checked = False
            CheckBox3.Checked = False

            ' Update combobox items
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("CEO")
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            ' Uncheck other checkboxes
            CheckBox1.Checked = False
            CheckBox3.Checked = False

            ' Update combobox items
            ComboBox1.Items.Clear()
            ComboBox1.Items.AddRange({"Director", "Manager", "Team Lead", "Regular"})
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            ' Uncheck other checkboxes
            CheckBox1.Checked = False
            CheckBox2.Checked = False

            ' Update combobox items
            ComboBox1.Items.Clear()
            ComboBox1.Items.AddRange({"Security", "Maintenance"})
        End If
    End Sub
    Private Sub ComboBoxPosition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem IsNot Nothing Then
            ' Display the salary based on the selected position
            Select Case ComboBox1.SelectedItem.ToString()
                Case "CEO"
                    TextBox4.Text = "180000.00"
                Case "Director"
                    TextBox4.Text = "120000.00"
                Case "Manager"
                    TextBox4.Text = "60000.00"
                Case "Team Lead"
                    TextBox4.Text = "40000.00"
                Case "Regular"
                    TextBox4.Text = "25000.00"
                Case "Security"
                    TextBox4.Text = "15000.00"
                Case "Maintenance"
                    TextBox4.Text = "10000.00"
                Case Else
                    TextBox4.Text = ""
            End Select
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            conn.Open()
            Dim query As String = "SELECT * FROM salary"
            Dim da As New MySqlDataAdapter(query, conn)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MsgBox("Connection Error!")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub UpdateEmployee()
        Dim ID As String = TextBox1.Text
        Dim FirstName As String = TextBox2.Text
        Dim LastName As String = TextBox3.Text
        Dim Position As String = ComboBox1.SelectedItem.ToString()
        Dim Salary As Decimal = TextBox4.Text
        Dim CalculatorSalary As Decimal = TextBox9.Text

        ' Create the update query
        Dim query As String = "UPDATE salary SET FirstName = @FirstName, LastName = @LastName, Position = @Position, Salary = @Salary, CalculatorSalary = @CalculatorSalary WHERE ID = @ID"
        Using command As New MySqlCommand(query, conn)
            command.Parameters.AddWithValue("@ID", ID)
            command.Parameters.AddWithValue("@FirstName", FirstName)
            command.Parameters.AddWithValue("@LastName", LastName)
            command.Parameters.AddWithValue("@Position", Position)
            command.Parameters.AddWithValue("@Salary", Salary)
            command.Parameters.AddWithValue("@CalculatorSalary", CalculatorSalary)


            Dim isSelected As Boolean = False
            If DataGridView1.SelectedRows.Count > 0 Then
                isSelected = True
            End If

            If isSelected = True Then
                Button2.Text = "UPDATE"
                TextBox1.Text = DataGridView1.CurrentRow.Cells(0).Value
                TextBox2.Text = DataGridView1.CurrentRow.Cells(1).Value
                TextBox3.Text = DataGridView1.CurrentRow.Cells(2).Value
                TextBox4.Text = DataGridView1.CurrentRow.Cells(3).Value
                TextBox8.Text = DataGridView1.CurrentRow.Cells(4).Value
                TextBox9.Text = DataGridView1.CurrentRow.Cells(5).Value
                TextBox1.ReadOnly = True
                Button5.PerformClick()
            Else
                MsgBox("Please select  a row First!")
            End If


            Try
                conn.Open()
                ' Execute the update query
                command.ExecuteNonQuery()
                MessageBox.Show("Employee updated successfully.")
                ' Clear textboxes after successful insertion
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                ComboBox1.SelectedIndex = -1
                TextBox4.Clear()
            Catch ex As Exception
                MessageBox.Show("Error updating employee record: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        UpdateEmployee()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim isSelected As Boolean = False

        If DataGridView1.SelectedRows.Count > 0 Then
            isSelected = True
        End If

        If isSelected = True Then
            Dim response As DialogResult = MessageBox.Show("Are you sure you want to delete the record? This action is not  reversible.", "Please Conform action", MessageBoxButtons.YesNo)

            If response = DialogResult.Yes Then
                Try
                    conn.Open()
                    Dim cmd As New MySqlCommand
                    Dim result As Integer
                    With cmd
                        .Connection = conn
                        .CommandText = "DELETE FROM salary " & "WHERE ID='" & DataGridView1.CurrentRow.Cells(0).Value & "';"
                        result = .ExecuteNonQuery()
                        If result > 0 Then
                            MsgBox("Record deleted successfully")
                        Else
                            MsgBox("No record was deleter!")
                        End If
                    End With
                Catch ex As Exception
                    MsgBox("Connection Error!")
                Finally
                    conn.Close()
                End Try
            End If


        Else
            MsgBox("Please select a row first!")
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        TextBox7.Text = ""
        TextBox7.AppendText("" + vbNewLine)
        TextBox7.AppendText("" + vbNewLine)
        TextBox7.AppendText("" + vbNewLine)

        TextBox7.AppendText(vbTab + vbTab + vbTab & "Pay-Slip" + vbNewLine)
        TextBox7.AppendText("" + vbNewLine)
        TextBox7.AppendText("" + vbNewLine)
        TextBox7.AppendText("" + vbNewLine)
        TextBox7.AppendText("FirstName:    " & TextBox2.Text + vbNewLine)
        TextBox7.AppendText("" + vbNewLine)
        TextBox7.AppendText("" + vbNewLine)

        TextBox7.AppendText("Position:        " & ComboBox1.SelectedItem + vbNewLine)
        TextBox7.AppendText("Salary:          " & TextBox4.Text + vbNewLine)

        TextBox7.AppendText("<= 35,000.00 = 0.45 + 0.045" + vbNewLine)
        TextBox7.AppendText("<= 30,000.00 = 0.01 + 0.01" + vbNewLine)
        TextBox7.AppendText("<= 80,000.00 = 0.015 + 0.015" + vbNewLine)


        TextBox7.AppendText("CalculatorSalary =     " + vbTab & TextBox9.Text + vbNewLine)
        TextBox7.AppendText("================================================================" + vbNewLine)
        TextBox7.AppendText("Date: " & Today + vbTab + vbTab & vbTab & "Time: " & TimeOfDay + vbNewLine)
        TextBox7.AppendText("================================================================" + vbNewLine)
        TextBox7.AppendText("" + vbNewLine)
        TextBox7.AppendText("" + vbNewLine)
        TextBox7.AppendText("" + vbNewLine)
        TextBox7.AppendText("================================================================" + vbNewLine)
        TextBox7.AppendText(" If you Have Problem Please shut up! And become a man" + vbNewLine)
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawString(TextBox7.Text, Font, Brushes.Black, 140, 140)
        e.Graphics.DrawImage(Me.PictureBox1.Image, 200, 100, PictureBox1.Width - 15, PictureBox1.Height - 25)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim AnnualIncome As Double = 0
        Dim Salary As Double = 0
        Dim CalculatorSalary As Double = 0 ' Change to Double type

        If Double.TryParse(TextBox4.Text, Salary) Then
            ' Calculate SSS deduction
            Dim sssRate As Double = 0.045 ' SSS rate of 4.5%
            Dim sssEmployeeDeduction As Double = 0
            Dim sssEmployerContribution As Double = 0
            If Salary <= 35000 Then
                sssEmployeeDeduction = Salary * sssRate
                sssEmployerContribution = sssEmployeeDeduction
            End If

            ' Calculate Pag-Ibig deduction
            Dim pagibigRate As Double = 0.01 ' Pag-Ibig rate of 1%
            Dim pagibigEmployeeDeduction As Double = 0
            Dim pagibigEmployerContribution As Double = 0
            If Salary <= 30000 Then
                pagibigEmployeeDeduction = Salary * pagibigRate
                pagibigEmployerContribution = pagibigEmployeeDeduction

            End If

            ' Calculate PhilHealth deduction
            Dim philhealthRate As Double = 0.015 ' PhilHealth rate of 1.5%
            Dim philhealthEmployeeDeduction As Double = 0
            Dim philhealthEmployerContribution As Double = 0
            If Salary <= 80000 Then
                philhealthEmployeeDeduction = Salary * philhealthRate
                philhealthEmployerContribution = philhealthEmployeeDeduction
            End If
            Dim TextBox9v As Double
            If Double.TryParse(TextBox9.Text, TextBox9v) Then

                TextBox11.Text = (TextBox9v * 12).ToString()
                Dim taxPercentage As Double = 0
                If Double.TryParse(TextBox7.Text, AnnualIncome) Then
                    If AnnualIncome > 250000 Then
                        Dim taxableIncome As Double = (Salary - (sssEmployeeDeduction + sssEmployerContribution + pagibigEmployeeDeduction + pagibigEmployerContribution + philhealthEmployeeDeduction + philhealthEmployerContribution)) * 12

                        If taxableIncome > 250000 AndAlso taxableIncome < 400000 Then
                            taxPercentage = 0.15 ' 15% tax
                        ElseIf taxableIncome >= 400000 AndAlso taxableIncome < 550000 Then
                            taxPercentage = 0.2 ' 20% tax
                        Else
                            ' Calculate additional 5% tax for every additional Php 150,000.00
                            Dim additionalTax As Double = Math.Floor((taxableIncome - 550000) / 150000)
                            taxPercentage = 0.2 + (additionalTax * 0.05)
                        End If
                        Dim TotalTax As Double = Salary * taxPercentage
                        TextBox10.Text = (taxableIncome - TotalTax).ToString
                        TextBox12.Text = (taxPercentage * 100 & "%").ToString
                    End If
                End If
            End If

            ' Calculate total deductions
            CalculatorSalary = (sssEmployeeDeduction + sssEmployerContribution + pagibigEmployeeDeduction + pagibigEmployerContribution + philhealthEmployeeDeduction + philhealthEmployerContribution).ToString

            Dim textBox8Value As Double
            If Double.TryParse(TextBox8.Text, textBox8Value) Then
                TextBox9.Text = (textBox8Value - CalculatorSalary).ToString()
            End If
        End If
    End Sub

    Private Sub Textbox()
        If ComboBox1.SelectedItem IsNot Nothing Then
            ' Display the salary based on the selected position
            Select Case ComboBox1.SelectedItem.ToString()
                Case "CEO"
                    TextBox8.Text = "180000.00"
                Case "Director"
                    TextBox8.Text = "120000.00"
                Case "Manager"
                    TextBox8.Text = "60000.00"
                Case "Team Lead"
                    TextBox8.Text = "40000.00"
                Case "Regular"
                    TextBox8.Text = "25000.00"
                Case "Security"
                    TextBox8.Text = "15000.00"
                Case "Maintenance"
                    TextBox8.Text = "10000.00"
                Case Else
                    TextBox8.Text = ""
            End Select
        End If
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        Textbox()
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        Textbox()
    End Sub

End Class