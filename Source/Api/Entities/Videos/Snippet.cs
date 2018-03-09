﻿using System;
using System.Collections.Generic;

namespace YoutubeSnoop.Api.Entities.Videos
{
    public class Snippet : TitleDescription
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PublishedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ChannelTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LiveBroadcastContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, Thumbnail> Thumbnails { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<string> Tags { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DefaultLanguage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DefaultAudioLanguage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TitleDescription Localized { get; set; }
    }
}