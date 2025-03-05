namespace MOHU.Integration.Domain.Features.Individuals.Constants;

public static partial class IndividualConstants
{
    public static partial class ManyToOneRelationShips
    {
        public static class CountryOfResidence
        {
            public const string ParentEntityLogicalName = "Country";
            public const string ForeignKey = "ldv_countryofresidenceid";
        }
    }
}