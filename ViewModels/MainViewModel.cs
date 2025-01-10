﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Books.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public MainViewModel()
    {

    }
    
    [RelayCommand]
    public async Task GoToSearch()
    {
        await Shell.Current.GoToAsync("//SearchView");
    }

    [RelayCommand]
    public async Task GoToReadBooks()
    {
        await Shell.Current.GoToAsync("//ReadBooksView");
    }
}
