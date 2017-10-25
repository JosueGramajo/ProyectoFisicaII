using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoFisicaII
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public static int graphicNumber;

        public ResultWindow()
        {
            InitializeComponent();
        }
        public ResultWindow(String field, String degree, int graphicNumber) {
            InitializeComponent();

            lblField.Content = "E = " + field;
            lblDegree.Content = "θ = " + degree;

            placeGraphic(graphicNumber);
        }
        private void placeGraphic(int graphic) { 
            switch(graphic){
                case 1:
                    graphicImage.Source = new BitmapImage(new Uri("Content/assets/graphics/1.png", UriKind.Relative));
                    break;
                case 2:
                    graphicImage.Source = new BitmapImage(new Uri("Content/assets/graphics/2.png", UriKind.Relative));
                    break;
                case 3:
                    graphicImage.Source = new BitmapImage(new Uri("Content/assets/graphics/3.png", UriKind.Relative));
                    break;
                case 4:
                    graphicImage.Source = new BitmapImage(new Uri("Content/assets/graphics/4.png", UriKind.Relative));
                    break;
                default:
                    graphicImage.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
