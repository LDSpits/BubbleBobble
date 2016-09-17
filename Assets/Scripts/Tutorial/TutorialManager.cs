using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialManager : MonoBehaviour {

    public Text tutorialTitle, tutorialText;

    //Missions
    private int currentMission = 0;
    private ArrayList missionTitle = new ArrayList();
    private ArrayList missionTitleColor = new ArrayList();
    private ArrayList missionText = new ArrayList();

    void Start () {
        //Voeg alle missies in
        AddMission(0, "Basisvaardigheden...", Color.yellow, "Loop naar links en rechts met de linker en rechter pijltjes of <color=#00FF15>A</color> & <color=#00FF15>D</color>.");
        AddMission(1, "Basisvaardigheden...", Color.yellow, "Spring met <color=#00FF15>pijl omhoog</color> of <color=#00FF15>W</color>.");
        AddMission(2, "Basisvaardigheden...", Color.yellow, "Vang vijanden in bubbels. Blaas bubbels met <color=#00FF15>Spatie</color>.");
        AddMission(3, "Goedzo!", Color.green, "Maak nu de bubbel kapot door er tegenaan te springen.");
        AddMission(4, "Geavanceerde vaardigheden...", new Color(1,0.5f,0.5f), "Spring op je bubbel om hogerop te komen.");

        //Toon de eerste missie
        DisplayMission(currentMission);
	}

    void Update() {
        if (currentMission == 0 && InputManager.Left || InputManager.Right) {
            CompleteMission(0);
        }
        else if (currentMission == 1 && InputManager.Jump) {
            CompleteMission(1);
        }
    }

    private void DisplayMission(int missionID) {
        //Verander de tekst van een opgegeven missie
        tutorialTitle.text = (string)missionTitle[missionID];
        tutorialTitle.color = (Color)missionTitleColor[missionID];
        tutorialText.text = (string)missionText[missionID];
    }

    private void AddMission(int missionID, string mTitle, Color mTitleColor, string mText) {
        missionTitle.Insert(missionID, mTitle);           //Titel van de missie
        missionTitleColor.Insert(missionID, mTitleColor); //Kleur van de titel van de missie
        missionText.Insert(missionID, mText);             //Tekst van de missie
    }

    public void CompleteMission(int missionID) {
        //Als de missie die voltooid moest worden is opgestuurd...
        if (missionID == currentMission) {
            //Verhoog en vernieuw de huidige missie dan
            currentMission++;
            DisplayMission(currentMission);
        }
    }

}
