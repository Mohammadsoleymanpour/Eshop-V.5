using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Convertor
{
    public class FixedText
    {
        public static string FixEmail(string email)
        {

            return email.Trim().ToLower();

        }

    }
}
