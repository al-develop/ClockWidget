using System;
using System.Windows;
using System.Windows.Input;

namespace ClockWidget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _mainWindowViewModel = new MainWindowViewModel();
        }


        private void contextClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void contextMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
        }

        private void contextMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _mainWindowViewModel.SaveSettings();
        }


        private void Info_Click(object sender, RoutedEventArgs e)
        {
            var info = new InfoWindow();
            info.Show();
        }
    }
}