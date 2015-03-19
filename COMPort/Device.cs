using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace COMPort
{
    class DevicePollingException : Exception
    {
        private string _message = "no error message";

        public override string Message
        {
            get { return _message; }
        }

        public DevicePollingException(string message)
        {
            _message = message;
        }
    }

    public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);

    /// <summary>
    /// Класс, содержащий данные для события OnlineChangedEvent.
    /// </summary>
    public class ProgressChangedEventArgs : EventArgs
    {
        private readonly int _id;
        private readonly string _message;

        public ProgressChangedEventArgs(int id, string message)
        {
            this._id = id;
            this._message = message;
        }

        public string ID
        {
            get { return _id.ToString(); }
        }

        public string Message
        {
            get { return _message; }
        }
    }

    class Device
    {
        //объявление делегата события (на него извне осуществляется подписка)
        public event ProgressChangedEventHandler ProgressChanged;

        //вызывает событие через делегат
        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChangedEventHandler handler = ProgressChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        //порт, по которому происходит опрос устройства
        private SerialPort _sPort { get; set; }
        
        //массив параметров устройства
        public Dictionary<string, float[]> Values { get; private set; }
        //ИД устройства в сети Modbus
        public int ID { get; private set; }
        //номер в очередности устройств (запись в БД идет по этому номер в AI->SET + /Sequence/)
        public int Sequence { get; private set; }
        private bool[] _get { get; set; }

        public Device(int id,int seq, SerialPort port, bool[] getParams)
        {
            ID = id;
            Sequence = seq;
            _sPort = port;
            _get = getParams;
            Values = new Dictionary<string, float[]>
            {
                {"Online", new[] {0.00f}}, //когда отвечает пишем значение 1.00f
                {"Kc", new[] {2.00f}}, //для нашего типа счетчика = 2
                {"Ci", new[] {1.00f}}, //для нашего типа счетчика = 1
                {"Kn", new[] {0.00f}},
                {"Kt", new[] {0.00f}},
                {"P", new[] {0.00f, 0.00f, 0.00f, 0.00f}},
                {"Q", new[] {0.00f, 0.00f, 0.00f, 0.00f}},
                {"S", new[] {0.00f, 0.00f, 0.00f, 0.00f}},
                {"CosPh", new[] {0.00f, 0.00f, 0.00f, 0.00f}},
                {"I", new[] {0.00f, 0.00f, 0.00f, 0.00f}},
                {"Uf", new[] {0.00f, 0.00f, 0.00f, 0.00f}},
                {"Kuf", new[] {0.00f, 0.00f, 0.00f, 0.00f}},
                {"Umf", new[] {0.00f, 0.00f, 0.00f, 0.00f}},
                {"Kumf", new[] {0.00f, 0.00f, 0.00f, 0.00f}},
                {"U1_1", new[] {0.00f}},
                {"K0u", new[] {0.00f}},
                {"K2u", new[] {0.00f}},
                {"F", new[] {0.00f}},
                {"T", new[] {0.00f}}
            };
        }

        /// <summary>
        /// Опрашивает устройство
        /// </summary>
        public void Poll()
        {
            //открываем порт
            try { _sPort.Open(); }
            catch (Exception ex)
            { OnProgressChanged(new ProgressChangedEventArgs(ID, "Невозможно открыть порт: " + ex.Message)); }

            if (_sPort.IsOpen)
            {
                try
                {
                    //обязательно добавляем в конец каждого сообщения 2 байта CRC в виде 0xFFFF
                    //далее вызываем метод AddCRC(ref bytes), который заменяет их на реальные значения контрольной суммы

                    //запрос на открытие канала
                    byte[] data1Bytes = new byte[] { (byte)ID, 0x01, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0xFF, 0xFF };
                    AddCRC(ref data1Bytes);
                   _sPort.Write(data1Bytes, 0, data1Bytes.Length);
                    byte[] response1 = GetResponse(4);
                    OnProgressChanged(new ProgressChangedEventArgs(ID,
                        "Send -        " + HexToStr(data1Bytes) +
                        Environment.NewLine +
                        "Recieved : " + HexToStr(response1) +
                        Environment.NewLine));

                    //запрос коэффициентов трансформации
                    byte[] data2Bytes = new byte[] { (byte)ID, 0x08, 0x02, 0xFF, 0xFF };
                    AddCRC(ref data2Bytes);
                    _sPort.Write(data2Bytes, 0, data2Bytes.Length);
                    byte[] response2 = GetResponse(13);
                    byte[] number = new byte[2];

                    number[0] = response2[2];
                    number[1] = response2[1];
                    Values["Kn"] = new[] { float.Parse(BitConverter.ToUInt16(number, 0).ToString()) };

                    number[0] = response2[4];
                    number[1] = response2[3];
                    Values["Kt"] = new[] { float.Parse(BitConverter.ToUInt16(number, 0).ToString()) };

                    //переписываем коэффициенты в переменные
                    //для удобства
                    float Kc = Values["Kc"][0];
                    float Kn = Values["Kn"][0];
                    float Kt = Values["Kt"][0];
                    float Ci = Values["Ci"][0];
                    float Ksum = Kc * Kn * Kt;

                    //P,Q,S = N*Kn*Kt*Kc
                    if (_get[0])
                    {
                        //запрос P (активная мощность)
                        float[] P = Get16DataBytes3(0x00);
                        Values["P"] = new[] {P[0]*Ksum, P[1]*Ksum, P[2]*Ksum, P[3]*Ksum};
                    }
                    if (_get[1])
                    {
                        //запрос Q (реактивная мощность)
                        float[] Q = Get16DataBytes3(0x04);
                        Values["Q"] = new[] {Q[0]*Ksum, Q[1]*Ksum, Q[2]*Ksum, Q[3]*Ksum};
                    }
                    if (_get[2])
                    {
                        //запрос S (полная мощность)
                        float[] S = Get16DataBytes3(0x08);
                        Values["S"] = new[] {S[0]*Ksum, S[1]*Ksum, S[2]*Ksum, S[3]*Ksum};
                    }
                    if (_get[3])
                    {
                        //запрос CosPh (коэффициент мощности)
                        float[] CosPh = Get16DataBytes2(0x30);
                        Values["CosPh"] = new[] {CosPh[0], CosPh[1], CosPh[2], CosPh[3]};
                    }
                    if (_get[4])
                    {
                        //I = (N*10)*Kt*Ci
                        //запрос I (ток)
                        float[] I = Get16DataBytes2(0x20);
                        Values["I"] = new[] {I[0]*10*Kt*Ci, I[1]*10*Kt*Ci, I[2]*10*Kt*Ci, I[3]*10*Kt*Ci};
                    }
                    if (_get[5])
                    {
                        //U=N*Kn
                        //запрос Uф (фазное напряжение)
                        float[] Uf = Get16DataBytes2(0x10);
                        Values["Uf"] = new[] {Uf[0]*Kn, Uf[1]*Kn, Uf[2]*Kn, Uf[3]*Kn};
                    }
                    if (_get[6])
                    {
                        //запрос Kuf 
                        float[] Kuf = Get16DataBytes2(0x80);
                        Values["Kuf"] = new[] { Kuf[0], Kuf[1], Kuf[2], Kuf[3] };
                    }
                    if (_get[7])
                    {
                        //запрос Uмф (межфазное напряжение\линейное)
                        float[] Umf = Get16DataBytes2(0x14);
                        Values["Umf"] = new[] {Umf[0]*Kn, Umf[1]*Kn, Umf[2]*Kn, Umf[3]*Kn};
                    }
                    if (_get[8])
                    {
                        //запрос Kumf 
                        float[] Kumf = Get16DataBytes2(0x84);
                        Values["Kumf"] = new[] {Kumf[0], Kumf[1], Kumf[2], Kumf[3]};
                    }
                    if (_get[9])
                    {
                        //запрос U1_1 
                        float[] U1_1 = Get11Data(0x19);
                        Values["U1_1"] = new[] {U1_1[0]*Kn};
                    }
                    if (_get[10])
                    {
                        //запрос K0u 
                        float[] K0u = Get11Data(0x8D);
                        Values["K0u"] = new[] {K0u[0]};
                    }
                    if (_get[11])
                    {
                        //запрос K2u 
                        float[] K2u = Get11Data(0x89);
                        Values["K2u"] = new[] {K2u[0]};
                    }
                    if (_get[12])
                    {
                        //запрос F (частота) 
                        float[] F = Get16DataBytes2(0x40);
                        Values["F"] = new[] {F[0], F[1], F[2], F[3]};
                    }
                    if (_get[13])
                    {
                        //запрос T (температура) 
                        float[] T = GetTemp();
                        Values["T"] = new[] {T[0]};
                    }

                    //если опрос прошел успешно, значит СЭТ онлайн
                    Values["Online"][0] = 1.00f;

                    //закрываем порт
                    _sPort.Close();

                    OnProgressChanged(new ProgressChangedEventArgs(ID, "Опрос окончен " + Environment.NewLine +
                        "________________________" +Environment.NewLine));
                }
                catch (Exception ex)
                {
                    OnProgressChanged(new ProgressChangedEventArgs(ID, "Poll Error: " + ex.Message + Environment.NewLine));
                    //обнуляем значения
                    Values["Online"] = new[] { 0.00f };
                    Values["Kn"] = new[] { 0.00f };
                    Values["Kt"] = new[] { 0.00f };
                    Values["P"] = new[] { 0.00f, 0.00f, 0.00f, 0.00f };
                    Values["Q"] = new[] { 0.00f, 0.00f, 0.00f, 0.00f };
                    Values["S"] = new[] { 0.00f, 0.00f, 0.00f, 0.00f };
                    Values["CosPh"] = new[] { 0.00f, 0.00f, 0.00f, 0.00f };
                    Values["I"] = new[] { 0.00f, 0.00f, 0.00f, 0.00f };
                    Values["Uf"] = new[] { 0.00f, 0.00f, 0.00f, 0.00f };
                    Values["Kuf"] = new[] { 0.00f, 0.00f, 0.00f, 0.00f };
                    Values["Umf"] = new[] { 0.00f, 0.00f, 0.00f, 0.00f };
                    Values["Kumf"] = new[] { 0.00f, 0.00f, 0.00f, 0.00f };
                    Values["U1_1"] = new[] { 0.00f };
                    Values["K0u"] = new[] { 0.00f };
                    Values["K2u"] = new[] { 0.00f };
                    Values["F"] = new[] { 0.00f };
                    Values["T"] = new[] { 0.00f };
                
                    //закрываем порт
                    _sPort.Close();
                    return;
                }
            }
        }

        /// <summary>
        /// Записывает параметры в базу SQL Server
        /// </summary>
        public void WriteValuesToSQL()
        {
            SQL.UpdateUDMItems(Sequence, Values);
        }

        /// <summary>
        /// Запрашивает температуру
        /// </summary>
        /// <returns>массив float[1] (целое число)</returns>
        private float[] GetTemp()
        {
            byte[] dataBytes = new byte[] { (byte)ID, 0x08, 0x01, 0xFF, 0xFF };
            AddCRC(ref dataBytes);
            _sPort.Write(dataBytes, 0, dataBytes.Length);
            byte[] response = GetResponse(5);
            OnProgressChanged(new ProgressChangedEventArgs(ID,
                "Send -        " + HexToStr(dataBytes) +
                Environment.NewLine +
                "Recieved : " + HexToStr(response) +
                Environment.NewLine));

            byte[] number = new byte[2];
            number[0] = response[2];
            number[1] = 0x00;
            float Val = float.Parse(BitConverter.ToUInt16(number, 0).ToString());

            return new float[] { Val };
        }

        /// <summary>
        /// Запрашивает данные вида
        /// TX	00 08 11 19 4C 7C 
        ///	          3 .11   
        /// RX	00 00 01 37 40 62 
        /// </summary>
        /// <param name="param">параметр запроса</param>
        /// <returns>массив float[1] (число/100)</returns>
        private float[] Get11Data(byte param)
        {
            byte[] dataBytes = new byte[] { (byte)ID, 0x08, 0x11, param, 0xFF, 0xFF };
            AddCRC(ref dataBytes);
            _sPort.Write(dataBytes, 0, dataBytes.Length);
            byte[] response = GetResponse(6);
            OnProgressChanged(new ProgressChangedEventArgs(ID,
                "Send -        " + HexToStr(dataBytes) +
                Environment.NewLine +
                "Recieved : " + HexToStr(response) +
                Environment.NewLine));

            byte[] number = new byte[2];

            number[0] = response[3];
            number[1] = response[2];
            float Val = float.Parse(BitConverter.ToUInt16(number, 0).ToString()) / 100;

            return new float[] { Val };
        }

        /// <summary>
        /// Запрашивает данные вида
        /// TX	00 08 16 30 8F 92 
        ///	Returns   0 .99    0 .93    0 .93    0 .93  CRC
        /// RX	00 00 00 63 00 00 5D 00 00 5D 40 00 5D AF 1E 
        /// 4 параметра по 2 байта (общий;L1;L2;L3)
        /// </summary>
        /// <param name="param">параметр запроса</param>
        /// <returns>массив float[4] (число/100)</returns>
        private float[] Get16DataBytes2(byte param)
        {
            byte[] dataBytes = new byte[] { (byte)ID, 0x08, 0x16, param, 0xFF, 0xFF };
            AddCRC(ref dataBytes);
            _sPort.Write(dataBytes, 0, dataBytes.Length);
            byte[] response = GetResponse(15);
            OnProgressChanged(new ProgressChangedEventArgs(ID,
                "Send -        " + HexToStr(dataBytes) +
                Environment.NewLine +
                "Recieved : " + HexToStr(response) +
                Environment.NewLine));

            byte[] number = new byte[2];

            number[0] = response[3];
            number[1] = response[2];
            float Val = float.Parse(BitConverter.ToUInt16(number, 0).ToString()) / 100;

            number[0] = response[6];
            number[1] = response[5];
            float ValF1 = float.Parse(BitConverter.ToUInt16(number, 0).ToString()) / 100;

            number[0] = response[9];
            number[1] = response[8];
            float ValF2 = float.Parse(BitConverter.ToUInt16(number, 0).ToString()) / 100;

            number[0] = response[12];
            number[1] = response[11];
            float ValF3 = float.Parse(BitConverter.ToUInt16(number, 0).ToString()) / 100;

            return new float[] { Val, ValF1, ValF2, ValF3 };
        }

        /// <summary>
        /// Запрашивает данные вида
        /// TX	00 08 16 00 8F 86 
        ///	Returns   799690      267133     266646    4460214     CRC
        /// RX	00 | 0C 33 CA |  04 13 7D | 04 11 96 | 44 0E B6 | 0D B1 
        /// 4 параметра по 3 байта (общий;L1;L2;L3)
        /// </summary>
        /// <param name="param">параметр запроса</param>
        /// <returns>массив float[4] (число/1000)</returns>
        private float[] Get16DataBytes3(byte param)
        {
            byte[] dataBytes = new byte[] { (byte)ID, 0x08, 0x16, param, 0xFF, 0xFF };
            AddCRC(ref dataBytes);
            _sPort.Write(dataBytes, 0, dataBytes.Length);
            byte[] response = GetResponse(15);
            OnProgressChanged(new ProgressChangedEventArgs(ID,
                "Send -        " + HexToStr(dataBytes) +
                Environment.NewLine +
                "Recieved : " + HexToStr(response) +
                Environment.NewLine));

            byte[] number = new byte[4];

            number[0] = response[3];
            number[1] = response[2];
            number[2] = response[1];
            number[3] = 0x00;
            float Val = float.Parse(BitConverter.ToUInt32(number, 0).ToString()) / 1000;

            number[0] = response[6];
            number[1] = response[5];
            number[2] = response[4];
            number[3] = 0x00;
            float ValF1 = float.Parse(BitConverter.ToUInt32(number, 0).ToString()) / 1000;

            number[0] = response[9];
            number[1] = response[8];
            number[2] = response[7];
            number[3] = 0x00;
            float ValF2 = float.Parse(BitConverter.ToUInt32(number, 0).ToString()) / 1000;

            number[0] = response[12];
            number[1] = response[11];
            //таким способов избавляемся от старшего бита в 10 байте
            //все время лепят по какой то причине 4ку
            number[2] = (byte)((byte)(response[10] << 4) >> 4);
            number[3] = 0x00;
            float ValF3 = float.Parse(BitConverter.ToUInt32(number, 0).ToString()) / 1000;

            return new float[] { Val, ValF1, ValF2, ValF3 };
        }

        /// <summary>
        /// Возвращает ответ устройства на запрос (0x66 в случае ошибки)
        /// </summary>
        /// <param name="cnt">количество возвращаемых байт необходимо знать заранее</param>
        /// <returns>byte[]</returns>
        private byte[] GetResponse(int cnt)
        {
            //There is a bug in .Net 2.0 DataReceived Event that prevents people from using this
            //event as an interrupt to handle data (it doesn't fire all of the time).  Therefore
            //we have to use the ReadByte command for a fixed length as it's been shown to be reliable.
            byte[] response = new byte[cnt];
            try
            {
                for (int i = 0; i < response.Length; i++)
                {
                    response[i] = (byte)(_sPort.ReadByte());
                }
            }
            //при первом же таймауте передаем исключение наверх, 
            //где завершаем опрос устройства
            catch (TimeoutException)
            {
                Values["Online"][0] = 0.00f;
                if (_sPort.IsOpen)
                    _sPort.Close();
                throw new DevicePollingException("Таймаут запроса.");
            }
            //при любом другом исключении возвращаем 0х00
            catch (Exception ex)
            {
                for (int i = 0; i < cnt; i++)
                {
                    response[i] = 0x00;
                }
            }
            return response;
        }

        /// <summary>
        /// Преобразует byte[] массив в соответствующее строкове представление
        /// </summary>
        /// <param name="hexData">byte[] массив данных</param>
        /// <returns>string</returns>
        private string HexToStr(byte[] hexData)
        {
            StringBuilder str = new StringBuilder();
            foreach (byte b in hexData)
            {
                str.Append(string.Format("{0:X2} ", b));
            }
            return str.ToString();
        }

        /// <summary>
        /// Возвращает byte[] массив с добавлением в конец контрольной суммы
        /// </summary>
        /// <param name="message">byte[] массив</param>
        private void AddCRC(ref byte[] message)
        {
            byte[] CRC = new byte[2];
            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;

            for (int i = 0; i < (message.Length) - 2; i++)
            {
                CRCFull = (ushort)(CRCFull ^ message[i]);

                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
            message[message.Length - 2] = CRC[0];
            message[message.Length - 1] = CRC[1];
        }
    }
}
