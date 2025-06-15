using System;
using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Configuration;

namespace wpfdotnet;

public partial class App : Application
{
  private readonly IConfiguration _configuration;

  public App()
  {
    var builder = new ConfigurationBuilder()
          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

    _configuration = builder.Build();
  }     
  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);
    
    try
    {
        MainWindow = new MainWindow(_configuration);
        MainWindow.Show(); 
    }
    catch (Exception ex)
    {
        System.Windows.MessageBox.Show($"Erreur lors de Show(): {ex.Message}");
    }
  }             
}

