﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;

namespace Microsoft.Data.Entity.Relational.Design.FunctionalTests.ReverseEngineering
{
    public class FileSet
    {
        public List<string> Files = new List<string>();
        private readonly IFileService _fileService;

        public FileSet(IFileService fileService, string directory)
        {
            _fileService = fileService;
            Directory = directory;
        }

        public string Directory { get; }

        public bool Exists(int index) => Exists(Files[index]);
        public bool Exists(string filepath) => _fileService.FileExists(Directory, filepath);
        public string Contents(int index) => Contents(Files[index]);
        public string Contents(string filepath) => _fileService.RetrieveFileContents(Directory, filepath);
    }
}
