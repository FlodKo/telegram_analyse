    NICHT AUF MASTER BRANCH PUSHEN!
    
    Repository klonen: git clone https://github.com/bischoflu/chatbots.git
    Überprüfen ob man im beta branch ist(git status) wenn nicht mit dem befehl: git checkout beta
    Die aktuelle version laden: git pull
    Davon wegbranchen: git checkout -b name_des_branches
    Do Work
    Checken ob alles läuft.
    Wenn man neue Files erstellt hat: git add -A
    Veränderungen committen: git commit -am 'kommentar fuer den commit'
    Schauen ob in der zwischenzeit jemand anders was veraendert hat: git checkout beta & git pull & git checkout name_des_branches & git merge beta
    Falls nötig Mergekonflikte beheben geht in VSCode relativ simple wenn man auf diesen Versionskontrolle button klickt anschließend nochmal committen.
    Veränderungen pushen git push --set-upstream origin name_des_branches
    Auf der github seite einen pull request vom eienen branch nach beta starten
