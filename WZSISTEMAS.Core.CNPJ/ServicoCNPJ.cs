using WZSISTEMAS.Core.CNPJ.Interfaces;

namespace WZSISTEMAS.Core.CNPJ;

/// <summary>
/// Representa os recurso do serviço para manipular um CNPJ.
/// </summary>
public class ServicoCNPJ : IServicoCNPJ
{
    public virtual string Gerar()
    {
        var digitos = string.Empty;
        var random = new Random();

        while (digitos.Length < 12)
            digitos += random.Next(0, 9).ToString();
        
        return GerarDV(digitos);
    }

    /// <summary>
    /// Gera os dígitos verificadores do digitos do CNPJ informado.
    /// </summary>
    /// <param name="digitos">Os dígitos do CNPJ sem os digitos verificadores.</param>
    /// <returns>Um valor <see cref="string"/> representando o CNPJ com os dígitos verificadores.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="digitos"/> é nulo.</exception>
    /// <exception cref="ArgumentException"><paramref name="digitos"/> é vázio.</exception>
    /// <exception cref="ArgumentException"><paramref name="digitos"/> com comprimento incorreto.</exception>
    public virtual string GerarDV(string digitos)
    {
        ArgumentNullException.ThrowIfNull(digitos, nameof(digitos));
        ArgumentException.ThrowIfNullOrEmpty(digitos, nameof(digitos));

        return digitos.Length != 12
            ? throw new ArgumentException("O CNPJ parcial deve conter 12 digitos")
            : ComputarSegundoDigito(
            ComputarPrimeiroDigito(digitos));
    }

    /// <summary>
    /// Computa o dígito verificador do CNPJ com base no multiplicador.
    /// </summary>
    /// <param name="digitos">Os digitos do CNPJ.</param>
    /// <param name="multiplicadores">Os multiplicadores para computar o digito verificador. (12 digito para o primeiro e 13 digito para o segundo)</param>
    /// <returns>Um valor <see cref="string"/> representando o CNPJ com o dígito verificador computador.</returns>
    private static string ComputarDigito(string digitos, int[] multiplicadores)
    {
        var total = 0;

        for(var i = 0; i < multiplicadores.Length; i++)
            total += Convert.ToInt32(digitos[i].ToString()) * multiplicadores[i];

        var resto = total % 11;
        var digito = resto > 2
            ? 11 - resto
            : 0;

        return $"{digitos}{digito}";
    }

    /// <summary>
    /// Computa o primeiro dígito verificador do CNPJ.
    /// </summary>
    /// <param name="digitos">Os digitos do CNPJ.</param>
    /// <returns>Um valor <see cref="string"/> representando o CNPJ com o primeiro dígito verificador computador.</returns>
    private static string ComputarPrimeiroDigito(string digitos)
    {
        return ComputarDigito(digitos, [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);
    }

    /// <summary>
    /// Computa o segundo dígito verificador do CNPJ.
    /// </summary>
    /// <param name="digitos">Os digitos do CNPJ. (O primeiro dígito verificador deve estar presente)</param>
    /// <returns>Um valor <see cref="string"/> representando o CNPJ com o segundo dígito verificador computador.</returns>
    private static string ComputarSegundoDigito(string digitos)
    {
        return ComputarDigito(digitos, [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);
    }

    /// <summary>
    /// Realiza a validação do CNPJ informado por parâmetro.
    /// </summary>
    /// <param name="cNPJ">O CNPJ que será validado.</param>
    /// <returns>Um valor <see cref="bool"/> representando se o CNPJ é válido.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="digitos"/> é nulo.</exception>
    /// <exception cref="ArgumentException"><paramref name="digitos"/> é vázio.</exception>
    public virtual bool Validar(string cNPJ)
    {
        ArgumentNullException.ThrowIfNull(cNPJ, nameof(cNPJ));
        ArgumentException.ThrowIfNullOrEmpty(cNPJ, nameof(cNPJ));

        if (cNPJ.Length != 14)
            return false;

        var digitos = string.Empty;

        foreach (var digito in cNPJ.Take(12).ToList())
            digitos += digito.ToString();

        return GerarDV(digitos) == cNPJ;
    }
}
