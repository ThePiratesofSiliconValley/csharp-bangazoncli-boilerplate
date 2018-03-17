using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_bangazoncli.app
{
    class SelectOrderToCompleteView
    {
        IList<string> _menuItems;
        int itemNumber = 0;

        string companyName = @"
                                *********************************************************
                                **  Welcome to Bangazon! Command Line Ordering System  **
                                *********************************************************";

        internal SelectOrderToCompleteView()
        {
            _menuItems = new List<string> { companyName };
        }

        internal SelectOrderToCompleteView AddMenuText(string text)
        {
            var menuText = $"{Environment.NewLine}{text}{Environment.NewLine}";
            _menuItems.Add(menuText);
            return this;
        }

        internal SelectOrderToCompleteView AddMenuOptions(IList<string> menuItems)
        {
            foreach (var menuItem in menuItems)
            {
                AddMenuOption(menuItem);
            }

            return this;
        }

        internal SelectOrderToCompleteView AddMenuOption(string menuItem)
        {
            ++itemNumber;
            var menuEntry = $"{itemNumber}. {menuItem}";
            _menuItems.Add(menuEntry);
            return this;
        }

        internal string GetFullMenu()
        {
            Console.Clear();
            var menu = string.Join(Environment.NewLine, _menuItems);
            return $"{menu}{Environment.NewLine}> ";
        }
    }
}

