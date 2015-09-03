using Windows.UI.Xaml.Media;

namespace Rhaeo.Agenda.ViewModels
{
  public sealed class OfferViewModel
  {
    #region Constructors

    public OfferViewModel(ImageSource imageSource, int width, int height)
    {
      ImageSource = imageSource;
      Width = width;
      Height = height;
    }

    #endregion

    #region Properties

    public ImageSource ImageSource { get; }

    public int Width { get; }

    public int Height { get; }

    #endregion
  }
}
