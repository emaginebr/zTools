using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace zTools.Domain.Utils
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            // 1. Remove acentos
            var normalized = texto.Normalize(NormalizationForm.FormD);
            var semAcentos = new StringBuilder();
            foreach (var c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    semAcentos.Append(c);
            }

            var resultado = semAcentos.ToString().Normalize(NormalizationForm.FormC);

            // 2. Substitui barras por hífens (ex: CI/CD -> CI-CD)
            resultado = resultado.Replace("/", "-");

            // 3. Remove caracteres especiais
            resultado = Regex.Replace(resultado, @"[^a-zA-Z0-9\s-]", "");

            // 4. Substitui espa�os e m�ltiplos h�fens por um �nico h�fen
            resultado = Regex.Replace(resultado, @"\s+", "-"); // espa�os por h�fen
            resultado = Regex.Replace(resultado, @"-+", "-");  // m�ltiplos h�fens por um

            // 5. Converte para min�sculas
            return resultado.ToLowerInvariant().Trim('-');
        }
    }
}
