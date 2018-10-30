using RSA_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.ViewModels
{
    public enum Buttons
    {
        RandomButton,
        ApplyButton,
        BackButton
    }

    public class ButtonsView
    {
        public List<Button> Buttons { get; set; }

        public ButtonsView(params Buttons[] buttons)
        {
            Buttons = new List<Button>();
            foreach (var button in buttons)
            {
             
                switch (button)
                {
                    case ViewModels.Buttons.ApplyButton:
                        {
                            Button NewButton = new Button()
                            {
                                Type = ButtonType.Submit,
                                Classes = new List<string>() { "btn","btn-success" },
                                Text = "Применить"
                            };
                            Buttons.Add(NewButton);
                            break;
                        }
                    case ViewModels.Buttons.RandomButton:
                        {
                            Button NewButton = new Button()
                            {
                                Type = ButtonType.Button,
                                Classes = new List<string>() { "btn","btn-warning" },
                                Text = "Случайные"
                            };
                            Buttons.Add(NewButton);
                            break;
                        }
                    case ViewModels.Buttons.BackButton:
                        {
                            Button NewButton = new Button()
                            {
                                Type = ButtonType.Button,
                                Classes = new List<string>()  { "btn", "btn-warning" },
                                Text = "Назад"
                            };
                            Buttons.Add(NewButton);
                            break;
                        }
                }
                
            }
        }
    }
}
