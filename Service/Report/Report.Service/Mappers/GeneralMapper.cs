using AutoMapper;
using Report.Service.Dtos;

namespace Report.Service.Mappers
{
    public class GeneralMapper: Profile
    {
        public GeneralMapper()
        {
            CreateMap<Report.Service.Models.Report, ReportDto>().ReverseMap();
            CreateMap<Report.Service.Models.Report, ReportCreateDto>().ReverseMap();
        }
    }
}
