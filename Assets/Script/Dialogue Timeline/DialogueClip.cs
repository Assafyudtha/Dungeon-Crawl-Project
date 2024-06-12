using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueClip : PlayableAsset
{
    [TextArea]
    public string dialogueText;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogueBehaviour>.Create(graph);

        DialogueBehaviour dialogueBehaviour = playable.GetBehaviour();
        dialogueBehaviour.dialogueText = dialogueText;
        return playable;
    }
}
