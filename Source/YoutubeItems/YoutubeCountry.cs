﻿using YoutubeSnoop.Entities.I18nRegions;
using YoutubeSnoop.Enums;
using YoutubeSnoop.Interfaces;

namespace YoutubeSnoop
{
    public class YoutubeCountry : IYoutubeItem
    {
        public ResourceKind Kind { get; }
        public string Id { get; }
        public I18nRegion Item { get; }
        public string CountryCode { get; }
        public string CountryName { get; }

        public YoutubeCountry(I18nRegion region)
        {
            if (region == null) return;

            Item = region;
            Kind = region.Kind;
            Id = region.Id;
            CountryCode = region.Snippet.Gl;
            CountryName = region.Snippet.Name;
        }

        public YoutubeCountry(string countryCode, string countryName)
        {
            Kind = ResourceKind.I18nRegion;
            Id = CountryCode = countryCode;
            CountryName = countryName;
        }
    }
}