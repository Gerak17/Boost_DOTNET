using System;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Extensions.Configuration;
using wpfdotnet.Data;
using wpfdotnet.Model;
using wpfdotnet.Service;
using wpfdotnet.UI;

namespace wpfdotnet
{
    public partial class MainWindow : Window
    {
        private readonly ArtpieceService _service;
        public ObservableCollection<Artpiece> Artpieces { get; } = new ObservableCollection<Artpiece>();

        public MainWindow(IConfiguration configuration)
        {
            InitializeComponent();
            var dbContext = new AppDbContext(configuration);
            _service = new ArtpieceService(dbContext);
            foreach (var item in _service.GetAll())
            {
                Artpieces.Add(item);
            }
            DataContext = this;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var editWindow = new EditWindow(_service);
                editWindow.ShowDialog();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ouverture : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Edit_Click(Artpiece? artpiece)
        {
            if (artpiece == null) return;
            try
            {
                var editWindow = new EditWindow(_service, artpiece);
                editWindow.ShowDialog();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'édition : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Delete_Click(Artpiece? artpiece)
        {
            if (artpiece == null) return;
            try
            {
                var result = MessageBox.Show($"Voulez-vous supprimer '{artpiece.Title}'?",
                    "Confirmer la suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _service.Delete(artpiece.Id);
                    RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshData()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Artpieces.Clear();
                foreach (var item in _service.GetAll())
                {
                    Artpieces.Add(item);
                }
            });
        }
    }
}