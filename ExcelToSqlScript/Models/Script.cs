﻿using System;
using System.IO;

namespace ExcelToSqlScript
{
    public class Script : IDisposable
    {
        public Script(string name, Stream content)
        {
            Name = name;
            Content = content;
        }

        public string Name { get; }

        public Stream Content { get; }

        public void Dispose()
        {
            Content?.Dispose();
        }
    }
}