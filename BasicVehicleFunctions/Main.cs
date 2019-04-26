[assembly: Rage.Attributes.Plugin("BasicVehicleFunctions", Author = "Samtoxie", Description = "Basic Vehicle Functions")]

namespace MenuExample
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;
    using Rage;

    using RAGENativeUI;
    using RAGENativeUI.Elements;
    using BasicVehicleFunctions.Utils;

    public static class EntryPoint
    {
        private static GameFiber MenusProcessFiber;

        private static UIMenu mainMenu;
        private static UIMenu DoorMainMenu;

        private static UIMenuItem toggleBoot;
        private static UIMenuItem toggleBonnet;
        private static UIMenuItem togglelf;
        private static UIMenuItem togglerf;
        private static UIMenuItem togglefd;
        private static UIMenuItem togglefp;

        public static UIMenuItem StartCalloutItem;

        private static MenuPool menuPool;

        public static void Main()
        {

            MenusProcessFiber = new GameFiber(ProcessLoop);
            Logger.DebugLog("ProcessFiber created");

            menuPool = new MenuPool();
            Logger.DebugLog("MenuPool created");

            mainMenu = new UIMenu("BVF", "~b~Basic Vehicle Functions");
            mainMenu.MouseControlsEnabled = false;
            mainMenu.AllowCameraMovement = true;
            Logger.DebugLog("Main Menu created");

            menuPool.Add(mainMenu);
            Logger.DebugLog("Main Menu added to pool");

            DoorMainMenu = new UIMenu("BVF", "~b~Basic Vehicle Functions");
            DoorMainMenu.MouseControlsEnabled = false;
            DoorMainMenu.AllowCameraMovement = true;
            Logger.DebugLog("Door Menu created");

            menuPool.Add(DoorMainMenu);
            Logger.DebugLog("Door Menu added to pool");

            //add items here
            toggleBoot = new UIMenuItem("Boot");
            toggleBonnet = new UIMenuItem("Bonnet");
            togglelf = new UIMenuItem("Rear Driver");
            togglerf = new UIMenuItem("Rear Passenger");
            togglefd = new UIMenuItem("Front Driver");
            togglefp = new UIMenuItem("Front Passenger");


            mainMenu.AddItem(StartCalloutItem = new UIMenuItem("Open/Close Doors", " Open and close specific doors"));
            mainMenu.BindMenuToItem(DoorMainMenu, StartCalloutItem);

            DoorMainMenu.AddItem(togglefd);
            DoorMainMenu.AddItem(togglefp);
            DoorMainMenu.AddItem(togglelf);
            DoorMainMenu.AddItem(togglerf);
            DoorMainMenu.AddItem(toggleBoot);
            DoorMainMenu.AddItem(toggleBonnet);
            DoorMainMenu.RefreshIndex();
            DoorMainMenu.OnItemSelect += BootOnItemSelect;
            DoorMainMenu.OnIndexChange += OnItemChange;
            Logger.DebugLog("Configured Door Menu");

            mainMenu.RefreshIndex();
            mainMenu.OnItemSelect += OnItemSelect;
            mainMenu.OnIndexChange += OnItemChange;
            Logger.DebugLog("Configured Main Menu");

            MenusProcessFiber.Start();
            Logger.DebugLog("ProcessFiber started");
            GameFiber.Hibernate();
        }


        public static void OnItemChange(UIMenu sender, int index)
        {
            sender.MenuItems[index].SetLeftBadge(UIMenuItem.BadgeStyle.None);
            Logger.DebugLog("ItemChange");
        }




        public static void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
        {
            if (sender != mainMenu) return; // We only want to detect changes from our menu.
            // You can also detect the button by using index
            
        }

        public static void BootOnItemSelect(UIMenu sender, UIMenuItem SelectedItem, int index)
        {
            if (sender != DoorMainMenu) return; // We only want to detect changes from our menu.
            // You can also detect the button by using index
            else if (SelectedItem == toggleBoot)
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
            else if (SelectedItem == toggleBonnet)
            {
                GameFiber.StartNew(delegate // Start a new fiber if the code sleeps or waits and we don't want to block the MenusProcessFiber
                {
                    try
                    {
                        if (Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[4].IsOpen)
                        {
                            Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[4].Close(false);
                        }
                        if (!Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[4].IsOpen)
                        {
                            Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[4].Open(false);
                        }
                    }
                    catch (Rage.Exceptions.InvalidHandleableException)
                    {
                        Game.DisplaySubtitle("Closest vehicle has no bonnet!", 2500);
                    }
                });
            }
            else if (SelectedItem == togglelf)
            {
                GameFiber.StartNew(delegate // Start a new fiber if the code sleeps or waits and we don't want to block the MenusProcessFiber
                {
                    try
                    {
                        if (Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[2].IsOpen)
                        {
                            Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[2].Close(false);
                        }
                        if (!Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[2].IsOpen)
                        {
                            Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[2].Open(false);
                        }
                    }
                    catch (Rage.Exceptions.InvalidHandleableException)
                    {
                        Game.DisplaySubtitle("Closest vehicle has no rear driverside door!", 2500);
                    }
                });
            }
            else if (SelectedItem == togglerf)
            {
                GameFiber.StartNew(delegate // Start a new fiber if the code sleeps or waits and we don't want to block the MenusProcessFiber
                {
                    try
                    {
                        if (Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[3].IsOpen)
                        {
                            Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[3].Close(false);
                        }
                        if (!Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[3].IsOpen)
                        {
                            Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[3].Open(false);
                        }
                    }
                    catch (Rage.Exceptions.InvalidHandleableException)
                    {
                        Game.DisplaySubtitle("Closest vehicle has no rear passengerside door!", 2500);
                    }
                });
            }
            else if (SelectedItem == togglefp)
            {
                GameFiber.StartNew(delegate // Start a new fiber if the code sleeps or waits and we don't want to block the MenusProcessFiber
                {
                    try
                    {
                        if (Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[1].IsOpen)
                        {
                            Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[1].Close(false);
                        }
                        if (!Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[1].IsOpen)
                        {
                            Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[1].Open(false);
                        }
                    }
                    catch (Rage.Exceptions.InvalidHandleableException)
                    {
                        Game.DisplaySubtitle("Closest vehicle has no front passengerside door!", 2500);
                    }
                });
            }
            else if (SelectedItem == togglefd)
            {
                GameFiber.StartNew(delegate // Start a new fiber if the code sleeps or waits and we don't want to block the MenusProcessFiber
                {
                    try
                    {
                        if (Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[0].IsOpen)
                        {
                            Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[0].Close(false);
                        }
                        if (!Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[0].IsOpen)
                        {
                            Game.LocalPlayer.Character.GetNearbyVehicles(1)[0].Doors[0].Open(false);
                        }
                    }
                    catch (Rage.Exceptions.InvalidHandleableException)
                    {
                        Game.DisplaySubtitle("Closest vehicle has no front driverside door!", 2500);
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

