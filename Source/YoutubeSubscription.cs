﻿using System;
using System.Collections.Generic;
using YoutubeSnoop.Enums;
using YoutubeSnoop.Api.Settings;
using YoutubeSnoop.Api.Entities.Subscriptions;
using YoutubeSnoop.Api.Entities;
using YoutubeSnoop.Api;

namespace YoutubeSnoop
{
    public sealed class YoutubeSubscription : YoutubeItem<Subscription, SubscriptionApiRequestSettings>, IYoutubeItem
    {

        private Subscription _item;
        public Subscription Item => Set(ref _item);

        private string _id;
        public string Id => Set(ref _id);

        private ResourceKind _kind;
        public ResourceKind Kind => Set(ref _kind);

        private DateTime _publishedAt;
        public DateTime PublishedAt => Set(ref _publishedAt);

        private string _channelId;
        public string ChannelId => Set(ref _channelId);

        private string _subscriberChannelId;
        public string SubscriberChannelId => Set(ref _subscriberChannelId);

        private string _title;
        public string Title => Set(ref _title);

        private string _description;
        public string Description => Set(ref _description);

        private string _channelTitle;
        public string ChannelTitle => Set(ref _channelTitle);

        private IReadOnlyDictionary<ThumbnailSize, Thumbnail> _thumbnails;
        public IReadOnlyDictionary<ThumbnailSize, Thumbnail> Thumbnails => Set(ref _thumbnails);

        public YoutubeSubscription(IApiRequest<Subscription, SubscriptionApiRequestSettings> request) : base(request) { }

        public YoutubeSubscription(Subscription response) : base(response) { }

        protected override void SetProperties(Subscription response)
        {
            if (response == null) return;

            _item = response;
            _id = response.Id;
            _kind = response.Kind;

            if (response.Snippet != null)
            {
                _publishedAt = response.Snippet.PublishedAt.GetValueOrDefault();
                _channelId = response.Snippet.ChannelId;
                _subscriberChannelId = response.Snippet.ResourceId?.ChannelId;
                _title = response.Snippet.Title;
                _description = response.Snippet.Description;
                _channelTitle = response.Snippet.ChannelTitle;
                _thumbnails = response.Snippet.Thumbnails?.Clone();
            }
        }
    }
}