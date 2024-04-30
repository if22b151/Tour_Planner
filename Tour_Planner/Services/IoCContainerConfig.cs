﻿using System;
using BusinessLayer;
using Microsoft.Extensions.DependencyInjection;
using Tour_Planner.ReportGeneration;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Services;

public class IoCContainerConfig {

    private readonly ServiceProvider _serviceProvider;
    
    public IoCContainerConfig() {
        var services = new ServiceCollection();
        services.AddSingleton<MainWindowVM>();
        services.AddSingleton<TourListVM>();
        services.AddSingleton<SearchbarVM>();
        services.AddSingleton<TabControlVM>();
        services.AddSingleton<TourLogsVM>();
        services.AddSingleton<IBusinessLogic, BusinessLogicImp>();
        services.AddSingleton<IPdfReportGeneration, PdfReportGeneration>();
        _serviceProvider = services.BuildServiceProvider();
    }
    
    public MainWindowVM MainWindowVm => _serviceProvider.GetRequiredService<MainWindowVM>();
    public TourListVM TourListVm => _serviceProvider.GetRequiredService<TourListVM>();
    public SearchbarVM SearchbarVm => _serviceProvider.GetRequiredService<SearchbarVM>();
    public TabControlVM TabControlVm => _serviceProvider.GetRequiredService<TabControlVM>();
    public TourLogsVM TourLogsVm => _serviceProvider.GetRequiredService<TourLogsVM>();
    public IBusinessLogic BusinessLogic => _serviceProvider.GetRequiredService<IBusinessLogic>();
    
    /*TourListVM tourListVM = new TourListVM();
    SearchbarVM searchbarVM = new SearchbarVM();
    TabControlVM tabControlVM = new TabControlVM();
    TourLogsVM tourLogsVM = new TourLogsVM();
    MainWindowVM mainWindowVM = new MainWindowVM(tourListVM, searchbarVM, tabControlVM, tourLogsVM);*/
    
}