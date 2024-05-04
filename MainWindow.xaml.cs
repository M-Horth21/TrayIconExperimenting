using System.ComponentModel;
using System.Windows;

namespace TrayIconExperimenting
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    // Minimize to system tray when application is closed.
    protected override void OnClosing(CancelEventArgs e)
    {
      // setting cancel to true will cancel the close request
      // so the application is not closed
      e.Cancel = true;

      this.Hide();

      base.OnClosing(e);
    }
  }
}
