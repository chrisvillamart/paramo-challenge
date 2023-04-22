using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sat.Recruitment.Configuration.Utilities
{
    public class FileReader
    {
        public static StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
