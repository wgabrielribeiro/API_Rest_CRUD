namespace RestWithAspNET.API.Infra
{
    public static class Configs
    {
        public static string connectionString = GetConnectionString();
        private static string GetConnectionString()
        {
            // To avoid storing the connection string in your code,
            // you can retrieve it from a configuration file.
            //return "Data Source=MSSQL1;Initial Catalog=AdventureWorks;"
            //    + "Integrated Security=true;";

            //return "Server=GabrielRibeiro\\MSSQLSERVER01;Database=Curso;User Id=desenvolvimento;Password=ga0904;SslMode=none;";
            return "Server=GabrielRibeiro\\MSSQLSERVER01;Database=CursoUdemy;User Id=desenvolvimento;Password=ga0904;SslMode=none;";
        }
    }    
}
