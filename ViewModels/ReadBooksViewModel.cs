using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Books.Services;
using Books.DbModels;
using Books.Views;

namespace Books.ViewModels;

public partial class ReadBooksViewModel : BaseViewModel
{
    private readonly DbService _dbService;

    [ObservableProperty] private ObservableCollection<DbBook> readBooks = [];

    [ObservableProperty] private bool isBusy;

    public ReadBooksViewModel(DbService dbService)
    {
        _dbService = dbService;
        ReadBooks = new ObservableCollection<DbBook>();
        LoadReadBooks();
    }

    public async Task LoadReadBooks()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var books = await _dbService.GetBookAsync();
            ReadBooks.Clear();
            foreach (var book in books)
            {
                ReadBooks.Add(book);
            }
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Błąd", $"Wystąpił problem: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task RemoveBook(DbBook book)
    {
        bool result = await Application.Current!.MainPage!.DisplayAlert("Potwierdzenie",
            $"Czy na pewno chcesz usunąć: {book.Title} z listy przeczytanych książek?",
            "Tak",
            "Nie"
            );
        if (result)
        {
            try
            {
                await _dbService.DeleteBookAsync(book);
                LoadReadBooks();
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Błąd", $"Wystąpił problem: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    public async Task ShowReadBookDetails(DbBook book)
    {
        var navigationParameter = new Dictionary<string, object>
    {
        { "DbBook", book }
    };
        try
        {
            await Shell.Current.GoToAsync(nameof(ReadBookDetailsView), true, navigationParameter);
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
