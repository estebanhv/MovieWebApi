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
        public RegisterView()
        {
            InitializeComponent();
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
            
            // Aquí puedes agregar la lógica para registrar al usuario
            // Por ejemplo, llamar a un servicio de autenticación
            MessageBox.Show("Registro exitoso. Ahora puedes iniciar sesión.");
        }
    }
}
