var System;
(function (System) {
    (function (ContextForm) {
        ContextForm[ContextForm["Loose"] = 0] = "Loose";
        ContextForm[ContextForm["StoreBounded"] = 1] = "StoreBounded";
    })(System.ContextForm || (System.ContextForm = {}));
    var ContextForm = System.ContextForm;

    (function (AppDomainManagerInitializationOptions) {
        AppDomainManagerInitializationOptions[AppDomainManagerInitializationOptions["None"] = 0] = "None";
        AppDomainManagerInitializationOptions[AppDomainManagerInitializationOptions["RegisterWithHost"] = 1] = "RegisterWithHost";
    })(System.AppDomainManagerInitializationOptions || (System.AppDomainManagerInitializationOptions = {}));
    var AppDomainManagerInitializationOptions = System.AppDomainManagerInitializationOptions;

    (function (AttributeTargets) {
        AttributeTargets[AttributeTargets["All"] = 0] = "All";
        AttributeTargets[AttributeTargets["Assembly"] = 1] = "Assembly";
        AttributeTargets[AttributeTargets["Class"] = 2] = "Class";
        AttributeTargets[AttributeTargets["Constructor"] = 3] = "Constructor";
        AttributeTargets[AttributeTargets["Delegate"] = 4] = "Delegate";
        AttributeTargets[AttributeTargets["Enum"] = 5] = "Enum";
        AttributeTargets[AttributeTargets["Event"] = 6] = "Event";
        AttributeTargets[AttributeTargets["Field"] = 7] = "Field";
        AttributeTargets[AttributeTargets["GenericParameter"] = 8] = "GenericParameter";
        AttributeTargets[AttributeTargets["Interface"] = 9] = "Interface";
        AttributeTargets[AttributeTargets["Method"] = 10] = "Method";
        AttributeTargets[AttributeTargets["Module"] = 11] = "Module";
        AttributeTargets[AttributeTargets["Parameter"] = 12] = "Parameter";
        AttributeTargets[AttributeTargets["Property"] = 13] = "Property";
        AttributeTargets[AttributeTargets["ReturnValue"] = 14] = "ReturnValue";
        AttributeTargets[AttributeTargets["Struct"] = 15] = "Struct";
    })(System.AttributeTargets || (System.AttributeTargets = {}));
    var AttributeTargets = System.AttributeTargets;

    (function (Base64FormattingOptions) {
        Base64FormattingOptions[Base64FormattingOptions["InsertLineBreaks"] = 0] = "InsertLineBreaks";
        Base64FormattingOptions[Base64FormattingOptions["None"] = 1] = "None";
    })(System.Base64FormattingOptions || (System.Base64FormattingOptions = {}));
    var Base64FormattingOptions = System.Base64FormattingOptions;

    (function (ConsoleColor) {
        ConsoleColor[ConsoleColor["Black"] = 0] = "Black";
        ConsoleColor[ConsoleColor["Blue"] = 1] = "Blue";
        ConsoleColor[ConsoleColor["Cyan"] = 2] = "Cyan";
        ConsoleColor[ConsoleColor["DarkBlue"] = 3] = "DarkBlue";
        ConsoleColor[ConsoleColor["DarkCyan"] = 4] = "DarkCyan";
        ConsoleColor[ConsoleColor["DarkGray"] = 5] = "DarkGray";
        ConsoleColor[ConsoleColor["DarkGreen"] = 6] = "DarkGreen";
        ConsoleColor[ConsoleColor["DarkMagenta"] = 7] = "DarkMagenta";
        ConsoleColor[ConsoleColor["DarkRed"] = 8] = "DarkRed";
        ConsoleColor[ConsoleColor["DarkYellow"] = 9] = "DarkYellow";
        ConsoleColor[ConsoleColor["Gray"] = 10] = "Gray";
        ConsoleColor[ConsoleColor["Green"] = 11] = "Green";
        ConsoleColor[ConsoleColor["Magenta"] = 12] = "Magenta";
        ConsoleColor[ConsoleColor["Red"] = 13] = "Red";
        ConsoleColor[ConsoleColor["White"] = 14] = "White";
        ConsoleColor[ConsoleColor["Yellow"] = 15] = "Yellow";
    })(System.ConsoleColor || (System.ConsoleColor = {}));
    var ConsoleColor = System.ConsoleColor;

    (function (ConsoleKey) {
        ConsoleKey[ConsoleKey["A"] = 0] = "A";
        ConsoleKey[ConsoleKey["Add"] = 1] = "Add";
        ConsoleKey[ConsoleKey["Applications"] = 2] = "Applications";
        ConsoleKey[ConsoleKey["Attention"] = 3] = "Attention";
        ConsoleKey[ConsoleKey["B"] = 4] = "B";
        ConsoleKey[ConsoleKey["Backspace"] = 5] = "Backspace";
        ConsoleKey[ConsoleKey["BrowserBack"] = 6] = "BrowserBack";
        ConsoleKey[ConsoleKey["BrowserFavorites"] = 7] = "BrowserFavorites";
        ConsoleKey[ConsoleKey["BrowserForward"] = 8] = "BrowserForward";
        ConsoleKey[ConsoleKey["BrowserHome"] = 9] = "BrowserHome";
        ConsoleKey[ConsoleKey["BrowserRefresh"] = 10] = "BrowserRefresh";
        ConsoleKey[ConsoleKey["BrowserSearch"] = 11] = "BrowserSearch";
        ConsoleKey[ConsoleKey["BrowserStop"] = 12] = "BrowserStop";
        ConsoleKey[ConsoleKey["C"] = 13] = "C";
        ConsoleKey[ConsoleKey["Clear"] = 14] = "Clear";
        ConsoleKey[ConsoleKey["CrSel"] = 15] = "CrSel";
        ConsoleKey[ConsoleKey["D"] = 16] = "D";
        ConsoleKey[ConsoleKey["D0"] = 17] = "D0";
        ConsoleKey[ConsoleKey["D1"] = 18] = "D1";
        ConsoleKey[ConsoleKey["D2"] = 19] = "D2";
        ConsoleKey[ConsoleKey["D3"] = 20] = "D3";
        ConsoleKey[ConsoleKey["D4"] = 21] = "D4";
        ConsoleKey[ConsoleKey["D5"] = 22] = "D5";
        ConsoleKey[ConsoleKey["D6"] = 23] = "D6";
        ConsoleKey[ConsoleKey["D7"] = 24] = "D7";
        ConsoleKey[ConsoleKey["D8"] = 25] = "D8";
        ConsoleKey[ConsoleKey["D9"] = 26] = "D9";
        ConsoleKey[ConsoleKey["Decimal"] = 27] = "Decimal";
        ConsoleKey[ConsoleKey["Delete"] = 28] = "Delete";
        ConsoleKey[ConsoleKey["Divide"] = 29] = "Divide";
        ConsoleKey[ConsoleKey["DownArrow"] = 30] = "DownArrow";
        ConsoleKey[ConsoleKey["E"] = 31] = "E";
        ConsoleKey[ConsoleKey["End"] = 32] = "End";
        ConsoleKey[ConsoleKey["Enter"] = 33] = "Enter";
        ConsoleKey[ConsoleKey["EraseEndOfFile"] = 34] = "EraseEndOfFile";
        ConsoleKey[ConsoleKey["Escape"] = 35] = "Escape";
        ConsoleKey[ConsoleKey["Execute"] = 36] = "Execute";
        ConsoleKey[ConsoleKey["ExSel"] = 37] = "ExSel";
        ConsoleKey[ConsoleKey["F"] = 38] = "F";
        ConsoleKey[ConsoleKey["F1"] = 39] = "F1";
        ConsoleKey[ConsoleKey["F10"] = 40] = "F10";
        ConsoleKey[ConsoleKey["F11"] = 41] = "F11";
        ConsoleKey[ConsoleKey["F12"] = 42] = "F12";
        ConsoleKey[ConsoleKey["F13"] = 43] = "F13";
        ConsoleKey[ConsoleKey["F14"] = 44] = "F14";
        ConsoleKey[ConsoleKey["F15"] = 45] = "F15";
        ConsoleKey[ConsoleKey["F16"] = 46] = "F16";
        ConsoleKey[ConsoleKey["F17"] = 47] = "F17";
        ConsoleKey[ConsoleKey["F18"] = 48] = "F18";
        ConsoleKey[ConsoleKey["F19"] = 49] = "F19";
        ConsoleKey[ConsoleKey["F2"] = 50] = "F2";
        ConsoleKey[ConsoleKey["F20"] = 51] = "F20";
        ConsoleKey[ConsoleKey["F21"] = 52] = "F21";
        ConsoleKey[ConsoleKey["F22"] = 53] = "F22";
        ConsoleKey[ConsoleKey["F23"] = 54] = "F23";
        ConsoleKey[ConsoleKey["F24"] = 55] = "F24";
        ConsoleKey[ConsoleKey["F3"] = 56] = "F3";
        ConsoleKey[ConsoleKey["F4"] = 57] = "F4";
        ConsoleKey[ConsoleKey["F5"] = 58] = "F5";
        ConsoleKey[ConsoleKey["F6"] = 59] = "F6";
        ConsoleKey[ConsoleKey["F7"] = 60] = "F7";
        ConsoleKey[ConsoleKey["F8"] = 61] = "F8";
        ConsoleKey[ConsoleKey["F9"] = 62] = "F9";
        ConsoleKey[ConsoleKey["G"] = 63] = "G";
        ConsoleKey[ConsoleKey["H"] = 64] = "H";
        ConsoleKey[ConsoleKey["Help"] = 65] = "Help";
        ConsoleKey[ConsoleKey["Home"] = 66] = "Home";
        ConsoleKey[ConsoleKey["I"] = 67] = "I";
        ConsoleKey[ConsoleKey["Insert"] = 68] = "Insert";
        ConsoleKey[ConsoleKey["J"] = 69] = "J";
        ConsoleKey[ConsoleKey["K"] = 70] = "K";
        ConsoleKey[ConsoleKey["L"] = 71] = "L";
        ConsoleKey[ConsoleKey["LaunchApp1"] = 72] = "LaunchApp1";
        ConsoleKey[ConsoleKey["LaunchApp2"] = 73] = "LaunchApp2";
        ConsoleKey[ConsoleKey["LaunchMail"] = 74] = "LaunchMail";
        ConsoleKey[ConsoleKey["LaunchMediaSelect"] = 75] = "LaunchMediaSelect";
        ConsoleKey[ConsoleKey["LeftArrow"] = 76] = "LeftArrow";
        ConsoleKey[ConsoleKey["LeftWindows"] = 77] = "LeftWindows";
        ConsoleKey[ConsoleKey["M"] = 78] = "M";
        ConsoleKey[ConsoleKey["MediaNext"] = 79] = "MediaNext";
        ConsoleKey[ConsoleKey["MediaPlay"] = 80] = "MediaPlay";
        ConsoleKey[ConsoleKey["MediaPrevious"] = 81] = "MediaPrevious";
        ConsoleKey[ConsoleKey["MediaStop"] = 82] = "MediaStop";
        ConsoleKey[ConsoleKey["Multiply"] = 83] = "Multiply";
        ConsoleKey[ConsoleKey["N"] = 84] = "N";
        ConsoleKey[ConsoleKey["NoName"] = 85] = "NoName";
        ConsoleKey[ConsoleKey["NumPad0"] = 86] = "NumPad0";
        ConsoleKey[ConsoleKey["NumPad1"] = 87] = "NumPad1";
        ConsoleKey[ConsoleKey["NumPad2"] = 88] = "NumPad2";
        ConsoleKey[ConsoleKey["NumPad3"] = 89] = "NumPad3";
        ConsoleKey[ConsoleKey["NumPad4"] = 90] = "NumPad4";
        ConsoleKey[ConsoleKey["NumPad5"] = 91] = "NumPad5";
        ConsoleKey[ConsoleKey["NumPad6"] = 92] = "NumPad6";
        ConsoleKey[ConsoleKey["NumPad7"] = 93] = "NumPad7";
        ConsoleKey[ConsoleKey["NumPad8"] = 94] = "NumPad8";
        ConsoleKey[ConsoleKey["NumPad9"] = 95] = "NumPad9";
        ConsoleKey[ConsoleKey["O"] = 96] = "O";
        ConsoleKey[ConsoleKey["Oem1"] = 97] = "Oem1";
        ConsoleKey[ConsoleKey["Oem102"] = 98] = "Oem102";
        ConsoleKey[ConsoleKey["Oem2"] = 99] = "Oem2";
        ConsoleKey[ConsoleKey["Oem3"] = 100] = "Oem3";
        ConsoleKey[ConsoleKey["Oem4"] = 101] = "Oem4";
        ConsoleKey[ConsoleKey["Oem5"] = 102] = "Oem5";
        ConsoleKey[ConsoleKey["Oem6"] = 103] = "Oem6";
        ConsoleKey[ConsoleKey["Oem7"] = 104] = "Oem7";
        ConsoleKey[ConsoleKey["Oem8"] = 105] = "Oem8";
        ConsoleKey[ConsoleKey["OemClear"] = 106] = "OemClear";
        ConsoleKey[ConsoleKey["OemComma"] = 107] = "OemComma";
        ConsoleKey[ConsoleKey["OemMinus"] = 108] = "OemMinus";
        ConsoleKey[ConsoleKey["OemPeriod"] = 109] = "OemPeriod";
        ConsoleKey[ConsoleKey["OemPlus"] = 110] = "OemPlus";
        ConsoleKey[ConsoleKey["P"] = 111] = "P";
        ConsoleKey[ConsoleKey["Pa1"] = 112] = "Pa1";
        ConsoleKey[ConsoleKey["Packet"] = 113] = "Packet";
        ConsoleKey[ConsoleKey["PageDown"] = 114] = "PageDown";
        ConsoleKey[ConsoleKey["PageUp"] = 115] = "PageUp";
        ConsoleKey[ConsoleKey["Pause"] = 116] = "Pause";
        ConsoleKey[ConsoleKey["Play"] = 117] = "Play";
        ConsoleKey[ConsoleKey["Print"] = 118] = "Print";
        ConsoleKey[ConsoleKey["PrintScreen"] = 119] = "PrintScreen";
        ConsoleKey[ConsoleKey["Process"] = 120] = "Process";
        ConsoleKey[ConsoleKey["Q"] = 121] = "Q";
        ConsoleKey[ConsoleKey["R"] = 122] = "R";
        ConsoleKey[ConsoleKey["RightArrow"] = 123] = "RightArrow";
        ConsoleKey[ConsoleKey["RightWindows"] = 124] = "RightWindows";
        ConsoleKey[ConsoleKey["S"] = 125] = "S";
        ConsoleKey[ConsoleKey["Select"] = 126] = "Select";
        ConsoleKey[ConsoleKey["Separator"] = 127] = "Separator";
        ConsoleKey[ConsoleKey["Sleep"] = 128] = "Sleep";
        ConsoleKey[ConsoleKey["Spacebar"] = 129] = "Spacebar";
        ConsoleKey[ConsoleKey["Subtract"] = 130] = "Subtract";
        ConsoleKey[ConsoleKey["T"] = 131] = "T";
        ConsoleKey[ConsoleKey["Tab"] = 132] = "Tab";
        ConsoleKey[ConsoleKey["U"] = 133] = "U";
        ConsoleKey[ConsoleKey["UpArrow"] = 134] = "UpArrow";
        ConsoleKey[ConsoleKey["V"] = 135] = "V";
        ConsoleKey[ConsoleKey["VolumeDown"] = 136] = "VolumeDown";
        ConsoleKey[ConsoleKey["VolumeMute"] = 137] = "VolumeMute";
        ConsoleKey[ConsoleKey["VolumeUp"] = 138] = "VolumeUp";
        ConsoleKey[ConsoleKey["W"] = 139] = "W";
        ConsoleKey[ConsoleKey["X"] = 140] = "X";
        ConsoleKey[ConsoleKey["Y"] = 141] = "Y";
        ConsoleKey[ConsoleKey["Z"] = 142] = "Z";
        ConsoleKey[ConsoleKey["Zoom"] = 143] = "Zoom";
    })(System.ConsoleKey || (System.ConsoleKey = {}));
    var ConsoleKey = System.ConsoleKey;

    (function (ConsoleModifiers) {
        ConsoleModifiers[ConsoleModifiers["Alt"] = 0] = "Alt";
        ConsoleModifiers[ConsoleModifiers["Control"] = 1] = "Control";
        ConsoleModifiers[ConsoleModifiers["Shift"] = 2] = "Shift";
    })(System.ConsoleModifiers || (System.ConsoleModifiers = {}));
    var ConsoleModifiers = System.ConsoleModifiers;

    (function (ConsoleSpecialKey) {
        ConsoleSpecialKey[ConsoleSpecialKey["ControlBreak"] = 0] = "ControlBreak";
        ConsoleSpecialKey[ConsoleSpecialKey["ControlC"] = 1] = "ControlC";
    })(System.ConsoleSpecialKey || (System.ConsoleSpecialKey = {}));
    var ConsoleSpecialKey = System.ConsoleSpecialKey;

    (function (DateTimeKind) {
        DateTimeKind[DateTimeKind["Local"] = 0] = "Local";
        DateTimeKind[DateTimeKind["Unspecified"] = 1] = "Unspecified";
        DateTimeKind[DateTimeKind["Utc"] = 2] = "Utc";
    })(System.DateTimeKind || (System.DateTimeKind = {}));
    var DateTimeKind = System.DateTimeKind;

    (function (DayOfWeek) {
        DayOfWeek[DayOfWeek["Friday"] = 0] = "Friday";
        DayOfWeek[DayOfWeek["Monday"] = 1] = "Monday";
        DayOfWeek[DayOfWeek["Saturday"] = 2] = "Saturday";
        DayOfWeek[DayOfWeek["Sunday"] = 3] = "Sunday";
        DayOfWeek[DayOfWeek["Thursday"] = 4] = "Thursday";
        DayOfWeek[DayOfWeek["Tuesday"] = 5] = "Tuesday";
        DayOfWeek[DayOfWeek["Wednesday"] = 6] = "Wednesday";
    })(System.DayOfWeek || (System.DayOfWeek = {}));
    var DayOfWeek = System.DayOfWeek;
})(System || (System = {}));
//# sourceMappingURL=Enumerations.js.map
