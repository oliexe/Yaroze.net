using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace psxserial_gui
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectexe.ShowDialog();



        }

        private void probe_Tick(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
        }


        public void UpdateStatus(string stats)
        {
            if (!InvokeRequired)
            {
                stat.Text = stats;
            }
            else
            {
                Invoke(new Action<string>(UpdateStatus), stats);
            }
        }


        public void SetMax(int max)
        {
            if (!InvokeRequired)
            {
                progressBar1.Maximum = max;
            }
            else
            {
                Invoke(new Action<int>(SetMax), max);
            }
        }

        public void SetCurrent(int current)
        {
            if (!InvokeRequired)
            {
                progressBar1.Value = current;
            }
            else
            {
                Invoke(new Action<int>(SetCurrent), current);
            }
        }

        public void SendExe(string path, string com)
        {
            string text = path;
            byte[] array = null;

            SerialPort serialPort = new SerialPort(com, 115200, 0, 8);
            serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1");

            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500;
            serialPort.RtsEnable = true;
            try
            {

                array = File.ReadAllBytes(text);
                serialPort.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            if (array.Length % 2048 > 0)
            {
                UpdateStatus("Not a valid Playstation executable !");
            }
            else
            {
                int num = array.Length / 2048;
                Thread.Sleep(500);
                bool flag = false;
                while (!flag)
                {
                    serialPort.Write(new byte[]
                    {
                        99
                    }, 0, 1);
                    Thread.Sleep(2048);
                    if (serialPort.BytesToRead != 0)
                    {
                        int num2 = serialPort.ReadByte();
                        if (num2 != 152)
                        {
                            UpdateStatus("Sucess");
                            flag = true;
                        }
                    }
                }

                serialPort.Write(array, 0, 2048);
                serialPort.Write(array, 16, 4);
                serialPort.Write(array, 24, 4);
                serialPort.Write(array, 28, 4);

                SetMax(num - 1);
                for (int i = 1; i < num; i++)
                {
                    serialPort.Write(array, 2048 * i, 2048);
                    UpdateStatus("Sending packet " + i + " of " + (num-1));
                    SetCurrent(i);
                }
                UpdateStatus("Executing the PS-EXE");
                for (int i = 0; i < 2048; i++)
                {
                    serialPort.Write(new byte[]
                    {
                        255
                    }, 0, 1);
                }
                while (serialPort.BytesToWrite > 0)
                {
                    Thread.Sleep(100);
                }
                serialPort.Close();
                UpdateStatus("Sucess");
            }
            SetCurrent(0);
            SetMax(100);
        }

        private void selectexe_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = selectexe.FileName;            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string com = comboBox1.Text;
            new Thread(() =>
            {
                SendExe(textBox1.Text, com);
            }).Start();
        }
    }
}
