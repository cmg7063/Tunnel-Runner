using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TunnelRunner
{
    class Levels
    {
        //attributes
        Files allLevel = new Files();
        int currlvl;
        int speedboost;
        int intenseMilk;
        int invincibility;
        int obst;
        int ids;
        
        public void Populate()
        {
            allLevel.LoadAllFile();
        }
        // this class will use data fron Files (fileInfo) to populate all lists
        //to access fileInfo[0][0]  <------this is the first item in first level which would be what number of the level is

    }
}
