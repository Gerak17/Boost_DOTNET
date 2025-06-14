using System.Windows;
using System.Windows.Controls;
using wpfdotnet.Service;
using wpfdotnet.Model;

namespace wpfdotnet.UI
{

    public partial class MainWindow : Window
    {
        private readonly ArtpieceService _service = new();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
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
}