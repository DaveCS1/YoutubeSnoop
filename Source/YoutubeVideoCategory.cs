﻿using System.Collections.Generic;
using YoutubeSnoop.Api;
using YoutubeSnoop.Api.Entities.VideoCategories;
using YoutubeSnoop.Enums;

namespace YoutubeSnoop
{
    public sealed class YoutubeVideoCategory : YoutubeItem<VideoCategory, VideoCategorySettings>, IYoutubeItem, ITitle
    {
        private VideoCategory _rawData;
        public VideoCategory RawData => Set(ref _rawData);

        private string _id;
        public string Id => Set(ref _id);

        private ResourceKind _kind;
        public ResourceKind Kind => Set(ref _kind);

        private string _channelId;
        public string ChannelId => Set(ref _channelId);

        private string _title;
        public string Title => Set(ref _title);

        private bool _assignable;
        public bool Assignable => Set(ref _assignable);

        public YoutubeVideoCategory(VideoCategory response) : base(response)
        {
        }

        public YoutubeVideoCategory(VideoCategorySettings settings, IEnumerable<PartType> partTypes = null) : base(settings, partTypes)
        {
        }

        protected override void SetProperties(VideoCategory response)
        {
            if (response == null) return;

            _rawData = response;
            _id = response.Id;
            _kind = response.Kind;

            if (response.Snippet == null) return;

            _channelId = response.Snippet.ChannelId;
            _title = response.Snippet.Title;
            _assignable = response.Snippet.Assignable.GetValueOrDefault();
        }
    }
}