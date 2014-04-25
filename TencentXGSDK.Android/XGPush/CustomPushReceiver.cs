using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using Com.Tencent.Android.Tpush;
using Org.Json;

namespace TencentXGSDK.Android.XGPush
{
    /*
         <receiver android:name="com.tencent.xgpushdemo.CustomPushReceiver" >
            <intent-filter>

                <!-- ������Ϣ͸�� -->
                <action android:name="com.tencent.android.tpush.action.PUSH_MESSAGE" />
                <!-- ����ע�ᡢ��ע�ᡢ����/ɾ����ǩ��֪ͨ������ȴ����� -->
                <action android:name="com.tencent.android.tpush.action.FEEDBACK" />
            </intent-filter>
        </receiver>
     */
    [BroadcastReceiver]
    [IntentFilter(new String[]{
        "com.tencent.android.tpush.action.PUSH_MESSAGE",
        "com.tencent.android.tpush.action.FEEDBACK"
    })]
    public class CustomPushReceiver : XGPushBaseReceiver
    {
        // public static String LogTag = "TPushReceiver";
        public static String LogTag = typeof(CustomPushReceiver).FullName;
        protected CustomPushReceiver(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

        public CustomPushReceiver() : base() { }
        private void show(Context context, String text)
        {
            Toast.MakeText(context, text, ToastLength.Short).Show();
        }

        /**
         * ע����
         * 
         * @param context
         *            APP�����Ķ���
         * @param errorCode
         *            �����룬{@link XGPushBaseReceiver#SUCCESS}��ʾ�ɹ���������ʾʧ��
         * @param registerMessage
         *            ע��������
         */
        public override void OnRegisterResult(Context context, int errorCode,
                XGPushRegisterResult registerMessage)
        {
            if (context == null || registerMessage == null)
            {
                return;
            }
            String text = null;
            if (errorCode == XGPushBaseReceiver.Success)
            {
                text = registerMessage + "ע��ɹ�";
                // ��������token
                String token = registerMessage.Token;
            }
            else
            {
                text = registerMessage + "ע��ʧ�ܣ������룺" + errorCode;
            }
            Log.Debug(LogTag, text);
            show(context, text);
        }

        /**
         * ��ע����
         * 
         * @param context
         *            APP�����Ķ���
         * @param errorCode
         *            �����룬{@link XGPushBaseReceiver#SUCCESS}��ʾ�ɹ���������ʾʧ��
         */
        public override void OnUnregisterResult(Context context, int errorCode)
        {
            if (context == null)
            {
                return;
            }
            String text = null;
            if (errorCode == XGPushBaseReceiver.Success)
            {
                text = "��ע��ɹ�";
            }
            else
            {
                text = "��ע��ʧ��" + errorCode;
            }
            Log.Debug(LogTag, text);
            show(context, text);
        }

        /**
         * ���ñ�ǩ�������
         * 
         * @param context
         *            APP�����Ķ���
         * @param errorCode
         *            �����룬{@link XGPushBaseReceiver#SUCCESS}��ʾ�ɹ���������ʾʧ��
         * @tagName ��ǩ����
         */
        public override void OnSetTagResult(Context context, int errorCode, String tagName)
        {
            if (context == null)
            {
                return;
            }
            String text = null;
            if (errorCode == XGPushBaseReceiver.Success)
            {
                text = "\"" + tagName + "\"���óɹ�";
            }
            else
            {
                text = "\"" + tagName + "\"����ʧ��,�����룺" + errorCode;
            }
            Log.Debug(LogTag, text);
            show(context, text);
        }

        /**
         * ɾ����ǩ�������
         * 
         * @param context
         *            APP�����Ķ���
         * @param errorCode
         *            �����룬{@link XGPushBaseReceiver#SUCCESS}��ʾ�ɹ���������ʾʧ��
         * @tagName ��ǩ����
         */
        public override void OnDeleteTagResult(Context context, int errorCode, String tagName)
        {
            if (context == null)
            {
                return;
            }
            String text = null;
            if (errorCode == XGPushBaseReceiver.Success)
            {
                text = "\"" + tagName + "\"ɾ���ɹ�";
            }
            else
            {
                text = "\"" + tagName + "\"ɾ��ʧ��,�����룺" + errorCode;
            }
            Log.Debug(LogTag, text);
            show(context, text);
        }

        /**
         * �յ���Ϣ<br>
         * 
         * @param context
         *            APP�����Ķ���
         * @param message
         *            �յ�����Ϣ
         */

        public override void OnTextMessage(Context context, XGPushTextMessage message)
        {
            if (context == null || message == null)
            {
                return;
            }
            String text = "�յ���Ϣ:" + message.ToString();
            // ��ȡ�Զ���key-value
            String customContent = message.CustomContent;
            if (!string.IsNullOrEmpty(customContent))
            {
                try
                {
                    JSONObject obj = new JSONObject(customContent);
                    // key1Ϊǰ̨���õ�key
                    if (!obj.IsNull("key"))
                    {
                        String value = obj.GetString("key");
                        Log.Debug(LogTag, "get custom value:" + value);
                    }
                    // ...
                }
                catch (JSONException e)
                {
                    e.PrintStackTrace();
                }
            }
            // APP����������Ϣ�Ĺ��̡�����
            Log.Debug(LogTag, text);
            show(context, text);
        }

        /**
         * ֪ͨ���򿪽������
         * 
         * @param context
         *            APP�����Ķ���
         * @param message
         *            ���򿪵���Ϣ����
         */

        public override void OnNotifactionClickedResult(Context context,
                XGPushClickedResult message)
        {
            if (context == null || message == null)
            {
                return;
            }
            String text = "֪ͨ���� :" + message;
            // ��ȡ�Զ���key-value
            String customContent = message.CustomContent;
            if (!string.IsNullOrEmpty(customContent))
            {
                try
                {
                    JSONObject obj = new JSONObject(customContent);
                    // key1Ϊǰ̨���õ�key
                    if (!obj.IsNull("key"))
                    {
                        String value = obj.GetString("key");
                        Log.Debug(LogTag, "get custom value:" + value);
                    }
                    // ...
                }
                catch (JSONException e)
                {
                    e.PrintStackTrace();
                }
            }
            // APP��������Ĺ��̡�����
            Log.Debug(LogTag, text);
            // show(context, text);
        }

        public override void OnNotifactionShowedResult(Context context,
                XGPushShowedResult notifiShowedRlt)
        {
            if (context == null || notifiShowedRlt == null)
            {
                return;
            }
            /*
            NotificationManager nm = (NotificationManager)context.GetSystemService("NOTIFICATION_SERVICE");
            var no = XGPushManager.GetNotificationBuilder(context, 2);
            no.SetTitle(notifiShowedRlt.Title);
            nm.Notify(1111,no.BuildNotification(context));
             */
            //notifiShowedRlt.Notify();

            //XGPushManager.AddLocalNotification(context, new XGLocalMessage(){BuilderId = notifiShowedRlt.MsgId,Content = notifiShowedRlt.Title});

            String text = "֪ͨ��չʾ ��title:" + notifiShowedRlt.Title
                    + ",content:" + notifiShowedRlt.Content
                    + ",custom_content:" + notifiShowedRlt.CustomContent;
            Log.Debug(LogTag, text);
            show(context, text);
        }
    }
}