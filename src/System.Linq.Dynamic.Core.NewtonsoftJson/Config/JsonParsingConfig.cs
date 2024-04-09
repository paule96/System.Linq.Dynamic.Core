﻿using JsonConverter.Abstractions.Models;

namespace System.Linq.Dynamic.Core.NewtonsoftJson.Config;

public class JsonParsingConfig : ParsingConfig
{
    public static JsonParsingConfig Default { get; } = new();

    public DynamicJsonClassOptions? DynamicJsonClassOptions { get; set; } 
}