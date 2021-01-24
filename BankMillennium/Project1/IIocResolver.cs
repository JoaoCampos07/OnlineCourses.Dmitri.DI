//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IIocResolver.cs" company="Bank Millennium">
//    Copyright © Bank Millennium SA
//  </copyright>
//  <summary>
//    Defines the IIocResolver.cs type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace BM.Ioc.Abstractions
{
    public interface IIocResolver
    {
        T Resolve<T>() where T : class;
    }
}