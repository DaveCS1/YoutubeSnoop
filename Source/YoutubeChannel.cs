﻿using System;
using System.Collections.Generic;
using YoutubeSnoop.Api;
using YoutubeSnoop.Api.Entities;
using YoutubeSnoop.Api.Entities.Channels;
using YoutubeSnoop.Enums;

namespace YoutubeSnoop
{
    public sealed class YoutubeChannel : YoutubeItem<Channel, ChannelSettings>, IYoutubeItem, ITitle, IDescription, IUrl
    {
        private const string _channelUrl = "https://www.youtube.com/channel/{0}";
        private const string _channelUrlCustom = "https://www.youtube.com/{0}";

        private Channel _rawData;
        public Channel RawData => Set(ref _rawData);

        private string _id;
        public string Id => Set(ref _id);

        private ResourceKind _kind;
        public ResourceKind Kind => Set(ref _kind);

        private string _title;
        public string Title => Set(ref _title);

        private string _description;
        public string Description => Set(ref _description);

        private string _customUrl;
        public string CustomUrl => Set(ref _customUrl);

        private DateTime _publishedAt;
        public DateTime PublishedAt => Set(ref _publishedAt);

        private IReadOnlyDictionary<ThumbnailSize, Thumbnail> _thumbnails;
        public IReadOnlyDictionary<ThumbnailSize, Thumbnail> Thumbnails => Set(ref _thumbnails);

        private Thumbnail _defaultThumbnail;
        public Thumbnail DefaultThumbnail => Set(ref _defaultThumbnail);

        private string _uploadsPlaylistId;
        public string UploadsPlaylistId => Set(ref _uploadsPlaylistId);

        private long _subscriberCount;
        public long SubscriberCount => Set(ref _subscriberCount);

        private long _uploadsCount;
        public long UploadsCount => Set(ref _uploadsCount);

        private long _viewCount;
        public long ViewCount => Set(ref _viewCount);

        private long _commentCount;
        public long CommentCount => Set(ref _commentCount);

        private string _url;
        public string Url => Set(ref _url);

        public YoutubeChannel(Channel response) : base(response)
        {
        }

        public YoutubeChannel(ChannelSettings settings, IEnumerable<PartType> partTypes = null) : base(settings, partTypes)
        {
        }

        protected override void SetProperties(Channel response)
        {
            if (response == null) return;

            _rawData = response;
            _id = response.Id;
            _kind = response.Kind;
            _url = GetUrl(response.Id);

            if (response.Snippet != null)
            {
                _title = response.Snippet.Title;
                _description = response.Snippet.Description;
                _customUrl = GetUrl(response.Snippet.CustomUrl, true);
                _publishedAt = response.Snippet.PublishedAt.GetValueOrDefault();
                _thumbnails = response.Snippet.Thumbnails?.Clone();
                _thumbnails?.TryGetValue(ThumbnailSize.Default, out _defaultThumbnail);
            }

            if (response.ContentDetails != null)
            {
                _uploadsPlaylistId = response.ContentDetails.RelatedPlaylists?.Uploads;
            }

            if (response.Statistics != null)
            {
                _subscriberCount = response.Statistics.SubscriberCount.GetValueOrDefault();
                _uploadsCount = response.Statistics.VideoCount.GetValueOrDefault();
                _viewCount = response.Statistics.ViewCount.GetValueOrDefault();
                _commentCount = response.Statistics.CommentCount.GetValueOrDefault();
            }
        }

        public static string GetUrl(string id, bool isCustom = false)
        {
            if (string.IsNullOrEmpty(id)) return null;
            return string.Format(isCustom ? _channelUrlCustom : _channelUrl, id);
        }
    }
}