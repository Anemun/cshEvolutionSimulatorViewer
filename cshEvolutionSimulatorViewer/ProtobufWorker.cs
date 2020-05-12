using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cshEvolutionSimulatorViewer
{
    // .\protobuf\bin\protoc.exe --proto_path=. --csharp_out=. data.proto

    static class ProtobufWorker
    {
        public static ChunkMessage LoadChunkData()
        {
            using (var input = File.OpenRead("F:\\testData\\chunk_0.bin"))
            {
                return ChunkMessage.Parser.ParseFrom(input);
            }
        }

        public static ChunkMessage LoadNextChunk(string chunkPath, int chunkIndex)
        {
            string path = string.Format("{0}\\chunk_{1}.bin", chunkPath, chunkIndex);
            if (File.Exists(path))
            {
                using (var data = File.OpenRead(path))
                {
                    return ChunkMessage.Parser.ParseFrom(data);
                }
            }
            else
                return null;
        }

        //public static TickMessage[] UnmarshalTick(ChunkMessage chunk)
        //{
        //    return TickMessage.Parser.ParseFrom(chunk.Ticks[0]);
        //}
    }
}
