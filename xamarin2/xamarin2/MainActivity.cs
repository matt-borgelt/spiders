using Android.App;
using Android.Widget;
using Android.OS;
using Android.Hardware;
using System.Collections.Generic;
using System.IO;
using Android.Runtime;
using System;
using Android.Content;
using Android.Media;

namespace xamarin2
{
    //public class Acc1 : ISensorEventListener
    //{
    //    public IntPtr Handle => throw new NotImplementedException();

    //        public float x;
    //        public float y;
    //        public float z;


    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
    //    {
    //        throw new NotImplementedException();

    //    }

    //    public void OnSensorChanged(SensorEvent e)
    //    {
    //        x = e.Values[0];
    //        y = e.Values[1];
    //        z = e.Values[2];
    //    }
    //}


    [Activity(Label = "xamarin2", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ISensorEventListener
    {
        static readonly object _syncLock = new object();
        SensorManager _sensorManager;
        TextView _sensorTextView;
        MediaRecorder _mediaRecorder;
        private string sensor_info;

        //static class info
        //{
        //    public static List<ISensorEventListener> l = new List<ISensorEventListener> { new Acc1()};
        //}
        //public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnSensorChanged(SensorEvent e)
        //{
        //    throw new NotImplementedException();
        //}

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            Do();
            TableLayout table = FindViewById<TableLayout>(Resource.Id.table);           
            
            
            //activity.SetContentView(Resource.Layout.ProfileRecordLayout);
            //Button button = FindViewById<Button>(Resource.Id.recordbutton);


            string ret = "";
            _sensorManager = (SensorManager)GetSystemService(Context.SensorService);
            //_sensorTextView = FindViewById<TextView>(Resource.Id.sensors_text);

            var _sensorList = _sensorManager.GetSensorList(SensorType.All);
            //var info.sensor_info = "";
            foreach (var __sensor in _sensorList)
            {
                ret += __sensor.Name.PadRight(__sensor.ToString().Length - 40);
                ret += __sensor.Vendor.PadRight(20);
                //if (__sensor.Type == SensorType.Accelerometer)
                //{
                //    info.sensor_info += x.ToString() + y.ToString() + z.ToString();
                //}
               
                ret += "\n";

                TableRow tr = new TableRow(this);
                TextView tv = new TextView(this);
                tr.TextAlignment = Android.Views.TextAlignment.TextStart;
                
                tv.Text = __sensor.ToString();
                tr.AddView(tv);
                table.AddView(tr);
                //info.l.Add(__sensor);
            }
            //_sensorTextView.Text = ret;
        }
        protected override void OnPause()
        {
            base.OnPause();
            _sensorManager.UnregisterListener(this);
        }
        protected override void OnResume()
        {
            base.OnResume();
            _sensorManager.RegisterListener(this, _sensorManager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Ui);
        }
        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            // We don't want to do anything here.
        }
        public void OnSensorChanged(SensorEvent e)
        {
            //lock (_syncLock)
            //{
            //    string ret = "";
            //    //_sensorTextView.Text = info.sensor_info + string.Format("x={0:f}, y={1:f}, z={2:f}", e.Values[0], e.Values[1], e.Values[2]);
            //    //foreach(var sensor in info.l)
            //    //{
            //    //    //if(sensor.Type==SensorType.Accelerometer)
            //    //    //{

            //    //    //}
            //    //}
            //}
        }


        public void Do()
        {
        }
        //public void RecordAudio(String filePath)
        //{
        //    try
        //    {
        //        if (File.Exists(filePath))
        //        {
        //            File.Delete(filePath);
        //        }
        //        if (_mediaRecorder == null)
        //        {
        //            _mediaRecorder = new MediaRecorder(); // Initial state.
        //        }
        //        else
        //        {
        //            _mediaRecorder.Reset();
        //            _mediaRecorder.SetAudioSource(AudioSource.Mic);
        //            _mediaRecorder.SetOutputFormat(OutputFormat.ThreeGpp);
        //            _mediaRecorder.SetAudioEncoder(AudioEncoder.AmrNb);
        //            // Initialized state.
        //            _mediaRecorder.SetOutputFile(filePath);
        //            // DataSourceConfigured state.
        //            _mediaRecorder.Prepare(); // Prepared state
        //            _mediaRecorder.Start(); // Recording state.
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine(ex.StackTrace);
        //    }
        //}
    }

}

