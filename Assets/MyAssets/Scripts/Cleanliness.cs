using UnityEngine;
using System.Collections;

public static class Cleanliness {

    /// <summary>
    /// ステージ全体の清潔度.
    /// </summary>
    public static float m_fCleanliness = 0.0f;

    public static void Initialize()
    {
        m_fCleanliness = 0.0f;
    }

}
