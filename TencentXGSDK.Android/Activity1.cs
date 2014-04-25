using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Tencent.Android.Tpush;
using Com.Tencent.Android.Tpush.Horse;
using Org.Json;
using Orientation = Android.Widget.Orientation;

namespace TencentXGSDK.Android
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true)]
    public class Activity1 : Activity
    {
        private static Context context = null;


        protected override void OnStart()
        {
            base.OnStart();
            XGPushClickedResult click = XGPushManager.OnActivityStarted(this);
            if (click != null)
            { // 判断是否来自信鸽的打开方式
                Toast.MakeText(this, "通知被点击:" + click.ToString(),
                        ToastLength.Short).Show();
                String customContent = click.CustomContent;
                // 获取自定义key-value
                if (!string.IsNullOrEmpty(customContent))
                {
                    try
                    {
                        JSONObject json = new JSONObject(customContent);
                        Log.Debug("TPush", "自定义key-value:" + json);
                        // 获取在线自定义key-value
                        // key1为下发的自定义key-value
                        String customValue1 = json.GetString("key1");
                        // 。。。。
                    }
                    catch (JSONException e)
                    {
                        e.PrintStackTrace();
                    }
                }
            }
        }


        protected override void OnStop()
        {
            // TODO Auto-generated method stub
            base.OnStop();
            XGPushManager.OnActivityStoped(this);
        }

        /**
         * 设置自定义样式，这样在下发通知时可以指定build_id。编号由开发者自己维护
         * 
         * @param context
         */
      


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            context = this.ApplicationContext;
            // 动态注册信鸽的回调
            // CustomPushReceiver pushReciver = new CustomPushReceiver();
            // IntentFilter intentFilter = new IntentFilter();
            // intentFilter.addAction(XGPushConstants.ACTION_FEEDBACK);
            // intentFilter.addAction(XGPushConstants.ACTION_PUSH_MESSAGE);
            // this.getApplicationContext().registerReceiver(pushReciver,
            // intentFilter);
            // 不需要的时候删除广播监听
            // unregisterReceiver(pushReciver);

            // 开启logcat输出，方便debug，发布时请关闭
            XGPushConfig.EnableDebug(this, true);
             XGPushManager.RegisterPush(this);
           // XGPushManager.RegisterPush(this, new XGIOperateCallback() {
            // 
            // public void onSuccess(Object data, int flag) {
            // Log.Debug("TPush", "注册成功，设备token为：" + data);
            // }
            //
            // 
            // public void onFail(Object data, int errCode, String msg) {
            // Log.Debug("TPush", "注册失败，错误码：" + errCode + ",错误信息：" + msg);
            // }
            // });
            //
            // XGPushConfig.getToken(context);
            //
            // XGPushManager.registerPush(this, "UserAccount");
            // XGPushManager.registerPush(this, "UserAccount",
            // new XGIOperateCallback() {
            // 
            // public void onSuccess(Object data, int flag) {
            // Log.Debug("TPush", "注册成功，设备token为：" + data);
            // }
            //
            // 
            // public void onFail(Object data, int errCode, String msg) {
            // Log.Debug("TPush", "注册失败，错误码：" + errCode + ",错误信息：" + msg);
            // }
            // });
            //
            // XGPushManager.registerPush(this, "UserAccount", "ticket", 1, null,
            // new XGIOperateCallback() {
            // 
            // public void onSuccess(Object data, int flag) {
            // Log.Debug("TPush", "注册成功，设备token为：" + data);
            // }
            //
            // 
            // public void onFail(Object data, int errCode, String msg) {
            // Log.Debug("TPush", "注册失败，错误码：" + errCode + ",错误信息：" + msg);
            // }
            // });
            //
            // XGPushManager.unregisterPush(this);
            // 配置accessid和accesskey
            // XGPushConfig.setAccessId(context, 2100001899);
            // XGPushConfig.setAccessKey(context, "AIW4A38Q37JQ");

            // #### 注意 ######
            // XGPushConfig的set接口必须要在startPushService或register之前调用才会及时生效
            // XGPushConfig.getToken()只有在注册成功后才有效

            // 设置通知的自定义样式，即build_id
            //initNotificationBuilder(this);

            FindViewById(Resource.Id.Button_register).Click += delegate
            {
               //XGPushManager.
                XGPushManager.RegisterPush(ApplicationContext);
            };
            FindViewById(Resource.Id.Button_registerAccount).Click += delegate
            {
                Context ctx = this;
                if (ctx != null)
                {
                    LinearLayout layout = new LinearLayout(ctx);
                    layout.Orientation = Orientation.Vertical;
                    EditText textviewGid = new EditText(ctx);
                    textviewGid.Hint = ("请输入需要绑定的账号");
                    layout.AddView(textviewGid);

                    AlertDialog.Builder builder = new AlertDialog.Builder(
                        ctx);
                    builder.SetView(layout);
                    builder.SetPositiveButton("账号注册", delegate
                    {
                        String text = textviewGid.Text
                            .ToString();
                        if (!string.IsNullOrEmpty(text))
                        {
                            // 注册应用（必须调用本接口，否则APP将无法接收到通知和消息）
                            // 使用绑定账号的注册接口（可针对账号下发通知和消息）
                            // 可以重复注册，以最后一次注册为准
                            XGPushManager
                                .RegisterPush(
                                    ApplicationContext,
                                    text);
                        }
                    });
                    builder.Show();
                }
            };
            FindViewById(Resource.Id.Button_unregister).Click += delegate
            {

                // 反注册，调用本接口后，APP将停止接收通知和消息
                XGPushManager.UnregisterPush(context);
            };

            FindViewById(Resource.Id.Button_setTag).Click += delegate
            {
                Context ctx = this;
                if (ctx != null)
                {
                    LinearLayout layout = new LinearLayout(ctx);
                    layout.Orientation = Orientation.Vertical;
                    EditText textviewGid = new EditText(ctx);
                    textviewGid.Hint = ("请输入标签名称");
                    layout.AddView(textviewGid);

                    AlertDialog.Builder builder = new AlertDialog.Builder(
                        ctx);
                    builder.SetView(layout);
                    builder.SetPositiveButton("设置标签", delegate
                    {

                        String text = textviewGid.Text
                            .ToString();
                        if (string.IsNullOrEmpty(text))
                        {
                            XGPushManager
                                .SetTag(this,
                                    text);
                        }
                    });
                    builder.Show();
                }
            };
            FindViewById(Resource.Id.Button_delTag).Click += delegate
            {

                Context ctx = this;
                if (ctx != null)
                {
                    LinearLayout layout = new LinearLayout(ctx);
                    layout.Orientation = Orientation.Vertical;
                    EditText textviewGid = new EditText(ctx);
                    textviewGid.Hint = ("请输入标签名称");
                    layout.AddView(textviewGid);

                    AlertDialog.Builder builder = new AlertDialog.Builder(
                        ctx);
                    builder.SetView(layout);
                    builder.SetPositiveButton("删除标签", delegate
                    {
                        String text = textviewGid.Text
                            .ToString();
                        if (!string.IsNullOrEmpty(text))
                        {
                            XGPushManager
                                .DeleteTag(
                                    this,
                                    text);
                        }
                    });
                    builder.Show();
                }
            }
                ;

            FindViewById(Resource.Id.Button_clearCache).Click += delegate
            {
                Tools.ClearCacheServerItems(ApplicationContext);
                Tools.ClearOptStrategyItem(ApplicationContext);
            };
            FindViewById(Resource.Id.Button_copyToken).Click += delegate
            {
                String token = XGPushConfig.GetToken(this);
                if (!string.IsNullOrEmpty(token))
                {
                    ClipboardManager copy = (ClipboardManager)this
                        .GetSystemService(ClipboardService);
                    copy.Text = (token);
                }
                else
                {
                    Toast.MakeText(this, "请先注册，获取token！", ToastLength.Short).Show();
                }
            };
        }


        /*
        public bool OnCreateOptionsMenu(Menu menu)
        {
            //MenuInflater.Inflate(Resource.main, menu);
            return true;
        }
        */
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main, menu);
            return true;
        }
    }
}