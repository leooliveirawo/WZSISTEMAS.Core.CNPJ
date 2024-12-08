using WZSISTEMAS.Core.CNPJ.Interfaces;

namespace WZSISTEMAS.Core.CNPJ.Test;

#nullable disable

[TestClass]
public class ServicoCNPJTest
{
    private readonly IServicoCNPJ servicoCNPJ;

    private readonly string cNPJNulo = default;
    private readonly string cNPJVazio = string.Empty;
    private readonly string cNPJComprimentoErrado = "16921806000";
    private readonly string cNPJInvalido = "16921806000151";
    private readonly string cNPJValido = "16921806000152";

    private readonly string digitosNulo = default;
    private readonly string digitosVazio = string.Empty;
    private readonly string digitosComprimentoErrado = "16921806000";
    private readonly string digitos = "169218060001";

    public ServicoCNPJTest() : this(new ServicoCNPJ())
	{
	}

    public ServicoCNPJTest(IServicoCNPJ servicoCNPJ)
    {
        this.servicoCNPJ = servicoCNPJ;
    }

    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
	public virtual void Validar_CNPJNulo()
	{
        servicoCNPJ.Validar(cNPJNulo);
	}

    [TestMethod, ExpectedException(typeof(ArgumentException))]
    public virtual void Validar_CNPJVazio()
	{
        servicoCNPJ.Validar(cNPJVazio);
	}

    [TestMethod]
    public virtual void Validar_CNPJComprimentoErrado()
    {
        if (servicoCNPJ.Validar(cNPJComprimentoErrado))
            Assert.Fail();
    }

    [TestMethod]
    public virtual void Validar_CNPJInvalido_OK()
    {
        if (servicoCNPJ.Validar(cNPJInvalido))
            Assert.Fail();
    }

    [TestMethod]
    public virtual void Validar_CNPJValido_OK()
    {
        if (!servicoCNPJ.Validar(cNPJValido))
            Assert.Fail();
    }

    [TestMethod]
    public virtual void Gerar_OK()
    {
        if (!servicoCNPJ.Validar(servicoCNPJ.Gerar()))
            Assert.Fail();
    }

    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    public virtual void GerarDV_DigitosNulo()
    {
        servicoCNPJ.GerarDV(digitosNulo);
    }

    [TestMethod, ExpectedException(typeof(ArgumentException))]
    public virtual void GerarDV_DigitosVazio()
    {
        servicoCNPJ.GerarDV(digitosVazio);
    }

    [TestMethod, ExpectedException(typeof(ArgumentException))]
    public virtual void GerarDV_DigitosComprimentoErrado()
    {
        servicoCNPJ.GerarDV(digitosComprimentoErrado);
    }

    [TestMethod]
    public virtual void GerarDV_OK()
    {
        var cNPJ = servicoCNPJ.GerarDV(digitos);

        Assert.AreEqual(cNPJ, cNPJValido);
    }
}
