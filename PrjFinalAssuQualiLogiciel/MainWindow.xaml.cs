using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;

namespace PrjFinalAssuQualiLogiciel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ColumnContent.Content = new GererEtudiants();
        }

        private void BtnStudents_Click(object sender, RoutedEventArgs e)
        {
            ColumnContent.Content = new GererEtudiants();
        }

        private void BtnGrades_Click(object sender, RoutedEventArgs e)
        {
            ColumnContent.Content = new GererNotes();
        }

        private void BtnCourses_Click(object sender, RoutedEventArgs e)
        {
            ColumnContent.Content = new GererCours();
        }
    }
}