﻿using BusinessLayer;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using BusinessLayer.BLException;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.WindowServices;
using Tour_Planner.Stores.TourLogStores;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.ViewModels {
    public class TourLogsVM : ViewModelBase {

        private ObservableCollection<TourLogs> _tourLogsObList = new ObservableCollection<TourLogs>();
        private readonly IBusinessLogicTourLogs _businessLogicTourLogs;
        private readonly IMessageBoxService _messageBoxService;
        private readonly ITourLogStore _tourLogStore;
        private readonly IWindowService<EditTourLogWindowVM, EditTourLogWindow> _editTourLogWindow;
        private readonly IWindowService<AddTourLogWindowVM, AddTourLogWindow> _addTourLogWindow;

        public TourLogsVM(IBusinessLogicTourLogs businessLogicTourLogs,ITourLogStore tourLogStore, ITourStore tourStore, 
            IMessageBoxService messageBoxService, IWindowService<EditTourLogWindowVM, EditTourLogWindow> editTourLogWindow,
            IWindowService<AddTourLogWindowVM, AddTourLogWindow> addTourLogWindow) {
            _editTourLogWindow = editTourLogWindow;
            _addTourLogWindow = addTourLogWindow;
            _messageBoxService = messageBoxService;
            tourStore.OnSelectedTourChangedEvent += SetTour;
            _tourLogStore = tourLogStore;
            _selectedTour = tourStore.CurrentTour;
            _businessLogicTourLogs = businessLogicTourLogs;
            _businessLogicTourLogs.OnTourLogDeleteEvent += DeleteTourLog;
            _businessLogicTourLogs.OnTourLogUpdateEvent += EditTourLog;
            _businessLogicTourLogs.AddTourLogEvent += AddTourLog;
            TourLogsCollectionView ??= new(_tourLogsObList);
            TourLogsCollectionView.Refresh();
            TourLogsCollectionView.MoveCurrentTo(null);

            AddTourLogCommand = new RelayCommand((_) => OpenAddTourLog(), (_) => CanExecuteAddTourLog());
            DeleteTourLogCommand = new RelayCommand((_) => OnDeleteTourLog(), (_) => CanExecuteDeleteEditTourLog());
            EditTourLogCommand = new RelayCommand((_) => OpenEditTourLog(), (_) => CanExecuteDeleteEditTourLog());
        }

        private TourLogs? _selectedTourLog;
        public TourLogs? SelectedTourLog {
            get => _selectedTourLog;
            set {
                if (_selectedTourLog != value) {
                    _selectedTourLog = value;
                    OnPropertyChanged(nameof(SelectedTourLog));
                    _tourLogStore.SetCurrentTour(SelectedTourLog);
                    EditTourLogCommand.RaiseCanExecuteChanged();
                    DeleteTourLogCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private Tour? _selectedTour;
        public Tour? SelectedTour {
            get => _selectedTour;
            set {
                if (_selectedTour != value) {
                    _selectedTour = value;
                    if (_selectedTour != null) {
                        _tourLogsObList = new(_selectedTour.TourLogsList);
                        TourLogsCollectionView.Refresh();
                    }
                    AddTourLogCommand.RaiseCanExecuteChanged();
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        public RelayCommand AddTourLogCommand { get; }
        public RelayCommand DeleteTourLogCommand { get; }
        public RelayCommand EditTourLogCommand { get; }

        public ListCollectionView TourLogsCollectionView { get; private set; }

        
        private void OpenAddTourLog() {
            _addTourLogWindow.ShowDialog();
        }

        private void AddTourLog(TourLogs tourLogs) {
            _selectedTour?.TourLogsList.Add(tourLogs);
            SelectedTourLog = tourLogs;
            TourLogsCollectionView.Refresh();
        }

        private void OpenEditTourLog() { 
            _editTourLogWindow.ShowDialog();
        }

        private void EditTourLog(TourLogs tourLogs) {
            if (_selectedTour != null)
            {
                int index = _selectedTour.TourLogsList.IndexOf(tourLogs);
                _tourLogsObList[index] = tourLogs;
            }

            SelectedTourLog = tourLogs;
            TourLogsCollectionView.Refresh();
        }

        private bool CanExecuteDeleteEditTourLog() {
            return SelectedTourLog != null;
        }

        private bool CanExecuteAddTourLog() {
            return SelectedTour != null;
        }

        private void OnDeleteTourLog() {
            MessageBoxResult result = _messageBoxService.Show("Are you sure you want to delete this tour log?", "Delete Tour Log", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.None);
            if (SelectedTourLog != null && result == MessageBoxResult.Yes && SelectedTour != null) {
                try {
                    _businessLogicTourLogs.DeleteTourLog(SelectedTour, SelectedTourLog);
                }
                catch (BusinessLayerException e) {
                    _messageBoxService.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteTourLog(TourLogs tourLogs) {
            _selectedTour?.TourLogsList.Remove(tourLogs);
            TourLogsCollectionView.Refresh();
        }

        private void SetTour(Tour? tour) {
            if (tour != null)
                SelectedTour = tour;
        }
    }
}
