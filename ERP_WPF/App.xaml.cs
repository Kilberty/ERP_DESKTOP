using ERP_WPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Windows;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace ERP_WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ExecutarMigracoes();
        }

        private void ExecutarMigracoes()
        {
            try
            {
                var config = ConfigManager.LoadConfig();
                using (var connection = ConectaBanco.Conectar(config))
                {
                    var optionsBuilder = new DbContextOptionsBuilder<Context>();
                    optionsBuilder.UseSqlServer(connection);

                    using (var context = new Context(optionsBuilder.Options))
                    {
                        context.Database.Migrate();
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}
