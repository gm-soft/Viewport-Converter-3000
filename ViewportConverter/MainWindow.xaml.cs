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
using System.Windows.Threading;
using ViewportConverter.Logic;

namespace ViewportConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Converter _converter;
        private readonly DispatcherTimer _messageClearTimer;

        public MainWindow()
        {
            InitializeComponent();

            _messageClearTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3)
            };
            _messageClearTimer.Tick += (sender, args) =>
            {
                // выключение таймера после первого тика
                _messageClearTimer.IsEnabled = false;
                Textblock_Messages.Text = "";
            };

            _converter = new Converter();

            SetStandardValues();
        }

        /// <summary>
        /// В текстбоксе желаемой ширины изменился текст
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Wanted_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender is TextBox textBox))
                throw new ArgumentException();

            SetStandardValues();
            if (textBox.Name == TextBox_WantedPx.Name)
            {
                TextBox_ViewportWidth.Text = _converter.GetAsViewportWidth(textBox.Text);
                TextBox_ViewportHeight.Text = _converter.GetAsViewportHeight(textBox.Text);
            }
            else 
                throw new ArgumentOutOfRangeException();
        }

        private void SetStandardValues()
        {
            if (!_converter.HasStandardValues()
                && !string.IsNullOrWhiteSpace(TextBox_StandardWidthPx.Text) 
                && !string.IsNullOrWhiteSpace(TextBox_StandardHeightPx.Text))
                _converter.SetStandardValues(
                    pxWidth: TextBox_StandardWidthPx.Text,
                    pxHeight: TextBox_StandardHeightPx.Text);
        }

        private void Button_WidthHeightCopy_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button))
                throw new ArgumentException();

            string textToClipboard;
            if (button.Name == Button_WidthCopy.Name)
            {
                textToClipboard = TextBox_ViewportWidth.Text;
            }
            else if (button.Name == Button_HeightCopy.Name)
            {
                textToClipboard = TextBox_ViewportHeight.Text;
            }
            else
                throw new ArgumentOutOfRangeException();

            if (string.IsNullOrWhiteSpace(textToClipboard))
                return;

            Clipboard.SetText(textToClipboard);
            DisplayMessage($"Значение \"{textToClipboard}\" скопировано в буфер обмена. Приятного использования =)");
        }

        public void DisplayException(Exception exception)
        {
            DisplayMessage($"Ошибка: {exception.Message + (exception.InnerException != null ? "\n" + exception.InnerException.Message : null)}");
        }

        public void DisplayMessage(string message)
        {
            if (_messageClearTimer.IsEnabled)
                _messageClearTimer.IsEnabled = false;

            Textblock_Messages.Text = message;
            // старт таймера очищения
            _messageClearTimer.IsEnabled = true;
        }
    }
}
