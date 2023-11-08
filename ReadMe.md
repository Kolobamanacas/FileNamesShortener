### Building the application.

1. Clone the repo with: `git clone git@github.com:Kolobamanacas/FileNamesShortener.git`.
2. Open `FileNamesShortener.sln` with Visual Studio.
3. Choose preferable mode (debug/release) and CPU architecture or just leave everything as it is.
4. Build the solution using menu Build > Build solution or by pressing F6.
5. Built application should appear in `\FileNamesShortener\bin\[Debug/Release]\net7.0\` folder.

### Using the application.

1. Open PowerShell/Command Prompt terminal in a folder where the app is located.
2. Run `FileNamesShortener.exe -path '[PathToFolder]' -prefix-length [NumberOfCharacters]`, where `[PathToFolder]` is path, where files, which names you wish to shorten are located and `[NumberOfCharacters]` is how much characters of the original file name you wish to leave untouched (file extension is never removed). Passing 0 will remove everything except from extension.

Important! When the application is provided with a path to a folder, it will rename all files in the folder and it's subfolders.

### Example.

Consider we have a folder `C:\Work\` with two files:
- `My Epic Poem.docx`
- `Secret Scetch.jpg`

Running `FileNamesShortener.exe -path 'C:\Work' -prefix-length 4` will turn them into:
- `My E.docx`
- `Secr.jpg`
