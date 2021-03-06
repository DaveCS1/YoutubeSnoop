﻿using System.Collections.Generic;
using System.Linq;
using YoutubeSnoop.Api;
using YoutubeSnoop.Enums;

namespace YoutubeSnoop.Fluent
{
    public static partial class Youtube
    {
        public static IEnumerable<YoutubeComment> TakePages(this YoutubeComments comments, int pageCount)
        {
            return comments.Take(comments.ResultsPerPage.GetValueOrDefault(ResultsPerPage) * pageCount);
        }

        public static IEnumerable<YoutubeComment> TakePage(this YoutubeComments comments)
        {
            return comments.TakePages(1);
        }

        public static YoutubeComments Comments(CommentSettings settings = null)
        {
            return new YoutubeComments(settings, null, ResultsPerPage);
        }

        public static YoutubeComment Comment(CommentSettings settings = null)
        {
            return new YoutubeComment(settings);
        }

        public static YoutubeComments Comments(params string[] ids)
        {
            return Comments(new CommentSettings { Id = ids.Aggregate() });
        }

        public static YoutubeComment Comment(string id)
        {
            return Comment(new CommentSettings { Id = id });
        }

        public static YoutubeComments ForParentId(this YoutubeComments comments, string id)
        {
            var settings = comments.Settings.Clone();
            settings.ParentId = id;
            return Comments(settings);
        }

        public static YoutubeComments TextFormat(this YoutubeComments comments, TextFormat f)
        {
            var settings = comments.Settings.Clone();
            settings.TextFormat = f;
            return Comments(settings);
        }

        public static YoutubeComment TextFormat(this YoutubeComment comment, TextFormat f)
        {
            var settings = comment.Settings.Clone();
            settings.TextFormat = f;
            return Comment(settings);
        }

        public static YoutubeComment FormatHtml(this YoutubeComment comment)
        {
            return comment.TextFormat(Enums.TextFormat.Html);
        }

        public static YoutubeComment FormatPlainText(this YoutubeComment comment)
        {
            return comment.TextFormat(Enums.TextFormat.PlainText);
        }

        public static YoutubeComments FormatHtml(this YoutubeComments comments)
        {
            return comments.TextFormat(Enums.TextFormat.Html);
        }

        public static YoutubeComments FormatPlainText(this YoutubeComments comments)
        {
            return comments.TextFormat(Enums.TextFormat.PlainText);
        }

        public static YoutubeComments RequestId(this YoutubeComments comments, params string[] ids)
        {
            var settings = comments.Settings.Clone();
            settings.Id = settings.Id.AddItems(ids);
            return Comments(settings);
        }

        public static YoutubeComments Replies(this YoutubeComment comment)
        {
            return Comments().ForParentId(comment.Id);
        }

        public static YoutubeComment Parent(this YoutubeComment comment)
        {
            if (comment.ParentId != null) return Comment(comment.ParentId);

            return null;
        }

        public static YoutubeCommentThread CommentThread(this YoutubeComment comment)
        {
            return CommentThread(comment.Id.Split('.')[0]);
        }

        public static YoutubeVideo Video(this YoutubeComment comment)
        {
            if (comment.VideoId == null) return null;
            return Video(comment.VideoId);
        }

        public static YoutubeChannel Channel(this YoutubeComment comment)
        {
            if (comment.ChannelId == null) return null;
            return Channel(comment.ChannelId);
        }

        public static YoutubeChannel AuthorChannel(this YoutubeComment comment)
        {
            if (comment.AuthorChannelId == null) return null;
            return Channel(comment.AuthorChannelId);
        }
    }
}