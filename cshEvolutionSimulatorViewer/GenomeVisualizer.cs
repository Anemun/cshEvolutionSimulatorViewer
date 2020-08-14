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
        List<Command> commands;         
        List<GraphNode> nodes;
        List<int> pointerStack;

        public List<GraphNode> analyseGenome()
        {
            DefineCommands();
            nodes = new List<GraphNode>();
            pointerStack = new List<int> { 0 };                     // начинаем стэк с нулевого указателя
            Command command = null;
            int[] genome = new int[64];            
            int i = 0;
            while ((pointerStack.Count > 0) && (i < 128))
            {
                int currentPointer = pointerStack[0];                                         // берём первый указатель из стэка указателей
                command = commands.Find(x => x.genomeValue == genome[currentPointer]);    
                // читаем значение генома под этим указателем. Значение либо определяет команду и двигает указатель, либо просто двигает указатель
                if (command != null)    // если такая команда есть
                {                    
                    GraphNode node = new GraphNode();     // создаём новую ноду для графика
                    node.name = command.name;
                    node.genomePointer = currentPointer;                    
                    foreach (CommandOptions option in command.options)        // смотрим, какие есть варианты исполнения у этой команды
                    {
                      // для каждого варианта исполнения мы делаем новую связь для ноды на графике
                      GraphLink link = new GraphLink();
                      link.origin = currentPointer;
                      link.name = option.name                      
                      //SearchTargetThroughPointers(genome, currentPointer, option.pointerIncrement, ref node, ref link)     

                      // если цель варианта исполнения содержит команду, а не просто указатель
                      if (commands.Find(x => x.genomeValue == genome[LoopValue(currentPointer+option.pointerIncrement, 0, 64)]) != null)          
                      {
                        link.target = LoopValue(currentPointer+option.pointerIncrement, 0, 64);     
                      }
                      // если указатель, то ищем следующую команду по это указателю
                      else 
                      {
                        int newPointer = FindPointerForNextCommand(genome, currentPointer, option.pointerIncrement);
                        
                        if (newPointer == -1) {continue;}
                        link.target = newPointer;                                      
                      }
                      // если команда таки нашлась, то прикрепляем к ней связь и записываем указатель на команду в стэк
                      if (nodes.Find(x => x.genomePointer == link.target) == null)
                      { 
                        pointerStack.Add(link.target);
                      }
                      // Если среди нод уже есть нода с таким указателем, то в стэк не кладём, просто отмечаем связь.   
                      node.links.Add(link);
                    }
                    nodes.Add(node);
                }
                // если первый указатель с которого мы начинаем, не содержит команды
                else
                {
                  // то ищем по указателям ближайшую комаду
                  int newPointer = FindPointerForNextCommand(genome, currentPointer);
                        
                  if (newPointer != -1) 
                  {   
                      // если находим, то добавляем её в стэк
                      pointerStack.Add(newPointer);       
                  } 
                }
                pointerStack.Remove(0);
                i++;
            }
            return nodes;
        }

        private int FindPointerForNextCommand(int[] genome, int currentPointer, int increment = 0)
        {          
          int tempPointer = LoopValue(currentPointer + genome[LoopValue(currentPointer+increment, 0, 64)], 0, 64);
          // то мы плюсуем указатель варианта к текущему указателю и смотрим снова. Так n раз максимум.
          int j = 0;
          while ((commands.Find(x => x.genomeValue == genome[tempPointer]) == null) && (j < 100))
          {
            tempPointer = LoopValue(tempPointer + genome[tempPointer], 0, 64);
            j++;
          }          
          if (commands.Find(x => x.genomeValue == genome[tempPointer]) != null)   
          {
              return tempPointer;
          }
          return -1;
        }

        private void DefineCommands()      // TODO: Переделать в загрузку из текстового файла или что-то такое
        {
            commands = new List<Command>();
            Command commandStay = new Command("STAY", 0, new List<CommandOptions> { new CommandOptions("staying...", 1) });
            commands.Add(commandStay);
        }

        // private void SearchTargetThroughPointers(int[] genome, int currentPointer, int optionIncrement, ref GraphNode node, ref GraphLink link)
        // {
        //     // если цель варианта исполнения не содержит команды, а просто указатель
        //     if (commands.Find(x => x.genomeValue == genome[currentPointer+optionIncrement]) == null)          // TODO: LOOP genome here
        //     {
        //         int j = 0;
        //         int tempPointer = currentPointer + genome[currentPointer+optionIncrement];
        //         // то мы плюсуем указатель варианта к текущему указателю и смотрим снова. Так n раз максимум.
        //         while ((commands.Find(x => x.genomeValue == genome[tempPointer]) == null) && (j < 100))     // TODO: LOOP genome here
        //         {
        //           tempPointer = tempPointer + genome[tempPointer];    // TODO: LOOP genome here
        //           j++;
        //         }
        //         // если команда таки нашлась, то прикрепляем к ней связь и записываем указатель на команду в стэк
        //         if (commands.Find(x => x.genomeValue == genome[tempPointer]) != null)   
        //         {
        //             link.target = tempPointer;
        //             link.name += "(" + j + ")";
        //             if (nodes.Find(x => x.genomePointer == link.target) == null)
        //             { 
        //               pointerStack.Add(link.target);
        //             }   
        //             node.links.Add(link);
        //         }
        //         // если команда так и не нашлась (потенциально бесконечный цикл), то обрываем и идём к следующему варианту исполения                
        //     }
        //     // если цель вариант исполнения содержит команду, записываем указатель на неё в стэк. Если среди нод уже есть нода с таким указателем, то в стэк не кладём, просто отмечаем связь.
        //     else 
        //     {
        //         link.target = currentPointer + optionIncrement;           // TODO: LOOP genome here                          
        //         if (nodes.Find(x => x.genomePointer == link.target) == null)
        //         { 
        //           pointerStack.Add(link.target);
        //         }   
        //         node.links.Add(link);                   
        //     }
        // } 
        private int LoopValue(int value, int min, int max)
        {
          // LoopValue returns value, looped between min (included) and max (not included)
          // LoopValue for example, val=6, min=0, max=10 returns 6
          // LoopValue for example, val=0, min=0, max=10 returns 0
          // LoopValue for example, val=10, min=0, max=10 returns 0
          // LoopValue for example, val=11, min=0, max=10 returns 1
          // LoopValue for example, val=12, min=-10, max=10 returns -8
          // LoopValue for example, val=5, min=10, max=15 returns 

          if (min == max) {
            return min;
          }

          if (value < min) {
            //throw new Exception e {"Not implemented"};
            //value = max - (value % max)
          }

          if (value >= max) {
            value = min + (value % max);
          }

          return value;
        }       
    }

    

    public class GraphNode
    {
        public string name;
        public int genomePointer;
        public List<GraphLink> links;
    }

    public struct GraphLink
    {
        public string name;
        public int origin;
        public int target;
    }

    public class Command
    {
        public Command(string name, int genomeValue, List<CommandOptions> optionsList)
        {
            this.name = name;
            this.genomeValue = genomeValue;
            this.options = optionsList;
        }
        public string name;
        public int genomeValue;
        public List<CommandOptions> options;
    }

    public struct CommandOptions
    {
        public CommandOptions(string name, int pointerIncrement)
        {
            this.name = name;
            this.pointerIncrement = pointerIncrement;
        }
        public string name;
        public int pointerIncrement;
    }
}
