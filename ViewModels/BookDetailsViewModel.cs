using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Models;
using CommunityToolkit.Mvvm.Input;

namespace Books.ViewModels;

public partial class BookDetailsViewModel : BaseViewModel
{
    [ObservableProperty] private Book book;

    public BookDetailsViewModel(Book book)
    {
        Book = book;
    }

    [RelayCommand]
    public async Task AddBookToRead()
    {
        await Application.Current!.MainPage!.DisplayAlert("Placeholder", $"Tutaj bedzie logika dodania ksiazki do przeczytanych", "OK");
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.Navigation.PopAsync();
    }
}
