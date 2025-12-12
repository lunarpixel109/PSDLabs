using GameCode;

namespace PSDLabs;

public class Node {
    public int x { get; set; }
    public int y { get; set; }
    public Node parentNode { get; set; }

    public Node(int x, int y) { //: this(x, y, 0), meaning that this a way of storing the next up or past positions and there parent so the path can be retraced 
        this.x = x;
        this.y = y;
        parentNode = null;
    }
}

public class Pathfinding {
    private static char[,] _maze = MazeGame.Maze; //A stored version of the maze so the pathfinding algorithm knows the maze already
    private static int[,] _directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } }; //List of directions that allow the pathfinding to 

    public static List<Node>? FindPath(Node start, Node end) {
        Queue<Node> queue = new Queue<Node>();
        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        
        queue.Enqueue(start);//adds the first point to the QUEUE meaning the source [ in this case enemy gameobject ]
        visited.Add((start.x, start.y));// adds gameoject coords to the visited area
        
        while (queue.Count > 0) {
            
            Node current = queue.Dequeue();//grabs the first item in the QUEUE and returns it
            if (current.x == end.x && current.y == end.y) { //if the current node in the path is the desired location it will the reconstruct the path to the location for the gameobject to follow 
                return ReconstructPath(current);
            }

            for (int i = 0; i < _directions.GetLength(0); i++) {
                int newX = current.x + _directions[i, 0];
                int newY = current.y + _directions[i, 1];
                
                if (IsWalkable(newX, newY) && !visited.Contains((newX, newY))) { //if the direction picked is a valid position & the path has not been visited it adds it to the path as the next step
                    Node neighbor = new Node(newX, newY) {
                        parentNode = current
                    }; 
                    
                    queue.Enqueue(neighbor);
                    visited.Add((newX, newY));//adds the position chosen to the visited list so the path wont go there again 
                }
            }
        }

        return null;
    }

    public static bool IsWalkable(int x, int y) {
        return _maze[y, x] == ' ';
    } 

    private static List<Node> ReconstructPath(Node node) {
        List<Node> path = new List<Node>();
        while (node != null) {
            path.Add(node);
            node = node.parentNode;
        } 
        path.Reverse();
        return path;
    }
}