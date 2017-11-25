using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ViewportConverter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _mainWindow;

        public void Application_Startup(object sender, StartupEventArgs e)
        {
            _mainWindow = new MainWindow();
            // Global exception handling  
            Application.Current.DispatcherUnhandledException += ShowUnhandledException;

            _mainWindow.Show();
        }

        private void ShowUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            _mainWindow?.DisplayException(e.Exception);
            //if (ShowErrorNoitificationMessageBox(e.Exception) == MessageBoxResult.No)
            //{
            //    Application.Current.Shutdown();
            //}
            //else
            //{
            //    _mainWindow?.DisplayException(e.Exception);
            //}
        }

        private MessageBoxResult ShowErrorNoitificationMessageBox(Exception exception)
        {
            string errorMessage =
                $"Обнаружена ошибка в программе\n" +
                $"Проверьте верность своих данных в программе. " +
                $"Если ошибка повторяется часто, то лучше всего закрыть ее и написать разработчикам, чтобы пофиксили. \n\n" +
                $"Ошибка: {exception.Message + (exception.InnerException != null ? "\n" + exception.InnerException.Message : null)}\n\n" +
                $"Хотите продолжить работать в программе?\n" +
                $"(Если Вы закроете программу, то данные не сохранятся. Никогда)";
            return ShowMessageBoxAndGetResult(errorMessage, "Ошибка программы");
        }

        private MessageBoxResult ShowMessageBoxAndGetResult(string errorMessage, string messageBoxTitle)
        {
            return MessageBox.Show(errorMessage,
                messageBoxTitle,
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Error);
        }
    }
}
