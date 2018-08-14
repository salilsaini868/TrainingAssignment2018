using System.Web;
using System.Web.Optimization;

namespace TrainingProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // create an object of ScriptBundle and 
            // specify bundle name (as virtual path) as constructor parameter 
            ScriptBundle javascriptBundle = new ScriptBundle("~/bundles/Project_Scripts");
            ScriptBundle bootstrapBundle = new ScriptBundle("~/bundles/Bootstrap");
            
            //use Include() method to add all the script files with their paths 
            javascriptBundle.Include(
                           "~/Scripts/Product.js",
                           "~/Scripts/Login.js",
                           "~/Scripts/CategoryInsert.js",
                           "~/Scripts/CategoryList.js",
                           "~/Scripts/jquery - 1.10.2.min.js",
                           "~/Scripts/jquery-3.2.1.js"
                         );

            bootstrapBundle.Include(
                         "~/Scripts/bootstrap.min.js",
                         "~/Scripts/modernizr-2.6.2.js",
                         "~/Scripts/custom.js",
                         "~/Scripts/popper.min.js"
                );
            
            //Add the bundle into BundleCollection
            bundles.Add(javascriptBundle);
            bundles.Add(bootstrapBundle);
            

            BundleTable.EnableOptimizations = true;
        }
    }
}
