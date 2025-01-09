﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Books.ViewModels;
using Books.Views;
using Books.Services;

namespace Books
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient<BooksService>(client =>
            {
                client.BaseAddress = new Uri("https://www.googleapis.com/books/v1/");
            });

            builder.Services.AddTransient<MainView>();
            builder.Services.AddTransient<MainViewModel>();

            builder.Services.AddTransient<SearchView>();
            builder.Services.AddTransient<SearchViewModel>();

            return builder.Build();
        }
    }
}
