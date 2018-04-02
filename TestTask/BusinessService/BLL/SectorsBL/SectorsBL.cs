using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel.Context;
using DomainModel.Models;

namespace BusinessService.BLL
{
    public class SectorsBL : ISectorsBL
    {
        private readonly IDomainDbContext _dbContext;

        public SectorsBL(IDomainDbContext domainDbContext)
        {
            _dbContext = domainDbContext;
        }

        public List<Sector> GetAllSectors()
        {
            return _dbContext.Sector.ToList();
        }
    }
}
