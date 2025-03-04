namespace MOHU.Integration.Domain.Individuals.Constants;

public static partial class IndividualConstants
{
    public static partial class ManyToOneRelationShips
    {
        public static class Nationality
        {
            public const string ParentEntityLogicalName = "Country";
            public const string ForeignKey = "ldv_nationalitycountryid";
        }
    }
}