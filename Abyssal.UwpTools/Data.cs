using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Abyssal.UwpTools
{
    public static class Data
    {
        public static ApplicationDataContainer GetRootContainer()
        {
            return ApplicationData.Current.RoamingSettings;
        }

        public static ApplicationDataContainer GetContainer(string name)
        {
            return ApplicationData.Current.RoamingSettings.CreateContainer(name, ApplicationDataCreateDisposition.Always);
        }

        public static StorageFolder GetFolder()
        {
            return ApplicationData.Current.RoamingFolder;
        }
    }
}
