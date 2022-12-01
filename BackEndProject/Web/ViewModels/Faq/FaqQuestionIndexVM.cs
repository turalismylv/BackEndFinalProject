namespace Web.ViewModels.Faq
{
    public class FaqQuestionIndexVM
    {

        public Core.Entities.FaqCategory FaqCategorie { get; set; }
        public List<Core.Entities.FaqQuestion> FaqQuestions { get; set; }

    }
}
