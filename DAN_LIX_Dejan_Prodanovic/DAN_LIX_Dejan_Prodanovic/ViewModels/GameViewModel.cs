using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_LIX_Dejan_Prodanovic.ViewModels
{
    public enum SlideCategories
    {
        Images
    }
    public class GameViewModel : ObservableObject
    {
        //Collection of slides we are playing with
        public SlideCollectionViewModel Slides { get; private set; }
        //Game information scores, attempts etc
        public GameInfoViewModel GameInfo { get; private set; }
        //Game timer for elapsed time
        public TimerViewModel Timer { get; private set; }
        //Category we are playing in
        public SlideCategories Category { get; private set; }

        public GameViewModel(SlideCategories category)
        {
            Category = category;
            SetupGame(category);
        }

        //Initialize game essentials
        private void SetupGame(SlideCategories category)
        {

            Slides = new SlideCollectionViewModel();

            GameInfo = new GameInfoViewModel();
            Timer = new TimerViewModel(new TimeSpan(0, 0, 1), GameInfo);

            //Set attempts to the maximum allowed
            GameInfo.ClearInfo();

            //Create slides from image folder then display to be memorized
            Slides.CreateSlides("Assets/" + category.ToString());
            Slides.Memorize();

            //Game has started, begin count.
            Timer.Start();

            //Slides have been updated
            OnPropertyChanged("Slides");
            OnPropertyChanged("Timer");
            OnPropertyChanged("GameInfo");
        }

        //Slide has been clicked
        public void ClickedSlide(object slide)
        {
            if (Slides.canSelect)
            {
                var selected = slide as PictureViewModel;
                Slides.SelectSlide(selected);
            }

            if (!Slides.areSlidesActive)
            {
                if (Slides.CheckIfMatched())
                    GameInfo.Award(); //Correct match
                //else
                //    GameInfo.Penalize();//Incorrect match
            }

            GameStatus();
        }

        //Status of the current game
        private void GameStatus()
        {
            if (GameInfo.MatchAttempts < 0)
            {
                GameInfo.GameStatus(false);
                Slides.RevealUnmatched();
                Timer.Stop();
            }

            if (Slides.AllSlidesMatched)
            {
                GameInfo.GameStatus(true);
                Timer.Stop();
                WriteToFile();
            }

        }

        //Restart game
        public void Restart()
        {

            SetupGame(Category);
        }

        void WriteToFile()
        {
            DateTime date = DateTime.Now;
            string dateStr = string.Format("{0}/{1}/{2} {3}:{4}:{5}", date.Day, date.Month, date.Year,
                date.Hour, date.Minute, date.Second);

            int totalTime = Timer.Time.Seconds;
            totalTime += Timer.Time.Minutes * 60;

            using (StreamWriter sw = File.AppendText(@"..\..\IgraPamcenja.txt"))
            {

                sw.WriteLine(dateStr + " Games lasted " + totalTime + "s");

            }
        }
    }
}
