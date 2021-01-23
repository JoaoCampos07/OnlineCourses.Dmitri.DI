using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Autofac.Features.AttributeFilters;
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

    public class ArtDisplay
    {
        private IArtWork _artWork;

        // Filter the components so that we got the right component to be injected using the metadata
        // (Obsiouly this is for when we have 2 compoents was 1 service)
        public ArtDisplay([MetadataFilter("Age", 100)]IArtWork artWork)
        {
            _artWork = artWork;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
