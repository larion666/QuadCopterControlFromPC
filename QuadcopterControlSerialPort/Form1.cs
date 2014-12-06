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

namespace QuadcopterControlSerialPort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            serialPort1.Open();
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }
        void SendASCIItoSerialPort(int ASCII)
        {
            byte[] buffer = new byte[] { Convert.ToByte(ASCII) };
            serialPort1.Write(buffer,0,1);
        }
         protected override void OnKeyDown(KeyEventArgs e)
        {
             base.OnKeyDown(e);
             switch(e.KeyCode)
             {
                 case Keys.Down:
                      SendASCIItoSerialPort(40);
                 break;
                 case Keys.Up:
                      SendASCIItoSerialPort(38);
                 break;
                 case Keys.Left:
                      SendASCIItoSerialPort(37);
                 break;
                 case Keys.Right:
                      SendASCIItoSerialPort(39);
                 break;
             }
        }
        void ChangeText ()
        {
            string message = serialPort1.ReadLine();
            SetText(message.ToString());
        }
        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.label1.Text = text;
            }
        }
        private void DataReceivedHandler( object sender, SerialDataReceivedEventArgs e)
        {
            ChangeText();
        }
    }
}
