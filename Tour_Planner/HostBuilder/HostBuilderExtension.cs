﻿using System;
using BusinessLayer;
using BusinessLayer.Services.AddTourLogServices;
using BusinessLayer.Services.AddTourServices;
using BusinessLayer.Services.DeleteTourLogServices;
using BusinessLayer.Services.DeleteTourServices;
using BusinessLayer.Services.EditTourLogServices;
using BusinessLayer.Services.EditTourServices;
using BusinessLayer.Services.GetToursServices;
using DataAccessLayer;
using DataAccessLayer.TourLogRepository;
using DataAccessLayer.TourRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tour_Planner.Configurations;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.OpenFileDialogServices;
using Tour_Planner.Services.OpenFolderDialogServices;
using Tour_Planner.Services.PdfReportGenerationServices;
using Tour_Planner.Services.SaveFileDialogServices;
using Tour_Planner.Services.WindowServices;
using Tour_Planner.Stores.TourLogStores;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.HostBuilder;

public static class HostBuilderExtension {
    public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices(services => {
            services.AddSingleton<MainWindowVM>();
            services.AddSingleton<TourListVM>();
            services.AddSingleton<SearchbarVM>();
            services.AddSingleton<TabControlVM>();
            services.AddSingleton<TourLogsVM>();
            services.AddSingleton<MenuVM>();
        });
        return hostBuilder;
    }

    public static IHostBuilder AddMainWindow(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices(services => {
            services.AddSingleton(s => new MainWindow() {
                DataContext = s.GetRequiredService<MainWindowVM>(),
                TourList = { DataContext = s.GetRequiredService<TourListVM>() },
                Searchbar = { DataContext = s.GetRequiredService<SearchbarVM>() },
                TabControl = { DataContext = s.GetRequiredService<TabControlVM>() },
                TourLogs = { DataContext = s.GetRequiredService<TourLogsVM>() },
                TopMenu = { DataContext = s.GetRequiredService<MenuVM>() }
            });
        });
        return hostBuilder;
    }

    public static IHostBuilder AddBusinessLayer(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices(services => {
            services.AddSingleton<IBusinessLogicTours, BusinessLogicImp>();
            services.AddSingleton<IBusinessLogicTourLogs, BusinessLogicImp>();
            services.AddSingleton<IOpenRouteService, BusinessLogicOpenRouteService>();
            services.AddSingleton<IOpenWeatherService, BusinessLogicOpenWeatherService>();
            services.AddSingleton<IGetJokeService, BusinessLogicGetJokeService>();
            services.AddSingleton<IGetToursService, GetToursService>();
            services.AddSingleton<IAddTourService, AddTourService>();
            services.AddSingleton<IEditTourService, EditTourService>();
            services.AddSingleton<IDeleteTourService, DeleteTourService>();
            services.AddSingleton<IAddTourLogService, AddTourLogService>();
            services.AddSingleton<IEditTourLogService, EditTourLogService>();
            services.AddSingleton<IDeleteTourLogService, DeleteTourLogService>();
        });
        return hostBuilder;
    }
    
    public static IHostBuilder AddDataAccessLayer(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices(services => {
            services.AddTransient<IToursRepository, ToursRepository>();
            services.AddTransient<ITourLogsRepository, TourLogsRepository>();
            services.AddSingleton<IUnitofWorkFactory, UnitofWorkFactory>();
            services.AddTransient<IUnitofWork, UnitofWork>();
            services.AddSingleton<Func<IToursRepository>>(s => s.GetRequiredService<IToursRepository>);
            services.AddSingleton<Func<ITourLogsRepository>>(s => s.GetRequiredService<ITourLogsRepository>);
            services.AddSingleton<Func<TourPlannerDbContext>>(s => s.GetRequiredService<TourPlannerDbContext>);
        });
        return hostBuilder;
    }

    public static IHostBuilder AddServices(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices((hostContext, services) => {
            services.AddSingleton<IPdfReportGenerationService, PdfReportGenerationService>();
            services.AddSingleton<IOpenFolderDialogService, OpenFolderDialogService>();
            services.AddSingleton<IOpenFileDialogService, OpenFileDialogService>();
            services.AddSingleton<ISaveFileDialogService, SaveFileDialogService>();
            services.AddSingleton<IMessageBoxService, MessageBoxService>();
            
            services.AddSingleton<IWindowStore, WindowStore>();
            services.AddSingleton<ITourStore, TourStore>();
            services.AddSingleton<ITourLogStore, TourLogStore>();
            
            services.AddSingleton(typeof(IWindowService<,>), typeof(WindowService<,>));
            
            services.AddTransient<AddTourWindowVM>();
            services.AddTransient<AddTourWindow>();
            services.AddSingleton<Func<AddTourWindowVM>>(s => s.GetRequiredService<AddTourWindowVM>);
            services.AddSingleton<Func<AddTourWindow>>(s => s.GetRequiredService<AddTourWindow>);
            
            services.AddTransient<AddTourLogWindowVM>();
            services.AddTransient<AddTourLogWindow>();
            services.AddSingleton<Func<AddTourLogWindowVM>>(s => s.GetRequiredService<AddTourLogWindowVM>);
            services.AddSingleton<Func<AddTourLogWindow>>(s => s.GetRequiredService<AddTourLogWindow>);
            
            services.AddTransient<ImportTourWindowVM>();
            services.AddTransient<ImportTourWindow>();
            services.AddSingleton<Func<ImportTourWindowVM>>(s => s.GetRequiredService<ImportTourWindowVM>);
            services.AddSingleton<Func<ImportTourWindow>>(s => s.GetRequiredService<ImportTourWindow>);

            services.AddTransient<ExportTourWindowVM>();
            services.AddTransient<ExportTourWindow>();
            services.AddSingleton<Func<ExportTourWindowVM>>(s => s.GetRequiredService<ExportTourWindowVM>);
            services.AddSingleton<Func<ExportTourWindow>>(s => s.GetRequiredService<ExportTourWindow>);

            services.AddTransient<EditTourWindow>();
            services.AddTransient<EditTourWindowVM>();
            services.AddSingleton<Func<EditTourWindowVM>>(s => s.GetRequiredService<EditTourWindowVM>);
            services.AddSingleton<Func<EditTourWindow>>(s => s.GetRequiredService<EditTourWindow>);
            
            services.AddTransient<EditTourLogWindow>();
            services.AddTransient<EditTourLogWindowVM>();
            services.AddSingleton<Func<EditTourLogWindowVM>>(s => s.GetRequiredService<EditTourLogWindowVM>);
            services.AddSingleton<Func<EditTourLogWindow>>(s => s.GetRequiredService<EditTourLogWindow>);
            
            services.AddTransient<GeneratePdfWindow>();
            services.AddTransient<GeneratePdfWindowVM>();
            services.AddSingleton<Func<GeneratePdfWindowVM>>(s => s.GetRequiredService<GeneratePdfWindowVM>);
            services.AddSingleton<Func<GeneratePdfWindow>>(s => s.GetRequiredService<GeneratePdfWindow>);
            
            services.AddSingleton<IConfigDatabase, AppConfiguration>(s => new AppConfiguration(hostContext.Configuration));
            services.AddSingleton<IConfigOpenRouteService, AppConfiguration>(s => new AppConfiguration(hostContext.Configuration));
            services.AddSingleton<IConfigOpenWeatherService, AppConfiguration>(s => new AppConfiguration(hostContext.Configuration));
        });
        return hostBuilder;
    }

    public static IHostBuilder AddDbContext(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices((hostContext, services) => {
            services.AddDbContextFactory<TourPlannerDbContext>(options => {
                options.UseNpgsql(hostContext.Configuration.GetConnectionString("DataBase"));
            });
        });
        return hostBuilder;
    }
}