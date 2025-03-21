using AgileObjects.AgileMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRestAPI.Shared.Utils
{
    public static class ExtensionsUtils
    {

        /// <summary>
        /// Captaliza uma string, isto é, deixa toda primeira letra de cada palavra maiúscula.
        /// </summary>
        /// <param name="value">String a ser captalizada.</param>
        /// <returns>Retorna a string captalizada.</returns>
        public static string ToCapitalize(this string value)
        {
            if (string.IsNullOrEmpty(value)) { return value; }
            var result = value.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + value.Substring(1, value.Length - 1);
            return result;
        }

        /// <summary>
        /// Clona um objeto.
        /// </summary>
        /// <param name="source">Objeto a ser clonado.</param>
        /// <returns>Obtem o objeto clonado.</returns>
        public static TSource ToClone<TSource>(this TSource source)
        {
            try
            {
                var result = Mapper.DeepClone(source);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao clonar objeto {nameof(TSource)}. {ex.Message}");
            }
        }

        /// <summary>
        /// Mapea uma entidade para padrão json.
        /// </summary>
        /// <param name="entity">Entitdade de a ser mapeada</param>
        /// <returns>Obtem a entidade mapeada para json.</returns>
        public static string ToJson<TEntity>(this TEntity entity)
        {
            try
            {
                if (entity == null) return null;
                var json = JsonConvert.SerializeObject(entity);
                return json;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao serializado objeto {nameof(TEntity)}. {ex.Message}");
            }
        }

        /// <summary>
        /// Gera um hash padrão MD5 
        /// </summary>
        /// <param name="value">String que sera gerada o hash.</param>
        /// <returns>Retorna um hash MD5.</returns>
        public static string ToMd5(this string value)
        {
            try
            {
                string strHash = String.Empty;

                for (int j = 0, len = value.Length; j < len; j++)
                {
                    strHash += (int)value[j];
                }

                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] hash = Encoding.UTF8.GetBytes(strHash);
                byte[] result = md5.ComputeHash(hash);
                strHash = String.Empty;
                for (int i = 0, len = result.Length; i < len; i++)
                {
                    strHash += result[i].ToString("x2");
                }
                return strHash;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar hash MD5. {ex.Message}");
            }
        }

        /// <summary>
        /// Gera um hash padrão SHA256 
        /// </summary>
        /// <param name="value">String que sera gerada o hash.</param>
        /// <returns>Retorna um hash SHA256.</returns>
        public static string HashSHA256(this string input)
        {
            using (SHA256 hasher = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = hasher.ComputeHash(Encoding.Unicode.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("X2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        /// <summary>
        /// Valida um cnpj se é verdadeiro ou não.
        /// </summary>
        /// <param name="value">valor a ser validado.</param>
        /// <returns></returns>
        public static bool IsCpf(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            value = value.Trim();
            value = value.Replace(".", "").Replace("-", "");
            if (value.Length != 11)
                return false;
            tempCpf = value.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            var result = value.EndsWith(digito);
            return result;
        }

        public static string RemovePunctuation(this string value)
        {
            return value.Trim().Replace(".", "").Replace("-", "").Replace(",", "").Replace("/", ""); ;
        }

        /// <summary>
        /// Valida um cnpj se é verdadeiro ou não.
        /// </summary>
        /// <param name="value">valor a ser validado.</param>
        /// <returns></returns>
        public static bool IsCnpj(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            value = value.Trim();
            value = value.Replace(".", "").Replace("-", "").Replace("/", "");
            if (value.Length != 14)
                return false;
            tempCnpj = value.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            var result = value.EndsWith(digito);
            return result;
        }
    }

}
