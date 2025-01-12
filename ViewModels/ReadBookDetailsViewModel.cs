﻿using Books.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.DbModels;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using Books.Popups;

namespace Books.ViewModels;

[QueryProperty(nameof(DbBook), "DbBook")]
public partial class ReadBookDetailsViewModel : BaseViewModel
{
    private readonly DbService _dbService;
    private DbBook _book;
    public DbBook DbBook
    {
        get => _book;
        set
        {
            SetProperty(ref _book, value);
            PagesRead = _book.PagesRead;
        }
    }
    private int _pagesRead;
    public int PagesRead
    {
        get => _pagesRead;
        set => SetProperty(ref _pagesRead, value);
    }

    public ReadBookDetailsViewModel(DbService dbService)
    {
        _dbService = dbService;
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.Navigation.PopAsync();
    }

    [RelayCommand]
    public async Task RemoveBook()
    {
        bool result = await Application.Current!.MainPage!.DisplayAlert("Potwierdzenie",
            $"Czy na pewno chcesz usunąć: {DbBook.Title} z listy przeczytanych książek?",
            "Tak",
            "Nie"
            );
        if (result)
        {
            try
            {
                await _dbService.DeleteBookAsync(DbBook);
                Back();
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Błąd", $"Wystąpił problem: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    public async Task ShowThumbnail()
    {
        Application.Current!.MainPage!.ShowPopup(new ThumbnailPopup(DbBook.Thumbnail));
    }

    [RelayCommand]
    public async Task SavePages()
    {
        try
        {
            if (PagesRead > DbBook.PageCount) 
                PagesRead = DbBook.PageCount;
            DbBook.PagesRead = PagesRead;
            await _dbService.UpdateBookAsync(DbBook);
            await Application.Current!.MainPage!.DisplayAlert("Sukces", "Liczba przeczytanych stron została zapisana!", "OK");
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Błąd", $"Wystąpił problem: {ex.Message}", "OK");
        }
    }
}
