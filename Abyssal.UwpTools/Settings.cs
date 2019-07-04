using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace Abyssal.UwpTools
{
    public static class Settings
    {
        private static ApplicationDataContainer _settingsContainer;

        public static ApplicationDataContainer GetSettingsContainer()
        {
            if (_settingsContainer == null) _settingsContainer = Data.GetContainer("settings");
            return _settingsContainer;
        }

        public static IPropertySet GetSettings()
        {
            return GetSettingsContainer().Values;
        }

        public static object GetSetting(string name, object valueToSetIfNotExist = null)
        {
            var settings = GetSettings();
            if (!settings.ContainsKey(name))
            {
                if (valueToSetIfNotExist == null) return null;
                settings[name] = valueToSetIfNotExist;
            }
            return settings[name];
        }

        public static void SetSetting(string name, object value)
        {
            var settings = GetSettings();
            settings[name] = value;
        }
    }
}
