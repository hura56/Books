using Books.ViewModels;

namespace Books.Views;

public partial class ReadBooksView : ContentPage
{
	private readonly ReadBooksViewModel _viewModel;
	public ReadBooksView(ReadBooksViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.LoadReadBooks();
	}
}