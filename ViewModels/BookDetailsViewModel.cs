using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using Books.Services;
using Books.Popups;

namespace Books.ViewModels;

[QueryProperty(nameof(Book), "Book")]
public partial class BookDetailsViewModel : BaseViewModel
{
    private readonly DbService _dbService;

    private Book _book;
    public Book Book
    {
        get => _book;
        set => SetProperty(ref _book, value);
    }

    public BookDetailsViewModel(DbService dbService)
    {
        _dbService = dbService;
    }

    [RelayCommand]
    public async Task AddBookToRead(Book book)
    {
        try
        {
            var dbBook = BooksService.ToDbBook(book);
            await _dbService.SaveBookAsync(dbBook);
            await Application.Current!.MainPage!.DisplayAlert("Sukces", $"Dodano książke: {book.Title} do listy przeczytanych", "OK");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Błąd", $"Wystąpił problem: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.Navigation.PopAsync();
    }

    [RelayCommand]
    public async Task ShowThumbnail()
    {
        Application.Current!.MainPage!.ShowPopup(new ThumbnailPopup(Book.Thumbnail));
    }
}
