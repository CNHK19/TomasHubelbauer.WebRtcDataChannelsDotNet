using Rhaeo.Agenda.ViewModels;
using Rhaeo.WebRtc.Qr.Extensions;
using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Rhaeo.Agenda.Views
{
  public sealed partial class IndexView
    : Page
  {
    #region Constructors

    public IndexView()
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

    #region Handlers

    private async void StartAgendaWebButton_Click(object sender, RoutedEventArgs e)
    {
      var localDescription = await Agenda.PeerConnection.LocalDescription;
      var width = 700;
      var height = 700;

      var dataUri = await localDescription.ToQrDataUri(width, height);
      await Launcher.LaunchUriAsync(new Uri($"http://localhost:60289/?overrideUserMedia={dataUri}"));

      var offerViewModel = new OfferViewModel(localDescription.ToQrImageSource(width, height), width, height);
      Frame.Navigate(typeof(OfferView), offerViewModel);
    }

    #endregion
  }
}
