using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SDIntegraion;

public partial class ServiceDeskRequest
{
    [JsonProperty("Interaction")]
    public InteractionDto Interaction { get; set; }

    public void AdjustPhoneNumber()
    {
        Interaction.PhoneNumber = ConvertArabicToEnglishDigits(Interaction.PhoneNumber);
        Interaction.PhoneNumber = NonNumericRegex().Replace(Interaction.PhoneNumber, "");
    }
    
    private static string ConvertArabicToEnglishDigits(string input)
    {
        return input
            .Replace('٠', '0')
            .Replace('١', '1')
            .Replace('٢', '2')
            .Replace('٣', '3')
            .Replace('٤', '4')
            .Replace('٥', '5')
            .Replace('٦', '6')
            .Replace('٧', '7')
            .Replace('٨', '8')
            .Replace('٩', '9');
    }

    [GeneratedRegex(@"\D")]
    private static partial Regex NonNumericRegex();
}