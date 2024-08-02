using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace PrjFinalAssuQualiLogiciel
{
    public partial class GererNotes : UserControl
    {
        private readonly EcoleContext _context;

        public GererNotes()
        {
            InitializeComponent();
            _context = new EcoleContext();
            LoadData();
            LoadComboBoxes();
        }

        private void LoadData()
        {
            NotesDataGrid.ItemsSource = _context.Notes.ToList();
        }

        private void LoadComboBoxes()
        {
            cbNumeroEtudiant.ItemsSource = _context.Etudiants.Select(e => e.NumeroEtudiant).ToList();
            cbNumeroCours.ItemsSource = _context.Cours.Select(c => c.NumeroCours).ToList();
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (cbNumeroEtudiant.SelectedItem == null ||
                cbNumeroCours.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtNote.Text))
            {
                MessageBox.Show("Tous les champs doivent être remplis.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(txtNote.Text, out double note))
            {
                MessageBox.Show("La note doit être un nombre.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Get selected student and course numbers
            int numeroEtudiant = (int)cbNumeroEtudiant.SelectedItem;
            string numeroCours = cbNumeroCours.SelectedItem.ToString();

            // Create a new note instance
            var noteEntity = new Note
            {
                NumeroEtudiant = numeroEtudiant,
                NumeroCours = numeroCours,
                Notee = note
            };

            // Add the new note to the database
            _context.Notes.Add(noteEntity);
            _context.SaveChanges();

            // Save the student's grades to a text file
            SaveStudentGradesToFile(numeroEtudiant);

            // Refresh the DataGrid to show the new note
            LoadData();

            // Clear the form fields
            ClearForm();
        }

        private void SaveStudentGradesToFile(int numeroEtudiant)
        {
            var student = _context.Etudiants.FirstOrDefault(e => e.NumeroEtudiant == numeroEtudiant);
            var notes = _context.Notes.Where(n => n.NumeroEtudiant == numeroEtudiant).ToList();

            if (student != null && notes.Any())
            {
                string filePath = $"{numeroEtudiant}.txt";
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"Relevé de notes pour l'étudiant {student.Nom} {student.Prenom} (Numéro: {student.NumeroEtudiant})");
                    writer.WriteLine("--------------------------------------------------------");

                    foreach (var note in notes)
                    {
                        var cours = _context.Cours.FirstOrDefault(c => c.NumeroCours == note.NumeroCours);
                        writer.WriteLine($"Cours: {cours?.Titre ?? note.NumeroCours}, Note: {note.Notee}");
                    }
                }
            }
        }

        private void ClearForm()
        {
            cbNumeroEtudiant.SelectedIndex = -1;
            cbNumeroCours.SelectedIndex = -1;
            txtNote.Clear();
        }

        private void txtSearchEtudiant_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(txtSearchEtudiant.Text, out int numeroEtudiant))
            {
                var notes = _context.Notes.Where(n => n.NumeroEtudiant == numeroEtudiant).ToList();
                NotesDataGrid.ItemsSource = notes;
            }
            else
            {
                NotesDataGrid.ItemsSource = null;
            }
        }

        private void DownloadTextFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtSearchEtudiant.Text, out int numeroEtudiant))
            {
                string filePath = $"{numeroEtudiant}.txt";

                if (File.Exists(filePath))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        FileName = $"{numeroEtudiant}.txt",
                        Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
                    };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        File.Copy(filePath, saveFileDialog.FileName, true);
                        MessageBox.Show("Fichier téléchargé avec succès.", "Téléchargement", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Le fichier de relevé de notes n'existe pas.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un numéro d'étudiant valide.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
