﻿using Newtonsoft.Json;
using YoutubeSnoop.Api.Converters;

namespace YoutubeSnoop.Api.Entities
{
    [JsonConverter(typeof(ThumbnailConverter))]
    public class Thumbnail
    {
        public string Url { get; }
        public int Width { get; }
        public int Height { get; }

        public Thumbnail(string url, int width, int height)
        {
            Url = url; Width = width; Height = height;
        }
    }
}