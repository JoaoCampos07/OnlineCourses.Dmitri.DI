using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Autofac.Features.Metadata;
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
        private string name;

        public Button(ICommand command, string name)
        {
            this.command = command;
        }

        public void OnClick()
        {
            command.Execute();
        }

        public void PrintMe() => Console.WriteLine($"I am a button called {name}");
    }

    public class Editor
    {
        private IEnumerable<Button> buttons;

        public Editor(IEnumerable<Button> buttons)
        {
            this.Buttons = buttons;
        }

        public IEnumerable<Button> Buttons { get => buttons; private set => buttons = value; }

        public void ClickAll()
        {
            foreach (var btn in Buttons)
                btn.OnClick();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<OpenCommand>().As<ICommand>()
                .WithMetadata("Name", "Open");
            b.RegisterType<SaveCommand>().As<ICommand>()
                .WithMetadata("Name", "Save");

            // Let's specifiy the name of the button with Metadata
            b.RegisterAdapter<Meta<ICommand>, Button>(cmd => new Button(cmd.Value, (string)cmd.Metadata["Name"]));
            b.RegisterType<Editor>();

            using (var c = b.Build())
            {
                var editor = c.Resolve<Editor>();

                foreach (var btn in editor.Buttons)
                {
                    btn.PrintMe();
                }
            }
        }
    }
}
