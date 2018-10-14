using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinFormsMVVM.MVVM
{
    public interface ICommandBinder
    {
        Type SourceType { get; }
        void Bind(Command command, object source);
    }

    public abstract class CommandBinder<T> : ICommandBinder where T : IComponent
    {
        public Type SourceType => typeof(T);

        public void Bind(Command command, object source)
        {
            Bind(command, (T) source);
        }

        protected abstract void Bind(Command command, T source);
    }

    public class ControlBinder : CommandBinder<Control>
    {
        protected override void Bind(Command command, Control target)
        {
            if (target.DataBindings["Enabled"] != null || target.DataBindings["Text"] != null) return;

            target.DataBindings.Add("Enabled", command, "Enabled");
            target.DataBindings.Add("Text", command, "Name");
            target.Click += command.Execute;
        }
    }

    //useless, ToolStripItem is Icomponent, see above imlementation
    public class MenuItemCommandBinder : CommandBinder<ToolStripItem>
    {
        protected override void Bind(Command command, ToolStripItem target)
        {
            target.Text = command.Name;
            target.Enabled = command.Enabled;
            target.Click += command.Execute;

            command.PropertyChanged += (o, e) => target.Enabled = command.Enabled;
        }
    }
}