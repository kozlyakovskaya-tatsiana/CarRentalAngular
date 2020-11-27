namespace CarRental.DAL.Enums
{
    public static class RolesExtension
    {
        public static string GetRoleName(this Role role)
        {
            return role.ToString().ToLower();
        }
    }
}
