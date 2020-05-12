using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cshEvolutionSimulatorViewer
{
    static class Program
    {
        public static MainForm mainForm;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //byte[] botGenome;

            //ChunkMessage chunk = ProtobufWorker.LoadChunkData();
            //foreach (TickMessage tick in chunk.Ticks)
            //{
            //    tick.TickIndex = tick.TickIndex;
            //    foreach (BotMessage bot in tick.Bots)
            //    {
            //        bot.Index = bot.Index;
            //        botGenome = bot.Genome.ToArray<byte>();
            //    }
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new MainForm();            
            mainForm.drawingSurface = new DrawingSurface();
            mainForm.Controls.Add(mainForm.drawingSurface);
            //mainForm.drawingSurface.PreviewKeyDown += new PreviewKeyDownEventHandler(mainForm.MoveView);
            //mainForm.drawingSurface.MouseWheel += new MouseEventHandler(mainForm.ZoomView);
            Application.Run(mainForm);
        }
    }    
}
