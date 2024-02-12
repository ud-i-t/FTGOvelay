using FTGOverlayControl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl
{
    internal class PlayerViewModel : INotifyPropertyChanged
    {
        private PlayerSetting _setting;
        private Action _onChange;

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _setting.Name = value;
                _onChange();
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _copy = "";
        public string Copy
        {
            get { return _copy; }
            set
            {
                _copy = value;
                _setting.Copy = value;
                _onChange();
                OnPropertyChanged(nameof(Copy));
            }
        }

        private string _character = "";
        public string Character
        {
            get { return _character; }
            set
            {
                _character = value;
                _setting.Character = value;
                _onChange();
                OnPropertyChanged(nameof(Character));
            }
        }

        private string _controlType = "";
        public string ControlType
        {
            get { return _controlType; }
            set
            {
                _controlType = value;
                _setting.ControlType = value;
                _onChange();
                OnPropertyChanged(nameof(ControlType));
            }
        }

        private string _rank = "";
        public string Rank
        {
            get { return _rank; }
            set
            {
                _rank = value;
                _setting.Rank = value;
                _onChange();
                OnPropertyChanged(nameof(Rank));
            }
        }

        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                _setting.Score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        private string _file = "";
        public string File
        {
            get { return _file; }
            set
            {
                _file = value;
                _setting.FilePath = value;
                _onChange();
                OnPropertyChanged(nameof(File));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand IncrementScore { get; private set; }
        public RelayCommand DecrementScore { get; private set; }
        public IEnumerable<PlayerModel> Players { get; } 

        public PlayerViewModel(PlayerSetting setting, Action onChange, IEnumerable<PlayerModel> players)
        {
            Players = players;

            _setting = setting;
            _onChange = onChange; 
            Name = _setting.Name;
            Score = _setting.Score;
            File = _setting.FilePath;
            Character = _setting.Character;
            ControlType = _setting.ControlType;
            Rank = _setting.Rank;

            IncrementScore = new RelayCommand(_ => 
            { 
                Score++;
                _onChange();
            }, _ => true);
            DecrementScore = new RelayCommand(_ =>
            {
                Score--;
                _onChange();
            }, _ => true);
        }
    }
}
