﻿namespace Arbor.EPiServer.MvcAreas
{
    public class AreaConfigurationSettings
    {
        public AreaConfigurationSettings()
        {
            EnableAreaDetectionByController = true;
        }

        public bool EnableAreaDetectionByController { get; set; }

        public bool EnableAreaDetectionBySite { get; set; }
    }
}
