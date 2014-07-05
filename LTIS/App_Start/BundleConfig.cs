using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace LTIS
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/scripts/libraries")
                    .Include("~/scripts/jquery-2.1.0.min.js")
                    .Include("~/scripts/toastr.js")
                    .Include("~/scripts/kendo/kendo.ui.core.min.js")
                    .Include("~/scripts/kendo/kendo.window.min.js")
                    .Include("~/scripts/app.js")
            );

            bundles.Add(
                new StyleBundle("~/content/css")
                    .Include("~/content/normalize.css")
                    .Include("~/content/kendo.common.min.css")
                    .Include("~/content/kendo.blueopal.min.css")
                    .Include("~/content/foundation.css")
                    .Include("~/content/toastr.css")
					.Include("~/content/app.css")
				);
        }
    }
}