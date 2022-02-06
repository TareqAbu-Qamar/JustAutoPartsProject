using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustAutoPartsProject.Countries
{
    public class Countries
    {
        public static List<SelectListItem> countries = new List<SelectListItem>();

        public Countries()
        {
            countries.Add(new SelectListItem
            {
                Text = "Amman",
                Value = "Amman",
                Selected = true
            });
            countries.Add(new SelectListItem
            {
                Text = "Ajlun",
                Value = "Ajlun"

            });
            countries.Add(new SelectListItem
            {
                Text = "Al 'Aqabah",
                Value = "Al 'Aqabah"
            });
            countries.Add(new SelectListItem
            {
                Text = "Al Balqa'",
                Value = "Al Balqa'"
            });
            countries.Add(new SelectListItem
            {
                Text = "Al Karak",
                Value = "Al Karak"
            });
            countries.Add(new SelectListItem
            {
                Text = "Al Mafraq",
                Value = "Al Mafraq"
            });
            countries.Add(new SelectListItem
            {
                Text = "At Tafilah",
                Value = "At Tafilah"
            });
            countries.Add(new SelectListItem
            {
                Text = "Az Zarqa'",
                Value = "Az Zarqa'"
            });
            countries.Add(new SelectListItem
            {
                Text = "Jarash",
                Value = "Jarash"
            });
            countries.Add(new SelectListItem
            {
                Text = "Irbid",
                Value = "Irbid"
            });
            countries.Add(new SelectListItem
            {
                Text = "Ma'an",
                Value = "Ma'an"
            });
            countries.Add(new SelectListItem
            {
                Text = "Madaba",
                Value = "Madaba"
            });
        }
    }
}