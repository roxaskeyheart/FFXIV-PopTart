using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Sharlayan;
using Sharlayan.Models;
using GalaSoft.MvvmLight.Ioc;

namespace FFXIV_PopTart
{
    class FFXIVInterface
    {
        private static readonly object RefreshLock = new object();
        private static readonly object CacheLock = new object();
        public static DateTime LastUpdated = DateTime.MinValue;
        private static readonly TimeSpan UpdateInterval = TimeSpan.FromSeconds(0.05);
        private static bool _siginit;
        private static bool _memoryready;
        private static List<Signature> _sList;
        private static bool _initialized;
        private static int _InstanceID;
        private static int _ContentRoulette;
        private static int _lastID;

        private static readonly ILocate Locate = SimpleIoc.Default.GetInstance<ILocate>();

        public static bool Attach()
        {
            var processes = Process.GetProcessesByName("ffxiv_dx11");
            if (processes.Length <= 0) return false;

            // supported: English, Chinese, Japanese, French, German, Korean
            string gameLanguage = "English";
            // whether to always hit API on start to get the latest sigs based on patchVersion, or use the local json cache (if the file doesn't exist, API will be hit)
            bool useLocalCache = true;
            // patchVersion of game, or latest
            string patchVersion = "latest";
            var process = processes[0];
            var processModel = new ProcessModel
            {
                Process = process,
                IsWin64 = true
            };
            MemoryHandler.Instance.SetProcess(processModel, gameLanguage, patchVersion, useLocalCache);

            return true;
        }

        public static void Loop()
        {
            if (Reader.CanGetPlayerInfo())
            {
                RefreshData();

                if (_lastID != InstanceID())
                {
                    Locate.CloseNotify();
                    if (InstanceID() > 0 || (ContentRoulette() < 0 || ContentRoulette() < 10) && ContentRoulette() == 15)
                    {
                        var instanceName = APIHelper.GetInstanceName(InstanceID());

                        if (instanceName != "invalid")
                        {
                            //Console.WriteLine(ContentRoulette());
                            Locate.ProcNotifyInstance(instanceName, ContentRoulette());
                        }
                        else
                        {
                            Console.WriteLine(@"Unable to locate instance name. (ID: " + InstanceID() + @")");
                        }
                    }

                    _lastID = InstanceID();
                }
            }
        }

        private static void RefreshData()
        {
            lock (RefreshLock)
            {
                if (!_memoryready)
                    if (!Scanner.Instance.Locations.ContainsKey("INSTANCE") || !_siginit)
                    {
                        _sList = new List<Signature>
                        {
                            new Signature
                            {
                                Key = "INSTANCE",
                                PointerPath = new List<long>
                                {
                                    0x018ECB30,
                                    0x24
                                }
                            }
                        };


                        Scanner.Instance.LoadOffsets(_sList);

                        Thread.Sleep(100);

                        if (Scanner.Instance.Locations.ContainsKey("INSTANCE"))
                        {
                            Console.WriteLine(@"Initializing INSTANCE done: " +
                                            Scanner.Instance.Locations["INSTANCE"].GetAddress().ToInt64()
                                                .ToString("X"));

                            _siginit = true;
                        }

                        if (_siginit)
                            _memoryready = true;
                    }

                if (_memoryready)
                {
                    if (Scanner.Instance.Locations.ContainsKey("INSTANCE"))
                    {
                        var address = Scanner.Instance.Locations["INSTANCE"];

                        //PluginController.debug(" " + address.ToString("X8"));
                        _InstanceID = MemoryHandler.Instance.GetInt32(address.GetAddress(), 0);
                        _ContentRoulette = MemoryHandler.Instance.GetByte(address.GetAddress(), 118);

                        _initialized = true;
                        //Debug.WriteLine(isPopped + "/" + _countdown);
                    }


                    LastUpdated = DateTime.Now;
                }
            }
        }

        public static void CheckCache()
        {
            lock (CacheLock)
            {
                if (LastUpdated + UpdateInterval <= DateTime.Now)
                    RefreshData();
            }
        }

        public static int InstanceID()
        {
            if (!_initialized)
                return 0;

            CheckCache();

            return _InstanceID;
        }

        public static int ContentRoulette()
        {
            if (!_initialized)
                return 0;

            CheckCache();

            return _ContentRoulette;
        }
    }
}
