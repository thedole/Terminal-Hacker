using System;
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
        {1, new List<string>{"ride", "line", "belt", "cart", "spin"} },
        {2, new List<string>{"lollipop", "candycane", "polkadots", "bubblegum", "fizzydrink"} },
        {3, new List<string>{"world domination", "mindcontrol", "disinformation", "world bank", "population control" } },
    };
    private static Dictionary<int, string> levelHeader = new Dictionary<int, string> {
        { 1, "Theme park ride control system" },
        { 2, "Candy heaven order system" },
        { 3, "Illuminati total control system" },
    };
    private static Dictionary<int, string> levelReward = new Dictionary<int, string> {
        { 1, @"You can now control all of the parks rides


__________________________
/ / / / / / / / / / / / / 
‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾
"
        },
        { 2, @"All the candy you can eat is yours

        .*´.‾.`*.
       /.´.´  .´ \ 
      |.´ /´‾`\.´ |
      |.´|     |.´|
      |.´|     '--'
      |.´|   
      |.´|
      '--'"
        },
        { 3, @"Total control is yours
        $
      $$$$$.
    $'  $   '
    $$  $
     '$$$$$. 
        $  $$
    $.  $  '$
     '$$$$$'
        $
" },
    };
    private string password;
    private int level;
    private string greeting;
    private Screen currentScreen;

	// Use this for initialization
	void Start () {
        currentScreen = Screen.MainMenu;
        greeting = "Hello h4xx0r!";
        ShowMainMenu(greeting);
    }

    // ------------ Main Menu ------------
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

    private void ShowMainMenu(string greeting)
    {
        currentScreen = Screen.MainMenu;
        level = 0;
        var menuBuilder = new StringBuilder();
        BuildMenu(menuBuilder, greeting);

        Terminal.ClearScreen();
        Terminal.WriteLine(menuBuilder.ToString());
    }

    private void SelectLevel(string input)
    {
        switch (input)
        {
            case "1":
            case "2":
            case "3":
                SetLevel(input);
                AskForPassword();
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

    // --------- input processing --------
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

    // ------------ Password --------------
    private string GetPassword(int level)
    {
        var passwordList = passwords[level];
        var index = UnityEngine.Random.Range(0, passwordList.Count);

        var password = passwordList.ElementAt(index);
        return password;
    }

    private void AskForPassword()
    {
        currentScreen = Screen.Password;
        password = GetPassword(level);
        var heading = levelHeader[level];

        Terminal.ClearScreen();
        Terminal.WriteLine(heading);
        Terminal.WriteLine("‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");
        Terminal.WriteLine("Type 'menu' to return to main menu");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter password, hint: " + password.Anagram());
    }

    private void CheckPassword(string input)
    {
        if (input == password)
        {
            ShowWinScreen();
            return;
        }

        AskForPassword();
    }

    // ---------- Win Screen -------------
    private void ProcessWinScreen(string input)
    {
        ShowMainMenu(greeting);
    }

    private void ShowWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    private void ShowLevelReward()
    {
        Terminal.WriteLine(levelReward[level]);
    }

    // --------- Easteregg ---------------
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

    // ------- Screen definition -----------
    enum Screen { MainMenu, Password, Win, EasterEgg }
}
