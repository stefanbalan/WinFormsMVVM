using System;
using System.Windows.Forms;
using WinFormsMVVM.MVVM;

namespace WinFormsMVVM
{
    public partial class Form1 : Form //: ViewBase<Form1ViewModel>
    {
        public Form1()
        {

            UIContext.Init();

            InitializeComponent();

            //ViewModel = new Form1ViewModel();
            //Bind(v => v.FinishedGettingItems, () => chkFinished.Checked);
            // Bind(vm => vm.Finished, v => ((Form1)v).chkFinished.Checked);
            propertyBindingManager = new PropertyBinder<Form1ViewModel>();
            propertyBindingManager.Bind(vm => vm.FinishedGettingItems, v => { chkFinished.Checked = v; });


            //CommandBindings.Add("1", new ControlBinder());

            //_commanBindingManager = new CommandBindingManager();
            commanBindingManager.Bind(propertyBindingManager.ViewModel.Toggle, btnNonBlocking);
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
