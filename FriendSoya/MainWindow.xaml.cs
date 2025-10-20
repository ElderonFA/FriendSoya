using FriendSoya.Scripts;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FriendSoya
{
    public partial class MainWindow : Window
    {
        private bool _isInReactOnClick;

        public MainWindow()
        {
            InitializeComponent();
            CreateAndPlayAnimHeartIdle();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Перемещение окна
            DragMove();
        }

        private async void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isInReactOnClick)
                await PlayReactAnim(2);
        }

        private async Task PlayReactAnim(double seconds)
        {
            _isInReactOnClick = true;
            var miliseconds = (int)(seconds * 1000);
            
            CreateAndPlayAnimHeartOnClickReact();

            await Task.Delay(miliseconds);

            _isInReactOnClick = false;
            CreateAndPlayAnimHeartIdle();
        }

        private void CreateAndPlayAnimHeartIdle()
        {
            gjinImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/gjinIdle.png"));

            //Анимация изменения размера
            var pulseSizeHeartAnim = AnimationCreator.CreateScaleDoubleAnim(0.8, 1.3, TimeSpan.FromSeconds(2), true, true);
            //Анимация изменения цвета
            var pulseColorHeartAnim = AnimationCreator.CreateColorDoubleAnim(Colors.White, TimeSpan.FromSeconds(2), true, true);

            heartScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, pulseSizeHeartAnim);
            heartScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, pulseSizeHeartAnim);
            heartGradientColor.BeginAnimation(GradientStop.ColorProperty, pulseColorHeartAnim);
        }

        private void CreateAndPlayAnimHeartOnClickReact()
        {
            gjinImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/gjinSmile.png"));

            //Анимация изменения размера
            var pulseSizeHeartAnim = AnimationCreator.CreateScaleDoubleAnim(0.8, 1.5, TimeSpan.FromSeconds(0.5), true, true);
            //Анимация изменения цвета
            var pulseColorHeartAnim = AnimationCreator.CreateColorDoubleAnim(Colors.LightGoldenrodYellow, TimeSpan.FromSeconds(0.5), true, true);

            heartScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, pulseSizeHeartAnim);
            heartScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, pulseSizeHeartAnim);
            heartGradientColor.BeginAnimation(GradientStop.ColorProperty, pulseColorHeartAnim);
        }
    }
}