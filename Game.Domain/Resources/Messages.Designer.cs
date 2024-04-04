﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Game.Domain.Resources {
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
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Game.Domain.Resources.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Oops! Something went wrong on our end. We&apos;re sorry for the inconvenience. Our team has been notified, and we&apos;re working to fix the issue as soon as possible. Please try again later..
        /// </summary>
        public static string BASE_ERROR_MESSAGE {
            get {
                return ResourceManager.GetString("BASE_ERROR_MESSAGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The board cells are required.
        /// </summary>
        public static string BOARD_CELLS_IS_REQUIRED {
            get {
                return ResourceManager.GetString("BOARD_CELLS_IS_REQUIRED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The board cells must be between 10 and 100.
        /// </summary>
        public static string BOARD_CELLS_OUT_OF_RANGE {
            get {
                return ResourceManager.GetString("BOARD_CELLS_OUT_OF_RANGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The board is required..
        /// </summary>
        public static string BOARD_IS_REQUIRED {
            get {
                return ResourceManager.GetString("BOARD_IS_REQUIRED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The board must be a square between 10 and 100.
        /// </summary>
        public static string BOARD_MUST_BE_A_SQUARE {
            get {
                return ResourceManager.GetString("BOARD_MUST_BE_A_SQUARE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Board not found with id.
        /// </summary>
        public static string BOARD_STATE_NOT_FOUND_WITH_ID {
            get {
                return ResourceManager.GetString("BOARD_STATE_NOT_FOUND_WITH_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Current board not found.
        /// </summary>
        public static string CURRRENT_BOARD_STATE_NOT_FOUND {
            get {
                return ResourceManager.GetString("CURRRENT_BOARD_STATE_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The height must be between 10 and 100.
        /// </summary>
        public static string HEIGHT_OUT_OF_RANGE {
            get {
                return ResourceManager.GetString("HEIGHT_OUT_OF_RANGE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Board&apos;s grid is invalid.
        /// </summary>
        public static string INVALID_BOARD_GRID {
            get {
                return ResourceManager.GetString("INVALID_BOARD_GRID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The current board id is invalid.
        /// </summary>
        public static string INVALID_BOARD_ID {
            get {
                return ResourceManager.GetString("INVALID_BOARD_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot generate the games&apos; final state.
        /// </summary>
        public static string INVALID_FINAL_STATE {
            get {
                return ResourceManager.GetString("INVALID_FINAL_STATE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The number of iterations must be greater than zero.
        /// </summary>
        public static string INVALID_ITERATIONS {
            get {
                return ResourceManager.GetString("INVALID_ITERATIONS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot generate the games&apos; next state.
        /// </summary>
        public static string INVALID_NEXT_STATE {
            get {
                return ResourceManager.GetString("INVALID_NEXT_STATE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The maximum number of iterations was reached.
        /// </summary>
        public static string MAX_NUMBER_OF_ITERATIONS_WAS_REACHED {
            get {
                return ResourceManager.GetString("MAX_NUMBER_OF_ITERATIONS_WAS_REACHED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The width must be between 10 and 100.
        /// </summary>
        public static string WIDTH_OUT_OF_RANGE {
            get {
                return ResourceManager.GetString("WIDTH_OUT_OF_RANGE", resourceCulture);
            }
        }
    }
}
