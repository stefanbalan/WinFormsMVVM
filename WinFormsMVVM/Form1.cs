﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using WinFormsMVVM.MVVM;


namespace WinFormsMVVM
{
    public partial class Form1 : Form
    {
        private readonly BindingManager<Form1ViewModel> _bindingManager = new BindingManager<Form1ViewModel>();

        public Form1()
        {
            InitializeComponent();

            _bindingManager
                .Bind(vm => vm.GetItems, btnNonBlocking)
                .Bind(vm => vm.FinishedGettingItems, v => { chkFinished.Checked = v; })
                .Bind(vm => vm.Items, v =>
                {
                    //cmbTest.DataSource = null;
                    //cmbTest.Items.Clear();
                    cmbTest.DataSource = v;
                });
        }

        private void BtnBlocking_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < 15; i++)
            {
                cmbTest.Items.Add($"Item {i}");
                Work.LongRunning();
            }
        }
    }

}
