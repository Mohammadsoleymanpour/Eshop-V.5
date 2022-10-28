using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Security
{
    public static class GetEnumName
    {
        public static string GetEnumDisPlayName(this System.Enum myEnum)
        {
            var enumDisplayName = myEnum.GetType()
                .GetMember(myEnum.ToString())
                .FirstOrDefault();

            if (enumDisplayName != null)
            {
                return enumDisplayName.GetCustomAttribute<DisplayAttribute>().GetName();
            }

            return "";
        }
    }
}
