using System;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Enums;
using DwapiCentral.SharedKernel.Tests.TestData.Models;
using FizzWare.NBuilder;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DwapiCentral.SharedKernel.Tests.TestData
{
    public class TestDataFactory
    {
        public static List<MasterFacility> TestMasterFacilities(int count = 2)
        {
            var facilities = Builder<MasterFacility>.CreateListOfSize(count)
                .Build()
                .ToList();

            int n = 0;
            foreach (var facility in facilities)
            {
                n++;
                facility.Id = n;
            }

            facilities[0].Id = 14950;
            facilities[1].Id = 12618;

            return facilities;
        }

        public static List<Facility> TestFacilities(int count = 2)
        {
            var facilities = Builder<Facility>.CreateListOfSize(count)
                .Build()
                .ToList();

            int n = 0;

            foreach (var facility in facilities)
            {
                n++;
                facility.SiteCode = n;
                facility.MasterFacilityId = n;
            }

            facilities[0].MasterFacilityId = facilities[0].SiteCode= 14950;
            facilities[1].MasterFacilityId = facilities[1].SiteCode= 12618;

            return facilities;
        }

        public static List<Facility> TestFacilityWithPatients(int count = 2, int childcount = 3)
        {
            var facilities = Builder<Facility>.CreateListOfSize(count)
                .Build()
                .ToList();

            int n = 0;

            foreach (var facility in facilities)
            {
                n++;
                facility.SiteCode = n;
                facility.MasterPatientIndices = Builder<MasterPatientIndex>.CreateListOfSize(childcount)
                    .All()
                    .With(x=>x.RowId=0)
                    .With(x => x.FacilityId = facility.Id)
                    .Build()
                    .ToList();
            }
            facilities[0].MasterFacilityId = facilities[0].SiteCode= 14950;
            facilities[1].MasterFacilityId = facilities[1].SiteCode= 12618;

            facilities.ForEach(x=>x.MasterPatientIndices.ToList().ForEach(m=>
            {
                n++;
                m.SiteCode = x.SiteCode;
               // m.RowId = x.SiteCode + n;
            }));

            return facilities;
        }

        public static List<Facility> TestFacilityWithMgs(int count = 2, int childcount = 3)
        {
            var facilities = Builder<Facility>.CreateListOfSize(count)
                .Build()
                .ToList();

            int n = 0;
            foreach (var facility in facilities)
            {
                n++;
                facility.SiteCode = n;
                facility.MetricMigrationExtracts = Builder<MetricMigrationExtract>.CreateListOfSize(childcount)
                    .All()
                    .With(x => x.FacilityId = facility.Id)
                    .Build()
                    .ToList();
            }

            facilities[0].MasterFacilityId = facilities[0].SiteCode= 14950;
            facilities[1].MasterFacilityId = facilities[1].SiteCode= 12618;

            facilities.ForEach(x=>x.MetricMigrationExtracts.ToList().ForEach(m=>m.SiteCode=x.SiteCode));

            return facilities;
        }

        public static List<MasterPatientIndex> TestMasterPatientIndices(int siteCode, Guid facilityId, int count = 5)
        {
            var patientIndices = Builder<MasterPatientIndex>
                .CreateListOfSize(count)
                .All().With(x => x.SiteCode = siteCode)
                .With(x => x.FacilityId = facilityId)
                .With(x => x.RowId = 0)
                .Build()
                .ToList();

            return patientIndices;
        }

        public static List<MetricMigrationExtract> TestMetricMigrationExtracts(int siteCode,Guid facilityId, int count = 5)
        {
            var metricMigrationExtracts = Builder<MetricMigrationExtract>.CreateListOfSize(count)
                .All().With(x=>x.SiteCode=siteCode)
                .With(x => x.FacilityId = facilityId)
                .Build()
                .ToList();
            return metricMigrationExtracts;
        }

        public static List<Manifest> TestManifests(int count = 2, int childcount = 3)
        {
            var manifests = Builder<Manifest>.CreateListOfSize(count)
                .All().With(x=>x.Status=ManifestStatus.Staged)
                .With(x=>x.ManifestType=ManifestType.Normal)
                .Build()
                .ToList();

            manifests[0].SiteCode = 14950;
            manifests[1].SiteCode = 12618;

            foreach (var manifest in manifests)
            {
                manifest.Cargoes = TestCargoes(manifest.Id);
            }
            return manifests;
        }

        public static List<Manifest> TestMgsManifests(int count = 2, int childcount = 3)
        {
            var manifests = Builder<Manifest>.CreateListOfSize(count)
                .All().With(x=>x.Status=ManifestStatus.Staged)
                .With(x=>x.ManifestType=ManifestType.Migration)
                .Build()
                .ToList();

            manifests[0].SiteCode = 14950;
            manifests[1].SiteCode = 12618;

            foreach (var manifest in manifests)
            {
                manifest.Cargoes = TestMgsCargoes(manifest.Id);
            }
            return manifests;
        }


        private static  List<Cargo> TestCargoes(Guid manifestId)
        {
            var patientCargo = "1,2,3,4,5";
            var metrcisCargo = @"{^EmrName^:^IQCare^,^EmrVersion^:^KenyaHMISVer2.2.2^,^LastLoginDate^:^2020-01-21T14:13:36.117^,^LastMoH731RunDate^:^2020-02-01T00:00:00^,^DateExtracted^:^2020-03-11T16:46:51.4814657^,^Id^:^289a96e2-1aaa-4d57-bc08-ab7b00e31a75^}";
            var appMetricsCargo = @"{^Version^:^2.4.4^,^Name^:^MasterPatientIndex^,^LogDate^:^2020-03-11T16:45:42.5923658^,^LogValue^:^^,^Status^:0,^Display^:^MasterPatientIndex^,^Action^:^Sent^,^Rank^:6,^Id^:^65fbc682-6fda-4367-9073-ab7b00e2c9bb^}";

            return new[]
            {
                new Cargo(CargoType.Patient, patientCargo, manifestId),
                new Cargo(CargoType.Metrics, metrcisCargo.Replace('^','"'), manifestId),
                new Cargo(CargoType.AppMetrics, appMetricsCargo.Replace('^','"'), manifestId)
            }.ToList();
        }

        private static  List<Cargo> TestMgsCargoes(Guid manifestId)
        {
            var datasetIdCargo = "1,2";
            var metrcisCargo = @"{^EmrName^:^IQCare^,^EmrVersion^:^KenyaHMISVer2.2.2^,^LastLoginDate^:^2020-01-21T14:13:36.117^,^LastMoH731RunDate^:^2020-02-01T00:00:00^,^DateExtracted^:^2020-03-11T16:46:51.4814657^,^Id^:^289a96e2-1aaa-4d57-bc08-ab7b00e31a75^}";
            var appMetricsCargo = @"{^Version^:^2.4.4^,^Name^:^MigrationService^,^LogDate^:^2020-03-11T16:45:42.5923658^,^LogValue^:^^,^Status^:0,^Display^:^MigrationService^,^Action^:^Sent^,^Rank^:6,^Id^:^65fbc682-6fda-4367-9073-ab7b00e2c9bb^}";

            return new[]
            {
                new Cargo(CargoType.MgsMetrics, datasetIdCargo, manifestId),
                new Cargo(CargoType.Metrics, metrcisCargo.Replace('^','"'), manifestId),
                new Cargo(CargoType.AppMetrics, appMetricsCargo.Replace('^','"'), manifestId)
            }.ToList();
        }
        public static List<Docket> TestDockets(int count= 2, int childcount = 3)
        {
           var dockets=   Builder<Docket>.CreateListOfSize(count)
                .Build()
                .ToList();
           dockets[0].Name = dockets[0].Id = "MPI";
           dockets[1].Name = dockets[1].Id = "MGS";

            foreach (var docket in dockets)
            {
                docket.Subscribers= Builder<Subscriber>.CreateListOfSize(childcount)
                    .All()
                    .With(x => x.DocketId == docket.Id)
                    .Build()
                    .ToList();
            }
            return dockets;
        }

        public static List<TestCar> TestCars(int count=2)
        {
            return Builder<TestCar>.CreateListOfSize(count)
                .Build()
                .ToList();
        }
        public static List<TestModel> TestModels(int count = 2)
        {
            return Builder<TestModel>.CreateListOfSize(count)
                .Build()
                .ToList();
        }
        public static List<TestCar> TestCarsWithModel(int count = 2,int childcount=3)
        {
            var cars= Builder<TestCar>.CreateListOfSize(count)
                .Build()
                .ToList();
            foreach (var testCar in cars)
            {
                testCar.Models = Builder<TestModel>.CreateListOfSize(childcount)
                    .All()
                    .With(x=>x.TestCarId==testCar.Id)
                    .Build()
                    .ToList();
            }
            return cars;
        }
    }
}
