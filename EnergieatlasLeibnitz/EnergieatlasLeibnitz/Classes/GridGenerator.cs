using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EnergieatlasLeibnitz.Classes
{
    class GridGenerator
    {
        int rowCount;
        int columnCount;
        Grid grid;

        public GridGenerator()
        {
            grid = new Grid();
        }

        public Grid GenerateGrid(int layer)
        {
            switch(layer)
            {
                case 15: { rowCount = 3; columnCount = 4; break; }
                case 16: { rowCount = 6; columnCount = 8; break; }
                case 17: { rowCount = 12; columnCount = 16; break; }
                case 18: { rowCount = 22; columnCount = 30; break; }
            }

            for(int i = 0; i < rowCount; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int j = 0; j < columnCount; j++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            return grid;
        }
    }
}
