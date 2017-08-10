using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using MahApps.Metro;

namespace ClockWidget
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _timeLeft;
        private int _fontSize;
        private string _finishTime;
        private string _timeColor;
        private string _fontFamily;
        private ObservableCollection<string> _fonts;
        private string _note;

        public string Note
        {
            get { return _note; }
            set { SetProperty(ref _note, value, () => Note); }
        }
        public ObservableCollection<string> Fonts
        {
            get { return _fonts; }
            set { SetProperty(ref _fonts, value, () => Fonts); }
        }
        public string FontFamily
        {
            get { return _fontFamily; }
            set { SetProperty(ref _fontFamily, value, () => FontFamily); }
        }
        public string TimeColor
        {
            get { return _timeColor; }
            set { SetProperty(ref _timeColor, value, () => TimeColor); }
        }
        public string FinishTime
        {
            get { return _finishTime; }
            set { SetProperty(ref _finishTime, value, () => FinishTime); }
        }
        public int FontSize
        {
            get { return _fontSize; }
            set { SetProperty(ref _fontSize, value, () => FontSize); }
        }
        public string TimeLeft
        {
            get { return _timeLeft; }
            set { SetProperty(ref _timeLeft, value, () => TimeLeft); }
        }
        public MainWindowViewModel()
        {
            LoadSettings();
            LoadAvailableThemes();
            CalculateTime();
            Fonts = new ObservableCollection<string>(System.Drawing.FontFamily.Families.Select(f => f.Name));
        }

        private async void CalculateTime()
        {
            try
            {
                await Task.Run(() =>
                {
                    while (true)
                    {
                        DateTime finishTime;
                        if (DateTime.TryParse(FinishTime, out finishTime))
                        {
                            TimeSpan left = finishTime - DateTime.Now;
                            DateTime leftTime = new DateTime(left.Ticks);
                            TimeLeft = leftTime.ToLongTimeString();
                        }
                        else
                        {
                            TimeLeft = $"00:00:00";
                        }

                        int hoursLeft;
                        if (Int32.TryParse(TimeLeft.Split(':').ElementAt(0), out hoursLeft))
                        {
                            if (hoursLeft >= 1)
                            {
                                TimeColor = "Green";
                            }
                            else
                            {
                                int minutesLeft;
                                if (Int32.TryParse(TimeLeft.Split(':').ElementAt(1), out minutesLeft))
                                {
                                    TimeColor = minutesLeft >= 30
                                                    ? "Gold"
                                                    : "Red";
                                }
                            }
                        }
                    } // ReSharper disable once FunctionNeverReturns
                });
            }
            catch (ArgumentOutOfRangeException)
            {
                TimeLeft = $"00:00:00";
                return;
            }
        }

        #region Settings
        public Settings Settings { get; set; }

        private bool _onTop;
        private ObservableCollection<AppTheme> _themes;
        private ObservableCollection<Accent> _accents;
        private AppTheme _selectedTheme;
        private Accent _selectedAccent;

        public Accent SelectedAccent
        {
            get { return _selectedAccent; }
            set
            {
                SetProperty(ref _selectedAccent, value, () => SelectedAccent);
                ThemeChanger.ChangeTheme(SelectedAccent, SelectedTheme);
            }
        }
        public AppTheme SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                SetProperty(ref _selectedTheme, value, () => SelectedTheme);
                ThemeChanger.ChangeTheme(SelectedAccent, SelectedTheme);
            }
        }
        public ObservableCollection<Accent> Accents
        {
            get { return _accents; }
            set { SetProperty(ref _accents, value, () => Accents); }
        }
        public ObservableCollection<AppTheme> Themes
        {
            get { return _themes; }
            set { SetProperty(ref _themes, value, () => Themes); }
        }
        public bool OnTop
        {
            get { return _onTop; }
            set { SetProperty(ref _onTop, value, () => OnTop); }
        }

        private void LoadAvailableThemes()
        {
            Themes = new ObservableCollection<AppTheme>(ThemeManager.AppThemes);
            Accents = new ObservableCollection<Accent>(ThemeManager.Accents);
            SelectedTheme = ThemeManager.GetAppTheme(Settings.SelectedThemeName);
            SelectedAccent = ThemeManager.GetAccent(Settings.SelectedAccentName);

        }
        private void LoadSettings()
        {
            Settings = SettingsLogic.GetSettings();
            FontSize = Settings.FontSize;
            FinishTime = Settings.FinishTime;
            FontFamily = Settings.FontFamily;
            Note = Settings.Note;
        }


        public void SaveSettings()
        {
            Settings.SelectedAccentName = SelectedAccent.Name;
            Settings.SelectedThemeName = SelectedTheme.Name;
            Settings.FinishTime = FinishTime;
            Settings.FontSize = FontSize;
            Settings.FontFamily = FontFamily;
            Settings.Note = Note;
            SettingsLogic.SaveSettings(Settings);
        }

        #endregion Settings

    }
}