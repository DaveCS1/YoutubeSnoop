﻿using System;
using YoutubeSnoop.Api.Entities.I18nLanguages;
using YoutubeSnoop.Api.Entities.Search;
using YoutubeSnoop.Api.Settings;
using YoutubeSnoop.Enums;

namespace YoutubeSnoop.Fluent
{
    public static partial class Youtube
    {
        public static YoutubeLanguages Languages(string languageCode = "")
        {
            var settings = new I18nLanguageApiRequestSettings { Hl = languageCode };
            var request = DefaultRequest<I18nLanguage, I18nLanguageApiRequestSettings>(settings, new[] { PartType.Snippet });
            return new YoutubeLanguages(request);
        }


    }
}
