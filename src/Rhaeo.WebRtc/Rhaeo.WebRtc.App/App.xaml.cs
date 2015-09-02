using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Rhaeo.WebRtc.App
{
  sealed partial class App
    : Application
  {
    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="e">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
      var rootFrame = Window.Current.Content as Frame;
      if (rootFrame == null)
      {
        rootFrame = new Frame();
        Window.Current.Content = rootFrame;
      }

      if (rootFrame.Content == null)
      {
        var ice = new Ice.Ice();
        var sdp = new Sdp.Sdp(ice);
        rootFrame.Content = sdp;
      }

      Window.Current.Activate();
    }
  }
}
