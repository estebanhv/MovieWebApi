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
    /// Lógica de interacción para RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {

        private readonly SessionService _authService;

        public RegisterView()
        {
            InitializeComponent();
            _authService = new SessionService();

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


        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string email = txUser.Text;
            string password = txPass.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("El correo no tiene un formato válido. Usa ejemplo@dominio.com");
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres.");
                return;
            }

            try
            {
                bool success = await _authService.RegisterAsync(email, password);

                if (success)
                {
                    MessageBox.Show("Registro exitoso. Ahora puedes iniciar sesión.");
                    var movieSearchWindow = new LoginView();
                    movieSearchWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El correo ya está en uso o no cuenta con el formato correcto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
