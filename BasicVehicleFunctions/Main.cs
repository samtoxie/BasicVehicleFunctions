[assembly: Rage.Attributes.Plugin("MenuExample", Author = "Guad / alexguirre", Description = "Example using RAGENativeUI")]

namespace MenuExample
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;
    using Rage;

    using RAGENativeUI;
    using RAGENativeUI.Elements;

    public static class EntryPoint
    {
        private static GameFiber MenusProcessFiber;

        private static UIMenu mainMenu;
        private static UIMenuItem toggleBoot;

        private static MenuPool menuPool;

        public static void Main()
        {
            // Create a fiber to process our menus
            MenusProcessFiber = new GameFiber(ProcessLoop);

            // Create the MenuPool to easily process our menus
            menuPool = new MenuPool();

            // Create our main menu
            mainMenu = new UIMenu("BVF", "~b~Basic Vehicle Functions");

            // Add our main menu to the MenuPool
            menuPool.Add(mainMenu);
            mainMenu.MouseControlsEnabled = false;
            mainMenu.AllowCameraMovement = true;
            // create our items and add them to our main menu
            toggleBoot = new UIMenuItem("Toggle boot");
            mainMenu.AddItem(toggleBoot);

            mainMenu.RefreshIndex();

            mainMenu.OnItemSelect += OnItemSelect;
            mainMenu.OnIndexChange += OnItemChange;

            // Start our process fiber
            MenusProcessFiber.Start();

            // Continue with our plugin... in this example, hibernate to prevent it from being unloaded
            GameFiber.Hibernate();
        }


        public static void OnItemChange(UIMenu sender, int index)
        {
            sender.MenuItems[index].SetLeftBadge(UIMenuItem.BadgeStyle.None);
        }



  
        public static void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (sender != mainMenu) return; // We only want to detect changes from our menu.
            // You can also detect the button by using index
            else if (selectedItem == toggleBoot)
            {
                GameFiber.StartNew(delegate // Start a new fiber if the code sleeps or waits and we don't want to block the MenusProcessFiber
                {
                        try
                        {
                            if (Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[5].IsOpen)
                            {
                             Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[5].Close(false);
                            }
                            if (!Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[5].IsOpen)
                            {
                            Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[5].Open(false);
                            }
                        }
                        catch (Rage.Exceptions.InvalidHandleableException)
                        {
                           Game.DisplaySubtitle("Closest vehicle has no boot!", 2500);
                        }
                });
            }
        }

        // The method that contains a loop to handle our menus
        public static void ProcessLoop()
        {
            // if we are using banners with a Rage.Texture (UIMenu.SetBannerType(...)), we need to draw them in the RawFrameRender, in this example we don't have a Rage.Texture banner so this isn't needed
            //Game.RawFrameRender += (s, e) => 
            //{
            //    _menuPool.DrawBanners(e.Graphics);
            //};

            while (true)
            {
                GameFiber.Yield();

                if (Game.IsKeyDown(Keys.F5) && !menuPool.IsAnyMenuOpen()) // Our menu on/off switch.
                {
                    mainMenu.Visible = !mainMenu.Visible;
                }

                menuPool.ProcessMenus();       // Process all our menus: draw the menu and process the key strokes and the mouse. 
            }
        }
    }
}

