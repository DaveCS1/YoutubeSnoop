﻿using System.Collections.Generic;
using YoutubeSnoop.Interfaces;

namespace YoutubeSnoop.Entities
{
    public class ChannelBrandingSettingsChannel : ITitleDescription
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string DefaultTab { get; set; }
        public string TrackingAnalyticsAccountId { get; set; }
        public bool? ModerateComments { get; set; }
        public bool? ShowRelatedChannels { get; set; }
        public bool? ShowBrowseView { get; set; }
        public string FeaturedChannelsTitle { get; set; }
        public IList<string> FeaturedChannelsUrls { get; set; }
        public string UnsubscribedTrailer { get; set; }
        public string ProfileColor { get; set; }
        public string DefaultLanguage { get; set; }
        public string Country { get; set; }
    }
}