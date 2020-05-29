namespace DwapiCentral.SharedKernel.Enums
{
    public enum CargoType
    {
        Patient,
        Metrics,
        AppMetrics,
        MgsMetrics
    }

    public enum ManifestStatus
    {
        Staged,
        Processed
    }
    public enum ManifestType
    {
        Normal,
        Migration
    }

    public enum EmrSetup
    {
        SingleFacility,
        MultiFacility
    }
}
