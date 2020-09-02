using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DAN_LIX_Dejan_Prodanovic.ViewModels
{
    public class TimerViewModel : ObservableObject
    {
        private DispatcherTimer _playedTimer;
        private TimeSpan _timePlayed;
        public GameInfoViewModel GameInfo { get; private set; }



        private const int _playSeconds = 1;

        public TimeSpan Time
        {
            get
            {
                return _timePlayed;
            }
            set
            {
                _timePlayed = value;
                OnPropertyChanged("Time");
            }
        }

        public TimerViewModel(TimeSpan time, GameInfoViewModel GameInfo)
        {
            _playedTimer = new DispatcherTimer();
            _playedTimer.Interval = time;
            _playedTimer.Tick += PlayedTimer_Tick;
            _timePlayed = new TimeSpan();
            this.GameInfo = GameInfo;
        }

        public void Start()
        {
            _playedTimer.Start();
        }

        public void Stop()
        {
            _playedTimer.Stop();
        }

        private void PlayedTimer_Tick(object sender, EventArgs e)
        {
            Time = _timePlayed.Add(new TimeSpan(0, 0, 1));
            if (_timePlayed.Minutes == 2)
            {
                GameInfo.GameStatus(false);
                _playedTimer.Stop();
            }
        }
    }
}
