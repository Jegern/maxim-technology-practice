using System.Text;

namespace Maxim_Technology_Practice;

internal static class Sort {
	public static string QuickSort(string line) {
		var stringBuilder = new StringBuilder(line);
		var sortedStringBuilder = RecursiveQuickSort(stringBuilder, 0, stringBuilder.Length - 1);
		return sortedStringBuilder.ToString();
	}

	private static StringBuilder RecursiveQuickSort(StringBuilder sb, int leftIndex, int rightIndex) {
		var i = leftIndex;
		var j = rightIndex;
		var pivot = sb[leftIndex];
		while (i <= j) {
			while (sb[i] < pivot)
				i++;

			while (sb[j] > pivot)
				j--;

			if (i > j) continue;
			(sb[i], sb[j]) = (sb[j], sb[i]);
			i++;
			j--;
		}

		if (leftIndex < j)
			RecursiveQuickSort(sb, leftIndex, j);
		if (i < rightIndex)
			RecursiveQuickSort(sb, i, rightIndex);
		return sb;
	}

	public static string TreeSort(string line) {
		var bst = new BinarySearchTree();
		bst.InsertString(line);
		return bst.InorderTraversal();
	}
}