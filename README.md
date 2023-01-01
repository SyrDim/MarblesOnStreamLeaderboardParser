# Marbles On Stream local leaderboard parser
This uses the gvas-converter library from https://github.com/13xforever/gvas-converter.

It is used in conjuction with https://streamer.bot/ and StreamElements to automatically execute the !addpoints command for the top 3 participants in each race.

The way it is set now, 1st place gets 1000 points, 2nd place 700 and 3rd place 500.

Unfortunately, the code is not very flexible, so if you want to have different values you need to edit and compile the code yourself.

## How to use <br/>

1. In Marbles On Stream create a new session with any name.

2. In settings, check "use custom points" and assign 1st place 3 points, 2nd place 2 points and 3rd place 1 point. (Make sure "Participation Points" is 0)

3. After that there should a .sav file in "AppData\Local\MarblesOnStream\Saved\SaveGames\Sessions" with the name of the session that you made.

4. In Streamer.bot import:
```
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
```
5. Go to Settings and then File/Folder Watcher add folder by clicking on the "..." and find "AppData\Local\MarblesOnStream\Saved\SaveGames\Sessions", Filter to "your_session_name".sav file and Action to PointAlloc.

6. In Sub-Actions set %save% argument value, in both Initializer and PointAlloc, to the full path of "your_session_name".sav in AppData\Local\MarblesOnStream\Saved\SaveGames\Sessions. (Make sure that it's not surronded by "")

7. Set %converter% argument, in both Initializer and PointAlloc, to the full path of GvasConverter.exe file. (Make sure that it's not in "")

8. Edit execute code, in both Initializer and PointAlloc, and add System.dll in the References tab at the bottom, if it's not already, and compile to see if there are any errors.

9. In commands, enable !manualinit if it's disabled.

10. Initialize once by typing !manualinit command in twitch chat. (This should create 2 extra files in "AppData\Local\MarblesOnStream\Saved\SaveGames\Sessions", a json and a txt)

After this, it should update automatically every time a race ends and it will assign the appropriate points to the chat member by type the commands.
