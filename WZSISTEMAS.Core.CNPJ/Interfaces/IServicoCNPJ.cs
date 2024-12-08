namespace WZSISTEMAS.Core.CNPJ.Interfaces;

/// <summary>
/// Expoe os recursos do serviço para manipular um CNPJ.
/// </summary>
public interface IServicoCNPJ
{
    /// <summary>
    /// Realiza a validação do CNPJ informado por parâmetro.
    /// </summary>
    /// <param name="cNPJ">O CNPJ que será validado.</param>
    /// <returns>Um valor <see cref="bool"/> representando se o CNPJ é válido.</returns>
    bool Validar(string cNPJ);
    
    /// <summary>
    /// Gera randômicamente um CNPJ.
    /// </summary>
    /// <returns>Um valor <see cref="string"/> representando o CNPJ gerado.</returns>
    string Gerar();

    /// <summary>
    /// Gera os dígitos verificadores do digitos do CNPJ informado.
    /// </summary>
    /// <param name="digitos">Os dígitos do CNPJ sem os digitos verificadores.</param>
    /// <returns>Um valor <see cref="string"/> representando o CNPJ com os dígitos verificadores.</returns>
    string GerarDV(string digitos);
}
