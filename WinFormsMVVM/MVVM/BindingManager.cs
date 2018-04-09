using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace WinFormsMVVM
{
    public class BindingManager : Component
    {
        private IList<ICommand> Commands { get; }
        private IList<ICommandBinder> Binders { get; }

        public BindingManager()
        {
            Commands = new List<ICommand>();

            Binders = new List<ICommandBinder>
                          {
                              new ControlBinder(),
                              //new MenuItemCommandBinder()
                          };

            Application.Idle += UpdateCommandState;
        }

        private void UpdateCommandState(object sender, EventArgs e)
        {
            // ReSharper disable once UnusedVariable
            foreach (var command in Commands)
            {
                //command.Enabled();
            }
        }

        public BindingManager Bind(ICommand command, IComponent component)
        {
            if (!Commands.Contains(command))
                Commands.Add(command);

            FindBinder(component).Bind(command, component);
            return this;
        }

        protected ICommandBinder FindBinder(IComponent component)
        {
            ICommandBinder binder = null;
            var type = component.GetType();
            while (type != null)
            {
                binder = Binders.FirstOrDefault(x => x.SourceType == type);
                if (binder != null)
                    return binder;

                type = type.BaseType;
            }

            if (binder == null)
                throw new Exception($"No binding found for component of type {component.GetType().Name}");

            return binder;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Application.Idle -= UpdateCommandState;

            base.Dispose(disposing);
        }
    }



}
