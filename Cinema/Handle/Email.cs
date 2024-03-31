using System.ComponentModel.DataAnnotations;

namespace Cinema.Handle
{
    public class Email
    {
        public static bool IsValiEmail(string email)
        {
            //Hello1
            var CheckEmail = new EmailAddressAttribute();
            return CheckEmail.IsValid(email);
        }
    }
}
