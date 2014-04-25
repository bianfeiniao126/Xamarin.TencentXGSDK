using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libXG-SDK.a", LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator, ForceLoad = true,Frameworks="CFNetwork SystemConfiguration CoreTelephony")]
