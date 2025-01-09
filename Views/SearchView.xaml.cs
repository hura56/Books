using Books.ViewModels;

namespace Books.Views;

public partial class SearchView : ContentPage
{
	public SearchView(SearchViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}