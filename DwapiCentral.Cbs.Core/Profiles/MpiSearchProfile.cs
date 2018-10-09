using System.Data;
using AutoMapper;
using DwapiCentral.SharedKernel.DTOs;
using DwapiCentral.SharedKernel.Utils;

namespace DwapiCentral.Cbs.Core.Profiles
{
    public class MpiSearchProfile : Profile
    {
        public MpiSearchProfile()
        {
            CreateMap<IDataRecord, MpiSearchResultDto>()
                .ForMember(x => x.FirstName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(MpiSearchResultDto.FirstName))))
                .ForMember(x => x.MiddleName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(MpiSearchResultDto.MiddleName))))
                .ForMember(x => x.LastName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(MpiSearchResultDto.LastName))))
                .ForMember(x => x.DateOfBirth, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(MpiSearchResultDto.DateOfBirth))))
                .ForMember(x => x.Gender, o => o.MapFrom(s => s.GetStringOrDefault(nameof(MpiSearchResultDto.Gender))))
                .ForMember(x => x.MflCode, o => o.MapFrom(s => s.GetStringOrDefault(nameof(MpiSearchResultDto.MflCode))))
                .ForMember(x => x.RegisteredFacility, o => o.MapFrom(s => s.GetStringOrDefault(nameof(MpiSearchResultDto.RegisteredFacility))))
                .ForMember(x => x.PhoneNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(MpiSearchResultDto.PhoneNumber))))
                .ForMember(x => x.NhifNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(MpiSearchResultDto.NhifNumber))))
                .ForMember(x => x.NationalId, o => o.MapFrom(s => s.GetStringOrDefault(nameof(MpiSearchResultDto.NationalId))))
                .ForMember(x => x.HomeSubCounty, o => o.MapFrom(s => s.GetStringOrDefault(nameof(MpiSearchResultDto.HomeSubCounty))))
                .ForMember(x => x.HomeCounty, o => o.MapFrom(s => s.GetStringOrDefault(nameof(MpiSearchResultDto.HomeCounty))))
                .ForMember(x => x.CccNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(MpiSearchResultDto.CccNumber))))
                .ForMember(x => x.MatchingScore, o => o.MapFrom(s => s.GetStringOrDefault(nameof(MpiSearchResultDto.MatchingScore))));
        }
    }
}