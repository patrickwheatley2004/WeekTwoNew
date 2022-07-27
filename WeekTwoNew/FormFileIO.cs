using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeekTwoNew
{
    public partial class FormFileIO : Form
    {
        public FormFileIO()
        {
            InitializeComponent();
        }

        static int max = 12; // row
        static int attributes = 3; // column
        private string[,] ArrayInfo = new string[max, attributes];
        int ptr = 0;

        private void DisplayList()
        {
            lbxOutput.Items.Clear();
            string rec = "";
            for (int x = 0; x < ptr; x++)
            {
                rec = ArrayInfo[x, 0] + " " + ArrayInfo[x, 1];
                lbxOutput.Items.Add(rec);
            }
        }

        private void clearTextBoxes()
        {
            tbxName.Clear();
            tbxPhone.Clear();
            tbxPosition.Clear();
            tbxName.Focus();
        }

        private void FormFileIO_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ptr < max)
            {
                try
                {
                    ArrayInfo[ptr, 0] = tbxName.Text;
                    ArrayInfo[ptr, 1] = tbxPhone.Text;
                    ArrayInfo[ptr, 2] = tbxPosition.Text;
                    ptr++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex);
                }
            }
            else
            {
                MessageBox.Show("The array is full");
            }
            clearTextBoxes();
            DisplayList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var stream = File.Open("employee.bin", FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, false)) // the false means that the system is asking if you want to leave the file open or not. 
                {
                    for (int x = 0; x < ptr; x++)
                    {
                        for (int y = 0; y < attributes; y++)
                        {
                            writer.Write(ArrayInfo[x, y]);
                        }
                    }
                }
            }
            MessageBox.Show("All the datate has been saved!");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (File.Exists("employee.bin"))
            {
                using (var stream = File.Open("employee.bin", FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false)) // the false means that the system is asking if you want to leave the file open or not. 
                    {
                        int x = 0;
                        //Array.Clear(ArrayInfo, 0, max);
                        while(stream.Position < stream.Length)
                        {
                            for (int y = 0; y < attributes; y++)
                            {
                                ArrayInfo[x, y] = reader.ReadString();
                            }
                            x++;
                        }
                        ptr = x;
                    }
                }
                DisplayList();
            }
            else
            {
                MessageBox.Show("The file you tried to open wasn't found.");
            }
        }
    }
}
