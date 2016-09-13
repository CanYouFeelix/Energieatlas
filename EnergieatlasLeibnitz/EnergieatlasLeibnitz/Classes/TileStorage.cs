using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EnergieatlasLeibnitz.Classes
{
    class TileStorage
    {
        public Tile[,] tileArrayLayer15;
        public Tile[,] tileArrayLayer16;
        public Tile[,] tileArrayLayer17;
        public Tile[,] tileArrayLayer18;

        public TileStorage()
        {
            tileArrayLayer15 = new Tile[3,4];
            tileArrayLayer16 = new Tile[6,8];
            tileArrayLayer17 = new Tile[12,16];
            tileArrayLayer18 = new Tile[22,30];
        }

        public void Generate()
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    string projectLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    projectLocation = projectLocation.Substring(0, projectLocation.LastIndexOf("EnergieatlasLeibnitz"));
                    //projectLocation = Directory.GetParent(projectLocation).FullName;
                    //projectLocation = Directory.GetParent(projectLocation).FullName;

                    Tile tile = new Tile(Path.Combine(projectLocation, @"EnergieatlasLeibnitz\Resources\MapTiles\Layer_15\Layer_15 [www.imagesplitter.net]-" + i + "-" + j + ".jpeg"));
                    tileArrayLayer15[i, j] = tile;
                }
            }

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string projectLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    projectLocation = projectLocation.Substring(0, projectLocation.LastIndexOf("EnergieatlasLeibnitz"));
                    //projectLocation = Directory.GetParent(projectLocation).FullName;
                    //projectLocation = Directory.GetParent(projectLocation).FullName;

                    Tile tile = new Tile(Path.Combine(projectLocation, @"EnergieatlasLeibnitz\Resources\MapTiles\Layer_16\Layer_16 [www.imagesplitter.net]-" + i + "-" + j + ".jpeg"));
                    tileArrayLayer16[i, j] = tile;
                }
            }

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    string projectLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    projectLocation = projectLocation.Substring(0, projectLocation.LastIndexOf("EnergieatlasLeibnitz"));
                    //projectLocation = Directory.GetParent(projectLocation).FullName;
                    //projectLocation = Directory.GetParent(projectLocation).FullName;

                    Tile tile = new Tile(Path.Combine(projectLocation, @"EnergieatlasLeibnitz\Resources\MapTiles\Layer_17\Layer_17 [www.imagesplitter.net]-" + i + "-" + j + ".jpeg"));
                    tileArrayLayer17[i, j] = tile;
                }
            }

            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    string projectLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    projectLocation = projectLocation.Substring(0, projectLocation.LastIndexOf("EnergieatlasLeibnitz"));
                    //projectLocation = Directory.GetParent(projectLocation).FullName;
                    //projectLocation = Directory.GetParent(projectLocation).FullName;

                    Tile tile = new Tile(Path.Combine(projectLocation, @"EnergieatlasLeibnitz\Resources\MapTiles\Layer_18\Layer_18 [www.imagesplitter.net]-" + i + "-" + j + ".jpeg"));
                    tileArrayLayer18[i, j] = tile;
                }
            }
        }
    }
}
