using System;
using System.Collections.Generic;
using DwapiCentral.SharedKernel.DTOs;
using MediatR;

namespace DwapiCentral.Cbs.Core.Command
{
    public class SearchMpi : IRequest<Guid>, IRequest<List<MpiSearchResultDto>>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public Gender Gender { get; set; }
        public string County { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string NhifNumber { get; set; }
    }

    public enum Gender
    {
        Female,
        Male
    }
}