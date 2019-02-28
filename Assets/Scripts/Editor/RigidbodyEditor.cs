using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    public class RigidbodyEditor : MonoBehaviour
    {
        [MenuItem("CONTEXT/Rigidbody/Toggle Constraints", false, 1100)]
        static void ToggleConstraints(MenuCommand command)
        {
            Rigidbody rb = (Rigidbody)command.context;
            if (rb.constraints == RigidbodyConstraints.FreezeAll)
                rb.constraints = RigidbodyConstraints.None;
            else
                rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        [MenuItem("CONTEXT/Rigidbody/Make Kinematic", false, 1110)]
        static void FreezeRigidbody(MenuCommand command)
        {
            Rigidbody rb = (Rigidbody)command.context;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}
