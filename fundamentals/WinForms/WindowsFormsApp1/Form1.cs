using dotNetStandardLibraryClass;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClickMe_Click(object sender, EventArgs e)
        {
            lbHelloWorld.Text = $"Hello, {txtName.Text}";
            MessageBox.Show($"Hello, {txtName.Text}!");

            Operator o = new Operator();
            var res = o.ToContat(txtName.Text);
            MessageBox.Show(res);
        }
    }
}
