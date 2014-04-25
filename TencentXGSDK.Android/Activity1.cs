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
            { // �ж��Ƿ������Ÿ�Ĵ򿪷�ʽ
                Toast.MakeText(this, "֪ͨ�����:" + click.ToString(),
                        ToastLength.Short).Show();
                String customContent = click.CustomContent;
                // ��ȡ�Զ���key-value
                if (!string.IsNullOrEmpty(customContent))
                {
                    try
                    {
                        JSONObject json = new JSONObject(customContent);
                        Log.Debug("TPush", "�Զ���key-value:" + json);
                        // ��ȡ�����Զ���key-value
                        // key1Ϊ�·����Զ���key-value
                        String customValue1 = json.GetString("key1");
                        // ��������
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
         * �����Զ�����ʽ���������·�֪ͨʱ����ָ��build_id������ɿ������Լ�ά��
         * 
         * @param context
         */
      


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            context = this.ApplicationContext;
            // ��̬ע���Ÿ�Ļص�
            // CustomPushReceiver pushReciver = new CustomPushReceiver();
            // IntentFilter intentFilter = new IntentFilter();
            // intentFilter.addAction(XGPushConstants.ACTION_FEEDBACK);
            // intentFilter.addAction(XGPushConstants.ACTION_PUSH_MESSAGE);
            // this.getApplicationContext().registerReceiver(pushReciver,
            // intentFilter);
            // ����Ҫ��ʱ��ɾ���㲥����
            // unregisterReceiver(pushReciver);

            // ����logcat���������debug������ʱ��ر�
            XGPushConfig.EnableDebug(this, true);
             XGPushManager.RegisterPush(this);
           // XGPushManager.RegisterPush(this, new XGIOperateCallback() {
            // 
            // public void onSuccess(Object data, int flag) {
            // Log.Debug("TPush", "ע��ɹ����豸tokenΪ��" + data);
            // }
            //
            // 
            // public void onFail(Object data, int errCode, String msg) {
            // Log.Debug("TPush", "ע��ʧ�ܣ������룺" + errCode + ",������Ϣ��" + msg);
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
            // Log.Debug("TPush", "ע��ɹ����豸tokenΪ��" + data);
            // }
            //
            // 
            // public void onFail(Object data, int errCode, String msg) {
            // Log.Debug("TPush", "ע��ʧ�ܣ������룺" + errCode + ",������Ϣ��" + msg);
            // }
            // });
            //
            // XGPushManager.registerPush(this, "UserAccount", "ticket", 1, null,
            // new XGIOperateCallback() {
            // 
            // public void onSuccess(Object data, int flag) {
            // Log.Debug("TPush", "ע��ɹ����豸tokenΪ��" + data);
            // }
            //
            // 
            // public void onFail(Object data, int errCode, String msg) {
            // Log.Debug("TPush", "ע��ʧ�ܣ������룺" + errCode + ",������Ϣ��" + msg);
            // }
            // });
            //
            // XGPushManager.unregisterPush(this);
            // ����accessid��accesskey
            // XGPushConfig.setAccessId(context, 2100001899);
            // XGPushConfig.setAccessKey(context, "AIW4A38Q37JQ");

            // #### ע�� ######
            // XGPushConfig��set�ӿڱ���Ҫ��startPushService��register֮ǰ���òŻἰʱ��Ч
            // XGPushConfig.getToken()ֻ����ע��ɹ������Ч

            // ����֪ͨ���Զ�����ʽ����build_id
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
                    textviewGid.Hint = ("��������Ҫ�󶨵��˺�");
                    layout.AddView(textviewGid);

                    AlertDialog.Builder builder = new AlertDialog.Builder(
                        ctx);
                    builder.SetView(layout);
                    builder.SetPositiveButton("�˺�ע��", delegate
                    {
                        String text = textviewGid.Text
                            .ToString();
                        if (!string.IsNullOrEmpty(text))
                        {
                            // ע��Ӧ�ã�������ñ��ӿڣ�����APP���޷����յ�֪ͨ����Ϣ��
                            // ʹ�ð��˺ŵ�ע��ӿڣ�������˺��·�֪ͨ����Ϣ��
                            // �����ظ�ע�ᣬ�����һ��ע��Ϊ׼
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

                // ��ע�ᣬ���ñ��ӿں�APP��ֹͣ����֪ͨ����Ϣ
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
                    textviewGid.Hint = ("�������ǩ����");
                    layout.AddView(textviewGid);

                    AlertDialog.Builder builder = new AlertDialog.Builder(
                        ctx);
                    builder.SetView(layout);
                    builder.SetPositiveButton("���ñ�ǩ", delegate
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
                    textviewGid.Hint = ("�������ǩ����");
                    layout.AddView(textviewGid);

                    AlertDialog.Builder builder = new AlertDialog.Builder(
                        ctx);
                    builder.SetView(layout);
                    builder.SetPositiveButton("ɾ����ǩ", delegate
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
                    Toast.MakeText(this, "����ע�ᣬ��ȡtoken��", ToastLength.Short).Show();
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