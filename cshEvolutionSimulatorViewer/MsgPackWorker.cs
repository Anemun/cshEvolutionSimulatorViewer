//using System;
//using System.IO;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using SimpleMsgPack;

//namespace cshEvolutionSimulatorViewer
//{
//    struct BotPack
//    {
//        byte coordX;
//        byte coordY;
//        uint energy;
//    }

//    struct TickPack
//    {
//        UInt64 tickIndex;
//        BotPack[] botPacks;
//    }

//    struct ChunkPack
//    {
//        uint worldSizeX;
//        uint worldSizeY;
//        uint chunkIndex;
//        TickPack[] tickPacks;
//    }

//    static class MsgPackWorker
//    {
//        public static void LoadFromDisk()
//        {
//            MsgPack msgPack = new MsgPack();
//            byte[] data = File.ReadAllBytes("F:\\testData\\chunk_0.bin");
//            msgPack.DecodeFromBytes(data);
//        }
//    }
//}
