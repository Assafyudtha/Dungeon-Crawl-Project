using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class DialogueScriptCutscene : MonoBehaviour, IPlayableBehaviour
{
    public GameObject dialogueCanvas;
    
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI textDialogUI;
    public DialogueSO currentDialogue;
    public float textSpeed;
    private int index;
    public void OnBehaviourPlay(PlayableDirector director, Playable playable)
    {
        // Get the DialogueData from the Timeline clip
        currentDialogue = (DialogueSO)director.GetGenericBinding(this);

        if (currentDialogue != null)
        {
            index = 0;
            DisplayDialogueLine();
        }
    }

    public void OnBehaviourPause(PlayableDirector director, Playable playable)
    {
        // Pause dialogue if needed (e.g., for player input)
    }

    public void OnBehaviourResume(PlayableDirector director, Playable playable)
    {
        // Resume dialogue playback
    }

    public void OnBehaviourStop(PlayableDirector director, Playable playable)
    {

    }


    private void DisplayDialogueLine()
    {
        if (index < currentDialogue.DialogBawah.Length)
        {
            textDialogUI.text = currentDialogue.DialogBawah[index];
            index++;
        }
        else
        {
            // Dialogue finished, handle next actions (e.g., resume timeline)
        }
    }

    public void OnGraphStart(PlayableDirector director)
    {
        // (Optional) Perform any actions needed when the Timeline graph starts
    }

    public void OnGraphStart(Playable playable)
    {
        throw new System.NotImplementedException();
    }

    public void OnGraphStop(Playable playable)
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayableCreate(Playable playable)
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayableDestroy(Playable playable)
    {
        throw new System.NotImplementedException();
    }

    public void OnBehaviourPlay(Playable playable, FrameData info)
    {
        throw new System.NotImplementedException();
    }

    public void OnBehaviourPause(Playable playable, FrameData info)
    {
        throw new System.NotImplementedException();
    }

    public void PrepareFrame(Playable playable, FrameData info)
    {
        throw new System.NotImplementedException();
    }

    public void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        throw new System.NotImplementedException();
    }

}
