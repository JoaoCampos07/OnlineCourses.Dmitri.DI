﻿using Autofac;
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
            b.RegisterType<Button>();
            b.RegisterType<Editor>();

            using (var c = b.Build())
            {
                var editor = c.Resolve<Editor>();
                editor.ClickAll();
            }
        }
    }
}
