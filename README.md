# Marbles On Stream local leaderboard parser
This uses the gvas-converter library from https://github.com/13xforever/gvas-converter.

It is used in conjuction with https://streamer.bot/ and StreamElements to automatically execute the !addpoints command for the top 3 participants in each race.

The way it is set now, 1st place gets 1000 points, 2nd place 700 and 3rd place 500.

Unfortunately, the code is not very flexible, so if you want to have different values you need to edit and compile it yourself. The executable code is in GvasConverter\Program.cs

## How to use <br/>

1. In Marbles On Stream create a new session with any name or use an existing one.

2. In settings, check "use custom points" and assign 1st place 3 points, 2nd place 2 points and 3rd place 1 point. (Make sure "Participation Points" is 0)

3. After that there should a .sav file in "AppData\Local\MarblesOnStream\Saved\SaveGames\Sessions" with the name of the session that you made.

4. In Streamer.bot import:
```
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
```
5. Go to Settings and then File/Folder Watcher add folder by clicking on the "..." and find "AppData\Local\MarblesOnStream\Saved\SaveGames\Sessions", Filter to "your_session_name".sav file and Action to PointAlloc.

6. In Sub-Actions set %save% argument value, in both Initializer and PointAlloc, to the full path of "your_session_name".sav in AppData\Local\MarblesOnStream\Saved\SaveGames\Sessions. (Make sure that it's not surrounded by "")

For Example:

Wrong: "C:\Users\PC\AppData\Local\MarblesOnStream\Saved\SaveGames\Sessions\mysession.sav"

Correct: C:\Users\PC\AppData\Local\MarblesOnStream\Saved\SaveGames\Sessions\mysession.sav

7. Set %converter% argument, in both Initializer and PointAlloc, to the full path of GvasConverter.exe file from the corverter.rar that you downloaded. (Make sure that it's not surrounded by "")

8. Edit execute code, in both Initializer and PointAlloc, and add System.dll in the References tab at the bottom, if it's not already, and compile to see if there are any errors.

9. In commands, enable !manualinit if it's disabled.

10. Initialize once by typing !manualinit in twitch chat. (This should create 2 extra files in "AppData\Local\MarblesOnStream\Saved\SaveGames\Sessions", a .json and a .txt)

After this, it should update automatically every time a race ends and it will assign the appropriate points to the chat member by type the commands.
