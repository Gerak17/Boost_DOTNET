using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

using Microsoft.Extensions.Configuration;

using wpfdotnet.Model;
using wpfdotnet.Service;
using wpfdotnet.UI;
using wpfdotnet.Data;

namespace wpfdotnet;

public partial class MainWindow : Window
{
    private readonly ArtpieceService _service;
    private ObservableCollection<Artpiece> _artpieces = new();

    public MainWindow(IConfiguration configuration)
    {
        try
        {
            InitializeComponent();

            var dbContext = new AppDbContext(configuration);
            _service = new ArtpieceService(dbContext);

            LoadData();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Erreur dans MainWindow: {ex.Message}");
        }
    }

    private void LoadData()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            var items = _service.GetAll();
            _artpieces.Clear();

            foreach (var item in items)
            {
                _artpieces.Add(item);
            }

            CardList.ItemsSource = _artpieces; 
        });
        
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        var editWindow = new EditWindow((title, artist, description, imageUrl) =>
        {
                
            var artpiece = new Artpiece
            {
                Title = title,
                Artist = artist,
                Description = description,
                ImageUrl = imageUrl
            };

            _service.Add(artpiece);  
            LoadData();             
        });

        editWindow.ShowDialog();
    }

    public void Edit_Click(Artpiece artpiece)
    {
        var editWindow = new EditWindow(artpiece, updated =>
        {
            _service.Update(updated);
            LoadData();
        });
        editWindow.ShowDialog();
    }

    public void Delete_Click(Artpiece artpiece)
    {
        if (MessageBox.Show("Supprimer cette œuvre ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            _service.Delete(artpiece.Id);
            LoadData();
        }
    }    
}