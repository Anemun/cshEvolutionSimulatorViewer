using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SFML.Graphics;
using SFML.Window;

namespace cshEvolutionSimulatorViewer
{
    public partial class MainForm : Form
    {
        Form mainForm;

        SFML.Graphics.View view;
        public DrawingSurface drawingSurface;
        RenderWindow renderwindow;
        float cameraMoveSpeed = 0.2f;
        float cameraBorder = 10;

        uint botPixelSize = 10;
        uint botPixelSpacing = 1;
        uint worldSizeX_cells;
        uint worldSizeY_cells;
        uint worldSizeX_pixels;
        uint worldSizeY_pixels;

        int tickIndex;
        int chunkIndex;
        int maxChunkIndex = 19;
        ChunkMessage currentChunk;
        ChunkMessage nextChunk;
        string chunkFolderPath;
        string chunkDefaultPath = "F:\\testData";

        SFML.System.Clock clock;
        uint drawCalls;
        uint FPS;
        uint FPScalcInterval = 3;
        float timeSinceClock;


        public MainForm()
        {
            InitializeComponent();

            mainForm = this;
            mainForm.Show();
            dialog_loadData.SelectedPath = chunkDefaultPath;
        }

        private void button_startSimulation_Click(object sender, EventArgs e)
        {
            button_startSimulation.Enabled = false;
            tickIndex = 0;
            clock = new SFML.System.Clock();
            timeSinceClock = 0f;
            drawCalls = 0;
            SFML.Graphics.Color backgroundColor = new SFML.Graphics.Color(120, 120, 120);

            if ((chunkFolderPath == null) || (currentChunk == null))
            {
                label_loadData.Text = "LOAD SOME DATA before starting";
                return;
            }
            //LoadChunks();

            drawingSurface.Parent = panel_view;
            drawingSurface.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom);
            drawingSurface.Dock = DockStyle.Fill;
            drawingSurface.PreviewKeyDown += new PreviewKeyDownEventHandler(MoveView);
            drawingSurface.MouseWheel += new MouseEventHandler(ZoomView);

            worldSizeX_cells = currentChunk.WorldSizeX;
            worldSizeY_cells = currentChunk.WorldSizeY;
            worldSizeX_pixels = worldSizeX_cells * (botPixelSize + botPixelSpacing);
            worldSizeY_pixels = worldSizeY_cells * (botPixelSize + botPixelSpacing);

            view = new SFML.Graphics.View(new FloatRect(0, 0, worldSizeX_pixels, worldSizeY_pixels));

            // initialize sfml
            renderwindow = new RenderWindow(drawingSurface.Handle); // creates our SFML RenderWindow on our surface control

            VertexArray verticesObjects;
            VertexArray verticesOrganLines;
            TickMessage tick;
            // drawing loop
            while (mainForm.Visible) // loop while the window is open
            {
                drawingSurface.Focus();
                worldSizeX_cells = currentChunk.WorldSizeX;
                worldSizeY_cells = currentChunk.WorldSizeY;
                tick = currentChunk.Ticks[tickIndex];
                UpdateStats(tick);
                verticesObjects = GetObjectVerticesForCurrentTick(tick);
                verticesOrganLines = GetOgranLinesVerticesForCurrentTick(tick);

                Application.DoEvents(); // handle form events
                renderwindow.DispatchEvents(); // handle SFML events - NOTE this is still required when SFML is hosted in another window                
                renderwindow.SetView(view);
                renderwindow.Clear(backgroundColor); // clear our SFML RenderWindow and fill it with color
                renderwindow.Draw(verticesObjects);
                renderwindow.Draw(verticesOrganLines);
                renderwindow.Display(); // display what SFML has drawn to the screen

                tickIndex++;
                if (tickIndex >= currentChunk.Ticks.Count)
                {
                    tickIndex = 0;
                    chunkIndex++;
                    LoadChunks();
                    if (currentChunk == null)
                    {
                        tickIndex = 0;
                        chunkIndex = 0;
                        LoadChunks();
                    }
                }
            }
        }        

