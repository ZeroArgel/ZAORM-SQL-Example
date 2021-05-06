namespace SQLExamples.MVC
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    public class Program
    {
        public static void Main(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(za => za.UseStartup<Startup>())
                .Build()
                .Run();
    }
}