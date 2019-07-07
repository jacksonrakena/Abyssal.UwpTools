# Abyssal.UwpTools
A set of tools and utilities used in my UWP applications. These tools and utilities are highly specialized and modified to work with my UWP applications, and as such, might require some tinkering to work with your applications.

### Contents
If you're an experienced UWP/WPF developer, I apologize greatly, because this repository might be disgusting to read. I'm really new to UWP and WPF, so please forgive me.
- `Data.cs` A wrapper for `Windows.Storage.ApplicationData.Current`, which currently uses `RoamingSettings` and `RoamingFolder`, which is used in my UWP projects to persist configuration and data.
- `Settings.cs` A wrapper around a `Windows.Storage.ApplicationDataContainer`, allowing easy access to get, set, and unset configuration properties (this is used with `Data`).
- `SettingsNotifyPropertyChanged.cs` Five implementations of `INotifyPropertyChanged` for easy use with data binding (`x:Bind`, `Binding`) in XAML applications:
  - `SettingsNotifyPropertyChanged` A shortcut to `SettingsNotifyPropertyChanged<string>`.
  - `SettingsNotifyPropertyChanged<T>` Contains a field of type `T` named `Value`, that, when changed, notifies all event handlers of `PropertyChanged` that the value has changed. It also persists the value to `Settings`, using the property name provided in the first constructor arguments. It also takes `Func<string, T> func` and `Func<T, string> reverser`, which are two functions that are responsible for converting the value to a string for storing in `Settings`, and also converting the string value returned from `Settings` into a `T` which is returned by `Value`.
  - `SettingsNotifyPropertyChanged<TFriendlyValue, TStoreValue>` A three-way, more complex version of `SettingsNotifyPropertyChanged<T>`, this one has two fields: `FriendlyValue`, and `StoreValue`, representing their type parameters respectively. When `StoreValue` is updated, it is persisted to the database and all event handlers are notified that both `StoreValue` and `FriendlyValue` have changed. `FriendlyValue` is computed from `StoreValue` using `_friendlyCalcuate`, which is provided in the constructor. This class persists all data in `Settings` as `System.String`, and as such, requires reverser/computer functions (the same as `SettingsNotifyPropertyChanged<T>`), but requires an additional function to calculate the friendly value.
  - `SettingsEnumNotifyPropertyChanged<TEnum>` A simplified version of `SettingsNotifyPropertyChanged<string, TEnum>`, which provides all the reverser and computer functions as `Enum.ToString` and `Enum.Parse<TEnum>`.
  - `SettingsNotifyPropertyChanged2<TFriendlyValue, TRawType>` An alternative version of `SettingsNotifyPropertyChanged<TFriendlyValue, TStoreValue>`, that stores the `TRawType` directly, instead of serializing and deserializing it to and from a `System.String`. `TRawType` should be an applicable data type defined under [Types of app data.](https://docs.microsoft.com/en-us/windows/uwp/design/app-settings/store-and-retrieve-app-data#types-of-app-data)
  

## Copyright
Copyright (c) 2019 abyssal512, licensed under the MIT License.
