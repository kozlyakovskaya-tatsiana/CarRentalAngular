namespace CarRental.Api.Security
{
    public static class Policy
    {
        public const string ForUserOnly = "ForUserOnly";

        public const string ForManagerOnly = "ForManagerOnly";

        public const string ForAdminOnly = "ForAdminOnly";

        public const string ForUsersAdmins = "ForUsersAdmins";

        public const string ForManagersAdmins = "ForManagersAdmins";
    }
}
