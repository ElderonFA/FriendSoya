using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FriendSoya.Windows
{
    public partial class ChatWidget : Window
    {
        public class ChatMessage
        {
            public string Text { get; set; }
            public Brush Background { get; set; }
        }

        private ObservableCollection<ChatMessage> chatMessages = new ObservableCollection<ChatMessage>();

        public ChatWidget()
        {
            InitializeComponent();
            chatItemsControl.ItemsSource = chatMessages;
            AddMessage("Сойя", "Привет, создатель! С чего начнем?", "#3A4FC3F7");
        }

        //--- События ---
        private void closeButton_Click(object sender, RoutedEventArgs e) => Hide();

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }

        private void ScrollViewer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
                e.Handled = true;
            }
        }

        //Отправка сообщения по клику на кнопку отправки
        private async void sendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            await SendMessageToSoya(inputTextBox.Text);
        }

        //Отправка сообщения по нажатию клавиши Enter
        private async void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(inputTextBox.Text))
            {
                await SendMessageToSoya(inputTextBox.Text);
            }
        }

        //--- Методы ---
        //Метод отправления сообщеня Сойе
        private async Task SendMessageToSoya(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            // Добавляем сообщение пользователя
            AddMessage("Вы", message, "#1A4FC3F7");
            inputTextBox.Text = "";
            sendMessageButton.IsEnabled = false;

            // TODO: Здесь будет вызов твоего API
            await Task.Delay(1000); // Имитация задержки сети

            // TODO: Заменить на реальный ответ от API
            var answer = "Это тестовый ответ от Сойи! 🖤";
            AddMessage("Сойя", answer, "#3A4FC3F7");

            sendMessageButton.IsEnabled = true;
            chatScrollViewer.ScrollToBottom();
        }

        //Метод добавления сообщения в чат
        private void AddMessage(string sender, string message, string color)
        {
            chatMessages.Add(new ChatMessage
            {
                Text = $"{sender}: {message}",
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color))
            });
        }
    }
}