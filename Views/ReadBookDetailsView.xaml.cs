using Books.ViewModels;

namespace Books.Views;

public partial class ReadBookDetailsView : ContentPage
{
	public ReadBookDetailsView(ReadBookDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}