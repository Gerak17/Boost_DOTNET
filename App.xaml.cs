using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace wpfdotnet;

public partial class App : Application
{
  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    System.Windows.MessageBox.Show("Avant création MainWindow");

    try
    {
        MainWindow = new MainWindow();
        MainWindow.Show(); 
    }
    catch (Exception ex)
    {
        System.Windows.MessageBox.Show($"Erreur lors de Show(): {ex.Message}");
    }
  }             
}

