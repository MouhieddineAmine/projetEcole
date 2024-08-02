using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PrjFinalAssuQualiLogiciel
{
    public partial class GererEtudiants : UserControl
    {
        private readonly EcoleContext _context;

        public GererEtudiants()
        {
            InitializeComponent();
            _context = new EcoleContext();
            LoadData();
        }

        private void LoadData()
        {
            StudentsDataGrid.ItemsSource = _context.Etudiants.ToList();
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtNumEtudiant.Text) ||
                string.IsNullOrWhiteSpace(txtNom.Text) ||
                string.IsNullOrWhiteSpace(txtPrenom.Text))
            {
                MessageBox.Show("Tous les champs doivent être remplis.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(txtNumEtudiant.Text, out int numeroEtudiant))
            {
                MessageBox.Show("Le numéro d'étudiant doit être un entier.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Check if the student number is already used
            if (_context.Etudiants.Any(e => e.NumeroEtudiant == numeroEtudiant))
            {
                MessageBox.Show("Le numéro d'étudiant est déjà utilisé.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a new student instance
            var student = new Etudiant
            {
                NumeroEtudiant = numeroEtudiant,
                Nom = txtNom.Text,
                Prenom = txtPrenom.Text
            };

            // Add the new student to the database
            _context.Etudiants.Add(student);
            _context.SaveChanges();

            // Refresh the DataGrid to show the new student
            LoadData();

            // Clear the form fields
            ClearForm();
        }

        private void ClearForm()
        {
            txtNumEtudiant.Clear();
            txtNom.Clear();
            txtPrenom.Clear();
        }
    }
}
