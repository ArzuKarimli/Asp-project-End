﻿
using Asp_project.Models;

namespace Asp_project.Services.Interfaces
{
    public interface ISettingService
    {
        Task<Dictionary<string, string>> GetAllAsync();
        

    }
}
