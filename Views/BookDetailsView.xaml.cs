using Books.ViewModels;

namespace Books.Views;

public partial class BookDetailsView : ContentPage
{
	public BookDetailsView(BookDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}