using Microsoft.AspNetCore.Mvc;

namespace MinecraftTrackerObjects
{
    public class MapPoint
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Coordinates { get; set; }

        [BindProperty]
        public string Position { get; set; }
    }
}
