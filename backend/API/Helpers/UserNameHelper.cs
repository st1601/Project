namespace API.Helpers
{
    public class UserNameHelper
    {
        public static int GetAge(DateTime birthDate)
        {
            var today = DateTime.Now;

            var age = today.Year - birthDate.Year;

            if (today.Month < birthDate.Month || (today.Month == birthDate.Month && today.Day < birthDate.Day)) { age--; }

            return age;
        }
    }
}
