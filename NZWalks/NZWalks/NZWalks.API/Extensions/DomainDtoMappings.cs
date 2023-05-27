using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Extensions
{
    public static class DomainDtoMappings
    {

        public static RegionDto RegionAsDto(this Region model)
        {
            return new RegionDto(model.Id, model.Code, model.Name, model.RegionImageUrl);
        }
    }
}