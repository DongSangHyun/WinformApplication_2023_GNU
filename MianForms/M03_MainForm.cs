using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MianForms
{
    public partial class M03_MainForm : Form
    {
        public M03_MainForm()
        {
            InitializeComponent();
        }
        public M03_MainForm(string sUserName)
        {
            InitializeComponent();
            stsUserName.Text = sUserName;
        }
    }
}
