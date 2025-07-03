#if UNITY_EDITOR
using UnityEngine;

/* **************************************************************************
* 2023 Alan Mattanó, Soaring Stars lab }}
* 
* Overall, this script allows you to add notes
* or comments to GameObjects in the Unity Editor,
* making it easier to communicate essential information
* about GameObjects or their components to other developers
*
*           WARNING: DO NOT MODIFY
*           you will lose all data
*
* By wrapping the script inside the #if UNITY_EDITOR directive,
* it will only be compiled and executed in the Unity Editor.
* When you build your project, the script will be excluded from the build,
* and it won't impact the final game. It may need to add using UnityEditor;
* at the beginning of the script if you encounter any issues with the
* #if UNITY_EDITOR directive.
* ************************************************************************/

[AddComponentMenu("Miscellaneous/README Info Note")]
public class CommentInformationNote : MonoBehaviour
{
    [TextArea(17,1000)]
    public string comment = "Information Here.";

    void Awake()
    {
        comment = null;
       
        // Assuming you want to destroy this script component
        Destroy(this);
    }
}
#endif