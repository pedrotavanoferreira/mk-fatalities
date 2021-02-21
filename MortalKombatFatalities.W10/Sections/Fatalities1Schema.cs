using System;
using AppStudio.DataProviders;

namespace MortalKombatFatalities.Sections
{
    /// <summary>
    /// Implementation of the Fatalities1Schema class.
    /// </summary>
    public class Fatalities1Schema : SchemaBase
    {

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
