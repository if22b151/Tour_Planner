﻿using BusinessLayer;
using DataAccessLayer;
using Microsoft.Extensions.Configuration;

namespace Tour_Planner.Configurations;

public class AppConfiguration : IConfigDatabase, IConfigOpenRouteService {

    private IConfiguration _configuration;
    
    public AppConfiguration(IConfiguration configuration) {
        _configuration = configuration;
    }

    public string ConnectionStringDb => _configuration["ConnectionStrings:DataBase"]!;

    public string ApiKey => _configuration["ConnectionStrings:API-Key"]!;
}