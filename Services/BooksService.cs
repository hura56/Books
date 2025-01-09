using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Books.Models;
using DotNetEnv;
using Books.DbModels;

namespace Books.Services;

public class BooksService
{
    private readonly HttpClient _httpClient;
    private string _apiKey;

    public BooksService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        Env.Load();
        _apiKey = Environment.GetEnvironmentVariable("API_KEY");
    }

    public async Task<List<Book>> SearchBooksAsync(string query)
    {
        var url = $"https://www.googleapis.com/books/v1/volumes?q={query}&maxResults=40&key={_apiKey}";
        var response = await _httpClient.GetAsync(url);
        var jsonResponse = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var data = JsonSerializer.Deserialize<BooksResponse>(jsonResponse, options);

        var books = new List<Book>();
        if (data?.Items != null)
        {
            foreach (var item in data.Items)
            {
                books.Add(new Book
                {
                    Id = item.Id,
                    Title = item.VolumeInfo?.Title,
                    Authors = item.VolumeInfo?.Authors != null
                        ? string.Join(", ", item.VolumeInfo.Authors)
                        : "Brak autora",
                    Description = item.VolumeInfo?.Description ?? "Brak opisu",
                    PublishedDate = item.VolumeInfo?.PublishedDate ?? "Data publikacji nieznana",
                    Thumbnail = item.VolumeInfo?.ImageLinks?.Thumbnail ?? "http://katalog.tarnowiec.eu:8081/assets/okladki/isbn/brakokladki.jpg"
                });
            }
        }

        return books;
    }

    public static DbBook ToDbBook(Book book) => new DbBook
    {
        GoogleId = book.Id,
        Title = book.Title,
        Authors = book.Authors,
        Description = book.Description,
        PublishedDate = book.PublishedDate,
        Thumbnail = book.Thumbnail,
    };
}
