﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gravitas.Platform.Web.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Gravitas.Platform.Web.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to ТОВ КЕ. Транспорт ##TransportNo## ##TrailerNo##  очікують на ##DestinationPoint##..
        /// </summary>
        internal static string DestinationPointApprovalSms {
            get {
                return ResourceManager.GetString("DestinationPointApprovalSms", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ТОВ КЕ. Транспорту  ##TransportNo## ##TrailerNo##  необхідне узгодження якості ##ProductName##. Чекайте інформацію про результати узгодження..
        /// </summary>
        internal static string DriverQualityMatchingSms {
            get {
                return ResourceManager.GetString("DriverQualityMatchingSms", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ТОВ КЕ. Транспорту  ##TransportNo## ##TrailerNo##  дозволений в&apos;їзд..
        /// </summary>
        internal static string EntrenceApprovalSms {
            get {
                return ResourceManager.GetString("EntrenceApprovalSms", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ТОВ КЕ. Транспорту ##TransportNo## ##TrailerNo## необхідно підтвердження охорони на ваговій платформі №##Number##..
        /// </summary>
        internal static string InvalidPerimeterSms {
            get {
                return ResourceManager.GetString("InvalidPerimeterSms", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ТОВ КЕ телефон диспетчера +##DispatcherPhoneNumber##. Транспорт ##TransportNo## ##TrailerNo##  зареєстрований в черзі вантажу ##ProductName##. Орієнтовний час в&apos;їзду ##EntraceTime##. Чекайте наступного СМС або стежте за інформацією на табло..
        /// </summary>
        internal static string QueueRegistrationSms {
            get {
                return ResourceManager.GetString("QueueRegistrationSms", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ТОВ КЕ. Транспорту ##TransportNo## ##TrailerNo##  необхідне узгодження якості ##ProductName##. Додаткова інформація на пошті ##Email##..
        /// </summary>
        internal static string RequestForApprovalSms {
            get {
                return ResourceManager.GetString("RequestForApprovalSms", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ТОВ КЕ. Транспорту ##TransportNo## ##TrailerNo##  змінили маршрут. Проїдьте будь ласка на ##DestinationPoint##. Телефон диспетчера +##DispatcherPhoneNumber##..
        /// </summary>
        internal static string RouteChangeSms {
            get {
                return ResourceManager.GetString("RouteChangeSms", resourceCulture);
            }
        }
    }
}