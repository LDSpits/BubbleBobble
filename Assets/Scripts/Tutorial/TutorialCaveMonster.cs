using UnityEngine;
using System.Collections;

#if UNITY_EDITOR  
using UnityEditor;
#endif

public class TutorialCaveMonster : CaveMonster {

    //Reference naar tutorialmanager nodig in de editor (public field)
    public TutorialManager tutorialManager;

    void LateUpdate() {

        //Zorg ervoor dat we niet aan de zijkant springen
        if (transform.position.x < 9 || transform.position.x > 13) {
            randomJumping = false;
        }else {
            randomJumping = true;
        }

        //Zorg ervoor dat de tutorial cavemonster op het bovenste platform blijft
        if (transform.position.x < 7) {
            SetDirection(MoveDirection.right);
        }else if (transform.position.x > 15) {
            SetDirection(MoveDirection.left);
        }

        if (IsCaptured) { //Missie : Vang de vijand in een bubbel
            if (tutorialManager.GetCurrentMission() == 2) {
                tutorialManager.CompleteMission(2);
            }
        }
        else if (tutorialManager.GetCurrentMission() == 3) { //Ontsnapt uit de bubbel
            tutorialManager.SetCurrentMission(2);
        }

    }

    void OnDestroy() {
        tutorialManager.CompleteMission(3); // Missie: Vernietig de vijand
        tutorialManager.AutoCompleteMission(4); //Missie: Wacht na de vijand vernietigen
    }

}
