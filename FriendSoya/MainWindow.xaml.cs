using FriendSoya.Scripts;
using FriendSoya.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FriendSoya
{
    public partial class MainWindow : Window
    {
        private bool _isInReactOnClick;

        private ChatWidget _chatWidget = new ChatWidget();

        public MainWindow()
        {
            InitializeComponent();
            CreateAndPlayAnimHeartIdle();
        }

        //---События
        //События Grid
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Перемещение окна при нажатии ЛКМ
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private async void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isInReactOnClick)
                await PlayReactAnim(2);
        }

        private async void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContextPopup.IsOpen = true;
            await FixPopupAutoClose(ContextPopup);
        }

        //События кнопок контекстного меню попапа
        private void ContextChatButton_Click(object sender, RoutedEventArgs e)
        {
            ContextPopup.IsOpen = false;

            _chatWidget.Left = Left;
            _chatWidget.Top = Top;
            _chatWidget.Show();
            _chatWidget.Focus();
        }

        private void ContextTopMostButton_Click(object sender, RoutedEventArgs e)
        {
            Topmost = !Topmost;

            var button = (Button)sender;
            button.Content = Topmost ? "Поверх всех окон" : "Обычное окно";
        }

        private void ContextAboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("FriendSoya. Version 1.0", "О программе");
        }

        private void ContextPopup_Closed(object sender, EventArgs e)
        {
            ContextPopup.StaysOpen = true;
        }



        //---Методы
        private async Task PlayReactAnim(double seconds)
        {
            _isInReactOnClick = true;
            var miliseconds = (int)(seconds * 1000);
            
            CreateAndPlayAnimHeartOnClickReact();

            await Task.Delay(miliseconds);

            _isInReactOnClick = false;
            CreateAndPlayAnimHeartIdle();
        }

        //Работа с анимациями, перенесу в класс Animator
        private void CreateAndPlayAnimHeartIdle()
        {
            //Меняем основное изображение
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
            //Меняем основное изображение
            gjinImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/gjinSmile.png"));

            //Анимация изменения размера
            var pulseSizeHeartAnim = AnimationCreator.CreateScaleDoubleAnim(0.8, 1.5, TimeSpan.FromSeconds(0.5), true, true);
            //Анимация изменения цвета
            var pulseColorHeartAnim = AnimationCreator.CreateColorDoubleAnim(Colors.LightGoldenrodYellow, TimeSpan.FromSeconds(0.5), true, true);

            heartScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, pulseSizeHeartAnim);
            heartScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, pulseSizeHeartAnim);
            heartGradientColor.BeginAnimation(GradientStop.ColorProperty, pulseColorHeartAnim);
        }

        private async Task FixPopupAutoClose(Popup popUp)
        {
            await Task.Delay(100);

            popUp.StaysOpen = false;
        }
    }
}