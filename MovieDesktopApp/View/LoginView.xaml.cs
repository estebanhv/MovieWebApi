using MovieDesktopApp.data.dto;
using MovieDesktopApp.helpers;
using MovieDesktopApp.services;
using MovieDesktopApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace MovieDesktopApp.view
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window

    {
        private readonly AuthService _authService;


        public LoginView()
        {
            InitializeComponent();
            _authService = new AuthService();


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

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txUser.Text;
            string password = txPass.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            try
            {
                var result = await _authService.LoginAsync(email, password);

                if (result != null)
                {
                    SessionManager.Token = result.Token;
                    SessionManager.UserEmail = email;
                    // SessionManager.UserId = result.UserId; // si lo tienes

                    MessageBox.Show("Login correcto.");

                    var movieSearchWindow = new SearchMovie();
                    movieSearchWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
        }
    }

    private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterView();
            registerWindow.Show();
            this.Close();
        }

    }
}
