namespace TranslateAPI.Entities
{
    public class Translate
    {
        public int TranslateID { get; set; }
        public int UserID { get; set; }
        public string inpLanguage { get; set; }
        public string outLanguage { get; set; }
        public string Input { get; set; }
        public string? Result { get; set; }
        public string? TranslateCode { get; set; }
        public DateTime? TimeTranslates { get; set; }
        public virtual User? User { get; set; }
    }
}
