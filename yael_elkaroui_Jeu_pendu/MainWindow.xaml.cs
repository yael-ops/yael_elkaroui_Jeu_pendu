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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace yael_elkaroui_Jeu_pendu
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] mots = { "table", "chaise", "ordinateur", "fenetre", "voiture", "telephone", "lampe", "papier", "porte", "plante", "cuisine", "souris", "coussin", "escalier", "bureau", "lit", "couverture", "telecommande", "carte", "horloge", "canape", "mur", "tapis", "refrigerateur", "miroir", "fauteuil", "cassette", "television", "casque", "clavier", "chargeur", "microphone", "placard", "ventilateur", "vent", "cordon", "poubelle", "livre", "stylo", "ecran", "cheminee", "radio", "journal", "peinture", "balcon", "appareil", "interrupteur", "plafond", "bouteille", "journal" };
        private string motSecret;
        private int viesRestantes = 5;
        private List<Button> boutonsLettres = new List<Button>();

        public MainWindow()
        {
            InitializeComponent();
            InitGame();
        }

        // Fonction qui initialise la partie
        private void InitGame() 
        {
            Random random = new Random();
            motSecret = mots[random.Next(mots.Length)].ToLower();
            viesRestantes = 6;
            Nombres_de_vies.Text = viesRestantes.ToString();
            Mot_a_trouver.Text = new string('-', motSecret.Length);
            ResetButtons();
            UpdateHangmanImage();  // Ajoutez ceci pour afficher l'image au début du jeu

            // Désactivez le bouton "Nouvelle partie" s'il est activé
            GridClavier.IsEnabled = true;
        }

        private void UpdateHangmanImage()
        {
            int imageNumber = 7 - viesRestantes;
            string imagePath = $"/Ressources/Images/{imageNumber}.png";
            Uri imageUri = new Uri(imagePath, UriKind.Relative);
            img_pendu.Source = new BitmapImage(imageUri);
        }

        private void ResetButtons()
        {
            boutonsLettres.Clear();
            boutonsLettres.Add(A);
            boutonsLettres.Add(B);
            boutonsLettres.Add(C);
            boutonsLettres.Add(D);
            boutonsLettres.Add(E);
            boutonsLettres.Add(F);
            boutonsLettres.Add(G);
            boutonsLettres.Add(H);
            boutonsLettres.Add(I);
            boutonsLettres.Add(J);
            boutonsLettres.Add(K);
            boutonsLettres.Add(L);
            boutonsLettres.Add(M);
            boutonsLettres.Add(N);
            boutonsLettres.Add(O);
            boutonsLettres.Add(P);
            boutonsLettres.Add(Q);
            boutonsLettres.Add(R);
            boutonsLettres.Add(S);
            boutonsLettres.Add(T);
            boutonsLettres.Add(U);
            boutonsLettres.Add(V);
            boutonsLettres.Add(W);
            boutonsLettres.Add(X);
            boutonsLettres.Add(Y);
            boutonsLettres.Add(Z);

            foreach (Button bouton in boutonsLettres)
            {
                bouton.IsEnabled = true;
                bouton.Background = new SolidColorBrush(Colors.LightSkyBlue);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (viesRestantes <= 0)
            {
                MessageBox.Show("Vous avez épuisé toutes vos vies. Le mot était : " + motSecret);
                InitGame();
                return;
            }

            Button bouton = (Button)sender;
            char lettre = bouton.Content.ToString().ToLower()[0];

            if (motSecret.Contains(lettre))
            {
                for (int i = 0; i < motSecret.Length; i++)
                {
                    if (motSecret[i] == lettre)
                    {
                        var textArray = Mot_a_trouver.Text.ToCharArray();
                        textArray[i] = lettre;
                        Mot_a_trouver.Text = new string(textArray);
                    }
                }

                bouton.IsEnabled = false;
                bouton.Background = new SolidColorBrush(Colors.LightGreen);

                if (!Mot_a_trouver.Text.Contains('-'))
                {
                    MessageBox.Show("Félicitations ! Vous avez trouvé le mot : " + motSecret);
                    InitGame();
                }
            }
            else
            {
                viesRestantes--;
                Nombres_de_vies.Text = viesRestantes.ToString();
                bouton.IsEnabled = false;
                bouton.Background = new SolidColorBrush(Colors.LightCoral);

                if (viesRestantes == 0)
                {
                    MessageBox.Show("Vous avez épuisé toutes vos vies. Le mot était : " + motSecret);
                    InitGame();
                }
                else
                {
                    UpdateHangmanImage(); // Met à jour l'image du pendu
                }
            }
        }
    }
}
