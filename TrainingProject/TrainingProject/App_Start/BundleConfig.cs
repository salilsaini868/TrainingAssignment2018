using System.Web;
using System.Web.Optimization;

namespace TrainingProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            // create an object of ScriptBundle and 
            // specify bundle name (as virtual path) as constructor parameter 
            ScriptBundle javascriptBundle = new ScriptBundle("~/bundles/Project_Scripts");
            ScriptBundle bootstrapBundle = new ScriptBundle("~/bundles/Bootstrap");
            StyleBundle LayoutCSS = new StyleBundle("~/bundles/LayoutCSS");


            //use Include() method to add all the script files with their paths 
            javascriptBundle.Include(
                           "~/Scripts/Product.js",
                           "~/Scripts/Login.js",
                           "~/Scripts/CategoryInsert.js",
                           "~/Scripts/CategoryList.js",
                           "~/Scripts/jquery - 1.10.2.min.js",
                           "~/Scripts/jquery-3.2.1.js",
                           "~/Scripts/ProductList.js"
                         );

            bootstrapBundle.Include(
                         "~/Scripts/bootstrap.min.js",
                         "~/Scripts/modernizr-2.6.2.js",
                         "~/Scripts/custom.js",
                         "~/Scripts/popper.min.js"
                );
            
            LayoutCSS.Include(
                       "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                       "~/Content/style.css",
                        "~/Content/LayoutStyle.css"
               );


            //Add the bundle into BundleCollection
            bundles.Add(javascriptBundle);
            bundles.Add(bootstrapBundle);
            bundles.Add(LayoutCSS);
            
            BundleTable.EnableOptimizations = true;
        }
    }
}
