Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Class Dots
    Inherits System.Web.UI.Page

    'The connenction string for the database
    Dim connectionString As String = "Data Source=titan.cs.weber.edu,10433;Initial Catalog=W0114267;User ID=w0114267;Password='C7Q3V4;lkj'"

    ''' <summary>
    ''' Gets parseable output for the dots in the database
    ''' </summary>
    ''' <returns>All the dots in the database</returns>
    Public Function GetDots() As String
        Dim sql As String = "SELECT DISTINCT x_coord, y_coord FROM dots;"
        Dim dots As List(Of String) = New List(Of String)()

        Using connection As SqlConnection = New SqlConnection(connectionString)
            connection.Open()

            Dim command As SqlCommand = New SqlCommand(sql, connection)
            Dim reader As SqlDataReader = command.ExecuteReader()

            While reader.Read()
                dots.Add(CInt(reader("x_coord")) & " " & CInt(reader("y_coord")))
            End While
        End Using

        Return String.Join(vbLf, dots)
    End Function

    ''' <summary>
    ''' Inserts a dot into the database at the point specified
    ''' </summary>
    ''' <param name="x">The x coordinate</param>
    ''' <param name="y">The y coordinate</param>
    ''' <returns>The number of rows affected (should be 1)</returns>
    Public Function AddDot(ByVal x As Integer, ByVal y As Integer) As Integer
        Dim sql As String = "INSERT INTO dots (x_coord, y_coord) VALUES ('" & x & "', '" & y & "');"
        Dim rowsAffected As Integer = 0

        Using connection As SqlConnection = New SqlConnection(connectionString)
            Dim command As SqlCommand = New SqlCommand(sql, connection)
            command.Connection.Open()
            rowsAffected = command.ExecuteNonQuery()
        End Using

        Return rowsAffected
    End Function

    ''' <summary>
    ''' Deletes all dots from the database
    ''' </summary>
    ''' <returns>The number of rows affected</returns>
    Public Function ClearDots() As Integer
        Dim sql As String = "DELETE FROM dots WHERE dot_id IS NOT NULL;"
        Dim rowsAffected As Integer = 0

        Using connection As SqlConnection = New SqlConnection(connectionString)
            Dim command As SqlCommand = New SqlCommand(sql, connection)
            command.Connection.Open()
            rowsAffected = command.ExecuteNonQuery()
        End Using

        Return rowsAffected
    End Function
End Class
