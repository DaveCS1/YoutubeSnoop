﻿using System.Collections.Generic;
using System.Linq;
using YoutubeSnoop.Api;
using YoutubeSnoop.Api.Entities.CommentThreads;
using YoutubeSnoop.Enums;

namespace YoutubeSnoop
{
    public sealed class YoutubeCommentThread : YoutubeItem<CommentThread, CommentThreadSettings>, IYoutubeItem
    {
        private CommentThread _rawData;
        public CommentThread RawData => Set(ref _rawData);

        private string _id;
        public string Id => Set(ref _id);

        private ResourceKind _kind;
        public ResourceKind Kind => Set(ref _kind);

        private IReadOnlyList<YoutubeComment> _replies;
        public IReadOnlyList<YoutubeComment> Replies => Set(ref _replies);

        private YoutubeComment _topLevelComment;
        public YoutubeComment TopLevelComment => Set(ref _topLevelComment);

        private int _totalReplyCount;
        public int TotalReplyCount => Set(ref _totalReplyCount);

        public YoutubeCommentThread(CommentThread response) : base(response)
        {
        }

        public YoutubeCommentThread(CommentThreadSettings settings, IEnumerable<PartType> partTypes = null) : base(settings, partTypes)
        {
        }

        protected override void SetProperties(CommentThread response)
        {
            if (response == null) return;

            _rawData = response;
            _id = response.Id;
            _kind = response.Kind;
            _replies = response.Replies?.Comments.Select(c => new YoutubeComment(c)).ToList().AsReadOnly();

            if (response.Snippet == null) return;

            _topLevelComment = new YoutubeComment(response.Snippet.TopLevelComment);
            _totalReplyCount = response.Snippet.TotalReplyCount.GetValueOrDefault();
        }
    }
}