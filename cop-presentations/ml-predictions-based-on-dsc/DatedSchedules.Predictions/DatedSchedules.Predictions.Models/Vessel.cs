using CsvHelper.Configuration.Attributes;

namespace DatedSchedules.Predictions.Models
{
    public record Vessel
    {
        [Name("ROWIDOBJECT")]
        public int Id { get; set; }

        [Name("IMONUMBER")]
        public string ImoNumber { get; set; }

        [Name("VESSELTYPECODE")]
        public string VesselTypeCode { get; set; }

        [Name("VESSELOWNERSHIP")]
        public string VesselOwnership { get; set; }

        [Name("VESSELSTATUS")]
        public string VesselStatus { get; set; }

        [Name("VESSELNAME")]
        public string VesselName { get; set; }

        [Name("VESSELCODE")]
        public string VesselCode { get; set; }

        [Name("OWNERSHIPTYPE")]
        public string OwnershipType { get; set; }

        [Name("LENGTHOVERALL")]
        public double? LengthOverall { get; set; }

        [Name("HEIGHT")]
        public double? Height { get; set; }

        [Name("DEADWEIGHT")]
        public double? DeadWeight { get; set; }

        [Name("GROSSTONNAGE")]
        public double? GrossTonnage { get; set; }

        [Name("NETTONNAGE")]
        public double? NetTonnage { get; set; }

        [Name("MINCRUISINGSPEED")]
        public double? MinimumCruisingSpeed { get; set; }

        [Name("MAXCRUISINGSPEED")]
        public double? MaximumCruisingSpeed { get; set; }
    }
}
