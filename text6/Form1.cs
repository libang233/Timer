using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace text6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DateTime beginTime1;
        DateTime endTime1;
        private string[] ports;
        string nowStates = "arrived";
        bool passedFlag = false;
        bool isReady = true;
        int hahaha = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            ports = SerialPort.GetPortNames();
            serialPort1.PortName = ports[0];
            serialPort1.BaudRate = 9600;
            serialPort1.Open();
            serialPort1.DataReceived += comDataReceived;
        }

        private void comDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = serialPort1.BytesToRead;//记录缓存数量
            byte[] buf = new byte[n];//声明一个临时数组储存当前来的串口数据
            serialPort1.Read(buf, 0, n);//读取缓冲数据
            nowStates = Encoding.ASCII.GetString(buf);
            if (nowStates == "1" && passedFlag == false && isReady == true)
            {
                nowStates = "0";
                isReady = false;
                passedFlag = true;
                beginTime1 = DateTime.Now;
                timer1.Start();
                timer1.Interval = 100;
                hahaha = 0;

            }
            else if (nowStates == "1" && passedFlag == true)
            {
                passedFlag = false;
                nowStates = "0";
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan midTime = DateTime.Now - beginTime1;
            if (passedFlag == false)
            { }
            else
            {

                label5.Text = midTime.Minutes.ToString() + "分" + (midTime.Seconds + hahaha).ToString() + "秒"
                    + midTime.Milliseconds.ToString() + "毫秒";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
            timer1.Dispose();
            passedFlag = false;
            isReady = true;
            label1.Text = "0分0秒0毫秒";
            label5.Text = "0分0秒0毫秒";
            if (timer1.Enabled)
            {
                timer1.Stop();

            }
            else
            {
                timer1.Start();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            passedFlag = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = label5.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            passedFlag = false;
            nowStates = "0";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            hahaha = 2;
            if (hahaha != 0)
            {
                hahaha = hahaha + 1;
            }
        }
    }
}
