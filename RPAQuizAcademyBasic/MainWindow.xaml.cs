using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using RPAQuizAcademyBasic.Services;

namespace RPAQuizAcademyBasic
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private QuizGenerator generator;
        private int nowSelection;
        private int correctAnswer;
        private DateTime startTime;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            generator = new QuizGenerator();
            this.StartBox.Text = $"RPAクイズアカデミーへようこそ!!{Environment.NewLine}本校はクイズ形式でRPAスキルを磨く場だ！全{generator.GetTotalQuizCount()}問のクイズが出題され、最後まで答えると正解数と回答時間が表示される。{Environment.NewLine}是非、短い時間での全問正解を目指してくれ!!";
        }

        private async void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            GroupBox.Visibility = Visibility.Collapsed;
            var quiz = generator.FetchQuiz(nowSelection);
            nowSelection++;
            this.SetDisplay(nowSelection);

            if (string.IsNullOrEmpty(ABox.Text) || this.ABox.Text != quiz.Answer)
            {
                await this.ShowMessageAsync("( ´･･)ﾉ(._.`)", "間違いです");
            }
            else
            {
                await this.ShowMessageAsync($"(●'◡'●)", "正解!!");
                correctAnswer++;
            }
            GroupBox.Visibility = Visibility.Visible;

            this.ABox.Text = string.Empty;
            if (generator.GetTotalQuizCount() <= nowSelection)
            {
                CompletedDisp();
                return;
            }
        }

        private void SetDisplay(int number)
        {
            if (number >= generator.GetTotalQuizCount()) return;
            var quiz = generator.FetchQuiz(number);
            this.GroupBox.Header = $"第{quiz.QNo}問";
            this.QText.Text = quiz.Question;
            this.QBox.Text = quiz.Selection.ToString();

            if (quiz.Hidden)
            {
                this.QBox.Opacity = 0;
            }
            else
            {
                this.QBox.Opacity = 1;
            }

            if (quiz.MultiCardGrid)
            {
                switch (Int32.Parse(quiz.Answer))
                {
                    case 1:
                        text1.SetValue(AutomationProperties.AutomationIdProperty, "OutofFrends");
                        text2.SetValue(AutomationProperties.AutomationIdProperty, "Friends2");
                        text3.SetValue(AutomationProperties.AutomationIdProperty, "Friends3");
                        text4.SetValue(AutomationProperties.AutomationIdProperty, "Friends4");
                        break;
                    case 2:
                        text1.SetValue(AutomationProperties.AutomationIdProperty, "Friends1");
                        text2.SetValue(AutomationProperties.AutomationIdProperty, "OutofFrends");
                        text3.SetValue(AutomationProperties.AutomationIdProperty, "Friends3");
                        text4.SetValue(AutomationProperties.AutomationIdProperty, "Friends4");
                        break;
                    case 3:
                        text1.SetValue(AutomationProperties.AutomationIdProperty, "Friends1");
                        text2.SetValue(AutomationProperties.AutomationIdProperty, "Friends2");
                        text3.SetValue(AutomationProperties.AutomationIdProperty, "OutofFrends");
                        text4.SetValue(AutomationProperties.AutomationIdProperty, "Friends4");
                        break;
                    case 4:
                        text1.SetValue(AutomationProperties.AutomationIdProperty, "Friends1");
                        text2.SetValue(AutomationProperties.AutomationIdProperty, "Friends2");
                        text3.SetValue(AutomationProperties.AutomationIdProperty, "Friends3");
                        text4.SetValue(AutomationProperties.AutomationIdProperty, "OutofFrends");
                        break;
                    default:
                        break;
                }
                multiCardGrid.Visibility = Visibility.Visible;
                singleCard.Visibility = Visibility.Collapsed;
            }
            else
            {
                multiCardGrid.Visibility = Visibility.Collapsed;
                singleCard.Visibility = Visibility.Visible;
            }
            return;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            StartDisp();            
        }

        private void StartDisp()
        {
            nowSelection = 0;
            correctAnswer = 0;
            startTime = DateTime.Now;
            startPanel.Visibility = Visibility.Collapsed;
            GroupBox.Visibility = Visibility.Visible;
            this.SetDisplay(nowSelection);
        }

        private void CompletedDisp()
        {
            start.Content = "再度実施する";   
            topImage.Source = this.FetchImage();
            var name = string.IsNullOrEmpty(NameBox.Text) ? "" : $"{NameBox.Text}さんの結果{Environment.NewLine}";
            this.StartBox.Text = $"{name}全{generator.GetTotalQuizCount()}問中{correctAnswer}問正解！{Environment.NewLine}回答時間は{string.Format("{0:f2}",(DateTime.Now-startTime).TotalSeconds)}秒でした";
            startPanel.Visibility = Visibility.Visible;
            GroupBox.Visibility = Visibility.Collapsed;
        }
        private BitmapImage FetchImage()
        {
            string path;
            var probability = Convert.ToDouble(correctAnswer) / Convert.ToDouble(generator.GetTotalQuizCount());
            if(probability <= 0.2)
            {
                path = $"Images/bad-{new Random().Next(1,3)}.jpg";
            }
            else if(probability <= 0.5)
            {
                path = $"Images/good-{new Random().Next(1, 3)}.jpg";
            }else if(probability <= 0.9)
            {
                path = $"Images/great-{new Random().Next(1, 3)}.jpg";
            }
            else
            {
                path = $"Images/awesome-{new Random().Next(1, 3)}.jpg";
            }

            return new BitmapImage(new Uri(path, UriKind.Relative));
        }
    }
}
