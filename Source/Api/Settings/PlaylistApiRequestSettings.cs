﻿using YoutubeSnoop.Enums;

namespace YoutubeSnoop.Api.Settings
{
    public sealed class PlaylistApiRequestSettings : ApiRequestSettings
    {
        public override RequestType RequestType => RequestType.Playlists;

        public string ChannelId { get; set; }
        public string Id { get; set; }
        public string Hl { get; set; }
    }
}