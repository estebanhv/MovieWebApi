using MovieDesktopApp.data.dto;
using MovieDesktopApp.helpers;
using MovieDesktopApp.services;
using MovieDesktopApp.view;
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

        private readonly FavMovieService _favMovieService;

        private readonly MovieService _movieService;

        private bool isFavoriteView = false;


        public SearchMovie()
        {
            InitializeComponent();
            _favMovieService = new FavMovieService();
            _movieService = new MovieService();


            if (SessionManager.UserId <= 0)
            {
                btnViewFavs.Visibility = Visibility.Collapsed;
                txtLoginLogout.Text = "Iniciar sesión";
            }
            else
            {
                txtLoginLogout.Text = "Cerrar sesión";
            }

            MovieListPanel.ItemTemplate = MovieItemTemplateFactory.CreateTemplate(btnAddFav_Click);
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
                var favoriteMovies = await _favMovieService.GetFavoritesByUserAsync(SessionManager.UserId);

                // ✅ Marcar IsFavorite según si ya está en la lista de favoritos
                movies.ForEach(m => m.IsFavorite = favoriteMovies.Any(f => f.ImdbId == m.ImdbId));

                foreach (var movie in movies)
                {
                    if (string.IsNullOrWhiteSpace(movie.Poster) || movie.Poster == "N/A")
                    {
                        movie.Poster = "N/A";
                    }
                }

                MovieListPanel.ItemsSource = movies;
                isFavoriteView = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private async void btnAddFav_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is MovieDTO selectedMovie)
            {
                try
                {
                    // Si estamos en vista de favoritos O la película ya es favorita, eliminar
                    if (isFavoriteView || selectedMovie.IsFavorite)
                    {
                        // Eliminar favorito
                        bool removed = await _favMovieService.RemoveFavoriteAsync(SessionManager.UserId, selectedMovie.ImdbId);

                        if (removed)
                        {
                            MessageBox.Show($"🗑️ '{selectedMovie.Title}' fue quitada de favoritos.");

                            if (isFavoriteView)
                            {
                                btnViewFavs_Click(null, null); // recarga lista de favoritos
                            }
                            else
                            {
                                // Actualizar el estado de la película en la búsqueda
                                selectedMovie.IsFavorite = false;
                                // Refrescar la vista para mostrar el cambio visual
                                RefreshCurrentView();
                            }
                        }
                        else
                        {
                            MessageBox.Show("⚠️ No se pudo quitar de favoritos.");
                        }
                    }
                    else
                    {
                        // Agregar favorito
                        var favorite = new FavoriteMovieDTO
                        {
                            UserId = SessionManager.UserId,
                            MovieId = selectedMovie.ImdbId,
                            Title = selectedMovie.Title,
                            Year = selectedMovie.Year,
                            Poster = selectedMovie.Poster,
                            AddDate = DateTime.UtcNow
                        };

                        bool added = await _favMovieService.AddFavoriteAsync(favorite);

                        if (added)
                        {
                            MessageBox.Show($"✅ Película '{selectedMovie.Title}' añadida a favoritos.");
                            // Actualizar el estado de la película
                            selectedMovie.IsFavorite = true;
                            // Refrescar la vista para mostrar el cambio visual
                            RefreshCurrentView();
                        }
                        else
                            MessageBox.Show($"⚠️ La película ya está en favoritos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error al modificar favoritos: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("❗ No se pudo obtener la película del botón.");
            }
        }

        private async void btnViewFavs_Click(object sender, RoutedEventArgs e)
        {
            int userId = SessionManager.UserId;

            try
            {
                List<MovieDTO> movies = await _favMovieService.GetFavoritesByUserAsync(userId);

                // 👇 Marcar como favoritos
                movies.ForEach(m => m.IsFavorite = true);

                MovieListPanel.ItemsSource = movies;
                isFavoriteView = true; // 👈 activar modo favoritos
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }




        private void txtLoginLogout_MouseDown(object sender, RoutedEventArgs e)
        {

            // Siempre va a la pantalla de login
            var login = new LoginView();
            login.Show();
            SessionManager.ClearSession();
            this.Close();


        }


        private void RefreshCurrentView()
        {
            var currentItems = MovieListPanel.ItemsSource;
            MovieListPanel.ItemsSource = null;
            MovieListPanel.ItemsSource = currentItems;
        }


    }
}