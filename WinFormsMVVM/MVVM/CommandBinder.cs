using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinFormsMVVM
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
            Bind(command, (T)source);
        }

        protected abstract void Bind(Command command, T source);
    }

    public class ControlBinder : CommandBinder<Control>
    {
        protected override void Bind(Command command, Control source)
        {
            if (source.DataBindings["Enabled"] != null || source.DataBindings["Text"] != null) return;

            source.DataBindings.Add("Enabled", command, "Enabled");
            source.DataBindings.Add("Text", command, "Name");
            source.Click += command.Execute;
        }
    }

    public class MenuItemCommandBinder : CommandBinder<ToolStripItem>
    {
        protected override void Bind(Command command, ToolStripItem source)
        {
            source.Text = command.Name;
            source.Enabled = command.Enabled;
            source.Click += command.Execute;

            command.PropertyChanged += (o, e) => source.Enabled = command.Enabled;
        }
    }
}