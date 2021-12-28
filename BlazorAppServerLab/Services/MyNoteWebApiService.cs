using System.Net.Mime;
using System.Text;
using System.Text.Json;
using BlazorAppServerLab.Models;

namespace BlazorAppServerLab.Services;

public class MyNoteWebApiService : IMyNoteService
{
    private readonly HttpClient _httpClient;

    public MyNoteWebApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task CreateAsync(MyNote myNote)
    {
        await _httpClient.PostAsync("api/MyNote",
            new StringContent(JsonSerializer.Serialize(myNote),
                Encoding.UTF8, MediaTypeNames.Application.Json)
        );
    }

    public async Task DeleteAsync(MyNote myNote)
    {
        await _httpClient.DeleteAsync($"api/MyNote/{myNote.Id}");
    }

    public async Task<List<MyNote>> RetrieveAsync()
    {
        return (await _httpClient.GetFromJsonAsync<List<MyNote>>("api/MyNote"))!;
    }

    public async Task UpdateAsync(MyNote origMyNote, MyNote myNote)
    {
        await _httpClient.PutAsync($"api/MyNote/{myNote.Id}",
            new StringContent(JsonSerializer.Serialize(myNote),
                Encoding.UTF8, MediaTypeNames.Application.Json)
        );
    }
}