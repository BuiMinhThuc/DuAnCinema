using Azure.Core;
using Cinema.Payloads.Response;
using System.Text.RegularExpressions;

namespace Cinema.Handle
{
    public class PassWord
    {
        public static string CheckPassWord(string password)
        {
            Regex regex = new Regex("[!@#$%^&*()_+{}\\[\\]:;<>,.?/~`]");
            if (password.Length < 8 || password.Length > 20 || !regex.IsMatch(password))
                return "Password phải từ 8-20 kí tự và chứa ít nhất 1 kí tự đặc biệt !";
            else
                return password;
        }
    }
}
