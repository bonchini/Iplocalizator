using System;
using System.Text.RegularExpressions;

namespace MP.IPLocalizator.Business.Validators
{
    public static class IpValidator
    {
        public static void Validate(string ip)
        {
            var IsOnlyNumbersAndDots = Regex.IsMatch(ip, "^\\d+(\\.\\d+)+$");

            if(!IsOnlyNumbersAndDots)
            {
                throw new Exception("La ip solo puede contener numeros y puntos");
            }
        }
    }
}
