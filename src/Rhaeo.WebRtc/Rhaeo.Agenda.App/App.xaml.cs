using Rhaeo.Agenda.ViewModels;
using Rhaeo.Agenda.Views;
using System;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Rhaeo.Agenda.App
{
  sealed partial class App
    : Application
  {
    /// <summary>
    /// Invoked when the application is launched normally by the end user. Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="e">Details about the launch request and process.</param>
    protected override async void OnLaunched(LaunchActivatedEventArgs e)
    {
      var rootFrame = Window.Current.Content as Frame;
      if (rootFrame == null)
      {
        rootFrame = new Frame();
        Window.Current.Content = rootFrame;
      }

      if (rootFrame.Content == null)
      {
        try
        {
          await Agenda.Run();
          rootFrame.Navigate(typeof(IndexView), e.Arguments);
        }
        catch (Exception exception)
        {
          rootFrame.Navigate(typeof(ErrorView), new ErrorViewModel(exception));
        }
      }

      Window.Current.Activate();
    }
  }
}
