using System.Linq;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public bool showFoldout;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameManager gameManager = (GameManager)target;

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("State Machine");

        if (gameManager.stateMachine == null) return;

        if (gameManager.stateMachine.CurrentBase != null)
        {
            EditorGUILayout.LabelField("Current State: ", gameManager.stateMachine.CurrentBase.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Avaiable States");

        if (showFoldout)
        {
            if (gameManager.stateMachine.dictionaryState != null)
            {
                var keys = gameManager.stateMachine.dictionaryState.Keys.ToArray();
                var values = gameManager.stateMachine.dictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], values[i]));
                }
            }
        }
    }
}
