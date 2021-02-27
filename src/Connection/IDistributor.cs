using System;

namespace Connection
{
    public interface IDistributor
    {
        public Core.Route GetRoute(Core.RoutePackage task);
    }
}