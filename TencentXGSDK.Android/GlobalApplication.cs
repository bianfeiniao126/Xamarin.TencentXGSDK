using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Tencent.Android.Tpush;

namespace TencentXGSDK.Android
{
    [Application(Label = "@string/ApplicationName", Icon = "@drawable/ic_launcher")]

    // push服务 

    //android:name="com.tencent.android.tpush.service.XGPushService" android:exported="true" android:persistent="true" android:process=":xg_service_v2"
    public class GlobalApplication : Application
    {
        protected GlobalApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

        public override void OnCreate()
        {
            base.OnCreate();
            InitNotificationBuilder(this.ApplicationContext);
            XGPushManager.RegisterPush(ApplicationContext);
        }

        private void InitNotificationBuilder(Context context)
        {
            // 新建自定义样式
            var build = new XGBasicPushNotificationBuilder();
            // 设置自定义样式属性，该属性对对应的编号生效，指定后不能修改。
            build.SetIcon(Resource.Drawable.ic_launcher)
                    .SetSound(
                            RingtoneManager.GetActualDefaultRingtoneUri(
                                    ApplicationContext, RingtoneType.Alarm)) // 设置声音
                    .SetDefaults(NotificationDefaults.Vibrate) // 振动
                    .SetFlags(NotificationFlags.NoClear); // 是否可清除
            // 设置通知样式，样式编号为2，即build_id为2，可通过后台脚本指定
            XGPushManager.SetPushNotificationBuilder(this, 2, build);

            // 下同

            var build11 = new XGBasicPushNotificationBuilder();
            build11.SetIcon(Resource.Drawable.ic_launcher)
                    .SetSound(
                            RingtoneManager
                                    .GetDefaultUri(RingtoneType.Alarm))
                    .SetVibrate(new long[] { 1000, 1000, 1000, 1000, 1000 })
                    .SetFlags(NotificationFlags.NoClear);
            XGPushManager.SetPushNotificationBuilder(this, 5, build11);
        }
    }
}