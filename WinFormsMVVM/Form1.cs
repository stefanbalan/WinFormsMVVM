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
            propertyBindingManager.Bind(vm => vm.Items, v =>
            {
                cmbTest.DataSource = null; cmbTest.Items.Clear(); cmbTest.DataSource = v;
            });

            //var cmbTestDataSource = new BindingSource { DataSource =  propertyBindingManager.ViewModel.Items};
            //cmbTest.DataSource = cmbTestDataSource;
            //propertyBindingManager.ViewModel.Items.CollectionChanged +=
            //    (sender, args) => UIContext.Invoke(() =>
            //    {
            //        cmbTestDataSource.
            //        cmbTest.DataSource = null;
            //        cmbTest.Items.Clear();
            //        cmbTest.DataSource = propertyBindingManager.ViewModel.Items;
            //    });


            //CommandBindings.Add("1", new ControlBinder());

            //_commanBindingManager = new CommandBindingManager();
            commanBindingManager.Bind(propertyBindingManager.ViewModel.GetItems, btnNonBlocking);
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
