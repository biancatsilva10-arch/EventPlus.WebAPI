namespace EventPlus.WebAPI.Utils
{
    public static class Criptografia
    {
        //metodo estatico ultilitario responsavel pelas operações de criptografia e hashing de senhas na API

        public static string GerarHsh(string senha)///admin1234
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
                                                   //admin123 
        }

        public static bool CompararHash(string senhaInformada, string senhaBanco) { 
        
        if (string.IsNullOrEmpty(senhaInformada) || string.IsNullOrEmpty(senhaBanco))
            {
                return false;
            }

            try
            {
            return BCrypt.Net.BCrypt.Verify(senhaInformada, senhaBanco);

            }
            catch (BCrypt.Net.SaltParseException)
            {

                return false;
            }
        
        }
    }
}
