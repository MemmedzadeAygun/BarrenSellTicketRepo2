using AutoMapper;
using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Application.Mappers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Mappers.Configurations
{
    public class MapperConfiguration:Profile
    {
        public MapperConfiguration()
        {
            var mappers = GetMaps();

            foreach (var item in mappers)
            {
                CreateMap(item.Source, item.Destination).ReverseMap();
            }
        }
        private static IEnumerable<MapModel> GetMaps()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            return types
                .SelectMany(type => type.GetInterfaces()
                .Where(z => z.IsGenericType && z.GetGenericTypeDefinition() == typeof(IMapTo<>))
                .Select(z => new MapModel
                {
                    Source = type,
                    Destination = z.GenericTypeArguments[0]
                }))
                .ToList();
        }
    }
}
