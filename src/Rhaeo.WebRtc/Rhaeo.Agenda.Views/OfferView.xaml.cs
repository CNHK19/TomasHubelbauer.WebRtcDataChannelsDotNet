using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Rhaeo.Agenda.Views
{
  public sealed partial class OfferView
    : Page
  {
    #region Constructors

    public OfferView()
    {
      InitializeComponent();
    }

    #endregion

    #region Methods

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      DataContext = e.Parameter;
      base.OnNavigatedTo(e);
    }

    #endregion
  }
}
