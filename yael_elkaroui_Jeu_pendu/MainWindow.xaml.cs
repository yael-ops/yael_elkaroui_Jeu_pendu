using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using yael_elkaroui_Jeu_pendu.Properties;

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
        private DispatcherTimer timer;
        private TimeSpan timeLeft = TimeSpan.FromMinutes(1) + TimeSpan.FromSeconds(0);
        private MediaPlayer mediaPlayer;
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            InitGame();
            InitializeMediaPlayer();
            PlayMusic();

        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void StartTimer()
        {
            timer.Start();
        }
        
        private void StopTimer()
        { 
            timer.Stop(); 
        }

        private void ResetTimer()
        {
            timer.Stop();  // Arrête le timer
            timeLeft = TimeSpan.FromMinutes(1) + TimeSpan.FromSeconds(0);  // Réinitialise le temps restant
            timer1.Content = timeLeft.ToString(@"mm\:ss");  // Met à jour l'affichage du temps
            StartTimer();  // Redémarre le timer
        }

       

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeLeft.TotalSeconds > 0)
            {
                timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));
                timer1.Content = timeLeft.ToString(@"mm\:ss"); // Met à jour le Label avec le temps restant
            }
            else
            {
                timer.Stop();             
                MessageBox.Show("Vous êtes trop lent.");
                InitGame();
                ResetTimer();

                // Réinitialise le jeu
            }
        }

        private void InitializeMediaPlayer()
        {
            mediaPlayer = new MediaPlayer();

            // Utilisation d'un chemin relatif
            mediaPlayer.Open(new Uri("Ressources/Musique/C418 - aria math(the drop part)[slowed & reverb].mp3", UriKind.Relative));
        }

        private void PlayMusic()
        {
            // Lancer la lecture de la musique
            mediaPlayer.Play();
        }

        private void InitGame()
        {
            Random random = new Random();
            motSecret = mots[random.Next(mots.Length)].ToLower();
            viesRestantes = 6;
            Nombres_de_vies.Text = viesRestantes.ToString();
            Mot_a_trouver.Text = new string('-', motSecret.Length);
            ResetButtons();
            UpdateHangmanImage();

            GridClavier.IsEnabled = true;
            StartTimer(); // Commence le compte à rebours au début du jeu
        }

        private void UpdateHangmanImage()
        {
            int imageNumber = 7 - viesRestantes;
            string imagePath = $"/Ressources/Images/{imageNumber}.png";
            Uri imageUri = new Uri(imagePath, UriKind.Relative);
            img_pendu.Source = new System.Windows.Media.Imaging.BitmapImage(imageUri);
        }
        private void Button_click_NewGame(object sender, RoutedEventArgs e)
        {
            InitGame();
            ResetTimer() ;
            ResetIndiceButton();

        }

        private void ResetIndiceButton()
        {
            IndiceButton.IsEnabled = true;
        }


        private void ResetButtons()
        {

            foreach (Button bouton in GridClavier.Children.OfType<Button>())
            {
                bouton.IsEnabled = true;
                bouton.Background = new SolidColorBrush(Colors.SaddleBrown);
            }
        }

        private void IndiceButton_Click(object sender, RoutedEventArgs e)
        {
            // Vérifiez si l'indice n'a pas déjà été donné pour ce mot
            if (Mot_a_trouver.Text.Contains('-'))
            {
                // Trouvez un indice pour le mot secret
                char lettreIndice = TrouverLettreIndice();

                // Mettez à jour le mot à trouver avec la lettre de l'indice
                var textArray = Mot_a_trouver.Text.ToCharArray();
                for (int i = 0; i < motSecret.Length; i++)
                {
                    if (motSecret[i] == lettreIndice)
                    {
                        textArray[i] = lettreIndice;
                    }
                }
                Mot_a_trouver.Text = new string(textArray);

                // Désactivez le bouton d'indice après utilisation
                IndiceButton.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Vous avez déjà utilisé l'indice pour ce mot.");
            }
        }

        private char TrouverLettreIndice()
        {
            // Ajoutez votre logique pour trouver une lettre d'indice ici.
            // Vous pouvez choisir une lettre aléatoire qui est encore inconnue dans le mot secret,
            // ou vous pouvez utiliser une autre méthode pour déterminer l'indice.

            // Exemple de logique simple : choisir une lettre aléatoire non révélée dans le mot secret
            Random random = new Random();
            char lettreIndice;
            do
            {
                lettreIndice = (char)('a' + random.Next(26));
            } while (!motSecret.Contains(lettreIndice));

            return lettreIndice;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (viesRestantes <= 0)
            {
                StopTimer();
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
                    StopTimer();
                    MessageBox.Show("Félicitations ! Vous avez trouvé le mot : " + motSecret);
                    InitGame();
                    ResetTimer();
                    ResetIndiceButton();
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
                    StopTimer();
                    MessageBox.Show("Vous avez épuisé toutes vos vies. Le mot était : " + motSecret);
                    InitGame();
                    ResetTimer();
                    ResetIndiceButton();
                }
                else
                {
                    UpdateHangmanImage();
                }
            }
        }
    }
}