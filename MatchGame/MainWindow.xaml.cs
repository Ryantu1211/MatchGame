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

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>+
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer(); // three lines of code : crate a new timer and add two fields to keep track of the time elapsed and number of matches the player has found
        int tenthOfSecondElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();

           
        }

        private void Timer_Tick(object sender, EventArgs e) // which updates the new TextBlock with the elapsed time and stops the timer once the player has found all of matches
        {
            tenthOfSecondElapsed++;
            timeTextBlock.Text = (tenthOfSecondElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🚲", "🚲",
                "🛩️", "🛩️",
                "🥑", "🥑",
                "🍕", "🍕",
                "🍔", "🍔",
                "☕", "☕",
                "🍟", "🍟",
                "🥐", "🥐",
            };
            Random random = new Random(); // create a new random number generator
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) // Find everyTextBlock in the mainGrid and repeat the following statements for each of them

            {
                if (textBlock.Name != "timeTextBlock")
                {
                    int index = random.Next(animalEmoji.Count); // pick random number between o and the number of emoji left in the list and call it "index"
                    string nextEmoji = animalEmoji[index];  // USe the random number called "index" to get a random emoji from the list
                    textBlock.Text = nextEmoji; // update textblock with random emoji from the list
                    animalEmoji.RemoveAt(index); // Remove emoji from the list
                }

            }
            timer.Start();
            tenthOfSecondElapsed = 0;
            matchesFound = 0;



        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;


        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }

        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8) // This resets the game if all 8 matched pairs have been found(othervise it does nothing because the game is still running)
            {
                SetUpGame();
            }
        }
    }
}
