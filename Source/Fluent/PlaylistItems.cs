﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoutubeSnoop.Entities.PlaylistItems;
using YoutubeSnoop.Enums;
using YoutubeSnoop.Interfaces;
using YoutubeSnoop.Settings;

namespace YoutubeSnoop.Fluent
{
    public static partial class Youtube
    {

        public static YoutubePlaylistItems PlaylistItems(PlaylistItemsApiRequestSettings settings, params PartType[] partTypes)
        {
            var request = DefaultRequest<PlaylistItem, PlaylistItemsApiRequestSettings>(settings, partTypes);
            return new YoutubePlaylistItems(request);
        }

        public static YoutubePlaylistItems PlaylistItems(PlaylistItemsApiRequestSettings settings = null)
        {
            return PlaylistItems(settings ?? new PlaylistItemsApiRequestSettings(), PartType.Snippet);
        }

        public static YoutubePlaylistItems PlaylistItems(string playlistId)
        {
            return PlaylistItems(new PlaylistItemsApiRequestSettings { PlaylistId = playlistId });
        }

        public static YoutubePlaylistItems PlaylistId(this YoutubePlaylistItems playlistItems, string id)
        {
            var request = playlistItems.Request.Clone();
            request.Settings.PlaylistId = id;
            return new YoutubePlaylistItems(request);
        }

        public static YoutubePlaylistItems RequestPart(this YoutubePlaylistItems playlistItems, PartType partType)
        {
            var request = playlistItems.Request.RequestPart(partType);
            return new YoutubePlaylistItems(request);
        }

        public static IYoutubeItem Details(this YoutubePlaylistItem playlistItem)
        {
            return playlistItem.Item.Snippet.ResourceId.Details();
        }

        public static TItem Details<TItem>(this YoutubePlaylistItem playlistItem) where TItem : class, IYoutubeItem
        {
            return Details(playlistItem) as TItem;
        }

        public static YoutubeVideos Videos(this YoutubePlaylistItems playlistItems)
        {
            return Videos(playlistItems.Where(i => i.Kind == ResourceKind.Video)
                                       .Select(i => i.Id));
        }
    }
}