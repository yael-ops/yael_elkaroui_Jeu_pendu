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
        private string wordToGuess;
        private string guessedWord;
        private int attemptsLeft = 5;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Remplacez ce mot par le mot que vous voulez que le joueur devine.
            wordToGuess = "PENDU";
            guessedWord = new string('_', wordToGuess.Length);
            WordToGuess.Text = guessedWord;
            IncorrectLetters.Text = "";
            AttemptsLeft.Text = attemptsLeft.ToString();
        }

        private void GuessButton_Click(object sender, RoutedEventArgs e)
        {
            if (attemptsLeft > 0)
            {
                string input = LetterInput.Text.ToUpper();

                if (input.Length == 1 && char.IsLetter(input[0]))
                {
                    if (wordToGuess.Contains(input))
                    {
                        for (int i = 0; i < wordToGuess.Length; i++)
                        {
                            if (wordToGuess[i] == input[0])
                            {
                                guessedWord = guessedWord.Remove(i, 1).Insert(i, input);
                            }
                        }

                        WordToGuess.Text = guessedWord;

                        if (!guessedWord.Contains("_"))
                        {
                            MessageBox.Show("Félicitations ! Vous avez deviné le mot !");
                            InitializeGame();
                        }
                    }
                    else
                    {
                        IncorrectLetters.Text += input + " ";
                        attemptsLeft--;
                        AttemptsLeft.Text = attemptsLeft.ToString();

                        if (attemptsLeft == 0)
                        {
                            MessageBox.Show("Désolé, vous avez perdu. Le mot était : " + wordToGuess);
                            InitializeGame();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez entrer une seule lettre valide.");
                }

                LetterInput.Clear();
            }
            else
            {
                MessageBox.Show("Le jeu est terminé. Cliquez sur Nouveau Jeu pour recommencer.");
            }
        }
    }
}