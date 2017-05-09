using System;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;
using Rainmeter;

namespace WebsocketForRainmeter
{
    //@TODO Write plugin to send out websocket
    //@TODO Write Wallpaper engine to accept websocket
    //@TODO Remove reference to GPMDP because I used that code because I was lazy
    //@TODO Decide if multiple connections is okay or not or just only use one port #

    internal class Measure
    {
        public static WebSocketServer wssv;
        public static string wsMessages = "";
        public static int instanceCount = 0;

        public class MessagePassing : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                wsMessages = e.Data;
            }

            //public void SendMessage(string stringToSend)
            //{
            //    Sessions.Broadcast(stringToSend);
            //}
        }



        internal Measure(Rainmeter.API api)
        {
            if (wssv == null)
            {
                wssv = new WebSocketServer(58932);
                wssv.AddWebSocketService<MessagePassing>("/");
            }

            if (wssv.IsListening == false)
            {
                wssv.Start();
            }
        }

        internal virtual void Dispose()
        {

        }

        internal virtual void Reload(Rainmeter.API api, ref double maxValue)
        {
            //@TODO Use this port
            int port = api.ReadInt("Port", 58932);
        }

        internal void ExecuteBang(string args)
        {
            string bang = args.ToLowerInvariant();
            if (bang.Equals("send"))
            {

            }
            else
            {
                wssv.WebSocketServices.Broadcast(args);
            }
        }

        internal virtual double Update()
        {

            return 0.0;
        }

        internal string GetString()
        {

            return wsMessages;
        }


    }
    public static class Plugin
    {
        static IntPtr StringBuffer = IntPtr.Zero;

        [DllExport]
        public static void Initialize(ref IntPtr data, IntPtr rm)
        {
            data = GCHandle.ToIntPtr(GCHandle.Alloc(new Measure(new Rainmeter.API(rm))));
            Measure.instanceCount++;
        }

        [DllExport]
        public static void Finalize(IntPtr data)
        {
            Measure.instanceCount--;
            if (Measure.instanceCount == 0)
            {
                Measure.wssv.Stop();
                Measure.wssv = null;
            }
            GCHandle.FromIntPtr(data).Free();

            if (StringBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(StringBuffer);
                StringBuffer = IntPtr.Zero;
            }
        }

        [DllExport]
        public static void Reload(IntPtr data, IntPtr rm, ref double maxValue)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            measure.Reload(new Rainmeter.API(rm), ref maxValue);
        }

        [DllExport]
        public static double Update(IntPtr data)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            return measure.Update();
        }

        [DllExport]
        public static IntPtr GetString(IntPtr data)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            if (StringBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(StringBuffer);
                StringBuffer = IntPtr.Zero;
            }

            string stringValue = measure.GetString();
            if (stringValue != null)
            {
                StringBuffer = Marshal.StringToHGlobalUni(stringValue);
            }

            return StringBuffer;
        }
        [DllExport]
        public static void ExecuteBang(IntPtr data, IntPtr args)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            measure.ExecuteBang(Marshal.PtrToStringUni(args));
        }
    }
}
