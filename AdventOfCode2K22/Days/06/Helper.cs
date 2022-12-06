namespace Day6;

public static class Helper {
    public static int Trace(string input, int size) {
        var temp = "";
        var marker = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (temp.Length < size) {
                temp += input[i];
            } else {
                temp = temp.Substring(1, size - 1) + input[i];
            }
            if (temp.Distinct().Count() == size) {
                marker = i + 1;
                break;
            }
        }
        return marker;
    }
}