﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Hacker : MonoBehaviour {
    private static string[] _menuText = {
        "=== H4CKsTat10N ===",
        "Todays objectives",
        "",
        "1 Theme park",
        "2 Online candy shop",
        "3 The Illuminati",
        "",
        "Select your target:"
    };
    private static Dictionary<int, List<string>> passwords = new Dictionary<int, List<string>>
    {
        {1, new List<string>{"test1"} },
        {2, new List<string>{"test2"} },
        {3, new List<string>{"test3"} },
    };
    private int level;
    private string greeting;
    private Screen currentScreen;

	// Use this for initialization
	void Start () {
        currentScreen = Screen.MainMenu;
        greeting = "Hello h4xx0r!";
        ShowMainMenu(greeting);
    }

    private void ShowMainMenu(string greeting)
    {
        currentScreen = Screen.MainMenu;
        level = 0;
        var menuBuilder = new StringBuilder();
        BuildMenu(menuBuilder, greeting);

        Terminal.ClearScreen();
        Terminal.WriteLine(menuBuilder.ToString());
    }

    private static void BuildMenu(StringBuilder builder, string greeting)
    {
        for (var i = 0; i < _menuText.Length; i++)
        {
            if (i == _menuText.Length - 1)
            {
                builder.AppendLine(greeting);
            }

            builder.AppendLine(_menuText[i]);
        }      
    }

    void OnUserInput(string input)
    {
        if (EasterEgg(input)) return;

        // We can always go directly to the main menu
        if (input.Equals("menu",StringComparison.InvariantCultureIgnoreCase))
        {
            ShowMainMenu(greeting);
            return;
        }

        if (currentScreen == Screen.MainMenu)
        {
            SelectLevel(input);
            return;
        }

        if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
            return;
        }

        if (currentScreen == Screen.Win)
        {
            ProcessWinScreen(input);
            return;
        }

        if (currentScreen == Screen.EasterEgg)
        {
            ShowMainMenu("1337");
            return;
        }
    }

    private void ProcessWinScreen(string input)
    {
        ShowMainMenu(greeting);
    }

    private bool EasterEgg(string input)
    {
        switch (level)
        {
            case 0:
                if (input.Equals("1337", StringComparison.InvariantCultureIgnoreCase))
                {
                    Terminal.ClearScreen();
                    Terminal.WriteLine("Hackers delight!");
                    currentScreen = Screen.EasterEgg;
                    return true;
                }
                break;
            case 1:
                if (input.Equals("loop", StringComparison.InvariantCultureIgnoreCase))
                {
                    Terminal.ClearScreen();
                    Terminal.WriteLine("Crazy speed demon!");
                    currentScreen = Screen.EasterEgg;
                    return true;
                }
                break;

            case 2:
                if (input.Equals("sugar", StringComparison.InvariantCultureIgnoreCase))
                {
                    Terminal.ClearScreen();
                    Terminal.WriteLine("May your teeth rot in sweet, sweet candy!");
                    currentScreen = Screen.EasterEgg;
                    return true;
                }
                break;
            case 3:
                if (input.Equals("conspiracy", StringComparison.InvariantCultureIgnoreCase))
                {
                    Terminal.ClearScreen();
                    Terminal.WriteLine("Trust no one!");
                    currentScreen = Screen.EasterEgg;
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

    private void CheckPassword(string input)
    {
        var password = passwords[level].First();
        if(input == password)
        {
            Terminal.WriteLine("Congratulations, you are 1337 h4xx0r! Press enter to return to menu");
            currentScreen = Screen.Win;
            return;
        }

        Terminal.WriteLine("That's not it, pick yourself up and try again, or type menu to return to main menu!");
    }

    private void SelectLevel(string input)
    {   
        switch (input)
        {
            // TODO Handle differently depending on current screen
            case "1":
            case "2":
            case "3":
                SetLevel(input);
                StartGame();
                break;

            default:
                var message = "Command unknown '" + input + "'";
                ShowMainMenu(message);
                break;
        }
    }

    private void SetLevel(string input)
    {
        int numericalInput;
        if (int.TryParse(input, out numericalInput))
        {
            if (numericalInput > 0 && numericalInput <= 3)
            {
                level = numericalInput;
            }
        }
    }

    private void StartGame()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        Terminal.WriteLine("You have selected level " + level);
    }

    enum Screen { MainMenu, Password, Win, EasterEgg }
}
