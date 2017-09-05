using System;

namespace BDSA2017.Lecture02
{
    public class TrafficLightController : ITrafficLightController
    {
        public bool MayIGo(TrafficLightColor color)
        {
            switch (color)
            {
                case TrafficLightColor.Red:
                case TrafficLightColor.Yellow:
                     return false;
               case TrafficLightColor.Green:
                    return true;
                default:
                    throw new ArgumentException("Wrong color", nameof(color));
            }
        }
    }
}
