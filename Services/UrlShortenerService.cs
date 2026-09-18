namespace minimal_api_shorterUrl.Services
{
    public class UrlShortenerService
    {
        // valid characters
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly Random _random;

        public string GenerateCode()
        {
            var codeChars = new char[6];
            for (int i = 0; i < 6; i++)
            {
                // choose random position
                int randomIndex = _random.Next(Alphabet.Length);
                codeChars[i] = Alphabet[randomIndex];
            }

            // array to string
            return new string(codeChars);
        }

    }
}