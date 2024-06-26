﻿namespace Tour_Planner.ViewModels {
    public class MainWindowVM : ViewModelBase {
        private readonly TourListVM _tourListVM;
        private readonly SearchbarVM _searchbarVM;
        private readonly TabControlVM _tabControlVM;
        private readonly TourLogsVM _tourLogsVM;
        private readonly MenuVM _menuVM;
        public MainWindowVM(TourListVM tourListVM, SearchbarVM searchbarVM, TabControlVM tabControlVM, 
            TourLogsVM tourLogsVM, MenuVM menuVM) {
            _tourListVM = tourListVM;
            _searchbarVM = searchbarVM;
            _tabControlVM = tabControlVM;
            _tourLogsVM = tourLogsVM;
            _menuVM = menuVM;

            _searchbarVM.SearchTextChanged += (s, e) => _tourListVM.SearchedTour(e);
        }
    }
}