using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WinFormsMVVM
{
    public partial class Form1 : ViewBase
    {
        private WindowsFormsSynchronizationContext _uiContext = new WindowsFormsSynchronizationContext();
        public Form1()
        {
            InitializeComponent();
            UIContext.Init();
        }


        private void BtnBlocking_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < 15; i++)
            {
                cmbTest.Items.Add($"Item {i}");
                Work.LongRunning();
            }
        }

        private void BtnNonBlocking_Click(object sender, EventArgs e)
        {
           
        }
    }

}
