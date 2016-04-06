using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TunnelRunner
{
    class Levels
    {
        int level;
        int speedBoost;
        int intenseMilk;
        int invincibility;
        int obstacle;
        int IDGenerated;
        int IDNeeded;
        int lineNum = 0;
        
        string line = "";

        int num;
        string str;

        string[] dataStr = new string[7];
        int[] data = new int[7];

        public void  ReadInLevel(string filename)
        {
            try
            {
                StreamReader read = new StreamReader(filename);
                while((line=read.ReadLine())!=null)
                {
                    dataStr[lineNum] = line;
                    lineNum++;
                }
                read.Close();
            }
            catch(IOException ioe)
            {
                Console.WriteLine("Error read in level " + level);
            }
            for (int i = 0; i < 7; i++) //parsing all strings to int, and then store them into a int array
            {
                str = dataStr[i];
                if(int.TryParse(str,out num))
                {
                    data[i] = num;
                }
            }
            level = data[0];
            speedBoost = data[1];
            intenseMilk = data[2];
            invincibility = data[3];
            obstacle = data[4];
            IDGenerated = data[5];
            IDNeeded = data[6];
        }

    }
}