        private VertexArray GetObjectVerticesForCurrentTick(TickMessage tick)
        {
            BotMessage[] bots = new BotMessage[tick.Bots.Count];
            tick.Bots.CopyTo(bots,0);

            VertexArray objectVertices = new VertexArray(PrimitiveType.Quads, worldSizeX_cells * worldSizeY_cells * 4);
           

            foreach (BotMessage bot in bots)
            {                
                CreateObjectVerticesForCoord(ref objectVertices, bot.CoordX, bot.CoordY, Color.Blue);
                foreach (OrganMessage organ in bot.organs) {
                    CreateObjectVerticesForCoord(ref objectVertices, organ.CoordX, organ.CoordY, Color.Red);
                }
            }
            return objectVertices;
        }

        private VertexArray GetOgranLinesVerticesForCurrentTick(TickMessage tick)
        {
             BotMessage[] bots = new BotMessage[tick.Bots.Count];
             tick.Bots.CopyTo(bots,0); 
             VertexArray organLinesVertices = new VertexArray(PrimitiveType.Lines, worldSizeX_cells * worldSizeY_cells * 2);
             foreach (BotMessage bot in bots)
              {               
                  if (bot.organs.Count > 0) 
                    foreach (OrganMessage organ in bot.organs) {
                        CreateOrganLinesVertices(ref organLinesVertices, bot, organ);
                    }
              }
              return organLinesVertices;
        }

        private void CreateObjectVerticesForCoord(ref VertexArray vertexArray, uint coordX, uint coordY, SFML.Graphics.Color color)     // TODO: create interface for bot/organ/object
        {
          uint x = coordX;
          uint y = coordY;
          uint tileIndex = x + y + y * (worldSizeX_cells - 1);  // здесь мы переводим индекс двумерого массива в индекс одномерного массива
          // так как на каждый объект нам нужно 4 индекса, мы распределяем эти индексы по массиву. 
          // Формула: [индекс объекта] * [кол-во индексов на объект-1] + [индекс объекта] + [порядковый номер индекса в объекте -1]
          uint v1 = tileIndex * 3 + tileIndex;
          uint v2 = tileIndex * 3 + tileIndex + 1;
          uint v3 = tileIndex * 3 + tileIndex + 2;
          uint v4 = tileIndex * 3 + tileIndex + 3;
          
          vertexArray[v1] = new Vertex(new SFML.System.Vector2f(x * (botPixelSpacing + botPixelSize) + botPixelSpacing, 
                                                                y * (botPixelSpacing + botPixelSize) + botPixelSpacing), 
                                                                new SFML.Graphics.Color(Color.Blue));
          vertexArray[v2] = new Vertex(new SFML.System.Vector2f(x * (botPixelSpacing + botPixelSize) + botPixelSpacing + botPixelSize, 
                                                                y * (botPixelSpacing + botPixelSize) + botPixelSpacing), 
                                                                new SFML.Graphics.Color(Color.Blue));
          vertexArray[v3] = new Vertex(new SFML.System.Vector2f(x * (botPixelSpacing + botPixelSize) + botPixelSpacing + botPixelSize, 
                                                                y * (botPixelSpacing + botPixelSize) + botPixelSpacing + botPixelSize), 
                                                                new SFML.Graphics.Color(Color.Blue));
          vertexArray[v4] = new Vertex(new SFML.System.Vector2f(x * (botPixelSpacing + botPixelSize) + botPixelSpacing, 
                                                                y * (botPixelSpacing + botPixelSize) + botPixelSpacing + botPixelSize), 
                                                                new SFML.Graphics.Color(Color.Blue));
        }

