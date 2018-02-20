using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Speech.Synthesis;
using System.Xml.Serialization;
using FFXIV_PopTart.Properties;

namespace FFXIV_PopTart
{
    public partial class PopTart : Form, ILocate
    {
        public static Notify notifyX;

        private CancellationTokenSource _attachcts = new CancellationTokenSource();
        private CancellationTokenSource _memoryTask = new CancellationTokenSource();
        private Task MemoryTask;
        private Task _call;

        private bool _allowClose;
        private bool _allowVisible = true;
        public bool listen = true;
        public bool startup;
        private readonly RegistryKey _rkApp =
            Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        public static bool _notifyOpen;
        private SpeechSynthesizer synth = new SpeechSynthesizer();

        public PoptartSettings PoptartSettings = new PoptartSettings();

        public PopTart()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            _allowVisible = false;
            ShowInTaskbar = false;
            
            MainForm();
            base.OnLoad(e);
        }
        
        private void MainForm()
        {
            //Startup
            FormClosing += OnFormClosing;
            Resize += PopTartForm_Resize;
            Application.ApplicationExit += OnApplicationExit;
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            notify_master.ContextMenuStrip = contextMenuStrip1;

            synth.SetOutputToDefaultAudioDevice();
            synth.Volume = 50;
            synth.Rate = 1;

            LoadSettings();

            mi_popuptoggle.Checked = PoptartSettings.PopupToggle;
            mi_voicetoggle.Checked = PoptartSettings.VoiceToggle;
            mi_roulettetoggle.Checked = PoptartSettings.RouletteToggle;
            
            startup = true;

            //Get API Data
            APIHelper.GetAPIData();
            
            //Attach to FFXIV
            var attach = FFXIVInterface.Attach();

            if (attach)
            {
                _attachcts = new CancellationTokenSource();

                MemoryTask = new Task(() =>
                {
                    _call = CallFfxivMemory(_attachcts.Token);
                }, _memoryTask.Token);

                MemoryTasks.Add(MemoryTask);
                MemoryTasks.Run(MemoryTask);

                notify_master.ShowBalloonTip(2000, @"FFXIV PopTart", @"Ready to Go!", ToolTipIcon.Info);
                
            }
            else
            {
                Console.WriteLine(@"Unable to attach. Are you running DX11?");
            }

            notifyX = new Notify { ShowInTaskbar = false };
            notifyX.ShowDialog();
            notifyX.Left = -16384;

            
        }

