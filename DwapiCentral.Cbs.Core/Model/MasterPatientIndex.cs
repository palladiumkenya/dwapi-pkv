﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using DwapiCentral.SharedKernel.Model;

namespace DwapiCentral.Cbs.Core.Model
{
    public class MasterPatientIndex:Entity<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RowId { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string FacilityName { get; set; }
        public string Serial { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FirstName_Normalized { get; set; }

        public string MiddleName_Normalized { get; set; }

        public string LastName_Normalized { get; set; }

        public string PatientPhoneNumber { get; set; }

        public string PatientAlternatePhoneNumber { get; set; }

        public string Gender { get; set; }

        public DateTime? DOB { get; set; }

        public string MaritalStatus { get; set; }

        public string PatientSource { get; set; }

        public string PatientCounty { get; set; }

        public string PatientSubCounty { get; set; }

        public string PatientVillage { get; set; }

        public string PatientID { get; set; }

        public string National_ID { get; set; }

        public string NHIF_Number { get; set; }

        public string Birth_Certificate { get; set; }

        public string CCC_Number { get; set; }

        public string TB_Number { get; set; }

        public string ContactName { get; set; }

        public string ContactRelation { get; set; }

        public string ContactPhoneNumber { get; set; }

        public string ContactAddress { get; set; }

        public DateTime? DateConfirmedHIVPositive { get; set; }

        public DateTime? StartARTDate { get; set; }

        public string StartARTRegimenCode { get; set; }

        public string StartARTRegimenDesc { get; set; }

        public string dmFirstName { get; set; }

        public string dmLastName { get; set; }

        public string sxFirstName { get; set; }

        public string sxLastName { get; set; }

        public string sxPKValue { get; set; }

        public string dmPKValue { get; set; }

        public string sxdmPKValue { get; set; }

        public string sxMiddleName { get; set; }

        public string dmMiddleName { get; set; }

        public string sxPKValueDoB { get; set; }

        public string dmPKValueDoB { get; set; }
        public string sxdmPKValueDoB { get; set; }
        public double? JaroWinklerScore { get; set; }
        public DateTime DateExtracted { get; set; }
        public virtual bool? Processed { get; set; }
        public virtual string QueueId { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime? StatusDate { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public Guid FacilityId { get; set; }
        public Guid? RefId { get; set; }

        public MasterPatientIndex()
        {
        }

        public void UpdateRef()
        {
            RefId = Id;
            Id = Guid.NewGuid();
        }
    }
}
