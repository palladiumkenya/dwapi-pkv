using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.DTOs;
using DwapiCentral.SharedKernel.Infrastructure.Data;

namespace DwapiCentral.Cbs.Infrastructure.Data.Repository
{
    public class MasterPatientIndexRepository : BaseRepository<MasterPatientIndex,Guid>, IMasterPatientIndexRepository
    {
        public MasterPatientIndexRepository(CbsContext context) : base(context)
        {
        }

        public void Process(Guid facilityId,IEnumerable<MasterPatientIndex> masterPatientIndices)
        {
            var mpi = masterPatientIndices.ToList();

            if (mpi.Any())
            {
                mpi.ForEach(x => x.FacilityId = facilityId);
                CreateBulk(mpi);
            }
        }

        public List<MpiSearchResultDto> MpiSearch(SearchMpi parameters)
        {
            string sex = "M";
            if (parameters.Gender == Gender.Female) sex = "F";
            var result = new List<MpiSearchResultDto>();
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Pr_SearchMasterPatientIndex";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;
            cmd.Parameters.Add(new SqlParameter("@FirstName", parameters.FirstName));
            cmd.Parameters.Add(new SqlParameter("@MiddleName", parameters.MiddleName));
            cmd.Parameters.Add(new SqlParameter("@LastName", parameters.LastName));
            cmd.Parameters.Add(new SqlParameter("@Gender", sex));
            cmd.Parameters.Add(new SqlParameter("@DateOfBirth", parameters.Dob));
            cmd.Parameters.Add(new SqlParameter("@Telephone", parameters.PhoneNumber));
            cmd.Parameters.Add(new SqlParameter("@NationalId", parameters.NationalId));
            cmd.Parameters.Add(new SqlParameter("@NHIFNumber", parameters.NhifNumber));
            cmd.Parameters.Add(new SqlParameter("@Actual", 0));

            connection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                //read the data
                while (reader.Read())
                {
                    var resultItem = Mapper.Map<IDataRecord, MpiSearchResultDto>(reader);
                    result.Add(resultItem);
                }
            }
            connection.Close();
            return result;
        }
    }
}