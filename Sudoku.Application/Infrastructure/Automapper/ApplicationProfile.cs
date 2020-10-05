using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Sudoku.Application.Interfaces;

namespace Sudoku.Application.Infrastructure.Automapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            LoadCustomMappings();
        }
        
        private void LoadCustomMappings()
        {
            var mapsFrom = FindMappings(Assembly.GetExecutingAssembly());

            foreach (var map in mapsFrom)
            {
                map.CreateMappings(this);
            }
        }

        private IList<IHaveCustomMapping> FindMappings(Assembly assembly)
        {
            var types = assembly.GetExportedTypes();

            var mapsFrom = (
                from type in types
                from instance in type.GetInterfaces()
                where
                    typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
                    !type.IsAbstract &&
                    !type.IsInterface
                select (IHaveCustomMapping)Activator.CreateInstance(type)).ToList();

            return mapsFrom;
        }
    }
}