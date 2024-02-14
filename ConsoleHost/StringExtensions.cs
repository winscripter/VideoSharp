namespace ConsoleHost;

public static class StringExtensions
{
    public static TimeSpan ToTimeSpan(this string s)
    {
        string[] splitted = s.Split(':');

        int hours = int.Parse(splitted[0]);
        int mins = int.Parse(splitted[1]);

        string[] s_splitted = splitted[2].Split('.');

        int seconds = int.Parse(s_splitted[0]);
        int milliseconds = int.Parse(s_splitted[1]);

        return new TimeSpan(hours / 24, hours, mins, seconds, milliseconds);
    }
}
