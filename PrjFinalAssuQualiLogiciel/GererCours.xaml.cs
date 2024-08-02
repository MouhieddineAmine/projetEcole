using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PrjFinalAssuQualiLogiciel
{
    public partial class GererCours : UserControl
    {
        private readonly EcoleContext _context;

        public GererCours()
        {
            InitializeComponent();
            _context = new EcoleContext();
            LoadData();
        }

        private void LoadData()
        {
            CoursesDataGrid.ItemsSource = _context.Cours.ToList();
        }

        private void AddCourseButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtNumCours.Text) ||
                string.IsNullOrWhiteSpace(txtCode.Text) ||
                string.IsNullOrWhiteSpace(txtTitre.Text))
            {
                MessageBox.Show("Tous les champs doivent être remplis.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Check if the course number is already used
            if (_context.Cours.Any(c => c.NumeroCours == txtNumCours.Text))
            {
                MessageBox.Show("Le numéro du cours est déjà utilisé.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a new course instance
            var course = new Cour
            {
                NumeroCours = txtNumCours.Text,
                Code = txtCode.Text,
                Titre = txtTitre.Text
            };

            // Add the new course to the database
            _context.Cours.Add(course);
            _context.SaveChanges();

            // Refresh the DataGrid to show the new course
            LoadData();

            // Clear the form fields
            ClearForm();
        }

        private void ClearForm()
        {
            txtNumCours.Clear();
            txtCode.Clear();
            txtTitre.Clear();
        }
    }
}
