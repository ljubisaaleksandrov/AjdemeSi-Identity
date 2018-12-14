using AjdemeSi.Domain;
using AjdemeSi.Domain.Models.Ride;
using AjdemeSi.Services.Interfaces;
using AutoMapper;
using System.Linq;

namespace AjdemeSi.Services.Logic
{
    public class SiteLabelsService : ISiteLabelsService
    {
        private readonly IMapper _mapper;

        public SiteLabelsService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public RideSiteLabelsViewModel GetRideSiteLabels(string language = "en")
        {
            using (DataContext db = new DataContext())
            {
                var siteLabels = db.SiteLabels.FirstOrDefault(sl => sl.LanguageName == language.ToLower());
                if(siteLabels != null)
                {
                    return _mapper.Map<RideSiteLabelsViewModel>(siteLabels);
                }
                else
                {
                    return new RideSiteLabelsViewModel();
                }
            }
        }
    }
}
