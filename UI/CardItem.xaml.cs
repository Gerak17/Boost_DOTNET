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
            if (DataContext is Artpiece artpiece)
            {
                (Window.GetWindow(this) as MainWindow)?.Edit_Click(artpiece);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Artpiece artpiece)
            {
                (Window.GetWindow(this) as MainWindow)?.Delete_Click(artpiece);
            }
        }
    }
}