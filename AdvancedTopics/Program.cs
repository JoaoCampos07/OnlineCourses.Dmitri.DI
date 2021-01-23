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
using System.ComponentModel.Composition;

namespace AdvancedTopics
{
    // Natural way of attach metadata to components

    // Obj that represents the metadata that we want 
    [MetadataAttribute]
    public class AgeMetadataAttribute : Attribute
    {
        public int Age { get; set; }

        public AgeMetadataAttribute(int age)
        {
            Age = age;
        }
    }

    public interface IArtWork
    {
        void Display();
    }

    [AgeMetadataAttribute(100)]
    public class CenturyArtwork : IArtWork
    {
        public void Display() => Console.WriteLine("Displaying a century-old piece.");
        
    }

    [AgeMetadataAttribute(1000)]
    public class MillenniumArtwork : IArtWork
    {
        public void Display() => Console.WriteLine("Displaying a millennium-old piece.");
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
