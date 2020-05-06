using System;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Enums;
using DwapiCentral.SharedKernel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DwapiCentral.Cbs.Infrastructure.Data.Repository
{
    public class ManifestRepository : BaseRepository<Manifest, Guid>, IManifestRepository
    {
        public ManifestRepository(CbsContext context) : base(context)
        {
        }

        public void ClearFacility(IEnumerable<Manifest> manifests)
        {
            var ids = string.Join(',', manifests.Select(x =>$"'{x.FacilityId}'"));
            ExecSql(
                $"DELETE FROM {nameof(CbsContext.MasterPatientIndices)} WHERE {nameof(MasterPatientIndex.FacilityId)} in ({ids})");


            var mids = string.Join(',', manifests.Select(x => $"'{x.Id}'"));
            ExecSql(
                $@"
                    UPDATE 
                        {nameof(CbsContext.Manifests)} 
                    SET 
                        {nameof(Manifest.Status)}={(int)ManifestStatus.Processed},
                        {nameof(Manifest.StatusDate)}='{DateTime.Now:yyyy-MM-dd HH:mm:ss}'
                    WHERE 
                        {nameof(Manifest.Id)} in ({mids})");
        }

        public void ClearFacilityMetrics(IEnumerable<Manifest> manifests)
        {
            var ids = string.Join(',', manifests.Select(x =>$"'{x.FacilityId}'"));
            ExecSql(
                $"DELETE FROM {nameof(CbsContext.MetricMigrationExtracts)} WHERE {nameof(MetricMigrationExtract.FacilityId)} in ({ids})");


            var mids = string.Join(',', manifests.Select(x => $"'{x.Id}'"));
            ExecSql(
                $@"
                    UPDATE 
                        {nameof(CbsContext.Manifests)} 
                    SET 
                        {nameof(Manifest.Status)}={(int)ManifestStatus.Processed},
                        {nameof(Manifest.StatusDate)}='{DateTime.Now:yyyy-MM-dd HH:mm:ss}'
                    WHERE 
                        {nameof(Manifest.Id)} in ({mids})");
        }

        public int GetPatientCount(Guid id)
        {
            var ctt = Context as CbsContext;
            var cargo = ctt.Cargoes.FirstOrDefault(x => x.ManifestId == id && x.Type == CargoType.Patient);
            if (null != cargo)
                return cargo.Items.Split(",").Length;

            return 0;
        }

        public IEnumerable<Manifest> GetStaged()
        {
            var ctt = Context as CbsContext;
            var manifests = DbSet.AsNoTracking()
                .Where(x =>
                    x.Status == ManifestStatus.Staged &&
                    x.ManifestType == ManifestType.Normal)
                .ToList();

            foreach (var manifest in manifests)
            {
                manifest.Cargoes = ctt.Cargoes.AsNoTracking()
                    .Where(x =>
                        x.Type != CargoType.Patient &&
                        x.Type != CargoType.MgsMetrics &&
                        x.ManifestId == manifest.Id).ToList();
            }

            return manifests;

        }

        public IEnumerable<Manifest> GetStagedMetrics()
        {
            var ctt = Context as CbsContext;
            var manifests = DbSet.AsNoTracking()
                .Where(x =>
                    x.Status == ManifestStatus.Staged &&
                    x.ManifestType == ManifestType.Migration)
                .ToList();

            foreach (var manifest in manifests)
            {
                manifest.Cargoes = ctt.Cargoes.AsNoTracking()
                    .Where(x =>
                        x.Type != CargoType.Patient &&
                        x.Type != CargoType.MgsMetrics &&
                        x.ManifestId == manifest.Id).ToList();
            }

            return manifests;

        }
    }
}
