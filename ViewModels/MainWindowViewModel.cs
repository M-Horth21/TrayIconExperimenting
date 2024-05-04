using System.Drawing;
using System.Windows.Input;
using TrayIconExperimenting.Commands;
using Forms = System.Windows.Forms;

namespace TrayIconExperimenting.ViewModels
{
  internal class MainWindowViewModel : BaseViewModel
  {
    enum Status
    {
      OK,
      Warning,
      Error
    }

    readonly Forms.NotifyIcon _notifyIcon;
    Status _currentStatus = Status.OK;

    public MainWindowViewModel(Forms.NotifyIcon icon)
    {
      // Store provided NotifyIcon reference.
      _notifyIcon = icon;

      // Prepare button commands.
      SetStatusOK = GenerateCommand(Status.OK);
      SetStatusWarning = GenerateCommand(Status.Warning);
      SetStatusError = GenerateCommand(Status.Error);
    }

    /// <summary>
    /// Creates a new <see cref="RelayCommand"/> that sets the NotifyIcon
    /// to the provided <paramref name="status"/>.
    /// </summary>
    /// <param name="status">A <see cref="Status"/> option.</param>
    /// <returns>A new <see cref="RelayCommand"/>.</returns>
    RelayCommand GenerateCommand(Status status)
    {
      return new RelayCommand(
        executeAction: () =>
        {
          ChangeStatus(status);
          ShowBalloonTip(status);
        },
        canExecute: () => _currentStatus != status);
    }

    public ICommand SetStatusOK { get; set; }
    public ICommand SetStatusWarning { get; set; }
    public ICommand SetStatusError { get; set; }

    void ChangeStatus(Status status)
    {
      _currentStatus = status;

      string iconFilePath = "Resources/" + _currentStatus switch
      {
        Status.Warning => "Warning.ico",
        Status.Error => "Error.ico",
        _ => "OK.ico",
      };

      _notifyIcon.Icon = new Icon(iconFilePath);
    }

    void ShowBalloonTip(Status status)
    {
      string balloonTipMessage = status switch
      {
        Status.Warning => "Oh no, shit's about to get bad!",
        Status.Error => "I pooped my pants!",
        _ => "This is fine."
      };

      _notifyIcon.ShowBalloonTip(
        2000,
        "Horthapotamus",
        balloonTipMessage,
        Forms.ToolTipIcon.None);
    }
  }
}