        private async Task CallFfxivMemory(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                await Task.Delay(300, ct);

                var processes = Process.GetProcessesByName("ffxiv_dx11");
                if (processes.Length <= 0)
                {
                    _attachcts.Cancel();
                    continue;
                }

                if (!listen) continue;

                FFXIVInterface.Loop();
            }
        }

        public void ProcNotifyInstance(string instance, int roulette)
        {
            if (!_notifyOpen)
            {
                if (PoptartSettings.RouletteToggle && roulette == 0) return;

                notify_master.ShowBalloonTip(2000, @"Duty Finder Popped", instance, ToolTipIcon.Info);

                if (PoptartSettings.VoiceToggle)
                {
                    synth.SpeakAsync(@"Duty Finder has popped with duty " + instance);
                }

                if (PoptartSettings.PopupToggle)
                {
                    notifyX.Invoke((MethodInvoker) delegate
                    {
                        notifyX.UpdateInstanceName(instance);
                        notifyX.Left = 0;
                        notifyX.Activate();
                    });
                    _notifyOpen = true;
                }
            }
        }

        public void CloseNotify()
        {
            if (_notifyOpen && PoptartSettings.PopupToggle)
            {
                notifyX.Invoke((MethodInvoker) delegate
                {
                    notifyX.UpdateInstanceName(@"");
                    notifyX.Left = -16384;
                });
                _notifyOpen = false;
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            if (!_allowVisible)
            {
                value = false;
                if (!IsHandleCreated) CreateHandle();
            }
            base.SetVisibleCore(value);
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_allowClose)
            {
                Hide();
                e.Cancel = true;
                return;
            }

            FinalFormClosing(sender);
            //base.OnFormClosing(e);
        }

        private void FinalFormClosing(object sender)
        {
            _attachcts.Cancel();

            BeginInvoke(new MethodInvoker(Close));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _allowClose = true;
            notify_master.Dispose();

            Application.Exit();
        }

        private void PopTartForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                notify_master.Visible = true;
                _allowVisible = false;
                Hide();
            }
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            //
        }

        private void mi_listenenable_Click(object sender, EventArgs e)
        {
            if (!startup) return;

            listen = mi_listenenable.Checked;
            SaveSettings(0);
        }

        private void mi_winstart_Click(object sender, EventArgs e)
        {
            if (mi_winstart.Checked)
                _rkApp.SetValue("FFXIV PopTart", Application.ExecutablePath);
            else
                _rkApp.DeleteValue("FFXIV PopTart", false);
        }

        private void mi_voicetoggle_Click(object sender, EventArgs e)
        {
            if (!startup) return;

            if (!mi_voicetoggle.Checked)
            {
                PoptartSettings.VoiceToggle = true;
                mi_voicetoggle.Checked = true;
            }
            else
            {
                PoptartSettings.VoiceToggle = false;
                mi_voicetoggle.Checked = false;
            }

            SaveSettings(0);
        }

        private void mi_popuptoggle_Click(object sender, EventArgs e)
        {
            if (!startup) return;

            if (!mi_popuptoggle.Checked)
            {
                PoptartSettings.PopupToggle = true;
                mi_popuptoggle.Checked = true;
            }
            else
            {
                PoptartSettings.PopupToggle = false;
                mi_popuptoggle.Checked = false;
            }

            SaveSettings(0);
        }

        private void mi_roulettetoggle_Click(object sender, EventArgs e)
        {
            if (!startup) return;

            if (!mi_roulettetoggle.Checked)
            {
                PoptartSettings.RouletteToggle = true;
                mi_roulettetoggle.Checked = true;
            }
            else
            {
                PoptartSettings.RouletteToggle = false;
                mi_roulettetoggle.Checked = false;
            }

            SaveSettings(0);
        }

        private void SaveSettings(int report)
        {
            //if (report == 1)
            //    WriteConsole(ConsoleTypes.SYSTEM, "Saving states to settings.chromatics..");
            var popSettings = new PoptartSettings();
            var enviroment = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            var path = enviroment + @"/settings.poptart";

            popSettings = PoptartSettings;

            try
            {
                using (var sw = new StreamWriter(path, false))
                {
                    var x = new XmlSerializer(popSettings.GetType());
                    x.Serialize(sw, popSettings);
                    sw.WriteLine();
                    sw.Close();
                }

                //if (report == 1)
                //    WriteConsole(ConsoleTypes.SYSTEM, "Saved states to settings.chromatics.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Error saving states to settings.poptart. Error: " + ex.Message);
            }
        }

        private void LoadSettings()
        {
            Console.WriteLine(@"Searching for settings.poptart..");
            var enviroment = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            var path = enviroment + @"/settings.poptart";

            if (File.Exists(path))
            {
                //Read Device Save
                Console.WriteLine(@"Attempting to load settings.poptart..");
                using (var sr = new StreamReader(path))
                {
                    try
                    {
                        var reader = new XmlSerializer(PoptartSettings.GetType());
                        var popSettings = (PoptartSettings)reader.Deserialize(sr);
                        sr.Close();

                        PoptartSettings = popSettings;

                        Console.WriteLine(@"settings.poptart loaded.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(@"Error loading settings.poptart. Error: " + ex.Message);
                    }
                }
            }
            else
            {
                //Create Device Save
                Console.WriteLine(@"settings.poptart not found. Creating one..");
                try
                {
                    using (var sw = new StreamWriter(path))
                    {
                        var x = new XmlSerializer(PoptartSettings.GetType());
                        x.Serialize(sw, PoptartSettings);
                        sw.WriteLine();
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Error creating settings.poptart. Error: " + ex.Message);
                }
            }
        }
    }

    public static class ExceptionExtensions
    {
        public static Exception GetOriginalException(this Exception ex)
        {
            if (ex.InnerException == null) return ex;

            return ex.InnerException.GetOriginalException();
        }
    }

    /// <summary>
    ///     Interface used for writing to the Console or debug output in application.
    ///     Additionally accesses GUI functions for LIFX methods.
    /// </summary>
    internal interface ILocate
    {
        void ProcNotifyInstance(string instance, int roulette);
        void CloseNotify();
    }

    
}
