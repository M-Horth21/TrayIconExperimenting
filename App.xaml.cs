﻿using System.Windows;
using TrayIconExperimenting.ViewModels;
using Forms = System.Windows.Forms;

namespace TrayIconExperimenting
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    // The notify icon exists at the application level.
    Forms.NotifyIcon _notifyIcon = new()
    {
      Text = "Horthapotamus",
      Visible = true,
      Icon = new("Resources/OK.ico"),
    };

    void Application_Startup(object sender, StartupEventArgs e)
    {
      // This is the chosen method to pass a reference to the notify icon.
      var mainWindow = new MainWindow();
      mainWindow.DataContext = new MainWindowViewModel(_notifyIcon);
      mainWindow.Show();

      _notifyIcon.Click += NotifyIconClicked;
      _notifyIcon.ContextMenuStrip = new();

      var quitButton = new Forms.ToolStripButton("Quit");
      quitButton.Click += OnQuitButtonClicked;
      _notifyIcon.ContextMenuStrip.Items.Add(quitButton);
    }

    void OnQuitButtonClicked(object? sender, System.EventArgs e)
    {
      Current.Shutdown();
    }

    void NotifyIconClicked(object? sender, System.EventArgs e)
    {
      MainWindow.WindowState = WindowState.Normal;
      MainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
      _notifyIcon.Dispose();

      base.OnExit(e);
    }
  }
}