using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.Models;
using AutoMapper;

namespace WebAdvert.AdvertAPI.Services
{
    public class AdvertProfile:Profile
    {
        public AdvertProfile()
        {
            CreateMap<AdvertModel, AdvertdbModel>();
        }
    }
}
