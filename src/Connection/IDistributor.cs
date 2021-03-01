using System;

namespace Connection
{
    public interface IDistributor
    {
        public Core.Route GetGeoRoute(Core.RoutePackage task);
    }
}