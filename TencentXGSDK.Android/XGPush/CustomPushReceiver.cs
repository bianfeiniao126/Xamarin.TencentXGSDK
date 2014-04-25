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

                <!-- 接收消息透传 -->
                <action android:name="com.tencent.android.tpush.action.PUSH_MESSAGE" />
                <!-- 监听注册、反注册、设置/删除标签、通知被点击等处理结果 -->
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
         * 注册结果
         * 
         * @param context
         *            APP上下文对象
         * @param errorCode
         *            错误码，{@link XGPushBaseReceiver#SUCCESS}表示成功，其它表示失败
         * @param registerMessage
         *            注册结果返回
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
                text = registerMessage + "注册成功";
                // 在这里拿token
                String token = registerMessage.Token;
            }
            else
            {
                text = registerMessage + "注册失败，错误码：" + errorCode;
            }
            Log.Debug(LogTag, text);
            show(context, text);
        }

        /**
         * 反注册结果
         * 
         * @param context
         *            APP上下文对象
         * @param errorCode
         *            错误码，{@link XGPushBaseReceiver#SUCCESS}表示成功，其它表示失败
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
                text = "反注册成功";
            }
            else
            {
                text = "反注册失败" + errorCode;
            }
            Log.Debug(LogTag, text);
            show(context, text);
        }

        /**
         * 设置标签操作结果
         * 
         * @param context
         *            APP上下文对象
         * @param errorCode
         *            错误码，{@link XGPushBaseReceiver#SUCCESS}表示成功，其它表示失败
         * @tagName 标签名称
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
                text = "\"" + tagName + "\"设置成功";
            }
            else
            {
                text = "\"" + tagName + "\"设置失败,错误码：" + errorCode;
            }
            Log.Debug(LogTag, text);
            show(context, text);
        }

        /**
         * 删除标签操作结果
         * 
         * @param context
         *            APP上下文对象
         * @param errorCode
         *            错误码，{@link XGPushBaseReceiver#SUCCESS}表示成功，其它表示失败
         * @tagName 标签名称
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
                text = "\"" + tagName + "\"删除成功";
            }
            else
            {
                text = "\"" + tagName + "\"删除失败,错误码：" + errorCode;
            }
            Log.Debug(LogTag, text);
            show(context, text);
        }

        /**
         * 收到消息<br>
         * 
         * @param context
         *            APP上下文对象
         * @param message
         *            收到的消息
         */

        public override void OnTextMessage(Context context, XGPushTextMessage message)
        {
            if (context == null || message == null)
            {
                return;
            }
            String text = "收到消息:" + message.ToString();
            // 获取自定义key-value
            String customContent = message.CustomContent;
            if (!string.IsNullOrEmpty(customContent))
            {
                try
                {
                    JSONObject obj = new JSONObject(customContent);
                    // key1为前台配置的key
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
            // APP自主处理消息的过程。。。
            Log.Debug(LogTag, text);
            show(context, text);
        }

        /**
         * 通知被打开结果反馈
         * 
         * @param context
         *            APP上下文对象
         * @param message
         *            被打开的消息对象
         */

        public override void OnNotifactionClickedResult(Context context,
                XGPushClickedResult message)
        {
            if (context == null || message == null)
            {
                return;
            }
            String text = "通知被打开 :" + message;
            // 获取自定义key-value
            String customContent = message.CustomContent;
            if (!string.IsNullOrEmpty(customContent))
            {
                try
                {
                    JSONObject obj = new JSONObject(customContent);
                    // key1为前台配置的key
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
            // APP自主处理的过程。。。
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

            String text = "通知被展示 ，title:" + notifiShowedRlt.Title
                    + ",content:" + notifiShowedRlt.Content
                    + ",custom_content:" + notifiShowedRlt.CustomContent;
            Log.Debug(LogTag, text);
            show(context, text);
        }
    }
}