using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Email;

namespace BrainstormSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Email(
                    new EmailConnectionInfo
                    {
                        FromEmail = "*******",
                        ToEmail = "*******",
                        MailServer = "smtp.gmail.com",
                        NetworkCredentials = new NetworkCredential
                        {
                            UserName = "*******",
                            Password = "*******"
                        },
                        EnableSsl = true,
                        Port = 587
                    },
                    new CompactJsonFormatter()
                )
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
