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

    // push���� 

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
            // �½��Զ�����ʽ
            var build = new XGBasicPushNotificationBuilder();
            // �����Զ�����ʽ���ԣ������ԶԶ�Ӧ�ı����Ч��ָ�������޸ġ�
            build.SetIcon(Resource.Drawable.ic_launcher)
                    .SetSound(
                            RingtoneManager.GetActualDefaultRingtoneUri(
                                    ApplicationContext, RingtoneType.Alarm)) // ��������
                    .SetDefaults(NotificationDefaults.Vibrate) // ��
                    .SetFlags(NotificationFlags.NoClear); // �Ƿ�����
            // ����֪ͨ��ʽ����ʽ���Ϊ2����build_idΪ2����ͨ����̨�ű�ָ��
            XGPushManager.SetPushNotificationBuilder(this, 2, build);

            // ��ͬ

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