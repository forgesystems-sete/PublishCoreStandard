// =============================================================
// PublishCore Standard — Domain Layer
// IReadingTimeCalculator.cs — Contrato para cálculo de tempo de leitura.
// =============================================================

namespace PublishCoreStandard.Domain.Interfaces;

/// <summary>Define o cálculo de tempo estimado de leitura.</summary>
public interface IReadingTimeCalculator
{
    int Calculate(string content);
        /// <summary>Calcula o tempo de leitura baseado no conteúdo.</summary>
}

