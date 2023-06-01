using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Oprosnik
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new Bundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));


            bundles.Add(new Bundle("~/bundles/bootstrap").Include(                                              
                        "~/Scripts/bootstrap.js"                        
                       ));

            bundles.Add(new Bundle("~/bundles/air-datepickerJS").Include(
                        "~/lib/air-datepicker/js/datepicker.js"
                       ));
            bundles.Add(new Bundle("~/bundles/ShowDialog").Include(
                        "~/Scripts/ShowDialog.js"
                       ));
            bundles.Add(new Bundle("~/bundles/TeacherClick").Include(
                        "~/Scripts/TeacherClick.js"
                       ));





            bundles.Add(new Bundle("~/Content/air-datepickerCSS").Include(
                   "~/lib/air-datepicker/css/datepicker.css"
                  ));


            bundles.Add(new Bundle("~/Content/css").Include(
                   "~/Content/bootstrap.css",
                   "~/Content/site.css"));
        }
    }
}