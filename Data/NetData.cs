using System.Net.Sockets;
using VisNetwork.Blazor.Models;
using VisNetwork.Blazor;
using System.Text;

namespace LearningMaps.Data
{
    public class NetData
    {
        HttpClient GrphhttpClient;
        Project graphproject;
       
        public event Action OnChange;

        
        public Network network;
        public NetworkData data;
        public List<Node> nodes = new List<Node>();
        public List<Edge> edges = new List<Edge>();
        public string layoutDirection = "LR";
        public string canvasColor = "#e8c6c8";
        public string nodeColor = "#D2E5FF";
        public string fontColor = "#000000";

        public NetworkOptions EditorConstructionOptions(Network network)
        {

            return new NetworkOptions
            {
                Layout = new LayoutOptions()
                {
                    Hierarchical = new HierarchicalOptions()
                    {
                        Direction = layoutDirection,
                        SortMethod = "directed",
                        ShakeTowards = "leaves",

                        LevelSeparation = 300
                    }
                },

                Physics = new PhysicsOptions()
                {
                    //Enabled = false
                    BarnesHut = new BarnesHutOption()
                    {

                        AvoidOverlap = 1,
                        Damping = 1,
                    }
                }

            };
        }


        public string SplitTextToFitWidth(string text, int maxWidth)
        {
            if (string.IsNullOrEmpty(text) || maxWidth <= 0)
            {
                return string.Empty;
            }

            if (text.Length < maxWidth)
            {
                return CenterTextWithSpaces(text, maxWidth);
            }
            else
            {
                string[] words = text.Split(' ');
                string result = string.Empty;
                string currentLine = string.Empty;

                foreach (string word in words)
                {
                    string tempLine = currentLine + " " + word;

                    if (GetStringWidth(tempLine) <= maxWidth)
                    {
                        currentLine = tempLine;
                    }
                    else
                    {
                        result += currentLine.Trim() + Environment.NewLine;
                        currentLine = word;
                    }
                }

                result += currentLine.Trim();
                return result;
            }



        }

        private int GetStringWidth(string text)
        {
            // Calculate the width of the text in pixels using your desired method
            // For simplicity, this example returns the length of the text as the width
            return text.Length;
        }


        public string CenterTextWithSpaces(string text, int desiredLength)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            // Remove any existing spaces from the text
            text = text.Trim();

            int numSpacesToAdd = desiredLength - text.Length;

            if (numSpacesToAdd <= 0)
                return text;

            int leftSpaces = numSpacesToAdd / 2;
            int rightSpaces = numSpacesToAdd - leftSpaces;

            StringBuilder result = new StringBuilder();

            // Add left spaces
            for (int i = 0; i < leftSpaces; i++)
            {
                result.Append(" ");
            }

            result.Append(text);

            // Add right spaces
            for (int i = 0; i < rightSpaces; i++)
            {
                result.Append(" ");
            }

            return result.ToString();
        }


        

        public void NotifyStateChanged() => OnChange?.Invoke();
    }
}
