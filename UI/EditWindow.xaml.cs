using System;
using System.Windows;
using wpfdotnet.Model;

namespace wpfdotnet.UI
{

    public partial class EditWindow : Window
    {

        private readonly Artpiece? _artpiece;
        private readonly Action<Artpiece>? _onSaveAction;
        
        // Constructeur pour la modification
        public EditWindow(Artpiece artpiece, Action<Artpiece> onSave)
        {
            InitializeComponent();

            _artpiece = artpiece ?? throw new ArgumentNullException(nameof(artpiece));
            _onSaveAction = onSave ?? throw new ArgumentNullException(nameof(onSave));

            TitleBox.Text = artpiece.Title;
            ArtistBox.Text = artpiece.Artist;
            DescriptionBox.Text = artpiece.Description;
            ImageUrlBox.Text = artpiece.ImageUrl;
        }

        // Constructeur pour l'ajout
        public EditWindow(Action<string, string, string, string> onSave)
        {
            InitializeComponent();

            SaveButton.Click += (s, e) =>
            {
                onSave(TitleBox.Text, ArtistBox.Text, DescriptionBox.Text, ImageUrlBox.Text);
                this.Close();
            };
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_artpiece == null) return;

            _artpiece.Title = TitleBox.Text;
            _artpiece.Artist = ArtistBox.Text;
            _artpiece.Description = DescriptionBox.Text;
            _artpiece.ImageUrl = ImageUrlBox.Text;

            _onSaveAction?.Invoke(_artpiece);
            this.Close();
        }
    }
}