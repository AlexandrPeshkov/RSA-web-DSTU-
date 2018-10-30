using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.Models
{
    public enum ButtonType
    {
        Submit,
        Button
    }
    public class Button
    {
        public ButtonType Type { get; set; }
        public List<string> Classes { get; set; }
        public string Text { get; set; }
    }
}
