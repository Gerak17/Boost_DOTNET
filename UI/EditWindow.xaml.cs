using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using wpfdotnet.Model;
using wpfdotnet.Service;

namespace wpfdotnet.UI
{
    public class RegexValidationRule : ValidationRule
    {
        public string? Regex { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public string? ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string input = value as string ?? "";
            if (input.Length < MinLength || (MaxLength > 0 && input.Length > MaxLength))
                return new ValidationResult(false, ErrorMessage ?? "Valeur invalide.");
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(Regex) && !System.Text.RegularExpressions.Regex.IsMatch(input, Regex))
                return new ValidationResult(false, ErrorMessage ?? "Format invalide.");
            return ValidationResult.ValidResult;
        }
    }

    public partial class EditWindow : Window, INotifyPropertyChanged
    {
        private readonly ArtpieceService? _service;
        private readonly Artpiece _artpiece; 
        private bool _isFormValid;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsFormValid
        {
            get => _isFormValid;
            set
            {
                _isFormValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsFormValid)));
            }
        }

        public Artpiece Artpiece { get; }

        public EditWindow(ArtpieceService? service = null, Artpiece? artpiece = null)
        {
            InitializeComponent();
            _service = service;
            _artpiece = artpiece ?? new Artpiece(); 
            Artpiece = _artpiece; 
            DataContext = this;

            UpdateFormValidity();
            TitleBox.TextChanged += (s, e) => UpdateFormValidity();
            ArtistBox.TextChanged += (s, e) => UpdateFormValidity();
            DescriptionBox.TextChanged += (s, e) => UpdateFormValidity();
            ImageUrlBox.TextChanged += (s, e) => UpdateFormValidity();
        }

        private void UpdateFormValidity()
        {
            IsFormValid = !Validation.GetHasError(TitleBox) &&
                          !Validation.GetHasError(ArtistBox) &&
                          !Validation.GetHasError(DescriptionBox) &&
                          !Validation.GetHasError(ImageUrlBox) &&
                          !string.IsNullOrWhiteSpace(TitleBox.Text);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsFormValid)
                {
                    MessageBox.Show("Veuillez corriger les erreurs dans le formulaire.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var artwork = _artpiece;
                artwork.Title = TitleBox.Text;
                artwork.Artist = ArtistBox.Text ?? string.Empty;
                artwork.Description = DescriptionBox.Text ?? string.Empty;
                artwork.ImageUrl = ImageUrlBox.Text ?? string.Empty;

                if (_service != null)
                {
                    if (_artpiece.Id == 0)
                        _service.Add(artwork);
                    else
                        _service.Update(artwork);
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}