﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DZRichPresenceClient.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("DayZ Rich Presence")]
        public string ApplicationName {
            get {
                return ((string)(this["ApplicationName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("527063225661915136")]
        public string ApplicationId {
            get {
                return ((string)(this["ApplicationId"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("rich_presence.json")]
        public string PresenceDataFile {
            get {
                return ((string)(this["PresenceDataFile"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://github.com/Kreyu/dayz-rich-presence")]
        public string RepositoryUrl {
            get {
                return ((string)(this["RepositoryUrl"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("dayz-rich-presence")]
        public string RepositoryId {
            get {
                return ((string)(this["RepositoryId"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Kreyu")]
        public string RepositoryOwner {
            get {
                return ((string)(this["RepositoryOwner"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://github.com/Kreyu/dayz-rich-presence/releases")]
        public string ReleasesUrl {
            get {
                return ((string)(this["ReleasesUrl"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Months, not years</string>
  <string>Would you look at that</string>
  <string>Who's shooting in cherno?</string>
  <string>Heyy hey, I'm friendly</string>
  <string>Beans before friends</string>
  <string>Loot spawned for you</string>
  <string>/scream</string>
  <string>Party at Balota</string>
  <string>Connection with the host has been lost</string>
  <string>Rotten kiwis everywhere</string>
  <string>Yikees</string>
  <string>Hitreg supported</string>
  <string>Oh boy, that wasn't a desync</string>
  <string>*licks the battery*</string>
  <string>I have a funny taste in my mouth</string>
  <string>My stomach grumbled violently</string>
  <string>Battle Royale included</string>
  <string>That's a lot of damage</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection LargeImageTexts {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["LargeImageTexts"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.1.1")]
        public string Version {
            get {
                return ((string)(this["Version"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://steamcommunity.com/sharedfiles/filedetails/?id=1605419390")]
        public string WorkshopUrl {
            get {
                return ((string)(this["WorkshopUrl"]));
            }
            set {
                this["WorkshopUrl"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("rich_presence.log")]
        public string LogFile {
            get {
                return ((string)(this["LogFile"]));
            }
            set {
                this["LogFile"] = value;
            }
        }
    }
}
