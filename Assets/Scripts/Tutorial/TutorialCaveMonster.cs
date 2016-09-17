using UnityEngine;
using System.Collections;

public class TutorialCaveMonster : CaveMonster {

    //Reference naar tutorialmanager nodig in de editor (public field)
    public TutorialManager tutorialManager;

	void OnTriggerEnter2D(Collider2D other) { //Bubbel geraakt
        tutorialManager.CompleteMission(0);
    }

}
