Imports System.Data.OleDb
Public Class MainForm
    'declare a string variable to store the database connection string
    Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ComputerStore.accdb;"
    Dim conn As New OleDbConnection(connStr)

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            conn.Open()
            Dim cmd As New OleDbCommand("SELECT * FROM Orders", conn)
            Dim adapter As New OleDbDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvOrders.DataSource = dt
            lblStatus.Text = "Data loaded successfully."
        Catch ex As Exception
            lblStatus.Text = "Error loading data: " & ex.Message
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        Try
            conn.Open()
            Dim cmd As New OleDbCommand("INSERT INTO Orders (CustomerID, ProductID, OrderDate, Quantity) VALUES (?, ?, ?, ?)", conn)
            cmd.Parameters.AddWithValue("@CustomerID", txtCustomerID.Text)
            cmd.Parameters.AddWithValue("@ProductID", txtProductID.Text)
            cmd.Parameters.AddWithValue("@OrderDate", DateTime.Parse(txtOrderDate.Text))
            cmd.Parameters.AddWithValue("@Quantity", Integer.Parse(txtQuantity.Text))
            cmd.ExecuteNonQuery()
            lblStatus.Text = "Record inserted successfully."
            LoadData()
        Catch ex As Exception
            lblStatus.Text = "Error inserting record: " & ex.Message
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            conn.Open()
            Dim cmd As New OleDbCommand("UPDATE Orders SET CustomerID = ?, ProductID = ?, OrderDate = ?, Quantity = ? WHERE OrderID = ?", conn)
            cmd.Parameters.AddWithValue("@CustomerID", txtCustomerID.Text)
            cmd.Parameters.AddWithValue("@ProductID", txtProductID.Text)
            cmd.Parameters.AddWithValue("@OrderDate", DateTime.Parse(txtOrderDate.Text))
            cmd.Parameters.AddWithValue("@Quantity", Integer.Parse(txtQuantity.Text))
            cmd.Parameters.AddWithValue("@OrderID", dgvOrders.SelectedRows(0).Cells("OrderID").Value)
            cmd.ExecuteNonQuery()
            lblStatus.Text = "Record updated successfully."
            LoadData()
        Catch ex As Exception
            lblStatus.Text = "Error updating record: " & ex.Message
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            conn.Open()
            Dim cmd As New OleDbCommand("DELETE FROM Orders WHERE OrderID = ?", conn)
            cmd.Parameters.AddWithValue("@OrderID", dgvOrders.SelectedRows(0).Cells("OrderID").Value)
            cmd.ExecuteNonQuery()
            lblStatus.Text = "Record deleted successfully."
            LoadData()
        Catch ex As Exception
            lblStatus.Text = "Error deleting record: " & ex.Message
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        Try
            conn.Open()
            Dim cmd As New OleDbCommand("SELECT * FROM Orders WHERE CustomerID = ?", conn)
            cmd.Parameters.AddWithValue("@CustomerID", txtCustomerID.Text)
            Dim adapter As New OleDbDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvOrders.DataSource = dt
            lblStatus.Text = "Filter applied."
        Catch ex As Exception
            lblStatus.Text = "Error filtering data: " & ex.Message
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtCustomerID.Clear()
        txtProductID.Clear()
        txtOrderDate.Clear()
        txtQuantity.Clear()
        LoadData()
        lblStatus.Text = "Form cleared."
    End Sub
End Class