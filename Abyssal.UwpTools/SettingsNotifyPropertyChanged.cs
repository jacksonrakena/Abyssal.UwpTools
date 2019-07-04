using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyssal.UwpTools
{
    public class SettingsNotifyPropertyChanged : SettingsNotifyPropertyChanged<string>
    {
        public SettingsNotifyPropertyChanged(string key, string defaultValue)
            : base(key, defaultValue, a => a, b => b)
        {
        }
    }

    public class SettingsNotifyPropertyChanged<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Func<string, T> _func;
        private readonly Func<T, string> _reverser;
        private readonly string _name;

        public T Value
        {
            get
            {
                var val = Settings.GetSetting(_name);
                if (val == null) return default;
                return _func(val.ToString());
            }
            set
            {
                Settings.SetSetting(_name, _reverser(value));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }
        public override string ToString()
        {
            return Value.ToString();
        }

        public SettingsNotifyPropertyChanged(string name, T defaultValue, Func<string, T> func, Func<T, string> reverser)
        {
            _reverser = reverser;
            _func = func;
            _name = name;
            if (Value == null) Value = defaultValue;
        }
    }

    public class SettingsNotifyPropertyChanged<TFriendlyValue, TStoreValue> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TFriendlyValue FriendlyValue
        {
            get
            {
                var vx = StoreValue;
                if (vx == null) return default;
                return _friendlyCalculate(vx);
            }
        }

        public TStoreValue StoreValue
        {
            get
            {
                var v = Settings.GetSetting(_settingsName, _defaultValue);
                if (v == null) return default;
                var proc = _storeCalculate(v.ToString());
                return proc;
            }
            set
            {
                var a = _storeCompute(value);
                Settings.SetSetting(_settingsName, a);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StoreValue)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FriendlyValue)));
            }
        }

        private readonly string _settingsName;
        private readonly string _defaultValue;
        private readonly Func<TStoreValue, string> _storeCompute;
        private readonly Func<string, TStoreValue> _storeCalculate;
        private readonly Func<TStoreValue, TFriendlyValue> _friendlyCalculate;

        public SettingsNotifyPropertyChanged(string settingsName, TStoreValue defaultValue,
            Func<TStoreValue, string> storeCompute, Func<string, TStoreValue> storeCalculate, Func<TStoreValue, TFriendlyValue> friendlyCalculate)
        {
            _settingsName = settingsName;
            _defaultValue = storeCompute(defaultValue);
            _storeCompute = storeCompute;
            _storeCalculate = storeCalculate;
            _friendlyCalculate = friendlyCalculate;
        }
    }

    public class SettingsEnumNotifyPropertyChanged<TEnum> : SettingsNotifyPropertyChanged<string, TEnum> where TEnum : struct
    {
        public SettingsEnumNotifyPropertyChanged(string settingsName, TEnum defaultValue, Func<TEnum, string> friendlyCalculate)
            : base(settingsName, defaultValue, a => a.ToString(), b => Enum.Parse<TEnum>(b), friendlyCalculate)
        {
        }
    }

    public class SettingsNotifyPropertyChanged2<TFriendlyValue, TRawType> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TFriendlyValue FriendlyValue
        {
            get
            {
                var vx = StoreValue;
                if (vx == null) return default;
                return _friendlyCalculate(vx);
            }
        }

        public TRawType StoreValue
        {
            get
            {
                var v = Settings.GetSetting(_settingsName, _defaultValue);
                if (v == null) return default;
                return (TRawType) v;
            }
            set
            {
                Settings.SetSetting(_settingsName, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StoreValue)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FriendlyValue)));
            }
        }

        private readonly string _settingsName;
        private readonly TRawType _defaultValue;
        private readonly Func<TRawType, TFriendlyValue> _friendlyCalculate;

        public SettingsNotifyPropertyChanged2(string settingsName, TRawType defaultValue,
            Func<TRawType, TFriendlyValue> friendlyCalculate)
        {
            _settingsName = settingsName;
            _defaultValue = defaultValue;
            _friendlyCalculate = friendlyCalculate;
        }
    }
}