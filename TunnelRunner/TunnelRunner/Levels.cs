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

        int frequency = 700;

        Texture2D speedImg;
        Texture2D milkImg;
        Texture2D invincImg;
        Texture2D idImg;
        Texture2D obsImg;

        List<Levels> lvlList = new List<Levels>();

        List<Collectibles> speedList;
        List<Collectibles> milkList;
        List<Collectibles> invincList;
        List<Collectibles> idList;
        List<Obstacles> obstList;

        Collectibles collObj;
        Obstacles obstObj;

        Random rgn = new Random();

        //properties
        public List<Levels> LvlList
        {
            get { return lvlList; }
        }
        public int Currlvl
        {
            get { return currlvl; }
            set { currlvl = value; }
        }
        public int Obst
        {
            get { return obst; }
        }
        public int IntenseMilk
        { get { return intenseMilk; } }

        public int Ids
        { get { return ids; } }

        public Texture2D SpeedImg
        {
            get { return speedImg; }
            set { speedImg = value; }
        }
        public Texture2D MilkImg
        {
            get { return milkImg; }
            set { milkImg = value; }
        }

        public Texture2D InvincImg
        {
            get { return invincImg; }
            set { invincImg = value; }
        }

        public Texture2D IdImg
        {
            get { return idImg; }
            set { idImg = value; }
        }
        public Texture2D ObsImg
        {
            get { return obsImg; }
            set { obsImg = value; }
        }

        //constructor
        public Levels( )
        {
            currlvl = 0;
            speedboost = 0;
            intenseMilk = 0;
            invincibility = 0;
            obst = 0;
            ids = 0;
        }

        public void Populate()
        {
            allLevel.LoadAllFile();
            
        }

        public void LoadLevels()
        {
            for (int i = 0; i < 90; i++) // Make a Length() property in Files to get the amount of files w/ "level" in name
            {
                Levels myLvl = new Levels();
                // Populate level list
                myLvl.currlvl = allLevel.FileInfo[i][0];

                myLvl.speedboost = allLevel.FileInfo[i][1];
              
                myLvl.intenseMilk = allLevel.FileInfo[i][2];
                
                myLvl.invincibility = allLevel.FileInfo[i][3];
                
                myLvl.obst = allLevel.FileInfo[i][4];
               
                myLvl.ids = allLevel.FileInfo[i][5];
                
                lvlList.Add(myLvl);
            }           
        }
        
        public void GetLvl(int lvl)
        {
            currlvl = LvlList[lvl-1].currlvl;
            speedboost = LvlList[lvl-1].speedboost;
            intenseMilk = LvlList[lvl-1].intenseMilk;
            invincibility = LvlList[lvl-1].invincibility;
            obst = LvlList[lvl-1].obst;
            ids = LvlList[lvl-1].ids;

        }
    }
}
