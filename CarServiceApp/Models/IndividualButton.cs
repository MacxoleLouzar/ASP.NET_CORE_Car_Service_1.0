using System;
using System.Text;

namespace CarServiceApp.Models
{
    public class IndividualButton
    {
        public string ButtonType { get; set; }
        public string Action { get; set; }
        public string Glyph { get; set; }
        public string Text { get; set; }

        public int? serviceId { get; set; }

        public string ActionParameters
        {
            get
            {
                var param = new StringBuilder(@" /");
                if (serviceId != 0 && serviceId != null)
                {
                    param.Append(String.Format("{0}", serviceId));
                }
                return param.ToString().Substring(0, param.Length);
            }
        }
    }
}
