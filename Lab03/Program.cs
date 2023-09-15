// See https://aka.ms/new-console-template for more information

//Librerías del ADO .NET
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Lab03;

class Program
{
    // Cadena de conexión a la base de datos
    public static string connectionString = "Data Source=LAB1504-27\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=tecsup;Password=123456";

    static void Main()
    {
        Console.WriteLine("Lista de productos usando DataTable:");
        List<Producto> productosUsingDataTable = GetProductosUsingDataTable();
        foreach (var producto in productosUsingDataTable)
        {
            Console.WriteLine($"ID: {producto.IdProducto}, Nombre: {producto.Nombre}, Categoría: {producto.Categoria}, Precio: {producto.Precio}, Fecha de Vencimiento: {producto.FechaVencimiento}");
        }
    }

    static List<Producto> GetProductosUsingDataTable()
    {
        List<Producto> productos = new List<Producto>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("SELECT * FROM [Productos]", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        productos.Add(new Producto
                        {
                            IdProducto = Convert.ToInt32(row["IdProducto"]),
                            Nombre = row["Nombre"].ToString(),
                            Categoria = row["Categoria"].ToString(),
                            Precio = Convert.ToDecimal(row["Precio"]),
                            FechaVencimiento = Convert.ToDateTime(row["FechaVencimiento"])
                        });
                    }
                }
            }
        }
        return productos;
    }
}

class Producto
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; }
    public string Categoria { get; set; }
    public decimal Precio { get; set; }
    public DateTime FechaVencimiento { get; set; }
}
