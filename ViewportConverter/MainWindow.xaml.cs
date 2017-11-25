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
using ViewportConverter.Logic;

namespace ViewportConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Converter _converter;

        public MainWindow()
        {
            InitializeComponent();
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
            if (textBox.Name == TextBox_WantedWidthPx.Name)
            {
                TextBox_ViewportWidth.Text = _converter.GetAsViewportWidth(textBox.Text);
            }
            else if (textBox.Name == TextBox_WantedHeightPx.Name)
            {
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
    }
}
