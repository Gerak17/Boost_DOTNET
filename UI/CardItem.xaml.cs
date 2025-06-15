using System.Windows;
using System.Windows.Controls;
using wpfdotnet.Model;

namespace wpfdotnet.UI
{
    public partial class CardItem : UserControl
    {
        public CardItem()
        {
            InitializeComponent();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var artpiece = DataContext as Artpiece;
            var window = Window.GetWindow(this) as MainWindow;
            window?.Edit_Click(artpiece);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var artpiece = DataContext as Artpiece;
            var window = Window.GetWindow(this) as MainWindow;
            window?.Delete_Click(artpiece);
        }
    }
}