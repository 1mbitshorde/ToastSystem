# Toast System

A toast message is a brief, non-intrusive notification that appears temporarily providing user feedback without interrupting their current task. These messages can inform users of an action's success or failure, display system messages, or provide other contextual information, and they disappear automatically after a few seconds. 

## How To Use

Instantiate the prefab `P_ToastManager` and use any static function to show normal and localized messages:

```csharp
// Simple Toast Message without Localization:
ToastManager.ShowMessage("This is a simple toast message!");

// Localized Toast Message:
ToastManager.ShowMessage(key: "player_jump", table: "Tutorial");

// Localized Toast Message with Smart Arguments:
// Assuming your Smart String in the LevelSummary Table is:
// win_message: "Congratulations! You have scored {score-points} points!"

var scorePoints = 1500;
var variable = new ToastVariable("score-points", scorePoints.ToString());
ToastManager.ShowMessage(
    key: "win_message",
    table: "LevelSummary",
    variable
);
// Note: win_message should be marked as Smart String in the Localization Table Editor.
```

## Installation

### Using the Git URL

You will need a **Git client** installed on your computer with the Path variable already set and the correct git credentials to 1M Bits Horde.

- In this repo, go to Code button, select SSH and copy the URL.
- In Unity, use the **Package Manager** "Add package from git URL..." feature and paste the URL.
- Set the version adding the suffix `#[x.y.z]` at URL

---

**1 Million Bits Horde**

[Website](https://www.1mbitshorde.com) -
[GitHub](https://github.com/1mbitshorde) -
[LinkedIn](https://www.linkedin.com/company/1m-bits-horde)
