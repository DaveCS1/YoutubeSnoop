﻿using System;
using System.Collections.Generic;
using System.Linq;
using YoutubeSnoop.Arguments;
using YoutubeSnoop.Entities;
using YoutubeSnoop.Enumerables;
using YoutubeSnoop.Enums;
using YoutubeSnoop.Interfaces;

namespace YoutubeSnoop
{
    public class ApiRequest<TItem, TSettings>
        where TItem : IResponse
        where TSettings : IApiRequestSettings
    {
        private const string _apiUrl = "https://www.googleapis.com/youtube/v3/{0}?{1}";

        private const string _apiKey = "AIzaSyAHVb6LDoO5aARmDlUe9PIeU_U1et1bWd8"; // project Api key, do not touch!
        private const bool _prettyPrint = false;

        private const string _prettyPrintName = "prettyPrint";
        private const string _apiKeyName = "key";

        private static readonly ApiArgument _prettyPrintArgument = new ApiArgument<bool>(_prettyPrintName, _prettyPrint);
        private static readonly ApiArgument _apiKeyArgument = new ApiArgument(_apiKeyName, _apiKey);

        private Response<TItem> _response;
        private IEnumerable<Response<TItem>> _totalResponses;
        private IEnumerable<TItem> _totalItems;

        public Response<TItem> Response => _response ?? (_response = Deserialize(PageToken));
        public IEnumerable<Response<TItem>> TotalResponses => _totalResponses ?? (_totalResponses = new PagedResponseEnumerable<TItem>(Response, Deserialize));
        public IEnumerable<TItem> TotalItems => _totalItems ?? (_totalItems = TotalResponses.SelectMany(r => r.Items));

        public TSettings Settings { get; }
        public int ResultsPerPage { get; set; }
        public string PageToken { get; set; }
        public IEnumerable<PartType> PartTypes { get; }

        public string RequestUrl { get; protected set; }

        public event EventHandler FirstResponseDownloaded; // TODO
        public event EventHandler ResponseDownloaded;

        public ApiRequest(TSettings settings, IEnumerable<PartType> partTypes, string pageToken = null, int resultsPerPage = 20)
        {
            ResultsPerPage = resultsPerPage;
            Settings = settings;
            PageToken = pageToken;
            PartTypes = partTypes ?? new[] { PartType.Snippet };
        }

        public ApiRequest(TSettings settings, PartType partType, string pageToken = null, int resultsPerPage = 20)
            : this(settings, new[] { partType }, pageToken, resultsPerPage) { }

        public ApiRequest(TSettings settings, string pageToken = null, int resultsPerPage = 20)
            : this(settings, null, pageToken, resultsPerPage) { }

        protected static string FormatApiUrl(TSettings settings, IEnumerable<PartType> partTypes, string pageToken, int resultsPerPage)
        {
            var arguments = settings.GetArguments().ToList();
            arguments.Add(new ApiPartArgument(partTypes));
            arguments.Add(new ApiArgument(nameof(pageToken), pageToken));
            arguments.Add(new ApiArgument<int>(nameof(resultsPerPage), resultsPerPage));
            arguments.Add(_prettyPrintArgument);
            arguments.Add(_apiKeyArgument);

            var argumentString = arguments.Select(a => a.ToString())
                                          .Where(s => !string.IsNullOrEmpty(s))
                                          .Aggregate((s1, s2) => $"{s1}&{s2}");

            return string.Format(_apiUrl, settings.RequestType.ToCamelCase(), argumentString);
        }

        public Response<TItem> Deserialize(string pageToken)
        {
            RequestUrl = FormatApiUrl(Settings, PartTypes, pageToken, ResultsPerPage);
            var json = JsonDownloader.Download(RequestUrl);
            return ResourceFactory.DeserializeResponse<TItem>(json);
        }

        protected void OnFirstResponseDownloaded(EventArgs e)
        {
            FirstResponseDownloaded?.Invoke(this, e);
        }

        protected void OnResponseDownloaded(EventArgs e)
        {
            ResponseDownloaded?.Invoke(this, e);
        }
    }
}