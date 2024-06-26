﻿using System;

namespace Tour_Planner.ViewModels {
    public class SearchbarVM : ViewModelBase {
        private string _searchText = "Search...";

        public string SearchText {
            get => _searchText;
            set {
                if (_searchText != value) {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    if (_searchText != "Search...")
                        SearchTextChanged?.Invoke(this, SearchText);
                }
            }
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand SearchbarGotFocusCommand { get; }
        public RelayCommand SearchbarLostFocusCommand { get; }

        public event EventHandler<string>? SearchTextChanged;

        public SearchbarVM() {
            SearchCommand = new RelayCommand((_) => SearchFunction());
            SearchbarGotFocusCommand = new RelayCommand((_) => SearchbarGotFocus());
            SearchbarLostFocusCommand = new RelayCommand((_) => SearchbarLostFocus());
        }

        private void SearchbarLostFocus() {
            if (SearchText == "") {
                SearchText = "Search...";
            }
        }

        private void SearchbarGotFocus() {
            if (SearchText == "Search..." || SearchText == "")
                SearchText = "";
        }

        private void SearchFunction() {
            if (_searchText != "Search...") {
                SearchTextChanged?.Invoke(this, SearchText);
            }
        }
    }
}
