using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace WinFormsMVVM
{

    public interface ICommandBinder
    {
        Type SourceType { get; }
        void Bind(ICommand command, object source);
    }

    public abstract class CommandBinder<T> : ICommandBinder where T : IComponent
    {
        public Type SourceType => typeof(T);

        public void Bind(ICommand command, object source)
        {
            Bind(command, (T)source);
        }

        protected abstract void Bind(ICommand command, T source);
    }

    public class ControlBinder : CommandBinder<Control>
    {
        protected override void Bind(ICommand command, Control source)
        {
            source.DataBindings.Add("Enabled", command, "Enabled");
            source.DataBindings.Add("Text", command, "Name");
            source.Click += (o, e) => command.Execute(o);
        }
    }

    //public class MenuItemCommandBinder : CommandBinder<ToolStripItem>
    //{
    //    protected override void Bind(ICommand command, ToolStripItem source)
    //    {
    //        source.Text = command.Name;
    //        source.Enabled = command.Enabled;
    //        source.Click += (o, e) => command.Execute();

    //        command.PropertyChanged += (o, e) => source.Enabled = command.Enabled;
    //    }
    //}
}