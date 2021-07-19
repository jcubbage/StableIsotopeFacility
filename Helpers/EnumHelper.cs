using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SIFCore.Helpers
{
    public static class EnumHelper
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

        
        public static List<string> GetListOfDisplayNames<T>() where T : struct
        {
            Type t = typeof(T);
            return !t.IsEnum ? null : Enum.GetValues(t).Cast<Enum>().Select(x => x.GetDisplayName()).ToList();
        }

        public static List<SelectListItem> GetSelectListItems<T>() where T : struct
        {
             Type t = typeof(T);
             return !t.IsEnum ? null : Enum.GetValues(t).Cast<Enum>().Select(x => new SelectListItem(x.GetDisplayName(), x.GetDisplayName())).ToList();

        }

        public static List<SelectListItem> GetSelectListItemsWithDisplay<T>() where T : struct
        {
             Type t = typeof(T);
             return !t.IsEnum ? null : Enum.GetValues(t).Cast<Enum>().Select(x => new SelectListItem(x.GetDisplayName(), x.ToString())).ToList();

        }

       
    }
}
