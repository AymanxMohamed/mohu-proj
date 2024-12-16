using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SDIntegraion;

public partial class ServiceDeskRequest
{
    [JsonProperty("Interaction")]
    public InteractionDto Interaction { get; set; }

    public void AdjustPhoneNumber()
    {
        Interaction.PhoneNumber = NonNumericRegex().Replace(Interaction.PhoneNumber, "");
    }

    [GeneratedRegex(@"\D")]
    private static partial Regex NonNumericRegex();
}