        private void CreateOrganLinesVertices(ref VertexArray vertexArray, BotMessage bot, OrganMessage organ)
        {
          uint x1 = bot.CoordX;
          uint x2 = organ.CoordX;
          uint y1 = bot.CoordY;
          uint y2 = organ.CoordY;
          uint arrayIndex = x2 + y2 + y2 * (worldSizeX_cells - 1);          
          uint v1 = arrayIndex + arrayIndex;
          uint v2 = arrayIndex + arrayIndex + 1;

          vertexArray[v1] = new Vertex(new SFML.System.Vector2f(x1 * (botPixelSpacing + botPixelSize) + botPixelSpacing + botPixelSize/2, 
                                                                y1 * (botPixelSpacing + botPixelSize) + botPixelSpacing + botPixelSize/2),
                                                                new SFML.Graphics.Color(Color.Black));
          vertexArray[v2] = new Vertex(new SFML.System.Vector2f(x2 * (botPixelSpacing + botPixelSize) + botPixelSpacing + botPixelSize/2, 
                                                                y2 * (botPixelSpacing + botPixelSize) + botPixelSpacing + botPixelSize/2),
                                                                new SFML.Graphics.Color(Color.Black));
        }

        public void MoveView(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    view.Move(new SFML.System.Vector2f(0, view.Size.Y * cameraMoveSpeed));                    
                    break;
                case Keys.Up:
                    view.Move(new SFML.System.Vector2f(0, view.Size.Y * -cameraMoveSpeed));
                    break;
                case Keys.Left:
                    view.Move(new SFML.System.Vector2f(view.Size.X * -cameraMoveSpeed, 0));
                    break;
                case Keys.Right:
                    view.Move(new SFML.System.Vector2f(view.Size.X * cameraMoveSpeed, 0));
                    break;
            }
            CheckCameraBorders();
        }

        public void ZoomView(object sender, MouseEventArgs e)
        {   
            if (e.Delta > 0)
            {
                if ((view.Size.X < 10) || (view.Size.Y < 10))
                    return;
                view.Zoom(0.95f);
            }
            if (e.Delta < 0)
            {
                if ((view.Size.Y > worldSizeY_pixels) && (view.Size.X > worldSizeX_pixels))
                    return;
                view.Zoom(1.05f);
            }
            CheckCameraBorders();

        }

        private void CheckCameraBorders()
        {
            if ((view.Center.X + view.Size.X/2) > worldSizeX_pixels + cameraBorder)
            {
                view.Center = new SFML.System.Vector2f(worldSizeX_pixels - (view.Size.X / 2) + cameraBorder, view.Center.Y);
            }
            if ((view.Center.Y + view.Size.Y/2) > worldSizeY_pixels + cameraBorder)
            {
                view.Center = new SFML.System.Vector2f(view.Center.X, worldSizeY_pixels - (view.Size.Y / 2) + cameraBorder);
            }
            if ((view.Center.X - view.Size.X/2) < 0 - cameraBorder)
            {
                view.Center = new SFML.System.Vector2f(0 + (view.Size.X / 2) - cameraBorder, view.Center.Y);
            }
            if ((view.Center.Y - view.Size.Y/2) < 0 - cameraBorder)
            {
                view.Center = new SFML.System.Vector2f(view.Center.X, 0 + (view.Size.Y / 2) - cameraBorder);
            }
        }

        private void UpdateStats(TickMessage tick)
        {
            label_tickIndex.Text = (tickIndex + currentChunk.Ticks.Count*chunkIndex).ToString();
            label_botCount.Text = tick.Bots.Count.ToString();
            label_worldSize.Text = currentChunk.WorldSizeX + ":" + currentChunk.WorldSizeY;

            drawCalls++;
            label_tps.Text = FPS.ToString();

            timeSinceClock += clock.Restart().AsSeconds();
            if (timeSinceClock >= FPScalcInterval)
            {
                timeSinceClock = 0f;
                FPS = drawCalls / FPScalcInterval;
                drawCalls = 0;
            }
        }

        private void LoadChunks()
        {
            currentChunk = ProtobufWorker.LoadNextChunk(chunkFolderPath, chunkIndex);            
        }

        private void button_setPath_Click(object sender, EventArgs e)
        {
            if (dialog_loadData.ShowDialog() == DialogResult.OK)
            {
                label_loadData.Text = "loading data...";
                label_loadData.Update();
                chunkFolderPath = dialog_loadData.SelectedPath;
                LoadChunks();
                label_loadData.Text = "Data loaded: " + dialog_loadData.SelectedPath;
                button_startSimulation.Enabled = true;
            }
        }

        private void button_showGenome_Click(object sender, EventArgs e)
        {
            GenomeVisualizer genomeVisualizer = new GenomeVisualizer();
            List<GraphNode> nodes = genomeVisualizer.analyseGenome();

            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer(); 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            foreach (GraphNode node in nodes)
            {
                graph.AddNode(node.genomePointer.ToString());
                foreach (GraphLink link in node.links) {
                  graph.AddEdge(link.origin.ToString(), link.target.ToString());
                }                
            }
            graph.Attr.MinimalWidth = 100;
            graph.Attr.MinimalHeight = 100;
            viewer.Graph = graph;
            viewer.Dock = DockStyle.Fill;
            viewer.Parent = panel_view;
        }

        #region testCode
        private VertexArray GetVertices(uint wSizeX, uint wSizeY)
        {
            Random random = new Random();

            //uint width = wd;
            //uint height = ht;
            uint quadSize = botPixelSize;
            uint spacing = botPixelSpacing;

            //uint wCount = width / (quadSize + spacing);
            //uint hCount = height / (quadSize + spacing);
            uint wCount = wSizeX;
            uint hCount = wSizeY;

            VertexArray vertices = new VertexArray(PrimitiveType.Quads, wCount * hCount * 4);

            for (uint h = 0; h < hCount; h++)
                for (uint w = 0; w < wCount; w++)
                {
                    int a = (int)(h + w);
                    if (a > 255) { a = 255; }
                    byte rnd = (byte)random.Next(a, 255);
                    uint tileIndex = w + h + h * (wCount - 1);
                    uint v1 = tileIndex * 3 + tileIndex;
                    uint v2 = tileIndex * 3 + tileIndex + 1;
                    uint v3 = tileIndex * 3 + tileIndex + 2;
                    uint v4 = tileIndex * 3 + tileIndex + 3;
                    vertices[v1] = new Vertex(new SFML.System.Vector2f(w * (spacing + quadSize) + spacing, h * (spacing + quadSize) + spacing), new SFML.Graphics.Color((byte)rnd, (byte)rnd, (byte)rnd));
                    vertices[v2] = new Vertex(new SFML.System.Vector2f(w * (spacing + quadSize) + spacing + quadSize, h * (spacing + quadSize) + spacing), new SFML.Graphics.Color((byte)rnd, (byte)rnd, (byte)rnd));
                    vertices[v3] = new Vertex(new SFML.System.Vector2f(w * (spacing + quadSize) + spacing + quadSize, h * (spacing + quadSize) + spacing + quadSize), new SFML.Graphics.Color((byte)rnd, (byte)rnd, (byte)rnd));
                    vertices[v4] = new Vertex(new SFML.System.Vector2f(w * (spacing + quadSize) + spacing, h * (spacing + quadSize) + spacing + quadSize), new SFML.Graphics.Color((byte)rnd, (byte)rnd, (byte)rnd));
                }

            return vertices;
        }
        private void VerticesTest()
        {
            Random random = new Random();
            SFML.System.Clock clock = new SFML.System.Clock();
            float timeSinceClock = 0f;

            uint drawCalls = 0;
            uint FPS = 0;
            uint width = 200;
            uint height = 130;
            uint quadSize = 5;
            uint spacing = 1;

            uint wCount = width / (quadSize + spacing);
            uint hCount = height / (quadSize + spacing);

            RenderWindow window = new RenderWindow(new VideoMode(width + 100, height + 100), "Vertices Test");
            window.SetVerticalSyncEnabled(false);
            window.SetFramerateLimit(150);
            //window.Closed += new EventHandler(OnClose);

            SFML.Graphics.Font font = new SFML.Graphics.Font("arial.ttf");
            SFML.Graphics.Text text = new Text(drawCalls.ToString(), font)
            {
                CharacterSize = 24,
                FillColor = SFML.Graphics.Color.Red,
                Position = new SFML.System.Vector2f(0, height)
            };

            VertexArray vertices = new VertexArray(PrimitiveType.Quads, wCount * hCount * 4);

            while (window.IsOpen)
            {

                //SFML.Graphics.Color color = SFML.Graphics.Color.Yellow;
                SFML.Graphics.Color color = new SFML.Graphics.Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));



                for (uint h = 0; h < hCount; h++)
                    for (uint w = 0; w < wCount; w++)
                    {
                        uint tileIndex = w + h + h * (wCount - 1);
                        uint v1 = tileIndex * 3 + tileIndex;
                        uint v2 = tileIndex * 3 + tileIndex + 1;
                        uint v3 = tileIndex * 3 + tileIndex + 2;
                        uint v4 = tileIndex * 3 + tileIndex + 3;
                        vertices[v1] = new Vertex(new SFML.System.Vector2f(w * (spacing + quadSize) + spacing, h * (spacing + quadSize) + spacing), new SFML.Graphics.Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));
                        vertices[v2] = new Vertex(new SFML.System.Vector2f(w * (spacing + quadSize) + spacing + quadSize, h * (spacing + quadSize) + spacing), new SFML.Graphics.Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));
                        vertices[v3] = new Vertex(new SFML.System.Vector2f(w * (spacing + quadSize) + spacing + quadSize, h * (spacing + quadSize) + spacing + quadSize), new SFML.Graphics.Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));
                        vertices[v4] = new Vertex(new SFML.System.Vector2f(w * (spacing + quadSize) + spacing, h * (spacing + quadSize) + spacing + quadSize), new SFML.Graphics.Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));
                    }



                //vertices[0] = new Vertex(new SFML.System.Vector2f(8, 8), SFML.Graphics.Color.Yellow);
                //vertices[1] = new Vertex(new SFML.System.Vector2f(16, 8), SFML.Graphics.Color.Yellow);
                //vertices[2] = new Vertex(new SFML.System.Vector2f(16, 16), SFML.Graphics.Color.Yellow);
                //vertices[3] = new Vertex(new SFML.System.Vector2f(8, 16), SFML.Graphics.Color.Yellow);
                //vertices[4] = new Vertex(new SFML.System.Vector2f(18, 8), SFML.Graphics.Color.Blue);
                //vertices[5] = new Vertex(new SFML.System.Vector2f(26, 8), SFML.Graphics.Color.Blue);
                //vertices[6] = new Vertex(new SFML.System.Vector2f(26, 16), SFML.Graphics.Color.Blue);
                //vertices[7] = new Vertex(new SFML.System.Vector2f(18, 16), SFML.Graphics.Color.Blue);


                window.DispatchEvents();
                window.Clear();
                window.Draw(vertices);
                window.Draw(text);
                window.Display();
                drawCalls++;
                text.DisplayedString = "FPS: " + FPS.ToString();

                timeSinceClock += clock.Restart().AsSeconds();
                if (timeSinceClock >= 1)
                {
                    timeSinceClock = 0f;
                    FPS = drawCalls;
                    drawCalls = 0;
                }
            }

            //uint w = 10;
            //uint h = 10;
            //VertexArray map = new VertexArray(PrimitiveType.Quads, w*h);
            //for (uint i = 0; i < w; i++)
            //    for (uint j = 0; j < h; j++)
            //    {
            //        Vertex cell = map[i + j * w]
            //        map[i+j*w] = new Vertex()
            //    }
        }

        #endregion
    }

    public class DrawingSurface : Control
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            // don't call base.OnPaint(e) to prevent forground painting
            // base.OnPaint(e);
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // don't call base.OnPaintBackground(e) to prevent background painting
            //base.OnPaintBackground(pevent);
        }
    }
}
