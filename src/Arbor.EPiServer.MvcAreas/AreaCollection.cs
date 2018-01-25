using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Arbor.EPiServer.MvcAreas
{
    public class AreaCollection : Collection<Area>
    {
        public bool Contains(string areaName)
        {
            if (string.IsNullOrWhiteSpace(areaName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(areaName));
            }

            return this.FirstOrDefault(a => a.Name.Equals(areaName, StringComparison.OrdinalIgnoreCase)) != null;
        }
    }
}
