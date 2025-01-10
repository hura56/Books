using Books.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Models;
using CommunityToolkit.Mvvm.Input;
using System.Linq.Expressions;
using Books.ViewModels;
using Books.Views;

namespace Books.ViewModels;

public partial class SearchViewModel : BaseViewModel
{
    private readonly BooksService _booksService;

    [ObservableProperty] private string searchQuery;
    [ObservableProperty] ObservableCollection<Book> books;
    [ObservableProperty] bool isBusy;

    public SearchViewModel(BooksService booksService)
    {
        _booksService = booksService;
        Books = new ObservableCollection<Book>();
    }

    [RelayCommand]
    public async Task SearchBooks()
    {
        if (string.IsNullOrEmpty(searchQuery))
            return;

        try
        {
            IsBusy = true;
            Books.Clear();
            var results = await _booksService.SearchBooksAsync(SearchQuery);
            foreach (var book in results)
            {
                Books.Add(book);
            }
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", $"Nieoczekiwany błąd: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false; 
        }
    }

    [RelayCommand]
    public async Task ShowBookDetails(Book book)
    {
        var navigationParameter = new Dictionary<string, object>
    {
        { "Book", book }
    };
        try
        {
            await Shell.Current.GoToAsync(nameof(BookDetailsView), true, navigationParameter);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas nawigacji: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("//MainView");
    }
}
