﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PacSharpApp.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PacSharpApp.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;map version=&quot;1.0&quot; tiledversion=&quot;1.0.3&quot; orientation=&quot;orthogonal&quot; renderorder=&quot;right-down&quot; width=&quot;28&quot; height=&quot;36&quot; tilewidth=&quot;8&quot; tileheight=&quot;8&quot; nextobjectid=&quot;58&quot;&gt;
        /// &lt;tileset firstgid=&quot;1&quot; name=&quot;Tiles&quot; tilewidth=&quot;8&quot; tileheight=&quot;8&quot; tilecount=&quot;256&quot; columns=&quot;16&quot;&gt;
        ///  &lt;image source=&quot;../Tiles.png&quot; width=&quot;128&quot; height=&quot;128&quot;/&gt;
        /// &lt;/tileset&gt;
        /// &lt;layer name=&quot;Maze&quot; width=&quot;28&quot; height=&quot;36&quot; opacity=&quot;0.9&quot;&gt;
        ///  &lt;data encoding=&quot;csv&quot;&gt;
        ///0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string OriginalMaze {
            get {
                return ResourceManager.GetString("OriginalMaze", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Palettes {
            get {
                object obj = ResourceManager.GetObject("Palettes", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Sprites {
            get {
                object obj = ResourceManager.GetObject("Sprites", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Tiles {
            get {
                object obj = ResourceManager.GetObject("Tiles", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
