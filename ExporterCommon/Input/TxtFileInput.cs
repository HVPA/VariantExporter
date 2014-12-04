using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ExporterCommon.Input
{
    public class TxtFileInput
    {
        string _filePath;

        public TxtFileInput(string filePath)
        {
            _filePath = filePath;
        }

        public List<string> GetLines()
        {
            List<string> lines = new List<string>();
            string line;

            StreamReader file = new StreamReader(_filePath);
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }

            return lines;
        }
    }
}
