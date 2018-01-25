using System;

namespace Arbor.EPiServer.MvcAreas
{
    public class Area
    {
        public Area(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        public string Name { get; private set; }
    }
}
