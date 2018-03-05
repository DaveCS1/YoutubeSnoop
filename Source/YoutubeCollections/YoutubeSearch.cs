﻿using YoutubeSnoop.Entities.Search;
using YoutubeSnoop.Settings;

namespace YoutubeSnoop
{
    public class YoutubeSearch : YoutubeCollection<YoutubeSearchResult, SearchResult, SearchApiRequestSettings>
    {
        public YoutubeSearch(IApiRequest<SearchResult, SearchApiRequestSettings> request) : base(request) { }

        protected override YoutubeSearchResult CreateItem(SearchResult response)
        {
            return new YoutubeSearchResult(response);
        }
    }
}