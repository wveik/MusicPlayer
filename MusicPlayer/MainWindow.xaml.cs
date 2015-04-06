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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace MusicPlayer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private readonly string PLAY_IMAGE = @".\Asserts\play.png";
        private readonly string PAUSE_IMAGE = @".\Asserts\pause.png";

        public MainWindow() {
            InitializeComponent();
        }

        private void Play(object sender, RoutedEventArgs args) {
            ToggleButton tb = (ToggleButton)sender;
            if (tb.IsChecked.Value) {
                MyMediaElement.Play();

                PlayImage.Source = GetImageForButton(PAUSE_IMAGE);
            } else {
                MyMediaElement.Pause();

                PlayImage.Source = GetImageForButton(PLAY_IMAGE);
            }
        }

        private BitmapImage GetImageForButton(string path) {

            BitmapImage result = new BitmapImage();
            result.BeginInit();
            result.UriSource = new Uri(path, UriKind.Relative);
            result.EndInit();

            return result;
        }
    }
}
