// =============================================================
// PublishCore Standard — Infrastructure Layer
// ReadingTimeCalculator.cs — Calcula tempo estimado de leitura.
// =============================================================
using System.Text.RegularExpressions;
using PublishCoreStandard.Domain.Interfaces;

namespace PublishCoreStandard.Infrastructure.Services;

public class ReadingTimeCalculator : IReadingTimeCalculator
{
    private const int WPM = 200;

    public int Calculate(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return 1;

        var plain = Regex.Replace(content, "<.*?>", " ");
        var words = plain.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

        return Math.Max(1, (int)Math.Ceiling(words / (double)WPM));
    }
}
