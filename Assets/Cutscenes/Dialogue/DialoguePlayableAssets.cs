using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
    public class DialoguePlayableAsset : PlayableAsset
    {
#pragma warning disable 649
        //[SerializeField] private PseudoDialogueSO dialogue;
        [SerializeField] private conversation dialogue;
#pragma warning restore 649

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<DialoguePlayableBehaviour>.Create(graph);
            var dialogueBehaviour = playable.GetBehaviour();
            //dialogueBehaviour.Dialogue = dialogue;
            dialogueBehaviour.Director = owner.GetComponent<PlayableDirector>();
            return playable;
        }
    }
