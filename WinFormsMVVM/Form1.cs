using System;

namespace WinFormsMVVM
{
    public partial class Form1 : ViewBase<Form1ViewModel>
    {
        private CommandManager commanManager;

        public Form1()
        {

            UIContext.Init();

            //ViewModel = new Form1ViewModel();
            //Bind(v => v.FinishedGettingItems, () => chkFinished.Checked);
            //Bind(vm => vm.Finished, v => ((Form1)v).chkFinished.Checked);
            Bind(vm => vm.FinishedGettingItems, v => { chkFinished.Checked = v; });

            InitializeComponent();

            //CommandBindings.Add("1", new ControlBinder());

            commanManager = new CommandManager();
            commanManager.Bind(ViewModel.Toggle, btnNonBlocking);
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
            //ViewModel.Toggle.Execute();
        }
    }

}
