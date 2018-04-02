using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessService.BLL
{
    public interface ISectorsBL
    {
        List<Sector> GetAllSectors();
    }
}
