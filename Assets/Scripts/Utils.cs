using UnityEngine;
using System.Collections;

public class Utils {

    public static string toSerialColor(string color)
    {
		if (color.Length == 0) {
			return string.Format("R{0:000}G{1:000}B{2:000}", 0, 0, 0);
		}
        if (color.Substring(0, 1).Equals("#")) {
            color = color.Substring(1, 6);
        }
        int r = int.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        int g = int.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        int b = int.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);

        return string.Format("R{0:000}G{1:000}B{2:000}", r, g, b);
    }
}
