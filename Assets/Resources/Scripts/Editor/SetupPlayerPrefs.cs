using UnityEditor;
using UnityEngine;

public class SetupPlayerPrefs
{
    [MenuItem("Tools/Setup Test PlayerPrefs")]
    public static void SetupPrefs()
    {
        PlayerPrefs.SetInt("Lv01", 1);
        PlayerPrefs.SetInt("Lv02", 1);
        PlayerPrefs.SetInt("Lv03", 1);
        PlayerPrefs.Save();

        Debug.Log("PlayerPrefs đã được thiết lập!");
    }

    [MenuItem("Tools/Clear All PlayerPrefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Tất cả PlayerPrefs đã bị xóa!");
    }
}