using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Android.App;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("MonoDroid.TencentXGSDK")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("MonoDroid.TencentXGSDK")]
[assembly: AssemblyCopyright("Copyright © Microsoft 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Add some common permissions, these can be removed if not needed
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.ReadPhoneState)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessWifiState)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission(Android.Manifest.Permission.ReceiveBootCompleted)]
[assembly: UsesPermission(Android.Manifest.Permission.RestartPackages)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteSettings)]
[assembly: UsesPermission("android.permission.RECEIVE_USER_PRESENT")]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
[assembly: UsesPermission(Android.Manifest.Permission.WakeLock)]
[assembly: UsesPermission(Android.Manifest.Permission.KillBackgroundProcesses)]
[assembly: UsesPermission(Android.Manifest.Permission.GetTasks)]
[assembly: UsesPermission(Android.Manifest.Permission.ReadLogs)]
[assembly: UsesPermission(Android.Manifest.Permission.Vibrate)]
[assembly: UsesPermission(Android.Manifest.Permission.Bluetooth)]
[assembly: UsesPermission(Android.Manifest.Permission.BatteryStats)]

/*http://xg.qq.com/ 申请Key*/
//换成你的key
[assembly: MetaDataAttribute("XG_V2_ACCESS_ID", Value = "2100017xxx")]
[assembly: MetaDataAttribute("XG_V2_ACCESS_KEY", Value = "A457IAxxx")]