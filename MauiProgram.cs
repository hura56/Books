using CommunityToolkit.Maui;
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
            builder.Services.AddHttpClient<BooksService>();

            builder.Services.AddSingleton<DbService>();

            builder.Services.AddTransient<SearchView>();
            builder.Services.AddTransient<SearchViewModel>();

            builder.Services.AddTransient<BookDetailsView>();
            builder.Services.AddTransient<BookDetailsViewModel>();

            builder.Services.AddTransient<ReadBooksView>();
            builder.Services.AddTransient<ReadBooksViewModel>();

            builder.Services.AddTransient<ReadBookDetailsView>();
            builder.Services.AddTransient<ReadBookDetailsViewModel>();

            return builder.Build();
        }
    }
}
