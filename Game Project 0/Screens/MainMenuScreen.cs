using Microsoft.Xna.Framework;
using GameArchitectureExample.StateManagement;

namespace GameArchitectureExample.Screens
{
    // The main menu screen is the first thing displayed when the game starts up.
    public class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen() : base("Ship Quest")
        {
            var playGameMenuEntry = new MenuEntry("Play Game");

            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;

            MenuEntries.Add(playGameMenuEntry);
        }

        private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {

            ScreenState = ScreenState.TransitionOff;
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            ScreenManager.Game.Exit();
        }

    }
}
