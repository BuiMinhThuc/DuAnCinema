using System.ComponentModel.DataAnnotations;

namespace Cinema.Handle
{
    public class Email
    {
        public static bool IsValiEmail(string email)
        {
            var CheckEmail = new EmailAddressAttribute();
            return CheckEmail.IsValid(email);
        }
    }
}
