using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.Models;

namespace WebAdvert.AdvertAPI.Services
{
    public interface IAdvertStorageService
    {
        Task<string> Add(AdvertModel model);
        Task Confirm(ConfirmAdvertModel model);
        Task<bool> CheckHealthasync();
    }
}
