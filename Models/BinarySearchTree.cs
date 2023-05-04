namespace Web_Maxim_Technology_Practice.Models; 

public class BinarySearchTree {
	private Node? _root;

	public BinarySearchTree() {
		_root = null;
	}

	public void InsertString(string s) {
		foreach (var ch in s) {
			Insert(ch);
		}
	}

	private void Insert(char key) {
		_root = RecursiveInsert(_root, key);
	}

	private static Node RecursiveInsert(Node? node, char key) {
		if (node is null) {
			node = new Node(key);
			return node;
		}

		if (key < node.Key)
			node.Left = RecursiveInsert(node.Left, key);
		else
			node.Right = RecursiveInsert(node.Right, key);

		return node;
	}

	public string InorderTraversal() {
		var resultString = string.Empty;
		var stack = new Stack<Node>();
		var currentNode = _root;
		
		while (currentNode != null || stack.Count > 0) {
			while (currentNode != null) {
				stack.Push(currentNode);
				currentNode = currentNode.Left;
			}

			currentNode = stack.Pop();
			resultString += currentNode.Key;
			currentNode = currentNode.Right;
		}

		return resultString;
	}
}

public class Node {
	public readonly char Key;
	public Node? Left;
	public Node? Right;

	public Node(char symbol) {
		Key = symbol;
		Left = Right = null;
	}
}