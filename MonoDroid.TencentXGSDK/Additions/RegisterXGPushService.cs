using System;
using System.Collections.Generic;
using System.Text;
using Com.Tencent.Android.Tpush.Service;
using Android.App;
namespace MonoDroid.TencentXGSDK
{
    [Service(Exported = true, Process = ":xg_service_v2")]
    public  class RegisterXGPushService : XGPushService
    {
    }
}
