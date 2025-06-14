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

using wpfdotnet.Model;
using wpfdotnet.Service;
using wpfdotnet.UI;

namespace wpfdotnet;

public partial class MainWindow : Window
{
    private readonly ArtpieceService _service = new();

    public MainWindow()
    {
        try
        {
            InitializeComponent();
            LoadData();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Erreur dans MainWindow: {ex.Message}");
        }
    }

    private void LoadData()
    {
        CardList.ItemsSource = _service.GetAll();
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

    private void Edit_Click(Artpiece artpiece)
    {
        var editWindow = new EditWindow(artpiece, updated =>
        {
            _service.Update(updated);
            LoadData();
        });
        editWindow.ShowDialog();
    }

    private void Delete_Click(Artpiece artpiece)
    {
        if (MessageBox.Show("Supprimer cette œuvre ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            _service.Delete(artpiece.Id);
            LoadData();
        }
    }    
}