using System.Collections.Generic;

namespace bopg.api.account.Output
{
    public class Combo : OutputBase
    {
        public ComboContent Content { get; set; }

        public Combo()
        {
            this.Content = new ComboContent();
        }
    }

    public class ComboContent
    {
        public List<ComboData> Data { get; set; }

        public ComboContent()
        {
            this.Data = new List<ComboData>();
        }
    }

    public class ComboData
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }
}
