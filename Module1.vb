Imports MySql.Data.MySqlClient

Module Module1
    Public conn As New MySqlConnection("host=127.0.0.1;user=root;password='W@2915djkq#';database=payrolldb1")
    Public query As String
    Public cmd As MySqlCommand
    Public da As MySqlDataAdapter
    Public dt As DataTable
    Public dr As MySqlDataReader
End Module
