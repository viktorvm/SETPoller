using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using COMPort.Properties;

namespace COMPort
{
    public partial class Form1 : Form
    {
        //Опрашиваемые параметры
        public bool[] getParams
        {
            get
            {
                return new bool[]
                {
                    PCBox.Checked, QCBox.Checked, SCBox.Checked, CosPhCBox.Checked,
                    ICBox.Checked, UfCBox.Checked, KufCBox.Checked, UmfCBox.Checked, KumfCBox.Checked,
                    U1_1CBox.Checked, K0uCBox.Checked, K2uCBox.Checked, FCBox.Checked, TCBox.Checked
                };
            }
        }

        //таким способом останавливаем опрос (не работает Thread.Abort:( )
        private bool _stopPolling = false;
        //период опроса
        private int _pollPeriod { get; set; }
        //имя порта (e.g."COM2")
        private string portName
        {
            get { return portNameTBox.Text; }
        }
        //скорость передачи данных (e.g.9600)
        private int baudRate
        {
            get
            {
                int rate;
                try { rate = Convert.ToInt32(baudRateTBox.Text); }
                catch { rate = 0; }
                return rate;
            }
        }
        //таймаут чтения и записи
        private int timeOut
        {
            get
            {
                int time;
                try { time = Convert.ToInt32(timeOutTBox.Text); }
                catch { time = 0; }
                return time;
            }
        }

        //поток, в котором выполняется опрос
        private Thread pollThread;
        //собственно порт
        private static SerialPort sPort;

        public Form1()
        {
            InitializeComponent();
            LoadSettings();
            SQL.ServName = ServNameTBox.Text;
            SQL.DBName = DBNameTBox.Text;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Start();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            _stopPolling = true;

            stopBtn.Enabled = false;
            startBtn.Enabled = true;
        }

        private void StartPolling(object devices)
        {
            while (!_stopPolling)
            {
                foreach (Device d in (Device[])devices)
                {
                    try
                    {
                        if (_stopPolling)
                            continue;
                        d.Poll();
                        d.WriteValuesToSQL();
                    }
                    catch (Exception ex)
                    {
                        trafficTBox.Invoke(new EventHandler(delegate { trafficTBox.Text += ex.Message + Environment.NewLine; }));
                    }
                }
                Thread.Sleep(_pollPeriod);
            }
        }

        private void trafficTBox_TextChanged(object sender, EventArgs e)
        {
            trafficTBox.Select(trafficTBox.TextLength, 0);
            trafficTBox.ScrollToCaret();
            //защита от переполнения TextBox
            if (trafficTBox.Text.Length > 10000)
                trafficTBox.Text = string.Empty;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _stopPolling = true;
            SaveSettings();
        }

        private void SaveSettings()
        {
            Settings.Default.PortName = portNameTBox.Text;
            Settings.Default.BaudRate = baudRateTBox.Text;
            Settings.Default.TimeOut = timeOutTBox.Text;
            Settings.Default.IDs = devIDsTBox.Text;
            Settings.Default.PollPeriod = pollPeriodTBox.Text;
            Settings.Default.ConnectionString = ServNameTBox.Text;
            Settings.Default.DBName = DBNameTBox.Text;
            Settings.Default.GetP = PCBox.Checked;
            Settings.Default.GetQ = QCBox.Checked;
            Settings.Default.GetS = SCBox.Checked;
            Settings.Default.GetCosPh = CosPhCBox.Checked;
            Settings.Default.GetI = ICBox.Checked;
            Settings.Default.GetUf = UfCBox.Checked;
            Settings.Default.GetKuf = KufCBox.Checked;
            Settings.Default.GetUmf = UmfCBox.Checked;
            Settings.Default.GetKumf = KumfCBox.Checked;
            Settings.Default.GetU1_1 = U1_1CBox.Checked;
            Settings.Default.GetK0u = K0uCBox.Checked;
            Settings.Default.GetK2u = K2uCBox.Checked;
            Settings.Default.GetF = FCBox.Checked;
            Settings.Default.GetT = TCBox.Checked;
            Settings.Default.Save();
        }

        private void LoadSettings()
        {
            portNameTBox.Text = Settings.Default.PortName;
            baudRateTBox.Text = Settings.Default.BaudRate;
            timeOutTBox.Text = Settings.Default.TimeOut;
            devIDsTBox.Text = Settings.Default.IDs;
            pollPeriodTBox.Text = Settings.Default.PollPeriod;
            ServNameTBox.Text = Settings.Default.ConnectionString;
            DBNameTBox.Text = Settings.Default.DBName;
            PCBox.Checked = Settings.Default.GetP;
            QCBox.Checked = Settings.Default.GetQ;
            SCBox.Checked = Settings.Default.GetS;
            CosPhCBox.Checked = Settings.Default.GetCosPh;
            ICBox.Checked = Settings.Default.GetI;
            UfCBox.Checked = Settings.Default.GetUf;
            KufCBox.Checked = Settings.Default.GetKuf;
            UmfCBox.Checked = Settings.Default.GetUmf;
            KumfCBox.Checked = Settings.Default.GetKumf;
            U1_1CBox.Checked = Settings.Default.GetU1_1;
            K0uCBox.Checked = Settings.Default.GetK0u;
            K2uCBox.Checked = Settings.Default.GetK2u;
            FCBox.Checked = Settings.Default.GetF;
            TCBox.Checked = Settings.Default.GetT;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Start();
            WindowState = FormWindowState.Minimized;
        }

        private void Start()
        {
            if (string.IsNullOrEmpty(devIDsTBox.Text))
            {
                MessageBox.Show("Введите ID счетчиков СЭТ через запятую", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            SQL.ServName = ServNameTBox.Text;
            SQL.DBName = DBNameTBox.Text;
            _pollPeriod = Int32.Parse(pollPeriodTBox.Text);

            startBtn.Enabled = false;
            stopBtn.Enabled = true;
            _stopPolling = false;

            //инициализация объекта порта
            sPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One)
            {
                Handshake = Handshake.None,
                ReadTimeout = timeOut,
                WriteTimeout = timeOut
            };

            //приступаем к опросу
            string[] ids = devIDsTBox.Text.Split(',');
            Device[] devices = new Device[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                if (String.IsNullOrEmpty(ids[i].Trim()))
                    continue;
                Device d = new Device(Int16.Parse(ids[i].Trim()), i+1, sPort, getParams);
                devices[i] = d;
                d.ProgressChanged += (s, args) => trafficTBox.Invoke(new EventHandler(delegate
                {
                    trafficTBox.Text += "Устройство № " + args.ID + ":" + Environment.NewLine
                                        + args.Message + Environment.NewLine;
                }));
            }

            pollThread = new Thread(StartPolling);
            pollThread.Start(devices);
        }
    }
}
