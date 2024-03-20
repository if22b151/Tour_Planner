﻿using Tour_Planner.Enums;
using Tour_Planner.Models;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test {

    public class EditTourLogWindowVMTests {

        [TestCase("2024-03-20T00:23:44", "2", "100", Rating.Good, Difficulty.Medium, true)]
        [TestCase("2024-03-20T00:45:30", "not valid time", "100", Rating.Good, Difficulty.Medium, false)]
        [TestCase("2024-03-20T00:00:00", "2", "not valid distance", Rating.Good, Difficulty.Medium, false)]
        [TestCase("2024-03-20T00:00:00", "", "100", Rating.Good, Difficulty.Medium, false)]
        [TestCase("2024-03-20T00:00:00", "2", "", Rating.Good, Difficulty.Medium, false)]
        [TestCase("2024-03-20T00:00:00", "2", "100", Rating.Excellent, Difficulty.Medium, true)]
        [TestCase("2024-03-20T00:00:00", "2", "100", Rating.Excellent, Difficulty.Easy, true)]
        public void IsTourLogValid_VariousScenarios_ReturnsExpectedResult(string dateTime, string totalTime, string distance, Rating rating, Difficulty difficulty, bool expected) {
            // Arrange
            var tourLog = new TourLogs() {
                DateTime = DateTime.Parse(dateTime),
                TotalTime = totalTime,
                Distance = distance,
                Rating = rating,
                Difficulty = difficulty
            };

            var viewModel = new EditTourLogWindowVM(ref tourLog, null);

            // Act
            var result = viewModel.IsTourLogValid();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}
