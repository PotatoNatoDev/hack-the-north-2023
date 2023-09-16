using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;
using WindowsInput;
using System.IO.Ports;

namespace hack_the_north_glove
{
    public partial class Form1 : Form
    {
        public VirtualKeyCode inputKeyboard1;
        public VirtualKeyCode inputKeyboard2;
        public VirtualKeyCode inputKeyboard3;
        public VirtualKeyCode inputKeyboard4;

        public VirtualKeyCode[] inputKeyboard = {VirtualKeyCode.F24, VirtualKeyCode.F24, VirtualKeyCode.F24 , VirtualKeyCode.F24 };

        static Timer inputTimer = new Timer();

        SerialPort serialPort;
        string arduinoOutput;
        public Form1()
        {
            InitializeComponent();
            Array itemData = System.Enum.GetValues(typeof(VirtualKeyCode));
            foreach(var data in itemData)
            {
                this.comboBox1.Items.Add(data);
                this.comboBox2.Items.Add(data);
                this.comboBox3.Items.Add(data);
                this.comboBox4.Items.Add(data);
            }
            //InputController.InputManagment();

            serialPort  = new SerialPort("COM4", 9600);

            try
            {
                serialPort.Open();
            }
            catch 
            {
                Console.WriteLine("COM port failed to open");
            }
            InputManagment();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputKeyboard1 = (VirtualKeyCode)comboBox1.SelectedItem;
            inputKeyboard[0] = (VirtualKeyCode)comboBox1.SelectedItem;
            //label1.Text = inputKeyboard1.ToString();
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            inputKeyboard2 = (VirtualKeyCode)comboBox2.SelectedItem;
            inputKeyboard[1] = (VirtualKeyCode)comboBox2.SelectedItem;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputKeyboard3 = (VirtualKeyCode)comboBox3.SelectedItem;
            inputKeyboard[2] = (VirtualKeyCode)comboBox3.SelectedItem;
        }

        private void comboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            inputKeyboard4 = (VirtualKeyCode)comboBox4.SelectedItem;
            inputKeyboard[3] = (VirtualKeyCode)comboBox4.SelectedItem;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
          
        }
        public void InputManagment()
        {
            inputTimer.Interval = 1000;
            inputTimer.Tick += new EventHandler(inputTimerTick);
            inputTimer.Start();
        }

        private void arduinoTimerTick(object Sender, EventArgs e)
        {
            arduinoOutput = serialPort.ReadLine();
            label2.Text = arduinoOutput;
        }
        private void inputTimerTick(object Sender, EventArgs e)
        {
            arduinoOutput = serialPort.ReadLine();
            label2.Text = arduinoOutput;

            // Set the caption to the current time.              

            if (arduinoOutput.Contains("right"))
            {
                InputSimulator simulator = new InputSimulator();
                simulator.Keyboard.KeyDown(inputKeyboard[3]);
            }
            if (arduinoOutput.Contains("up"))
            {
                InputSimulator simulator = new InputSimulator();
                simulator.Keyboard.KeyDown(inputKeyboard[1]);
            }
            if (arduinoOutput.Contains("down"))
            {
                InputSimulator simulator = new InputSimulator();
                simulator.Keyboard.KeyDown(inputKeyboard[2]);
            }
            if (arduinoOutput.Contains("left"))
            {
                InputSimulator simulator = new InputSimulator();
                simulator.Keyboard.KeyDown(inputKeyboard[0]);
            }
            
            
            

        }

        
    }
}
