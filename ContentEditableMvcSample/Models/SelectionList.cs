using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContentEditableMvcSample.Models
{
    public class SelectionList
    {
        public static IEnumerable<SelectListItem> Titles()
        {
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "Choice1Val",
                    Text = "Choice 1"
                },
                new SelectListItem
                {
                    Value = "Choice2Val",
                    Text = "Choice 2"
                },
                new SelectListItem
                {
                    Value = "Choice3Val",
                    Text = "Choice 3"
                }
            };
            return list;
        }
    }
}