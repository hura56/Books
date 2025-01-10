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
            await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił problem: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task RemoveBook(DbBook book)
    {
        try
        {
            await _dbService.DeleteBookAsync(book);
            await Application.Current!.MainPage!.DisplayAlert("Sukces", $"Usunięto książkę: {book.Title} z listy przeczytanych", "OK");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Błąd", $"Wystąpił problem: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    public async Task ShowReadBookDetails(DbBook book)
    {
        // logika wyswietlania szczegolow przeczytanej ksiazki, dodac nowy ViewModel i View dla przeczytanej ksiazki
        await Application.Current!.MainPage!.DisplayAlert("Sukces", $"Wybrałeś: {book.Title} z listy przeczytanych", "OK");
    }
}
