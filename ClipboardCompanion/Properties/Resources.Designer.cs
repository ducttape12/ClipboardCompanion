﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClipboardCompanion.Properties {
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
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ClipboardCompanion.Properties.Resources", typeof(Resources).Assembly);
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
        
        /// <summary>
        ///   Looks up a localized string similar to Clipboard Companion.
        /// </summary>
        public static string ApplicationName {
            get {
                return ResourceManager.GetString("ApplicationName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Version 1.0.
        /// </summary>
        public static string ApplicationVersion {
            get {
                return ResourceManager.GetString("ApplicationVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        public static System.Drawing.Icon ClipboardCompanion {
            get {
                object obj = ResourceManager.GetObject("ClipboardCompanion", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Copyright (C) 2018  Keith Ott
        ///
        ///This program is free software: you can redistribute it and/or modify
        ///it under the terms of the GNU General Public License as published by
        ///the Free Software Foundation, either version 3 of the License, or
        ///(at your option) any later version.
        ///
        ///This program is distributed in the hope that it will be useful,
        ///but WITHOUT ANY WARRANTY; without even the implied warranty of
        ///MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
        ///GNU General Public License for more detai [rest of string was truncated]&quot;;.
        /// </summary>
        public static string License {
            get {
                return ResourceManager.GetString("License", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Visit KeithOtt.com for more free utilities, games, and more!.
        /// </summary>
        public static string WebsiteDescription {
            get {
                return ResourceManager.GetString("WebsiteDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://www.keithott.com/.
        /// </summary>
        public static string WebsiteLink {
            get {
                return ResourceManager.GetString("WebsiteLink", resourceCulture);
            }
        }
    }
}
