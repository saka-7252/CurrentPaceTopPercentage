# CurrentPaceTopPercentage

A LiveSplit component that calculates and displays how your current run's pace ranks within your historical data as a percentage.

## Features

- **Dual Percentile Tracking**: Shows your standing from two perspectives:
  - **Global**: Compared against all attempts (including resets).
  - **Reach**: Compared only against runs that reached that specific split.
- **Customizable Display**: 
  - Toggle between Global only, Reach only, or both (split by a slash).
  - Adjust decimal precision.

## Installation

1. Download the latest `CurrentPaceTopPercentage.dll` from the [Releases](https://github.com/saka-7252/CurrentPaceTopPercentage/releases/latest) page.
2. Exit LiveSplit.
3. Copy the DLL file into the `Components` folder within your LiveSplit installation directory.
4. Restart LiveSplit.
5. Right-click LiveSplit -> `Edit Layout` -> Click the `+` button -> `Information` -> `CurrentPaceTopPercentage`.

## Settings

You can customize the component via `Layout Settings` under the `CurrentPaceTopPercentage` tab:

- **Display Mode**:
  - `Global (Includes Resets)`: Percentile based on total attempts.
  - `Reach (Doesn't include Resets)`: Percentile based on the number of times you've reached that split.
  - `Both`: Shows both values separated by a slash.
- **Decimals**: Sets the number of decimal places for the percentage.

## Credits

Developed by **saka**

## License

This project is licensed under the [MIT License](LICENSE).
