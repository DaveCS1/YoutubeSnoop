﻿namespace YoutubeSnoop.Api.Entities
{
    public class Location
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Altitude { get; set; }

        public override string ToString()
        {
            return $"({Latitude:0.#####},{Longitude:0.#####})";
        }
    }
}