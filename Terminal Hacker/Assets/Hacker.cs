using System;
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
            Terminal.WriteLine("You typed: '" + input + "'");
            return;
        }

        
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

            case "conspiracy":
            case "Conspiracy":
                ShowMainMenu("Trust no one!");
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

    enum Screen { MainMenu, Password, Win }
}
