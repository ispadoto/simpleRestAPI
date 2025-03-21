namespace SimpleRestAPI.Shared.Constants
{
    public static class ValidationsConstants
    {
        public static class Messages
        {
            public const string FieldMustBeProvided = "{0} - Campo de preenchimento Obrigatório!";
            public const string MaxLength = "{0} deve ter no máximo {1} caractere(s)";
            public const string FieldMustNotContainSpecialCharacter = "{0} não pode conter caracteres especiais";
            public const string GreaterThanDateTime = "{0} - a data não pode ser maior que {1:dd/MM/yyyy}";
            public const string InvalidSize = "{0} - tamanho inválido, máximo permitido {1}";
            public const string MinLength = "{0} deve ter {1} caractere(s)";
        }

        public static class MaxSize
        {
            public const decimal Decimal83 = 99999.999m;
        }
    }
}
