public class GeneratePassword
{
	private static Random rand = new Random();
	private static char[] symbols = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'y', 'u', 'z', 'p', '0'
	,'0','1','2','3','4','5','6','7','8','9'};
	public static string Generate(int length)
	{
		string result = "";

		for (int i = 0; i < length; i++)
		{
			result = result + symbols[rand.Next(0, symbols.Length)];
		}
		return result;
	}
}
