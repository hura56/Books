using Books.ViewModels;

namespace Books.Views;

public partial class MainView : ContentPage
{
	public MainView(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}