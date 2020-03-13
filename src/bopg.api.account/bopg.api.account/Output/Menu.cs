using System;
using System.Collections.Generic;

namespace bopg.api.account.Output
{
    public class Menu : OutputBase
    {
        public MenuContent Content { get; set; }

        public Menu()
        {
            this.Content = new MenuContent();
        }
    }

    public class MenuContent
    {
        public List<SectionData> Data { get; set; }

        public MenuContent()
        {
            this.Data = new List<SectionData>();
        }
    }

    public class MenuData
    {
        public string MenuID { get; set; }
        public string MenuName { get; set; }
        public Int32 MenuOrder { get; set; }
        public string WebName { get; set; }
        public string WebURL { get; set; }
    }

    public class SectionData
    {
        public string SectionID { get; set; }
        public string SectionName { get; set; }
        public string SectionIcon { get; set; }
        public List<MenuData> Data { get; set; }

        public SectionData()
        {
            this.Data = new List<MenuData>();
        }
    }
}
