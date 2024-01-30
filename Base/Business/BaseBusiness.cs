﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shoping.Data_Access.DB.MongoDB;
using Shoping.Data_Access.DB.UnitOfWork;
using Shoping.Data_Access.Repo;

namespace Shoping.Business
{
    public class BaseBusiness<TEntity> where TEntity : class
    {
        public UnitOfWork<TEntity> UnitOfWork { get; set; }
        public Repository<TEntity> Repository { get; set; }
        public IConfiguration IConfiguration { get; set; }
        public BaseBusiness(IConfiguration iConfiguration, string type)
        {
            IConfiguration = iConfiguration;
            var _dbName = IConfiguration.GetSection("Database").GetSection("DatabaseName").Value;
            DbContext dbContext = null;
            switch (type)
            {
                case "MongoDB":
                    {
                        dbContext = new MongoDBContext<TEntity>(iConfiguration, _dbName);
                        break;
                    }
                case "Posgrest":
                    {
                        break;
                    }
                case "MySQL":
                    {

                        break;
                    }
                    default :
                    {
                        throw new Exception("Không có loại DB");
                    }
            }
            UnitOfWork = new UnitOfWork<TEntity>(dbContext);
            Repository = UnitOfWork.Repository;
        }
    }
}
