using System;

namespace Connection
{
    public interface IDistributor
    {
        // public Core.Route GetRoute(Core.RoutePackage task);

        public Core.GeoRoute GetGeoRoute(Core.RoutePackage task);
    }
}