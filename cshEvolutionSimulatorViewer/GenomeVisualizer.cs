using Microsoft.Msagl.Core.Layout;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace cshEvolutionSimulatorViewer
{
    class GenomeVisualizer
    {
        //int[] genome = new int[64];
        int pointer = 0;
        List<Command> commands = new List<Command>();
        List<int> pointerStack = new List<int> { 0 };
        List<GraphNode> nodes = new List<GraphNode>();

        public List<GraphNode> analyseGenome()
        {
            Command command = null;
            int[] genome = new int[64];
            Command commandStay = new Command("STAY", 0, new List<CommandOptions> { new CommandOptions("staying...", 1) });
            commands.Add(commandStay);
            while (pointerStack.Count > 0)
            {
                command = commands.Find(x => x.index == genome[pointerStack[0]]);
                if (command != null)
                {
                    GraphNode node = new GraphNode();
                    node.name = command.name;
                    node.index = command.index;
                    nodes.Add(node);
                    //foreach (CommandOptions option in command.options)
                    //{

                    //}
                }
                else
                {

                }

                //switch (genome[pointerStack[0]])
                //{
                //    case 0:             // command Stay

                //        break;
                //    default:
                //        break;          
                //}
            }
            return nodes;
            // 1. get genome value at index X (starts at 0)
            // 2. if it's a pointer? compare with command list
            //  yes - goto (1)
            //  no  - get command options and place them at stack
            // 3. get first item from stack and goto (1)
        }
    }

    public struct GraphNode
    {
        public string name;
        public int index;
        public List<GraphLink> links;
    }

    public struct GraphLink
    {
        public string name;
        public int target;
    }

    public class Command
    {
        public Command(string name, int index, List<CommandOptions> optionsList)
        {
            this.name = name;
            this.index = index;
            this.options = optionsList;
        }
        public string name;
        public int index;
        public List<CommandOptions> options;
    }

    public struct CommandOptions
    {
        public CommandOptions(string name, int pointerIncrement)
        {
            this.name = name;
            this.pointerIncrement = pointerIncrement;
        }
        string name;
        int pointerIncrement;
    }
}
