using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AccServerAdmin.Domain.AccResults;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace AccServerAdmin.Tests
{
    
    public class Test
    {
        [Test]
        public void ReadResult()
        {
            var path = "C:\\ProgramData\\AccServerAdmin\\ServerInstances\\results";
            var entries = Directory.EnumerateFiles(path, "*R.json").ToList();

            foreach (var entry in entries)
            {
                var json = File.ReadAllText(entry, Encoding.Unicode);
                var results = JsonConvert.DeserializeObject<ResultFile>(json);
            }
        }
    }
}
