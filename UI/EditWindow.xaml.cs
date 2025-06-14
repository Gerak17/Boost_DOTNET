using System.Windows;
using wpfdotnet.Model;

namespace wpfdotnet.UI
{

    public partial class EditWindow : Window
    {
        private readonly Action<string, string, string, string> _onSave;

        public EditWindow(Artpiece artpiece, Action<Artpiece> onSave)
        {
            InitializeComponent();

            TitleBox.Text = artpiece.Title;
            ArtistBox.Text = artpiece.Artist;
            DescriptionBox.Text = artpiece.Description;
            ImageUrlBox.Text = artpiece.ImageUrl;

            SaveButton.Click += (s, e) =>
            {
                artpiece.Title = TitleBox.Text;
                artpiece.Artist = ArtistBox.Text;
                artpiece.Description = DescriptionBox.Text;
                artpiece.ImageUrl = ImageUrlBox.Text;

                onSave(artpiece);
                this.Close();
            };
        }
    }
}