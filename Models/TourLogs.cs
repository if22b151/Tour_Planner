﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Models.Enums;

namespace Models {
    public class TourLogs : INotifyPropertyChanged
    {

        private int _id;
        private DateTime _dateTime = DateTime.Now;
        private string _totalTime = "";
        private string _distance = "";
        private Difficulty _difficulty = Difficulty.Easy;
        private string _comment = "";
        private Rating _rating = Rating.Excellent;

        [JsonIgnore]
        public int Id {
            get => _id;
            set {
                if (_id != value) {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public DateTime DateTime {
            get => _dateTime;
            set {
                if (_dateTime != value) {
                    _dateTime = value;
                    OnPropertyChanged(nameof(DateTime));
                }
            }
        }
        public string TotalTime {
            get => _totalTime;
            set {
                if (_totalTime != value) {
                    _totalTime = value;
                    OnPropertyChanged(nameof(TotalTime));
                }
            }
        }
        public string Distance {
            get => _distance;
            set {
                if (_distance != value) {
                    _distance = value;
                    OnPropertyChanged(nameof(Distance));
                }
            }
        }

        public Rating Rating {
            get => _rating;
            set {
                if (_rating != value) {
                    _rating = value;
                    OnPropertyChanged(nameof(Rating));
                }
            }
        }

        public string Comment {
            get => _comment;
            set {
                if (_comment != value) {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        public Difficulty Difficulty {
            get => _difficulty;
            set {
                if (_difficulty != value) {
                    _difficulty = value;
                    OnPropertyChanged(nameof(Difficulty));
                }
            }
        }

        public TourLogs() { }

        public TourLogs(TourLogs tourLogs) {
            Id = tourLogs.Id;
            DateTime = tourLogs.DateTime;
            TotalTime = tourLogs.TotalTime;
            Distance = tourLogs.Distance;
            Rating = tourLogs.Rating;
            Comment = tourLogs.Comment;
            Difficulty = tourLogs.Difficulty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") {

            ValidatePropertyName(propertyName);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ValidatePropertyName(string propertyName) {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null) {
                throw new ArgumentException("Property not found", propertyName);
            }
        }
    }
}