﻿using BusinessLayer;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Enums;
using Tour_Planner.Configurations;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourLogStores;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test {

    public class EditTourLogWindowVMTests {
        
        [TestCase("2024-03-20T00:00:00", "2", "", Rating.Good, Difficulty.Medium, false)]
        [TestCase("2024-03-20T00:00:00", "2", "100", Rating.Excellent, Difficulty.Medium, true)]
        public void IsTourLogValid_VariousScenarios_ReturnsExpectedResult(string dateTime, string totalTime, string distance, Rating rating, Difficulty difficulty, bool expected) {
            // Arrange
            TourLogStore tourLog = new TourLogStore();
            tourLog.SetCurrentTour(new TourLogs() {
                DateTime = DateTime.Parse(dateTime),
                TotalTime = totalTime,
                Distance = distance,
                Rating = rating,
                Difficulty = difficulty
            });
            
            IConfiguration configuration = new ConfigurationManager();
            IConfigOpenRouteService configOpenRouteService = new AppConfiguration(configuration);
            IOpenRouteService openRouteService = new BusinessLogicOpenRouteService(configOpenRouteService);
            var viewModel = new EditTourLogWindowVM(new WindowStore(), new TourStore(), tourLog, new BusinessLogicImp(openRouteService), new MessageBoxService());

            // Act
            var result = viewModel.IsTourLogValid();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}
