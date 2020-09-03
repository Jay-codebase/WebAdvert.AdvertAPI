using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.Models;
using AutoMapper;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace WebAdvert.AdvertAPI.Services
{
    public class Dynamodbadvertstorage : IAdvertStorageService
    {
        private readonly IMapper _mapper;
        public Dynamodbadvertstorage(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<string> Add(AdvertModel model)
        {
            var dbmodel = _mapper.Map<AdvertdbModel> (model);
            dbmodel.Id = new Guid().ToString();
            dbmodel.CreationDateTime = DateTime.UtcNow;
            dbmodel.Status = AdvertStatus.Pending;
            using (var client= new AmazonDynamoDBClient())
            {
                using(var context = new DynamoDBContext(client))
                {
                    await context.SaveAsync(dbmodel);
                }
            }
            return dbmodel.Id;
        }

        public async Task<bool> CheckHealthasync()
        {
            using (var client = new AmazonDynamoDBClient())
            {
                var tableData = await client.DescribeTableAsync("Advert");
                return string.Compare(tableData.Table.TableStatus,"active",true)==0;
            }
        }

            public async Task Confirm(ConfirmAdvertModel model)
        {
            using(var client = new AmazonDynamoDBClient())
            {
                using (var context = new DynamoDBContext(client))
                {
                    var record = await context.LoadAsync<AdvertdbModel>(model.Id);
                    if (record == null)
                    {
                        throw new KeyNotFoundException($"A record with ID={model.Id} was mot found.");
                    }
                    if (model.Status == AdvertStatus.Active)
                    {
                        record.Status = AdvertStatus.Active;
                        await context.SaveAsync(record);
                    }
                    else
                    {
                        await context.DeleteAsync(record);
                    }
                }
            }
        }
    }
}
