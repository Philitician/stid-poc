using Microsoft.EntityFrameworkCore;
using stid_management.Models;

namespace stid_management.Data;

public class StidContext : DbContext
{
    public StidContext(DbContextOptions<StidContext> options) : base(options) { }
    
    public DbSet<Group> Stid { get; set; }
    public DbSet<AppRegistration> AppRegistrations { get; set; }
    
    // public string DbPath { get; }
    // public StidContext()
    // {
    //     var folder = Environment.SpecialFolder.LocalApplicationData;
    //     var path = Environment.GetFolderPath(folder);
    //     DbPath = Path.Join(path, "stid.db");
    // }
    //
    // // The following configures EF to create a Sqlite database file in the
    // // special "local" folder for your platform.
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlite($"Data Source={DbPath}");
    //
}