using DomainModel.Models;
using TestTask.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Web;

namespace TestTask.Mappers
{
    public static class UserAnswersMapper
    {
        private readonly static string sectorGroupSeparator = HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;");

        public static UserAnswersViewModel MapToViewModel(UserAnswer userAnswers, List<Sector> sectors)
        {      
            var model = new UserAnswersViewModel()
            {
                Name = userAnswers?.Name,
                UserSectors = userAnswers?.UserSectors.Select(s => s.Sector.Code).ToArray(),
                AgreedToTerms = userAnswers?.AgreedToTerms ?? false,
                AllSectors = sectors
            };
            return model;
        }

        public static UserAnswer MapToEntity(UserAnswersViewModel model)
        {
            return new UserAnswer
            {
                Name = model.Name,
                UserSectorCodes = model.UserSectors,
                AgreedToTerms = model.AgreedToTerms
            };
        }

        public static List<Sector> MapSectorsToFormattedSelectList(List<Sector> sectors)
        {           
            var formatted = new List<Sector>();
            var rootElements = sectors.Where(s => s.ParentId == null).OrderBy(x => x.Code).ToList();

            foreach(var elem in rootElements)
            {
                GetSectorsOrdered(formatted, elem, sectors);
            }

            return formatted;
        }

        private static void GetSectorsOrdered(List<Sector> formatted, Sector current, List<Sector> all, int depth = 0)
        {
            var groupSeparator = "";
            for (var i = 0; i < depth; i++)
            {
                groupSeparator += sectorGroupSeparator;
            }

            formatted.Add(new Sector { Code = current.Code, Name = groupSeparator + current.Name });
            var children = GetChildSectorsOrdered(all, current.Id);
            depth++;
            foreach (var child in children)
            {
                GetSectorsOrdered(formatted, child, all, depth);
            }
        }

        private static List<Sector> GetChildSectorsOrdered(List<Sector> sectors, Guid parentId)
        {
            return sectors.Where(s => s.ParentId == parentId).OrderBy(c => c.Code).ToList();
        }
    }
}
