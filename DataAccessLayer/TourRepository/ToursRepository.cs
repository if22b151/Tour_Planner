﻿using DataAccessLayer.DALException;
using DataAccessLayer.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.TourRepository;

//TODO: add Logging
public class ToursRepository : IToursRepository
{
    private readonly TourPlannerDbContext _dbContext;

    public ToursRepository(TourPlannerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public TourDTO GetById(int id, string name) {
        try {
            return _dbContext.Tours.Find(id) ?? throw new DataLayerException("");
        }
        catch (Exception) {
            throw new DataLayerException($"Failed to find Tour with Tour Name {name} and ID: {id}");
        }
    }

    public IEnumerable<TourDTO> GetTours() {
        try {
            return _dbContext.Tours.Include(tour => tour.TourLogsList).ToList();
        }
        catch (Exception) {
            throw new DataLayerException("Failed to retrieve list of Tours");
        }
    }

    public void AddTour(TourDTO tour)
    {
        try {
            _dbContext.Tours.Add(tour);
        }
        catch (Exception) {
            throw new DataLayerException($"Failed to Add Tour with Tour Name: {tour.Name}");
        }
        
    }

    public void DeleteTour(TourDTO tour)
    {
        try {
            _dbContext.Tours.Remove(tour);
        }
        catch (Exception) {
            throw new DataLayerException($"Failed to delete Tour with Tour Name: {tour.Name} and ID: {tour.Id}");
        }
    }

    public void UpdateTour(TourDTO tour) {
        try {
            _dbContext.Entry(tour).State = EntityState.Modified;
        }
        catch (Exception) {
            throw new DataLayerException($"Failed to update Tour with Tour Name: {tour.Name} and ID: {tour.Id}");
        }
    }
    
}