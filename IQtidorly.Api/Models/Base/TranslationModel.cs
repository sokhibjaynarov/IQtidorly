namespace IQtidorly.Api.Models.Base
{
    public class TranslationModel
    {
        public string uz_UZ { get; set; } // Uzbek
        public string ru_RU { get; set; } // Russian
        public string en_US { get; set; } // English
        public string kaa_UZ { get; set; } // Karakalpak

        public string GetTranslation(string language)
        {
            return language switch
            {
                "uz_UZ" => uz_UZ,
                "ru_RU" => ru_RU,
                "en_US" => en_US,
                "kaa_UZ" => kaa_UZ,
                _ => en_US // Default to English if no match
            };
        }
    }
}
