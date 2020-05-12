using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshEvolutionSimulatorViewer
{
    class World
    {
        public World(uint worldSizeX, uint worldSizeY)
        {
            this.worldSizeX = worldSizeX;
            this.worldSizeY = worldSizeY;
        }
        public uint currentTick;
        public uint worldSizeX;
        public uint worldSizeY;
    }
}
