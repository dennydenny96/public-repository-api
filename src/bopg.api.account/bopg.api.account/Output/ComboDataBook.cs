using System.Collections.Generic;

namespace bopg.api.account.Output
{
    public class ComboDataBook : OutputBase
    {
        public ComboDataBookContent Content { get; set; }

        public ComboDataBook()
        {
            this.Content = new ComboDataBookContent();
        }
    }

    public class ComboDataBookContent
    {
        public List<ComboDataBookData> Data { get; set; }

        public ComboDataBookContent()
        {
            this.Data = new List<ComboDataBookData>();
        }
    }

    public class ComboDataBookData
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Borrowed { get; set; }
    }
}
