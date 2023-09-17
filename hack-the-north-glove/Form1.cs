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
        public VirtualKeyCode[] inputKeyboard = {VirtualKeyCode.F24, VirtualKeyCode.F24, VirtualKeyCode.F24 , VirtualKeyCode.F24 };
        public string[] inputMouseMovement = { "", "", "", "" };
        public string[] mouseMovementValues = {"MouseLeft", "MouseUp", "MouseDown", "MouseRight"};

        static Timer inputTimer = new Timer();

        SerialPort serialPort;
        string arduinoOutput;
        public Form1()
        {
            InitializeComponent();
            
            //Keyboard dropdowns
            Array itemData = System.Enum.GetValues(typeof(VirtualKeyCode));
            foreach(var data in itemData)
            {
                this.comboBox1.Items.Add(data);
                this.comboBox2.Items.Add(data);
                this.comboBox3.Items.Add(data);
                this.comboBox4.Items.Add(data);
            }
            //Mouse movement dropdowns
            foreach(var data in mouseMovementValues)
            {
                this.comboBox5.Items.Add(data);
                this.comboBox6.Items.Add(data);
                this.comboBox7.Items.Add(data);
                this.comboBox8.Items.Add(data);
            }

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
            inputKeyboard[0] = (VirtualKeyCode)comboBox1.SelectedItem;
            //label1.Text = inputKeyboard1.ToString();
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            inputKeyboard[1] = (VirtualKeyCode)comboBox2.SelectedItem;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputKeyboard[2] = (VirtualKeyCode)comboBox3.SelectedItem;
        }

        private void comboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            inputKeyboard[3] = (VirtualKeyCode)comboBox4.SelectedItem;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputMouseMovement[0] = (string)comboBox5.SelectedItem;
        }
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputMouseMovement[1] = (string)comboBox6.SelectedItem;
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputMouseMovement[2] = (string)comboBox7.SelectedItem;
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputMouseMovement[3] = (string)comboBox8.SelectedItem;
        }
        public void InputManagment()
        {
            inputTimer.Interval = 800;
            inputTimer.Tick += new EventHandler(inputTimerTick);
            inputTimer.Start();
        }

        public void handleKeyboard(VirtualKeyCode vKC)
        {
            InputSimulator simulator = new InputSimulator();
            simulator.Keyboard.KeyDown(vKC);
        }
        
        public void handleMouseMove(string s)
        {
            int adder = 50;
            
            this.Cursor = new Cursor(Cursor.Current.Handle);
            if (s == "MouseLeft")
            {
                Cursor.Position = new Point(Cursor.Position.X - adder, Cursor.Position.Y);
                Cursor.Clip = new Rectangle(this.Location, this.Size);
            }
            else if (s == "MouseUp")
            {
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - adder);
                Cursor.Clip = new Rectangle(this.Location, this.Size);
            }
            else if (s == "MouseDown")
            {
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + adder);
                Cursor.Clip = new Rectangle(this.Location, this.Size);
            }
            else if (s == "MouseRight")
            {
                Cursor.Position = new Point(Cursor.Position.X + adder, Cursor.Position.Y);
                Cursor.Clip = new Rectangle(this.Location, this.Size);
            }
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
    
            if (arduinoOutput.Contains("left"))
            {
                handleKeyboard(inputKeyboard[0]);
                handleMouseMove(inputMouseMovement[0]);
            }
            if (arduinoOutput.Contains("up"))
            {
                handleKeyboard(inputKeyboard[1]);
                handleMouseMove(inputMouseMovement[1]);
            }
            if (arduinoOutput.Contains("down"))
            {
                handleKeyboard(inputKeyboard[2]);
                handleMouseMove(inputMouseMovement[2]);
            }
            if (arduinoOutput.Contains("right"))
            {
                handleKeyboard(inputKeyboard[3]);
                handleMouseMove(inputMouseMovement[3]);
            }

        }
    }
}
