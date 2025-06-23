using System.Configuration;
using System.Data;
using System.Windows;

namespace MovieDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string? Token { get; internal set; }
    }

}
