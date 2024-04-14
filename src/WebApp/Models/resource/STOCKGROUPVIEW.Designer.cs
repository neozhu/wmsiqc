namespace WebApp.Models.resource {
  using System;
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public class STOCKGROUPVIEW {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal STOCKGROUPVIEW() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApp.Models.resource.STOCKGROUPVIEW", typeof(STOCKGROUPVIEW).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
    public static string RID {
            get {
                return ResourceManager.GetString("RID", resourceCulture);
            }
    }
    public static string SUPPLIERCODE {
            get {
                return ResourceManager.GetString("SUPPLIERCODE", resourceCulture);
            }
    }
    public static string SUPPLIERNAME {
            get {
                return ResourceManager.GetString("SUPPLIERNAME", resourceCulture);
            }
    }
    public static string SKU {
            get {
                return ResourceManager.GetString("SKU", resourceCulture);
            }
    }
    public static string SKUNAME {
            get {
                return ResourceManager.GetString("SKUNAME", resourceCulture);
            }
    }
    public static string TOTAL {
            get {
                return ResourceManager.GetString("TOTAL", resourceCulture);
            }
    }
    public static string STATUS {
            get {
                return ResourceManager.GetString("STATUS", resourceCulture);
            }
    }
 }
}