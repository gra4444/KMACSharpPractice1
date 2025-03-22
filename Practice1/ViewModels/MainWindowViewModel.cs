using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KMA.Krachylo.Practice1.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private static readonly string[] animals = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };

        private DateTime _datePicked;
        private string? _ageText;
        private string? _westernZodiacText;
        private string? _chineseZodiacText;
        private bool _errorShown;

        public DateTime DatePicked
        {
            get
            {
                return _datePicked;
            }
            set
            {
                _datePicked = value;
                OnPropertyChanged();
                CalculateAge();
                CalculateZodiacs();
            }
        }

        public string? AgeText
        {
            get
            {
                return _ageText;
            }
            set
            {
                _ageText = value;
                OnPropertyChanged();
            }
        }

        public string? WesternSign
        {
            get
            {
                return _westernZodiacText;
            }
            set
            {
                _westernZodiacText = value;
                OnPropertyChanged();
            }
        }
        public string? ChineseSign
        {
            get
            {
                return _chineseZodiacText;
            }
            set
            {
                _chineseZodiacText = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            _datePicked = DateTime.Now;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void CalculateAge()
        {
            var currentDate = DateTime.Now;
            int age = currentDate.Year - DatePicked.Year;

            if (age <= 0 && currentDate.DayOfYear < DatePicked.DayOfYear)
            {
                if (!_errorShown)
                {
                    _errorShown = true;
                    MessageBox.Show("Surely you can't be from the future :) Enter your real birthday, please!!");
                }
                return;
            }

            if (age > 135)
            {
                if (!_errorShown)
                {
                    _errorShown = true;
                    MessageBox.Show("You should be in Guinness World Records book :) Enter your real birthday, please!!");
                }
                return;
            }

            _errorShown = false;

            AgeText = $"Oh, so you're {age} years old.";

            if (DatePicked.Month == currentDate.Month && DatePicked.Day == currentDate.Day)
            {
                AgeText = $"{AgeText} Happy birthday my friend!";
            }
        }

        private void CalculateZodiacs()
        {
            WesternSign = GetWesternSign(DatePicked.Month, DatePicked.Day);
            ChineseSign = GetChineseSign(DatePicked.Year);
        }

        private static string GetWesternSign(int month, int day)
        {
            switch (month)
            {
                case 1:
                    if (day <= 19)
                        return "Capricorn";
                    return "Aquarius";
                case 2:
                    if (day <= 18)
                        return "Aquarius";
                    return "Pisces";
                case 3:
                    if (day <= 20)
                        return "Pisces";
                    return "Aries";
                case 4:
                    if (day <= 19)
                        return "Aries";
                    return "Taurus";
                case 5:
                    if (day <= 20)
                        return "Taurus";
                    return "Gemini";
                case 6:
                    if (day <= 20)
                        return "Gemini";
                    return "Cancer";
                case 7:
                    if (day <= 22)
                        return "Cancer";
                    return "Leo";
                case 8:
                    if (day <= 22)
                        return "Leo";
                    return "Virgo";
                case 9:
                    if (day <= 22)
                        return "Virgo";
                    return "Libra";
                case 10:
                    if (day <= 22)
                        return "Libra";
                    return "Scorpio";
                case 11:
                    if (day <= 21)
                        return "Scorpio";
                    return "Saggitarius";
                case 12:
                    if (day <= 21)
                        return "Saggitarius";
                    return "Capricorn";
                default:
                    throw new ArgumentOutOfRangeException($"Invalid month {month} provided");
            }
        }

        private static string GetChineseSign(int year)
        {
            return animals[(year - 4) % 12];
        }
    }
}
