﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YeSql.Net {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ExceptionMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExceptionMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("YeSql.Net.Common.Resources.ExceptionMessages", typeof(ExceptionMessages).Assembly);
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
        ///   Looks up a localized string similar to &apos;{0}&apos; collection cannot contain elements with a null value, an empty string or consists only of white-space characters..
        /// </summary>
        internal static string CollectionHasNullValueOrOnlyWhitespace {
            get {
                return ResourceManager.GetString("CollectionHasNullValueOrOnlyWhitespace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data source is empty or consists only in whitespace..
        /// </summary>
        internal static string DataSourceIsEmptyOrWhitespace {
            get {
                return ResourceManager.GetString("DataSourceIsEmptyOrWhitespace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}: error: No such directory exists..
        /// </summary>
        internal static string DirectoryNotFound {
            get {
                return ResourceManager.GetString("DirectoryNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The given tag &apos;{0}&apos; is duplicated..
        /// </summary>
        internal static string DuplicateTagName {
            get {
                return ResourceManager.GetString("DuplicateTagName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to error: &apos;{0}&apos; has no sql extension..
        /// </summary>
        internal static string FileHasNotSqlExtension {
            get {
                return ResourceManager.GetString("FileHasNotSqlExtension", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}: error: No such file or directory..
        /// </summary>
        internal static string FileNotFound {
            get {
                return ResourceManager.GetString("FileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; line is not associated with any tag..
        /// </summary>
        internal static string LineIsNotAssociatedWithAnyTag {
            get {
                return ResourceManager.GetString("LineIsNotAssociatedWithAnyTag", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The tag name is empty..
        /// </summary>
        internal static string TagIsEmptyOrWhitespace {
            get {
                return ResourceManager.GetString("TagIsEmptyOrWhitespace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The given tag &apos;{0}&apos; is not present in the collection..
        /// </summary>
        internal static string TagNotFound {
            get {
                return ResourceManager.GetString("TagNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to error: No tag found in the collection..
        /// </summary>
        internal static string TagNotFoundDefault {
            get {
                return ResourceManager.GetString("TagNotFoundDefault", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to error: Loader found an error while loading the SQL file..
        /// </summary>
        internal static string YeSqlLoaderDefault {
            get {
                return ResourceManager.GetString("YeSqlLoaderDefault", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to error: Parser found syntax errors..
        /// </summary>
        internal static string YeSqlParserDefault {
            get {
                return ResourceManager.GetString("YeSqlParserDefault", resourceCulture);
            }
        }
    }
}
