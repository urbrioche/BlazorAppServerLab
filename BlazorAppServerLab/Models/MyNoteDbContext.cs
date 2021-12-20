using Microsoft.EntityFrameworkCore;

namespace BlazorAppServerLab.Models;

public class MyNoteDbContext : DbContext
{
    public MyNoteDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<MyNote> MyNotes { get; set; }
}