using BlazorAppServerLab.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppServerLab.Services;

public class MyNoteService : IMyNoteService
{
    public MyNoteService(MyNoteDbContext myNoteDbContext)
    {
        MyNoteDbContext = myNoteDbContext;
    }

    public MyNoteDbContext MyNoteDbContext { get; }
    public List<MyNote> MyNotes { get; set; }

    public async Task CreateAsync(MyNote myNote)
    {
        await MyNoteDbContext.MyNotes.AddAsync(myNote);
        await MyNoteDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(MyNote myNote)
    {
        var item = await MyNoteDbContext.MyNotes.FirstOrDefaultAsync(x => x.Id == myNote.Id);
        if (item != null)
        {
            MyNoteDbContext.MyNotes.Remove(item);
            await MyNoteDbContext.SaveChangesAsync();
        }
    }

    public async Task<List<MyNote>> RetrieveAsync()
    {
        return await MyNoteDbContext.MyNotes.ToListAsync();
    }

    public async Task UpdateAsync(MyNote origMyNote, MyNote myNote)
    {
        var item = await MyNoteDbContext.MyNotes.FirstOrDefaultAsync(x => x.Id == origMyNote.Id);
        if (item != null)
        {
            item.Title = myNote.Title;
            await MyNoteDbContext.SaveChangesAsync();
        }
    }
}