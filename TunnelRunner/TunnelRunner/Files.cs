using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TunnelRunner
{
    class Files
    {
        int numFiles = 0;
        List<int[]> fileInfo = new List<int[]>();
        int content; //what's in that line of a file
        int[] data = new int[7];

        //properties
        public List<int[]> FileInfo
        { get { return fileInfo; } }

        public void LoadAllFile()
        {
            try
            {
                //count all level files
                string[] files = Directory.GetFiles(".");
                foreach(string file in files)
                {
                    if (file.Contains("level"))
                    {
                        numFiles++;
                    }
                }
                //sort the files, so levels load in in order
                for (int i = 1; i <= numFiles; i++)
                {
                    int lineNum = 0;
                    StreamReader read = new StreamReader(File.OpenRead("level" + i + ".txt"));
                    string line = "";
                    while ((line = read.ReadLine()) != null)
                    {
                        data[lineNum] = int.Parse(line);
                        //Console.WriteLine("lineNum: " + lineNum + " Content: " + line);
                        lineNum++;
                    }
                    read.Close();
                    fileInfo.Add(data);
                    //Console.WriteLine("level " + i + " added\n");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
