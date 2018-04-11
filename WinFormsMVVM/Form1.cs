using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;


namespace WinFormsMVVM
{
    public partial class Form1 : Form
    {
        private readonly BindingManager<Form1ViewModel> bindingManager = new BindingManager<Form1ViewModel>();

        public Form1()
        {

            UIContext.Init();

            InitializeComponent();

            bindingManager
                .Bind(vm => vm.GetItems, btnNonBlocking)
                .Bind(vm => vm.FinishedGettingItems, v => { chkFinished.Checked = v; })
                .Bind<Collection<string>>(vm => vm.Items, v =>
                {
                    cmbTest.DataSource = null;
                    cmbTest.Items.Clear();
                    cmbTest.DataSource = v;
                });

            bindingManager.Bind(bindingManager.ViewModel.GetItems, btnNonBlocking);
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
