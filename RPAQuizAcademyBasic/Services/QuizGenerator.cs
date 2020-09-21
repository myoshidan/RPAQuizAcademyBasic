using RPAQuizAcademyBasic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPAQuizAcademyBasic.Services
{
    public class QuizGenerator
    {
        private List<Quiz> QuizLists { get; set; }

        public QuizGenerator()
        {
            QuizLists = new List<Quiz>();
            QuizLists.Add(CreateQ1());
            QuizLists.Add(CreateQ2());
            QuizLists.Add(CreateQ3());
            QuizLists.Add(CreateQ4());
            QuizLists.Add(CreateQ5());
            QuizLists.Add(CreateQ6());
            QuizLists.Add(CreateQ7());
            QuizLists.Add(CreateQ8());
            QuizLists.Add(CreateQ9());
            QuizLists.Add(Create10());
        }

        private Quiz CreateQ1()
        {
            var q = new Quiz();
            q.QNo = 1;
            q.Question = "下の数字から先頭3文字と末尾2文字のみを抽出してください";
            q.Selection = new Random().Next(1000000000, 1199999999);
            q.Answer = q.Selection.ToString().Substring(0, 3) + q.Selection.ToString().Substring(q.Selection.ToString().Length - 2, 2);
            q.Hidden = false;
            q.MultiCardGrid = false;
            return q;
        }
        private Quiz CreateQ2()
        {
            var num = new Random().Next(5, 20);
            var item = Guid.NewGuid().ToString("N").Substring(0, num);
            var q = new Quiz();
            q.QNo = 2;
            q.Question = "下の文字列の桁数を答えてください";
            q.Selection = item;
            q.Answer = item.Length.ToString();
            return q;
        }

        private Quiz CreateQ3()
        {
            var item = Guid.NewGuid().ToString("N").Substring(0, 10);
            var q = new Quiz();
            q.QNo = 3;
            q.Question = "下の文字列の前後の空白を削除してください";
            q.Selection = $"    {item}     ";
            q.Answer = item.Trim();
            return q;
        }

        private Quiz CreateQ4()
        {
            var item = Guid.NewGuid().ToString("N").Substring(0, 10);
            var q = new Quiz();
            q.QNo = 4;
            q.Question = "下の文字列の小文字を大文字に変換してください";
            q.Selection = item;
            q.Answer = item.ToUpper();
            return q;
        }
        private Quiz CreateQ5()
        {
            var item = Guid.NewGuid().ToString("N").Substring(0, 10);
            var q = new Quiz();
            q.QNo = 5;
            q.Question = "数字の「1」を「壱」に、「7」を「七」に変換してください";
            q.Selection = item;
            q.Answer = item.Replace("1","壱").Replace("7","七");
            return q;
        }
        private Quiz CreateQ6()
        {
            var inpDate = DateTime.Now;
            var q = new Quiz();
            q.QNo = 6;
            q.Question = "今日の日付を入力してください";
            q.Selection = "yyyy年MM月dd日形式";
            q.Answer = inpDate.ToString("yyyy年MM月dd日");
            return q;
        }

        private Quiz CreateQ7()
        {
            var inpDate = DateTime.Now.AddDays(new Random().Next(-100, 100));
            var q = new Quiz();
            q.QNo = 7;
            q.Question = "以下の日付から100日前の日付を入力してください";
            q.Selection = inpDate.ToString("yyyy年MM月dd日");
            q.Answer = inpDate.AddDays(-100).ToString("yyyy年MM月dd日");
            return q;
        }

        private Quiz CreateQ8()
        {
            string input;
            switch (new Random().Next(1,3))
            {
                case 1:
                    input = "あるひ もりの なか くまさんに であった はな さく もりの みち くまさんに であった くまさんの いうことにゃ おじょうさん おにげなさい スタコラ サッサッサのサ スタコラ サッサッサのサ";
                    break;
                case 2:
                    input = "Momotaro san, Momotaro san. Can you give me kibidango, Then I will go to Onigashima, Here you are, Here you are You can have my Kibidango. Now let's go to Onigashima";
                    break;
                case 3:
                    input = "Honey Bee, better not sting me. Sting my father, sting my mother, Sting my sister, sting my brother, Honey bee, better not sting me.";
                    break;
                default:
                    input = "あるひ もりの なか くまさんに であった はな さく もりの みち くまさんに であった くまさんの いうことにゃ おじょうさん おにげなさい スタコラ サッサッサのサ スタコラ サッサッサのサ";
                    break;
            }
            var q = new Quiz();
            q.QNo = 8;
            q.Question = "以下の文章を全て転記して下さい";
            q.Selection = input;
            q.Answer = input;
            return q;
        }

        private Quiz CreateQ9()
        {
            var input = new Random().Next(10000, 99999);
            var q = new Quiz();
            q.QNo = 9;
            q.Question = "隠された数字を探してください";
            q.Selection = input.ToString();
            q.Answer = input.ToString();
            q.Hidden = true;
            return q;
        }

        private Quiz Create10()
        {
            var input = new Random().Next(1, 4);
            var q = new Quiz();
            q.QNo = 10;
            q.Question = "仲間外れを探してください";
            q.Selection = "AutomationIdがカギだよ";
            q.Answer = input.ToString();
            q.Hidden = true;
            q.MultiCardGrid = true;
            return q;
        }

        public Quiz FetchQuiz(int number)
        {
            if (number >= QuizLists.Count()) return null;
            return QuizLists[number];
        }
        public int GetTotalQuizCount()
        {
            return QuizLists.Count();
        }
    }
}
