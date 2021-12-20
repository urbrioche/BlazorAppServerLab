using BlazorAppServerLab.Models;

namespace BlazorAppServerLab.Services;

public interface IMyNoteService
{
    Task CreateAsync(MyNote myNote);
    Task DeleteAsync(MyNote myNote);
    Task<List<MyNote>> RetrieveAsync();
    Task UpdateAsync(MyNote origMyNote, MyNote myNote);
}