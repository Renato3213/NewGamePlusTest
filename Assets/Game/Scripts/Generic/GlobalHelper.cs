using UnityEngine;

public static class GlobalHelper 
{
    public static string GenerateUID(GameObject obj)
    {
        return $"{obj.scene.name}_{obj.transform.position.x}_{obj.transform.position.y}";
    }

    public static string GenerateUID(RectTransform obj)
    {
        return $"{obj.gameObject.scene.name}_{obj.position.x}_{obj.position.y}";
    }
}
