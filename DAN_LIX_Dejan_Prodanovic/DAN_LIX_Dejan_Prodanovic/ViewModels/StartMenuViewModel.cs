using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_LIX_Dejan_Prodanovic.ViewModels
{
    public class StartMenuViewModel
    {
        private MainWindow _mainWindow;
        public StartMenuViewModel(MainWindow main)
        {
            _mainWindow = main;

        }

        public void StartNewGame(int categoryIndex)
        {
            var category = (SlideCategories)categoryIndex;
            GameViewModel newGame = new GameViewModel(category);
            _mainWindow.DataContext = newGame;
        }
    }
}
