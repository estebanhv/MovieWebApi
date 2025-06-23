using MovieDesktopApp.data.dto;
using MovieDesktopApp.services;
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

namespace MovieDesktopApp.View
{
    /// <summary>
    /// Lógica de interacción para SearchMovie.xaml
    /// </summary>
    public partial class SearchMovie : Window
    {

        private readonly MovieService _movieService;

        public SearchMovie()
        {
            InitializeComponent();
            _movieService = new MovieService();

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string title = SearchBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Por favor, ingresa un título.");
                return;
            }

            try
            {
                List<MovieDTO> movies = await _movieService.SearchMoviesAsync(title);
                MovieListPanel.ItemsSource = movies;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }
    }
}
