using ItemManagment.Models.DataTransferModels;
using ItemManagment.Models.DbContexts;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ItemManagment.Services
{
    public static class DatabaseMigrationService
    {
        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            try
            {
                using (var serverScope = app.ApplicationServices.CreateScope())
                {
                    var db = serverScope.ServiceProvider.GetService<ItemDbContext>()?.Database;
                    if (db.EnsureCreated())
                    {
                        Log.Error($"----------NEW DATABASE CREATED---------{db}");
                    }
                    db.Migrate();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while migrating the database.");
            }
            
        }
    }
}
