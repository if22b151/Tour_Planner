﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Models;

namespace Tour_Planner.Services.PdfReportGenerationServices;

public class PdfReportGenerationService : IPdfReportGenerationService {
    public void GenerateOneTourReport(Tour tour, string path) {
        var writer = new PdfWriter(path);
        var pdf = new PdfDocument(writer);
        var document = new Document(pdf);

        document.Add(new Paragraph("Tour Name: " + tour.Name).SetFontSize(18).SetBold());
        document.Add(new Paragraph("Tour Description: " + tour.Description).SetFontSize(12));
        document.Add(new Paragraph("Start Location: " + tour.StartLocation).SetFontSize(12));
        document.Add(new Paragraph("End Location: " + tour.EndLocation).SetFontSize(12));
        document.Add(new Paragraph("Distance: " + tour.Distance + " km").SetFontSize(12));
        document.Add(new Paragraph("Transport Type: " + tour.TransportType).SetFontSize(12));

        /*if (!string.IsNullOrEmpty(tour.RouteInformationImage))
        {
            ImageData imageData = ImageDataFactory.Create(tour.RouteInformationImage);
            Image image = new Image(imageData).SetAutoScale(true);
            document.Add(image);
        }*/

        document.Add(new LineSeparator(new SolidLine(1)));

        document.Add(new Paragraph("Tour Logs").SetFontSize(16).SetBold());

        foreach (var log in tour.TourLogsList) {
            document.Add(new Paragraph("Log Date: " + log.DateTime.ToString("yyyy-MM-dd HH:mm:ss")).SetFontSize(12)
                .SetBold());
            document.Add(new Paragraph("Total Time: " + log.TotalTime).SetFontSize(12));
            document.Add(new Paragraph("Distance: " + log.Distance + " km").SetFontSize(12));
            document.Add(new Paragraph("Difficulty: " + log.Difficulty).SetFontSize(12));
            document.Add(new Paragraph("Comment: " + log.Comment).SetFontSize(12));
            document.Add(new Paragraph("Rating: " + log.Rating).SetFontSize(12));
            document.Add(new LineSeparator(new DottedLine(1)));
        }

        document.Close();
    }

    public void GenerateToursSummaryReport(List<Tour> tours, string path) {
        var writer = new PdfWriter(path);
        var pdf = new PdfDocument(writer);
        var document = new Document(pdf);

        // Add a title to the document
        document.Add(new Paragraph("Tours Summary Report").SetFontSize(20).SetBold()
            .SetTextAlignment(TextAlignment.CENTER));

        // Iterate through the list of tours and add a summary for each
        foreach (var tour in tours) {
            // Calculate tour logs summary details
            var averageRating = tour.TourLogsList.Any()
                ? tour.TourLogsList.Average(log => (int)log.Rating)
                : 0;
            /*var totalDistance = tour.TourLogsList.Sum(log => log.Distance);
            var totalTime = tour.TourLogsList.Any()
                ? TimeSpan.FromTicks(tour.TourLogsList.Sum(log => TimeSpan.TryParse(log.TotalTime).Ticks))
                : TimeSpan.Zero;*/
            var averageDifficulty = tour.TourLogsList.Any()
                ? tour.TourLogsList.Average(log => (int)log.Difficulty)
                : 0;

            // Add tour summary details
            document.Add(new Paragraph("Tour Name: " + tour.Name).SetFontSize(14).SetBold());
            document.Add(new Paragraph("Description: " + tour.Description).SetFontSize(12));
            document.Add(new Paragraph("Start Location: " + tour.StartLocation).SetFontSize(12));
            document.Add(new Paragraph("End Location: " + tour.EndLocation).SetFontSize(12));
            document.Add(new Paragraph("Distance: " + tour.Distance + " km").SetFontSize(12));
            document.Add(new Paragraph("Transport Type: " + tour.TransportType).SetFontSize(12));

            document.Add(new LineSeparator(new DottedLine(1)));
            document.Add(new Paragraph("Tour Logs Summary").SetFontSize(13).SetBold());

            // Add calculated tour logs summary details
            document.Add(new Paragraph("Average Rating: " + averageRating).SetFontSize(12));
            /*document.Add(new Paragraph("Total Distance: " + totalDistance + " km").SetFontSize(12));
            document.Add(new Paragraph("Total Time: " + totalTime.ToString(@"hh\:mm\:ss")).SetFontSize(12));*/
            document.Add(new Paragraph("Average Difficulty: " + averageDifficulty).SetFontSize(12));

            // Add a separator
            document.Add(new LineSeparator(new SolidLine(1)));
            document.Add(new Paragraph("\n"));
        }

        document.Close();
    }
}