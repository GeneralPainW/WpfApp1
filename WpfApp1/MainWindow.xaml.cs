using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static System.Windows.Threading.DispatcherTimer playbackTimer;
        public MainWindow()
        {
            InitializeComponent();
            playbackTimer = new System.Windows.Threading.DispatcherTimer();
            playbackTimer.Tick += new EventHandler(OnTimedEvent);
            playbackTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);

        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            double total = media.NaturalDuration.TimeSpan.TotalSeconds;
            double elapsed = media.Position.TotalSeconds;
            double percent = elapsed / total * 100;
            pbPosition.Value = percent;

        }

        private void pbOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = ""; // Default file name
            dlg.DefaultExt = ".mp3"; // Default file extension
            dlg.Filter = "Music files (.m4a)|*.m4a; *.mp3; *.mp4; *.FLAC"; // Filter files by extension


            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                lblFilename.Content = filename.Split('\\').Last();
                media.LoadedBehavior = MediaState.Manual;
                media.Source = new System.Uri(filename);
                playbackTimer.Start();
                media.Play();
                media.Volume = 0.05;
                pbVolume.Value = 5;

                //double totalSecond = media.NaturalDuration.TimeSpan.TotalSeconds;

                //lblPlayTime.Content = String.Format("{0:D2}:{1:D2}:{2:D2}", totalSecond, totalSecond,totalSecond);
                //double totalMinute = totalSecond / 60;
                //lblPlayTime.Content = totalMinute;
            }
        }

        private void pbPlay_Click(object sender, RoutedEventArgs e)
        {
            media.Play();
        }

        private void pbPause_Click(object sender, RoutedEventArgs e)
        {
            media.Pause();
        }

        private void pbMute_Click(object sender, EventArgs e)
        {
            media.IsMuted = !media.IsMuted;
            if (media.IsMuted == false)
            {
                var uriImageSource = new Uri(@"C:\Users\Ульяна\Desktop\WpfApp1\WpfApp1\034-volume-adjustment.png", UriKind.RelativeOrAbsolute);
                imgMute.Source = new BitmapImage(uriImageSource);
            }
            else
            {
                var uriImageSource = new Uri(@"C:\Users\Ульяна\Desktop\WpfApp1\WpfApp1\030-mute.png", UriKind.RelativeOrAbsolute);
                imgMute.Source = new BitmapImage(uriImageSource);
            }


        }


        private void pbPosition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double clickPixel = e.GetPosition(pbPosition).X;
            double fullLength = pbPosition.ActualWidth;
            double percent = clickPixel / fullLength * 100;
            double totalSecond = media.NaturalDuration.TimeSpan.TotalSeconds;
            double newSecond = totalSecond / 100 * percent;

            media.Position = new TimeSpan(0, 0, (int)newSecond);
            pbPosition.Value = percent;

        }

        private void pbVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double sliderVolume = pbVolume.Value;
            double speakerVolume = sliderVolume / 100;
            media.Volume = speakerVolume;
        }

        private void pbBalance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double sliderBalance = pbBalance.Value;
            double speakerBalance = sliderBalance / 100;
            media.Balance = speakerBalance;
        }

        private void lblVolume_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            media.Volume = 0.2;
            pbVolume.Value = 20;
        }

        private void lblBalance_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            media.Balance = 0;
            pbBalance.Value = 0;
        }

      

        private void pbSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double sliderSpeed = pbSpeed.Value;
            double speakerSpeed = sliderSpeed / 2;
            media.SpeedRatio = speakerSpeed;
        }

        private void lblSpeed_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            media.SpeedRatio = 1;
            pbSpeed.Value = 2;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //media.SpeedRatio = 0.5;
            //pbSpeed.Value = 1;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            //media.SpeedRatio = 1;
            //pbSpeed.Value = 2;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            media.SpeedRatio = 1.5;
            pbSpeed.Value = 3;
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            media.SpeedRatio = 2;
            pbSpeed.Value = 4;
        }
    }
}
