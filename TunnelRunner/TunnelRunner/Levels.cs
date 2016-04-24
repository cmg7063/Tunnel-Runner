using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TunnelRunner
{        
    // This class uses data fron Files() (fileInfo) to populate all lists
    // To access, fileInfo[0][0]  <--- This is the first item in first level which would be what number of the level is
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

        Texture2D speedImg;
        Texture2D milkImg;
        Texture2D invincImg;
        Texture2D idImg;


        List<Levels> lvlList;

        List<Collectibles> speedList;
        List<Collectibles> milkList;
        List<Collectibles> invincList;
        List<Collectibles> idList;
        List<Obstacles> obstList;

        Collectibles collectible;
        Obstacles obstObj;

        public Levels()
        {

        }

        public void Populate()
        {
            allLevel.LoadAllFile();
        }

        public void LoadLevels()
        {
            for (int i = 0; i < 90; i++) // Make a Length() property in Files to get the amount of files w/ "level" in name
            {
                // Within each level in the list of Levels, populate individual collectible and obst. lists
                foreach (Levels level in lvlList)
                {
                    // Populate level list
                    currlvl = allLevel.FileInfo[i][0];
                
                    // Populate speedboost list, add objs to list
                    speedboost = allLevel.FileInfo[i][1];
                    speedList = new List<Collectibles>(speedboost);
                    foreach (Collectibles collect in speedList)
                    {
                        speedList.Add(collectible);
                        collectible.CollectibleImage = speedImg;
                    }

                    // Populate intenseMilk list
                    intenseMilk = allLevel.FileInfo[i][2];
                    milkList = new List<Collectibles>(intenseMilk);
                    foreach (Collectibles collect in milkList)
                    {
                        milkList.Add(collectible);
                        collectible.CollectibleImage = milkImg;
                    }

                    // Populate invincibility list
                    invincibility = allLevel.FileInfo[i][3];
                    invincList = new List<Collectibles>(invincibility);
                    foreach(Collectibles collect in invincList)
                    {
                        invincList.Add(collectible);
                        collectible.CollectibleImage = invincImg;
                    }

                    // Populate obstacle list
                    obst = allLevel.FileInfo[i][4];
                    obstList = new List<Obstacles>(obst);
                    foreach (Obstacles obstacle in obstList)
                    {
                        obstList.Add(obstObj);
                    }

                    // Populate id list
                    ids = allLevel.FileInfo[i][5];
                    idList = new List<Collectibles>(ids);
                    foreach (Collectibles collect in idList)
                    {
                        idList.Add(collectible);
                        collectible.CollectibleImage = idImg;
                    }
                }           
            }
        }
    }
}
