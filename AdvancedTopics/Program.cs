using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Autofac.Features.ResolveAnything;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AdvancedTopics
{
    public interface ICommand
    {
        void Execute();
    }

    public class SaveCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Saving a file.");
        }
    }

    public class OpenCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Opening a file");
        }
    }

    // A button executes one command  
    public class Button
    {
        private ICommand command;

        public Button(ICommand command)
        {
            this.command = command;
        }

        public void OnClick()
        {
            command.Execute();
        }
    }

    public class Editor
    {
        private IEnumerable<Button> buttons;

        public Editor(IEnumerable<Button> buttons)
        {
            this.buttons = buttons;
        }

        public void ClickAll()
        {
            foreach (var btn in buttons)
                btn.OnClick();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<OpenCommand>().As<ICommand>();
            b.RegisterType<SaveCommand>().As<ICommand>();

            // How to set up Connection between command and button ? 
            //b.RegisterType<Button>();
            // We need to tell that the button is a adapter 
            b.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd));
            b.RegisterType<Editor>();

            using (var c = b.Build())
            {
                var editor = c.Resolve<Editor>();
                editor.ClickAll();
                // What is happen ? 
                // Autofac starts resolving Editor, it sees it needs Ienumerable<Buttons>, is going to get a button, 
                // them to resolve a ICOmmand for the button it needs to fulfill the criteria, one comand one button. 
                // since OpenCommand and SaveCommand implements ICommand, we needs to create aditional button, to resolve the editor dependencie.
            }
        }
    }
}
