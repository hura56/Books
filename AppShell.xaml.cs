using Books.Views;
using Books.ViewModels;

namespace Books
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(BookDetailsView), typeof(BookDetailsView));
        }
    }
}
