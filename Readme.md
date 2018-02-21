# Clipboard Companion
Helpful utilities to modify text on your clipboard.

# Development
## Building
You'll need Visual Studio 2017 installed with the ".NET desktop development" option checked.  You'll also need .NET Framework 4.6.1.

## Getting the XAML editor to work
Since ViewModels are injected into each companion UserControl, the XAML editor doesn't work great. Here's the weird work around I've found to make it work:
* Open the XAML window you wish to edit
* Open a code file
* Build, Rebuild Solution
* Switch back to the XAML window you opened previously