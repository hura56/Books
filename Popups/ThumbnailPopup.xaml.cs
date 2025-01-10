namespace Books.Popups;

public partial class ThumbnailPopup : CommunityToolkit.Maui.Views.Popup
{
	public ThumbnailPopup(string thumbnailUrl)
	{
		InitializeComponent();
		BindingContext = new { Thumbnail =  thumbnailUrl };
	}
}