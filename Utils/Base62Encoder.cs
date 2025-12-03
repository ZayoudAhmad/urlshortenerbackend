using System.Text;

namespace urlshortenerbackend.Utils;

public static class Base62Encoder
{
    private const string CHARS = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const int BASE = 62;

    public static string Encode(long num)
    {
        if (num < 0)
        {
            throw new ArgumentException("Input number must be non-negative.", nameof(num));
        }

        if (num == 0)
        {
            return CHARS[0].ToString();
        }

        var sb = new StringBuilder();

        while (num > 0)
        {
            int remainder = (int)(num % BASE);

            sb.Insert(0, CHARS[remainder]);

            num /= BASE;
        }

        return sb.ToString();
    }
}